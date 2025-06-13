using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FIFAWPF.ViewModels;

namespace FIFAWPF
{
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
			// Set defaults or load from config
			LoadSettings();
		}

		private void LoadSettings()
		{
			var (isFullScreen, size, language) = AppConfig.Load();

			// Language
			if (language == "Croatian")
				rbCroatian.IsChecked = true;
			else
				rbEnglish.IsChecked = true;

			// Display mode
			if (isFullScreen)
			{
				rbFullScreen.IsChecked = true;
				grpWindowSize.IsEnabled = false;
			}
			else
			{
				rbWindowed.IsChecked = true;
				grpWindowSize.IsEnabled = true;
			}

			// Window size
			switch (size)
			{
				case AppConfig.WindowSize.Small:
					rbSmall.IsChecked = true;
					break;
				case AppConfig.WindowSize.Large:
					rbLarge.IsChecked = true;
					break;
				default:
					rbMedium.IsChecked = true;
					break;
			}
		}

		private void rbFullScreen_Checked(object sender, RoutedEventArgs e)
		{
			grpWindowSize.IsEnabled = false;
		}

		private void rbWindowed_Checked(object sender, RoutedEventArgs e)
		{
			grpWindowSize.IsEnabled = true;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			// Language
			string selectedLanguage = rbCroatian.IsChecked == true ? "Croatian" : "English";

			// Display mode
			bool isFullScreen = rbFullScreen.IsChecked == true;

			// Window size
			AppConfig.WindowSize selectedSize = AppConfig.WindowSize.Medium;
			if (rbSmall.IsChecked == true)
				selectedSize = AppConfig.WindowSize.Small;
			else if (rbLarge.IsChecked == true)
				selectedSize = AppConfig.WindowSize.Large;

			if (!isFullScreen && rbSmall.IsChecked != true && rbMedium.IsChecked != true && rbLarge.IsChecked != true)
			{
				MessageBox.Show("Please select a window size.", "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			AppConfig.Save(isFullScreen, selectedSize, selectedLanguage);
			DialogResult = true;
			Close();
			AppConfig.RefreshApplication();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				btnSave.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
			else if (e.Key == Key.Escape)
			{
				btnCancel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
		}
	}
}
