using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Internship;

public class Internship
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime FinishedDate { get; set; }
    public DateTime? EvaluationDate { get; set; }
    public bool IsWorkingSaturday { get; set; }
    //staj mentor bilgileri
    public string ApprovedUserName { get; set; }
    public bool IsApproved { get; set; }
    public InternshipStatus InternshipStatus { get; set; }
    public Company Company { get; set; }

    #region Users
    public Guid AdminUserId { get; set; }
    public Guid StudentUserId { get; set; }
    public StudentUser StudentUser { get; set; }
    public Guid CompanyUserId { get; set; }
    public CompanyUser CompanyUser { get; set; }

    #endregion

    #region FileManagement

    

    #endregion
}