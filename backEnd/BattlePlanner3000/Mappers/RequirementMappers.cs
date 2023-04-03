using System.Data;
using BattlePlanner3000.Models;

namespace BattlePlanner3000.Mappers;

public static class RequirementMappers
{
	public static Requirement GetRequirement(this IDataReader reader, Dictionary<string,int> columnIndexes)
	{
		var requirement = new Requirement(reader.GetString(columnIndexes[Columns.Requirement.Title]));
		return requirement;
	}
}