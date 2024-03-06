using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Persistance.CQRS.User.TeacherUser;

public record UpdateTeacherCommand : IRequest<Result>
{
    public string? UserCode { get; set; }
    public string? Password { get; set; }
    public string? NameSurname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    public sealed class UpdateTeacherCommandHandler(
        UserManager<Domain.User.User> userManager,
        IDecodeTokenService decodeTokenService,
        ILogService logService,
        IEmailSender emailSender)
        : IRequestHandler<UpdateTeacherCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IDecodeTokenService _decodeTokenService = decodeTokenService;
        private readonly ILogService _logService = logService;
        private readonly IEmailSender _emailSender = emailSender;

        public async Task<Result> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(currentUser.UserName);

            if (request.Password is not null)
            {
                if (user.PasswordHash != Hash.ToHash(request.Password))
                {
                    user.PasswordHash = Hash.ToHash(request.Password) ?? user.PasswordHash;
                    await _emailSender.SendEmailAsync(user.Email, user.TeacherNameSurname, "Şifre Değişikliği",
                        "Şifre Değiştirme İşlemi başarıyla gerçekleşti.");
                }
            }
            user.StudentNameSurname = request.NameSurname ?? user.StudentNameSurname;
            user.UserName = request.UserCode ?? user.UserName;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            
            await _userManager.UpdateAsync(user);
            
            _logService.Information($"{user.UserName} kullancısı güncelleme işlemi yapıldı.");
            
            return new Result
            {
                Message = "Öğretmen başarıyla güncellendi.",
                Success = true
            };
        }
    }
}