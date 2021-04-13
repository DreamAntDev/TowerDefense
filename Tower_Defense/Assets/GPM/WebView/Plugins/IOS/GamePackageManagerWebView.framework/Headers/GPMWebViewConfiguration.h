//
//  GPMWebViewConfiguration.h
//  GPMWebView
//
//  Created by NHN on 2020/11/23.
//  Copyright © 2020 NHN. All rights reserved.
//

#ifndef GPMWebViewConfiguration_h
#define GPMWebViewConfiguration_h

#import <UIKit/UIKit.h>
#import "GPMWebViewDelegate.h"
#import "GPMWebViewConsts.h"

typedef NSInteger GPMWebViewContentMode;

/** The GPMWebViewConfiguration configures the behavior of webview launching.
 */
@interface GPMWebViewConfiguration : NSObject

/**---------------------------------------------------------------------------------------
 * @name Style
 *  ---------------------------------------------------------------------------------------
 */

/**
 Launching Style.
 
 @warning Default is a GPMWebViewLaunchFullScreen.
 */
@property (atomic) GPMWebViewStyle style;


/**---------------------------------------------------------------------------------------
 * @name Orientation
 *  ---------------------------------------------------------------------------------------
 */

/**
Boolean value.

@warning Default value is NO.
*/
@property (assign) BOOL isClearCookie;

/**
Boolean value.

@warning Default value is NO.
*/
@property (assign) BOOL isClearCache;


/**
 Orientation Mask.
 
 @see GPMWebViewOrientation.
 @warning Default is a value that is converted to application's orientation value toward GPMWebViewOrientation bit mask.
 */
@property (atomic, setter=setOrientationMask:) UIInterfaceOrientationMask orientationMask;

/**---------------------------------------------------------------------------------------
 * @name ContentMode
 *  ---------------------------------------------------------------------------------------
 */

/**
 Content Mode.
 
 @see GPMWebViewContent
 @warning Default is a GPMWebViewContentModeRecommended.
 */
@property (atomic, setter=setContentMode:) GPMWebViewContentMode contentMode;

/**---------------------------------------------------------------------------------------
 * @name Navigation
 *  ---------------------------------------------------------------------------------------
 */

/**
 Navigation Bar Color.
 
 Only used in full screen launch style.
 @warning Default value is blue.
 */
@property (strong, nonatomic) UIColor *navigationBarColor;

/**
 Navigation Bar Title.
 
 Only used in full screen launch style.
 @warning Default value is html document's title. If it was set, this title would be displayed.
 */
@property (strong, nonatomic) NSString *navigationBarTitle;

/**
 Boolean value.
 
 @warning Default value is YES. It is only used in FullScreen Style.
 */
@property (assign) BOOL isNavigationBarVisible;

/**
 Boolean value.
 
 @warning Default value is YES.
 */
@property (assign) BOOL isBackButtonVisible;

/**
Boolean value.

@warning Default value is YES.
*/
@property (assign) BOOL isForwardButtonVisible;

/**
 Close Image Path.
 
 Only used in full screen launch style.
 @warning Default value is "gpm-webview-cancel-white.png" in GPMWebView.bundle
 */
@property (strong, nonatomic) NSString *closeImagePathForFullScreenNavigation;

/**
 Back Button Image Path.
 
 Only used in full screen launch style.
 @warning Default value is "gpm-webview-goback-white.png" in GPMWebView.bundle
 */
@property (strong, nonatomic) NSString *goBackImagePathForFullScreenNavigation;

/**
Forwrad Button Image Path.

Only used in full screen launch style.
@warning Default value is "gpm-webview-forward-white.png" in GPMWebView.bundle
*/
@property (strong, nonatomic) NSString *goForwardImagePathForFullScreenNavigation;

/**---------------------------------------------------------------------------------------
 * @name Popup
 *  ---------------------------------------------------------------------------------------
 */

/**
 Alpha value of the background.
 
 Only used in popup launch style.
 @warning This value should be between 0 and 1.
 */
@property (atomic, setter=setBackgroundOpacity:) CGFloat backgroundOpacity;

/**
 Background Mask View Color.
 
 Only used in popup launch style.
 @warning Default value is black.
 */
@property (strong, nonatomic) UIColor *backgroundColor;

/**
 Close Image Path.
 
 Only used in popup launch style.
 @warning Default value is "gpm-cancel-circle-white.png" in GPMWebView.bundle
 */
@property (strong, nonatomic) NSString *closeImagePathForPopup;

/**
 Offset of close button in Pop Up Style.
 
 Base point is the right upper corner point. Default value is (0, 0)
 */
@property (atomic) CGPoint closeButtonOffsetInPopupStyle;


/**---------------------------------------------------------------------------------------
 * @name Delegate Protocol
 *  ---------------------------------------------------------------------------------------
 */

/**
 UIViewController Delegate.
 
 It is used to delegate UIViewController's methods such as viewDidLoad: viewWillLoad: viewDidDisappear: and etc.
 */
@property (strong, nonatomic) id<GPMWebViewDelegate> delegate;

@end

#endif /* GPMWebViewConfiguration_h */
