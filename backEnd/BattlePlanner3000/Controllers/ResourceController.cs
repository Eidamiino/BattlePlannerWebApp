using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ResourceController : ControllerBase
{
	private readonly ResourceProvider provider;
	private readonly RequirementProvider requirementProvider;

	public ResourceController(ResourceProvider provider, RequirementProvider requirementProvider)
	{
		this.provider = provider;
		this.requirementProvider = requirementProvider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public IActionResult GetResources()
	{
		return Ok(provider.GetResources());
	}

	[HttpGet]
	[Route("{name}")]
	public IActionResult GetResource([FromRoute] string name, [FromQuery] bool returnList)
	{
		if (returnList) return Ok(provider.SearchResources(name));
		else return Ok(provider.FindResource(name));
	}

	[HttpPost]
	[Route("")]
	public IActionResult PostResources([FromBody] ResourceRequirementAmount input)
	{
		Resource resource = new Resource(input.Resource.Name, new List<RequirementAmount>());
		provider.AddResource(resource);
		resource.RequirementList.Add(new RequirementAmount(requirementProvider.FindRequirement(input.RequirementAmount.Requirement.Name), input.RequirementAmount.Amount));
		return Ok(resource);
	}
	[HttpDelete]
	[Route("{name}")]
	public IActionResult DeleteResource([FromRoute] string name)
	{
		var resource = provider.FindResource(name);
		provider.DeleteResource(name);
		return Ok(resource);
	}

	// [HttpPost]
	// [Route("")]
	// public IActionResult PostResources([FromBody] string name)
	// {
	// 	Resource resource = new Resource(name, new List<RequirementAmount>());
	// 	provider.AddResource(resource);
	// 	//resource.RequirementList.Add(RequirementProvider.FindRequirement(requirementName), amount);
	// 	return Ok();
	// }

	// [HttpPost]
	// [Route("{resourceName}/requirements")]
	// public IActionResult AddRequirement([FromRoute] string resourceName, [FromBody] RequirementAmount requirementAmount)
	// {
	// 	var requirement = requirementProvider.FindRequirement(requirementAmount.Requirement.Name);
	// 	if (requirement == null) return NotFound("Requirement Not Found");
	//
	// 	var resource = provider.FindResource(resourceName);
	// 	if (resource == null) return NotFound("Resource Not Found");
	//
	// 	resource.RequirementList.Add(new RequirementAmount(requirement, requirementAmount.Amount));
	// 	//resource.RequirementList.Add(RequirementProvider.FindRequirement(requirementName), amount);
	// 	return Ok();
	// }
}