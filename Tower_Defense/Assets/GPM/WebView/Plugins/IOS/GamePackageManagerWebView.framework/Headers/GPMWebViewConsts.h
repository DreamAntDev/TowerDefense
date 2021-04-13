//
//  GPMWebViewConsts.h
//  GamePackageManagerWebView
//
//  Created by NHN on 2020/12/04.
//  Copyright © 2020 NHN. All rights reserved.
//

#ifndef GPMWebViewConsts_h
#define GPMWebViewConsts_h

#pragma mark - GPMWebViewError Codes
typedef NS_ENUM(NSInteger, GPMWebViewErrorCode) {
    GPM_WEBVIEW_ERROR_ALREADY_OPEN      = 1,
    GPM_WEBVIEW_ERROR_INVALID_PARAMETER = 2,
    GPM_WEBVIEW_ERROR_INVALID_URL       = 3,
    GPM_WEBVIEW_ERROR_TIMEOUT           = 11,
    GPM_WEBVIEW_ERROR_EXTERNAL          = 101,
    GPM_WEBVIEW_ERROR_UNKNOWN           = 999
};

/** These constants indicate the type of launch style such as popup, fullscreen.
 * @see style
 */
typedef NS_ENUM(NSUInteger, GPMWebViewStyle) {
    GPMWebViewLaunchPopUp        = 0,
    GPMWebViewLaunchFullScreen   = 1,
};

/** Configure webview's content mode.
 GPMWebViewContentMode represents the type of content to load
 * @see contentMode
 */
typedef NS_ENUM(NSInteger, GPMWebViewContent) {
    /** Recommended content mode
     * Default value. The recommended content mode for the current platform.
     */
    GPMWebViewContentModeRecommended = 0,

    /** mobile browsers
     *  Represents content targeting mobile browsers.
     */
    GPMWebViewContentModeMobile      = 1,

    /** desktop browsers
     * Represents content targeting desktop browsers.
     */
    GPMWebViewContentModeDesktop     = 2,
};

#endif /* GPMWebViewConsts_h */
