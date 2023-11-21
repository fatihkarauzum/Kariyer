namespace Kariyer.Model.Entities;

public class Company : BaseEntity {

	public required string Name { get; set; }
	public required string Address { get; set; }
	public required long Phone { get; set; }
	public int RemainingPublishRight { get; set; } = 2;
	public string? Description { get; set; }
	public string? WebAddress { get; set; }
	public int? EstablishmentYear { get; set; }

	public virtual List<Industry> Industries { get; set; } = new List<Industry>();
}
