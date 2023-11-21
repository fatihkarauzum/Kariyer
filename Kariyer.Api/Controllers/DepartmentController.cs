using Kariyer.Business.Dtos.DepartmentDtos;
using Kariyer.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kariyer.Api.Controllers;

[Route("department")]
[ApiController]
public class DepartmentController : ControllerBase {

	private readonly DepartmentService departmentService;

	public DepartmentController(DepartmentService departmentService) {
		this.departmentService = departmentService;
	}

	[HttpGet("get/{id}")]
	public async Task<IActionResult> Get(int id) {

		return Ok(await departmentService.Get(id));
	}

	[HttpGet("list/{pageNumber}/{pageSize}")]
	public async Task<IActionResult> List(int pageNumber, int pageSize) {

		return Ok(await departmentService.List(pageNumber, pageSize));
	}

	[HttpGet("list")]
	public async Task<IActionResult> List() {

		return Ok(await departmentService.List());
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create(PostDepartmentItem postDepartmentItem) {

		return Ok(await departmentService.Create(postDepartmentItem));
	}

	[HttpPut("update")]
	public async Task<IActionResult> Update(PostDepartmentItem postDepartmentItem) {

		await departmentService.Update(postDepartmentItem);

		return Ok();
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id) {

		await departmentService.Delete(id);

		return Ok();
	}

	[HttpPatch("passive/{id}")]
	public async Task<IActionResult> Passive(int id) {

		await departmentService.Delete(id);

		return Ok();
	}
}
