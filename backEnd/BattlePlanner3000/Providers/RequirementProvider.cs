using BattlePlanner3000.Models;
using Newtonsoft.Json;
using System.Data;

namespace BattlePlanner3000.Providers;
public class RequirementProvider
{
	private readonly DbProvider dbProvider;
	public static List<Requirement> RequirementsList { get; private set; } = new List<Requirement>();

	public RequirementProvider(DbProvider dbProvider)
	{
		this.dbProvider = dbProvider;
	}

	public async Task<List<Requirement>> GetRequirementsAsync()
	{
		var data = await dbProvider.GetAllItemsAsync(Constants.RequirementsTable, reader => new Requirement(reader.GetString(reader.GetOrdinal(Constants.RequirementsSearchCol))));
		return data;
	}
	public async Task<List<Requirement>> FindRequirementAsync(string input)
	{
		var query = $"SELECT * FROM {Constants.RequirementsTable} WHERE {Constants.RequirementsSearchCol}='{input}'";
		var data = await dbProvider.QueryGetDataAsync(query,
			reader => new Requirement(reader.GetString(reader.GetOrdinal(Constants.RequirementsSearchCol))));
		return data;
	}
	
	public async Task<List<Requirement>> SearchRequirementsAsync(string input)
	{
		var query = $"SELECT * FROM {Constants.RequirementsTable} WHERE {Constants.RequirementsSearchCol} like '{input}%'";
		var data = await dbProvider.QueryGetDataAsync(query,
			reader => new Requirement(reader.GetString(reader.GetOrdinal(Constants.RequirementsSearchCol))));
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
	public async Task DeleteRequirementAsync(string input)
	{
		var query = $"DELETE FROM {Constants.RequirementsTable} WHERE {Constants.RequirementsSearchCol}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}

	//pouziva to resource, pak smazu
	public Requirement FindRequirement(string name)
	{
		return RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}

	

	

	

}