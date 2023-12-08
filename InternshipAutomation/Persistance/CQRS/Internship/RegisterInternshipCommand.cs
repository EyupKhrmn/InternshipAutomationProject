using System.Collections.Concurrent;
using InternshipAutomation.Application.Mail;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class RegisterInternshipCommand : IRequest<RegisterInternshipResponse>
{
    public InternshipApplicationDto InternshipApplication { get; set; }
    public Guid InternshipPeriodId { get; set; }
    
    
    public class RegisterInternshipCommandHandler : IRequestHandler<RegisterInternshipCommand,RegisterInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterInternshipCommandHandler(IGeneralRepository generalRepository, IHttpContextAccessor httpContextAccessor, IDecodeTokenService decodeTokenService, UserManager<Domain.User.User> userManager, IEmailSender emailSender)
        {
            _generalRepository = generalRepository;
            _httpContextAccessor = httpContextAccessor;
            _decodeTokenService = decodeTokenService;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<RegisterInternshipResponse> Handle(RegisterInternshipCommand request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];

            var currentUserUsername = _decodeTokenService.GetUsernameFromToken(token);
            
            var internshipPeriod = await _generalRepository
                .Query<InternshipPeriod>()
                .SingleOrDefaultAsync(_ => _.Id == request.InternshipPeriodId, cancellationToken: cancellationToken);

            var studentUser = await _userManager.FindByNameAsync(currentUserUsername);
            //var teacherUser = await _userManager.FindByNameAsync(//yeni gelen kullanıcı ıd si ile);

            var internship = new Domain.Entities.Internship.Internship
            {
                StudentUser = studentUser, //TODO o anki kullanıcının öğrenci verileri alındı -----SİLİNECEK
                TeacherUser = request.InternshipApplication.TeacherUser, //TODO internship üzerinden gelen UserID ile doldurulacak
                CompanyUser = request.InternshipApplication.CompanyUser,
                InternshipApplicationFile = request.InternshipApplication.InternshipApplicationFile,
                InternshipPeriod = internshipPeriod
            };
            
            _generalRepository.Add(internship);
            internshipPeriod.Internships.Add(internship);
            _generalRepository.Update(internshipPeriod);

            await _generalRepository.SaveChangesAsync(cancellationToken);

            #region MailSender

            string mailSubject = "Staj Kayıt İşlemi";
            string mailContent = "Staj dönemine kayıt işlemini başarıyla gerçekleşti";

            await _emailSender.SendEmailAsync(studentUser.Email, studentUser.UserName, mailSubject, mailContent);

            
            //TODO Öğretmen kullanıcıya mail gönderme işlemi öğrencinin kayıt yaptığı dönemi başlatan öğretmene gidecek
            string mailSubjectForTeacher = "Staj Dönemi Kayıt İşlemi";
            string mailContentForTeacher =
                $"Staj döneminize yeni kayıt gerçekleşmiştir. kayıt olan öğrenci numarası: {studentUser.UserName}";

            //await _emailSender.SendEmailAsync();

            #endregion
            
            return new RegisterInternshipResponse
            {
                Message = "Staj Dönemine Kaydınız başarıyla tamamlanmıştır.",
                Success = true
            };
        }
     }
}

public class RegisterInternshipResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}