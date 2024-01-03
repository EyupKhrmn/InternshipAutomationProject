using InternshipAutomation.Persistance.CQRS.Response;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Role;

public class UpdateRoleCommand : IRequest<Result>
{
    public string RoleName { get; set; }
    public string NewName { get; set; }
    
    
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand,Result>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);

            role.Name = request.NewName;

            await _roleManager.UpdateAsync(role);

            return new Result
            {
                Success = true
            };
        }
    }
}