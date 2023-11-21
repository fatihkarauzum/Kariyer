using Kariyer.Data.Repositories.Elasticsearch;
using Kariyer.Data.Repositories;
using Kariyer.Model.Documents;
using Kariyer.Data.Repositories.Elasticsearch.Queries;

namespace Kariyer.Business.Services.Impl;

public class HarmfulWordsServiceImpl : HarmfulWordsService {

	public ElasticsearchRepository elasticsearchRepository;

	public HarmfulWordsServiceImpl(ElasticsearchRepository elasticsearchRepository) {

		this.elasticsearchRepository = elasticsearchRepository;
	}

	public async Task<List<string>> List(string word = "") {

		var hitHarmfulWordDocumentList = await elasticsearchRepository.SearchDocumentsAsync(HarmfulWordsQueries.Get(word));

		List<HarmfulWordsDocument> harmfulWordDocumentList = HarmfulWordsDocument.CreateFromIHit(hitHarmfulWordDocumentList);

		return harmfulWordDocumentList.Select(h => h.Word).ToList();
	}

	public async Task Create(string word) {

		int id = 1;

		var hitHarmfulWordDocumentList = await elasticsearchRepository.SearchDocumentsAsync(HarmfulWordsQueries.Get(word));
		if (hitHarmfulWordDocumentList.Count() > 0)
			return;

		hitHarmfulWordDocumentList = await elasticsearchRepository.SearchLastDocumentAsync<HarmfulWordsDocument>();
		if (hitHarmfulWordDocumentList.Count() > 0)
			id = HarmfulWordsDocument.CreateFromIHit(hitHarmfulWordDocumentList.First()).Id + 1;

		await elasticsearchRepository.IndexDocumentAsync(new HarmfulWordsDocument { Id = id, Word = word, CreatedDate = DateTime.UtcNow });
	}

	public async Task Update(string oldWord, string newWord) {

		await Delete(oldWord);
		await Create(newWord);
	}

	public async Task Delete(string word) {

		await elasticsearchRepository.DeleteDocumentAsync(HarmfulWordsQueries.Get(word));
	}

	public async Task<bool> Contains(string description) {

		var hitHarmfulWordDocumentList = await elasticsearchRepository.SearchDocumentsAsync(HarmfulWordsQueries.Contains(description));

		return hitHarmfulWordDocumentList.Count() > 0;
	}
}
