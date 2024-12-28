namespace Api.Endpoints;

public static class RedirectEndpoints
{
    public static void RegisterRedirectEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        var conversationGroup = apiGroup
            .MapGroup("redirect")
            .WithTags("Redirect");
        
        conversationGroup
            .MapGet("chatLink/{base64Url}", (string base64Url) =>
            {
                var bytes = Convert.FromBase64String(base64Url);
                var url = new string(System.Text.Encoding.UTF8.GetChars(bytes));
                var unescaped = Uri.UnescapeDataString(url);
                
                return Uri.TryCreate(unescaped, UriKind.Absolute, out var uri)
                    ? Results.Redirect(uri.AbsoluteUri)
                    : Results.BadRequest();
            });
    }
}