using Kariyer.Business.Dtos.CompanyDtos;
using Kariyer.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kariyer.Api.Controllers;

[Route("company")]
[ApiController]
public class CompanyController : ControllerBase {

	private readonly CompanyService companyService;

    public CompanyController(CompanyService companyService)
    {
		this.companyService = companyService;
    }

    [HttpGet("get/{id}")]
	public async Task<IActionResult> Get(int id) {

		return Ok(await companyService.Get(id));
	}

	[HttpGet("list/{pageNumber}/{pageSize}")]
	public async Task<IActionResult> List(int pageNumber, int pageSize) {

		return Ok(await companyService.List(pageNumber, pageSize));
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create(PostCompanyItem postCompanyItem) {

		return Ok(await companyService.Create(postCompanyItem));
	}

	[HttpPut("update")]
	public async Task<IActionResult> Update(PostCompanyItem postCompanyItem) {

		await companyService.Update(postCompanyItem);

		return Ok();
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id) {

		await companyService.Delete(id);

		return Ok();
	}

	[HttpPatch("passive/{id}")]
	public async Task<IActionResult> Passive(int id) {

		await companyService.Delete(id);

		return Ok();
	}
}
