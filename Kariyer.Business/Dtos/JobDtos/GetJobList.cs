using Kariyer.Business.Dtos.IndustryDtos;
using Kariyer.Model.Documents;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Nest;

namespace Kariyer.Business.Dtos.JobDtos;

public class GetJobList : BasePagedResult<JobDocument> {

	public static GetJobList CreateFromBasePagedResult(BasePagedResult<IHit<JobDocument>> basePagedResult) {

		return new GetJobList {
			Items = JobDocument.CreateFromIHit(basePagedResult.Items.ToList()),
			TotalItems = basePagedResult.TotalItems,
			PageNumber = basePagedResult.PageNumber,
			PageSize = basePagedResult.PageSize,
		};
	}
}
