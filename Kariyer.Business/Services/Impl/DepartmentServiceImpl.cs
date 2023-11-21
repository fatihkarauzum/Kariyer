using Kariyer.Business.Dtos.DepartmentDtos;
using Kariyer.Data.Repositories.Builders;
using Kariyer.Data.Repositories;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Kariyer.Core.Attributes;
using Kariyer.Common.Exceptions;
using Nest;

namespace Kariyer.Business.Services.Impl;

public class DepartmentServiceImpl : DepartmentService {

	public UnitOfWork unitOfWork;

	public DepartmentServiceImpl(UnitOfWork unitOfWork) {
		this.unitOfWork = unitOfWork;
	}

	public async Task<GetDepartmentItem> Get(int id) {

		Department? department = await unitOfWork.department.GetByIdAsync(id);

		if (department == null)
			throw DepartmentExceptions.DepartmentNotFound($"Company Not Found (Id: {id})");

		return GetDepartmentItem.CreateFromDepartment(department);
	}

	public async Task<GetDepartmentList> List(int pageNumber, int pageSize) {

		QueryBuilder<Department> queryBuilder =
			new QueryBuilder<Department>()
			.WithPageNumber(pageNumber)
			.WithPageSize(pageSize);

		BasePagedResult<Department> pagedResult = await unitOfWork.department.FindAsync(queryBuilder);

		return GetDepartmentList.CreateFromBasePagedResult(pagedResult);
	}

	[Cacheable(Key = "ALL_DEPARTMENT")]
	public async Task<GetDepartmentList> List() {

		QueryBuilder<Department> queryBuilder =
			new QueryBuilder<Department>()
				.WithPaging(false);

		BasePagedResult<Department> pagedResult = await unitOfWork.department.FindAsync(queryBuilder);

		return GetDepartmentList.CreateFromBasePagedResult(pagedResult);
	}

	[CacheEvict(Key = "ALL_DEPARTMENT")]
	public async Task<GetDepartmentItem> Create(PostDepartmentItem postDepartment) {

		Department department = await unitOfWork.department.Create(PostDepartmentItem.CreateFromDepartmentItem(postDepartment));

		await unitOfWork.CommitAsync();

		return GetDepartmentItem.CreateFromDepartment(department);
	}

	[CacheEvict(Key = "ALL_DEPARTMENT")]
	public async Task Update(PostDepartmentItem postDepartment) {

		Department? department = await unitOfWork.department.GetByIdAsync(postDepartment.Id);

		if (department == null)
			throw DepartmentExceptions.DepartmentNotFound($"Department Not Found (Id: {postDepartment.Id})");

		unitOfWork.department.Update(PostDepartmentItem.CreateFromDepartmentItem(postDepartment));

		await unitOfWork.CommitAsync();
	}

	[CacheEvict(Key = "ALL_DEPARTMENT")]
	public async Task Delete(int id) {

		Department? department = await unitOfWork.department.GetByIdAsync(id);

		if (department == null)
			throw DepartmentExceptions.DepartmentNotFound($"Department Not Found (Id: {id})");

		unitOfWork.department.Delete(department);
		await unitOfWork.CommitAsync();
	}

	[CacheEvict(Key = "ALL_DEPARTMENT")]
	public async Task Passive(int id) {

		Department? department = await unitOfWork.department.GetByIdAsync(id);

		if (department == null)
			throw DepartmentExceptions.DepartmentNotFound($"Department Not Found (Id: {id})");

		await unitOfWork.department.Passive(id);
		await unitOfWork.CommitAsync();
	}
}
