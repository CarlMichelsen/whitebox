using FluentValidation.Results;

namespace LLMIntegration.Exception;

public class LlmValidationException(ValidationResult validationResult) : System.Exception();