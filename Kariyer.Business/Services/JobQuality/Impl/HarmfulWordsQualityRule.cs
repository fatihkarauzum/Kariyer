using Kariyer.Model.Entities;

namespace Kariyer.Business.Services.JobQuality.Impl;

public class HarmfulWordsQualityRule : QualityRule {

	private readonly HarmfulWordsService harmfulWordsService;

	public HarmfulWordsQualityRule(HarmfulWordsService harmfulWordsService) {

		this.harmfulWordsService = harmfulWordsService;
    }

	public double CalculateQualityScore(Job job) {

		if (!ContainsHarmfulWord(job.Description))
			return 2;

		return 0;
	}

	private bool ContainsHarmfulWord(string description) {

		bool containsHarmfulWords = false;

		Task task = Task.Run(async () => containsHarmfulWords = await harmfulWordsService.Contains(description));
		task.Wait();

		return containsHarmfulWords;
	}
}
