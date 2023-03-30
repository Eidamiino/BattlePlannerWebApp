using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class ResourceProvider
{
	private readonly DbProvider dbProvider;
	public static List<Resource> ResourceList { get; private set; } = new List<Resource>();
	public ResourceProvider(DbProvider dbProvider)
	{
		this.dbProvider = dbProvider;
	}
	public async Task<IEnumerable<Resource>> GetResourcesAsync()
	{
		var dataReader = await dbProvider.GetAllItemsMtoN(@"SELECT r.title, rr.amount, req.title 
                        FROM resource r 
                        JOIN resource_requirement rr ON r.resource_id = rr.resource_id 
                        JOIN requirement req ON rr.requirement_id = req.requirement_id");
		if (dataReader.IsClosed)
		{
			return new List<Resource>();
		}
		return dataReader.Select(dr =>
		{
			var mainTitle = dr.GetString(0);
			var listItem = new RequirementAmount(new Requirement(dr.GetString(2)), dr.GetInt32(1));
			var mainList = new List<Resource>();
			var mainListItem = mainList.FirstOrDefault(r => r.Name == mainTitle);
			if (mainListItem == null)
			{
				mainListItem = new Resource(mainTitle, new List<RequirementAmount>());
				mainList.Add(mainListItem);
			}
			mainListItem.RequirementList.Add(listItem);

			return mainListItem;
		}).ToList();
	}
	public async Task<IEnumerable<Resource>> FindResourceAsync(string query)
	{
		var dataReader = await dbProvider.GetItemAsync(@Constants.ResourcesTable, @Constants.ResourcesSearchCol, query);
		if (!dataReader.IsClosed)
		{
			return dataReader.Select(r =>
				new Resource(r.GetString(r.GetOrdinal(@Constants.ResourcesSearchCol)),new List<RequirementAmount>())).ToList();
		}

		return new List<Resource>();
	}

	public async Task<IEnumerable<Requirement>> SearchRequirementsAsync(string query)
	{
		var dataReader = await dbProvider.GetItemsStartsWith(@Constants.RequirementsTable, @Constants.RequirementsSearchCol, query);
		if (!dataReader.IsClosed)
		{
			return dataReader.Select(r =>
					new Requirement(r.GetString(r.GetOrdinal(@Constants.RequirementsSearchCol))))
				.ToList();
		}

		return new List<Requirement>();
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
		var resource= ResourceList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		ResourceList.Remove(resource);
	}
	public List<Resource> SearchResources(string query)
	{
		return ResourceList.Where(item => item.Name.StartsWith(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
	}
}