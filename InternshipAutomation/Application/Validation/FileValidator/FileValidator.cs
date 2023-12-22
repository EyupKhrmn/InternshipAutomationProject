using System.Data;
using FluentValidation;
using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;

namespace InternshipAutomation.Application.Validation.FileValidator;

public class FileValidator
{
    public class DailReportFileValidator : AbstractValidator<AddDailyReportFileCommand>
    {
        public DailReportFileValidator()
        {
            RuleFor(_ => _.DescriptionOfWork)
                .NotEmpty().MaximumLength(12);
            
            RuleFor(_ => _.TopicTitleOfWork).NotEmpty().MaximumLength(61)
                .WithMessage("Çok fazla karakter kullandınız.");
            
            RuleFor(_ => _.InternshipId)
                .NotEmpty();
        }
    }
    
    public class EvaluateStudentFileValidator : AbstractValidator<EvaluateStudentCommand>
    {
        public EvaluateStudentFileValidator()
        {
            RuleFor(_ => _.InternshipId).NotEmpty();
            RuleFor(_ => _.Appearance).NotEmpty();
            RuleFor(_ => _.Attendance).NotEmpty();
            RuleFor(_ => _.Creativity).NotEmpty();
            RuleFor(_ => _.BeingInnovative).NotEmpty();
            RuleFor(_ => _.Initiative).NotEmpty();
            RuleFor(_ => _.Leadership).NotEmpty();
            RuleFor(_ => _.ProblemSolving).NotEmpty();
            RuleFor(_ => _.SelfConfidence).NotEmpty();
            RuleFor(_ => _.TechnicalKnowledge).NotEmpty();
            RuleFor(_ => _.WorkAgain).NotEmpty();
            RuleFor(_ => _.WorkingArea).NotEmpty();
            RuleFor(_ => _.WorkingDate).NotEmpty();
            RuleFor(_ => _.CommunicationWithCustomers).NotEmpty();
            RuleFor(_=>_.CommunicationWithSupervisors).NotEmpty();
            RuleFor(_=>_.FamiliarityOfTechnology).NotEmpty();
            RuleFor(_=>_.FulfillingTheDuties).NotEmpty();
            RuleFor(_=>_.InterestForWork).NotEmpty();
            RuleFor(_=>_.TakingOnResponsibility).NotEmpty();
            RuleFor(_=>_.CommunicationWithWorkFriends).NotEmpty();
            RuleFor(_=>_.EffectiveUserOfResources).NotEmpty();
            RuleFor(_=>_.EfficiencyInTimeUsing).NotEmpty();
            RuleFor(_=>_.FamiliarityOfTeamWorks).NotEmpty();
            RuleFor(_=>_.SuitabilityForJobStandards).NotEmpty();
            RuleFor(_=>_.DevelopmentSuggestionForStudentUser).NotEmpty();
        }
    }
    
    public class ApplicationFileValidator : AbstractValidator<RegisterInternshipCommand>
    {
        public ApplicationFileValidator()
        {
            RuleFor(_ => _.InternshipPeriod).NotEmpty();
            RuleFor(_ => _.InternshipApplication.IsApproved).NotNull();
            RuleFor(_ => _.InternshipApplication.TeacherUser).NotEmpty();
            RuleFor(_ => _.InternshipApplication.CompanyUser).NotEmpty();
        }
    }
}