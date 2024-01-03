using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetApplicationFileByStudentNumber : IRequest<GetApplicationFileByStudentNumberResponse>
{
    public string StudentNumber { get; set; }
    
    public class GetApplicationFileByStudentNumberHandler : IRequestHandler<GetApplicationFileByStudentNumber,GetApplicationFileByStudentNumberResponse>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IGeneralRepository _generalRepository;

        public GetApplicationFileByStudentNumberHandler(UserManager<Domain.User.User> userManager, IGeneralRepository generalRepository)
        {
            _userManager = userManager;
            _generalRepository = generalRepository;
        }
        
        public async Task<GetApplicationFileByStudentNumberResponse> Handle(GetApplicationFileByStudentNumber request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.StudentNumber);
            var applicationFile = await _generalRepository.Query<InternshipApplicationFile>()
                .Where(_ => _.StudentNumber == request.StudentNumber)
                .OrderByDescending(_=>_.CreatedDate)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

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

            return new GetApplicationFileByStudentNumberResponse
            {
                InternshipApplicationFileDto = applicationFileDto,
                Success = true
            };
        }
    }
}

public class GetApplicationFileByStudentNumberResponse
{
    public InternshipApplicationFileDto InternshipApplicationFileDto { get; set; }
    public bool Success { get; set; }
}