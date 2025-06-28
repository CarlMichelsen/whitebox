using Interface.Dto.Model;
using LLMIntegration;
using LLMIntegration.Util;

namespace Application.Mapper;

public static class AvailableModelsMapper
{
    public static List<LlmProviderGroupDto> GetProviders()
    {
        return LlmModels
            .GetModels()
            .GroupBy(m => m.Provider)
            .Select(Map)
            .ToList();
    }
    
    public static LlmModelDto Map(LlmModel llmModel)
    {
        return new LlmModelDto(
            Provider: ToProviderString(llmModel.Provider),
            ModelName: llmModel.ModelName,
            ModelDescription: llmModel.ModelDescription,
            ModelIdentifier: llmModel.ModelIdentifier);
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
}