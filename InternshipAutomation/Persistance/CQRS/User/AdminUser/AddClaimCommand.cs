using System.Security.Claims;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Persistance.CQRS.Response;
using IntershipOtomation.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Persistance.CQRS.User;

public class AddClaimCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string Role { get; set; }
    
    public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand,Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AddClaimCommandHandler(IGeneralRepository generalRepository, UserManager<Domain.User.User> userManager, RoleManager<AppRole> roleManager)
        {
            _generalRepository = generalRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(AddClaimCommand request, CancellationToken cancellationToken)
        {
            Claim claim = new Claim(request.Name, request.Value);
            
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                if (role == request.Role)
                {
                    await _userManager.AddClaimAsync(user, claim);
                }
                else
                {
                    throw new Exception("Eklemek istediğiniz alan bu kullanıcı için eklenemez");
                }
            }
            
            return new Result
            {
                Success = true
            };
        }
    }
}