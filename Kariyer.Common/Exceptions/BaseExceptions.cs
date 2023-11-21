namespace Kariyer.Common.Exceptions;

public class BaseExceptions {

    public string Message { get; set; } = "";
    public Exception? InnerException { get; set; }
    public string? Type { get; set; }
    public int? Code { get; set; }
    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
}
