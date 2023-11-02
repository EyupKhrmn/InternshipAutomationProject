using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Files;

public class BackUpFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    public DateTime SendingDate { get; set; }
    public AdminUser AdminUser { get; set; }
}