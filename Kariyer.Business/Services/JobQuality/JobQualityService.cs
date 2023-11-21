using Kariyer.Model.Entities;

namespace Kariyer.Business.Services.JobQuality;

public interface JobQualityService {

	double CalculateJobQualityScore(Job job);
}
