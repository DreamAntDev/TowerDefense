using Gpm.Common.Util;
using System;

namespace Gpm.Common.Indicator.Internal
{
    public class EditorIndicator : BaseIndicator
    {
        public EditorIndicator()
        {
            Initialize();
        }

        protected override void GetLaunchingInfo(UnityWebRequestHelper helper, Action<LaunchingInfo> callback)
        {
            EditorCoroutine.Start(helper.SendWebRequest(result =>
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
            EditorCoroutine.Start(ExecuteQueue());
        }

        protected override void SendLogNCrash(UnityWebRequestHelper helper, byte[] sendData)
        {
            EditorCoroutine.Start(helper.SendWebRequest());
        }
    }
}