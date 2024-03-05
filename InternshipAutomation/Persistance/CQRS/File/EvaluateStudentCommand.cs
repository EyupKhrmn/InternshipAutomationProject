using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public record EvaluateStudentCommand : IRequest<Result>
{
    public Guid InternshipId { get; set; }
    public string WorkingArea { get; set; }
    public DateTime WorkingDate { get; set; }
    public int SelfConfidence { get; set; }
    public int Initiative { get; set; }
    public int InterestForWork { get; set; }
    public int Creativity { get; set; }
    public int Leadership { get; set; }
    public int Appearance { get; set; }
    public int CommunicationWithSupervisors  { get; set; }
    public int CommunicationWithWorkFriends { get; set; }
    public int CommunicationWithCustomers { get; set; }
    public int Attendance { get; set; }
    public int EfficiencyInTimeUsing { get; set; }
    public int ProblemSolving { get; set; }
    public int FamiliarityOfTeamWorks { get; set; }
    public int TechnicalKnowledge { get; set; }
    public int SuitabilityForJobStandards { get; set; }
    public int TakingOnResponsibility { get; set; }
    public int FulfillingTheDuties { get; set; }
    public int EffectiveUserOfResources { get; set; }
    public int FamiliarityOfTechnology { get; set; }
    public int BeingInnovative { get; set; }
    public bool WorkAgain { get; set; }
    public string DevelopmentSuggestionForStudentUser { get; set; }
    
    public class EvaluateStudentCommandHandler : IRequestHandler<EvaluateStudentCommand,Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly ILogService _logService;

        public EvaluateStudentCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, ILogService logService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _logService = logService;
        }

        public async Task<Result> Handle(EvaluateStudentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _decodeTokenService.GetUsernameFromToken();

            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_ => _.Id == request.InternshipId, cancellationToken: cancellationToken);

            var studentUser = await _generalRepository.Query<Domain.User.User>()
                .FirstOrDefaultAsync(_ => _.Id == internship.StudentUser, cancellationToken: cancellationToken);
            
            #region Null Control

            if (internship is null)
            {
                _logService.Error($"{request.InternshipId} id'li staj bulunamadı.");
                return new Result
                {
                    Message = "Stajyer bulunamadı.",
                    Success = false
                };
            }

            if (studentUser is null)
            {
                _logService.Error($"{internship.StudentUser} id'li öğrenci bulunamadı.");
                return new Result
                {
                    Message = "Staj içerisinde öğrenci bulunamadı.",
                    Success = false
                };
            }

            #endregion

            #region File

            var file = new InternshipEvaluationFormForCompany
            {
                StudentUserName = studentUser.StudentNameSurname,
                Appearance = request.Appearance,
                Attendance = request.Attendance,
                BeingInnovative = request.BeingInnovative,
                CommunicationWithCustomers = request.CommunicationWithCustomers,
                CommunicationWithSupervisors = request.CommunicationWithSupervisors,
                CommunicationWithWorkFriends = request.CommunicationWithWorkFriends,
                CreatedDate = DateTime.UtcNow,
                SelfConfidence = request.SelfConfidence,
                Creativity = request.Creativity,
                Leadership = request.Leadership,
                Initiative = request.Initiative,
                InterestForWork = request.InterestForWork,
                WorkingArea = request.WorkingArea,
                EfficiencyInTimeUsing = request.EfficiencyInTimeUsing,
                TechnicalKnowledge = request.TechnicalKnowledge,
                FulfillingTheDuties = request.FulfillingTheDuties,
                WorkAgain = request.WorkAgain,
                SuitabilityForJobStandards = request.SuitabilityForJobStandards,
                DevelopmentSuggestionForStudentUser = request.DevelopmentSuggestionForStudentUser,
                ProblemSolving = request.ProblemSolving,
                FamiliarityOfTechnology = request.FamiliarityOfTechnology,
                FamiliarityOfTeamWorks = request.FamiliarityOfTeamWorks,
                TakingOnResponsibility = request.TakingOnResponsibility,
                SupervisorNameSurname = currentUser.CompanyUserNameSurname,
                EffectiveUserOfResources = request.EffectiveUserOfResources,
                WorkingDate = request.WorkingDate,
                Internship = internship,
                InternshipId = internship.Id,
                
            };

            #endregion

            _generalRepository.Add(file);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            _logService.Information(
                $"{currentUser.UserName} kullanıcısı {studentUser.UserName} kullanıcısını değerlendirdi.");

            return new Result
            {
                Message = "Staj Öğrencisini değerlendirme işlemi başarıyla gerçekleşti",
                Success = true
            };

        }
    }
}