using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure;
using Azure.Core;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Newtonsoft.Json.Linq;

namespace InternshipAutomation.Persistance.CQRS.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(UserManager<Domain.User.User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var role = await _userManager.GetRolesAsync(user);

            //TODO Kullanıcının Passwordu kontrol edilecek
            //if (user.PasswordHash == request.Password)
            //{
            //    throw new Exception("Kullanıcı adı veya şifre yanlış");
            //}
            
            var token = TokenHandler.CreateToken(_configuration, request.UserName, request.Password, role);

            user.Token = token.AccessToken;

            #region Cookie Features

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Sadece HTTP üzerinden erişilebilir
                SameSite = SameSiteMode.Strict, // Güvenlik için sadece aynı site üzerinden erişime izin ver
                Expires = DateTime.Now.AddDays(1) // Token'ın geçerlilik süresi
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", user.Token, cookieOptions);

            #endregion

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