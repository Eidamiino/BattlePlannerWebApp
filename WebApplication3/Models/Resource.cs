namespace WebApplication3.Models;

public class Resource
{
	public string Name { get; set; }
	public List<RequirementAmount> RequirementList { get; set; }

	public Resource(string name, List<RequirementAmount> requirementList)
	{
		Name = name;
		RequirementList = requirementList;
	}

}