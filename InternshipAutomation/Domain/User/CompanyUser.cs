using InternshipAutomation.Domain.Entities.Internship;

namespace InternshipAutomation.Domain.User;

public class CompanyUser : User
{
    public Company Company { get; set; }
    public Guid InternshipId { get; set; }
    public Internship Internship { get; set; }   
}