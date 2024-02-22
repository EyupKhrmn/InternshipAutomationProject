using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Persistance.CQRS.User;

public class UpdateUserCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public string? Password { get; set; }
    public string? NameSurname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? UserNumber { get; set; }
    
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,Result>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly IEmailSender _emailSender;

        public UpdateUserCommandHandler(UserManager<Domain.User.User> userManager, IDecodeTokenService decodeTokenService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _decodeTokenService = decodeTokenService;
            _emailSender = emailSender;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            
            if (request.Password is not null)
            {
                if (user.PasswordHash != Hash.ToHash(request.Password))
                {
                    user.PasswordHash = Hash.ToHash(request.Password) ?? user.PasswordHash;
                    await _emailSender.SendEmailAsync(user.Email, user.TeacherNameSurname, "Şifre Değişikliği",
                        "Şifre Değiştirme İşlemi başarıyla gerçekleşti.");
                }
            }

            var userRole = await _userManager.GetRolesAsync(user);

            switch (userRole[0])
            {
                case "Öğretmen":
                    user.TeacherNameSurname = request.NameSurname ?? user.TeacherNameSurname;
                    break;
                case "Öğrenci":
                    user.StudentNameSurname = request.NameSurname ?? user.StudentNameSurname;
                    break;
                case "Admin":
                    user.AdminUserNameSurname = request.NameSurname ?? user.AdminUserNameSurname;
                    break;
                case "Şirket":
                    user.CompanyUserNameSurname = request.NameSurname ?? user.CompanyUserNameSurname;
                    break;
            }
            
            user.UserName = request.UserNumber ?? user.UserName;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            
            await _userManager.UpdateAsync(user);

            return new Result
            {
                Message = "Kullanıcı başarıyla güncellendi.",
                Success = true
            };

        }
    }
}