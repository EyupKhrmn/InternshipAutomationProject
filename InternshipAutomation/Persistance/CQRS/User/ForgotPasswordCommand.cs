using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public record ForgotPasswordCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    
    public sealed class ForgotPasswordCommandHandler(UserManager<Domain.User.User> userManager, IEmailSender emailSender, ILogService logService)
        : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var oneTimePassword = new Random().Next(100000, 999999).ToString();
            var user = await _userManager.FindByNameAsync(request.UserCode);
            
            if (user == null)
            {
                return new Result
                {
                    Message = "Kullanıcı Bulunamadı",
                    Success = false
                };
            }
            
            user.OneTimePassword = oneTimePassword;
            user.PasswordExpirationDate = DateTime.Now.AddMinutes(10);
            user.IsFirstLoginAfterForgotPassword = true;
            
            await _userManager.UpdateAsync(user);
            
            await _emailSender.SendEmailAsync(user.Email, "MAKÜ Staj","Tek Kullanımlık Şifre", $"Şifreniz: {oneTimePassword}");
            
            _logService.Information($"{user.UserName} kullanıcısının şifresi mail olarak gönderildi. Şifremi unuttum işlemi gerçekleştirildi.");

            return new Result
            {
                Message = "Tek Kullanımlık Şifreniz mail adresinize gönderildi.",
                Success = true
            };
        }
    }
}