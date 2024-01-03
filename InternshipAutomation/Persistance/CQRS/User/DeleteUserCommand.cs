using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class DeleteUserCommand : IRequest<Result>
{
    public string Email { get; set; }
    
    
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,Result>
    {
        private readonly UserManager<Domain.User.User> _userManager;

        public DeleteUserCommandHandler(UserManager<Domain.User.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            await _userManager.DeleteAsync(user);

            return new Result
            {
                Success = true
            };
        }
    }
}