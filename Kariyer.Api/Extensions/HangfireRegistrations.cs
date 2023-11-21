using Hangfire;
using Hangfire.PostgreSql;
using HangfireBasicAuthenticationFilter;
using Kariyer.Model.Configurations;
using Kariyer.Schedule.Schedules;
using Microsoft.Extensions.DependencyInjection;

namespace Kariyer.Api.Extensions;

public static class HangfireRegistrations {

	public static void ConfigureHangfireServices(this IServiceCollection services, ConfigurationManager configuration) {

		IConfigurationSection hangfireConfigs = configuration.GetSection("HangfireConfig");
		services.Configure<HangfireConfig>(hangfireConfigs);

		services.AddHangfire(config => config
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UsePostgreSqlStorage(options => {

				options.UseNpgsqlConnection(hangfireConfigs.GetSection("ConnectionString").Value);
			}));

		services.AddHangfireServer();
	}

	public static void ConfigureHangfireDashboard(this WebApplication app, ConfigurationManager configuration) {

		IConfigurationSection hangfireConfigs = configuration.GetSection("HangfireConfig");

		app.UseHangfireDashboard();
		app.MapHangfireDashboard("/hangfire", new DashboardOptions() {
			DashboardTitle = "Kariyer",
			Authorization = new [] {

				new HangfireCustomBasicAuthenticationFilter() {

					Pass = hangfireConfigs.GetSection("Password").Value,
					User = hangfireConfigs.GetSection("Username").Value
				}
			}
		});

		SetDefaultJobs();
	}

	private static void SetDefaultJobs() {

		FireAndForgotJobs.JobIndexOperations();
		RecurringJobs.SyncJobsOperation();
	}

}
