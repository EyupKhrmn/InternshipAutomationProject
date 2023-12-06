using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class AddInternshipCommand : IRequest<AddInternshipResponse>
{
    public Domain.Entities.Internship.Internship Internship { get; set; }
    
    public class AddInternshipCommandHandler : IRequestHandler<AddInternshipCommand,AddInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDecodeTokenService _decodeTokenService;

        public AddInternshipCommandHandler(IGeneralRepository generalRepository, IHttpContextAccessor httpContextAccessor, IDecodeTokenService decodeTokenService)
        {
            _generalRepository = generalRepository;
            _httpContextAccessor = httpContextAccessor;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<AddInternshipResponse> Handle(AddInternshipCommand request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];
            var currentUserUsername = _decodeTokenService.GetUsernameFromToken(token);
            
            var user = await _generalRepository
                .Query<Domain.User.User>()
                .SingleOrDefaultAsync(_ => _.UserName == currentUserUsername, cancellationToken: cancellationToken);
            
            user.Internships.Add(request.Internship);

            return new AddInternshipResponse
            {
                Success = true
            };
        }
    }
}

public class AddInternshipResponse
{
    public bool Success { get; set; }
}