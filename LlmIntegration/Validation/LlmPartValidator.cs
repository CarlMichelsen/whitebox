using FluentValidation;
using LLMIntegration.Generic.Dto;

namespace LLMIntegration.Validation;

public class LlmPartValidator : AbstractValidator<LlmPart>
{
    public LlmPartValidator()
    {
        this.RuleFor(part => part.Content)
            .NotEmpty()
            .WithMessage("Message-part must have content.");
    }
}