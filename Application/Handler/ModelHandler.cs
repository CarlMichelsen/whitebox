using Application.Mapper;
using Interface.Dto;
using Interface.Dto.Model;
using Interface.Handler;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Handler;

public class ModelHandler(
    IMemoryCache memoryCache) : IModelHandler
{
    private const string ModelCacheKey = "LargeLanguageModels";
    
    public ServiceResponse<List<LlmProviderGroupDto>> GetModels()
    {
        var models = memoryCache.GetOrCreate(
            ModelCacheKey,
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return AvailableModelsMapper.GetModels();
            });

        if (models is null)
        {
            throw new NullReferenceException("Did not find llm models.");
        }
        
        return new ServiceResponse<List<LlmProviderGroupDto>>(models);
    }
}