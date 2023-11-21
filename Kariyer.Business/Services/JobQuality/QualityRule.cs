using Kariyer.Model.Entities;

namespace Kariyer.Business.Services.JobQuality;

public interface QualityRule {

	double CalculateQualityScore(Job job);
}
