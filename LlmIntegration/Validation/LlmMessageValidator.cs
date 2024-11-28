using FluentValidation;
using Interface.Llm.Dto.Generic;

namespace LLMIntegration.Validation;

public class LlmMessageValidator : AbstractValidator<LlmMessage>
{
    public LlmMessageValidator()
    {
        this.RuleFor(message => message.Parts)
            .NotEmpty()
            .WithMessage("Message must have at least one part.");
        
        this.RuleForEach(message => message.Parts)
            .SetValidator(new LlmPartValidator());
    }
}