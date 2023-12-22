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
                .GreaterThan(DateTime.UtcNow.Year - 1)
                .WithMessage("Geçmiş dönemler için staj dönemi oluşturulamaz");
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