using Gpm.Common.Util;
using Gpm.Manager.Constant;
using Gpm.Manager.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Gpm.Manager.Internal
{
    internal class GpmServiceDownloader
    {
        private const int DOWNLOAD_BUFFER = 32 * 1024;

        private readonly Queue<PackageInstallInfo> downloadQueue = new Queue<PackageInstallInfo>();

        public void Process(ServiceInfo service, List<string> dependencyServices, Action<ManagerError, List<PackageInstallInfo>> callback)
        {
            Action downloadAction = () =>
            {
                ServiceInfo.Package package = service.GetPackage(service.version);
                if(package == null)
                {
                    callback(new ManagerError(ManagerErrorCode.INSTALL,
                                    string.Format(ManagerInfos.GetString(ManagerStrings.ERROR_MESSAGE_DEPENDENCY_SERVICE_INSTALL_FAILED), service.title),
                                    ManagerInfos.GetString(ManagerStrings.UNITY_NOT_SUPPORT_VERSION)), null);
                    return;
                }

                foreach (var install in package.installList)
                {
                    downloadQueue.Enqueue(new PackageInstallInfo
                    {
                        serviceName = service.title,
                        serviceVersion = service.version,
                        packageName = install.name,
                        packageIntallPath = install.path
                    });
                }

                EditorCoroutine.Start(DownloadPackages(callback));
            };

            if (dependencyServices.Count <= 0)
            {
                downloadAction();
                return;
            }

            EditorCoroutine.Start(RefreshDepedencyInfo(dependencyServices,
                (error) =>
                {
                    if (error == null)
                    {
                        downloadAction();
                    }
                    else
                    {
                        callback(error, null);
                    }
                }));
        }

        private IEnumerator RefreshDepedencyInfo(List<string> dependencyServices, Action<ManagerError> callback)
        {
            ManagerError occurrenceError = null;
            foreach (string serviceName in dependencyServices)
            {
                IEnumerator innerCoroutineEnumerator = RefreshServiceInfo(serviceName, (error) => { occurrenceError = error; });
                while (innerCoroutineEnumerator.MoveNext() == true)
                {
                    yield return innerCoroutineEnumerator.Current;
                }

                if (occurrenceError != null)
                {
                    callback(occurrenceError);
                    yield break;
                }
            }

            callback(null);
        }

        private IEnumerator RefreshServiceInfo(string serviceName, Action<ManagerError> callback)
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
                callback(new ManagerError(ManagerErrorCode.INSTALL, errorMessage, errorSubMessage));
            }
            else
            {
                bool installableUnityVersion = StringUtil.IsInstallableUnityVersion(service.dependencies[ManagerInfos.DEPENDENCY_UNITY_INFO_KEY].version);
                if (installableUnityVersion == true)
                {
                    ServiceInfo.Package package = service.GetPackage(service.version);
                    if (package != null)
                    {
                        foreach (var install in package.installList)
                        {
                            downloadQueue.Enqueue(new PackageInstallInfo
                            {
                                serviceName = service.title,
                                serviceVersion = service.version,
                                packageName = install.name,
                                packageIntallPath = install.path
                            });
                        }
                        callback(null);
                    }
                    else
                    {
                        callback(new ManagerError(ManagerErrorCode.INSTALL,
                                    string.Format(ManagerInfos.GetString(ManagerStrings.ERROR_MESSAGE_DEPENDENCY_SERVICE_INSTALL_FAILED), service.title),
                                    ManagerInfos.GetString(ManagerStrings.UNITY_NOT_SUPPORT_VERSION)));
                    }
                }
                else
                {
                    callback(new ManagerError(ManagerErrorCode.INSTALL, 
                        string.Format(ManagerInfos.GetString(ManagerStrings.ERROR_MESSAGE_DEPENDENCY_SERVICE_INSTALL_FAILED), service.title),
                        ManagerInfos.GetString(ManagerStrings.UNITY_NOT_SUPPORT_VERSION)));
                }
            }
        }

        private IEnumerator DownloadPackages(Action<ManagerError, List<PackageInstallInfo>> callback)
        {
            int downloadCount = downloadQueue.Count;
            List<PackageInstallInfo> downloadedList = new List<PackageInstallInfo>();

            ManagerError error = null;

            while (downloadQueue.Count > 0)
            {
                var downloadInfo = downloadQueue.Dequeue();

                string downloadUrl = GpmPathUtil.UrlCombine(GpmManager.CdnUri, downloadInfo.serviceName, downloadInfo.packageName);
                string localFilePath = ManagerPaths.GetCachingPath(downloadInfo.serviceName, downloadInfo.serviceVersion, downloadInfo.packageName);

                if (File.Exists(localFilePath) == true)
                {
                    downloadCount--;
                    downloadedList.Add(downloadInfo);
                }
                else
                {
                    string localPath = Path.GetDirectoryName(localFilePath);
                    if (Directory.Exists(localPath) == false)
                    {
                        Directory.CreateDirectory(localPath);
                    }

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(downloadUrl);
                    request.Method = "GET";
                    request.Timeout = 30000;
                    request.AllowWriteStreamBuffering = false;

                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    Action<Exception> errorProcess = (exception) =>
                    {
                        downloadCount--;

                        if (error != null)
                        {
                            return;
                        }

                        if (IsNetworkError(exception) == true)
                        {
                            error = new ManagerError(ManagerErrorCode.NETWORK, ManagerStrings.ERROR_MESSAGE_NETWORK, exception.Message);
                        }
                        else
                        {
                            error = new ManagerError(ManagerErrorCode.INSTALL, ManagerStrings.ERROR_MESSAGE_DOWNLOAD_FAILED, exception.Message);
                        }
                    };

                    try
                    {
                        request.BeginGetResponse(
                            (result) =>
                            {
                                try
                                {
                                    using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result))
                                    {
                                        if (response.StatusCode != HttpStatusCode.OK)
                                        {
                                            errorProcess(null);
                                        }
                                        else
                                        {
                                            using (var resStream = response.GetResponseStream())
                                            {
                                                using (var fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                                                {
                                                    var buffer = new byte[DOWNLOAD_BUFFER];
                                                    int bytesRead;
                                                    while ((bytesRead = resStream.Read(buffer, 0, buffer.Length)) > 0)
                                                    {
                                                        fs.Write(buffer, 0, bytesRead);
                                                    }

                                                    downloadCount--;
                                                    downloadedList.Add(downloadInfo);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    errorProcess(e);
                                }
                            }, null);
                    }
                    catch (Exception e)
                    {
                        errorProcess(e);
                    }
                }
            }

            while (downloadCount > 0)
            {
                yield return null;
            }
            
            callback(error, downloadedList);
        }

        private bool IsNetworkError(Exception exception)
        {
            WebException webException = exception as WebException;
            if (webException == null)
            {
                return false;
            }

            return webException.Status == WebExceptionStatus.ReceiveFailure ||
                webException.Status == WebExceptionStatus.ConnectFailure ||
                webException.Status == WebExceptionStatus.KeepAliveFailure;
        }
    }
}