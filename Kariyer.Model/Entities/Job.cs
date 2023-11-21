using Kariyer.Model.Enums;

namespace Kariyer.Model.Entities;

public class Job : BaseEntity {

	public required string Position { get; set; }
	public required string Description { get; set; }
	public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(15);
	public required double Quality { get; set; }
	public WorkingType WorkingType { get; set; }
	public WorkingMode WorkingMode { get; set; }
	public EducationLevel EducationLevel { get; set; }
	public ExperienceType ExperienceType { get; set; }
	public int MinExperience { get; set; }
	public int MaxExperience { get; set; }
	public int Salary { get; set; }
	public string? Benefits { get; set; }

	public int CompanyId { get; set; }
	public int DepartmentId { get; set; }
	public Company Company { get; set; }
	public Department Department { get; set; }
}