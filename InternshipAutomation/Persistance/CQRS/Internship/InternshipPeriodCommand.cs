using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using MediatR;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class InternshipPeriodCommand : IRequest<InternshipPeriodResponse>
{
    public int StartedDate { get; set; }

    public class InternshipPeriodCommandHandler : IRequestHandler<InternshipPeriodCommand,InternshipPeriodResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public InternshipPeriodCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<InternshipPeriodResponse> Handle(InternshipPeriodCommand request, CancellationToken cancellationToken)
        {
            var internshipPeriod = new InternshipPeriod
            {
                StartedDate = request.StartedDate,
                CreatedDate = DateTime.Now
                //TODO User olarak şuanki kullanıcı bilgileri ile veriler getirilecek
            };

            _generalRepository.Add(internshipPeriod);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new InternshipPeriodResponse
            {
                Message = "Yeni Staj Dönemi Başlatılmıştır !",
                Success = true
            };
        }
    }
}


public class InternshipPeriodResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}