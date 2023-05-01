using BattlePlanner3000.Mappers;
using BattlePlanner3000.Models;
using Npgsql;

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
		var query = $@"SELECT r.{Columns.Resource.Id}, rr.{Columns.ResourceRequirement.Amount}, req.{Columns.Requirement.Title}, r.{Columns.Resource.Title}, req.{Columns.Requirement.Id} 
									FROM {Tables.Resources} r
									JOIN {Tables.ResourceRequirements} rr ON r.{Columns.Resource.Id} = rr.{Columns.ResourceRequirement.ResourceId} 
                  JOIN {Tables.Requirements} req ON rr.{Columns.ResourceRequirement.RequirementId} = req.{Columns.Requirement.Id}
									order by {Columns.Resource.Title}";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => ResourceMappers.GetResource(reader, columnIndexes, ResourceList));
		return data;
	}


	public async Task<List<Resource>> FindResourceAsync(string input)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"SELECT r.{Columns.Resource.Id}, rr.{Columns.ResourceRequirement.Amount}, req.{Columns.Requirement.Title}, r.{Columns.Resource.Title}, req.{Columns.Requirement.Id} 
                    FROM {Tables.Resources} r
                    JOIN {Tables.ResourceRequirements} rr ON r.{Columns.Resource.Id} = rr.{Columns.ResourceRequirement.ResourceId} 
                    JOIN {Tables.Requirements} req ON rr.{Columns.ResourceRequirement.RequirementId} = req.{Columns.Requirement.Id}
                    where {Columns.Resource.Title}=@input
                    order by {Columns.ResourceRequirement.Amount} desc";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter("@input", NpgsqlTypes.NpgsqlDbType.Text) { Value = input }
		};
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetResource(columnIndexes, ResourceList),
			parameters);
		return data;
	}


	public async Task<List<Unit>> GetUnitsWithResourceAsync(int resId)
	{
		List<Unit> UnitList= new List<Unit>();
		var query = $@"SELECT u.{Columns.Unit.Id}, u.{Columns.Unit.Title}
									FROM {Tables.Units} u
									JOIN {Tables.UnitResources} ur ON u.{Columns.Unit.Id} = ur.{Columns.UnitResource.UnitId}									
									where {Columns.UnitResource.ResourceId}={resId}";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => reader.GetPlainUnit(columnIndexes, UnitList));
		return data;
	}

	public async Task<List<Resource>> SearchResourcesAsync(string input)
	{
		List<Resource> ResourceList = new List<Resource>();
		var query = $@"select r.{Columns.Resource.Id}, rr.{Columns.ResourceRequirement.Amount}, req.{Columns.Requirement.Title}, r.{Columns.Resource.Title}, req.{Columns.Requirement.Id}
                    FROM {Tables.Resources} r
                    JOIN {Tables.ResourceRequirements} rr ON r.{Columns.Resource.Id} = rr.{Columns.ResourceRequirement.ResourceId} 
                    JOIN {Tables.Requirements} req ON rr.{Columns.ResourceRequirement.RequirementId} = req.{Columns.Requirement.Id}
                    WHERE lower({Columns.Resource.Title}) like lower(@input)
                    order by {Columns.Resource.Title}";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter("@input", NpgsqlTypes.NpgsqlDbType.Text) { Value = input.ToLower() + "%" }
		};
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetResource(columnIndexes, ResourceList),
			parameters);
		return data;
	}


	public async Task InsertResourceAsync(string resourceName, int requirementId, int amount)
	{
		var resourceValues = new Dictionary<string, object>()
		{
			{ Columns.Resource.Title, resourceName }
		};
		var resourceId = await dbProvider.InsertItemAsync(Tables.Resources, resourceValues, Columns.Resource.Id);

		var resourceRequirementValues = new Dictionary<string, object>()
		{
			{ Columns.ResourceRequirement.ResourceId, resourceId },
			{ Columns.ResourceRequirement.RequirementId, requirementId },
			{ Columns.ResourceRequirement.Amount, amount }
		};

		await dbProvider.InsertItemAsync(Tables.ResourceRequirements, resourceRequirementValues);
	}

	public async Task DeleteResourceAsync(string input)
	{
		var query = $"DELETE FROM {Tables.Resources} WHERE {Columns.Resource.Title}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}
	public async Task DeleteRequirementFromListAsync(int resourceId, int requirementId)
	{
		var query = $"DELETE FROM {Tables.ResourceRequirements}" +
											$" WHERE {Columns.ResourceRequirement.ResourceId}={resourceId}" +
											$" and {Columns.ResourceRequirement.RequirementId}={requirementId}";
		await dbProvider.QueryExecuteAsync(query);
	}

	public async Task AlterAmountAsync(int resourceId, int requirementId, int newAmount)
	{
		var query =
			$"update {Tables.ResourceRequirements} set {Columns.ResourceRequirement.Amount}= {newAmount}" +
			$" where {Columns.ResourceRequirement.RequirementId}={requirementId}" +
			$" and {Columns.ResourceRequirement.ResourceId}={resourceId}";
		await dbProvider.QueryExecuteAsync(query);
	}

	public async Task AddItemToList(int resourceId, int requirementId, int amount)
	{
		var query = $"insert into {Tables.ResourceRequirements}" +
		            $"({Columns.ResourceRequirement.ResourceId}, {Columns.ResourceRequirement.RequirementId}, {Columns.ResourceRequirement.Amount})" +
		            $" values ({resourceId},{requirementId},{amount})";
		await dbProvider.QueryExecuteAsync(query);
	}
}