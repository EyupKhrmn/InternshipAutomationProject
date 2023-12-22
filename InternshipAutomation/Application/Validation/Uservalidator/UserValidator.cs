using FluentValidation;
using InternshipAutomation.Persistance.CQRS.User;
using Org.BouncyCastle.Tls.Crypto.Impl;

namespace InternshipAutomation.Application.Validation.Uservalidator;

public class UserValidator
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress().NotEmpty();
            RuleFor(_ => _.NameSurname).NotEmpty();
            RuleFor(_ => _.Password).NotEmpty();
            RuleFor(_ => _.UserNumber).NotEmpty();
            RuleFor(_ => _.Role).NotEmpty();
        }
    }
    
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(_ => _.Email).NotEmpty();
        }
    }
    
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            
        }
    }
}