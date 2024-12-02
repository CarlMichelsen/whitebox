using Interface.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class ModelEndpoints
{
    public static void RegisterModelEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        var modelGroup = apiGroup
            .MapGroup("model")
            .WithTags("Model");
        
        modelGroup.MapGet(
            "/",
            ([FromServices] IModelHandler handler) => handler.GetModels())
            .AllowAnonymous();
    }
}