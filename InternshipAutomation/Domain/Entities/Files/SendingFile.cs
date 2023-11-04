using System.ComponentModel.DataAnnotations.Schema;
using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Files;

public class SendingFile
{
    public int Id { get; set; }
    [NotMapped]
    public IFormFile FileData { get; set; }
    public DateTime SendingDate { get; set; }
}