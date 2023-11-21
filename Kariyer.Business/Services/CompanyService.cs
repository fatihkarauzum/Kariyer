using Kariyer.Business.Dtos.CompanyDtos;

namespace Kariyer.Business.Services;
public interface CompanyService {

	Task<GetCompanyItem> Get(int id);
	Task<GetCompanyList> List(int pageNumber, int pageSize);
	Task<GetCompanyItem> Create(PostCompanyItem postCompanyItem);
	Task Update(PostCompanyItem postCompanyItem);
	Task Delete(int id);
	Task Passive(int id);
}
