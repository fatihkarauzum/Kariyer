using Nest;

namespace Kariyer.Model.Documents;

public class HarmfulWordsDocument : BaseDocument {

    public required string Word { get; set; }

    public static HarmfulWordsDocument CreateFromIHit(IHit<HarmfulWordsDocument> jobHit) {

		return jobHit.Source;
	}

	public static List<HarmfulWordsDocument> CreateFromIHit(IEnumerable<IHit<HarmfulWordsDocument>> jobHits) {

		return jobHits.Select(CreateFromIHit).ToList();
	}
}
