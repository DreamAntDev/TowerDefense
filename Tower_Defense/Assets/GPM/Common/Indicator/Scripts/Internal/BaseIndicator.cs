using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Networking;

namespace Gpm.Common.Indicator.Internal
{
    public abstract class BaseIndicator
    {
        protected abstract void GetLaunchingInfo(UnityWebRequestHelper helper, Action<LaunchingInfo> callback);
        protected abstract void ExecuteQueueDelegate();
        protected abstract void SendLogNCrash(UnityWebRequestHelper helper, byte[] sendData);

        protected LaunchingInfo.Launching.Indicator.Zone indicatorInfo;
        protected Queue<QueueItem> queue;
        protected QueueItem queueItem;

        public BaseIndicator()
        {
            queue = new Queue<QueueItem>();
        }

        public void Send(string serviceName, string serviceVersion, Dictionary<string, string> customData, bool ignoreActivation = false)
        {
            queue.Enqueue(new QueueItem(serviceName, serviceVersion, customData, ignoreActivation));
        }

        protected void Initialize()
        {
            var request = UnityWebRequest.Get(string.Format("{0}/{1}/appkeys/{2}/configurations", Launching.URI, Launching.VERSION, Launching.APP_KEY));
            request.method = UnityWebRequest.kHttpVerbGET;

            var helper = new UnityWebRequestHelper(request);
            GetLaunchingInfo(helper, (launchingInfo) =>
            {
                if (launchingInfo == null)
                {
                    return;
                }

                if (launchingInfo.header.isSuccessful == true)
                {
                    indicatorInfo = launchingInfo.launching.indicator.real;
                    SetDevelopmentZone(launchingInfo);

                    ExecuteQueueDelegate();
                }
            });
        }

        [Conditional("GPM_INDICATOR_DEVELOPMENT")]
        private void SetDevelopmentZone(LaunchingInfo launchingInfo)
        {
            indicatorInfo = launchingInfo.launching.indicator.alpha;
        }

        protected IEnumerator ExecuteQueue()
        {
            while (true)
            {
                if (IsWaiting() == true)
                {
                    yield return null;
                }
                else
                {
                    queueItem = queue.Dequeue();
                    SetQueueItemStatus();

                    if (CanExecutable(queueItem.ignoreActivation) == true)
                    {
                        byte[] sendData =
                        IndicatorField.CreateSendData(
                            indicatorInfo.appKey,
                            indicatorInfo.logVersion,
                            queueItem.serviceName,
                            queueItem.serviceVersion,
                            queueItem.customData);

                        var request = UnityWebRequest.Put(string.Format("{0}/{1}/log", indicatorInfo.url, indicatorInfo.logVersion), sendData);
                        request.method = UnityWebRequest.kHttpVerbPOST;
                        var helper = new UnityWebRequestHelper(request);

                        SendLogNCrash(helper, sendData);
                    }
                }
            }
        }

        protected virtual void SetQueueItemStatus()
        {
            queueItem.isRunning = false;
        }

        protected virtual bool IsWaitingInChild()
        {
            return false;
        }

        protected bool CheckInvalidResult(UnityWebRequest result)
        {
            return ((result == null) || (string.IsNullOrEmpty(result.downloadHandler.text) == true));
        }

        private bool IsWaiting()
        {
            if (indicatorInfo == null)
            {
                return true;
            }

            if (queue.Count == 0)
            {
                return true;
            }

            return IsWaitingInChild();
        }

        protected bool CanExecutable(bool ignoreActivation)
        {
            if (indicatorInfo.activation.Equals("off", StringComparison.Ordinal) == true)
            {
                if (ignoreActivation == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
