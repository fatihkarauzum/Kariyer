using Kariyer.Common.Utils;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.IndustryDtos;
public class GetIndustryItem : BaseDto<GetIndustryItem> {

	public string Name { get; set; }
	public string Description { get; set; }
    public string Code { get; set; }

    public static GetIndustryItem CreateFromIndustry(Industry industry) {

		return new GetIndustryItem {
			Id = industry.Id,
			Name = industry.Name,
			Description = industry.Description,
			Code = industry.Code,
			IsActive = industry.IsActive,
			CreatedDate = industry.CreatedDate.SetKindLocal(),
			UpdatedDate = industry.UpdatedDate.SetKindLocal(),
		};
	}

	public static List<GetIndustryItem> CreateFromIndustry(List<Industry> ındustries) {

		return ındustries.Select(CreateFromIndustry).ToList();
	}
}
