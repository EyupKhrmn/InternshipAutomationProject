namespace InternshipAutomation.Domain.Dtos;

public class InternshipApplicationFileDto
{
    public string? StudentNameSurname { get; set; }
    public string? StudentNumber { get; set; }
    public string? StudentTCKN { get; set; }
    public string? StudentPhoneNumber { get; set; }
    public string? StudentProgram { get; set; }
    public float? StudentAGNO { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyEMail { get; set; }
    public string? CompanySector { get; set; }
}