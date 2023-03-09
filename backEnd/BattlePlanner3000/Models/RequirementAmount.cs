namespace BattlePlanner3000.Models;

public class RequirementAmount
{
	public int Amount { get; set; }
	public Requirement Requirement { get; set; }

	public RequirementAmount(Requirement requirement, int amount)
	{
		Amount = amount;
		Requirement = requirement;
	}
}