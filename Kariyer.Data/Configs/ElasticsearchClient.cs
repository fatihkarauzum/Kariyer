using Kariyer.Model.Configurations;
using Microsoft.Extensions.Options;
using Nest;

namespace Kariyer.Data.Configs;

public class ElasticsearchClient {

	private readonly IElasticClient client;
	private readonly ElasticsearchConfig elasticsearchConfig;

	public ElasticsearchClient(IOptions<ElasticsearchConfig> elasticsearchConfig) {

		this.elasticsearchConfig = elasticsearchConfig.Value;

		this.client = CreateClient();
	}

	public IElasticClient Client { 

		get { 
			return client; 
		} 
	}

	private IElasticClient CreateClient() {

		ConnectionSettings connectionSettings = new ConnectionSettings(new Uri(elasticsearchConfig.Host));

		return new ElasticClient(connectionSettings);
	}
}
