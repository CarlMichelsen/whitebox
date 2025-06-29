using Presentation.Dto;
using Presentation.Dto.Model;

namespace Presentation.Handler;

public interface IModelHandler
{
    ServiceResponse<List<LlmProviderGroupDto>> GetModels();
}