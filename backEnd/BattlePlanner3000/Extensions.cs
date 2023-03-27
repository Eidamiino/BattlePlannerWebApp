﻿using System.Data;

namespace BattlePlanner3000
{
	public static class Extensions
	{
		public static IEnumerable<T> Select<T>(this IDataReader reader,
			Func<IDataReader, T> projection)
		{
			while (reader.Read())
			{
				yield return projection(reader);
			}
		}
	}
}
