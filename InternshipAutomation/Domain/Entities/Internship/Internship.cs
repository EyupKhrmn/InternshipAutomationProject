using System.Text.Json.Serialization;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.User;
using Newtonsoft.Json;

namespace InternshipAutomation.Domain.Entities.Internship;

public class Internship
{
    public Guid Id { get; set; }
    //public string Title { get; set; }
    //public string Description { get; set; }
    //public DateTime StartedDate { get; set; }
    //public DateTime FinishedDate { get; set; }
    //public DateTime? EvaluationDate { get; set; }
    //public bool IsWorkingSaturday { get; set; }
    ////staj mentor bilgileri
    //public string ApprovedUserName { get; set; }
    //public bool IsApproved { get; set; }
    //public InternshipStatus InternshipStatus { get; set; }
    //public Company Company { get; set; }
    //public Guid AdminUserId { get; set; }
    //public Guid StudentUserId { get; set; }
    //public StudentUser StudentUser { get; set; }
    //public Guid CompanyUserId { get; set; }
    //public CompanyUser CompanyUser { get; set; }

    public Guid? StudentUser { get; set; }
    public Guid? TeacherUser { get; set; }
    public Guid? CompanyUser { get; set; }
    public InternshipStatus Status { get; set; }

    public InternshipApplicationFile? InternshipApplicationFile { get; set; }
    public InternshipDailyReportFile? InternshipDailyReportFile { get; set; }
    public StateContributionFile? StateContributionFile { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public InternshipPeriod? InternshipPeriod { get; set; }

    
}