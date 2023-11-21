using Kariyer.Business.Dtos.CompanyDtos;
using Kariyer.Common.Exceptions;
using Kariyer.Data.Repositories;
using Kariyer.Data.Repositories.Builders;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Nest;

namespace Kariyer.Business.Services.Impl;
public class CompanyServiceImpl : CompanyService {

    public UnitOfWork unitOfWork;

    public CompanyServiceImpl(UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

	public async Task<GetCompanyItem> Get(int id) {

		Company? company = await unitOfWork.company.GetByIdAsync(id);

		if (company == null)
			throw CompanyExceptions.CompanyNotFound($"Company Not Found (Id: {id})");

		return GetCompanyItem.CreateFromCompany(company);
	}

	public async Task<GetCompanyList> List(int pageNumber, int pageSize) {

		QueryBuilder<Company> queryBuilder = 
			new QueryBuilder<Company>()
			.WithPageNumber(pageNumber)
			.WithPageSize(pageSize);

		BasePagedResult<Company> pagedResult = await unitOfWork.company.FindAsync(queryBuilder);

		return GetCompanyList.CreateFromBasePagedResult(pagedResult);
	}

	public async Task<GetCompanyItem> Create(PostCompanyItem postCompany) {

		Company? company = await unitOfWork.company.GetByPhone(postCompany.Phone);

		if (company != null)
			throw CompanyExceptions.DuplicatePhoneNumber($"Duplicate Phone Number (PhoneNumber: {postCompany.Phone})");

		QueryBuilder<Industry> queryBuilder =
			new QueryBuilder<Industry>()
			.WithExpression(industry => postCompany.IndustryIds.Contains(industry.Id));

		var industries = await unitOfWork.industry.FindAsync(queryBuilder);

		company = PostCompanyItem.CreateFromCompanyItem(postCompany);
		company.Industries.AddRange(industries.Items);

		company = await unitOfWork.company.Create(company);

		await unitOfWork.CommitAsync();

		return GetCompanyItem.CreateFromCompany(company);
	}

	public async Task Update(PostCompanyItem postCompany) {

		Company? company = await unitOfWork.company.GetByIdAsync(postCompany.Id);

		if (company == null)
			throw CompanyExceptions.CompanyNotFound($"Company Not Found (Id: {postCompany.Id})");

		unitOfWork.company.Update(PostCompanyItem.CreateFromCompanyItem(postCompany));

		await unitOfWork.CommitAsync();
	}

	public async Task Delete(int id) {

		Company? company = await unitOfWork.company.GetByIdAsync(id);

		if (company == null)
			throw CompanyExceptions.CompanyNotFound($"Company Not Found (Id: {id})");

		unitOfWork.company.Delete(company);
		await unitOfWork.CommitAsync();
	}

	public async Task Passive(int id) {

		await unitOfWork.company.Passive(id);
		await unitOfWork.CommitAsync();
	}
}
