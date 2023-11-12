using System.Collections.Concurrent;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Internship;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class RegisterInternshipCommand : IRequest<RegisterInternshipResponse>
{
    public InternshipApplicationDto InternshipApplication { get; set; }
    public Guid InternshipPeriodId { get; set; }
    
    
    public class RegisterInternshipCommandHandler : IRequestHandler<RegisterInternshipCommand,RegisterInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public RegisterInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<RegisterInternshipResponse> Handle(RegisterInternshipCommand request, CancellationToken cancellationToken)
        {
            var internshipPeriod = await _generalRepository
                .Query<InternshipPeriod>()
                .SingleOrDefaultAsync(_ => _.Id == request.InternshipPeriodId, cancellationToken: cancellationToken);

            var internship = new Domain.Entities.Internship.Internship
            {
                StudentUser = request.InternshipApplication.StudentUser, //TODO o anki kullanıcının verileri ile doldurulacak
                TeacherUser = request.InternshipApplication.TeacherUser, //TODO internship üzerinden gelen UserID ile doldurulacak
                CompanyUser = request.InternshipApplication.CompanyUser,
                InternshipApplicationFile = request.InternshipApplication.InternshipApplicationFile,
                InternshipPeriod = internshipPeriod
            };
            
            _generalRepository.Add(internship);
            internshipPeriod.Internships.Add(internship);
            _generalRepository.Update(internshipPeriod);

            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            return new RegisterInternshipResponse
            {
                Message = "Staj Dönemine Kaydınız başarıyla tamamlanmıştır.",
                Success = true
            };
        }
     }
}

public class RegisterInternshipResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}