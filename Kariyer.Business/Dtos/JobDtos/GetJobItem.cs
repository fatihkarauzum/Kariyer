using Kariyer.Business.Dtos.IndustryDtos;
using Kariyer.Common.Utils;
using Kariyer.Model.Dtos;
using Kariyer.Model.Entities;
using Kariyer.Model.Enums;

namespace Kariyer.Business.Dtos.JobDtos;

public class GetJobItem : BaseDto<GetJobItem> {

	public string Position { get; set; }
	public string Description { get; set; }
	public DateTime ExpirationDate { get; set; }
	public double Quality { get; set; }
	public WorkingType WorkingType { get; set; }
	public WorkingMode WorkingMode { get; set; }
	public EducationLevel EducationLevel { get; set; }
	public ExperienceType ExperienceType { get; set; }
	public int MinExperience { get; set; }
	public int MaxExperience { get; set; }
	public int Salary { get; set; }
	public string? Benefits { get; set; }

	public static GetJobItem CreateFromJob(Job job) {

		return new GetJobItem {
			Id = job.Id,
			Position = job.Position,
			Description = job.Description,
			ExpirationDate = job.ExpirationDate.SetKindLocal(),
			Quality = job.Quality,	
			WorkingType = job.WorkingType,	
			WorkingMode = job.WorkingMode,	
			EducationLevel = job.EducationLevel,
			ExperienceType = job.ExperienceType,
			MinExperience = job.MinExperience,
			MaxExperience = job.MaxExperience,
			Salary = job.Salary,
			Benefits = job.Benefits,
			IsActive = job.IsActive,
			CreatedDate = job.CreatedDate.SetKindLocal(),
			UpdatedDate = job.UpdatedDate.SetKindLocal()
		};
	}

	public static List<GetJobItem> CreateFromJob(List<Job> ındustries) {

		return ındustries.Select(CreateFromJob).ToList();
	}
}
