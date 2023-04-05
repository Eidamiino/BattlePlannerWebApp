﻿namespace BattlePlanner3000
{
	public static class Tables
	{
		public const string Requirements = "requirement";
		public const string Resources = "resource";
		public const string ResourceRequirements = "resource_requirement";
		public const string Units = "unit";
		public const string UnitResources= "unit_resource";
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

		public static class Unit
		{
			public const string Id = "unit_id";
			public const string Title = "title_unit";
		}

		public static class ResourceRequirement
		{
			public const string ResourceId = "resource_id";
			public const string RequirementId = "requirement_id";
			public const string Amount = "amount";
		}

		public static class UnitResource
		{
			public const string UnitId= "unit_id";
			public const string ResourceId = "resource_id";
			public const string Amount = "amount";
		}
	}
}
