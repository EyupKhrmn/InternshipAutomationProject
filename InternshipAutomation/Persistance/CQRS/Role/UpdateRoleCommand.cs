using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Role;

public class UpdateRoleCommand : IRequest<UpdateRoleResponse>
{
    public string RoleName { get; set; }
    public string NewName { get; set; }
    
    
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand,UpdateRoleResponse>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);

            role.Name = request.NewName;

            await _roleManager.UpdateAsync(role);

            return new UpdateRoleResponse
            {
                Success = true
            };
        }
    }
}

public class UpdateRoleResponse
{
    public bool Success { get; set; }
}