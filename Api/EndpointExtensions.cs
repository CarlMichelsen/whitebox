using Application.Hub;

namespace Api;

public static class EndpointExtensions
{
    public static void RegisterEndpoints(
        this IEndpointRouteBuilder app)
    {
        var apiGroup = app
            .MapGroup("api/v1")
            .RequireAuthorization();

        apiGroup
            .MapHub<SpeechToTextHub>("speechToTextHub");
    }
}