using InternshipAutomation.Application.Mail;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace InternshipAutomation.Persistance.CQRS.User;

public class AddUserCommand : IRequest<AddUserResponse>
{
    public string UserNumber { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand,AddUserResponse>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly RoleManager<AppRole>? _roleManager;
        private readonly IEmailSender _emailSender;

        public AddUserCommandHandler(UserManager<Domain.User.User> userManager, RoleManager<AppRole>? roleManager, IEmailSender emailSender)
        {   
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Domain.User.User user = new();

            user.UserName = request.UserNumber;
            user.Email = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = request.Password;
            
            switch (request.Role)
            {
                case "Öğretmen":
                    user.TeacherNameSurname = request.NameSurname;
                    break;
                case "Öğrenci":
                    user.StudentNameSurname = request.NameSurname;
                    break;
                case "Şirket":
                    user.CompanyUserNameSurname = request.NameSurname;
                    break;
                case "Admin":
                    user.AdminUserNameSurname = request.NameSurname;
                    break;
            }
            
            await _userManager.CreateAsync(user);

            var createdUser = await _userManager.FindByNameAsync(request.UserNumber);
            
            switch (request.Role)
            {
                case "Öğretmen":
                    await _userManager.AddToRoleAsync(createdUser, request.Role);
                    break;
                case "Öğrenci":
                    await _userManager.AddToRoleAsync(createdUser, request.Role);
                    break;
                case "Şirket":
                    await _userManager.AddToRoleAsync(createdUser, request.Role);
                    break;
                case "Admin":
                    await _userManager.AddToRoleAsync(createdUser,request.Role);
                    break;
            }

            #region MailSender
            
            string mailSubject = "Kullanıcı Ekleme İşlemi";
            string mailContent = "Kullanıcı ekleme işlemi başarıyla gerçekleşti.";

            await _emailSender.SendEmailAsync(request.Email, request.UserNumber, mailSubject, mailContent);
            
            #endregion
            
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