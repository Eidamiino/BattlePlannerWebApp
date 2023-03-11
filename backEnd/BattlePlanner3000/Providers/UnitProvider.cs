using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class UnitProvider
{
	public static List<Unit> UnitList { get; private set; } = new List<Unit>();
	public UnitProvider()
	{
		
	}

	public List<Unit> GetUnits()
	{
		return UnitList;
	}
	public void AddUnit(Unit unit)
	{
		UnitList.Add(unit);
	}
	public Unit FindUnit(string name)
	{
		return UnitList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
	public void DeleteUnit(string name)
	{
		var unit= UnitList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		UnitList.Remove(unit);
	}
	public List<Unit> SearchUnits(string query)
	{
		return UnitList.Where(item => item.Name.StartsWith(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
	}
}