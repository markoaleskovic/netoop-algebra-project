namespace FIFAWFORMS
{
	partial class SettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			grpChampionship = new GroupBox();
			rbWomen = new RadioButton();
			rbMen = new RadioButton();
			grpLanguage = new GroupBox();
			rbCroatian = new RadioButton();
			rbEnglish = new RadioButton();
			btnSave = new Button();
			btnCancel = new Button();
			grpChampionship.SuspendLayout();
			grpLanguage.SuspendLayout();
			SuspendLayout();
			// 
			// grpChampionship
			// 
			grpChampionship.Controls.Add(rbWomen);
			grpChampionship.Controls.Add(rbMen);
			grpChampionship.Location = new Point(56, 55);
			grpChampionship.Name = "grpChampionship";
			grpChampionship.Size = new Size(245, 280);
			grpChampionship.TabIndex = 0;
			grpChampionship.TabStop = false;
			grpChampionship.Text = "Championship";
			// 
			// rbWomen
			// 
			rbWomen.AutoSize = true;
			rbWomen.Location = new Point(43, 115);
			rbWomen.Name = "rbWomen";
			rbWomen.Size = new Size(132, 19);
			rbWomen.TabIndex = 1;
			rbWomen.TabStop = true;
			rbWomen.Text = "Womens World Cup";
			rbWomen.UseVisualStyleBackColor = true;
			// 
			// rbMen
			// 
			rbMen.AutoSize = true;
			rbMen.Location = new Point(43, 57);
			rbMen.Name = "rbMen";
			rbMen.Size = new Size(114, 19);
			rbMen.TabIndex = 0;
			rbMen.TabStop = true;
			rbMen.Text = "Mens World Cup";
			rbMen.UseVisualStyleBackColor = true;
			// 
			// grpLanguage
			// 
			grpLanguage.Controls.Add(rbCroatian);
			grpLanguage.Controls.Add(rbEnglish);
			grpLanguage.Location = new Point(467, 55);
			grpLanguage.Name = "grpLanguage";
			grpLanguage.Size = new Size(245, 280);
			grpLanguage.TabIndex = 1;
			grpLanguage.TabStop = false;
			grpLanguage.Text = "Language";
			// 
			// rbCroatian
			// 
			rbCroatian.AutoSize = true;
			rbCroatian.Location = new Point(50, 115);
			rbCroatian.Name = "rbCroatian";
			rbCroatian.Size = new Size(70, 19);
			rbCroatian.TabIndex = 3;
			rbCroatian.TabStop = true;
			rbCroatian.Text = "Croatian";
			rbCroatian.UseVisualStyleBackColor = true;
			// 
			// rbEnglish
			// 
			rbEnglish.AutoSize = true;
			rbEnglish.Location = new Point(50, 57);
			rbEnglish.Name = "rbEnglish";
			rbEnglish.Size = new Size(63, 19);
			rbEnglish.TabIndex = 2;
			rbEnglish.TabStop = true;
			rbEnglish.Text = "English";
			rbEnglish.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			btnSave.Location = new Point(334, 112);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(93, 77);
			btnSave.TabIndex = 4;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnCancel
			// 
			btnCancel.Location = new Point(334, 258);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(93, 77);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// SettingsForm
			// 
			AcceptButton = btnSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(800, 450);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(grpLanguage);
			Controls.Add(grpChampionship);
			Name = "SettingsForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "SettingsForm";
			grpChampionship.ResumeLayout(false);
			grpChampionship.PerformLayout();
			grpLanguage.ResumeLayout(false);
			grpLanguage.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private GroupBox grpChampionship;
		private RadioButton rbWomen;
		private RadioButton rbMen;
		private GroupBox grpLanguage;
		private RadioButton rbCroatian;
		private RadioButton rbEnglish;
		private Button btnSave;
		private Button btnCancel;
	}
}