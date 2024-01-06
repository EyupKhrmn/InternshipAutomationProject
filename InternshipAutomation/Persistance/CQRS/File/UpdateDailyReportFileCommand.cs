using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace InternshipAutomation.Persistance.CQRS.File;

public class UpdateDailyReportFileCommand : IRequest<Result>
{
    public Guid DailyReportFileId { get; set; }
    public string? TopicTitleOfWork { get; set; }
    public string? DescriptionOfWork  { get; set; }
    public string? StudentNameSurname { get; set; }
    public string? CompanyManagerNameSurname { get; set; }
    public DateTime? WorkingDate { get; set; }
 
    public class UpdateDailyReportFileCommandHandler : IRequestHandler<UpdateDailyReportFileCommand, Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public UpdateDailyReportFileCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result> Handle(UpdateDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.DailyReportFileId, cancellationToken: cancellationToken);

            if (file == null)
            {
                _logService.Error($"{request.DailyReportFileId} id'li staj günlük raporu bulunamadı.");
                return new Result
                {
                    Message = "Staj günlük raporu bulunamadı.",
                    Success = false
                };
            }
            
            file.TopicTitleOfWork = request.TopicTitleOfWork ?? file.TopicTitleOfWork;
            file.DescriptionOfWork = request.DescriptionOfWork ?? file.DescriptionOfWork;
            file.StudentNameSurname = request.StudentNameSurname ?? file.StudentNameSurname;
            file.CompanyManagerNameSurname = request.CompanyManagerNameSurname ?? file.CompanyManagerNameSurname;
            file.WorkingDate = request.WorkingDate ?? file.WorkingDate;
            
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            _logService.Information($"{file.StudentNameSurname} kullanısı {file.WorkingDate} tarihli staj günlük raporunu güncelledi. Eklenen rapor: {file.Id}");

            return new Result
            {
                Message = "Günlük rapor dosyası başarıyla güncellendi.",
                Success = true
            };
        }
    }
}