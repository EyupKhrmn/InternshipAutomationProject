using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public record GetDailyReportFileCommand : IRequest<Result<InternshipDailyReportFile>>
{
    public Guid StudentId { get; set; }
    public DateTime ReportDate { get; set; }
    
    
    public class GetDailyReportFileCommandHandler : IRequestHandler<GetDailyReportFileCommand,Result<InternshipDailyReportFile>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public GetDailyReportFileCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result<InternshipDailyReportFile>> Handle(GetDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var studentUser = await _generalRepository.Query<Domain.User.User>()
                .FirstOrDefaultAsync(_ => _.Id == request.StudentId, cancellationToken: cancellationToken);

            if (studentUser is null)
            {
                _logService.Error($"{request.StudentId} ID'li öğrenci bulunamadı.");
                return new Result<InternshipDailyReportFile>
                {
                    Message = "Öğrenci bulunamadı.",
                    Success = false
                };
            }

            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ =>
                    _.StudentNameSurname == studentUser.StudentNameSurname && _.WorkingDate.Date == request.ReportDate.Date, cancellationToken: cancellationToken);
            
            if (file is null)
            {
                _logService.Error($"{request.StudentId} ID'li öğrenciye ait {request.ReportDate} tarihli staj günlük raporu bulunamadı.");
                return new Result<InternshipDailyReportFile>
                {
                    Message = "Staj Günlük Raporu bulunamadı.",
                    Success = false
                };
            }

            return new Result<InternshipDailyReportFile>
            {
                Data = file,
                Message = $"{file.StudentNameSurname} adlı öğrenciye ait {file.WorkingDate} tarihli staj günlük raporu başarıyla getirildi.",
                Success = true
            };
        }
    }
}