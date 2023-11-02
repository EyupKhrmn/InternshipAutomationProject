namespace InternshipAutomation.Domain.Entities.Base;

public interface IEntity
{
    public DateTime LastModificationDate { get; set; }
    public DateTime CreatedDate { get; set; }
}