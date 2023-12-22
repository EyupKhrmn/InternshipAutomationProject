using InternshipAutomation.Application.Mail;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class InternshipPeriodCommand : IRequest<InternshipPeriodResponse>
{
    public int StartedDate { get; set; }

    public class InternshipPeriodCommandHandler : IRequestHandler<InternshipPeriodCommand,InternshipPeriodResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly IEmailSender _emailSender;

        public InternshipPeriodCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, IEmailSender emailSender)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _emailSender = emailSender;
        }

        public async Task<InternshipPeriodResponse> Handle(InternshipPeriodCommand request, CancellationToken cancellationToken)
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