using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Extensions.Logging.Abstractions;
using DataLayer.Services;

namespace FIFAWFORMS
{
	public partial class InitialForm : Form
	{

		private string userSettings = "user_settings.txt";
		public string SelectedLanguage { get; private set; }
		public string SelectedLeague { get; private set; }

		public InitialForm()
		{
			InitializeComponent();
		}

		private void InitialForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists(userSettings))
				{
					var lines = File.ReadAllLines(userSettings);
					if (lines.Length >= 2)
					{
						SelectedLanguage = lines[1];
						SelectedLeague = lines[0];
						OpenMainForm();
						return;
					}
				}
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to load previous settings. Please check the log file for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void OpenMainForm()
		{
			try
			{

				this.Hide();
				var mainform = new FIFA(SelectedLanguage, SelectedLeague);
				mainform.ShowDialog();
				this.Close();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to open the team manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		private void btnMens_Click(object sender, EventArgs e)
		{
			SelectedLeague = "men";
			btnMens.BackColor = Color.LightGreen;
			btnWomens.BackColor = SystemColors.Control;
		}

		private void btnWomens_Click(object sender, EventArgs e)
		{
			SelectedLeague = "women";
			btnWomens.BackColor = Color.LightGreen;
			btnMens.BackColor = SystemColors.Control;
		}

		private void btnEnglish_Click(object sender, EventArgs e)
		{
			SelectedLanguage = "english";
			btnEnglish.BackColor = Color.LightGreen;
			btnCroatian.BackColor = SystemColors.Control;
		}

		private void btnCroatian_Click(object sender, EventArgs e)
		{
			SelectedLanguage = "croatian";
			btnCroatian.BackColor = Color.LightGreen;
			btnEnglish.BackColor = SystemColors.Control;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (SelectedLeague == null || SelectedLanguage == null)
			{
				MessageBox.Show("Please select a league and a language.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				File.WriteAllLines(userSettings, new[] { SelectedLeague, SelectedLanguage });
				OpenMainForm();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to save settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
