using Kariyer.Model.Enums;

namespace Kariyer.Business.Dtos.JobDtos;

public class SearchJob {

	public string QueryString { get; set; }
	public WorkingType WorkingType { get; set; }
	public WorkingMode WorkingMode { get; set; }
    public EducationLevel EducationLevel { get; set; }
    public ExperienceType ExperienceType { get; set; }
	public int? CompanyId { get; set; }
	public int? DepartmentId { get; set; }
	public int? MinExperience { get; set; } = 0;
    public int? MaxExperience { get; set; } = 15;

	public int PageNumber { get; set; } = 0;
	public int PageSize { get; set; } = 20;
}
