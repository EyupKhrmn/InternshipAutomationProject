namespace InternshipAutomation.Persistance.LogService;

public interface ILogService
{
    public void Information(string message);
    public void InformationForParam(string message, object? param);
    
    public void Error(string message, object? param);
}