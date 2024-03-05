using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Controller.Filters;

public class ConsoleLogAttribute(string actionName, string callerId) : Attribute, IActionFilter
{
    private readonly string _actionName = actionName;
    private readonly string _callerId = callerId;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"{_actionName} Action Called By: {_callerId}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"{_actionName} Action Executed By: {_callerId}");
    }
}