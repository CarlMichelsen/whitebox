using Interface.Dto.Model;
using Interface.Handler;
using LLMIntegration.Util;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Handler;

public class ModelHandler(
    IMemoryCache memoryCache) : IModelHandler
{
    private const string ModelCacheKey = "LargeLanguageModels";
    
    public List<LlmProviderGroupDto> GetModels()
    {
        var models = memoryCache.GetOrCreate(
            ModelCacheKey,
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return LlmModels
                    .GetModels()
                    .GroupBy(m => m.Provider)
                    .Select(Map)
                    .ToList();
            });

        if (models is null)
        {
            throw new NullReferenceException("Did not find llm models.");
        }
        
        return models;
    }

    private static string ToProviderString(LlmProvider llmProvider)
    {
        return Enum.GetName(llmProvider)!;
    }

    private static LlmProviderGroupDto Map(IGrouping<LlmProvider, LlmModel> grouping)
    {
        return new LlmProviderGroupDto(
            Provider: ToProviderString(grouping.Key),
            Models: grouping.Select(Map).ToList());
    }

    private static LlmModelDto Map(LlmModel llmModel)
    {
        return new LlmModelDto(
            Provider: ToProviderString(llmModel.Provider),
            ModelName: llmModel.ModelName,
            ModelDescription: llmModel.ModelDescription,
            ModelIdentifier: llmModel.ModelIdentifier);
    }
}