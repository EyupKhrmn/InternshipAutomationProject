using System.Runtime.Serialization;
using InternshipAutomation.Domain.Entities.Enums;

namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipDailyReportFile
{
    public string StudentNameSurname { get; set; }
    public string StudentNumber { get; set; }
    public string CompanyName { get; set; }
    public string CompanyOfficerNameSurname { get; set; }
    public string TeachingStaffNameSurname { get; set; }
    public SchoolTerm SchoolTerm { get; set; }
    public DateTime EducationDate { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime WritingDate { get; set; }
    public int DayCount { get; set; }
}