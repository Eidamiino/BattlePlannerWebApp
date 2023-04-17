namespace BattlePlanner3000.Models;

public class RequirementAmountTotal
{
	public int Amount { get; set; }
	public int TotalAmount { get; set; }
	public Requirement Requirement { get; set; }

	public RequirementAmountTotal(Requirement requirement, int amount, int totalAmount)
	{
		Amount = amount;
		Requirement = requirement;
		TotalAmount = totalAmount;
	}
}