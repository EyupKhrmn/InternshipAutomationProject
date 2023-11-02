using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipApplicationFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    public StudentUser StudentUser { get; set; }
    public Internship.Internship Internship { get; set; }
}