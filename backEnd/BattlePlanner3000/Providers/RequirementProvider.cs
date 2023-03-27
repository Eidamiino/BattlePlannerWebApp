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

	public async Task<IEnumerable<Requirement>> GetRequirementsAsync(string tableName)
	{
		var dataReader = await dbProvider.GetAllItemsAsync(tableName);
		if (!dataReader.IsClosed)
		{
			return dataReader.Select(r =>
					new Requirement(r.GetString(r.GetOrdinal("title"))))
				.ToList();
		}

		return new List<Requirement>();
	}

	// public async Task<IEnumerable<Requirement>> GetRequirementsAsync(string tableName)
	// {
	// 	var json = await dbProvider.GetAllItemsAsync(tableName);
	// 	var dataTable = JsonConvert.DeserializeObject<DataTable>(json);
	// 	return dataTable.AsEnumerable().Select(r => new Requirement(r.Field<string>("title"))).ToList();
	// }

	public void AddRequirement(string name)
	{
		RequirementsList.Add(new Requirement(name));
	}
	public void DeleteRequirement(string name)
	{
		var requirement = RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		RequirementsList.Remove(requirement);
	}
	public Requirement FindRequirement(string name)
	{
		return RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
	public List<Requirement> SearchRequirements(string query)
	{
		return RequirementsList.Where(item => item.Name.StartsWith(query,StringComparison.InvariantCultureIgnoreCase)).ToList();
	}

}