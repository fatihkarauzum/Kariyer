using Kariyer.Model.Entities;

namespace Kariyer.Business.Services.JobQuality.Impl;

public class JobQualityServiceImpl : JobQualityService {

	private readonly HarmfulWordsService harmfulWordsService;

	public JobQualityServiceImpl(HarmfulWordsService harmfulWordsService) {

		this.harmfulWordsService = harmfulWordsService;
	}

	public double CalculateJobQualityScore(Job job) {

		HarmfulWordsQualityRule harmfulWordsQualityRule = new HarmfulWordsQualityRule(harmfulWordsService);

		BenefitsQualityRule benefitsQualityRule = new BenefitsQualityRule(harmfulWordsQualityRule);

		SalaryQualityRule salaryQualityRule = new SalaryQualityRule(benefitsQualityRule);

		WorkingTypeQualityRule workingTypeQualityRule = new WorkingTypeQualityRule(salaryQualityRule);

		return workingTypeQualityRule.CalculateQualityScore(job);
	}
}
