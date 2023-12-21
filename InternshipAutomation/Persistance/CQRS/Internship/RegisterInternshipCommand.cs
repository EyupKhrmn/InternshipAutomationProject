using System.Collections.Concurrent;
using InternshipAutomation.Application.Mail;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class RegisterInternshipCommand : IRequest<RegisterInternshipResponse>
{
    public InternshipApplicationDto InternshipApplication { get; set; }
    public Guid InternshipPeriod { get; set; }
    
    
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
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var internshipPeriod = await _generalRepository
                .Query<InternshipPeriod>()
                .SingleOrDefaultAsync(_ => _.Id == request.InternshipPeriod, cancellationToken: cancellationToken);

            InternshipApplicationFile internshipApplicationFile = new()
            {
                CompanyName = request.InternshipApplication.InternshipApplicationFile.CompanyName,
                CompanySector = request.InternshipApplication.InternshipApplicationFile.CompanySector,
                StudentProgram = request.InternshipApplication.InternshipApplicationFile.StudentProgram,
                FinishedDate = request.InternshipApplication.InternshipApplicationFile.FinishedDate,
                StartedDate = request.InternshipApplication.InternshipApplicationFile.StartedDate,
                StudentNumber = request.InternshipApplication.InternshipApplicationFile.StudentNumber,
                CompanyEMail = request.InternshipApplication.InternshipApplicationFile.CompanyEMail,
                StudentPhoneNumber = request.InternshipApplication.InternshipApplicationFile.StudentPhoneNumber,
                CompanyPhoneNumber = request.InternshipApplication.InternshipApplicationFile.CompanyPhoneNumber,
                StudentNameSurname = request.InternshipApplication.InternshipApplicationFile.StudentNameSurname,
                StudentAGNO = request.InternshipApplication.InternshipApplicationFile.StudentAGNO,
                StudentTCKN = request.InternshipApplication.InternshipApplicationFile.StudentTCKN
            };

            var studentUser = currentUser;
            //var teacherUser = await _userManager.FindByIdAsync(request.InternshipApplication.TeacherUser.ToString() ?? string.Empty);

            var internship = new Domain.Entities.Internship.Internship
            {
                StudentUser = studentUser,
                TeacherUser = request.InternshipApplication.TeacherUser, //TODO tıklanan internship üzerinden gelen UserID ile doldurulacak
                CompanyUser = request.InternshipApplication.CompanyUser,
                InternshipApplicationFile = internshipApplicationFile,
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