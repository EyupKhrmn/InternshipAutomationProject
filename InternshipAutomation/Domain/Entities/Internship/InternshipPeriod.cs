using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Internship;

public class InternshipPeriod
{
    public Guid Id { get; set; }
    public DateTime StartedDate { get; set; }
    public List<Internship> Internships { get; set; }
}