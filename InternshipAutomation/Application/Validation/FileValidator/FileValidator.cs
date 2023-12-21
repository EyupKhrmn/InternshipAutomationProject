using FluentValidation;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.File;

namespace InternshipAutomation.Application.Validation.FileValidator;

public class FileValidator : AbstractValidator<AddDailyReportFileCommand>
{
    public FileValidator()
    {
        RuleFor(_ => _.TopicTitleOfWork).MaximumLength(61)
            .WithMessage("Öğrenci Adı en fazla 61 karakter olabilir")
            .NotEmpty();

        RuleFor(_ => _.DescriptionOfWork)
            .NotEmpty()
            .WithMessage("İş Açıklaması boş geçilemez");
        
    }
}