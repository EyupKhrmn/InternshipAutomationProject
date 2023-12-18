using InternshipAutomation.Domain.Entities.Base;

namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipEvaluationFormForCompany : Entity
{
    public Guid Id { get; set; }
    public string StudentUserName { get; set; }
    public string WorkingArea { get; set; }
    public DateTime WorkingDate { get; set; }
    public int SelfConfidence { get; set; }
    public int Initiative { get; set; }
    public int InterestForWork { get; set; }
    public int Creativity { get; set; }
    public int Leadership { get; set; }
    public int Appearance { get; set; }
    public int CommunicationWithSupervisors  { get; set; }
    public int CommunicationWithWorkFriends { get; set; }
    public int CommunicationWithCustomers { get; set; }
    public int Attendance { get; set; }
    public int EfficiencyInTimeUsing { get; set; }
    public int ProblemSolving { get; set; }
    public int FamiliarityOfTeamWorks { get; set; }
    public int TechnicalKnowledge { get; set; }
    public int SuitabilityForJobStandards { get; set; }
    public int TakingOnResponsibility { get; set; }
    public int FulfillingTheDuties { get; set; }
    public int EffectiveUserOfResources { get; set; }
    public int FamiliarityOfTechnology { get; set; }
    public int BeingInnovative { get; set; }
    public string SupervisorNameSurname { get; set; }
    public bool WorkAgain { get; set; }
    public string DevelopmentSuggestionForStudentUser { get; set; }

    public Guid InternshipId { get; set; }
    public Internship.Internship Internship { get; set; }
}