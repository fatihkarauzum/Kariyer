using Kariyer.Business.Dtos.IndustryDtos;
using Kariyer.Data.Repositories.Builders;
using Kariyer.Data.Repositories;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Kariyer.Common.Exceptions;
using Nest;

namespace Kariyer.Business.Services.Impl;

public class IndustryServiceImpl : IndustryService {

	public UnitOfWork unitOfWork;

	public IndustryServiceImpl(UnitOfWork unitOfWork) {
		this.unitOfWork = unitOfWork;
	}

	public async Task<GetIndustryItem> Get(int id) {

		Industry? industry = await unitOfWork.industry.GetByIdAsync(id);

		if (industry == null)
			throw IndustryExceptions.IndustryNotFound($"Industry Not Found (Id: {id})");

		return GetIndustryItem.CreateFromIndustry(industry);
	}

	public async Task<GetIndustryList> List(int pageNumber, int pageSize) {

		QueryBuilder<Industry> queryBuilder =
			new QueryBuilder<Industry>()
			.WithPageNumber(pageNumber)
			.WithPageSize(pageSize);

		BasePagedResult<Industry> pagedResult = await unitOfWork.industry.FindAsync(queryBuilder);

		return GetIndustryList.CreateFromBasePagedResult(pagedResult);
	}

	public async Task<GetIndustryItem> Create(PostIndustryItem postIndustry) {

		Industry industry = await unitOfWork.industry.Create(PostIndustryItem.CreateFromIndustryItem(postIndustry));

		await unitOfWork.CommitAsync();

		return GetIndustryItem.CreateFromIndustry(industry);
	}

	public async Task Update(PostIndustryItem postIndustry) {

		Industry? industry = await unitOfWork.industry.GetByIdAsync(postIndustry.Id);

		if (industry == null)
			throw IndustryExceptions.IndustryNotFound($"Industry Not Found (Id: {postIndustry.Id})");

		unitOfWork.industry.Update(PostIndustryItem.CreateFromIndustryItem(postIndustry));

		await unitOfWork.CommitAsync();
	}

	public async Task Delete(int id) {

		Industry? industry = await unitOfWork.industry.GetByIdAsync(id);

		if (industry == null)
			throw IndustryExceptions.IndustryNotFound($"Industry Not Found (Id: {id})");

		unitOfWork.industry.Delete(industry);
		await unitOfWork.CommitAsync();
	}

	public async Task Passive(int id) {

		Industry? industry = await unitOfWork.industry.GetByIdAsync(id);

		if (industry == null)
			throw IndustryExceptions.IndustryNotFound($"Industry Not Found (Id: {id})");

		await unitOfWork.industry.Passive(id);
		await unitOfWork.CommitAsync();
	}
}
