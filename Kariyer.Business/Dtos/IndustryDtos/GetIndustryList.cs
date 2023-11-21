using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Business.Dtos.IndustryDtos;
public class GetIndustryList : BasePagedResult<GetIndustryItem> {

	public static GetIndustryList CreateFromBasePagedResult(BasePagedResult<Industry> basePagedResult) {

		return new GetIndustryList {
			Items = GetIndustryItem.CreateFromIndustry(basePagedResult.Items.ToList()),
			TotalItems = basePagedResult.TotalItems,
			PageNumber = basePagedResult.PageNumber,
			PageSize = basePagedResult.PageSize,
		};
	}
}
