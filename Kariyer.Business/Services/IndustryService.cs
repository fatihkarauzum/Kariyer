using Kariyer.Business.Dtos.IndustryDtos;

namespace Kariyer.Business.Services;

public interface IndustryService {

	Task<GetIndustryItem> Get(int id);
	Task<GetIndustryList> List(int pageNumber, int pageSize);
	Task<GetIndustryItem> Create(PostIndustryItem postIndustryItem);
	Task Update(PostIndustryItem postIndustryItem);
	Task Delete(int id);
	Task Passive(int id);
}
