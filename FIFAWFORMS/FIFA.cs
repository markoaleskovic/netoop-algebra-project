using System.Data;
using DataLayer.Repository;
using DataLayer.Services;
using DataLayer.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Globalization;

namespace FIFAWFORMS
{
	public partial class FIFA : Form
	{
		private string league;
		private string language;

		private WorldCupRepository repository;

		private readonly string favTeamFile = "favorite_team.txt";
		private readonly string favPlayersFile = "favorite_players.txt";

		private List<Team> teams;
		private List<Player> selectedTeamPlayers;
		private Team selectedTeam;
		private HashSet<Player> favoritePlayers;

		public FIFA(string language, string league)
		{
			if (language == "croatian")
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("hr-HR");
				Thread.CurrentThread.CurrentCulture = new CultureInfo("hr-HR");
			}
			InitializeComponent();
			this.league = league;
			this.language = language;

			repository = new WorldCupRepository(new ApiService(), new FileService());
			favoritePlayers = new HashSet<Player>();
			flpTeamPlayers.AllowDrop = true;
			flpFavoritePlayers.AllowDrop = true;
		}

		private async void FIFA_Load(object sender, EventArgs e)
		{
			try
			{
				LoadTeams();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load the form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private async void LoadTeams()
		{
			try
			{
				ShowLoadingBar(true);
				teams = await repository.GetTeamsAsync(gender: league, useCache: true);

				// Clear before binding
				cmbTeams.Items.Clear();
				cmbTeams.DataSource = teams;
				cmbTeams.DisplayMember = "Country";

				// Load favorite team AFTER binding teams
				if (File.Exists(favTeamFile))
				{
					var favTeamName = File.ReadAllText(favTeamFile);
					var favTeam = teams.FirstOrDefault(t => t.Country.Equals(favTeamName, StringComparison.OrdinalIgnoreCase));
					if (favTeam != null)
					{
						cmbTeams.SelectedItem = favTeam;
					}
				}
				UpdateRankingDisplay();
				ShowLoadingBar(false);
				btnRetry.Visible = false;
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void ShowLoadingBar(bool show) { pnlLoading.Visible = show; pnlLoading.BringToFront(); }

		private void btnRetry_Click(object sender, EventArgs e) => LoadTeams();

		private async void cmbTeams_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedTeam = cmbTeams.SelectedItem as Team;
			if (selectedTeam == null) return;

			try
			{
				ShowLoadingBar(true);
				await LoadTeamPlayers();
				LoadFavoritePlayers();  // Load after players are loaded
				DisplayTeamPlayers();
				DisplayFavoritePlayers(); // Add this method
				UpdateRankingDisplay(); // Update rankings after loading players
				ShowLoadingBar(false);
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load team players: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void DisplayFavoritePlayers()
		{
			flpFavoritePlayers.Controls.Clear();

			foreach (var player in favoritePlayers)
			{
				var playerControl = new PlayerControl(player)
				{
					isFavorite = true
				};
				playerControl.ToggleFavorite(true);
				PlayerEventSetup(playerControl);
				flpFavoritePlayers.Controls.Add(playerControl);
			}
		}
		private void DisplayTeamPlayers()
		{
			flpTeamPlayers.Controls.Clear();

			foreach (var player in selectedTeamPlayers)
			{
				var playerControl = new PlayerControl(player)
				{
					isFavorite = favoritePlayers.Contains(player)
				};
				playerControl.ToggleFavorite(playerControl.isFavorite);

				// Event setup and add to panel
				PlayerEventSetup(playerControl);
				flpTeamPlayers.Controls.Add(playerControl);
			}
		}

		private List<string> LoadFavoritePlayerNames()
		{
			try
			{
				return File.Exists(favPlayersFile)
					? File.ReadAllLines(favPlayersFile).ToList()
					: new List<string>();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				return new List<string>();
			}
		}

		private void LoadFavoritePlayers()
		{
			var favoriteNames = LoadFavoritePlayerNames();
			favoritePlayers.Clear();

			if (selectedTeamPlayers == null) return;

			foreach (var name in favoriteNames)
			{
				var player = selectedTeamPlayers.FirstOrDefault(p =>
					p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

				if (player != null && !favoritePlayers.Contains(player))
				{
					favoritePlayers.Add(player);
				}
			}
		}

		private void SaveFavoriteTeam()
		{
			try
			{
				File.WriteAllText(favTeamFile, selectedTeam?.Country ?? "");
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
			}
		}

		private void SaveFavorites()
		{
			try
			{
				var names = favoritePlayers.Select(p => p.Name);
				File.WriteAllLines(favPlayersFile, names);
				SaveFavoriteTeam(); // Save both favorites and team
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
			}
		}


		private void PlayerEventSetup(PlayerControl playerControl)
		{
			playerControl.TransferRequested += (s, e) =>
			{
				var sourcePanel = playerControl.Parent as FlowLayoutPanel;
				var destinationPanel = sourcePanel == flpFavoritePlayers ? flpTeamPlayers : flpFavoritePlayers;
				MoveSelectedToPanel(sourcePanel, destinationPanel);
			};
			playerControl.MoveToFavorites += (s, e) => MovePlayerToPanel(playerControl, flpFavoritePlayers);
			playerControl.MoveToTeam += (s, e) => MovePlayerToPanel(playerControl, flpTeamPlayers);
			flpTeamPlayers.Controls.Add(playerControl);
		}

		private async Task LoadTeamPlayers()
		{
			if (selectedTeam == null) return;


			var matches = await repository.GetMatchesByCountryAsync(league, selectedTeam.Fifa_Code, useCache: true);
			var firstMatch = matches.FirstOrDefault();

			if (firstMatch == null)
			{
				MessageBox.Show("No matches found for this team.", "Information",
							  MessageBoxButtons.OK, MessageBoxIcon.Information);
				selectedTeamPlayers = new List<Player>();
				return;
			}

			selectedTeamPlayers = firstMatch.Home_Team_Statistics.Country == selectedTeam.Country
				? firstMatch.Home_Team_Statistics.Starting_Eleven.Concat(firstMatch.Home_Team_Statistics.Substitutes).ToList()
				: firstMatch.Away_Team_Statistics.Starting_Eleven.Concat(firstMatch.Away_Team_Statistics.Substitutes).ToList();

			if (selectedTeamPlayers == null)
				selectedTeamPlayers = new List<Player>();
		}
		private void MovePlayerToPanel(PlayerControl playerControl, FlowLayoutPanel targetPanel)
		{
			if (favoritePlayers.Contains(playerControl.player) && targetPanel == flpFavoritePlayers)
			{
				MessageBox.Show("Player is already in favorites.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (targetPanel == flpFavoritePlayers)
			{
				if (favoritePlayers.Count >= 5)
				{
					MessageBox.Show("Maximum of 5 players and no duplicates.");
					return;
				}

				var favControl = new PlayerControl(playerControl.player)
				{
					isFavorite = true
				};
				favControl.ToggleFavorite(true);
				playerControl.ToggleFavorite(true);
				PlayerEventSetup(favControl);

				flpFavoritePlayers.Controls.Add(favControl);
				favoritePlayers.Add(playerControl.player);
			}
			else
			{

				var favControl = flpFavoritePlayers.Controls
					.OfType<PlayerControl>()
					.FirstOrDefault(p => p.player == playerControl.player);

				var teamControl = flpTeamPlayers.Controls
				.OfType<PlayerControl>()
				.FirstOrDefault(p => p.player.Name == playerControl.player.Name);

				if (favControl != null && teamControl != null)
				{
					teamControl.ToggleFavorite(false);
					flpFavoritePlayers.Controls.Remove(favControl);
					favoritePlayers.Remove(playerControl.player);
				}
			}
		}

		private void MoveSelectedToPanel(FlowLayoutPanel source, FlowLayoutPanel target)
		{
			var selectedControls = source.Controls.OfType<PlayerControl>().Where(pc => pc.IsSelected).ToList();
			if (selectedControls.Count == 0)
			{
				MessageBox.Show("No players selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			foreach (var playerControl in selectedControls)
			{
				MovePlayerToPanel(playerControl, target);
			}
		}

		private void flpFavoritePlayers_DragEnter(object sender, DragEventArgs e)
		{
			var playerControl = e.Data.GetData(typeof(PlayerControl)) as PlayerControl;
			e.Effect = playerControl?.Parent == flpTeamPlayers
				? DragDropEffects.Move
				: DragDropEffects.None;
		}

		private void flpTeamPlayers_DragEnter(object sender, DragEventArgs e)
		{
			var playerControl = e.Data.GetData(typeof(PlayerControl)) as PlayerControl;
			e.Effect = playerControl?.Parent == flpFavoritePlayers
				? DragDropEffects.Move
				: DragDropEffects.None;
		}

		private void flpFavoritePlayers_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(PlayerControl)))
			{
				var sourceControl = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
				MovePlayerToPanel(sourceControl, flpFavoritePlayers);
			}
		}

		private void flpTeamPlayers_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(PlayerControl)))
			{
				var sourceControl = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
				MovePlayerToPanel(sourceControl, flpTeamPlayers);
			}
		}

		private void FIFA_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFavoriteTeam();
			SaveFavorites();
		}

		private async void UpdateRankingDisplay()
		{
			dgvPlayerRankings.DataSource = null;
			dgvMatchAttendanceRanking.DataSource = null;
			dgvPlayerRankings.DataSource = await GetPlayerRankings();
			dgvMatchAttendanceRanking.DataSource = await GetAttendanceRankings();

		}

		private async Task<List<MatchAttendance>> GetAttendanceRankings()
		{
			try
			{
				ShowLoadingBar(true);
				var rankings = await repository.GetMatchAttendanceRankingsAsync(selectedTeam, league);
				ShowLoadingBar(false);
				return rankings;
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load match attendance rankings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

		private async Task<List<PlayerRanking>> GetPlayerRankings()
		{
			try
			{
				ShowLoadingBar(true);
				var rankings = await repository.GetPlayerRankingsAsync(selectedTeam, league);
				ShowLoadingBar(false);
				return rankings;
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load player rankings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new List<PlayerRanking>();
			}
		}
		private void ExportDataGridViewToPdf(DataGridView dgv, string title, string fileName)
		{
			try
			{
				using (var document = new PdfDocument())
				{
					var page = document.AddPage();
					var gfx = XGraphics.FromPdfPage(page);
					var font = new XFont("Arial", 10, XFontStyleEx.Regular);
					var boldFont = new XFont("Arial", 10, XFontStyleEx.Bold);


					double y = 40;
					gfx.DrawString(title, boldFont, XBrushes.Black, new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);
					y += 30;

					// Draw headers
					double x = 40;
					foreach (DataGridViewColumn col in dgv.Columns)
					{
						gfx.DrawString(col.HeaderText, boldFont, XBrushes.Black, new XRect(x, y, 100, 20), XStringFormats.TopLeft);
						x += 100;
					}
					y += 25;

					// Draw rows
					foreach (DataGridViewRow row in dgv.Rows)
					{
						if (row.IsNewRow) continue;
						x = 40;
						foreach (DataGridViewCell cell in row.Cells)
						{
							gfx.DrawString(cell.Value?.ToString() ?? "", font, XBrushes.Black, new XRect(x, y, 100, 20), XStringFormats.TopLeft);
							x += 100;
						}
						y += 20;
						if (y > page.Height - 40)
						{
							page = document.AddPage();
							gfx = XGraphics.FromPdfPage(page);
							y = 40;
						}
					}

					document.Save(fileName);
					Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
				}
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to export data to PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnPrintPlayerRankings_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf", FileName = "PlayerRankings.pdf" })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					ExportDataGridViewToPdf(dgvPlayerRankings, "Player Rankings", sfd.FileName);
				}
			}
		}

		private void btnPrintMatchRankings_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf", FileName = "MatchAttendanceRankings.pdf" })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					ExportDataGridViewToPdf(dgvMatchAttendanceRanking, "Match Attendance Rankings", sfd.FileName);
				}
			}
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			using (var settingsForm = new SettingsForm(league, language))
			{
				if (settingsForm.ShowDialog() == DialogResult.OK)
				{
					Application.Restart();
				}
			}

		}
	}
}
