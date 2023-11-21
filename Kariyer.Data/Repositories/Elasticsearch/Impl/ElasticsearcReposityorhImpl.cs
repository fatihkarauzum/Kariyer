using Kariyer.Data.Configs;
using Kariyer.Model.Documents;
using Kariyer.Model.Dtos;
using Nest;
using System.Xml;

namespace Kariyer.Data.Repositories.Elasticsearch.Impl;

public class ElasticsearcReposityorhImpl : ElasticsearchRepository {

	private readonly IElasticClient client;

	private static int numberOfShard = 3;
	private static int numberOfReplicas = 1;

	public ElasticsearcReposityorhImpl(ElasticsearchClient elasticsearchClient) {

		client = elasticsearchClient.Client;
	}

	public async Task CreateIndexAsync<T>() where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		ExistsResponse existsResponse = await client.Indices.ExistsAsync(indexName);
		if (existsResponse.Exists)
			return;

		var createIndexResponse = await client.Indices.CreateAsync(indexName, c => c
			.Map<T>(m => m
				.AutoMap()
			)
			.Settings(s => s.NumberOfShards(numberOfShard).NumberOfReplicas(numberOfReplicas))
		);

		if (!createIndexResponse.IsValid)
			throw new Exception($"Index oluşturma hatası: {createIndexResponse.DebugInformation}");
	}

	public async Task IndexDocumentAsync<T>(T document) where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		var indexResponse = await client.IndexAsync(document, idx => idx.Index(indexName));

		if (!indexResponse.IsValid)
			throw new Exception($"Belge ekleme hatası: {indexResponse.DebugInformation}");
	}

	public async Task BulkIndexDocumentAsync<T>(IEnumerable<T> documents) where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		await client.IndexManyAsync(documents, indexName);
	}

	public async Task DeleteDocumentAsync<T>(int id) where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		var deleteResponse = await client.DeleteAsync<T>(id, idx => idx.Index(indexName));

		if (!deleteResponse.IsValid)
			throw new Exception($"Belge silme hatası: {deleteResponse.DebugInformation}");
	}

	public async Task DeleteDocumentAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryBuilder) where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		var deleteResponse = await client.DeleteByQueryAsync<T>(s => s
			.Index(indexName)
			.Query(q => queryBuilder(q))
		);

		if (!deleteResponse.IsValid)
			throw new Exception($"Belge silme hatası: {deleteResponse.DebugInformation}");
	}

	public async Task<IEnumerable<IHit<T>>> SearchDocumentsAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryBuilder) where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		var searchResponse = await client.SearchAsync<T>(s => s
			.Index(indexName)
			.Query(q => queryBuilder(q))
		);

		if (!searchResponse.IsValid) 
			throw new Exception($"Belge sorgulama hatası: {searchResponse.DebugInformation}");

		return searchResponse.Hits;
	}

	public async Task<BasePagedResult<IHit<T>>> SearchDocumentsAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryBuilder, int? pageNumber, int? pageSize) where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		SearchDescriptor<T> searchDescriptor = new SearchDescriptor<T>();
		searchDescriptor.Index(indexName);

		if (pageSize.HasValue && pageNumber.HasValue)
			searchDescriptor.From(pageNumber).Size(pageSize);

		searchDescriptor.Query(q => queryBuilder(q));

		var searchResponse = await client.SearchAsync<T>(searchDescriptor);

		if (!searchResponse.IsValid)
			throw new Exception($"Belge sorgulama hatası: {searchResponse.DebugInformation}");

		var pagedResult = new BasePagedResult<IHit<T>> {

			Items = searchResponse.Hits,
			TotalItems = searchResponse.Total,
			PageNumber = pageNumber ?? 0,
			PageSize = pageSize ?? 0,
		};

		return pagedResult;
	}

	public async Task<IEnumerable<IHit<T>>> SearchLastDocumentAsync<T>() where T : BaseDocument {

		string indexName = typeof(T).Name.ToLowerInvariant();

		var searchResponse = await client.SearchAsync<T>(s => s
					.Index(indexName)
					.Take(1)
					.Sort(sort => sort
						.Descending(field => field.Id)
					)
				);

		if (!searchResponse.IsValid)
			throw new Exception($"Belge sorgulama hatası: {searchResponse.DebugInformation}");

		return searchResponse.Hits;
	}
}
