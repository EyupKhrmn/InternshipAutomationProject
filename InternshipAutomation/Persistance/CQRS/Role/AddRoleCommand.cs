using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.CQRS.User;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Role;

public record AddRoleCommand : IRequest<Result>
{
    public string RoleName { get; set; }
    
    
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand,Result>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AddRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new AppRole
            {
                Name = request.RoleName
            };
            await _roleManager.CreateAsync(role);

            return new Result
            {
                Message = "Role başarıyla eklendi.",
                Success = true
            };
        }
    }
}