using Interface.Dto;
using Interface.Dto.Model;

namespace Interface.Handler;

public interface IModelHandler
{
    ServiceResponse<List<LlmProviderGroupDto>> GetModels();
}