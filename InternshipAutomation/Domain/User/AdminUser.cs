using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;

namespace InternshipAutomation.Domain.User;

public class AdminUser : User
{
    public List<BackUpFile> BackUpFiles { get; set; }
    public Guid InternshipId { get; set; }
    public List<Internship> Internship { get; set; }
}