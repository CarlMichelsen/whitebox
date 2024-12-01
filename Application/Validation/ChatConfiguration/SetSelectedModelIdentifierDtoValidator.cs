using FluentValidation;
using Interface.Dto.Configuration;
using LLMIntegration.Util;

namespace Application.Validation.ChatConfiguration;

public class SetSelectedModelIdentifierDtoValidator : AbstractValidator<SetSelectedModelDto>
{
    public SetSelectedModelIdentifierDtoValidator()
    {
        this.RuleFor(s => s.ModelIdentifier)
            .NotEmpty()
            .WithMessage("ModelIdentifier is required.")
            .Must(s => LlmModels.TryGetModel(s, out _))
            .WithMessage("ModelIdentifier is not valid.");
    }
}