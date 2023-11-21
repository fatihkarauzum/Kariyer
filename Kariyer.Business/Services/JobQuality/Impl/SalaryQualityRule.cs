using Kariyer.Model.Entities;

namespace Kariyer.Business.Services.JobQuality.Impl;

public class SalaryQualityRule : QualityRule {

	private readonly QualityRule? nextRule;

	public SalaryQualityRule(QualityRule? nextRule = null) {

		this.nextRule = nextRule;
	}

	public double CalculateQualityScore(Job job) {

		if (job.Salary > 0)
			return 1 + (nextRule?.CalculateQualityScore(job) ?? 0);

		return nextRule?.CalculateQualityScore(job) ?? 0;
	}
}
