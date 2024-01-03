using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Persistance.CQRS.User;

public class UpdateUserCommand : IRequest<Result>
{
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string StudentNumber { get; set; }
    
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,Result>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IDecodeTokenService _decodeTokenService;

        public UpdateUserCommandHandler(UserManager<Domain.User.User> userManager, IDecodeTokenService decodeTokenService)
        {
            _userManager = userManager;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(currentUser.UserName);
            
            user.PasswordHash = request.Password ?? user.PasswordHash;
            user.StudentNameSurname = request.Name ?? user.StudentNameSurname;
            user.UserName = request.StudentNumber ?? user.UserName;
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