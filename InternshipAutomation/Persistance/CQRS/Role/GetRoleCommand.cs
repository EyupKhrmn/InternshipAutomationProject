using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InternshipAutomation.Persistance.CQRS.Role;

public class GetRoleCommand : IRequest<GetRoleResponse>
{
    public string? RoleName { get; set; }
    
    public class GetRoleCommandHandler : IRequestHandler<GetRoleCommand,GetRoleResponse>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public GetRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetRoleResponse> Handle(GetRoleCommand request, CancellationToken cancellationToken)
        {
            var query = _roleManager.Roles;
            query = !request.RoleName.IsNullOrEmpty()
                ? _roleManager.Roles.Where(_ => _.Name == request.RoleName)
                : query;

            var roles = await query.ToListAsync(cancellationToken: cancellationToken);

            return new GetRoleResponse
            {
                Roles = roles
            };
        }
    }
}

public class GetRoleResponse
{
    public List<AppRole> Roles { get; set; }
}