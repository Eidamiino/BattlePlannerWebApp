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
	public IActionResult GetRequirements()
	{
		return Ok(provider.GetRequirements());
	}

	[HttpGet]
	[Route("{name}")]
	public IActionResult GetRequirement([FromRoute] string name, [FromQuery]bool returnList)
	{
		if (returnList) return Ok(provider.SearchRequirements(name));
		else return Ok(provider.FindRequirement(name));
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