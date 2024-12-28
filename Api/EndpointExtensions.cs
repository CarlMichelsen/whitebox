using Api.Endpoints;

namespace Api;

public static class EndpointExtensions
{
    public static void RegisterEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapFallbackToFile("index.html");
        
        app.MapGet("health", () => Results.Ok());
        
        var apiGroup = app
            .MapGroup("api/v1")
            .RequireAuthorization();
        
        apiGroup.RegisterSpeechEndpoints();

        apiGroup.RegisterChatConfigurationEndpoints();

        apiGroup.RegisterConversationEndpoints();
        
        apiGroup.RegisterModelEndpoints();
        
        apiGroup.RegisterRedirectEndpoints();
    }
}