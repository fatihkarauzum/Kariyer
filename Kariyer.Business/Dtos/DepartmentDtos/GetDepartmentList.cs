using Kariyer.Business.Dtos.CompanyDtos;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.DepartmentDtos;

public class GetDepartmentList : BasePagedResult<GetDepartmentItem> {

	public static GetDepartmentList CreateFromBasePagedResult(BasePagedResult<Department> basePagedResult) {

		return new GetDepartmentList {
			Items = GetDepartmentItem.CreateFromDepartment(basePagedResult.Items.ToList()),
			TotalItems = basePagedResult.TotalItems,
			PageNumber = basePagedResult.PageNumber,
			PageSize = basePagedResult.PageSize,
		};
	}
}
