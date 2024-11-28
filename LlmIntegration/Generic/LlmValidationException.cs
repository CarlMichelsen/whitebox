using FluentValidation.Results;

namespace LLMIntegration.Generic;

public class LlmValidationException(ValidationResult validationResult) : System.Exception();