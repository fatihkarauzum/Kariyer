using Hangfire;
using Kariyer.Schedule.Jobs.FireAndForgotJobs;

namespace Kariyer.Schedule.Schedules;

public static class FireAndForgotJobs {

	public static void JobIndexOperations() {

		BackgroundJob.Enqueue<JobIndex>(job => job.Process());
		BackgroundJob.Enqueue<HarmfulWordsIndex>(job => job.Process());
	}
}
