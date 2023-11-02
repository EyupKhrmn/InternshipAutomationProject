using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class UpdateUserCommand : IRequest<UpdateUserResponse>
{
    
    
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,UpdateUserResponse>
    {
        private readonly UserManager<Domain.User.User> _userManager;

        public UpdateUserCommandHandler(UserManager<Domain.User.User> userManager)
        {
            _userManager = userManager;
        }

        public Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

public class UpdateUserResponse
{
    public bool Success { get; set; }
}