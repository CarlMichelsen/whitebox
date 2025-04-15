using System.Reflection;
using LLMIntegration.Anthropic.Dto.Model;
using LLMIntegration.Google.Dto.Model;
using LLMIntegration.OpenAi.Dto.Model;
using LLMIntegration.X.Dto.Model;

namespace LLMIntegration.Util;

/// <summary>
/// A static class that contains models for each LlmProvider.
/// </summary>
public static class LlmModels
{
    public static AnthropicModelGroup Anthropic { get; } = new();
    
    public static GoogleModelGroup Google { get; } = new();
    
    public static OpenAiModelGroup OpenAi { get; } = new();
    
    public static XModelGroup X { get; } = new();
    
    public static LegacyModels Legacy { get; } = new();

    public static bool TryGetModel(string modelIdentifier, out LlmModel? model, bool includeLegacy = false)
    {
        var allModels = GetModels(includeLegacy);
        model = allModels.FirstOrDefault(m => m.ModelIdentifier == modelIdentifier);
        return model is not null;
    }

    public static List<LlmModel> GetModels(bool includeLegacy = false)
    {
        var list = new List<LlmModel>();
        
        var modelsClass = typeof(LlmModels);
        var members = modelsClass.GetMembers(BindingFlags.Public | BindingFlags.Static);
        foreach (var member in members)
        {
            if (member.MemberType != MemberTypes.Property)
            {
                continue;
            }

            var valueInstance = (member as PropertyInfo)!.GetValue(null)!;
            var valueType = valueInstance.GetType();
            var properties = valueType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.GetValue(valueInstance) is not LlmModel value)
                {
                    continue;
                }
                
                list.Add(value);
            }
        }

        return list
            .Where(m => m.IsLegacy == includeLegacy)
            .ToList();
    }
}