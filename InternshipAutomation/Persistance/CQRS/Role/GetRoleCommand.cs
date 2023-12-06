using System.IdentityModel.Tokens.Jwt;
using Azure.Core;
using InternshipAutomation.Security.Token;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace InternshipAutomation.Persistance.CQRS.Role;

public class GetRoleCommand : IRequest<GetRoleResponse>
{
    public string? RoleName { get; set; }

    public class GetRoleCommandHandler : IRequestHandler<GetRoleCommand, GetRoleResponse>
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDecodeTokenService _tokenService;

        public GetRoleCommandHandler(RoleManager<AppRole> roleManager, IHttpContextAccessor contextAccessor, IDecodeTokenService tokenService)
        {
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
        }

        public async Task<GetRoleResponse> Handle(GetRoleCommand request, CancellationToken cancellationToken)
        {
            var query = _roleManager.Roles;
            query = !request.RoleName.IsNullOrEmpty()
                ? _roleManager.Roles.Where(_ => _.Name == request.RoleName)
                : query;

            var roles = await query.ToListAsync(cancellationToken: cancellationToken);

            var token = _contextAccessor.HttpContext.Request.Cookies["AuthToken"];
            
            return new GetRoleResponse
            {
                Roles = roles,
            };
        }
    }
}

public class GetRoleResponse
{
    public List<AppRole> Roles { get; set; }
}