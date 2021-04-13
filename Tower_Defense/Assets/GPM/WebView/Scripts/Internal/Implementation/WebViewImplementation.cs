namespace Gpm.WebView.Internal
{
    using System.Collections.Generic;

    public class WebViewImplementation
    {
        private static readonly WebViewImplementation instance = new WebViewImplementation();

        public static WebViewImplementation Instance
        {
            get { return instance; }
        }

        private IWebView webview;

        private WebViewImplementation()
        {
#if UNITY_ANDROID
            webview = new AndroidWebView();
#elif UNITY_IPHONE
            webview = new IOSWebView();
#else
            webview = new DefaultWebView();
#endif
        }

        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            webview.ShowUrl(url, configuration, openCallback, closeCallback, schemeList, schemeEvent);
        }

        public void ShowHtmlFile(
            string filePath,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            webview.ShowHtmlFile(filePath, configuration, openCallback, closeCallback, schemeList, schemeEvent);
        }

        public void ShowHtmlString(
            string htmlString,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            webview.ShowHtmlString(htmlString, configuration, openCallback, closeCallback, schemeList, schemeEvent);
        }

        public void Close()
        {
            webview.Close();
        }

        public void ExecuteJavaScript(string script)
        {
            webview.ExecuteJavaScript(script);
        }

        public void SetFileDownloadPath(string path)
        {
            webview.SetFileDownloadPath(path);
        }
    }
}
