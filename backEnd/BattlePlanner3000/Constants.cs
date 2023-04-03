namespace BattlePlanner3000
{
	public class Constants
	{
		public static readonly string RequirementsTable= "requirement";
		public static readonly string RequirementsSearchCol= "title";
		public static readonly string ResourcesTable= "resource";
		public static readonly string ResourcesSearchCol = "title";
	}

	public static class Tables
	{
		public const string Requirements = "requirement";
		public const string Resources = "resource";
	}
	public static class Columns
	{
		public static class Requirement
		{
			public const string Title = "title";
		}
		public static class Resource
		{
			public const string Title = "title_resource";
		}
		public static class ResourceRequirement
		{
			public const string Amount= "amount";
		}
	}
}
