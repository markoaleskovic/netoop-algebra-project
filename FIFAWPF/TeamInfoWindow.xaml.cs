using System.Windows;
using System.Windows.Input;
using DataLayer.Models;
using FIFAWPF.ViewModels;
using System.Windows.Media.Animation;

namespace FIFAWPF
{
    public partial class TeamInfoWindow : Window
    {
        private FIFAViewModel _viewModel;

        public TeamInfoWindow(Team team, FIFAViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            // Start loading data
            LoadTeamStatistics(team);
        }

        private async void LoadTeamStatistics(Team team)
        {
            try
            {
                if (_viewModel != null)
                {
                    var stats = await _viewModel.GetTeamStatistics(team);
                    if (stats != null)
                    {
                        DataContext = stats;
                        
                        // Hide loading indicator and show content with animation
                        LoadingIndicator.Visibility = Visibility.Collapsed;
                        var animation = (Storyboard)Resources["FadeInAnimation"];
                        animation.Begin(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load team statistics: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


		private void Background_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.Close();
		}

		private void InfoPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}


	}
} 