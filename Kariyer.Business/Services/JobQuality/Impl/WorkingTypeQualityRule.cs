using Kariyer.Model.Entities;
using Kariyer.Model.Enums;

namespace Kariyer.Business.Services.JobQuality.Impl;

public class WorkingTypeQualityRule : QualityRule {

	private readonly QualityRule? nextRule;

	public WorkingTypeQualityRule(QualityRule? nextRule = null) {

		this.nextRule = nextRule;
	}

	public double CalculateQualityScore(Job job) {

		if (job.WorkingType != WorkingType.NONE)
			return 1 + (nextRule?.CalculateQualityScore(job) ?? 0);

		return nextRule?.CalculateQualityScore(job) ?? 0;
	}
}
