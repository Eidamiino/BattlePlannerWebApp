using BattlePlanner3000.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Dynamic;

namespace BattlePlanner3000.Providers
{
	public class DbProvider
	{
		private readonly string connectionString;

		public DbProvider(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString(ConnectionStrings.WebApiDatabase);
		}

		private async Task<NpgsqlConnection> OpenConnectionAsync()
		{
			var connection = new NpgsqlConnection(connectionString);
			await connection.OpenAsync();
			return connection;
		}

		public async Task<List<T>> QueryGetDataAsync<T>(string query, Func<IDataReader, Dictionary<string, int>, T> mapper)
		{
			await using var connection = await OpenConnectionAsync();
			var command = new NpgsqlCommand(query, connection);
			await using var dataReader = await command.ExecuteReaderAsync();

			var columns = dataReader.GetColumnSchema();
			var columnIndexes = columns.ToDictionary(x => x.ColumnName.ToLower(), x => x.ColumnOrdinal ?? -1);

			var result = dataReader.Select(x => mapper(x, columnIndexes)).ToList();
			return result;
		}

		public async Task QueryExecuteAsync(string query)
		{
			await using var connection = await OpenConnectionAsync();
			var command = new NpgsqlCommand(query, connection);
			await command.ExecuteNonQueryAsync();
		}


		public async Task<object?> InsertItemAsync(string tableName, Dictionary<string, object> values, string returning="")
		{
			var connection = await OpenConnectionAsync();
			var columns = string.Join(",", values.Keys);
			var parameters = string.Join(",", values.Keys.Select(x => $"@{x}"));
			
			var commandText = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";
			if (returning != string.Empty) commandText += " returning " + returning;

			var command = new NpgsqlCommand(commandText, connection);
			{
				foreach (var kvp in values)
				{
					command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
				}

				return await command.ExecuteScalarAsync(); 
			}

		}
	}
}
