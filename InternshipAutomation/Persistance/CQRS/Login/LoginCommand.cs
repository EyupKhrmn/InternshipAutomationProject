using System.Security.Claims;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace InternshipAutomation.Persistance.CQRS.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public class LoginCommandHandler : IRequestHandler<LoginCommand,LoginResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.User.User> _userManager;

        public LoginCommandHandler(UserManager<Domain.User.User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            
            //if (user.PasswordHash == request.Password)
            //{
            //    throw new Exception("Kullanıcı adı veya şifre yanlış");
            //}

            var token = TokenHandler.CreateToken(_configuration,request.UserName, request.Password);

            user.Token = token.AccessToken;
            await _userManager.UpdateAsync(user);
            
            return new LoginResponse
            {
                Token = token.AccessToken
            };
        }
    }
    
}

public class LoginResponse
{
    public string Token { get; set; }
}