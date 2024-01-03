using System.Security.Claims;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InternshipAutomation.Persistance.CQRS.User;

public class GetUserCommand : IRequest<Result<List<Domain.User.User>>>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    
    
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand,Result<List<Domain.User.User>>>
    {
        private readonly UserManager<Domain.User.User> _userManager;

        public GetUserCommandHandler(UserManager<Domain.User.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<List<Domain.User.User>>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users;

            query = !request.Email.IsNullOrEmpty() 
                ? _userManager.Users.Where(_ => _.Email == request.Email) 
                : query;

            query = !request.Username.IsNullOrEmpty()
                ? _userManager.Users.Where(_ => _.UserName == request.Username)
                : query;

            var users = await query.ToListAsync(cancellationToken: cancellationToken);

            return new Result<List<Domain.User.User>>
            {
                Data = users
            };
        }
    }
}