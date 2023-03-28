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
		if (returnList) return Ok(await provider.SeachRequirementsAsync(name));
		return Ok(await provider.FindRequirementAsync(name));
	}

	[HttpDelete]
	[Route("{name}")]
	public IActionResult DeleteRequirement([FromRoute] string name)
	{
		var requirement = provider.FindRequirement(name);
		provider.DeleteRequirement(name);
		return Ok(requirement);
	}

	[HttpPost]
	[Route("")]
	public IActionResult PostRequirement([FromBody] string name)
	{
		Requirement requirement = new Requirement(name);
		provider.AddRequirement(name);
		return Ok(requirement);
	}
	/*[HttpGet]
	[Route("")]
	public IActionResult SearchRequirements([FromBody] string query)
	{
		return Ok(provider.SearchRequirements(query));
	}
	*/
	/*
	[HttpGet]
	[Route("{query}")]
	public IActionResult SearchRequirements([FromQuery] string query)
	{
		return Ok(provider.SearchRequirements(query));
	}
	*/

}