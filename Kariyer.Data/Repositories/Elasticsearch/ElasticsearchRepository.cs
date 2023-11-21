using Kariyer.Model.Documents;
using Kariyer.Model.Dtos;
using Nest;

namespace Kariyer.Data.Repositories.Elasticsearch;

public interface ElasticsearchRepository {

	Task CreateIndexAsync<T>() where T : BaseDocument;

	Task IndexDocumentAsync<T>(T document) where T : BaseDocument;

	Task BulkIndexDocumentAsync<T>(IEnumerable<T> documents) where T : BaseDocument;

	Task DeleteDocumentAsync<T>(int id) where T : BaseDocument;

	Task DeleteDocumentAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryBuilder) where T : BaseDocument;

    Task<IEnumerable<IHit<T>>> SearchDocumentsAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryBuilder) where T : BaseDocument;

	Task<BasePagedResult<IHit<T>>> SearchDocumentsAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryBuilder, int? pageNumber, int? pageSize) where T : BaseDocument;

	Task<IEnumerable<IHit<T>>> SearchLastDocumentAsync<T>() where T : BaseDocument;
}
