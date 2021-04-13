//
//  GPMWebView.h
//  GPMWebView
//
//  Created by NHN on 2020/11/23.
//  Copyright © 2020 NHN. All rights reserved.
//

#ifndef GPMWebView_h
#define GPMWebView_h

#import <UIKit/UIKit.h>
#import <WebKit/WebKit.h>
#import "GPMWebViewConfiguration.h"

@class GPMWebViewError;

typedef void(^GPMWebViewOpenCompletion)(GPMWebViewError *error);
typedef void(^GPMWebViewCloseCompletion)(GPMWebViewError *error);
typedef void(^GPMWebViewSchemeEvent)(NSString *fullUrl, GPMWebViewError *error);

/** The GPMWebView class represents the entry point for **launching WebView**.
*/
@interface GPMWebView : NSObject
/**---------------------------------------------------------------------------------------
 * @name Properties
 *  ---------------------------------------------------------------------------------------
 */

/**
 
 This property is a global configuration for launching webview.<br/>
 When you handle the webview without any configuration, GPMWebView set its configuration with this value.
 */
@property (nonatomic, strong) GPMWebViewConfiguration *defaultWebConfiguration;


/**---------------------------------------------------------------------------------------
 * @name Initialization
 *  ---------------------------------------------------------------------------------------
 */

/**
 Creates and returns an `GPMWebView` object.
 */
+ (instancetype)sharedGPMWebView;

/**---------------------------------------------------------------------------------------
 * @name Launching WebView
 *  ---------------------------------------------------------------------------------------
 */

/**
 Show WebView that is not for local url.
 
 @param urlString The string value for target url
 @param viewController The presenting view controller
 @warning If viewController is nil, GPMWebView set it to top most view controller automatically.
 @param configuration This configuration is applied to the behavior of webview.
 @warning If configuration is nil, GPMWebView set it to default value. It is described in `GPMWebViewConfiguration`.
 @param openCompletion This completion would be called when webview is opened
 @param closeCompletion This completion would be called when webview is closed
 @param schemeList This schemeList would be filtered every web view request and call schemeEvent
 @param schemeEvent This schemeEvent would be called when web view request matches one of the schemeLlist
 
 */
+ (void)showWithURL:(NSString *)urlString
     viewController:(UIViewController *)viewController
      configuration:(GPMWebViewConfiguration *)configuration
     openCompletion:(GPMWebViewOpenCompletion) openCompletion
    closeCompletion:(GPMWebViewCloseCompletion)closeCompletion
         schemeList:(NSArray<NSString *> *)schemeList
        schemeEvent:(GPMWebViewSchemeEvent)schemeEvent;


/**
 Show WebView for local html (or other web resources)
 
 @param filePath The string value for target local path.
 @param bundle where the html file is located.
 @warning If bundle is nil, GPMWebView set it to main bundle automatically.
 @param viewController The presenting view controller
 @warning If viewController is nil, GPMWebView set it to top most view controller automatically.
 @param configuration This configuration is applied to the behavior of webview.
 @warning If configuration is nil, GPMWebView set it to default value. It is described in `GPMWebViewConfiguration`.
 @param openCompletion This completion would be called when webview is opened
 @param closeCompletion This completion would be called when webview is closed
 @param schemeList This schemeList would be filtered every web view request and call schemeEvent
 @param schemeEvent This schemeEvent would be called when web view request matches one of the schemeLlist
 */
+ (void)showWithHTMLFile:(NSString *)filePath
                  bundle:(NSBundle *)bundle
          viewController:(UIViewController *)viewController
           configuration:(GPMWebViewConfiguration *)configuration
          openCompletion:(GPMWebViewOpenCompletion) openCompletion
         closeCompletion:(GPMWebViewCloseCompletion)closeCompletion
              schemeList:(NSArray<NSString *> *)schemeList
             schemeEvent:(GPMWebViewSchemeEvent)schemeEvent;

/**
Show WebView for local html (or other web resources)

@param filePath The string value for target local path.
@param viewController The presenting view controller
@warning If viewController is nil, GPMWebView set it to top most view controller automatically.
@param configuration This configuration is applied to the behavior of webview.
@warning If configuration is nil, GPMWebView set it to default value. It is described in `GPMWebViewConfiguration`.
@param openCompletion This completion would be called when webview is opened
@param closeCompletion This completion would be called when webview is closed
@param schemeList This schemeList would be filtered every web view request and call schemeEvent
@param schemeEvent This schemeEvent would be called when web view request matches one of the schemeLlist
*/
+ (void)showWithHTMLFile:(NSString *)filePath
 viewController:(UIViewController *)viewController
  configuration:(GPMWebViewConfiguration *)configuration
 openCompletion:(GPMWebViewOpenCompletion) openCompletion
closeCompletion:(GPMWebViewCloseCompletion)closeCompletion
     schemeList:(NSArray<NSString *> *)schemeList
    schemeEvent:(GPMWebViewSchemeEvent)schemeEvent;

/**
Show WebView for local html (or other web resources)

@param htmlString The string value for HTML code.
@param viewController The presenting view controller
@warning If viewController is nil, GPMWebView set it to top most view controller automatically.
@param configuration This configuration is applied to the behavior of webview.
@warning If configuration is nil, GPMWebView set it to default value. It is described in `GPMWebViewConfiguration`.
@param openCompletion This completion would be called when webview is opened
@param closeCompletion This completion would be called when webview is closed
@param schemeList This schemeList would be filtered every web view request and call schemeEvent
@param schemeEvent This schemeEvent would be called when web view request matches one of the schemeLlist
*/
+ (void)showWithHTMLString:(NSString *)htmlString
             viewController:(UIViewController *)viewController
              configuration:(GPMWebViewConfiguration *)configuration
             openCompletion:(GPMWebViewOpenCompletion) openCompletion
            closeCompletion:(GPMWebViewCloseCompletion)closeCompletion
                 schemeList:(NSArray<NSString *> *)schemeList
                schemeEvent:(GPMWebViewSchemeEvent)schemeEvent;


/**
 Open the Browser with urlString
 
 @param urlString The URL to be loaded.
 @warning If urlString is not valid, to open browser would be failed. Please check the url before calling.
 */
+ (void)openWebBrowserWithURL:(NSString *)urlString;

/**
 Close the presenting Webview
 */
+ (void)close;

+ (void)executeJavaScriptWithScript:(NSString *)script;

@end

#endif /* GPMWebView_h */
