using FluentValidation;
using LLMIntegration.Generic.Dto;

namespace LLMIntegration.Validation;

public class LlmPromptValidator : AbstractValidator<LlmPrompt>
{
    public LlmPromptValidator()
    {
        this.RuleFor(prompt => prompt.Model.ModelIdentifier)
            .NotEmpty()
            .WithMessage("Model identifier is required.")
            .MinimumLength(5)
            .WithMessage("Model identifier must be at least 5 characters long.");
        
        this.RuleFor(prompt => prompt.Model.ModelName)
            .NotEmpty()
            .WithMessage("Model name is required.")
            .MinimumLength(2)
            .WithMessage("Model name must be at least 2 characters long.");
        
        this.RuleFor(prompt => prompt.Model.ModelDescription)
            .NotEmpty()
            .WithMessage("Model description is required.")
            .MinimumLength(5)
            .WithMessage("Model description must be at least 5 characters long.");
        
        this.RuleFor(prompt => prompt.Content.Messages)
            .NotEmpty()
            .WithMessage("Prompt must contain at least one message.");
        
        this.RuleFor(prompt => prompt.Content.Messages)
            .Must(StartAndEndWithUserMessage)
            .WithMessage("Messages must start and end with a user message.");
        
        this.RuleFor(prompt => prompt.Content.Messages)
            .Must(HaveAlternatingRoles)
            .WithMessage("Messages must have alternating roles.");
        
        this.RuleForEach(prompt => prompt.Content.Messages)
            .SetValidator(new LlmMessageValidator());
    }

    private static bool StartAndEndWithUserMessage(List<LlmMessage> messages)
    {
        var first = messages.FirstOrDefault();
        if (first?.Role != LlmRole.User)
        {
            return false;
        }
        
        var last = messages.FirstOrDefault();
        return last?.Role == LlmRole.User;
    }

    private static bool HaveAlternatingRoles(List<LlmMessage> messages)
    {
        var role = messages.First().Role;
        foreach (var message in messages.Skip(1))
        {
            if (role == message.Role)
            {
                return false;
            }
            
            role = message.Role;
        }
        
        return true;
    }
}