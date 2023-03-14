using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;
using Newtonsoft.Json.Linq;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BattlePlanController: ControllerBase
{
	private readonly BattlePlanProvider provider;
	private readonly UnitProvider unitProvider;

	public BattlePlanController(BattlePlanProvider provider, UnitProvider unitProvider)
	{
		this.provider = provider;
		this.unitProvider= unitProvider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public IActionResult GetBattlePlans()
	{
		return Ok(provider.GetBattlePlans());
	}

	[HttpGet]
	[Route("{name}")]
	public IActionResult GetBattlePlan([FromRoute] string name, [FromQuery] bool returnList)
	{
		if (returnList) return Ok(provider.SearchBattlePlan(name));
		else return Ok(provider.FindBattlePlan(name));
	}

	[HttpPost]
	[Route("")]
	public IActionResult PostBattlePlan([FromBody] BattlePlanUnitDuration input)
	{

		//Unit unit = new Unit(input.Unit.Name, new List<ResourceAmount>());
		BattlePlan battlePlan = new BattlePlan(input.BattlePlan.Name, new List<Unit>(), input.BattlePlan.Duration);
		provider.AddBattlePlan(battlePlan);
		battlePlan.UnitList.Add(unitProvider.FindUnit(input.Unit.Name));
		battlePlan.CalculateSummary();
		return Ok(battlePlan);
	}
	[HttpDelete]
	[Route("{name}")]
	public IActionResult DeleteBattlePlan([FromRoute] string name)
	{
		var battlePlan= provider.FindBattlePlan(name);
		provider.DeleteBattlePlan(name);
		return Ok(battlePlan);
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