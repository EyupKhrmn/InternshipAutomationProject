using InternshipAutomation.Persistance.CQRS.User;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Role;

public class AddRoleCommand : IRequest<AddRoleResponse>
{
    public string RoleName { get; set; }
    
    
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand,AddRoleResponse>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AddRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<AddRoleResponse> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new AppRole
            {
                Name = request.RoleName
            };
            await _roleManager.CreateAsync(role);

            return new AddRoleResponse
            {
                Success = true
            };
        }
    }
}

public class AddRoleResponse
{
    public bool Success { get; set; }
}