using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class AddUserRoleCommand : IRequest<AddUserRoleResponse>
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    
    public class AddUserRoleCommandHandler: IRequestHandler<AddUserRoleCommand,AddUserRoleResponse>
    {
        private readonly UserManager<Domain.User.User> _userManager;

        public AddUserRoleCommandHandler(UserManager<Domain.User.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddUserRoleResponse> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            await _userManager.AddToRoleAsync(user, request.RoleName);

            return new AddUserRoleResponse
            {
                Success = true
            };
        }
    }
}

public class AddUserRoleResponse
{
    public bool Success { get; set; }
}