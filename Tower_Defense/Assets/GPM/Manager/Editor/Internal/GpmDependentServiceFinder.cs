using Gpm.Common.Util;
using Gpm.Manager.Constant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Gpm.Manager.Internal
{
    internal static class GpmDependentServiceFinder
    {
        public static void Process(ServiceInfo findService, List<InstallInfo.Service> installedServices, Action<ManagerError> callback)
        {
            if (installedServices.Count <= 0)
            {
                callback(null);
                return;
            }

            EditorCoroutine.Start(RefreshDepedencyInfo(installedServices, findService.title,
                (dependecies, error) =>
                {
                    if (error != null)
                    {
                        callback(error);
                        return;
                    }

                    if (dependecies.Count == 0)
                    {
                        callback(null);
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (var service in dependecies)
                        {
                            builder.AppendFormat("\n- {0}", service);
                        }

                        callback(new ManagerError(ManagerErrorCode.UNINSTALL, string.Format(ManagerInfos.GetString(ManagerStrings.ERROR_MESSAGE_DEPENDENCY_SERVICE_REMOVE_FAILED), builder.ToString()), isFullMessage : true));
                    }
                }));
        }

        private static IEnumerator RefreshDepedencyInfo(List<InstallInfo.Service> installedServices, string findServiceName, Action<List<string>, ManagerError> callback)
        {
            List<string> dependencyServices = new List<string>();

            ManagerError occurrenceError = null;
            foreach (var service in installedServices)
            {
                IEnumerator innerCoroutineEnumerator = RefreshServiceInfo(service.name, (serviceInfo, error) => 
                {
                    occurrenceError = error;

                    if (serviceInfo != null && serviceInfo.dependencies.ContainsKey(findServiceName) == true)
                    {
                        dependencyServices.Add(serviceInfo.title);
                    }
                });

                while (innerCoroutineEnumerator.MoveNext() == true)
                {
                    yield return innerCoroutineEnumerator.Current;
                }

                if (occurrenceError != null)
                {
                    callback(null, occurrenceError);
                    yield break;
                }
            }

            callback(dependencyServices, null);
        }

        private static IEnumerator RefreshServiceInfo(string serviceName, Action<ServiceInfo, ManagerError> callback)
        {
            string url = GpmPathUtil.Combine(GpmManager.CdnUri, serviceName, ManagerPaths.SERVICE_FILE_NAME);
            ServiceInfo service = null;
            bool isDone = false;
            bool isError = false;
            string errorMessage = string.Empty;
            string errorSubMessage = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            errorMessage = ManagerStrings.ERROR_MESSAGE_NETWORK;
                            errorSubMessage = string.Format("Code= {0}", response.StatusCode);
                            isError = true;
                            isDone = true;
                        }

                        string text = reader.ReadToEnd();
                        XmlHelper.LoadXmlFromText<ServiceInfo>(
                            text,
                            (responseCode, xmlData, message) =>
                            {
                                if (responseCode != XmlHelper.ResponseCode.SUCCESS)
                                {
                                    errorMessage = ManagerStrings.ERROR_MESSAGE_SERVICE_INFO_LOAD_FAILED;
                                    errorSubMessage = string.Format("Service= {0}, Code= {1}, Message= {2}", serviceName, responseCode, message);
                                    isDone = true;
                                    return;
                                }

                                service = xmlData;
                                isDone = true;
                            });
                    }
                }
            }

            while (isDone == false)
            {
                yield return null;
            }

            if (isError == true)
            {
                callback(service, new ManagerError(ManagerErrorCode.UNINSTALL, errorMessage, errorSubMessage));
            }
            else
            {
                callback(service, null);
            }
        }
    }
}