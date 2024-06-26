using FluentValidation;
using InternshipAutomation.Persistance.CQRS.Internship;

namespace InternshipAutomation.Application.Validation.InternshipValidator;

public class InternshipValidator
{
    public class InternshipPeriodValidator : AbstractValidator<InternshipPeriodCommand>
    {
        public InternshipPeriodValidator()
        {
            RuleFor(_ => _.StartedDate)
                .LessThan(DateTime.UtcNow.Year)
                .WithMessage("Staj dönemi Aynı yıl içerisinde başlatılabilir");
        }
    }
    
    public class GiveNoteValidator : AbstractValidator<GiveNoteForInternshipCommand>
    {
        public GiveNoteValidator()
        {
            RuleFor(_ => _.Note).NotEmpty().LessThan(101);
            RuleFor(_ => _.InternshipId).NotEmpty();
        }
    }
}