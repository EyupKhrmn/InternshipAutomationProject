using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class AddUserRoleCommand : IRequest<Result>
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    
    public class AddUserRoleCommandHandler: IRequestHandler<AddUserRoleCommand,Result>
    {
        private readonly UserManager<Domain.User.User> _userManager;

        public AddUserRoleCommandHandler(UserManager<Domain.User.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            await _userManager.AddToRoleAsync(user, request.RoleName);

            return new Result
            {
                Success = true
            };
        }
    }
}