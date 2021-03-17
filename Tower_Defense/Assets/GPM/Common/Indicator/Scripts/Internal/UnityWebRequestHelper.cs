using Gpm.Common.Log;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine.Networking;

namespace Gpm.Common.Indicator.Internal
{
    public class UnityWebRequestHelper
    {
        private const int TIMEOUT = 10;

        private UnityWebRequest request;

        public UnityWebRequestHelper(UnityWebRequest request)
        {
            this.request = request;
        }

        public IEnumerator SendWebRequest(Action<UnityWebRequest> callback = null)
        {
            request.timeout = TIMEOUT;
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            while (request.isDone == false)
            {
                yield return null;
            }

            PrintLog();

            if (callback != null)
            {
                callback(request);
            }
        }

        [Conditional("GPM_INDICATOR_DEBUGGING")]
        private void PrintLog()
        {
            if (IsSuccess() == false)
            {
                GpmLogger.Debug(
                        string.Format(
                            "Failed to send request. responseCode:{0}, error:{1}, text:{2}",
                            request.responseCode,
                            request.error,
                            request.downloadHandler.text), GpmIndicator.SERVICE_NAME, GetType());
            }
        }

        private bool IsSuccess()
        {
            if (request.responseCode != 200)
            {
                return false;
            }

            if (request.isNetworkError == true)
            {
                return false;
            }

            if (string.IsNullOrEmpty(request.downloadHandler.text) == true)
            {
                return false;
            }

            return true;
        }
    }
}