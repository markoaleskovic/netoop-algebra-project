using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataLayer.Models;

namespace FIFAWPF
{
	public partial class PlayerUserControl : UserControl
	{
		public PlayerUserControl()
		{
			InitializeComponent();
			this.MouseLeftButtonUp += PlayerUserControl_MouseLeftButtonUp;
		}

		private void PlayerUserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			// DataContext is the Player
			if (DataContext is Player player)
			{
				try
				{
					// Get the FIFAViewModel from the main window at the time of click
					var mainWindow = Application.Current.Windows
						.OfType<FIFA>()
						.FirstOrDefault(w => w.IsActive) ?? Application.Current.MainWindow as FIFA;
					var viewModel = mainWindow?.DataContext as FIFAWPF.ViewModels.FIFAViewModel;
					if (viewModel == null)
					{
						MessageBox.Show("Unable to find the current FIFAViewModel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}
					var popup = new PlayerInfoWindow(player, viewModel);
					popup.Show();
				}
				catch (Exception)
				{
					MessageBox.Show("Error handling player info");
					return;
				}
			}
		}
	}
}
