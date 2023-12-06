using Azure.Core;
using InternshipAutomation.Domain.Entities.Base;

namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipApplicationFile : Entity
{
    public Guid Id { get; set; }
    #region Student

    public string? StudentNameSurname { get; set; }
    public string? StudentNumber { get; set; }
    public string? StudentTCKN { get; set; }
    public string? StudentPhoneNumber { get; set; }
    public string? StudentProgram { get; set; }
    public float? StudentAGNO { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? FinishedDate { get; set; }

    #endregion

    #region Company

    public string? CompanyName { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyEMail { get; set; }
    public string? CompanySector { get; set; }

    #endregion
}