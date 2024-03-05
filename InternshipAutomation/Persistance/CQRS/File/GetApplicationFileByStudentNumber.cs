using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public record GetApplicationFileByStudentNumber : IRequest<Result<InternshipApplicationFileDto>>
{
    public string StudentNumber { get; set; }
    
    public class GetApplicationFileByStudentNumberHandler : IRequestHandler<GetApplicationFileByStudentNumber,Result<InternshipApplicationFileDto>>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public GetApplicationFileByStudentNumberHandler(UserManager<Domain.User.User> userManager, IGeneralRepository generalRepository, ILogService logService)
        {
            _userManager = userManager;
            _generalRepository = generalRepository;
            _logService = logService;
        }
        
        public async Task<Result<InternshipApplicationFileDto>> Handle(GetApplicationFileByStudentNumber request, CancellationToken cancellationToken)
        {
            var applicationFile = await _generalRepository.Query<InternshipApplicationFile>()
                .Where(_ => _.StudentNumber == request.StudentNumber)
                .OrderByDescending(_=>_.CreatedDate)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (applicationFile is null)
            {
                _logService.Error($"{request.StudentNumber} numaralı öğrenciye ait staj başvuru dosyası bulunamadı.");
                return new Result<InternshipApplicationFileDto>
                {
                    Message = "Staj başvuru dosyası bulunamadı.",
                    Success = false
                };
            }

            InternshipApplicationFileDto applicationFileDto = new()
            {
                StudentNumber = applicationFile.StudentNumber,
                StudentNameSurname = applicationFile.StudentNameSurname,
                StudentProgram = applicationFile.StudentProgram,
                StudentTCKN = applicationFile.StudentTCKN,
                StudentPhoneNumber = applicationFile.StudentPhoneNumber,
                StudentAGNO = applicationFile.StudentAGNO,
                StartedDate = applicationFile.StartedDate,
                FinishedDate = applicationFile.FinishedDate,
                CompanyName = applicationFile.CompanyName,
                CompanyPhoneNumber = applicationFile.CompanyPhoneNumber,
                CompanyEMail = applicationFile.CompanyEMail,
                CompanySector = applicationFile.CompanySector
            };

            return new Result<InternshipApplicationFileDto>
            {
                Data = applicationFileDto,
                Success = true
            };
        }
    }
}