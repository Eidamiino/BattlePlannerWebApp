using Microsoft.AspNetCore.Mvc;
using BattlePlanner3000.Models;
using BattlePlanner3000.Providers;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RequirementsController : ControllerBase
{
	private readonly RequirementProvider provider;
	
	public RequirementsController(RequirementProvider provider)
	{
		this.provider = provider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public async Task<IActionResult> GetRequirements()
	{
		return Ok(await provider.GetRequirementsAsync());
	}

	[HttpGet]
	[Route("{name}")]
	public async Task<IActionResult> GetRequirement([FromRoute] string name, [FromQuery]bool returnList)
	{
		if (returnList) return Ok(await provider.SearchRequirementsAsync(name));
		return Ok(await provider.FindRequirementAsync(name));
	}

	[HttpGet]
	[Route("{name}/contained")]
	public async Task<IActionResult> GetResourcesWithRequirement([FromRoute] string name)
	{
		var requirement = (await provider.FindRequirementAsync(name)).Distinct().ToList();
		return Ok((await provider.GetResourcesWithRequirementAsync(requirement[0].Id)).Distinct());
	}

	[HttpDelete]
	[Route("{name}")]
	public async Task<IActionResult> DeleteRequirement([FromRoute] string name)
	{
		await provider.DeleteRequirementAsync(name);
		return Ok();
	}

	[HttpPost]
	[Route("")]
	public async Task<IActionResult> PostRequirement([FromBody] string name)
	{
		await provider.InsertRequirementAsync(name);
		return Ok();
	}
	

}