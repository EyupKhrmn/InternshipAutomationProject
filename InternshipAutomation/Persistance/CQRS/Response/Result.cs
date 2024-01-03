namespace InternshipAutomation.Persistance.CQRS.Response;

public class Result
{
    public string? Message { get; set; }
    public bool? Success { get; set; }
}

public class Result<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
}