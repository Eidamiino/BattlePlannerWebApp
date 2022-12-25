namespace WebApplication3.Models;

public class Resource
{
	public string Name { get; set; }
	public Dictionary<Requirement,int> RequirementList { get; set; }

	public Resource(string name, Dictionary<Requirement,int> requirementList)
	{
		Name = name;
		RequirementList = requirementList;
	}

}