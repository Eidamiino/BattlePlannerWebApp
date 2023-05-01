using BattlePlanner3000.Models;
using Newtonsoft.Json;
using System.Data;
using BattlePlanner3000.Mappers;
using Npgsql;

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

	public async Task<List<Resource>> GetResourcesWithRequirementAsync(int reqId)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"SELECT r.{Columns.Resource.Id}, r.{Columns.Resource.Title}
									FROM {Tables.Resources} r
									JOIN {Tables.ResourceRequirements} rr ON r.{Columns.Resource.Id} = rr.{Columns.ResourceRequirement.ResourceId}									
									where {Columns.ResourceRequirement.RequirementId}={reqId}";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => reader.GetPlainResource(columnIndexes, ResourceList));
		return data;
	}

	public async Task<List<Requirement>> FindRequirementAsync(string input)
	{
		var query = $"SELECT * FROM {Tables.Requirements} WHERE {Columns.Requirement.Title}=@input";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter("@input", NpgsqlTypes.NpgsqlDbType.Text) { Value = input }
		};
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetRequirement(columnIndexes),
			parameters);
		return data;
	}

	public async Task<List<Requirement>> SearchRequirementsAsync(string input)
	{
		var query = $"SELECT * FROM {Tables.Requirements} WHERE lower({Columns.Requirement.Title}) like '{input}%'";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter("input", NpgsqlTypes.NpgsqlDbType.Text) { Value = input.ToLower() + "%" }
		};
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => RequirementMappers.GetRequirement(reader, columnIndexes),
			parameters);
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

	

	

	

	

}