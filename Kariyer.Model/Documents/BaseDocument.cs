namespace Kariyer.Model.Documents;

public abstract class BaseDocument {

	public int Id { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
}
