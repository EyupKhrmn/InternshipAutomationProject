namespace InternshipAutomation.Domain.Dtos;

public class InternDto
{
    public Guid? InternId { get; set; }
    public string NameSurname { get; set; }
    public string StudentTCKN { get; set; }
    public string StundetPhoneNumber { get; set; }
    public string StundentEmail { get; set; }
    public string StudentProgram { get; set; }
    public float? StudentAGNO { get; set; }
}