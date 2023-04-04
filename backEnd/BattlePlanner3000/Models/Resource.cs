namespace BattlePlanner3000.Models;

public class Resource
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<RequirementAmount> RequirementList { get; set; }

	public Resource(int id,string name, List<RequirementAmount> requirementList)
	{
		Id = id;
		Name = name;
		RequirementList = requirementList;
	}

}