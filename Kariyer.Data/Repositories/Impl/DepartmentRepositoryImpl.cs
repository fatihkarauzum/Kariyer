using Kariyer.Data.Contexts;
using Kariyer.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kariyer.Data.Repositories.Impl;

public class DepartmentRepositoryImpl : BaseRepositoryImpl<Department>, DepartmentRepository {

	private readonly PostgreContext postgreContext;

	public DepartmentRepositoryImpl(PostgreContext postgreContext) : base(postgreContext) {

		this.postgreContext = postgreContext;
	}

	public override async Task<Department> Create(Department entity) {

		Department? department = await postgreContext.Set<Department>().FirstOrDefaultAsync(d => d.Code == entity.Code);

		if (department != null)
			return department;

		return (await postgreContext.Set<Department>().AddAsync(entity)).Entity;
	}

}
