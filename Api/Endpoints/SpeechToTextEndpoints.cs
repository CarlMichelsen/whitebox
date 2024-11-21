using Interface.Dto;
using Interface.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class SpeechToTextEndpoints
{
    public static void RegisterSpeechEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        var speechGroup = apiGroup
            .MapGroup("speech")
            .WithTags("Speech");
        
        // TODO: Something better than this...
        speechGroup.MapPost(
            string.Empty,
            async ([FromBody] SpeechBlob blob, [FromServices] ISpeechToTextHandler handler) =>
                await handler.ToText(blob))
            .Produces<SpeechToTextResponse>();
    }
}