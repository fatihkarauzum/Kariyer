using Hangfire;
using Kariyer.Data.Repositories;
using Kariyer.Data.Repositories.Builders;
using Kariyer.Data.Repositories.Elasticsearch;
using Kariyer.Data.Repositories.Elasticsearch.Queries;
using Kariyer.Model.Documents;
using Kariyer.Model.Dtos;
using Nest;
using JobEntity = Kariyer.Model.Entities.Job;

namespace Kariyer.Schedule.Jobs.RecurringJobs;

public class SyncJobs {

    private readonly ElasticsearchRepository elasticsearchRepository;
    private readonly JobRepository jobRepository;

    private const int pageSize = 10000;

    public SyncJobs(ElasticsearchRepository elasticsearchRepository, JobRepository jobRepository) {
        
        this.elasticsearchRepository = elasticsearchRepository;
        this.jobRepository = jobRepository;
    }

    public async Task Process(int pageNumber = 1) {

        BasePagedResult<JobEntity> pagedResult = await GetJobs(pageNumber);

        List<JobEntity> jobs = pagedResult.Items.ToList();
        List<int> jobIds = jobs.Select(x => x.Id).ToList();

		IEnumerable<IHit<JobDocument>> hits = await elasticsearchRepository.SearchDocumentsAsync(JobQueries.GetByIds(jobIds));

        if (hits.Count() > 0) {

			List<int> willUpdateJobIds = JobDocument.CreateFromIHit(hits).Select(jobDocument => jobDocument.Id).ToList();
            List<JobEntity> willUpdateJobs = jobs.Where(job => willUpdateJobIds.Contains(job.Id)).ToList();
			List<JobEntity> willCreateJobs = jobs.Where(job => !willUpdateJobIds.Contains(job.Id)).ToList();

			foreach (var job in willUpdateJobs) {

                await elasticsearchRepository.DeleteDocumentAsync<JobDocument>(job.Id);
                await elasticsearchRepository.IndexDocumentAsync(JobDocument.CreateFromJob(job));
            }

			await elasticsearchRepository.BulkIndexDocumentAsync(JobDocument.CreateFromJob(willCreateJobs));
		}
		else {

			await elasticsearchRepository.BulkIndexDocumentAsync(JobDocument.CreateFromJob(jobs));
		}

        if (pagedResult.TotalPages > pageNumber)
            BackgroundJob.Schedule<SyncJobs>(job => job.Process(pageNumber + 1), TimeSpan.FromMinutes(5));
	}

    private async Task<BasePagedResult<JobEntity>> GetJobs(int pageNumber) {

		QueryBuilder<JobEntity> queryBuilder =
			new QueryBuilder<JobEntity>()
                .WithPageNumber(pageNumber)
                .WithPageSize(pageSize);

		return await jobRepository.FindAsync(queryBuilder);
	}
}
