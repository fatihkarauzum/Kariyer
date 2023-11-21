using Kariyer.Data.Contexts;
using Kariyer.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kariyer.Data.Repositories.Impl;

public class CompanyRepositoryImpl : BaseRepositoryImpl<Company>, CompanyRepository {

	private readonly PostgreContext postgreContext;

	public CompanyRepositoryImpl(PostgreContext postgreContext) : base(postgreContext) {

		this.postgreContext = postgreContext;
	}

	public async Task<Company?> GetByPhone(long phone) {

		return await postgreContext.Set<Company>().FirstOrDefaultAsync(c => c.Phone == phone);
	}

	public async Task DecRemainingPublishRight(int id) {

		Company? company = await GetByIdAsync(id);

		if (company == null)
			return;

		company.RemainingPublishRight--;
		postgreContext.Entry(company).Property(e => e.RemainingPublishRight).IsModified = true;
	}
}
