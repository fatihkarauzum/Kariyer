namespace Kariyer.Common.Exceptions;

public enum DepartmentExceptionType {

	DEPARTMENT_NOT_FOUND
}

public sealed class DepartmentException : Exception {

	public DepartmentException(DepartmentExceptionType type, string message, int code) : base(message) {

		Data.Add("Code", code);
		Data.Add("Type", type.ToString());
		Data.Add("Exception", nameof(DepartmentException));
	}
}

public static class DepartmentExceptions {

	public static DepartmentException DepartmentNotFound(string message = "Department Not Found") =>
		new DepartmentException(DepartmentExceptionType.DEPARTMENT_NOT_FOUND, message, 200);
}
