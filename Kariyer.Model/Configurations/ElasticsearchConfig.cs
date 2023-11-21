namespace Kariyer.Model.Configurations;

public class ElasticsearchConfig {

    public required string Host { get; set; }
	public string? UserName { get; set; }
    public string? Password { get; set; }
}
