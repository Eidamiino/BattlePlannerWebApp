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

	[HttpGet]
	[Route("")]
	public async Task<IActionResult> GetResources()
	{
		return Ok((await provider.GetResourcesAsync()).Distinct());
	}

	[HttpGet]
	[Route("{name}")]
	public async Task<IActionResult> GetResource([FromRoute] string name, [FromQuery] bool returnList)
	{
		if (returnList) return Ok((await provider.SearchResourcesAsync(name)).Distinct());
		return Ok((await provider.FindResourceAsync(name)).Distinct());
	}

	[HttpGet]
	[Route("{name}/contained")]
	public async Task<IActionResult> GetUnitsWithResource([FromRoute] string name)
	{
		var resource= (await provider.FindResourceAsync(name)).Distinct().ToList();
		return Ok((await provider.GetUnitsWithResourceAsync(resource[0].Id)).Distinct());
	}

	[HttpPost]
	[Route("")]
	public async Task<IActionResult> PostResources([FromBody] ResourceRequirementAmount input)
	{ await provider.InsertResourceAsync(input.ResourceName,input.RequirementId,input.RequirementCapacity);
		return Ok();
	}

	[HttpPut]
	[Route("{name}/addItem")]
	public async Task<IActionResult> AddItemToListAsync([FromRoute] string name, [FromBody] IdAmount input)
	{
		var resourceId = (await provider.FindResourceAsync(name)).Distinct().ToList();
		await provider.AddItemToList(resourceId[0].Id, input.Id, input.Amount);
		return Ok();
	}

	[HttpPut]
	[Route("{name}")]
	public async Task<IActionResult> UpdateAmountAsync([FromRoute] string name,[FromBody] IdAmount input)
	{
		var resourceId = (await provider.FindResourceAsync(name)).Distinct().ToList();
		await provider.AlterAmountAsync(resourceId[0].Id, input.Id,input.Amount);
		return Ok();
	}
	[HttpDelete]
	[Route("{name}")]
	public async Task DeleteResource([FromRoute] string name)
	{
		await provider.DeleteResourceAsync(name);
	}
	[HttpDelete]
	[Route("{name}/removeReq/{requirementId}")]
	public async Task<IActionResult> DeleteRequirementFromListAsync([FromRoute] string name, [FromRoute] int requirementId)
	{
		var resource = (await provider.FindResourceAsync(name)).Distinct().ToList();
		await provider.DeleteRequirementFromListAsync(resource[0].Id, requirementId);
		return Ok();
	}

}