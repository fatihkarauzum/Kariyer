using Kariyer.Business.Dtos.CompanyDtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.DepartmentDtos;

public class PostDepartmentItem {

	public int Id { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
    public required string Code { get; set; }

    public static Department CreateFromDepartmentItem(PostDepartmentItem createDepartmentItem) {

		return new Department {
			Id = createDepartmentItem.Id,
			Name = createDepartmentItem.Name,
			Description = createDepartmentItem.Description,
			Code = createDepartmentItem.Code,
			UpdatedDate = createDepartmentItem.Id > 0 ? DateTime.UtcNow : null,
		};
	}
}
