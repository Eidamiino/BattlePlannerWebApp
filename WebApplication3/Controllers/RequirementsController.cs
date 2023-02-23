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
	public IActionResult GetRequirement([FromRoute] string name)
	{
		var requirement = _provider.FindRequirementByName(name);

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
}