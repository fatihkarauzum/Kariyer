namespace Kariyer.Common.Exceptions;

public enum IndustryExceptionType {

	INDUSTRY_NOT_FOUND
}

public sealed class IndustryException : Exception {

	public IndustryException(IndustryExceptionType type, string message, int code) : base(message) {

		Data.Add("Code", code);
		Data.Add("Type", type.ToString());
		Data.Add("Exception", nameof(IndustryException));
	}
}

public static class IndustryExceptions {

	public static IndustryException IndustryNotFound(string message = "Industry Not Found") =>
		new IndustryException(IndustryExceptionType.INDUSTRY_NOT_FOUND, message, 300);
}
