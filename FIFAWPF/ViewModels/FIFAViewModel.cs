using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Services;
using System.Windows;
using System.Threading.Tasks;
using System.Globalization;
using System;
using System.IO;

namespace FIFAWPF.ViewModels
{
	public class FIFAViewModel : INotifyPropertyChanged
	{
		private List<Team> _teams;
		private Team _selectedTeamLeft;
		private Team _selectedTeamRight;
		private bool _isRightComboBoxEnabled;

		private bool _isLoading;
		private readonly string _favTeamFile = "favorite_team.txt";

		private List<Player> _leftTeamStartingPlayers;
		private List<Player> _rightTeamStartingPlayers;
		private List<Player> _leftTeamSubstitutes;
		private List<Player> _rightTeamSubstitutes;

		private string _scoreDisplay = "0:0";
		private string _leftTeamFormation;
		private string _rightTeamFormation;

		private AppConfig.WindowSize _currentWindowSize = AppConfig.WindowSize.Medium;
		public AppConfig.WindowSize CurrentWindowSize
		{
			get => _currentWindowSize;
			set
			{
				if (_currentWindowSize != value)
				{
					_currentWindowSize = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _noMatchFound;
		public bool NoMatchFound
		{
			get => _noMatchFound;
			set { _noMatchFound = value; OnPropertyChanged(); }
		}

		public List<Team> TeamsForLeftComboBox { get; set; } = new List<Team>();
		public List<Team> TeamsForRightComboBox { get; set; } = new List<Team>();

		private readonly WorldCupRepository _repository;

		public string ScoreDisplay
		{
			get => _scoreDisplay;
			set
			{
				_scoreDisplay = value;
				OnPropertyChanged();
			}
		}

		public Team SelectedTeam { get; set; }

		public bool IsRightComboBoxEnabled
		{
			get => _isRightComboBoxEnabled;
			set
			{
				_isRightComboBoxEnabled = value;
				OnPropertyChanged();
			}
		}

		public Team SelectedTeamLeft
		{
			get => _selectedTeamLeft;
			set
			{
				if (_selectedTeamLeft == value) return;
				_selectedTeamLeft = value;
				OnPropertyChanged();
				UpdateScore();
				LeftTeamStartingPlayers = null;
				LeftTeamFormation = null;
				LeftTeamSubstitutes = null;
				UpdateFilteredTeams();
			}
		}

		public Team SelectedTeamRight
		{
			get => _selectedTeamRight;
			set
			{
				if (_selectedTeamRight == value) return;
				_selectedTeamRight = value;
				OnPropertyChanged();
				UpdateScore();
				RightTeamStartingPlayers = null;
				RightTeamFormation = null;
				RightTeamSubstitutes = null;
				UpdateFilteredTeams();
			}
		}


		public bool IsLoading
		{
			get => _isLoading;
			set { _isLoading = value; OnPropertyChanged(); }
		}

		public List<Player> LeftTeamStartingPlayers
		{
			get => _leftTeamStartingPlayers;
			set { _leftTeamStartingPlayers = value; OnPropertyChanged(); }
		}

		public List<Player> RightTeamStartingPlayers
		{
			get => _rightTeamStartingPlayers;
			set { _rightTeamStartingPlayers = value; OnPropertyChanged(); }
		}

		public List<Player> LeftTeamSubstitutes
		{
			get => _leftTeamSubstitutes;
			set { _leftTeamSubstitutes = value; OnPropertyChanged(); }
		}

		public List<Player> RightTeamSubstitutes
		{
			get => _rightTeamSubstitutes;
			set { _rightTeamSubstitutes = value; OnPropertyChanged(); }
		}

		public string LeftTeamFormation
		{
			get => _leftTeamFormation;
			set { _leftTeamFormation = value; OnPropertyChanged(); }
		}

		public string RightTeamFormation
		{
			get => _rightTeamFormation;
			set { _rightTeamFormation = value; OnPropertyChanged(); }
		}

		public event Action ValidMatchFound;

		public FIFAViewModel()
		{
			try
			{
				_repository = new WorldCupRepository(new ApiService(), new FileService());
				TeamsForLeftComboBox = [];
				TeamsForRightComboBox = [];
				IsRightComboBoxEnabled = false;
				LoadTeams();
				if (TeamsForLeftComboBox?.Any() == true)
					SelectedTeamLeft = TeamsForLeftComboBox.First();

				if (TeamsForRightComboBox?.Any() == true)
					SelectedTeamRight = TeamsForRightComboBox.First();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load teams. Please check the logs for more details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		public async void LoadTeams()
		{
			IsLoading = true;
			try
			{
				_teams = await _repository.GetTeamsAsync("men", true);

				// Try to load favorite team
				string favTeamName = null;
				if (File.Exists(_favTeamFile))
				{
					try
					{
						favTeamName = File.ReadAllText(_favTeamFile);
					}
					catch (Exception ex)
					{
						FileService.LogError(ex);
					}
				}

				if (!string.IsNullOrEmpty(favTeamName))
				{
					var favTeam = _teams.FirstOrDefault(t => t.Country.Equals(favTeamName, StringComparison.OrdinalIgnoreCase));
					if (favTeam != null)
					{
						_selectedTeamLeft = favTeam; // Set directly to avoid save
						OnPropertyChanged(nameof(SelectedTeamLeft));
					}
					else
					{
						_selectedTeamLeft = _teams.FirstOrDefault();
						OnPropertyChanged(nameof(SelectedTeamLeft));
					}
				}
				else
				{
					_selectedTeamLeft = _teams.FirstOrDefault();
					OnPropertyChanged(nameof(SelectedTeamLeft));
				}

				SelectedTeamRight = _teams.Skip(1).FirstOrDefault();
				UpdateFilteredTeams();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load teams: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				_selectedTeamLeft = _teams?.FirstOrDefault()!;
				SelectedTeamRight = _teams?.Skip(1).FirstOrDefault()!;
			}
			finally 
			{ 
				IsLoading = false;
			}
		}

		private async void UpdateFilteredTeams()
		{
			TeamsForLeftComboBox = _teams.ToList();
			OnPropertyChanged(nameof(TeamsForLeftComboBox));

			if (SelectedTeamLeft != null)
			{
				IsLoading = true;
				try
				{
					// Get all matches for the selected left team
					var matches = await _repository.GetMatchesByCountryAsync("men", SelectedTeamLeft.Fifa_Code, useCache: true);
					
					// Get all teams that have played against the selected left team
					var teamsWithMatches = matches
						.Select(m => m.Home_Team_Country == SelectedTeamLeft.Country ? m.Away_Team_Country : m.Home_Team_Country)
						.Distinct()
						.ToList();

					// Filter the teams list to only include teams that have matches with the selected left team
					TeamsForRightComboBox = _teams
						.Where(team => teamsWithMatches.Contains(team.Country))
						.ToList();

					IsRightComboBoxEnabled = true;

					// If current right selection is not in the filtered list, select the first available team
					if (SelectedTeamRight != null && !TeamsForRightComboBox.Any(t => t.Fifa_Code == SelectedTeamRight.Fifa_Code))
					{
						SelectedTeamRight = TeamsForRightComboBox.FirstOrDefault();
					}
					else if (SelectedTeamRight == null)
					{
						SelectedTeamRight = TeamsForRightComboBox.FirstOrDefault();
					}
				}
				catch (Exception ex)
				{
					FileService.LogError(ex);
					MessageBox.Show("Failed to update team list: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					TeamsForRightComboBox = new List<Team>();
					SelectedTeamRight = null;
				}
				finally
				{
					IsLoading = false;
				}
			}
			else
			{
				TeamsForRightComboBox = new List<Team>();
				SelectedTeamRight = null;
				IsRightComboBoxEnabled = false;
			}

			OnPropertyChanged(nameof(TeamsForRightComboBox));
		}

		private async void UpdateScore()
		{
			try
			{
				if (SelectedTeamLeft == null || SelectedTeamRight == null)
				{
					ScoreDisplay = "0:0";
					return;
				}
				IsLoading = true;
				try
				{
					MatchScore matchscore = null;
					try
					{
						matchscore = await _repository.GetMatchScoreAsync(SelectedTeamLeft, SelectedTeamRight, "men");
					}
					catch (Exception ex)
					{
						FileService.LogError(ex);
						MessageBox.Show("Failed to load match data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					if (matchscore == null)
					{
						ScoreDisplay = "0:0";
						return;
					}
					ScoreDisplay = $"{matchscore.LeftScore}:{matchscore.RightScore}";
					// Do not load formations/players here
					ValidMatchFound?.Invoke();
				}
				catch (Exception ex)
				{
					FileService.LogError(ex);
					MessageBox.Show("Failed to update score: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					ScoreDisplay = "0:0";
				}
				finally { IsLoading = false; }
			}
			catch (Exception e)
			{
				MessageBox.Show("An error occurred while updating the score: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		public async Task LoadLeftFormation()
		{
			if (SelectedTeamLeft == null || SelectedTeamRight == null) return;
			IsLoading = true;
			try
			{
				var match = await Task.Run(() => GetMatchForTeams(SelectedTeamLeft, SelectedTeamRight).Result);
				if (match == null)
				{
					LeftTeamStartingPlayers = null;
					LeftTeamFormation = null;
					LeftTeamSubstitutes = null;
					NoMatchFound = true;
					return;
				}
				NoMatchFound = false;
				var homeStats = match.Home_Team_Country == SelectedTeamLeft.Country ? match.Home_Team_Statistics : match.Away_Team_Statistics;
				
				Application.Current.Dispatcher.Invoke(() =>
				{
					LeftTeamStartingPlayers = homeStats?.Starting_Eleven?.ToList();
					LeftTeamSubstitutes = homeStats?.Substitutes?.ToList();
					LeftTeamFormation = homeStats?.Tactics ?? "4-4-2"; // Default formation if none specified
				});
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load left formation: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				LeftTeamStartingPlayers = null;
				LeftTeamFormation = null;
				LeftTeamSubstitutes = null;
			}
			finally { IsLoading = false; }
		}
		
		public void LoadSubstitues(bool isLeftTeam)
		{
			// This method is now just a wrapper that calls the appropriate formation method
			// since we're loading substitutes along with formations
			if (isLeftTeam)
			{
				LoadLeftFormation();
			}
			else
			{
				LoadRightFormation();
			}
		}

		public async Task LoadRightFormation()
		{
			if (SelectedTeamLeft == null || SelectedTeamRight == null) return;
			IsLoading = true;
			try
			{
				var match = await Task.Run(() => GetMatchForTeams(SelectedTeamLeft, SelectedTeamRight).Result);
				if (match == null)
				{
					RightTeamStartingPlayers = null;
					RightTeamFormation = null;
					RightTeamSubstitutes = null;
					NoMatchFound = true;
					return;
				}
				NoMatchFound = false;
				var awayStats = match.Home_Team_Country == SelectedTeamLeft.Country ? match.Away_Team_Statistics : match.Home_Team_Statistics;
				
				Application.Current.Dispatcher.Invoke(() =>
				{
					RightTeamStartingPlayers = awayStats?.Starting_Eleven?.ToList();
					RightTeamSubstitutes = awayStats?.Substitutes?.ToList();
					if (RightTeamSubstitutes != null)
					{
						foreach (var sub in RightTeamSubstitutes)
						{
							System.Diagnostics.Debug.WriteLine($"Sub: {sub.Name} #{sub.Shirt_Number}");
						}
					}
					RightTeamFormation = awayStats?.Tactics ?? "4-4-2"; // Default formation if none specified
				});
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load right formation: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				RightTeamStartingPlayers = null;
				RightTeamFormation = null;
				RightTeamSubstitutes = null;
			}
			finally { IsLoading = false; }
		}

		public async Task<TeamStat> GetTeamStatistics(Team team)
		{
			IsLoading = true;
			try
			{
				TeamStat stat = null;
				var matches = await _repository.GetMatchesByCountryAsync("men", team.Fifa_Code, useCache: true);
				var totalgames = matches.Count(m => m.Home_Team_Country == team.Country || m.Away_Team_Country == team.Country);
				var wins = matches.Count(m => m.Winner == team.Country);
				var losses = matches.Count(m => (m.Home_Team_Country == team.Country && m.Winner != team.Country && m.Winner != "draw") ||
				                                (m.Away_Team_Country == team.Country && m.Winner != team.Country && m.Winner != "draw"));
				var draws = matches.Count(m => m.Winner == "draw" && (m.Home_Team_Country == team.Country || m.Away_Team_Country == team.Country));
				var goalsScored = matches.Where(m => m.Home_Team_Country == team.Country).Sum(m => m.Home_Team.Goals) +
								  matches.Where(m => m.Away_Team_Country == team.Country).Sum(m => m.Away_Team.Goals) ?? 0;
				var goalsConceded = matches.Where(m => m.Home_Team_Country == team.Country).Sum(m => m.Away_Team.Goals) +
					matches.Where(m => m.Away_Team_Country == team.Country).Sum(m => m.Home_Team.Goals) ?? 0;
				var goalDifference = goalsScored - goalsConceded;

				return stat = new TeamStat
				{
					TotalGames = totalgames,
					Wins = wins,
					Losses = losses,
					Draws = draws,
					GoalsScored = goalsScored,
					GoalsConceded = goalsConceded,
					GoalDifference = goalDifference
				};
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show(ScoreDisplay = "Failed to load team statistics: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			}
			finally { IsLoading = false; }
		}

		public async Task<Match> GetMatchForTeams(Team left, Team right)
		{
			IsLoading = true;
			try
			{
				var matches = await _repository.GetMatchesByCountryAsync("men", left.Fifa_Code, useCache: true);
				return matches.FirstOrDefault(m =>
					(m.Home_Team_Country == left.Country && m.Away_Team_Country == right.Country) ||
					(m.Home_Team_Country == right.Country && m.Away_Team_Country == left.Country));
			}
			finally { IsLoading = false; }
		}

		public class PlayerOnField
		{
			public Player Player { get; set; }
			public double X { get; set; } // 0-1 relative to field width
			public double Y { get; set; } // 0-1 relative to field height
		}

		public static List<PlayerOnField> GetPlayerPositions(List<Player> startingEleven, string tactics, bool isLeftTeam)
		{
			var result = new List<PlayerOnField>();
			if (startingEleven == null || startingEleven.Count == 0 || string.IsNullOrWhiteSpace(tactics)) return result;
			var formation = tactics.Split('-').Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToList();
			int playerIndex = 0;
			// Goalkeeper always first
			result.Add(new PlayerOnField
			{
				Player = startingEleven[playerIndex++],
				X = isLeftTeam ? 0.08 : 0.92,
				Y = 0.5
			});
			// Outfield rows (from defense to attack)
			int rows = formation.Count;
			for (int row = 0; row < rows; row++)
			{
				int playersInRow = formation[row];
				for (int i = 0; i < playersInRow; i++)
				{
					double x = isLeftTeam ? 0.18 + row * 0.18 : 0.82 - row * 0.18;
					double y = playersInRow == 1 ? 0.5 : 0.15 + i * (0.7 / (playersInRow - 1));
					result.Add(new PlayerOnField
					{
						Player = startingEleven[playerIndex++],
						X = x,
						Y = y
					});
				}
			}
			return result;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public async Task<PlayerRanking> GetPlayerInfo(Player player)
		{
			if (SelectedTeam == null || player == null)
				return null;

			// Get all player rankings for the selected team
			var rankings = await _repository.GetPlayerRankingsAsync(SelectedTeam, "men");

			// Find the ranking for the specific player (case-insensitive match on name)
			var playerRanking = rankings
				.FirstOrDefault(r => string.Equals(r.Name, player.Name, StringComparison.OrdinalIgnoreCase));

			// If not found, return a default PlayerRanking with zero stats
			return playerRanking ?? new PlayerRanking { Name = player.Name, Goals = 0, Yellow_Cards = 0 };
		}

		public void SaveFavoriteTeam()
		{
			try
			{
				if (SelectedTeamLeft != null)
				{
					File.WriteAllText(_favTeamFile, SelectedTeamLeft.Country);
				}
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to save favorite team: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}

	public class TeamStat
	{
		public int TotalGames { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public int Draws { get; set; }
		public int GoalsScored { get; set; }
		public int GoalsConceded { get; set; }
		public int GoalDifference { get; set; }
	}
}