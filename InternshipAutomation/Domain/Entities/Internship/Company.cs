using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Internship;

public class Company
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string CompanyWorkingArea { get; set; }
    public List<Internship> Internships { get; set; }
    public List<CompanyUser> CompanyUsers { get; set; }
}