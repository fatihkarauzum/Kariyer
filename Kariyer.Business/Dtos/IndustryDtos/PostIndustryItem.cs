using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.IndustryDtos;

public class PostIndustryItem {

	public int Id { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required string Code { get; set; }

	public static Industry CreateFromIndustryItem(PostIndustryItem createIndustryItem) {

		return new Industry {
			Id = createIndustryItem.Id,
			Name = createIndustryItem.Name,
			Description = createIndustryItem.Description,
			Code = createIndustryItem.Code,
			UpdatedDate = createIndustryItem.Id > 0 ? DateTime.Now : null,
		};
	}
}
