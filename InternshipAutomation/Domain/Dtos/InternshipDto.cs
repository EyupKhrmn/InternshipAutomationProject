using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;

namespace InternshipAutomation.Domain.Dtos;

public class InternshipDto
{
    public string? StudentUser { get; set; }
    public string? TeacherUser { get; set; }
    public string? CompanyUser { get; set; }
    public InternshipStatus? Status { get; set; }
    public int? Note { get; set; }
    public double InternshipAverage { get; set; }
}