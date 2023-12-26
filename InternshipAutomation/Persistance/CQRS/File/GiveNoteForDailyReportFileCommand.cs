using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GiveNoteForDailyReportFileCommand : IRequest<GiveNoteForDailyReportFileResponse>
{
    public Guid InternshipDailyReportFileId { get; set; }
    public int Note { get; set; }
    
    public class GiveNoteForDailyReportFileCommandHandler : IRequestHandler<GiveNoteForDailyReportFileCommand,GiveNoteForDailyReportFileResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public GiveNoteForDailyReportFileCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<GiveNoteForDailyReportFileResponse> Handle(GiveNoteForDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ => _.Id == request.InternshipDailyReportFileId, cancellationToken: cancellationToken);

            file.Note = request.Note;

            _generalRepository.Update(file);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return new GiveNoteForDailyReportFileResponse
            {
                    Message = "Günlük Staj raporuna puan verme işlemi başarılı",
                    Success = true
            };
        }
    }
}

public class GiveNoteForDailyReportFileResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}