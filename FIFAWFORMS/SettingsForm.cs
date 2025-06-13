using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIFAWFORMS
{
	public partial class SettingsForm : Form
	{
		private string userSettings = "user_settings.txt";
		public string SelectedLanguage { get; private set; }
		public string SelectedLeague { get; private set; }
		public SettingsForm(string currentLeague, string currentLanguage)
		{
			InitializeComponent();
			if (currentLeague == "men") rbMen.Checked = true;
			else rbWomen.Checked = true;
			if (currentLanguage == "English") rbEnglish.Checked = true;
			else rbCroatian.Checked = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				SelectedLeague = rbMen.Checked ? "men" : "women";
				SelectedLanguage = rbEnglish.Checked ? "English" : "Croatian";
				File.WriteAllLines(userSettings, new[] { SelectedLeague, SelectedLanguage });
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Failed to save settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
