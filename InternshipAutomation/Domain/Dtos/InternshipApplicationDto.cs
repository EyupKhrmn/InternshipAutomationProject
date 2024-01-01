using InternshipAutomation.Domain.Entities.Files;

namespace InternshipAutomation.Domain.Dtos;

public class InternshipApplicationDto
{
    public Guid? TeacherUser { get; set; }
    public Guid? CompanyUser { get; set; }
    public bool IsApproved { get; set; }
    public InternshipApplicationFileDto? InternshipApplicationFile { get; set; }
}