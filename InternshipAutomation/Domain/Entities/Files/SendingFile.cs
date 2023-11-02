using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Files;

public class SendingFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    public DateTime SendingDate { get; set; }
    public StudentUser StudentUser { get; set; }
}