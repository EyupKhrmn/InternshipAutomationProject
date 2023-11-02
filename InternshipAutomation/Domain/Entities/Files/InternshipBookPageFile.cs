namespace InternshipAutomation.Domain.Entities.Files;

public class InternshipBookPageFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    public DateTime WritingDate { get; set; }
    public Internship.Internship Internship { get; set; }
}