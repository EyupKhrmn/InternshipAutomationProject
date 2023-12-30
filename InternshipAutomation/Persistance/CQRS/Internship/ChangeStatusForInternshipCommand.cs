using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class ChangeStatusForInternshipCommand : IRequest<ChangeStatusForInternshipResponse>
{
    public Guid InternshipId { get; set; }
    public InternshipStatus Status { get; set; }
    
    public class ChangeStatusForInternshipCommandHandler : IRequestHandler<ChangeStatusForInternshipCommand, ChangeStatusForInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public ChangeStatusForInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<ChangeStatusForInternshipResponse> Handle(ChangeStatusForInternshipCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);
            
            internship.Status = request.Status;
            
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            return new ChangeStatusForInternshipResponse
            {
                Message = "Staj durumu başarıyla değiştirildi.",
                Success = true
            };
        }
    }
}

public class ChangeStatusForInternshipResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}