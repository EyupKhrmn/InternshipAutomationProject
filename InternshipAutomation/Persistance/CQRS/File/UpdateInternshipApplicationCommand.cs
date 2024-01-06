using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class UpdateInternshipApplicationCommand : IRequest<Result>
{
    public Guid InternshipApplicationFile { get; set; }
    public string? StudentNameSurname { get; set; }
    public string? StudentNumber { get; set; }
    public string? StudentTCKN { get; set; }
    public string? StudentPhoneNumber { get; set; }
    public string? StudentProgram { get; set; }
    public float? StudentAGNO { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyEMail { get; set; }
    public string? CompanySector { get; set; }
    
    public class UpdateInternshipApplicationCommandHandler : IRequestHandler<UpdateInternshipApplicationCommand, Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public UpdateInternshipApplicationCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result> Handle(UpdateInternshipApplicationCommand request, CancellationToken cancellationToken)
        {
            var file = await _generalRepository.Query<InternshipApplicationFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.InternshipApplicationFile, cancellationToken: cancellationToken);
            
            if (file == null)
                throw new Exception("Başvuru dosyası bulunamadı.");
            
            file.StudentNameSurname = request.StudentNameSurname ?? file.StudentNameSurname;
            file.StudentNumber = request.StudentNumber ?? file.StudentNumber;
            file.StudentTCKN = request.StudentTCKN ?? file.StudentTCKN;
            file.StudentPhoneNumber = request.StudentPhoneNumber ?? file.StudentPhoneNumber;
            file.StudentProgram = request.StudentProgram ?? file.StudentProgram;
            file.StudentAGNO = request.StudentAGNO ?? file.StudentAGNO;
            file.StartedDate = request.StartedDate ?? file.StartedDate;
            file.FinishedDate = request.FinishedDate ?? file.FinishedDate;
            file.CompanyName = request.CompanyName ?? file.CompanyName;
            file.CompanyPhoneNumber = request.CompanyPhoneNumber ?? file.CompanyPhoneNumber;
            file.CompanyEMail = request.CompanyEMail ?? file.CompanyEMail;
            file.CompanySector = request.CompanySector ?? file.CompanySector;
            
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            _logService.Information($"{file.StudentNameSurname} kullanıcısının staj başvuru dosyası güncellendi. Güncellenen dosya: {file.Id}");

            return new Result
            {
                Message = "Staj başvuru dosyası başarıyla güncellendi.",
                Success = true
            };
        }
    }
}