using Kariyer.Model.Entities;
using Kariyer.Model.Enums;
using Nest;

namespace Kariyer.Model.Documents;

public class JobDocument : BaseDocument {

	public required string Position { get; set; }
	public required string Description { get; set; }
	public required DateTime ExpirationDate { get; set; }
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

	public static JobDocument CreateFromIHit(IHit<JobDocument> jobHit) {

		return jobHit.Source;
	}

	public static List<JobDocument> CreateFromIHit(IEnumerable<IHit<JobDocument>> jobHits) {

		return jobHits.Select(CreateFromIHit).ToList();
	}

	public static JobDocument CreateFromJob(Entities.Job job) {

		return new JobDocument {
			Id = job.Id,
			Position = job.Position,
			Description = job.Description,
			ExpirationDate = job.ExpirationDate,
			Quality = job.Quality,
			WorkingType = job.WorkingType,
			WorkingMode = job.WorkingMode,
			EducationLevel = job.EducationLevel,
			ExperienceType = job.ExperienceType,
			MinExperience = job.MinExperience,
			MaxExperience = job.MaxExperience,
			Salary = job.Salary,
			Benefits = job.Benefits,
			CompanyId = job.CompanyId,
			DepartmentId = job.DepartmentId,
			Company = job.Company,
			Department = job.Department,
			IsActive = job.IsActive,
			CreatedDate = job.CreatedDate,
			UpdatedDate = job.UpdatedDate
		};
	}

	public static List<JobDocument> CreateFromJob(List<Entities.Job> jobs) {

		return jobs.Select(CreateFromJob).ToList();
	}
}
