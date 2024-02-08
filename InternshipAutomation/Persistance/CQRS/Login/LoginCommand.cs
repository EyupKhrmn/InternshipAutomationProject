using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.Login;

public class LoginCommand : IRequest<Result>
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly ILogService _logger;
   

        public LoginCommandHandler(UserManager<Domain.User.User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, ILogService logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var role = await _userManager.GetRolesAsync(user);
            
            bool isOneTimePasswordExpired = (user.PasswordExpirationDate > DateTime.Now && user.PasswordExpirationDate < DateTime.Now.AddMinutes(11)) ? false : true;
            
            if (!isOneTimePasswordExpired && user.IsFirstLoginAfterForgotPassword)
            {
                if (user.OneTimePassword == request.Password)
                {
                    return new Result
                    {
                        Message = "Tek Kullanımlık Şifre Başarıyla Kullanıldı, Şimdi Şifre sıfırlama işlemi yapınız.",
                        Success = true
                    };
                }
                else if (user.OneTimePassword != request.Password)
                {
                    return new Result
                    {
                        Message = "Tek Kullanımlık Şifreniz Yanlış",
                        Success = false
                    };
                }
                
                
            }
            
            
            if (user.PasswordHash != Hash.ToHash(request.Password))
            {
                return new Result
                {
                    Message = "Kullanıcı adı veya şifre hatalı.",
                    Success = false
                };
            }
            
            if (user.PasswordExpirationDate < DateTime.Now)
            {
                return new Result
                {
                    Message = "Şifrenizin süresi dolmuş. Lütfen şifrenizi Şifremi Unuttum alanından güncelleyin.",
                    Success = false
                };
            }
            
            var token = TokenHandler.CreateToken(_configuration, request.UserName, request.Password, role);

            user.Token = token.AccessToken;
            
            _logger.Information($"{user.UserName} Kullanıcısı sisteme giriş yaptı");

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
            
            return new Result
            {
                Message = token.AccessToken
            };
        }
    }

}