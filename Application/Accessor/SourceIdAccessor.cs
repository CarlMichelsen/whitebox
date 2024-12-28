using Application.Configuration;
using Database.Entity.Id;
using Interface.Accessor;
using Microsoft.AspNetCore.Http;

namespace Application.Accessor;

public class SourceIdAccessor(IHttpContextAccessor httpContextAccessor) : ISourceIdAccessor
{
    public SourceId GetSourceId(HttpContext? httpContext = default)
    {
        httpContext ??= httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            throw new NullReferenceException("HttpContext is null when attempting to get SourceId");
        }

        if (!httpContext.Request.Headers.TryGetValue(ApplicationConstants.SourceIdCookieName, out var sourceIdString))
        {
            throw new NullReferenceException("No sourceId found in request header");
        }

        if (!Guid.TryParse(sourceIdString, out var sourceId))
        {
            throw new NullReferenceException("Invalid sourceId found in request header");
        }
        
        if (sourceId.Version != 7)
        {
            throw new NullReferenceException("Invalid sourceId (not version 7 guid) found in request header");
        }
        
        return new SourceId(sourceId);
    }
}