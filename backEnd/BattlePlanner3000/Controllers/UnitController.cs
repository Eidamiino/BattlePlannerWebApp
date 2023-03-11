using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;
using Newtonsoft.Json.Linq;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UnitController : ControllerBase
{
	private readonly UnitProvider provider;
	private readonly ResourceProvider resourceProvider;

	public UnitController(UnitProvider provider, ResourceProvider resourceProvider)
	{
		this.provider = provider;
		this.resourceProvider = resourceProvider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public IActionResult GetUnits()
	{
		return Ok(provider.GetUnits());
	}

	[HttpGet]
	[Route("{name}")]
	public IActionResult GetUnit([FromRoute] string name, [FromQuery] bool returnList)
	{
		if (returnList) return Ok(provider.SearchUnits(name));
		else return Ok(provider.FindUnit(name));
	}

	[HttpPost]
	[Route("")]
	public IActionResult PostUnit([FromBody] UnitResourceAmount input)
	{
		Unit unit = new Unit(input.Unit.Name, new List<ResourceAmount>());
		provider.AddUnit(unit);
		unit.ResourceList.Add(new ResourceAmount(resourceProvider.FindResource(input.ResourceAmount.Resource.Name),input.ResourceAmount.Amount));
		return Ok(unit);
	}
	[HttpDelete]
	[Route("{name}")]
	public IActionResult DeleteUnit([FromRoute] string name)
	{
		var unit= provider.FindUnit(name);
		provider.DeleteUnit(name);
		return Ok(unit);
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