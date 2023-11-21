using Kariyer.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kariyer.Api.Controllers;

[Route("harmful-words")]
[ApiController]
public class HarmfulWordsController : ControllerBase {

	private readonly HarmfulWordsService harmfulWordsService;

	public HarmfulWordsController(HarmfulWordsService harmfulWordsService) {

		this.harmfulWordsService = harmfulWordsService;
	}

	[HttpGet("list/{word}")]
	[HttpGet("list")]
	public async Task<IActionResult> List(string word = "") {

		return Ok(await harmfulWordsService.List(word));
	}

	[HttpPost("create/{word}")]
	public async Task<IActionResult> Create(string word) {

		await harmfulWordsService.Create(word);

		return Ok();
	}

	[HttpPut("update/{oldWord}/{newWord}")]
	public async Task<IActionResult> Update(string oldWord, string newWord) {

		await harmfulWordsService.Update(oldWord, newWord);

		return Ok();
	}

	[HttpDelete("update/{word}")]
	public async Task<IActionResult> Delete(string word) {

		await harmfulWordsService.Delete(word);

		return Ok();
	}
}
