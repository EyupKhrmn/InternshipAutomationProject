using InternshipAutomation.Application.PDfs;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

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
            PdfForApplicationFile pdfForApplicationFile = new();
            
            var user = await _userManager.FindByNameAsync(request.StudentNumber);
            var applicationFile = await _generalRepository.Query<InternshipApplicationFile>()
                .Where(_ => _.StudentNumber == request.StudentNumber)
                .OrderByDescending(_=>_.CreatedDate)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            pdfForApplicationFile.GeneratePdfForApplicationFile().GeneratePdfAndShow();

            return new GetApplicationFileByStudentNumberResponse
            {
                Success = true
            };
        }
    }
}

public class GetApplicationFileByStudentNumberResponse
{
    public bool Success { get; set; }
}