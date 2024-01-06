using Serilog;

namespace InternshipAutomation.Persistance.LogService;

public class LogService : ILogService
{
    public void Information(string message)
    {
        Log.Information(message);
    }

    public void InformationForParam(string message, object? param)
    {
        if (param is not null)
        {
            Log.Information("message and param => {@param}", param);
        }
        else
        {
            Log.Information(message);
        }
    }

    public void Error(string message, object? param)
    {
        if (param is not null)
        {
            Log.Error("message and param => {@param}", param);
        }
        else
        {
            Log.Error(message);
        }
    }

    public void Error(string message)
    {
        Log.Error(message);
    }
}