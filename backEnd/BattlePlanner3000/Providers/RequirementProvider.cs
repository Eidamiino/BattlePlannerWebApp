using BattlePlanner3000.Models;
using Newtonsoft.Json;
using System.Data;
using BattlePlanner3000.Mappers;

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
		var query = $"select * from {Tables.Requirements} order by {Columns.Requirement.Title}";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetRequirement(columnIndexes));
		return data;
	}
	public async Task<List<Requirement>> FindRequirementAsync(string input)
	{
		var query = $"SELECT * FROM {Tables.Requirements} WHERE {Columns.Requirement.Title}='{input}'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader,columnIndexes)=> reader.GetRequirement(columnIndexes));
		return data;
	}
	
	public async Task<List<Requirement>> SearchRequirementsAsync(string input)
	{
		var query = $"SELECT * FROM {Tables.Requirements} WHERE lower({Columns.Requirement.Title}) like '{input}%'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader,columnIndexes)=> RequirementMappers.GetRequirement(reader,columnIndexes));
		return data;
	}

	public async Task InsertRequirementAsync(string name)
	{
		var values = new Dictionary<string, object>()
		{
			{ Columns.Requirement.Title, name}
		};
		await dbProvider.InsertItemAsync(Tables.Requirements, values);
	}
	public async Task DeleteRequirementAsync(string input)
	{
		var query = $"DELETE FROM {Tables.Requirements} WHERE {Columns.Requirement.Title}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}

	//pouziva to resource, pak smazu
	public Requirement FindRequirement(string name)
	{
		return RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}

	

	

	

}