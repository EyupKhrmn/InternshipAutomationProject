using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.Hasing;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User.StudentUser;

public class UpdateStudentCommand : IRequest<Result>
{
    public string Password { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string StudentNumber { get; set; }
    
    public class UpdateStudentCommandHandler(
        UserManager<Domain.User.User> userManager,
        IDecodeTokenService decodeTokenService,
        ILogService logService)
        : IRequestHandler<UpdateStudentCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IDecodeTokenService _decodeTokenService = decodeTokenService;
        private readonly ILogService _logService = logService;

        public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(currentUser.UserName);
            
            user.PasswordHash = Hash.ToHash(request.Password) ?? user.PasswordHash;
            user.StudentNameSurname = request.NameSurname ?? user.StudentNameSurname;
            user.UserName = request.StudentNumber ?? user.UserName;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            
            await _userManager.UpdateAsync(user);
            
            _logService.Information($"{user.UserName} kullancısı güncelleme işlemi yapıldı.");

            return new Result
            {
                Message = "Kullanıcı başarıyla güncellendi.",
                Success = true
            };
        }
    }
}