using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using FIFAWPF.ViewModels;

namespace FIFAWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			try
			{
				var (isFullScreen, size, language) = AppConfig.Load();
				if (File.Exists("user_settings.txt"))
				{
					// Settings exist, open FIFA window directly
					var fifaWindow = new FIFA(size, language, isFullScreen);
					fifaWindow.Show();
				}
				else
				{
					// No settings, show initial window
					var initialWindow = new InitialWindow();
					initialWindow.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to start the application: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				Shutdown();
			}
		}
	}
}
