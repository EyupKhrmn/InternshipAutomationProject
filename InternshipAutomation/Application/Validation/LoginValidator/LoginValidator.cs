using FluentValidation;
using InternshipAutomation.Persistance.CQRS.Login;
using InternshipAutomation.Persistance.CQRS.User;

namespace InternshipAutomation.Application.Validation.LoginValidator;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(_ => _.UserName).NotEmpty().NotNull();
        RuleFor(_ => _.Password).NotEmpty().NotNull();
    }
}

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidator()
    {
        RuleFor(_ => _.UserCode).NotEmpty().NotNull();
    }
}