using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetInternshipResultReportCommand : IRequest<Result<InternshipResultReport>>
{
    public Guid InternshipId { get; set; }
    
    public class GetInternshipResultReportCommandHandler : IRequestHandler<GetInternshipResultReportCommand, Result<InternshipResultReport>>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetInternshipResultReportCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<Result<InternshipResultReport>> Handle(GetInternshipResultReportCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            var resultReport = await _generalRepository.Query<InternshipResultReport>()
                .FirstOrDefaultAsync(_=>_.InternshipId == request.InternshipId, cancellationToken: cancellationToken);

            return new Result<InternshipResultReport>
            {
                Data = resultReport,
                Success = true
            };
        }
    }
}