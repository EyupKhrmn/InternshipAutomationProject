using System.Text.Json.Serialization;
using InternshipAutomation.Domain.Entities.Base;
using InternshipAutomation.Domain.Entities.Enums;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.User;
using Newtonsoft.Json;

namespace InternshipAutomation.Domain.Entities.Internship;

public class Internship : Entity
{
    public Guid Id { get; set; }
    public Guid? StudentUser { get; set; }
    public Guid? TeacherUser { get; set; }
    public Guid? CompanyUser { get; set; }
    public InternshipStatus? Status { get; set; }
    public InternshipApplicationFile? InternshipApplicationFile { get; set; }
    public List<InternshipDailyReportFile>? InternshipDailyReportFiles { get; set; }
    public StateContributionFile? StateContributionFile { get; set; }
    public InternshipResultReport? InternshipResultReport { get; set; }
    public InternshipEvaluationFormForCompany? InternshipEvaluationFormForCompany { get; set; }
    public int? Note { get; set; }
    public InternshipPeriod? InternshipPeriod { get; set; }
    public double InternshipAverage { get; set; }

    
}