using Kariyer.Data.Repositories.Elasticsearch;
using Kariyer.Model.Documents;

namespace Kariyer.Schedule.Jobs.FireAndForgotJobs;

public class JobIndex {

	private readonly ElasticsearchRepository elasticsearchRepository;

	public JobIndex(ElasticsearchRepository elasticsearchRepository) {

		this.elasticsearchRepository = elasticsearchRepository;
	}

	public async Task Process() {

		await elasticsearchRepository.CreateIndexAsync<JobDocument>();
	}
}
