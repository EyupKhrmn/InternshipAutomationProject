using InternshipAutomation.Application.Mail;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User.CompanyUser;

public class AddCompanyUserCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public class AddCompanyUserCommandHandler(UserManager<Domain.User.User> userManager, ILogService logService)
        : IRequestHandler<AddCompanyUserCommand, Result>
    {
        
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(AddCompanyUserCommand request, CancellationToken cancellationToken)
        {
            Domain.User.User user = new();

            user.UserName = request.UserCode;
            user.Email = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = Hash.ToHash(request.Password);
            user.CompanyUserNameSurname = request.NameSurname;
            
            await _userManager.CreateAsync(user);

            var createdUser = await _userManager.FindByNameAsync(request.UserCode);
            
            await _userManager.AddToRoleAsync(createdUser, "Şirket");
            
            _logService.Information($"{user.UserName} kullanıcısı şifket kullanıcısı olarak eklendi.");

            return new Result
            {
                Message = "Şirket kullanıcısı başarıyla eklendi.",
                Success = true
            };
        }
    }
}