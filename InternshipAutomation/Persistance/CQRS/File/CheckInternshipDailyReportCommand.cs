using System.Security.Cryptography.X509Certificates;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public record CheckInternshipDailyReportCommand : IRequest<Result>
{
    public Guid DailyReportFileId { get; set; }
    public bool IsChecked { get; set; }
    
    public class CheckInternshipDailyReportCommandHandler(IGeneralRepository generalRepository, ILogService logService, IDecodeTokenService decodeTokenService)
        : IRequestHandler<CheckInternshipDailyReportCommand, Result>
    {
        private readonly IGeneralRepository _generalRepository = generalRepository;
        private readonly ILogService _logService = logService;
        private readonly IDecodeTokenService _decodeTokenService = decodeTokenService;

        public async Task<Result> Handle(CheckInternshipDailyReportCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var dailyReportFile = await _generalRepository.Query<Domain.Entities.Files.InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.DailyReportFileId, cancellationToken: cancellationToken);

            if (dailyReportFile is null)
            {
                return new Result
                {
                    Message = "Staj günlük raporu bulunamadı.",
                    Success = false
                };
            }

            dailyReportFile.IsCheckCompany = request.IsChecked;

            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            _logService.Information($"{currentUser.UserName} kullanıcısı {dailyReportFile.StudentNameSurname} kullanıcısının {dailyReportFile.WorkingDate} tarihli staj günlük raporunu onayladı. Onaylanan rapor: {dailyReportFile.Id}");

            return new Result
            {
                Message = "Staj günlük raporu başarıyla onaylandı.",
                Success = true
            };
        }
    }
}