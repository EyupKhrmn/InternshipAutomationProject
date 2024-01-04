using System.Security.Cryptography.X509Certificates;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class CheckInternshipDailyReportCommand : IRequest<Result>
{
    public Guid DailyReportFileId { get; set; }
    public bool IsChecked { get; set; }
    
    public class CheckInternshipDailyReportCommandHandler(IGeneralRepository generalRepository)
        : IRequestHandler<CheckInternshipDailyReportCommand, Result>
    {
        private readonly IGeneralRepository _generalRepository = generalRepository;

        public async Task<Result> Handle(CheckInternshipDailyReportCommand request, CancellationToken cancellationToken)
        {
            var dailyReportFile = await _generalRepository.Query<Domain.Entities.Files.InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.DailyReportFileId, cancellationToken: cancellationToken);

            dailyReportFile.IsCheckCompany = request.IsChecked;

            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new Result
            {
                Message = "Staj günlük raporu başarıyla onaylandı.",
                Success = true
            };
        }
    }
}