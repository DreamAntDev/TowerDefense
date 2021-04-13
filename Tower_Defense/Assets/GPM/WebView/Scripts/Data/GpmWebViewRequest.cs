namespace Gpm.WebView
{
    public static class GpmWebViewRequest
    {
        public class Configuration
        {
            /// <summary>
            /// These constants indicate the type of launch style such as popup, fullscreen.
            /// Refer to <see cref="GpmWebViewStyle"/>
            /// </summary>
            public int style;

            /// <summary>
            /// Clear cookies.
            /// </summary>
            public bool isClearCookie;

            /// <summary>
            /// Clear cache.
            /// </summary>
            public bool isClearCache;

            /// <summary>
            /// Sets the visibility of the navigation bar.
            /// </summary>
            public bool isNavigationBarVisible;

            /// <summary>
            /// The page title.
            /// </summary>
            public string title;

            /// <summary>
            /// Sets the visibility of the back button.
            /// </summary>
            public bool isBackButtonVisible;

            /// <summary>
            /// Sets the visibility of the forward button.
            /// </summary>
            public bool isForwardButtonVisible;

            /// <summary>
            /// iOS only.
            /// The content mode for the web view to use when it loads and renders a webpage.
            /// Refer to <see cref="GpmWebViewContentMode"/>
            /// </summary>
            public int contentMode;
        }
    }
}
