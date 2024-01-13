using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User.TeacherUser;

public class AddTeacherCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public class AddTeacherCommandHandler(UserManager<Domain.User.User> userManager, IEmailSender emailSender, ILogService logService)
        : IRequestHandler<AddTeacherCommand, Result>
    {
        
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            Domain.User.User user = new();

            user.UserName = request.UserCode;
            user.Email = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = Hash.ToHash(request.Password);
            user.TeacherNameSurname = request.NameSurname;
            
            await _userManager.CreateAsync(user);

            var createdUser = await _userManager.FindByNameAsync(request.UserCode);
            
            await _userManager.AddToRoleAsync(createdUser, "Öğretmen");
            
            _logService.Information($"{user.UserName} kullanıcısı öğretmen kullanıcısı olarak eklendi.");

            return new Result
            {
                Message = "Öğretmen başarıyla eklendi.",
                Success = true
            };
        }
    }
}