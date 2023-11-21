using Kariyer.Model.Entities;

namespace Kariyer.Data.Repositories;

public interface CompanyRepository : BaseRepository<Company> {

	Task<Company?> GetByPhone(long phone);

	Task DecRemainingPublishRight(int id);
}