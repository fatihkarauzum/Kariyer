using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kariyer.Common.Exceptions;

public enum CompanyExceptionType {

    COMPANY_NOT_FOUND, DUPLICATE_PHONE_NUMBER, NO_PUBLISH_RIGHT
}

public sealed class CompanyException : Exception
{

    public CompanyException(CompanyExceptionType type, string message, int code) : base(message)
    {

        Data.Add("Code", code);
		Data.Add("Type", type.ToString());
		Data.Add("Exception", nameof(CompanyException));
	}
}

public static class CompanyExceptions {

    public static CompanyException CompanyNotFound(string message = "Company Not Found") => 
        new CompanyException(CompanyExceptionType.COMPANY_NOT_FOUND, message, 100);

	public static CompanyException DuplicatePhoneNumber(string message = "Duplicate Phone Number") =>
	   new CompanyException(CompanyExceptionType.DUPLICATE_PHONE_NUMBER, message, 101);

	public static CompanyException NoPublishRight(string message = "No Publish Right") =>
	   new CompanyException(CompanyExceptionType.NO_PUBLISH_RIGHT, message, 101);
}
