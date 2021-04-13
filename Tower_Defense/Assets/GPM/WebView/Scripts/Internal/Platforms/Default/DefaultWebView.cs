namespace Gpm.WebView.Internal
{
    using System.Collections.Generic;

    public class DefaultWebView : IWebView
    {
        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            // TODO
        }

        public void ShowHtmlFile(
            string fileName,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            // TODO
        }

        public void ShowHtmlString(
            string source,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent)
        {
            // TODO
        }

        public void Close()
        {
            // TODO
        }

        public void ExecuteJavaScript(string script)
        {
            // TODO
        }

        public void SetFileDownloadPath(string path)
        {
            // TODO
        }
    }
}
