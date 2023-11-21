namespace Kariyer.Model.Dtos;

public class BasePagedResult<T> {

	public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
	public long TotalItems { get; set; } = 0;
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public int TotalPages => (int) Math.Ceiling(TotalItems / (double) PageSize);
}
