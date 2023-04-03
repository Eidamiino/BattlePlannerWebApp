using System.Data;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;

namespace BattlePlanner3000.Mappers;

public static class ResourceMappers
{
	public static Resource GetResource(this IDataReader reader, Dictionary<string, int> columnIndexes, List<Resource> resources)
	{
		var title = reader.GetString(columnIndexes[Columns.Resource.Title]);
		
		var existingResource = resources.FirstOrDefault(r => r.Name == title);
		if (existingResource != null)
		{
			var requirementAmount = new RequirementAmount(new Requirement(reader.GetString(columnIndexes[Columns.Requirement.Title])), reader.GetInt32(columnIndexes[Columns.ResourceRequirement.Amount]));
			existingResource.RequirementList.Add(requirementAmount);
			return existingResource;
		}
		else
		{
			var resource = new Resource(title, new List<RequirementAmount>());
			var requirementAmount = new RequirementAmount(new Requirement(reader.GetString(columnIndexes[Columns.Requirement.Title])), reader.GetInt32(columnIndexes[Columns.ResourceRequirement.Amount]));
			resource.RequirementList.Add(requirementAmount);
			resources.Add(resource);
			return resource;
		}
	}
}