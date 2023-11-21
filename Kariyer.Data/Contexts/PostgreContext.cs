using Kariyer.Model.Configurations;
using Kariyer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kariyer.Data.Contexts;

public class PostgreContext : DbContext {

    public readonly PostgreConfig postgreConfig;

    public PostgreContext(DbContextOptions<PostgreContext> options, IOptions<PostgreConfig> postgreConfig)
        : base(options) {

        this.postgreConfig = postgreConfig.Value;
    }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        optionsBuilder.UseNpgsql(postgreConfig.ConnectionString);
	}

	public DbSet<Company> Company { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Industry> Industry { get; set; }
    public DbSet<Job> Job { get; set; }
}
 