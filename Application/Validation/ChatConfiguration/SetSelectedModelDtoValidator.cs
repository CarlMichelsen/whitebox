using FluentValidation;
using Interface.Dto.Configuration;
using LLMIntegration;

namespace Application.Validation.ChatConfiguration;

public class SetSelectedModelDtoValidator : AbstractValidator<SetSelectedModelDto>
{
    public SetSelectedModelDtoValidator()
    {
        this.RuleFor(s => s.ModelIdentifier)
            .NotEmpty()
            .WithMessage("ModelIdentifier is required.")
            .Must(s => LlmModels.TryGetModel(s, out _))
            .WithMessage("ModelIdentifier is not valid.");
    }
}