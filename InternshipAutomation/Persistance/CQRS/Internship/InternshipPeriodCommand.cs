using InternshipAutomation.Application.Mail;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public record InternshipPeriodCommand : IRequest<Result>
{
    public int StartedDate { get; set; }

    public sealed class InternshipPeriodCommandHandler : IRequestHandler<InternshipPeriodCommand,Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly IEmailSender _emailSender;
        private readonly ILogService _logService;

        public InternshipPeriodCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, IEmailSender emailSender, ILogService logService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _emailSender = emailSender;
            _logService = logService;
        }

        public async Task<Result> Handle(InternshipPeriodCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var internshipPeriod = new InternshipPeriod
            {
                StartedDate = request.StartedDate,
                CreatedDate = DateTime.Now,
                User = currentUser
            };

            _generalRepository.Add(internshipPeriod);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            #region MailSender

            string mailSubject = "Staj Dönemi Başlatma";
            string mailContent =
                "Yeni staj dönemi başarıyla oluşturuldu. Staj dönemine kayıt yapan öğrencilerin bilgileri mail yolu ile paylaşılacaktır. Lütfen Gelen kutunuzu kontrol ediniz.";
            await _emailSender.SendEmailAsync(currentUser.Email,currentUser.UserName,mailSubject,mailContent);


            #endregion
            
            _logService.Information($"{currentUser.UserName} kullanısı yeni staj dönemi başlattı. Başlatılan staj dönemi: {internshipPeriod.Id}");
            
            return new Result
            {
                Message = "Yeni Staj Dönemi Başlatılmıştır !",
                Success = true
            };
        }
    }
}