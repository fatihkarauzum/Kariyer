using Kariyer.Business.Dtos.DepartmentDtos;

namespace Kariyer.Business.Services;

public interface DepartmentService {

	Task<GetDepartmentItem> Get(int id);
	Task<GetDepartmentList> List(int pageNumber, int pageSize);
	Task<GetDepartmentList> List();
	Task<GetDepartmentItem> Create(PostDepartmentItem postDepartmentItem);
	Task Update(PostDepartmentItem postDepartmentItem);
	Task Delete(int id);
	Task Passive(int id);
}
