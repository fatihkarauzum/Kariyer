using Hangfire;
using Kariyer.Schedule.Jobs.RecurringJobs;

namespace Kariyer.Schedule.Schedules;

public static class RecurringJobs {

	public static void SyncJobsOperation() {

		RecurringJob.RemoveIfExists(nameof(SyncJobs));
		RecurringJob.AddOrUpdate<SyncJobs>(nameof(SyncJobs), job => job.Process(1), "59 23 * * *");
	}
}
