using Kariyer.Data.Repositories.Builders;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;

namespace Kariyer.Data.Repositories;

public interface BaseRepository<T> where T : BaseEntity {

	Task<T?> GetByIdAsync(int id);

	Task<BasePagedResult<T>> FindAsync(QueryBuilder<T> queryBuilder);

	Task<T> Create(T entity);

	Task Create(IEnumerable<T> entities);

	void Update(T entity);

	void Update(IEnumerable<T> entities);

	void Delete(T entity);

	void DeleteRange(IEnumerable<T> entities);

	Task Passive(int id);
}
