using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class UpdateInternshipApplicationCommand : IRequest<UpdateInternshipApplicationResponse>
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
    
    public class UpdateInternshipApplicationCommandHandler : IRequestHandler<UpdateInternshipApplicationCommand, UpdateInternshipApplicationResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public UpdateInternshipApplicationCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<UpdateInternshipApplicationResponse> Handle(UpdateInternshipApplicationCommand request, CancellationToken cancellationToken)
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

            return new UpdateInternshipApplicationResponse
            {
                Message = "Staj başvuru dosyası başarıyla güncellendi.",
                Success = true
            };
        }
    }
}

public class UpdateInternshipApplicationResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}