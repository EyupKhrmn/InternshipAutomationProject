using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GiveNoteForDailyReportFileCommand : IRequest<Result>
{
    public Guid InternshipDailyReportFileId { get; set; }
    public int Note { get; set; }
    
    public class GiveNoteForDailyReportFileCommandHandler : IRequestHandler<GiveNoteForDailyReportFileCommand,Result>
    {
        private readonly IGeneralRepository _generalRepository;

        public GiveNoteForDailyReportFileCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<Result> Handle(GiveNoteForDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.InternshipDailyReportFileId, cancellationToken: cancellationToken);

            file.Note = request.Note;

            _generalRepository.Update(file);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new Result
            {
                    Message = "Günlük Staj raporuna puan verme işlemi başarılı",
                    Success = true
            };
        }
    }
}