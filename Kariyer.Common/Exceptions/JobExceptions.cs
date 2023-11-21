
namespace Kariyer.Common.Exceptions;

public enum JobExceptionType {

	JOB_NOT_FOUND
}

public sealed class JobException : Exception {

	public JobException(JobExceptionType type, string message, int code) : base(message) {

		Data.Add("Code", code);
		Data.Add("Type", type.ToString());
		Data.Add("Exception", nameof(JobException));
	}
}

public static class JobExceptions {

	public static JobException JobNotFound(string message = "Job Not Found") =>
		new JobException(JobExceptionType.JOB_NOT_FOUND, message, 400);
}