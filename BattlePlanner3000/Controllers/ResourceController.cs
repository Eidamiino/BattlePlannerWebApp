using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;
using Newtonsoft.Json.Linq;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ResourceController : ControllerBase
{
	private readonly ResourceProvider _provider;
	private readonly RequirementProvider _requirementProvider;

	public ResourceController(ResourceProvider provider, RequirementProvider requirementProvider)
	{
		this._provider = provider;
		_requirementProvider = requirementProvider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public IActionResult GetResources()
	{
		return Ok(_provider.GetResources());
	}

	[HttpPost]
	[Route("")]
	public IActionResult PostResources([FromBody] ResourceRequirementAmount input)
	{
		Resource resource = new Resource(input.Resource.Name, new List<RequirementAmount>());
		_provider.AddResource(resource);
		resource.RequirementList.Add(new RequirementAmount(_requirementProvider.FindRequirementByName(input.RequirementAmount.Requirement.Name),input.RequirementAmount.Amount));
		return Ok(resource);
	}

	// [HttpPost]
	// [Route("")]
	// public IActionResult PostResources([FromBody] string name)
	// {
	// 	Resource resource = new Resource(name, new List<RequirementAmount>());
	// 	_provider.AddResource(resource);
	// 	//resource.RequirementList.Add(RequirementProvider.FindRequirementByName(requirementName), amount);
	// 	return Ok();
	// }

	// [HttpPost]
	// [Route("{resourceName}/requirements")]
	// public IActionResult AddRequirement([FromRoute] string resourceName, [FromBody] RequirementAmount requirementAmount)
	// {
	// 	var requirement = _requirementProvider.FindRequirementByName(requirementAmount.Requirement.Name);
	// 	if (requirement == null) return NotFound("Requirement Not Found");
	//
	// 	var resource = _provider.FindResourceByName(resourceName);
	// 	if (resource == null) return NotFound("Resource Not Found");
	//
	// 	resource.RequirementList.Add(new RequirementAmount(requirement, requirementAmount.Amount));
	// 	//resource.RequirementList.Add(RequirementProvider.FindRequirementByName(requirementName), amount);
	// 	return Ok();
	// }


	[HttpGet]
	[Route("{resourceName}/requirements")]
	public IActionResult GetRequirements([FromRoute] string resourceName)
	{
		var resource = _provider.FindResourceByName(resourceName);
		if (resource == null) return NotFound("Resource Not Found");
		//resource.RequirementList.Add(RequirementProvider.FindRequirementByName(requirementName), amount);
		return Ok(resource.RequirementList);
	}

	[HttpGet]
	[Route("{name}")]
	public IActionResult GetResource([FromRoute] string name)
	{
		var resource = _provider.FindResourceByName(name);
		if (resource == null) return NotFound("Resource Not Found");
		//resource.RequirementList.Add(RequirementProvider.FindRequirementByName(requirementName), amount);
		return Ok(resource);
	}
}