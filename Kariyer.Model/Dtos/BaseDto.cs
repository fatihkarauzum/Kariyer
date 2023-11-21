namespace Kariyer.Model.Dtos;

public class BaseDto<T>
	where T : class, new() {

	public static T EMPTY = new T();
	public bool isEmpty() {
		return EMPTY.Equals(this);
	}

	public bool isNotEmpty() {
		return !EMPTY.Equals(this);
	}

	public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
}
