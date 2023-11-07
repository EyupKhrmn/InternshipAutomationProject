using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class AddUserCommand : IRequest<AddUserResponse>
{
    public string StudentNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand,AddUserResponse>
    {
        private readonly UserManager<Domain.User.User> _userManager;

        public AddUserCommandHandler(UserManager<Domain.User.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Domain.User.User user = new();
            Claim claim = new Claim(nameof(request.Gender), request.Gender);

            user.UserName = request.StudentNumber;
            user.Email = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            await _userManager.AddClaimAsync(user, claim);

            //IdentityResult result1 = await _userManager.AddClaimAsync(user,claim);
            await _userManager.CreateAsync(user, request.Password);

            return new AddUserResponse()
            {
                Success = true
            };
        }
    }
}

public class AddUserResponse
{
    public bool Success { get; set; }
}