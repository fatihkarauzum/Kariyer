using Kariyer.Data.Contexts;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kariyer.Data.Repositories.Builders;

public class QueryBuilder<T> where T : BaseEntity {

	private Expression<Func<T, bool>>? expression;
	private Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy;
	private int pageNumber = 1;
	private int pageSize = 10;
	private bool paging = true;

	public QueryBuilder<T> WithExpression(Expression<Func<T, bool>> expression) {

		this.expression = expression;
		return this;
	}

	public QueryBuilder<T> WithOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy) {

		this.orderBy = orderBy;
		return this;
	}

	public QueryBuilder<T> WithPageNumber(int pageNumber) {

		this.pageNumber = pageNumber;
		return this;
	}

	public QueryBuilder<T> WithPageSize(int pageSize) {

		this.pageSize = pageSize;
		return this;
	}

	public QueryBuilder<T> WithPaging(bool paging = true) {

		this.paging = paging;
		return this;
	}

	public async Task<BasePagedResult<T>> ExecuteAsync(PostgreContext postgreContext) {

		IQueryable<T>? query = postgreContext.Set<T>();

		if (expression != null)
			query = query.Where(expression);

		if (orderBy != null)
			query = orderBy(query);

		int totalItems = await query.CountAsync();

        if (paging)
			query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

		List<T>? items = await query.ToListAsync();

		return new BasePagedResult<T> {
			Items = items,
			TotalItems = totalItems,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
}
