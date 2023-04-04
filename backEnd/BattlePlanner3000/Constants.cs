namespace BattlePlanner3000
{
	public static class Tables
	{
		public const string Requirements = "requirement";
		public const string Resources = "resource";
		public const string ResourceRequirements = "resource_requirement";
	}
	public static class Columns
	{
		public static class Requirement
		{
			public const string Id = "requirement_id";
			public const string Title = "title";
		}
		public static class Resource
		{
			public const string Id = "resource_id";
			public const string Title = "title_resource";
		}
		public static class ResourceRequirement
		{
			public const string Amount= "amount";
			public const string RequirementId = "requirement_id";
			public const string ResourceId= "resource_id";
		}
	}
}
