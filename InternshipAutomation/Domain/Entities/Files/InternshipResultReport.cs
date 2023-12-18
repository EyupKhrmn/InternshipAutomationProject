using InternshipAutomation.Domain.Entities.Enums;

namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipResultReport
{
    public Guid? Id { get; set; }
    public string StudentNameSurname { get; set; }
    public string StudentNumber { get; set; }
    public string StudentProgram { get; set; }
    public string CompanyName { get; set; }
    public string CompanyManager { get; set; }
    public string TeacherNameSurname { get; set; }
    public SchoolTerm SchoolTerm { get; set; }
    public string AcademicYear { get; set; }

    public Guid InternshipId { get; set; }
    public Internship.Internship Internship { get; set; }
}