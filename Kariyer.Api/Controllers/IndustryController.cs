using Kariyer.Business.Dtos.IndustryDtos;
using Kariyer.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kariyer.Api.Controllers;

[Route("industry")]
[ApiController]
public class IndustryController : ControllerBase {

	private readonly IndustryService industryService;

	public IndustryController(IndustryService industryService) {
		this.industryService = industryService;
	}

	[HttpGet("get/{id}")]
	public async Task<IActionResult> Get(int id) {

		return Ok(await industryService.Get(id));
	}

	[HttpGet("list/{pageNumber}/{pageSize}")]
	public async Task<IActionResult> List(int pageNumber, int pageSize) {

		return Ok(await industryService.List(pageNumber, pageSize));
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create(PostIndustryItem postIndustryItem) {

		return Ok(await industryService.Create(postIndustryItem));
	}

	[HttpPut("update")]
	public async Task<IActionResult> Update(PostIndustryItem postIndustryItem) {

		await industryService.Update(postIndustryItem);

		return Ok();
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id) {

		await industryService.Delete(id);

		return Ok();
	}

	[HttpPatch("passive/{id}")]
	public async Task<IActionResult> Passive(int id) {

		await industryService.Delete(id);

		return Ok();
	}
}
