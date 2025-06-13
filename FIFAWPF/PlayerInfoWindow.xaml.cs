using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DataLayer.Models;
using FIFAWPF.ViewModels;

namespace FIFAWPF
{
	public partial class PlayerInfoWindow : Window
	{

		public class PlayerInfo
		{
			public int Goals { get; set; }
			public int Yellow_Cards { get; set; }
			public string PlayerName { get; set; }
			public string Position { get; set; }
			public int Shirt_Number { get; set; }
			public bool Captain { get; set; }
			public PlayerInfo(int goals, int yellowCards, string playerName, string position, int shirtNumber, bool captain)
			{
				Goals = goals;
				Yellow_Cards = yellowCards;
				PlayerName = playerName;
				Position = position;
				Shirt_Number = shirtNumber;
				Captain = captain;
			}
		}
		public int Goals { get; set; }
		public int Yellow_Cards { get; set; }
		public string PlayerName { get; set; }
		public string Position { get; set; }
		public int Shirt_Number { get; set; }
		public bool Captain { get; set; }

		private FIFAViewModel _viewModel;
		private Player player;
		public PlayerInfoWindow(Player player, FIFAViewModel viewModel)
		{
			this.player = player;
			this._viewModel = viewModel;
			InitializeComponent();
			ShowInTaskbar = false;
			LoadPlayerInfo();
		}

		private async void LoadPlayerInfo()
		{
			try
			{
				if (_viewModel != null)
				{
					var info = await _viewModel.GetPlayerInfo(player);
					if (info != null)
					{
						Goals = info.Goals ?? 0;
						Yellow_Cards = info.Yellow_Cards ?? 0;
						PlayerName = player.Name;
						Position = player.Position;
						Shirt_Number = player.Shirt_Number ?? 0;
						Captain = player.Captain ?? false;
						PlayerInfo playerInfo = new PlayerInfo(Goals, Yellow_Cards, PlayerName, Position, Shirt_Number, Captain);
						DataContext = playerInfo;
						// Hide loading indicator and show content with animation
						LoadingIndicator.Visibility = Visibility.Collapsed;
						var animation = (Storyboard)Resources["PopupShowAnimation"];
						animation.Begin(InfoPanel);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to load team statistics: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				Close();
			}
		}
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);
			DragMove();
		}

		private void Background_MouseDown(object sender, MouseButtonEventArgs e)
		{
			// Only close if the click is not on the info panel
			this.Close();
		}

		private void InfoPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			// Prevent the click from bubbling up to the background
			e.Handled = true;
		}

	}
}
