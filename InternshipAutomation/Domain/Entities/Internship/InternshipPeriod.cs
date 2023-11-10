using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Domain.Entities.Internship;

public class InternshipPeriod
{
    public Guid Id { get; set; }
    public int StartedDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public User.User? User { get; set; }
    public List<Internship>? Internships { get; set; }
}