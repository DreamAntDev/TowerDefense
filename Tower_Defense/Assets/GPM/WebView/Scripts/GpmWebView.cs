namespace Gpm.WebView
{
    using System.Collections.Generic;
    using Gpm.WebView.Internal;

    public static class GpmWebView
    {
        public const string VERSION = "1.2.0";

        /// <summary>
        /// Create the webview and loads the web content referenced by the specified URL.
        /// </summary>
        /// <param name="url">The URL of the resource to load.</param>
        /// <param name="configuration">The configuration of GPM WebWiew. Refer to <see cref="GpmWebViewRequest.Configuration"/></param>
        /// <param name="openCallback">Notifies users when a WebView is opened.</param>
        /// <param name="closeCallback">Notifies users when a WebView is closed.</param>
        /// <param name="schemeList">Specifies the list of customized schemes a user wants.</param>
        /// <param name="schemeEvent">Notifies url including customized scheme specified by the schemeList with a callback.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void ShowUrl()
        /// {
        ///     GpmWebView.ShowUrl(
        ///         "http://gameplatform.toast.com/",
        ///         new GpmWebViewRequest.Configuration()
        ///         {
        ///             style = GpmWebViewStyle.FULLSCREEN,
        ///             isClearCookie = false,
        ///             isClearCache = false,
        ///             isNavigationBarVisible = true,
        ///             title = "The page title.",
        ///             isBackButtonVisible = true,
        ///             isForwardButtonVisible = true,
        /// #if UNITY_IOS
        ///             contentMode = GpmWebViewContentMode.MOBILE
        /// #endif
        ///         },
        ///         OnOpenCallback,
        ///         OnCloseCallback,
        ///         new List&lt;string>()
        ///         {
        ///             "USER_ CUSTOM_SCHEME"
        ///         },
        ///         OnSchemeEvent);
        /// }
        /// 
        /// private void OnOpenCallback(GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnOpenCallback] succeeded.");
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnOpenCallback] failed. error:{0}", error));
        ///     }
        /// }
        /// 
        /// private void OnCloseCallback(GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnCloseCallback] succeeded.");
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnCloseCallback] failed. error:{0}", error));
        ///     }
        /// }
        /// 
        /// private void OnSchemeEvent(string data, GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnSchemeEvent] succeeded.");
        ///         
        ///         if (data.Equals("USER_ CUSTOM_SCHEME") == true || data.Contains("CUSTOM_SCHEME") == true)
        ///         {
        ///             Debug.Log(string.Format("scheme:{0}", data));
        ///         }
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnSchemeEvent] failed. error:{0}", error));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            WebViewImplementation.Instance.ShowUrl(url, configuration, openCallback, closeCallback, schemeList, schemeEvent);
        }

        /// <summary>
        /// Create the webview and loads the web content from the specified file.
        /// </summary>
        /// <param name="filePath">The URL of a file that contains web content. This URL must be a file-based URL.</param>
        /// <param name="configuration">The configuration of GPM WebWiew. Refer to <see cref="GpmWebViewRequest.Configuration"/></param>
        /// <param name="openCallback">Notifies users when a WebView is opened.</param>
        /// <param name="closeCallback">Notifies users when a WebView is closed.</param>
        /// <param name="schemeList">Specifies the list of customized schemes a user wants.</param>
        /// <param name="schemeEvent">Notifies url including customized scheme specified by the schemeList with a callback.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void ShowHtmlFile()
        /// {
        ///     var htmlFilePath = string.Empty;
        /// #if UNITY_IOS
        ///     htmlFilePath = string.Format("file://{0}/{1}", Application.streamingAssetsPath, "test.html");
        /// #elif UNITY_ANDROID
        ///     htmlFilePath = string.Format("file:///android_asset/{0}", "test.html");
        /// #endif
        ///     
        ///     GpmWebView.ShowHtmlFile(
        ///         htmlFilePath,
        ///         new GpmWebViewRequest.Configuration()
        ///         {
        ///             style = GpmWebViewStyle.FULLSCREEN,
        ///             isClearCookie = false,
        ///             isClearCache = false,
        ///             isNavigationBarVisible = true,
        ///             title = "The page title.",
        ///             isBackButtonVisible = true,
        ///             isForwardButtonVisible = true,
        /// #if UNITY_IOS
        ///             contentMode = GpmWebViewContentMode.MOBILE
        /// #endif
        ///         },
        ///         OnOpenCallback,
        ///         OnCloseCallback,
        ///         new List&lt;string>()
        ///         {
        ///             "USER_ CUSTOM_SCHEME"
        ///         },
        ///         OnSchemeEvent);
        /// }
        /// 
        /// private void OnOpenCallback(GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnOpenCallback] succeeded.");
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnOpenCallback] failed. error:{0}", error));
        ///     }
        /// }
        /// 
        /// private void OnCloseCallback(GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnCloseCallback] succeeded.");
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnCloseCallback] failed. error:{0}", error));
        ///     }
        /// }
        /// 
        /// private void OnSchemeEvent(string data, GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnSchemeEvent] succeeded.");
        ///         
        ///         if (data.Equals("USER_ CUSTOM_SCHEME") == true || data.Contains("CUSTOM_SCHEME") == true)
        ///         {
        ///             Debug.Log(string.Format("scheme:{0}", data));
        ///         }
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnSchemeEvent] failed. error:{0}", error));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void ShowHtmlFile(
            string filePath,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            WebViewImplementation.Instance.ShowHtmlFile(filePath, configuration, openCallback, closeCallback, schemeList, schemeEvent);
        }

        /// <summary>
        /// Create the webview and loads the contents of the specified HTML string.
        /// </summary>
        /// <param name="htmlString">The string to use as the contents of the webpage.</param>
        /// <param name="configuration">The configuration of GPM WebWiew. Refer to <see cref="GpmWebViewRequest.Configuration"/></param>
        /// <param name="openCallback">Notifies users when a WebView is opened.</param>
        /// <param name="closeCallback">Notifies users when a WebView is closed.</param>
        /// <param name="schemeList">Specifies the list of customized schemes a user wants.</param>
        /// <param name="schemeEvent">Notifies url including customized scheme specified by the schemeList with a callback.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void ShowHtmlString()
        /// {
        ///     GpmWebView.ShowHtmlString(
        ///         "${HTML_STRING}",
        ///         new GpmWebViewRequest.Configuration()
        ///         {
        ///             style = GpmWebViewStyle.FULLSCREEN,
        ///             isClearCookie = false,
        ///             isClearCache = false,
        ///             isNavigationBarVisible = true,
        ///             title = "The page title.",
        ///             isBackButtonVisible = true,
        ///             isForwardButtonVisible = true,
        /// #if UNITY_IOS
        ///             contentMode = GpmWebViewContentMode.MOBILE
        /// #endif
        ///         },
        ///         OnOpenCallback,
        ///         OnCloseCallback,
        ///         new List&lt;string>()
        ///         {
        ///             "USER_ CUSTOM_SCHEME"
        ///         },
        ///         OnSchemeEvent);
        /// }
        /// 
        /// private void OnOpenCallback(GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnOpenCallback] succeeded.");
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnOpenCallback] failed. error:{0}", error));
        ///     }
        /// }
        /// 
        /// private void OnCloseCallback(GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnCloseCallback] succeeded.");
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnCloseCallback] failed. error:{0}", error));
        ///     }
        /// }
        /// 
        /// private void OnSchemeEvent(string data, GpmWebViewError error)
        /// {
        ///     if (error == null)
        ///     {
        ///         Debug.Log("[OnSchemeEvent] succeeded.");
        ///         
        ///         if (data.Equals("USER_ CUSTOM_SCHEME") == true || data.Contains("CUSTOM_SCHEME") == true)
        ///         {
        ///             Debug.Log(string.Format("scheme:{0}", data));
        ///         }
        ///     }
        ///     else
        ///     {
        ///         Debug.Log(string.Format("[OnSchemeEvent] failed. error:{0}", error));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void ShowHtmlString(
            string htmlString,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            WebViewImplementation.Instance.ShowHtmlString(htmlString, configuration, openCallback, closeCallback, schemeList, schemeEvent);
        }

        /// <summary>
        /// Execute the specified JavaScript string.
        /// </summary>
        /// <param name="script">The JavaScript string to execute.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void ExecuteJavaScriptSample()
        /// {
        ///     GpmWebView.ExecuteJavaScript("alert('ExecuteJavaScript');");
        /// }
        /// </code>
        /// </example>
        public static void ExecuteJavaScript(string script)
        {
            WebViewImplementation.Instance.ExecuteJavaScript(script);
        }

        /// <summary>
        /// Close currently displayed WebView.
        /// </summary>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void CloseSample()
        /// {
        ///     GpmWebView.Close();
        /// }
        /// </code>
        /// </example>
        public static void Close()
        {
            WebViewImplementation.Instance.Close();
        }
    }
}