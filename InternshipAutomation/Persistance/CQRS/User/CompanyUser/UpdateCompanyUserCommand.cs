using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User.CompanyUser;

public class UpdateCompanyUserCommand : IRequest<Result>
{
    public string Password { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string UserCode { get; set; }
    
    public class UpdateCompanyUserCommandHandler(
        UserManager<Domain.User.User> userManager,
        IDecodeTokenService decodeTokenService,
        ILogService logService)
        : IRequestHandler<UpdateCompanyUserCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IDecodeTokenService _decodeTokenService = decodeTokenService;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(UpdateCompanyUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(currentUser.UserName);

            if (user is null)
            {
                _logService.Error($"{user.UserName} kullanıcısı bulunamadı");
                return new Result
                {
                    Message = "Kullanıcı bulunamadı.",
                    Success = false
                };
            }
            
            user.PasswordHash = request.Password ?? user.PasswordHash;
            user.CompanyUserNameSurname = request.NameSurname ?? user.CompanyUserNameSurname;
            user.UserName = request.UserCode ?? user.UserName;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            
            await _userManager.UpdateAsync(user);
            
            _logService.Information($"{user.UserName} kullanıcısı güncelleme işlemi yapıldı.");
            
            return new Result
            {
                Message = "Şirket kullanıcısı başarıyla güncellendi.",
                Success = true
            };
        }
    }
}