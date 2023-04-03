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
		var query = @"SELECT r.title_resource, rr.amount, req.title 
									FROM resource r
									JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                  JOIN requirement req ON rr.requirement_id = req.requirement_id";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => ResourceMappers.GetResource(reader, columnIndexes, ResourceList));
		return data;
	}


	public async Task<List<Resource>> FindResourceAsync(string input)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"SELECT r.title_resource, rr.amount, req.title 
									FROM resource r
									JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                  JOIN requirement req ON rr.requirement_id = req.requirement_id
									where {Columns.Requirement.Title}='{input}'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetResource(columnIndexes, ResourceList));
		return data;
	}

	public async Task<List<Resource>> SearchResourcesAsync(string input)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"SELECT r.title_resource, rr.amount, req.title 
									FROM resource r
									JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                  JOIN requirement req ON rr.requirement_id = req.requirement_id
									WHERE {Constants.RequirementsSearchCol} like '{input}%'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetResource(columnIndexes, ResourceList));
		return data;
	}

	public async Task<int> InsertRequirementAsync(Requirement requirement)
	{
		var values = new Dictionary<string, object>()
		{
			{ @Constants.RequirementsSearchCol, requirement.Name}
		};
		return await dbProvider.InsertItemAsync(Constants.RequirementsTable, values);
	}
	public async Task<int> DeleteRequirementAsync(string query)
	{
		return await dbProvider.DeleteItemAsync(Constants.RequirementsTable, Constants.RequirementsSearchCol, query);
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
	public void DeleteResource(string name)
	{
		var resource = ResourceList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		ResourceList.Remove(resource);
	}
	public List<Resource> SearchResources(string query)
	{
		return ResourceList.Where(item => item.Name.StartsWith(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
	}
}