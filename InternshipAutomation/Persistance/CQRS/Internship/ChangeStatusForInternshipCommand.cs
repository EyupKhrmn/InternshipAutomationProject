using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class ChangeStatusForInternshipCommand : IRequest<Result>
{
    public Guid InternshipId { get; set; }
    public InternshipStatus Status { get; set; }
    
    public class ChangeStatusForInternshipCommandHandler : IRequestHandler<ChangeStatusForInternshipCommand, Result>
    {
        private readonly IGeneralRepository _generalRepository;

        public ChangeStatusForInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<Result> Handle(ChangeStatusForInternshipCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            internship.Status = request.Status;
            
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            return new Result
            {
                Message = "Staj durumu başarıyla değiştirildi.",
                Success = true
            };
        }
    }
}