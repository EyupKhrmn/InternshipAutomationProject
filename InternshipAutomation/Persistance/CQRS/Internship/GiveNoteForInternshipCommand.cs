using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GiveNoteForInternshipCommand : IRequest<Result>
{
    public Guid InternshipId { get; set; }
    public int Note { get; set; }

    public class
        GiveNoteForInternshipCommandHandler : IRequestHandler<GiveNoteForInternshipCommand,
        Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public GiveNoteForInternshipCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result> Handle(GiveNoteForInternshipCommand request,
            CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .Where(_ => _.Id == request.InternshipId)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            internship.Note = request.Note;

            _generalRepository.Update(internship);
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            _logService.Information($"{internship.StudentUser} kullanıcısının staj notu güncellendi. Güncellenen not: {internship.Note}. Staj: {internship.Id}. Güncelleyen: {internship.TeacherUser}");

            return new Result
            {
                Message = "Not verme işlemi başarıyla gerçekleşti",
                Success = true
            };
        }
    }
}