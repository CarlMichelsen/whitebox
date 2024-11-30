using Interface.Dto.Model;

namespace Interface.Handler;

public interface IModelHandler
{
    List<LlmProviderGroupDto> GetModels();
}