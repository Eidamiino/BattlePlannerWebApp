using System.Data;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;

namespace BattlePlanner3000.Mappers;

public static class ResourceMappers
{
	public static Resource GetResource(this IDataReader reader, Dictionary<string, int> columnIndexes, List<Resource> resources)
	{
		var title = reader.GetString(columnIndexes[Columns.Resource.Title]);

		var existingResource = resources.FirstOrDefault(r => r.Name== title);
		if (existingResource != null)
		{
			var requirementAmount = new RequirementAmount(new Requirement(reader.GetInt32(columnIndexes[Columns.Requirement.Id]),reader.GetString(columnIndexes[Columns.Requirement.Title])), reader.GetInt32(columnIndexes[Columns.ResourceRequirement.Amount]));
			existingResource.RequirementList.Add(requirementAmount);
			return existingResource;
		}
		else
		{
			var resourceId = reader.GetInt32(columnIndexes[Columns.Resource.Id]);

			var resource = new Resource(resourceId,title, new List<RequirementAmount>());
			var requirementAmount = new RequirementAmount(new Requirement(reader.GetInt32(columnIndexes[Columns.Requirement.Id]), reader.GetString(columnIndexes[Columns.Requirement.Title])), reader.GetInt32(columnIndexes[Columns.ResourceRequirement.Amount]));
			resource.RequirementList.Add(requirementAmount);
			resources.Add(resource);
			return resource;
		}
	}
	public static Resource GetResourceNoList(this IDataReader reader, Dictionary<string, int> columnIndexes)
	{
		var resource= new Resource(reader.GetInt32(columnIndexes[Columns.Resource.Id]), reader.GetString(columnIndexes[Columns.Resource.Title]),new List<RequirementAmount>());
		return resource;


		// var title = reader.GetString(columnIndexes[Columns.Resource.Title]);
		//
		// var existingResource = resources.FirstOrDefault(r => r.Name == title);
		// if (existingResource != null)
		// {
		// 	var requirementAmount = new RequirementAmount(new Requirement(reader.GetInt32(columnIndexes[Columns.Requirement.Id]), reader.GetString(columnIndexes[Columns.Requirement.Title])), reader.GetInt32(columnIndexes[Columns.ResourceRequirement.Amount]));
		// 	existingResource.RequirementList.Add(requirementAmount);
		// 	return existingResource;
		// }
		// else
		// {
		// 	var resourceId = reader.GetInt32(columnIndexes[Columns.Resource.Id]);
		//
		// 	var resource = new Resource(resourceId, title, new List<RequirementAmount>());
		// 	var requirementAmount = new RequirementAmount(new Requirement(reader.GetInt32(columnIndexes[Columns.Requirement.Id]), reader.GetString(columnIndexes[Columns.Requirement.Title])), reader.GetInt32(columnIndexes[Columns.ResourceRequirement.Amount]));
		// 	resource.RequirementList.Add(requirementAmount);
		// 	resources.Add(resource);
		// 	return resource;
		// }
	}
}