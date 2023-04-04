namespace BattlePlanner3000.Models;

public class Requirement
{
	public int Id { get; set; }
	public string Name { get; set; }

	public Requirement(int id,string name)
	{
		Id = id;
		Name = name;
	}

}