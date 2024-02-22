using FluentValidation;
using InternshipAutomation.Persistance.CQRS.User;
using InternshipAutomation.Persistance.CQRS.User.CompanyUser;
using InternshipAutomation.Persistance.CQRS.User.StudentUser;
using InternshipAutomation.Persistance.CQRS.User.TeacherUser;
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
            RuleFor(_ => _.UserId).NotNull().NotEmpty();
        }
    }
    
    public class AddStudentUserValidator : AbstractValidator<AddStudentCommand>
    {
        public AddStudentUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress().NotEmpty();
            RuleFor(_ => _.NameSurname).NotEmpty();
            RuleFor(_ => _.Password).NotEmpty();
            RuleFor(_ => _.StudentNumber).NotEmpty();
        }
    }
    
    public class UpdateStudentUserValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress();
            RuleFor(_ => _.NameSurname);
            RuleFor(_ => _.Password);
            RuleFor(_ => _.StudentNumber);
        }
    }
    
    public class AddTeacherUserValidator : AbstractValidator<AddTeacherCommand>
    {
        public AddTeacherUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress().NotEmpty();
            RuleFor(_ => _.NameSurname).NotEmpty();
            RuleFor(_ => _.Password).NotEmpty();
            RuleFor(_ => _.UserCode).NotEmpty();
        }
    }
    
    public class UpdateTeacherUserValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress();
            RuleFor(_ => _.NameSurname);
            RuleFor(_ => _.Password);
            RuleFor(_ => _.UserCode);
        }
    }
    
    public class AddCompanyUserValidator : AbstractValidator<AddCompanyUserCommand>
    {
        public AddCompanyUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress().NotEmpty();
            RuleFor(_ => _.NameSurname).NotEmpty();
            RuleFor(_ => _.Password).NotEmpty();
            RuleFor(_ => _.UserCode).NotEmpty();
        }
    }
    
    public class UpdateCompanyUserValidator : AbstractValidator<UpdateCompanyUserCommand>
    {
        public UpdateCompanyUserValidator()
        {
            RuleFor(_ => _.Email).EmailAddress();
            RuleFor(_ => _.NameSurname);
            RuleFor(_ => _.Password);
            RuleFor(_ => _.UserCode);
        }
    }
}