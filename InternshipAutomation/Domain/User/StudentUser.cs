using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;

namespace InternshipAutomation.Domain.User;

public class StudentUser : User
{
    public string StudentNumber { get; set; }
    public int Class { get; set; }
    public bool IsActive { get; set; }
    public List<SendingFile> SendingFiles { get; set; }
    public List<InternshipApplicationFile> InternshipApplicationFiles { get; set; }
    public Guid InternshipId { get; set; }
    public Internship Internship { get; set; }
}