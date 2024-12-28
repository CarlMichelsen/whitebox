using Database.Entity.Id;
using Microsoft.AspNetCore.Http;

namespace Interface.Accessor;

public interface ISourceIdAccessor
{
    SourceId GetSourceId(HttpContext? httpContext = default);
}