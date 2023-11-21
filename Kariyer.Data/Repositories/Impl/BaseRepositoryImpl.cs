using Kariyer.Data.Contexts;
using Kariyer.Data.Repositories.Builders;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kariyer.Data.Repositories.Impl;

public abstract class BaseRepositoryImpl<T> : BaseRepository<T> where T : BaseEntity {

	private readonly PostgreContext postgreContext;

    public BaseRepositoryImpl(PostgreContext postgreContext)
    {
        this.postgreContext = postgreContext;
    }

    public async Task<T?> GetByIdAsync(int id) {

		return await postgreContext.Set<T>().FindAsync(id);
	}

	public async Task<BasePagedResult<T>> FindAsync(QueryBuilder<T> queryBuilder) {

		return await queryBuilder.ExecuteAsync(postgreContext);
	}

	public virtual async Task<T> Create(T entity) {

		return (await postgreContext.Set<T>().AddAsync(entity)).Entity;
	}

	public async Task Create(IEnumerable<T> entities) {

		await postgreContext.Set<T>().AddRangeAsync(entities);
	}

	public void Update(T entity) => postgreContext.Entry(entity).State = EntityState.Modified;

	public void Update(IEnumerable<T> entities) => postgreContext.UpdateRange(entities);

	public void Delete(T entity) => postgreContext.Remove(entity);

	public void DeleteRange(IEnumerable<T> entities) => postgreContext.RemoveRange(entities);

	public async Task Passive(int id) {

		T? entity = await GetByIdAsync(id);

		if (entity != null) {
			entity.IsActive = false;
			postgreContext.Entry(entity).Property(e => e.IsActive).IsModified = true;
		}
	}
}
