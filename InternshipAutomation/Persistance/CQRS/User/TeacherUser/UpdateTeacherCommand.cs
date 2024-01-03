using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User.TeacherUser;

public class UpdateTeacherCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    public string Password { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public class UpdateTeacherCommandHandler(
        UserManager<Domain.User.User> userManager,
        IDecodeTokenService decodeTokenService)
        : IRequestHandler<UpdateTeacherCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;
        private readonly IDecodeTokenService _decodeTokenService = decodeTokenService;

        public async Task<Result> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(currentUser.UserName);
            
            user.PasswordHash = request.Password ?? user.PasswordHash;
            user.StudentNameSurname = request.NameSurname ?? user.StudentNameSurname;
            user.UserName = request.UserCode ?? user.UserName;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            
            await _userManager.UpdateAsync(user);
            
            return new Result
            {
                Message = "Öğretmen başarıyla güncellendi.",
                Success = true
            };
        }
    }
}