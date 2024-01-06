using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetAllInternshipDailyReportForInternCommand : IRequest<Result<List<DailyReportFileForCompanyDto>>>
{
    public Guid InternId { get; set; }
    
    public class GetAllInternshipDailyReportForInternCommandHandler(IGeneralRepository generalRepository)
        : IRequestHandler<GetAllInternshipDailyReportForInternCommand, Result<List<DailyReportFileForCompanyDto>>>
    {
        private readonly IGeneralRepository _generalRepository = generalRepository;

        public async Task<Result<List<DailyReportFileForCompanyDto>>> Handle(GetAllInternshipDailyReportForInternCommand request, CancellationToken cancellationToken)
        {
            List<DailyReportFileForCompanyDto> dailyReportFileForCompanyDtos = new();
            
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.StudentUser == request.InternId, cancellationToken: cancellationToken);
            
            var files = await _generalRepository.Query<InternshipDailyReportFile>()
                .Where(_ => _.Internship.Id == internship.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            #region Null Control

            if (internship is null)
            {
                return new Result<List<DailyReportFileForCompanyDto>>
                {
                    Data = new List<DailyReportFileForCompanyDto>(),
                    Message = "Staj bulunamadı.",
                    Success = false
                };
            }

            if (files is null)
            {
                return new Result<List<DailyReportFileForCompanyDto>>
                {
                    Data = new List<DailyReportFileForCompanyDto>(),
                    Message = "Staj doslayaları bulunamadı.",
                    Success = false
                };
            }

            #endregion

            foreach (var file in files)
            {
                DailyReportFileForCompanyDto dailyReportFileForCompanyDto = new()
                {
                    FileId = file.Id,
                    TopicTitleOfWork = file.TopicTitleOfWork,
                    DescriptionOfWork = file.DescriptionOfWork,
                    StudentNameSurname = file.StudentNameSurname,
                    CompanyManagerNameSurname = file.CompanyManagerNameSurname,
                    WorkingDate = file.WorkingDate
                };
                
                dailyReportFileForCompanyDtos.Add(dailyReportFileForCompanyDto);
            }
            

            return new Result<List<DailyReportFileForCompanyDto>>
            {
                Data = dailyReportFileForCompanyDtos
            };
        }
    }
}