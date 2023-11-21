using Kariyer.Business.Dtos.CompanyDtos;
using Kariyer.Common.Utils;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.DepartmentDtos;

public class GetDepartmentItem : BaseDto<GetDepartmentItem> {

	public string Name { get; set; }
	public string Description { get; set; }
    public string Code { get; set; }

    public static GetDepartmentItem CreateFromDepartment(Department department) {

		return new GetDepartmentItem {
			Id = department.Id,
			Name = department.Name,
			Description = department.Description,
			Code = department.Code,
			IsActive = department.IsActive,
			CreatedDate = department.CreatedDate.SetKindLocal(),
			UpdatedDate = department.UpdatedDate.SetKindLocal(),
		};
	}

	public static List<GetDepartmentItem> CreateFromDepartment(List<Department> departments) {

		return departments.Select(CreateFromDepartment).ToList();
	}
}
