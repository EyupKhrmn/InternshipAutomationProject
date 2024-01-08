using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetAllDailyReportCommand : IRequest<Result<List<DailyReportFileDto>>>
{
    public Guid InternshipId { get; set; }
    
    public class GetAllDailyReportCommandHandler : IRequestHandler<GetAllDailyReportCommand, Result<List<DailyReportFileDto>>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public GetAllDailyReportCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result<List<DailyReportFileDto>>> Handle(GetAllDailyReportCommand request, CancellationToken cancellationToken)
        {
            List<DailyReportFileDto> response = new();
            
            var files = _generalRepository.Query<InternshipDailyReportFile>()
                .Where(_ => _.Internship.Id == request.InternshipId)
                .ToList();

            //TODO: Ortalama hesaplanacak. Hocaya Sorulacak
            double? average = files.Average(_ => _.Note);
            
            var internship = _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefault(_ => _.Id == request.InternshipId);

            if (internship is null)
            {
                _logService.Error($"{request.InternshipId} ID'li staj bulunamadı.");
                return new Result<List<DailyReportFileDto>>
                {
                    Message = "Staj bulunamadı.",
                    Success = false
                };
            }

            internship.InternshipAverage = (double)average;

            foreach (var file in files)
            {
                DailyReportFileDto fileDto = new()
                {
                    FileId = file.Id,
                    TopicTitleOfWork = file.TopicTitleOfWork,
                    DescriptionOfWork = file.DescriptionOfWork,
                    StudentNameSurname = file.StudentNameSurname,
                    WorkingDate = file.WorkingDate,
                    Note = file.Note,
                    CompanyManagerNameSurname = file.CompanyManagerNameSurname,
                };
                
                response.Add(fileDto);
            }

            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new Result<List<DailyReportFileDto>>
            {
                Data = response,
                Success = true,
                Message = $"Staj raporları getirildi. Staj günlük rapor Ortalaması: {average}"
            };
        }
    }
}