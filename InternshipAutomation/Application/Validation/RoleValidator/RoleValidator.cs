using FluentValidation;
using InternshipAutomation.Persistance.CQRS.Role;

namespace InternshipAutomation.Application.Validation.RoleValidator;

public class RoleValidator
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidator()
        {
            RuleFor(_ => _.RoleName).NotEmpty();
        }
    }
    
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(_ => _.Name).NotEmpty();
        }
    }
    
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(_ => _.RoleName)
                .NotEmpty();
            RuleFor(_ => _.NewName).NotEmpty();
        }
    }
}