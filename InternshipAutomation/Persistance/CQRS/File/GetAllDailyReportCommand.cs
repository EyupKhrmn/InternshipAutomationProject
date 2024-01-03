using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetAllDailyReportCommand : IRequest<Result<List<DailyReportFileDto>>>
{
    public Guid InternshipId { get; set; }
    
    public class GetAllDailyReportCommandHandler : IRequestHandler<GetAllDailyReportCommand, Result<List<DailyReportFileDto>>>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetAllDailyReportCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<Result<List<DailyReportFileDto>>> Handle(GetAllDailyReportCommand request, CancellationToken cancellationToken)
        {
            List<DailyReportFileDto> response = new();
            
            var files = _generalRepository.Query<InternshipDailyReportFile>()
                .Where(_ => _.Internship.Id == request.InternshipId)
                .ToList();

            var average = files.Average(_ => _.Note);
            
            var internship = _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefault(_ => _.Id == request.InternshipId);

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
            };
        }
    }
}