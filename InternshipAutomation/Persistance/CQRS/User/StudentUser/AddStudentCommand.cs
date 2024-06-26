using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User.StudentUser;

public record AddStudentCommand : IRequest<Result>
{
    public string StudentNumber { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public sealed class AddStudentCommandHandler(UserManager<Domain.User.User> userManager, IEmailSender emailSender, ILogService logService)
        : IRequestHandler<AddStudentCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            Domain.User.User user = new();

            user.UserName = request.StudentNumber;
            user.Email = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = Hash.ToHash(request.Password);
            user.StudentNameSurname = request.NameSurname;
            
            await _userManager.CreateAsync(user);

            var createdUser = await _userManager.FindByNameAsync(request.StudentNumber);
            
            await _userManager.AddToRoleAsync(createdUser, "Öğrenci");
            
            _logService.Information($"{user.UserName} kullanıcısı öğrenci kullanıcısı olarak eklendi.");

            return new Result
            {
                Message = "Öğrenci başarıyla eklendi.",
                Success = true
            };
        }
    }
}