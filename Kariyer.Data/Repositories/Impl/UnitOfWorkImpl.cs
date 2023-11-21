
using Kariyer.Data.Contexts;

namespace Kariyer.Data.Repositories.Impl;

public class UnitOfWorkImpl : UnitOfWork {

    public readonly PostgreContext postgreContext;
	private bool disposed;

	public UnitOfWorkImpl(
        PostgreContext context,
        CompanyRepository company,
		DepartmentRepository department,
		IndustryRepository industry,
		JobRepository job) 
    {
        this.postgreContext = context;
        this.company = company;
		this.department = department;
		this.industry = industry;
		this.job = job;

    }

	public CompanyRepository company { get; }

	public DepartmentRepository department { get; }

	public IndustryRepository industry { get; }

	public JobRepository job { get; }

	Task<int> UnitOfWork.CommitAsync() {

		return postgreContext.SaveChangesAsync();
	}

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~UnitOfWorkImpl() {
		Dispose(false);
	}

	protected virtual void Dispose(bool disposing) {
		if (!disposed)
			if (disposing)
				postgreContext.Dispose();

		disposed = true;
	}
}
