namespace Kariyer.Data.Repositories;

public interface UnitOfWork {

	CompanyRepository company { get; }

	DepartmentRepository department { get; }

	IndustryRepository industry { get; }

	JobRepository job { get; }

	Task<int> CommitAsync();
}
