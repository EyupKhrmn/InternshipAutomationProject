using System.Security.Policy;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Hash = InternshipAutomation.Persistance.Hasing.Hash;

namespace InternshipAutomation.Persistance.CQRS.User;

public class ResetPasswordCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    public string NewPassword { get; set; }
    
    public class ResetPasswordCommandHandler(UserManager<Domain.User.User> userManager, ILogService logService)
        : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserCode);

            user.PasswordHash = Hash.ToHash(request.NewPassword);
            user.PasswordExpirationDate = DateTime.Now.AddMonths(12);
            
            await _userManager.UpdateAsync(user);
            
            _logService.Information($"{user.UserName} kullanıcısının şifresi başarıyla değiştirildi.");
            
            return new Result
            {
                Message = "Yeni Şifreniz başarıyla oluşturuldu.",
                Success = true
            };
        }
    }
}