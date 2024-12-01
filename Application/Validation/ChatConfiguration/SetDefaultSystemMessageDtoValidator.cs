using FluentValidation;
using Interface.Dto.Configuration;

namespace Application.Validation.ChatConfiguration;

public class SetDefaultSystemMessageDtoValidator : AbstractValidator<SetDefaultSystemMessageDto>
{
    public SetDefaultSystemMessageDtoValidator()
    {
        this.RuleFor(s => s.SystemMessage)
            .NotEmpty()
            .WithMessage("ModelIdentifier is required.");
    }
}