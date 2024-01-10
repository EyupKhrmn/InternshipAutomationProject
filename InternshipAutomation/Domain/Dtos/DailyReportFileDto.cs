namespace InternshipAutomation.Domain.Dtos;

public class DailyReportFileDto
{
    public Guid? FileId { get; set; }
    public string TopicTitleOfWork { get; set; }
    public string DescriptionOfWork  { get; set; }
    public string StudentNameSurname { get; set; }
    public string CompanyManagerNameSurname { get; set; }
    public DateTime WorkingDate { get; set; }
    public int? Note { get; set; }
}