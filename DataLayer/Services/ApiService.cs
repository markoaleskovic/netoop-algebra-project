using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Services
{
	// Handles HTTP requests and JSON parsing from the World Cup API
	public class ApiService : IApiService
	{
		private readonly HttpClient _httpClient;
		private const string BaseUrl = "https://worldcup-vua.nullbit.hr";

		public ApiService()
		{
			_httpClient = new HttpClient();
		}

		private async Task<List<T>> GetAsync<T>(string endpoint)
		{
			try
			{
				var response = await _httpClient.GetAsync(BaseUrl + endpoint);
				response.EnsureSuccessStatusCode();
				var json = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			catch (HttpRequestException ex)
			{
				LogError(ex);
				throw new ApplicationException("Network error occurred while fetching data.");
			}
			catch (JsonException ex)
			{
				LogError(ex);
				throw new ApplicationException("Error parsing server response.");
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public Task<List<Team>> GetTeamsAsync(string gender)
			=> GetAsync<Team>($"/{gender}/teams");

		public Task<List<Team>> GetTeamsResultsAsync(string gender)
			=> GetAsync<Team>($"/{gender}/teams/results");

		public Task<List<GroupResult>> GetTeamsGroupResultsAsync(string gender)
			=> GetAsync<GroupResult>($"/{gender}/teams/group_results");

		public Task<List<Match>> GetMatchesAsync(string gender)
			=> GetAsync<Match>($"/{gender}/matches");

		public Task<List<Match>> GetMatchesByCountryAsync(string gender, string fifaCode)
			=> GetAsync<Match>($"/{gender}/matches/country?fifa_code={fifaCode}");

		private void LogError(Exception ex)
		{
			File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
		}
	}
}
