using System.Text.Json.Serialization;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace InternshipAutomation.Persistance.CQRS.File;

public class AddDailyReportFileCommand : IRequest<AddDailyReportFileResponse>
{
    public string TopicTitleOfWork { get; set; }
    public string DescriptionOfWork { get; set; }
    public Guid InternshipId { get; set; }
    
    public class AddDailyReportFileCommandHandler : IRequestHandler<AddDailyReportFileCommand,AddDailyReportFileResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;

        public AddDailyReportFileCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<AddDailyReportFileResponse> Handle(AddDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();

            // TODO Kullanıcının seçmiş olduğu staj dönemi için gelen ıd ile bulunacak
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            var companyUser = await _generalRepository.Query<Domain.User.User>()
                .FirstOrDefaultAsync(_ => _.Id == internship.CompanyUser, cancellationToken: cancellationToken);

            var file = new InternshipDailyReportFile
            {
                TopicTitleOfWork = request.TopicTitleOfWork,
                DescriptionOfWork = request.DescriptionOfWork,
                StudentNameSurname = currentUser.StudentNameSurname,
                CompanyManagerNameSurname = companyUser.CompanyUserNameSurname,
                WorkingDate = DateTime.UtcNow,
                Internship = internship
            };

            _generalRepository.Add(file);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new AddDailyReportFileResponse
            {
                Success = true
            };
        }
    }
}

public class AddDailyReportFileResponse
{
    public bool Success { get; set; }
}