using DataLayer.Interfaces;
using DataLayer.Models;
using Match = DataLayer.Models.Match;

namespace DataLayer.Repository
{
	//Central point for all data operations, combining API and file services
	public class WorldCupRepository : IRepository
	{
		private readonly IApiService _apiService;
		private readonly IFileService _fileService;
		public WorldCupRepository(IApiService apiService, IFileService fileService)
		{
			_apiService = apiService;
			_fileService = fileService;
		}

		public async Task<List<Team>> GetTeamsAsync(string gender, bool useCache = false)
		{
			var filePath = $"{gender}_teams.json";
			try
			{
				if (useCache && _fileService.FileExists(filePath))
					return _fileService.LoadFromFile<Team>(filePath);

				var teams = await _apiService.GetTeamsAsync(gender);
				_fileService.SaveToFile(filePath, teams);
				return teams;
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public async Task<List<Team>> GetTeamsResultsAsync(string gender, bool useCache = false)
		{
			var filePath = $"{gender}_teams_results.json";
			try
			{
				if (useCache && _fileService.FileExists(filePath))
					return _fileService.LoadFromFile<Team>(filePath);

				var teams = await _apiService.GetTeamsResultsAsync(gender);
				_fileService.SaveToFile(filePath, teams);
				return teams;
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public async Task<List<GroupResult>> GetTeamsGroupResultsAsync(string gender, bool useCache = false)
		{
			var filePath = $"{gender}_teams_group_results.json";
			try
			{
				if (useCache && _fileService.FileExists(filePath))
					return _fileService.LoadFromFile<GroupResult>(filePath);

				var groupResults = await _apiService.GetTeamsGroupResultsAsync(gender);
				_fileService.SaveToFile(filePath, groupResults);
				return groupResults;
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public async Task<List<Match>> GetMatchesAsync(string gender, bool useCache = false)
		{
			var filePath = $"{gender}_matches.json";
			try
			{
				if (useCache && _fileService.FileExists(filePath))
					return _fileService.LoadFromFile<Match>(filePath);

				var matches = await _apiService.GetMatchesAsync(gender);
				_fileService.SaveToFile(filePath, matches);
				return matches;
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public async Task<List<Match>> GetMatchesByCountryAsync(string gender, string fifaCode, bool useCache = false)
		{
			var filePath = $"{gender}_matches_{fifaCode}.json";
			try
			{
				if (useCache && _fileService.FileExists(filePath))
					return _fileService.LoadFromFile<Match>(filePath);

				var matches = await _apiService.GetMatchesByCountryAsync(gender, fifaCode);
				_fileService.SaveToFile(filePath, matches);
				return matches;
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public void SaveData<T>(string filePath, List<T> data)
		{
			try
			{
				_fileService.SaveToFile(filePath, data);
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public List<T> LoadData<T>(string filePath)
		{
			try
			{
				return _fileService.LoadFromFile<T>(filePath);
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		private void LogError(Exception ex)
		{
			File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
		}

		public async Task<List<PlayerRanking>> GetPlayerRankingsAsync(Team team, string gender)
		{
			var rankings = new Dictionary<string, PlayerRanking>(StringComparer.OrdinalIgnoreCase);

			try
			{
				// Retrieve all matches for the team
				var matches = await GetMatchesByCountryAsync(gender, team.Fifa_Code, useCache: true);

				foreach (var match in matches)
				{
					// Determine if the team is home or away in this match
					bool isHome = string.Equals(match.Home_Team_Country, team.Country, StringComparison.OrdinalIgnoreCase);
					var events = isHome ? match.Home_Team_Events : match.Away_Team_Events;

					if (events == null) continue;

					foreach (var ev in events)
					{
						if (string.IsNullOrWhiteSpace(ev.Player)) continue;

						if (!rankings.TryGetValue(ev.Player, out var ranking))
						{
							ranking = new PlayerRanking { Name = ev.Player, Goals = 0, Yellow_Cards = 0 };
							rankings[ev.Player] = ranking;
						}

						// Count goals
						if (ev.Type_Of_Event.Equals("goal", StringComparison.OrdinalIgnoreCase) ||
							ev.Type_Of_Event.Equals("goal-penalty", StringComparison.OrdinalIgnoreCase) ||
							ev.Type_Of_Event.Equals("goal-own", StringComparison.OrdinalIgnoreCase))
						{
							ranking.Goals++;
						}

						// Count yellow cards
						if (ev.Type_Of_Event.Equals("yellow-card", StringComparison.OrdinalIgnoreCase) ||
							ev.Type_Of_Event.Equals("yellow-card-second", StringComparison.OrdinalIgnoreCase))
						{
							ranking.Yellow_Cards++;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			// Return rankings ordered by goals descending, then yellow cards descending
			return rankings.Values
				.OrderByDescending(r => r.Goals)
				.ThenByDescending(r => r.Yellow_Cards)
				.ToList();
		}

		public async Task<List<MatchAttendance>> GetMatchAttendanceRankingsAsync(Team team, string gender)
		{
			var attendances = new List<MatchAttendance>();

			// Retrieve all matches for the team
			var matches = await GetMatchesByCountryAsync(gender, team.Fifa_Code, useCache: true);

			try
			{
				foreach (var match in matches)
				{
					// Parse attendance as int, default to 0 if not valid
					int attendance = 0;
					int.TryParse(match.Attendance, out attendance);

					var matchAttendance = new MatchAttendance
					{
						Location = match.Location,
						Attendance = attendance,
						Home_Team = match.Home_Team_Country,
						Away_Team = match.Away_Team_Country
					};

					attendances.Add(matchAttendance);
				}
			}
			catch (Exception)
			{
				throw;
			}

			// Order by attendance descending
			return attendances
				.OrderByDescending(a => a.Attendance)
				.ToList();
		}


		public async Task<MatchScore> GetMatchScoreAsync(Team team1, Team team2, string gender)
		{
			try
			{
				var matches1 = await GetMatchesByCountryAsync(gender, team1.Fifa_Code, useCache: true);
				var matches2 = await GetMatchesByCountryAsync(gender, team2.Fifa_Code, useCache: true);

				var match = matches1.FirstOrDefault(m =>
					(m.Home_Team_Country == team1.Country && m.Away_Team_Country == team2.Country) ||
					(m.Home_Team_Country == team2.Country && m.Away_Team_Country == team1.Country));
				if (match == null)
				{
					return new MatchScore
					{
						LeftTeam = team1,
						RightTeam = team2,
						LeftScore = 0,
						RightScore = 0
					};
				}
				else
				{
					int? leftScore = match.Home_Team.Goals;
					int? rightScore = match.Away_Team.Goals;
					if (match.Home_Team_Country == team2.Country)
					{
						// Swap scores if team2 is home
						leftScore = match.Away_Team.Goals;
						rightScore = match.Home_Team.Goals;
					}
					return new MatchScore
					{
						LeftTeam = team1,
						RightTeam = team2,
						LeftScore = leftScore,
						RightScore = rightScore
					};
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
