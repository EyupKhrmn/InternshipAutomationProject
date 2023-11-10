using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class RegisterInternshipCommand : IRequest<RegisterInternshipResponse>
{
    public Guid UserId { get; set; }
    public Guid InternshipPeriodId { get; set; }
    public Guid InternshipId { get; set; }
    
    
    public class RegisterInternshipCommandHandler : IRequestHandler<RegisterInternshipCommand,RegisterInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public RegisterInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<RegisterInternshipResponse> Handle(RegisterInternshipCommand request, CancellationToken cancellationToken)
        {
            var ınternshipPeriod = await _generalRepository
                .Query<InternshipPeriod>()
                .SingleOrDefaultAsync(_ => _.Id == request.InternshipPeriodId, cancellationToken: cancellationToken);

            var internship = await _generalRepository
                .Query<Domain.Entities.Internship.Internship>()
                .SingleOrDefaultAsync(_ => _.Id == request.InternshipId, cancellationToken: cancellationToken);

            internship.StudentUser = request.UserId;
            internship.TeacherUser = ınternshipPeriod.User.Id;
            
            ınternshipPeriod.Internships.Add(internship);
            _generalRepository.Update(ınternshipPeriod);

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