using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class ForgotPasswordCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    
    public class ForgotPasswordCommandHandler(UserManager<Domain.User.User> userManager, IEmailSender emailSender, ILogService logService)
        : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserCode);
            
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }
            
            await _emailSender.SendEmailAsync(user.Email, "MAKÜ Staj","Şifre Hatırlatma", $"Şifreniz: {user.PasswordHash}");
            
            _logService.Information($"{user.UserName} kullanıcısının şifresi mail olarak gönderildi. Şifremi unuttum işlemi gerçekleştirildi.");

            return new Result
            {
                Message = "Şifreniz mail adresinize gönderildi.",
                Success = true
            };
        }
    }
}