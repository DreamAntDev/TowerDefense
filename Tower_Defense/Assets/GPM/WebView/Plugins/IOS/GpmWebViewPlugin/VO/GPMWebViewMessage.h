#import <Foundation/Foundation.h>

@interface GPMWebViewMessage : NSObject {
    NSString* _scheme;
    NSString* _error;
    NSInteger _callback;
    NSString* _data;
    NSString* _extra;
}

@property (nonatomic, strong) NSString* scheme;
@property (nonatomic, assign) NSInteger callback;
@property (nonatomic, strong) NSString* data;
@property (nonatomic, strong) NSString* extra;
@property (nonatomic, strong) NSString* error;

-(id)initWithJsonString:(NSString*)jsonString;
-(NSString*)toJsonString;
@end

