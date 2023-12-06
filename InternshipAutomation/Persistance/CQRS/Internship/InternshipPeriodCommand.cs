using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class InternshipPeriodCommand : IRequest<InternshipPeriodResponse>
{
    public int StartedDate { get; set; }

    public class InternshipPeriodCommandHandler : IRequestHandler<InternshipPeriodCommand,InternshipPeriodResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDecodeTokenService _decodeTokenService;

        public InternshipPeriodCommandHandler(IGeneralRepository generalRepository, IHttpContextAccessor contextAccessor, IDecodeTokenService decodeTokenService)
        {
            _generalRepository = generalRepository;
            _contextAccessor = contextAccessor;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<InternshipPeriodResponse> Handle(InternshipPeriodCommand request, CancellationToken cancellationToken)
        {
            var token = _contextAccessor.HttpContext.Request.Cookies["AuthToken"];
            var currentUserUsername = _decodeTokenService.GetUsernameFromToken(token);

            var currentUser = await _generalRepository.Query<Domain.User.User>()
                .SingleOrDefaultAsync(_ => _.UserName == currentUserUsername, cancellationToken: cancellationToken);
            
            var internshipPeriod = new InternshipPeriod
            {
                StartedDate = request.StartedDate,
                CreatedDate = DateTime.Now,
                User = currentUser
            };

            _generalRepository.Add(internshipPeriod);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new InternshipPeriodResponse
            {
                Message = "Yeni Staj Dönemi Başlatılmıştır !",
                Success = true
            };
        }
    }
}


public class InternshipPeriodResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}