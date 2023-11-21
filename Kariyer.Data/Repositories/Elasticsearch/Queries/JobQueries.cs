using Kariyer.Model.Documents;
using Kariyer.Model.Enums;
using Nest;

namespace Kariyer.Data.Repositories.Elasticsearch.Queries;

public static class JobQueries {

	public static Func<QueryContainerDescriptor<JobDocument>, QueryContainer> GetById(int id) {

		Func<QueryContainerDescriptor<JobDocument>, QueryContainer> query =
			new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
				.Term(t => t
					.Field(f => f.Id)
					.Value(id)
					)
				);

		return query;
	}

	public static Func<QueryContainerDescriptor<JobDocument>, QueryContainer> GetByIds(List<int> ids) {
		Func<QueryContainerDescriptor<JobDocument>, QueryContainer> query =
			new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
				.Terms(t => t
					.Field(f => f.Id)
					.Terms(ids)
				)
			);

		return query;
	}

	public static Func<QueryContainerDescriptor<JobDocument>, QueryContainer> Search(
		string queryString,
		WorkingType workingType,
		WorkingMode workingMode,
		EducationLevel educationLevel,
		ExperienceType experienceType,
		int? companyId,
		int? departmentId,
		int? minExperience = 0,
		int? maxExperience = 15) {

		var baseQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.MultiMatch(t => t
				.Fields(f => f
					.Field(f => f.Position)
					.Field(f => f.Description)
					.Field(f => f.Company.Name)
					.Field(f => f.Department.Name))
				.Query(queryString)
			)
		);

		var workingTypeQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Term(t => t
				.Field(f => f.WorkingType)
				.Value(workingType)
			)
		);

		var workingModeQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Term(t => t
				.Field(f => f.WorkingMode)
				.Value(workingMode)
			)
		);

		var educationLevelQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Term(t => t
				.Field(f => f.EducationLevel)
				.Value(educationLevel)
			)
		);

		var experienceTypeQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Term(t => t
				.Field(f => f.ExperienceType)
				.Value(experienceType)
			)
		);

		var minExperienceQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Range(t => t
				.Field(f => f.MinExperience)
				.GreaterThanOrEquals(minExperience)
			)
		);

		var maxExperienceQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Range(t => t
				.Field(f => f.MaxExperience)
				.LessThanOrEquals(maxExperience)
			)
		);

		var companyIdQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Term(t => t
				.Field(f => f.CompanyId)
				.Value(companyId)
			)
		);

		var departmentIdQuery = new Func<QueryContainerDescriptor<JobDocument>, QueryContainer>(q => q
			.Term(t => t
				.Field(f => f.DepartmentId)
				.Value(departmentId)
			)
		);

		var mustQueries = new List<Func<QueryContainerDescriptor<JobDocument>, QueryContainer>> { baseQuery };

		if (workingType != WorkingType.NONE) {
			mustQueries.Add(workingTypeQuery);
		}

		if (workingMode != WorkingMode.NONE) {
			mustQueries.Add(workingModeQuery);
		}

		if (educationLevel != EducationLevel.NONE) {
			mustQueries.Add(educationLevelQuery);
		}

		if (experienceType == ExperienceType.EXPERIENCED) {
			mustQueries.Add(experienceTypeQuery);
			mustQueries.Add(minExperienceQuery);
			mustQueries.Add(maxExperienceQuery);
		}

		if (companyId != null) {
			mustQueries.Add(companyIdQuery);
		}

		if (departmentId != null) {
			mustQueries.Add(departmentIdQuery);
		}

		return q => q
			.Bool(b => b
				.Must(mustQueries
					.ToArray()));
	}
}
