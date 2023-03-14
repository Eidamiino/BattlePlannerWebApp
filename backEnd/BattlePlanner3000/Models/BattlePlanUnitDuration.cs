namespace BattlePlanner3000.Models
{
	public class BattlePlanUnitDuration
	{
		public BattlePlanUnitDuration(string PlanName, string UnitName, int UnitCapacity)
		{
			this.BattlePlan= new BattlePlan(PlanName, new List<Unit>(), UnitCapacity);
			this.Unit= new Unit(UnitName, new List<ResourceAmount>());
		}

		public BattlePlan BattlePlan{ get; set; }
		public Unit Unit{ get; set; }

	}
}
