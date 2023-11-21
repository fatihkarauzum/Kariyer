namespace Kariyer.Common.Utils;

public static class DateTimeUtils {

	public static DateTime? SetKindUtc(this DateTime? dateTime) {

		if (dateTime.HasValue)
			return dateTime.Value.SetKindUtc();

		return null;
	}

	public static DateTime SetKindUtc(this DateTime dateTime) {

		if (dateTime.Kind == DateTimeKind.Utc)
			return dateTime;

		return DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
	}

	public static DateTime? SetKindLocal(this DateTime? dateTime) {

		if (dateTime.HasValue)
			return dateTime.Value.SetKindUtc();

		return null;
	}

	public static DateTime SetKindLocal(this DateTime dateTime) {

		if (dateTime.Kind == DateTimeKind.Local)
			return dateTime;

		return DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Local);
	}
}
