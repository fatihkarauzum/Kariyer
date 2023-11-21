namespace Kariyer.Model.Entities;

public class Department : BaseEntity {

	public required string Name { get; set; }
	public required string Description { get; set; }
	public required string Code { get; set; }

	public virtual List<Job> Jobs { get; set; } = new List<Job>();
}
