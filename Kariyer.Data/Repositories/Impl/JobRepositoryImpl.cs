using Kariyer.Data.Contexts;
using Kariyer.Model.Entities;

namespace Kariyer.Data.Repositories.Impl;

public class JobRepositoryImpl : BaseRepositoryImpl<Job>, JobRepository {

	public JobRepositoryImpl(PostgreContext postgreContext) : base(postgreContext) {
	}

}