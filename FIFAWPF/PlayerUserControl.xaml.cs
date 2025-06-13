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
					var mainWindow = Application.Current.MainWindow;
					var viewModel = mainWindow?.DataContext as FIFAWPF.ViewModels.FIFAViewModel;
					var popup = new PlayerInfoWindow(player, viewModel);
					popup.ShowDialog();
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
