using System.Text.Json.Serialization;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace InternshipAutomation.Persistance.CQRS.File;

public record AddDailyReportFileCommand : IRequest<Result>
{
    public string TopicTitleOfWork { get; set; }
    public string DescriptionOfWork { get; set; }
    public Guid InternshipId { get; set; }
    
    public sealed class AddDailyReportFileCommandHandler : IRequestHandler<AddDailyReportFileCommand,Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly ILogService _logService;

        public AddDailyReportFileCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, ILogService logService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _logService = logService;
        }

        public async Task<Result> Handle(AddDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            var companyUser = await _generalRepository.Query<Domain.User.User>()
                .FirstOrDefaultAsync(_ => _.Id == internship.CompanyUser, cancellationToken: cancellationToken);

            if (internship is null)
            {
                _logService.Error($"{request.InternshipId} ID'li Staj bulunamadı.");
                return new Result
                {
                    Message = "Staj bulunamadı.",
                    Success = false
                };
            }

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
            
            _logService.Information($"{currentUser.UserName} kullanısı {file.WorkingDate} tarihli staj günlük raporunu ekledi. Eklenen rapor: {file.Id}");

            return new Result
            {
                Message = $"{file.TopicTitleOfWork} başlıklı günlük rapor başarıyla eklendi.",
                Success = true
            };
        }
    }
}