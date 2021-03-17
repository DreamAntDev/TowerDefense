using Gpm.Common.Util;
using System;
using UnityEngine;

namespace Gpm.Common.Indicator.Internal
{
    public sealed class InAppIndicator : BaseIndicator
    {
        private MonoBehaviour monoObject;

        public InAppIndicator()
        {
            monoObject = GameObjectContainer.GetGameObject(GpmIndicator.SERVICE_NAME).GetComponent<MonoBehaviour>();
            Initialize();
        }

        protected override void GetLaunchingInfo(UnityWebRequestHelper helper, Action<LaunchingInfo> callback)
        {
            monoObject.StartCoroutine(helper.SendWebRequest(result =>
            {
                if (CheckInvalidResult(result) == true)
                {
                    callback(null);
                }
                else
                {
                    var launchingInfo = GpmJsonMapper.ToObject<LaunchingInfo>(result.downloadHandler.text);
                    callback(launchingInfo);
                }
            }));
        }

        protected override void ExecuteQueueDelegate()
        {
            monoObject.StartCoroutine(ExecuteQueue());
        }

        protected override void SendLogNCrash(UnityWebRequestHelper helper, byte[] sendData)
        {
            monoObject.StartCoroutine(helper.SendWebRequest((result) =>
            {
                queueItem.isRunning = false;
            }));
        }

        protected override void SetQueueItemStatus()
        {
            queueItem.isRunning = true;
        }

        protected override bool IsWaitingInChild()
        {
            if (queueItem != null && queueItem.isRunning == true)
            {
                return true;
            }

            return false;
        }
    }
}