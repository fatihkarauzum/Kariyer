using Kariyer.Model.Documents;
using Nest;

namespace Kariyer.Data.Repositories.Elasticsearch.Queries;

public static class HarmfulWordsQueries {

	public static Func<QueryContainerDescriptor<HarmfulWordsDocument>, QueryContainer> Get(string word = "") {

		QueryContainerDescriptor<HarmfulWordsDocument> queryContainerDescriptor = new QueryContainerDescriptor<HarmfulWordsDocument>();

		if (!string.IsNullOrWhiteSpace(word)) {

			queryContainerDescriptor
				.Match(t => t
					.Field(f => f.Word)
					.Query(word)
					);
		}

		return new Func<QueryContainerDescriptor<HarmfulWordsDocument>, QueryContainer>(q => queryContainerDescriptor);
	}

	public static Func<QueryContainerDescriptor<HarmfulWordsDocument>, QueryContainer> Contains(string description) {

		QueryContainerDescriptor<HarmfulWordsDocument> queryContainerDescriptor = new QueryContainerDescriptor<HarmfulWordsDocument>();

		queryContainerDescriptor
			.Match(t => t
				.Field(f => f.Word)
				.Query(description)
				);

		return new Func<QueryContainerDescriptor<HarmfulWordsDocument>, QueryContainer>(q => queryContainerDescriptor);
	}
}
