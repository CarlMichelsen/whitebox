namespace Application.Configuration;

public static class ApplicationConstants
{
    public const string ApplicationName = "WhiteBox";
    
    public const string ApplicationUserAgent = $"{ApplicationName}/1.0.0";

    public const string DevelopmentCorsPolicyName = "development-cors";
    
    public const string DevelopmentCorsUrl = "http://localhost:5444";

    public const string TraceIdHeaderName = "X-Trace-Id";

    public const string AccessCookieName = "identity-access-token";
    
    public const string SourceIdCookieName = "source-unique-identifier";
}