using Kariyer.Business.Dtos.JobDtos;
using Kariyer.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kariyer.Api.Controllers;

[Route("job")]
[ApiController]
public class JobController : ControllerBase {

	private readonly JobService jobService;

	public JobController(JobService jobService) {
		this.jobService = jobService;
	}

	[HttpGet("get/{id}")]
	public async Task<IActionResult> Get(int id) {

		return Ok(await jobService.Get(id));
	}

	[HttpPost("list")]
	public async Task<IActionResult> List(SearchJob searchJob) {

		return Ok(await jobService.List(searchJob));
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create(PostJobItem postJobItem) {

		return Ok(await jobService.Create(postJobItem));
	}

	[HttpPut("update")]
	public async Task<IActionResult> Update(PostJobItem postJobItem) {

		await jobService.Update(postJobItem);

		return Ok();
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id) {

		await jobService.Delete(id);

		return Ok();
	}

	[HttpPatch("passive/{id}")]
	public async Task<IActionResult> Passive(int id) {

		await jobService.Delete(id);

		return Ok();
	}
}
