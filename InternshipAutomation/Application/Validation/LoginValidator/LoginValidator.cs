using FluentValidation;
using InternshipAutomation.Persistance.CQRS.Login;

namespace InternshipAutomation.Application.Validation.LoginValidator;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(_ => _.UserName).NotEmpty().NotNull();
        RuleFor(_ => _.Password).NotEmpty().NotNull();
    }
}