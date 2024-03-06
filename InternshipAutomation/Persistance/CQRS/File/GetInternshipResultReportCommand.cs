using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public record GetInternshipResultReportCommand : IRequest<Result<InternshipResultReport>>
{
    public Guid InternshipId { get; set; }
    
    public sealed class GetInternshipResultReportCommandHandler : IRequestHandler<GetInternshipResultReportCommand, Result<InternshipResultReport>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public GetInternshipResultReportCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result<InternshipResultReport>> Handle(GetInternshipResultReportCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            var resultReport = await _generalRepository.Query<InternshipResultReport>()
                .FirstOrDefaultAsync(_=>_.InternshipId == request.InternshipId, cancellationToken: cancellationToken);

            #region Null Control

            if (internship is null || resultReport is null)
            {
                if (internship is null)
                    _logService.Error($"{request.InternshipId} ID'li staj bulunamadı.");
                if (resultReport is null)
                    _logService.Error($"{request.InternshipId} ID'li staj için sonuç raporu bulunamadı.");
                return new Result<InternshipResultReport>
                {
                    Data = null,
                    Message = "Staj bulunamadı.",
                    Success = false
                };
            }

            #endregion

            return new Result<InternshipResultReport>
            {
                Data = resultReport,
                Message = $"{internship.InternshipApplicationFile.StudentNameSurname} adlı öğrenciye ait staj sonuç raporu başarıyla getirildi.",
                Success = true
            };
        }
    }
}