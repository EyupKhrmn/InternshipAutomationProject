using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Controller.Filters;

public class ConsoleLogAttribute(string actionName) : Attribute, IActionFilter
{
    private readonly string _actionName = actionName;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"{_actionName} Action Called In Time: {DateTime.Now}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"{_actionName} Action Executed In Time: {DateTime.Now}");
    }
}