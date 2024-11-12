namespace Domain.User;

public static class JwtClaims
{
    public const string User = "user";
    
    public const string AccessId = "jti";
    
    public const string RefreshId = "refresh";
    
    public const string LoginId = "login";
}