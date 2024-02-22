using System.Security.Claims;
using InternshipAutomation.Application.Caching;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace InternshipAutomation.Persistance.CQRS.User;

public class GetUserCommand : IRequest<Result<List<Domain.User.User>>>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    
    
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand,Result<List<Domain.User.User>>>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly CacheService _cacheService;
        private readonly CacheObject _cacheObject;

        public GetUserCommandHandler(UserManager<Domain.User.User> userManager, CacheService cacheService, CacheObject cacheObject)
        {
            _userManager = userManager;
            _cacheService = cacheService;
            _cacheObject = cacheObject;
        }

        public async Task<Result<List<Domain.User.User>>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var cache = await _cacheService.GetCache("users");

            if (cache is not null)
            {
                return new Result<List<Domain.User.User>>
                {
                    Data = await _cacheObject.DeserializeObject<List<Domain.User.User>>(cache),
                    Success = true
                };
            }

            var query = _userManager.Users;

            query = !request.Email.IsNullOrEmpty() 
                ? _userManager.Users.Where(_ => _.Email == request.Email) 
                : query;

            query = !request.Username.IsNullOrEmpty()
                ? _userManager.Users.Where(_ => _.UserName == request.Username)
                : query;
            
            var users = await query.ToListAsync(cancellationToken: cancellationToken);
            
            await _cacheService.SetCache("users", await _cacheObject.SerializeObject(users));
            
            return new Result<List<Domain.User.User>>
            {
                Data = users,
                Success = true
            };
        }
    }
}