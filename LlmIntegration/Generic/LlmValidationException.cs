using FluentValidation.Results;

namespace LLMIntegration.Generic;

public class LlmValidationException(ValidationResult validationResult) : Exception
{
    public ValidationResult ValidationResult { get; } = validationResult;
}