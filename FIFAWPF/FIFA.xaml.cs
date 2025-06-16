using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FIFAWPF.ViewModels;
using System.Windows.Media.Animation;
using DataLayer.Models;

namespace FIFAWPF
{
	/// <summary>
	/// Interaction logic for FIFA.xaml
	/// </summary>
	public partial class FIFA : Window
	{
		private int _renderVersion = 0;
		private List<Player> _lastLeftPlayers;
		private List<Player> _lastRightPlayers;
		private bool _lastIsLeftDraw;
		private bool _isRefreshing = false;
		private bool _isExitConfirmed = false;

		public FIFA(AppConfig.WindowSize windowSize, string language, bool isFullScreen = false)
		{
			InitializeComponent();
			this.Closing += FIFA_Closing;
			AppConfig.ApplicationRefreshed += OnApplicationRefreshed;

			if (isFullScreen)
			{
				WindowStyle = WindowStyle.None;
				WindowState = WindowState.Maximized;
				ResizeMode = ResizeMode.NoResize;
				Topmost = true;
			}
			else
			{
				// Apply window size
				var (width, height) = AppConfig.GetWindowDimensions(windowSize);
				Width = width;
				Height = height;
				WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}

			// Apply language
			Title = $"FIFA - {language}";

			// Bind loading overlay
			var binding = new Binding("IsLoading") { Converter = new BooleanToVisibilityConverter() };
			LoadingOverlay.SetBinding(Grid.VisibilityProperty, binding);

			if (DataContext is FIFAViewModel vm)
			{
				vm.CurrentWindowSize = windowSize;
			}
		}
		private void OnApplicationRefreshed()
		{
			// Reload settings
			var (isFullScreen, size, language) = AppConfig.Load();

			Application.Current.Dispatcher.Invoke(() =>
			{
				// Clear player canvases
				if (LeftTeamPositionCanvas != null)
				{
					LeftTeamPositionCanvas.Players = null;
					LeftTeamPositionCanvas.IsVisible = false;
				}
				if (RightTeamPositionCanvas != null)
				{
					RightTeamPositionCanvas.Players = null;
					RightTeamPositionCanvas.IsVisible = false;
				}

				// Close all windows including PlayerInfoWindow and TeamInfoWindow
				foreach (Window win in Application.Current.Windows.Cast<Window>().ToList())
				{
					if (win != this)
					{
						win.Close();
					}
				}

				// Open a new FIFA window with new settings
				var newWindow = new FIFA(size, language, isFullScreen);
				newWindow.Show();
				_isRefreshing = true;
				this.Close();
			});
		}

		private void btnLeftInfo_Click(object sender, RoutedEventArgs e)
		{
			if (DataContext is FIFAViewModel viewModel && viewModel.SelectedTeamLeft != null)
			{
				var teamInfoWindow = new TeamInfoWindow(viewModel.SelectedTeamLeft, viewModel);
				teamInfoWindow.ShowDialog();
			}
		}

		private void btnRightInfo_Click(object sender, RoutedEventArgs e)
		{
			if (DataContext is FIFAViewModel viewModel && viewModel.SelectedTeamRight != null)
			{
				var teamInfoWindow = new TeamInfoWindow(viewModel.SelectedTeamRight, viewModel);
				teamInfoWindow.ShowDialog();
			}
		}

		private async void btnShowLeftFormation_Click(object sender, RoutedEventArgs e)
		{
			if (DataContext is FIFAViewModel vm)
			{
				await vm.LoadLeftFormation();
				vm.LoadSubstitues(true);
				vm.SelectedTeam = vm.SelectedTeamLeft;
				LeftTeamPositionCanvas.IsVisible = true;
				RightTeamPositionCanvas.IsVisible = false;
				// Optionally clear right formation for clarity:
				vm.RightTeamStartingPlayers = null;
				vm.RightTeamFormation = null;
				vm.RightTeamSubstitutes = null;
				// Force re-initialization for interactivity/animations
				LeftTeamPositionCanvas.ArrangePlayers();
			}
		}

		private async void btnShowRightFormation_Click(object sender, RoutedEventArgs e)
		{
			if (DataContext is FIFAViewModel vm)
			{
				await vm.LoadRightFormation();
				vm.LoadSubstitues(false);
				vm.SelectedTeam = vm.SelectedTeamRight;
				LeftTeamPositionCanvas.IsVisible = false;
				RightTeamPositionCanvas.IsVisible = true;
				// Optionally clear left formation for clarity:
				vm.LeftTeamStartingPlayers = null;
				vm.LeftTeamFormation = null;
				vm.LeftTeamSubstitutes = null;
				// Force re-initialization for interactivity/animations
				RightTeamPositionCanvas.ArrangePlayers();
			}
		}

		private bool HandleExit()
		{
			if (_isRefreshing || _isExitConfirmed)
				return true;

			var result = MessageBox.Show(
				"Do you really want to close the application?",
				"Confirm Exit",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (DataContext is FIFAViewModel viewModel)
				{
					viewModel.SaveFavoriteTeam();
				}
				_isExitConfirmed = true;
				return true;
			}
			return false;
		}

		private void FIFA_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!HandleExit())
			{
				e.Cancel = true;
			}
		}

		private void btnSettings_Click(object sender, RoutedEventArgs e)
		{ 
			var settingswindow = new SettingsWindow();
			settingswindow.Owner = this;
			settingswindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			settingswindow.ShowDialog();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			AppConfig.ApplicationRefreshed -= OnApplicationRefreshed;
		}

		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			if (HandleExit())
			{
				Application.Current.Shutdown();
			}
		}
	}
}
