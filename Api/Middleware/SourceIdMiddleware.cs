using Application.Configuration;
using Database.Entity.Id;

namespace Api.Middleware;

public class SourceIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sourceId = GetSourceId(context);
        if (sourceId is not null && context.Request.Headers.TryAdd(ApplicationConstants.SourceIdCookieName, sourceId.ToString()))
        {
            await next(context);
        }
        else
        {
            sourceId = new SourceId(Guid.CreateVersion7());
            SetSourceId(context, sourceId);

            await next(context);
        }
    }

    private static SourceId? GetSourceId(HttpContext context)
    {
        if (!context.Request.Cookies.TryGetValue(ApplicationConstants.SourceIdCookieName, out var sourceIdValue))
        {
            return default;
        }
        
        if (!Guid.TryParse(sourceIdValue, out var sourceId))
        {
            return default;
        }

        return sourceId.Version != 7
            ? default
            : new SourceId(sourceId);
    }
    
    private static void SetSourceId(HttpContext context, SourceId sourceId)
    {
        context.Response.Cookies.Append(ApplicationConstants.SourceIdCookieName, sourceId.ToString());
        if (!context.Request.Headers.TryAdd(ApplicationConstants.SourceIdCookieName, sourceId.ToString()))
        {
            throw new Exception("Failed to set cookie source id to header of request.");
        }
    }
}