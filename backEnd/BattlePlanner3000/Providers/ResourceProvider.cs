using BattlePlanner3000.Mappers;
using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class ResourceProvider
{

	private readonly DbProvider dbProvider;
	public ResourceProvider(DbProvider dbProvider)
	{
		this.dbProvider = dbProvider;
	}

	public async Task<List<Resource>> GetResourcesAsync()
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = @"SELECT r.resource_id, rr.amount, req.title, r.title_resource, req.requirement_id 
									FROM resource r
									JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                  JOIN requirement req ON rr.requirement_id = req.requirement_id";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => ResourceMappers.GetResource(reader, columnIndexes, ResourceList));
		return data;
	}


	public async Task<List<Resource>> FindResourceAsync(string input)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"SELECT r.resource_id, rr.amount, req.title, r.title_resource, req.requirement_id 
									FROM resource r
									JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                  JOIN requirement req ON rr.requirement_id = req.requirement_id
									where {Columns.Resource.Title}='{input}'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetResource(columnIndexes, ResourceList));
		return data;
	}

	public async Task<List<Resource>> SearchResourcesAsync(string input)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"select r.resource_id, rr.amount, req.title, r.title_resource, req.requirement_id
									FROM resource r
									JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                  JOIN requirement req ON rr.requirement_id = req.requirement_id
									WHERE {Columns.Resource.Title} like '{input}%'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetResource(columnIndexes, ResourceList));
		return data;
	}

	public async Task InsertResourceAsync(string resourceName, int requirementId, int amount)
	{
		var resourceValues = new Dictionary<string, object>()
		{
			{ Columns.Resource.Title, resourceName}
		};
		var resourceId=await dbProvider.InsertItemAsync(Tables.Resources, resourceValues, Columns.Resource.Id);

		// foreach (var requirement in resource.RequirementList)
		var resourceRequirementValues = new Dictionary<string, object>()
			{
				{ Columns.ResourceRequirement.ResourceId, resourceId},
				{ Columns.ResourceRequirement.RequirementId, requirementId},
				{ Columns.ResourceRequirement.Amount, amount}
			};

			await dbProvider.InsertItemAsync(Tables.ResourceRequirements, resourceRequirementValues);
		}
	public async Task DeleteResourceAsync(string input)
	{
		var query = $"DELETE FROM {Tables.Resources} WHERE {Columns.Resource.Title}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}

	List<Resource> ResourceList = new List<Resource>();
	public List<Resource> GetResources()
	{
		return ResourceList;
	}
	public void AddResource(Resource resource)
	{
		ResourceList.Add(resource);
	}
	public Resource FindResource(string name)
	{
		return ResourceList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
}