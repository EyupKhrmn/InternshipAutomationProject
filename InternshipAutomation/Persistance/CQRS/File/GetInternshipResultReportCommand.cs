using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetInternshipResultReportCommand : IRequest<GetInternshipResultReportResponse>
{
    public Guid InternshipId { get; set; }
    
    public class GetInternshipResultReportCommandHandler : IRequestHandler<GetInternshipResultReportCommand, GetInternshipResultReportResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetInternshipResultReportCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<GetInternshipResultReportResponse> Handle(GetInternshipResultReportCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            var resultReport = await _generalRepository.Query<InternshipResultReport>()
                .FirstOrDefaultAsync(_=>_.InternshipId == request.InternshipId, cancellationToken: cancellationToken);

            return new GetInternshipResultReportResponse
            {
                InternshipResultReport = resultReport,
                Succes = true
            };
        }
    }
}

public class GetInternshipResultReportResponse
{
    public InternshipResultReport InternshipResultReport { get; set; }
    public bool Succes { get; set; }
}