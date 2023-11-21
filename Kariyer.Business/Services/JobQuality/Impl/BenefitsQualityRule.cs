using Kariyer.Model.Entities;

namespace Kariyer.Business.Services.JobQuality.Impl;

public class BenefitsQualityRule : QualityRule {

	private readonly QualityRule? nextRule;

	public BenefitsQualityRule(QualityRule? nextRule = null) {

		this.nextRule = nextRule;
	}

	public double CalculateQualityScore(Job job) {

		if (!string.IsNullOrEmpty(job.Benefits))
			return 1 + (nextRule?.CalculateQualityScore(job) ?? 0);

		return nextRule?.CalculateQualityScore(job) ?? 0;
	}
}
