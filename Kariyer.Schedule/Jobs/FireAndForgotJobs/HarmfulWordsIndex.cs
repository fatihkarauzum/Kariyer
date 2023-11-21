using Kariyer.Data.Repositories.Elasticsearch;
using Kariyer.Model.Documents;

namespace Kariyer.Schedule.Jobs.FireAndForgotJobs;

public class HarmfulWordsIndex {

	private readonly ElasticsearchRepository elasticsearchRepository;

	public HarmfulWordsIndex(ElasticsearchRepository elasticsearchRepository) {

		this.elasticsearchRepository = elasticsearchRepository;
	}

	public async Task Process() {

		await elasticsearchRepository.CreateIndexAsync<HarmfulWordsDocument>();
	}
}

