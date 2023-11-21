using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Kariyer.Core.Middlewares;

public class ExceptionHandler
{

    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandler> logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {

        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {

        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/json";

			string result = SerializeException(exception);

            logger.LogError(message: result);
            await context.Response.WriteAsync(result);
        }
    }

    private string SerializeException(Exception exception) {

        return JsonSerializer
			.Serialize(
			new {
				innerException = GetSerializableInnerException(exception.InnerException),
				message = exception.Message,
                type = exception.Data["Type"] ?? nameof(Exception),
				code = exception.Data["Code"],
				exception = exception.Data["Exception"] ?? nameof(Exception),
				stackTrace = exception.StackTrace
			});
	}

	private object? GetSerializableInnerException(Exception? innerException) {

		if (innerException == null)
			return null;

		return new {
			message = innerException.Message,
			type = innerException.GetType().FullName,
			stackTrace = innerException.StackTrace
		};
	}
}