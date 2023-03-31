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

		public async Task<List<T>> QueryGetDataAsync<T>(string query, Func<IDataReader, T> mapper)
		{
			var connection = await OpenConnectionAsync();
			var command = new NpgsqlCommand(query, connection);
			var dataReader = await command.ExecuteReaderAsync();

			var result = dataReader.Select(mapper).ToList();
			return result;
		}
		//genericka query
		public async Task<List<T>> GetAllItemsAsync<T>(string tableName, Func<IDataReader,T> mapper)
		{
			var query= $"SELECT * FROM {tableName}";
			return await QueryGetDataAsync(query,mapper);
		}

		public async Task<int> InsertItemAsync(string tableName, Dictionary<string, object> values)
		{
			var connection = await OpenConnectionAsync();
			var columns = string.Join(",", values.Keys);
			var parameters = string.Join(",", values.Keys.Select(x => $"@{x}"));
			var commandText = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

			var command = new NpgsqlCommand(commandText, connection);
			{
				foreach (var kvp in values)
				{
					command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
				}

				return await command.ExecuteNonQueryAsync();
			}

		}
		public async Task<int> DeleteItemAsync(string tableName, string col, string query)
		{
			var connection = await OpenConnectionAsync();
			var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE {col}='{query}'", connection);
			var rowsAffected = await command.ExecuteNonQueryAsync();

			return rowsAffected;
		}
		public async Task<IDataReader> GetAllItemsMtoN(string query)
		{
			var connection = await OpenConnectionAsync();
			var command = new NpgsqlCommand(query, connection);
			var dataReader = await command.ExecuteReaderAsync();
			return dataReader;
		}
		// public async Task<IDataReader> GetAllItemsAsync(string tableName)
		// {
		// 	var connection = await OpenConnectionAsync();
		// 	var command = new NpgsqlCommand($"SELECT * FROM {tableName}", connection);
		// 	var reader = await command.ExecuteReaderAsync();
		//
		// 	return reader;
		// }

	}
}
