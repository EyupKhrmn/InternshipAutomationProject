using InternshipAutomation.Persistance.CQRS.Response;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Role;

public record DeleteRoleCommand : IRequest<Result>
{
    public string Name { get; set; }
    
    
    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand,Result>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);

            await _roleManager.DeleteAsync(role);

            return new Result
            {
                Message = "Role başarıyla silindi.",
                Success = true
            };
        }
    }
}