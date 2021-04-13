namespace Gpm.WebView
{
    public static class GpmWebViewCallback
    {
        public delegate void GpmWebViewErrorDelegate(GpmWebViewError error);
        public delegate void GpmWebViewDelegate<T>(T data, GpmWebViewError error);
    }
}
