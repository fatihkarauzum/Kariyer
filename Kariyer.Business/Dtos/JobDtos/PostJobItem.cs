using Kariyer.Common.Utils;
using Kariyer.Model.Entities;
using Kariyer.Model.Enums;

namespace Kariyer.Business.Dtos.JobDtos;

public class PostJobItem {

    public int Id { get; set; }
    public required string Position { get; set; }
	public required string Description { get; set; }
	public double Quality { get; set; }
	public WorkingType WorkingType { get; set; }
	public WorkingMode WorkingMode { get; set; }
	public EducationLevel EducationLevel { get; set; }
	public ExperienceType ExperienceType { get; set; }
	public int MinExperience { get; set; }
	public int MaxExperience { get; set; }
	public int Salary { get; set; }
	public string? Benefits { get; set; }
	public required int CompanyId { get; set; }
	public required int DepartmentId { get; set; }

	public static Job CreateFromJobItem(PostJobItem createJobItem) {

		return new Job {
			Id = createJobItem.Id,
			Position = createJobItem.Position,
			Description = createJobItem.Description,	
			Quality = createJobItem.Quality,
			WorkingType = createJobItem.WorkingType,
			WorkingMode = createJobItem.WorkingMode,
			EducationLevel = createJobItem.EducationLevel,
			ExperienceType = createJobItem.ExperienceType,
			MinExperience = createJobItem.MinExperience,
			MaxExperience = createJobItem.MaxExperience,
			Salary = createJobItem.Salary,
			Benefits = createJobItem.Benefits,
			CompanyId = createJobItem.CompanyId,
			DepartmentId = createJobItem.DepartmentId,
			UpdatedDate = createJobItem.Id > 0 ? DateTime.UtcNow : null,
		};
	}
}
