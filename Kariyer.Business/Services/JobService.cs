using Kariyer.Business.Dtos.JobDtos;

namespace Kariyer.Business.Services;

public interface JobService {

	Task<GetJobItem> Get(int id);
	Task<GetJobList> List(SearchJob searchJob);
	Task<GetJobItem> Create(PostJobItem postJobItem);
	Task Update(PostJobItem postJobItem);
	Task Delete(int id);
	Task Passive(int id);
}
