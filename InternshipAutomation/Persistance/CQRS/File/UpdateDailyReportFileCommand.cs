using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace InternshipAutomation.Persistance.CQRS.File;

public class UpdateDailyReportFileCommand : IRequest<UpdateDailyReportFileResponse>
{
    public Guid DailyReportFileId { get; set; }
    public string? TopicTitleOfWork { get; set; }
    public string? DescriptionOfWork  { get; set; }
    public string? StudentNameSurname { get; set; }
    public string? CompanyManagerNameSurname { get; set; }
    public DateTime? WorkingDate { get; set; }
 
    public class UpdateDailyReportFileCommandHandler : IRequestHandler<UpdateDailyReportFileCommand, UpdateDailyReportFileResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public UpdateDailyReportFileCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<UpdateDailyReportFileResponse> Handle(UpdateDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.DailyReportFileId, cancellationToken: cancellationToken);
            
            if (file == null)
                throw new Exception("Dosya bulunamadı.");
            
            file.TopicTitleOfWork = request.TopicTitleOfWork ?? file.TopicTitleOfWork;
            file.DescriptionOfWork = request.DescriptionOfWork ?? file.DescriptionOfWork;
            file.StudentNameSurname = request.StudentNameSurname ?? file.StudentNameSurname;
            file.CompanyManagerNameSurname = request.CompanyManagerNameSurname ?? file.CompanyManagerNameSurname;
            file.WorkingDate = request.WorkingDate ?? file.WorkingDate;
            
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new UpdateDailyReportFileResponse
            {
                Message = "Günlük rapor dosyası başarıyla güncellendi.",
                Success = true
            };
        }
    }
}

public class UpdateDailyReportFileResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}