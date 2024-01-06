using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
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
        private readonly ILogService _logService;

        public GiveNoteForDailyReportFileCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result> Handle(GiveNoteForDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .Include(_=>_.Internship)
                .FirstOrDefaultAsync(_ => _.Id == request.InternshipDailyReportFileId, cancellationToken: cancellationToken);

            file.Note = request.Note;

            _generalRepository.Update(file);
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            _logService.Information($"{file.StudentNameSurname} kullanısının {file.WorkingDate} tarihli staj günlük raporuna puan verildi. Verilen puan: {file.Note}. Staj: {file.Internship.Id}. Puan veren: {file.Internship.TeacherUser}");

            return new Result
            {
                    Message = "Günlük Staj raporuna puan verme işlemi başarılı",
                    Success = true
            };
        }
    }
}