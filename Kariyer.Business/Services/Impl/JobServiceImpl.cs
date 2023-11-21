using Kariyer.Business.Dtos.JobDtos;
using Kariyer.Data.Repositories;
using Kariyer.Model.Entities;
using Kariyer.Data.Repositories.Elasticsearch;
using Kariyer.Model.Documents;
using Kariyer.Business.Services.JobQuality;
using Kariyer.Data.Repositories.Elasticsearch.Queries;
using Kariyer.Common.Exceptions;

namespace Kariyer.Business.Services.Impl;

public class JobServiceImpl : JobService {

	private readonly UnitOfWork unitOfWork;
	private readonly ElasticsearchRepository elasticsearchRepository;
	private readonly JobQualityService jobQualityService;

	public JobServiceImpl(UnitOfWork unitOfWork, ElasticsearchRepository elasticsearchRepository, JobQualityService jobQualityService) {

		this.unitOfWork = unitOfWork;
		this.elasticsearchRepository = elasticsearchRepository;
		this.jobQualityService = jobQualityService;
	}

	public async Task<GetJobItem> Get(int id) {

		Job? job = await unitOfWork.job.GetByIdAsync(id);

		if (job == null)
			throw JobExceptions.JobNotFound($"Job Not Found (Id: {id})");

		return GetJobItem.CreateFromJob(job);
	}

	public async Task<GetJobList> List(SearchJob searchJob) {

		var searchQuery = JobQueries.Search(
			searchJob.QueryString, 
			searchJob.WorkingType, 
			searchJob.WorkingMode, 
			searchJob.EducationLevel, 
			searchJob.ExperienceType, 
			searchJob.CompanyId, 
			searchJob.DepartmentId, 
			searchJob.MinExperience,
			searchJob.MaxExperience);

		var result = await elasticsearchRepository.SearchDocumentsAsync(searchQuery, searchJob.PageNumber, searchJob.PageSize);

		return GetJobList.CreateFromBasePagedResult(result);
	}

	public async Task<GetJobItem> Create(PostJobItem postJob) {

		Company? company = await unitOfWork.company.GetByIdAsync(postJob.CompanyId);
		if (company == null)
			throw CompanyExceptions.CompanyNotFound($"Company Not Found (Id: {postJob.CompanyId})");

		if (company.RemainingPublishRight <= 0)
			throw CompanyExceptions.NoPublishRight();

		Job newItem = PostJobItem.CreateFromJobItem(postJob);
		newItem.Quality = jobQualityService.CalculateJobQualityScore(newItem);

		Job job = await unitOfWork.job.Create(newItem);
		await unitOfWork.company.DecRemainingPublishRight(postJob.CompanyId);

		await unitOfWork.CommitAsync();

		await elasticsearchRepository.IndexDocumentAsync(JobDocument.CreateFromJob(job));

		return GetJobItem.CreateFromJob(job);
	}

	public async Task Update(PostJobItem postJob) {

		int jobId = postJob.Id;
		Job? job = await unitOfWork.job.GetByIdAsync(jobId);

		if (job == null)
			throw JobExceptions.JobNotFound($"Job Not Found (Id: {postJob.Id})");

		unitOfWork.job.Update(PostJobItem.CreateFromJobItem(postJob));
		await unitOfWork.CommitAsync();

		await UpdateIndex(jobId);
	}

	public async Task Delete(int id) {

		Job? job = await unitOfWork.job.GetByIdAsync(id);

		if (job == null)
			throw JobExceptions.JobNotFound($"Job Not Found (Id: {id})");

		unitOfWork.job.Delete(job);
		await unitOfWork.CommitAsync();

		await elasticsearchRepository.DeleteDocumentAsync<JobDocument>(id);
	}

	public async Task Passive(int id) {

		int jobId = id;
		Job? job = await unitOfWork.job.GetByIdAsync(jobId);

		if (job == null)
			throw JobExceptions.JobNotFound($"Job Not Found (Id: {id})");

		await unitOfWork.job.Passive(id);
		await unitOfWork.CommitAsync();

		await UpdateIndex(id);
	}

	private async Task UpdateIndex(int id) {

		Job? job = await unitOfWork.job.GetByIdAsync(id);

		if (job == null)
			throw JobExceptions.JobNotFound($"Job Not Found (Id: {id})");

		await elasticsearchRepository.DeleteDocumentAsync<JobDocument>(id);
		await elasticsearchRepository.IndexDocumentAsync(JobDocument.CreateFromJob(job));
	}
}
