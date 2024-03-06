using InternshipAutomation.Application.Mail;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public record GiveNoteForDailyReportFileCommand : IRequest<Result>
{
    public Guid InternshipDailyReportFileId { get; set; }
    public bool IsApproved { get; set; }
    public int Note { get; set; }
    
    public sealed class GiveNoteForDailyReportFileCommandHandler : IRequestHandler<GiveNoteForDailyReportFileCommand,Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;
        private readonly IEmailSender _emailSender;
        private readonly IDecodeTokenService _decodeTokenService;

        public GiveNoteForDailyReportFileCommandHandler(IGeneralRepository generalRepository, ILogService logService, IEmailSender emailSender, IDecodeTokenService decodeTokenService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
            _emailSender = emailSender;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<Result> Handle(GiveNoteForDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .Include(_=>_.Internship)
                .FirstOrDefaultAsync(_ => _.Id == request.InternshipDailyReportFileId, cancellationToken: cancellationToken);

            if (file is null)
            {
                _logService.Error($"{request.InternshipDailyReportFileId} id'li staj günlük raporu bulunamadı.");
                return new Result
                {
                    Message = "Staj günlük raporu bulunamadı.",
                    Success = false
                };
            }

            file.IsApproved = request.IsApproved;
            file.Note = request.Note;

            if (!file.IsApproved)
            {
                var user = await _generalRepository.Query<Domain.User.User>()
                    .FirstOrDefaultAsync(_=>_.Id == file.Internship.StudentUser, cancellationToken: cancellationToken);
                _emailSender.SendEmailAsync(user.Email,user.UserName,"Staj Günlük Raporu Onayı","Staj Günlük Raporunuz onaylanmadı. Lütfen tekrar gönderiniz.");
                _logService.Information($"{request.InternshipDailyReportFileId} id'li staj günlük raporu {currentUser.Id} kullanıcısı tarafından reddedildi.");
            }

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