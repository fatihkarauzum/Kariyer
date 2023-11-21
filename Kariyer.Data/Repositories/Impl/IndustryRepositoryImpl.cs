using Kariyer.Data.Contexts;
using Kariyer.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kariyer.Data.Repositories.Impl;

public class IndustryRepositoryImpl : BaseRepositoryImpl<Industry>, IndustryRepository {

	private readonly PostgreContext postgreContext;

	public IndustryRepositoryImpl(PostgreContext postgreContext) : base(postgreContext) {

		this.postgreContext = postgreContext;
	}

	public override async Task<Industry> Create(Industry entity) {

		Industry? industry = await postgreContext.Set<Industry>().FirstOrDefaultAsync(d => d.Code == entity.Code);

		if (industry != null)
			return industry;

		return (await postgreContext.Set<Industry>().AddAsync(entity)).Entity;
	}

}