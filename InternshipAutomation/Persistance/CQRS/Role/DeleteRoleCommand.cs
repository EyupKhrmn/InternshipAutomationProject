using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Role;

public class DeleteRoleCommand : IRequest<DeleteRoleResponse>
{
    public string Name { get; set; }
    
    
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand,DeleteRoleResponse>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);

            await _roleManager.DeleteAsync(role);

            return new DeleteRoleResponse
            {
                Success = true
            };
        }
    }
}

public class DeleteRoleResponse
{
    public bool Success { get; set; }
}