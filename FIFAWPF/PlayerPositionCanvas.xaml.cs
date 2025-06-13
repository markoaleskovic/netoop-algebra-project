using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataLayer.Models;

namespace FIFAWPF
{
    public partial class PlayerPositionCanvas : UserControl
    {
        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register(nameof(Players), typeof(List<Player>), typeof(PlayerPositionCanvas),
                new PropertyMetadata(null, (d, e) => ((PlayerPositionCanvas)d).ArrangePlayers()));

        public static readonly DependencyProperty FormationProperty =
            DependencyProperty.Register(nameof(Formation), typeof(string), typeof(PlayerPositionCanvas),
                new PropertyMetadata("", (d, e) => ((PlayerPositionCanvas)d).ArrangePlayers()));

        public static readonly DependencyProperty IsLeftTeamProperty =
            DependencyProperty.Register(nameof(IsLeftTeam), typeof(bool), typeof(PlayerPositionCanvas),
                new PropertyMetadata(true, (d, e) => ((PlayerPositionCanvas)d).ArrangePlayers()));

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(PlayerPositionCanvas),
                new PropertyMetadata(false, (d, e) => ((PlayerPositionCanvas)d).UpdateVisibility()));

        public List<Player> Players
        {
            get => (List<Player>)GetValue(PlayersProperty);
            set => SetValue(PlayersProperty, value);
        }

        public string Formation
        {
            get => (string)GetValue(FormationProperty);
            set => SetValue(FormationProperty, value);
        }

        public bool IsLeftTeam
        {
            get => (bool)GetValue(IsLeftTeamProperty);
            set => SetValue(IsLeftTeamProperty, value);
        }

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        private const double PLAYER_WIDTH = 70;
        private const double PLAYER_HEIGHT = 90;
        private const double PADDING = 20;

        public PlayerPositionCanvas()
        {
            InitializeComponent();
            SizeChanged += (s, e) => ArrangePlayers();
        }

        private void UpdateVisibility()
        {
            MainCanvas.Visibility = IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

		public void ArrangePlayers()
		{
			MainCanvas.Children.Clear();
			if (Players == null || string.IsNullOrWhiteSpace(Formation)) return;
			try
			{
				var formationNumbers = Formation.Split('-').Select(n => int.Parse(n)).ToList();
				int totalRows = formationNumbers.Count + 1; // +1 for goalkeeper

				double width = ActualWidth > 0 ? ActualWidth : 600;
				double height = ActualHeight > 0 ? ActualHeight : 400;

				// Place goalkeeper at the left or right edge, vertically centered
				var goalie = Players.FirstOrDefault(p => p.Position == "Goalie");
				if (goalie != null)
				{
					var gkControl = new PlayerUserControl { DataContext = goalie };
					Panel.SetZIndex(gkControl, 10);
					double gkY = (height - PLAYER_HEIGHT) / 2;
					double gkX = IsLeftTeam ? PADDING : width - PLAYER_WIDTH - PADDING;
					MainCanvas.Children.Add(gkControl);
					Canvas.SetLeft(gkControl, gkX);
					Canvas.SetTop(gkControl, gkY);
				}

				// Outfield players
				var outfield = Players.Where(p => p.Position != "Goalie").ToList();
				int playerIndex = 0;

				// X: distance from goal line, Y: spread vertically
				double fieldStartX = IsLeftTeam ? PADDING : width - PLAYER_WIDTH - PADDING;
				double fieldEndX = IsLeftTeam ? width - PLAYER_WIDTH - PADDING : PADDING;
				double colSpacing = Math.Abs(fieldEndX - fieldStartX) / (formationNumbers.Count + 1);

				for (int col = 0; col < formationNumbers.Count; col++)
				{
					int playersInCol = formationNumbers[col];
					double x = IsLeftTeam
						? fieldStartX + colSpacing * (col + 1)
						: fieldStartX - colSpacing * (col + 1);

					// Spread players vertically in this column
					for (int i = 0; i < playersInCol && playerIndex < outfield.Count; i++, playerIndex++)
					{
						var player = outfield[playerIndex];
						var control = new PlayerUserControl { DataContext = player };
						Panel.SetZIndex(control, 10);
						double y = (height - PLAYER_HEIGHT) * (i + 1) / (playersInCol + 1);
						MainCanvas.Children.Add(control);
						Canvas.SetLeft(control, x);
						Canvas.SetTop(control, y);
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"PlayerPositionCanvas error: {ex.Message}");
				// Add more detailed error logging
				System.Diagnostics.Debug.WriteLine($"Players count: {Players?.Count ?? 0}");
				System.Diagnostics.Debug.WriteLine($"Formation: {Formation}");
				System.Diagnostics.Debug.WriteLine($"IsLeftTeam: {IsLeftTeam}");
				System.Diagnostics.Debug.WriteLine($"Canvas size: {ActualWidth}x{ActualHeight}");
			}
		}

	}
} 