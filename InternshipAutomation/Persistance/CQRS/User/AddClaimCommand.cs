using System.Security.Claims;
using InternshipAutomation.Application.Repository.GeneralRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternshipAutomation.Persistance.CQRS.User;

public class AddClaimCommand : IRequest<AddClaimResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    
    
    
    public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand,AddClaimResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly UserManager<Domain.User.User> _userManager;

        public AddClaimCommandHandler(IGeneralRepository generalRepository, UserManager<Domain.User.User> userManager)
        {
            _generalRepository = generalRepository;
            _userManager = userManager;
        }

        public async Task<AddClaimResponse> Handle(AddClaimCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            Claim claim = new Claim(request.Name, request.Value);

            await _userManager.AddClaimAsync(user, claim);

            return new AddClaimResponse
            {
                Success = true
            };
        }
    }
}


public class AddClaimResponse
{
    public bool Success { get; set; }
}