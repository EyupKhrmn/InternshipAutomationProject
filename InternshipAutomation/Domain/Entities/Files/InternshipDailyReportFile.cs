using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using InternshipAutomation.Domain.Entities.Base;
using InternshipAutomation.Domain.Entities.Enums;

namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipDailyReportFile : Entity
{
    public Guid? Id { get; set; }
    public string TopicTitleOfWork { get; set; }
    public string DescriptionOfWork  { get; set; }
    public string StudentNameSurname { get; set; }
    public string CompanyManagerNameSurname { get; set; }
    public DateTime WorkingDate { get; set; }
    public Internship.Internship Internship { get; set; }
}