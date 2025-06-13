using System;
using System.Windows;
using FIFAWPF.ViewModels;

namespace FIFAWPF
{
	/// <summary>
	/// Interaction logic for InitialWindow.xaml
	/// </summary>
	public partial class InitialWindow : Window
	{
		public InitialWindow()
		{
			InitializeComponent();
			FullScreenRadio.Checked += FullScreenRadio_Checked;
			WindowedRadio.Checked += WindowedRadio_Checked;
			LoadSettings();
		}

		private void LoadSettings()
		{
			// Set default language
			LanguageComboBox.SelectedIndex = 0; // Default to English

			// Set default window size
			MediumSizeRadio.IsChecked = true;
		}

		private void FullScreenRadio_Checked(object sender, RoutedEventArgs e)
		{
			SmallSizeRadio.IsChecked = false;
			MediumSizeRadio.IsChecked = false;
			LargeSizeRadio.IsChecked = false;
		}

		private void WindowedRadio_Checked(object sender, RoutedEventArgs e)
		{
			if (SmallSizeRadio.IsChecked != true && MediumSizeRadio.IsChecked != true && LargeSizeRadio.IsChecked != true)
			{
				MediumSizeRadio.IsChecked = true;
			}
		}

		private void SaveSettings_Click(object sender, RoutedEventArgs e)
		{
			if (LanguageComboBox.SelectedItem == null)
			{
				MessageBox.Show("Please select a language.", "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!FullScreenRadio.IsChecked.Value && !WindowedRadio.IsChecked.Value)
			{
				MessageBox.Show("Please select a display mode.", "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (WindowedRadio.IsChecked.Value && !SmallSizeRadio.IsChecked.Value && !MediumSizeRadio.IsChecked.Value && !LargeSizeRadio.IsChecked.Value)
			{
				MessageBox.Show("Please select a window size.", "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Get selected language
			string selectedLanguage = ((System.Windows.Controls.ComboBoxItem)LanguageComboBox.SelectedItem).Content.ToString();

			// Get selected window size
			AppConfig.WindowSize selectedSize;
			if (SmallSizeRadio.IsChecked.Value)
				selectedSize = AppConfig.WindowSize.Small;
			else if (MediumSizeRadio.IsChecked.Value)
				selectedSize = AppConfig.WindowSize.Medium;
			else
				selectedSize = AppConfig.WindowSize.Large;

			bool isFullScreen = FullScreenRadio.IsChecked == true;
			// Save configuration
			AppConfig.Save(isFullScreen, selectedSize, selectedLanguage);

			// Open FIFA window with selected parameters
			var fifaWindow = new FIFA(selectedSize, selectedLanguage, isFullScreen);
			fifaWindow.Show();

			// Close the initial window
			Close();
		}
	}
}
