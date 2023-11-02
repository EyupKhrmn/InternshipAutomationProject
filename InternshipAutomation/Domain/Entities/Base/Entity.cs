namespace InternshipAutomation.Domain.Entities.Base;

public class Entity : IEntity
{
    public DateTime LastModificationDate { get; set; }
    public DateTime CreatedDate { get; set; }
}