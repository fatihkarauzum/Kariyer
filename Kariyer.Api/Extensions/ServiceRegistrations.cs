using Castle.DynamicProxy;
using Kariyer.Business.Services;
using Kariyer.Business.Services.Impl;
using Kariyer.Business.Services.JobQuality;
using Kariyer.Business.Services.JobQuality.Impl;
using Kariyer.Common.Aspects;
using Kariyer.Common.Services.Impl;
using Kariyer.Common.Services;
using Kariyer.Core.Aspects;

namespace Kariyer.Api.Extensions;

public static class ServiceRegistration {

	public static void AddServiceInfrastructure(this IServiceCollection services) {

		services.AddSingleton(new ProxyGenerator());
		services.AddSingleton<CacheService, CacheServiceImpl>();
		services.AddScoped<IInterceptor, CacheableAspect>();
		services.AddScoped<IInterceptor, CacheEvictAspect>();

		services.AddScoped<CompanyService, CompanyServiceImpl>();
		services.AddProxiedScoped<DepartmentService, DepartmentServiceImpl>();
		services.AddScoped<IndustryService, IndustryServiceImpl>();
		services.AddScoped<JobService, JobServiceImpl>();
		services.AddScoped<HarmfulWordsService, HarmfulWordsServiceImpl>();
		services.AddScoped<JobQualityService, JobQualityServiceImpl>();
	}

	public static void AddProxiedScoped<TInterface, TImplementation>
			(this IServiceCollection services)
			where TInterface : class
			where TImplementation : class, TInterface {

		services.AddScoped<TImplementation>();
		services.AddScoped(typeof(TInterface), serviceProvider => {
			var proxyGenerator = serviceProvider
				.GetRequiredService<ProxyGenerator>();

			var actual = serviceProvider
				.GetRequiredService<TImplementation>();

			var interceptors = serviceProvider
				.GetServices<IInterceptor>().ToArray();

			return proxyGenerator.CreateInterfaceProxyWithTarget(
				typeof(TInterface), actual, interceptors);
		});
	}

}
