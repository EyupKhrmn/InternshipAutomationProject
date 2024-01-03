using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class ForgotPasswordCommand : IRequest<Result>
{
    public string UserCode { get; set; }
    
    public class ForgotPasswordCommandHandler(UserManager<Domain.User.User> userManager)
        : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly UserManager<Domain.User.User> _userManager = userManager;

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserCode);
            
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            return new Result
            {
                Message = $"Şifreniz: {user.PasswordHash}",
                Success = true
            };
        }
    }
}