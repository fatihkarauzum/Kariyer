using Kariyer.Api.Extensions;
using Kariyer.Core.Extensions;
using Kariyer.Core.Middlewares;
using Kariyer.Model.Configurations;
using Serilog;
using Serilog.Sinks.Graylog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var logger = new LoggerConfiguration()
			.Enrich.FromLogContext()
			.WriteTo.Graylog(new GraylogSinkOptions {
				HostnameOrAddress = configuration.GetValue<string>("Logging:Graylog:Address"),
				Port = configuration.GetValue<int>("Logging:Graylog:Port"),
				TransportType = Serilog.Sinks.Graylog.Core.Transport.TransportType.Udp
			})
			.CreateLogger();

IServiceCollection services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<PostgreConfig>(configuration.GetSection("PostgreConfig"));
services.Configure<ElasticsearchConfig>(configuration.GetSection("ElasticsearchConfig"));

services.AddDataInfrastructure();
services.AddServiceInfrastructure();
services.ConfigureHangfireServices(configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureHangfireDashboard(configuration);
app.UseMiddleware<ExceptionHandler>();

app.Run();
