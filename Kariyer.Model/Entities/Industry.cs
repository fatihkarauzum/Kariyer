namespace Kariyer.Model.Entities;

public class Industry : BaseEntity {

	public required string Name { get; set; }
	public required string Description { get; set; }
	public required string Code { get; set; }

	public virtual List<Company> Companies { get; set; } = new List<Company>();
}