using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Providers;

namespace WebApplication3.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RequirementsController : ControllerBase
{
	private readonly RequirementProvider _provider;
	public RequirementsController(RequirementProvider provider)
	{
		this._provider = provider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public IActionResult GetRequirements()
	{
		return Ok(_provider.GetRequirements());
	}

	[HttpGet]
	[Route("{name}")]
	public IActionResult GetRequirement([FromRoute] string name, [FromQuery]bool returnList)
	{
		if (returnList) return Ok(_provider.GetRequirementsByQuery(name));
		else return Ok(_provider.FindRequirementByName(name));
	}

	[HttpDelete]
	[Route("{name}")]
	public IActionResult DeleteRequirement([FromRoute] string name)
	{
		var requirement = _provider.FindRequirementByName(name);
		_provider.DeleteRequirement(name);
		return Ok(requirement);
	}

	[HttpPost]
	[Route("")]
	public IActionResult PostRequirement([FromBody] string name)
	{
		Requirement requirement = new Requirement(name);
		_provider.AddRequirement(name);
		return Ok(requirement);
	}
	/*[HttpGet]
	[Route("")]
	public IActionResult GetRequirementsByQuery([FromBody] string query)
	{
		return Ok(_provider.GetRequirementsByQuery(query));
	}
	*/
	/*
	[HttpGet]
	[Route("{query}")]
	public IActionResult GetRequirementsByQuery([FromQuery] string query)
	{
		return Ok(_provider.GetRequirementsByQuery(query));
	}
	*/
}