namespace Application.Configuration;

public static class ApplicationConstants
{
    public const string Name = "WhiteBox";
    
    public const string Version = "0.0.1";
    
    public const string UserAgent = $"{Name}/{Version}";

    public const string DevelopmentCorsPolicyName = "development-cors";
    
    public const string DevelopmentCorsUrl = "http://localhost:5444";

    public const string TraceIdHeaderName = "X-Trace-Id";

    public const string AccessCookieName = "identity-access-token";
    
    public const string SourceIdCookieName = "source-unique-identifier";
}