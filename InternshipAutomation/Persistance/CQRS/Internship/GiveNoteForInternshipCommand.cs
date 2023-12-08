using InternshipAutomation.Application.Repository.GeneralRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GiveNoteForInternshipCommand : IRequest<GiveNoteForInternshipResponse>
{
    public Guid InternshipId { get; set; }
    public int Note { get; set; }
    
    public class GiveNoteForInternshipCommandHandler : IRequestHandler<GiveNoteForInternshipCommand,GiveNoteForInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public GiveNoteForInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<GiveNoteForInternshipResponse> Handle(GiveNoteForInternshipCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .Where(_ => _.Id == request.InternshipId)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            internship.Note = request.Note;

            _generalRepository.Update(internship);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new GiveNoteForInternshipResponse
            {
                Message = "Not verme işlemi başarıyla gerçekleşti",
                Success = true
            };
        }
    }
}


public class GiveNoteForInternshipResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}