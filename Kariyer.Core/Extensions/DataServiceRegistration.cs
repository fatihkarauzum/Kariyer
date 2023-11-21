using Castle.DynamicProxy;
using Kariyer.Data.Configs;
using Kariyer.Data.Contexts;
using Kariyer.Data.Repositories;
using Kariyer.Data.Repositories.Elasticsearch;
using Kariyer.Data.Repositories.Elasticsearch.Impl;
using Kariyer.Data.Repositories.Impl;

using Microsoft.Extensions.DependencyInjection;

namespace Kariyer.Core.Extensions;

public static class DataServiceRegistration {

	public static void AddDataInfrastructure(this IServiceCollection services) {

		services.AddDbContext<PostgreContext>();
		services.AddSingleton<ElasticsearchClient>();

		services.AddScoped<CompanyRepository, CompanyRepositoryImpl>();
		services.AddScoped<DepartmentRepository, DepartmentRepositoryImpl>();
		services.AddScoped<IndustryRepository, IndustryRepositoryImpl>();
		services.AddScoped<JobRepository, JobRepositoryImpl>();
		services.AddScoped<ElasticsearchRepository, ElasticsearcReposityorhImpl>();

		services.AddScoped<UnitOfWork, UnitOfWorkImpl>();
	}

}
