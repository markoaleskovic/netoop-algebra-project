namespace FIFAWFORMS
{
	partial class FIFA
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
			components = new System.ComponentModel.Container();
			cmbTeams = new ComboBox();
			label1 = new Label();
			progressBar = new ProgressBar();
			pnlLoading = new Panel();
			btnRetry = new Button();
			flpTeamPlayers = new FlowLayoutPanel();
			flpFavoritePlayers = new FlowLayoutPanel();
			label2 = new Label();
			playerRankingBindingSource = new BindingSource(components);
			matchAttendanceBindingSource = new BindingSource(components);
			dgvPlayerRankings = new DataGridView();
			dgvMatchAttendanceRanking = new DataGridView();
			btnPrintPlayerRankings = new Button();
			btnPrintMatchRankings = new Button();
			btnSettings = new Button();
			pnlLoading.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)playerRankingBindingSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)matchAttendanceBindingSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvPlayerRankings).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvMatchAttendanceRanking).BeginInit();
			SuspendLayout();
			// 
			// cmbTeams
			// 
			cmbTeams.DisplayMember = "Team";
			cmbTeams.Font = new Font("Segoe UI", 12F);
			cmbTeams.FormattingEnabled = true;
			cmbTeams.Location = new Point(61, 69);
			cmbTeams.Name = "cmbTeams";
			cmbTeams.Size = new Size(121, 29);
			cmbTeams.TabIndex = 0;
			cmbTeams.ValueMember = "Team";
			cmbTeams.SelectedIndexChanged += cmbTeams_SelectedIndexChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 18F);
			label1.Location = new Point(61, 34);
			label1.Name = "label1";
			label1.Size = new Size(115, 32);
			label1.TabIndex = 1;
			label1.Text = "All Teams";
			// 
			// progressBar
			// 
			progressBar.Location = new Point(3, 15);
			progressBar.MarqueeAnimationSpeed = 30;
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(474, 32);
			progressBar.TabIndex = 2;
			// 
			// pnlLoading
			// 
			pnlLoading.Controls.Add(progressBar);
			pnlLoading.Controls.Add(btnRetry);
			pnlLoading.Location = new Point(463, 8);
			pnlLoading.Name = "pnlLoading";
			pnlLoading.Size = new Size(706, 67);
			pnlLoading.TabIndex = 3;
			// 
			// btnRetry
			// 
			btnRetry.Location = new Point(493, 15);
			btnRetry.Name = "btnRetry";
			btnRetry.Size = new Size(201, 32);
			btnRetry.TabIndex = 3;
			btnRetry.Text = "Retry Connection";
			btnRetry.UseVisualStyleBackColor = true;
			btnRetry.Click += btnRetry_Click;
			// 
			// flpTeamPlayers
			// 
			flpTeamPlayers.AutoScroll = true;
			flpTeamPlayers.BorderStyle = BorderStyle.FixedSingle;
			flpTeamPlayers.Location = new Point(61, 122);
			flpTeamPlayers.Name = "flpTeamPlayers";
			flpTeamPlayers.Size = new Size(672, 528);
			flpTeamPlayers.TabIndex = 4;
			flpTeamPlayers.DragDrop += flpTeamPlayers_DragDrop;
			flpTeamPlayers.DragEnter += flpTeamPlayers_DragEnter;
			// 
			// flpFavoritePlayers
			// 
			flpFavoritePlayers.AllowDrop = true;
			flpFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
			flpFavoritePlayers.Location = new Point(786, 122);
			flpFavoritePlayers.Name = "flpFavoritePlayers";
			flpFavoritePlayers.Size = new Size(837, 165);
			flpFavoritePlayers.TabIndex = 5;
			flpFavoritePlayers.DragDrop += flpFavoritePlayers_DragDrop;
			flpFavoritePlayers.DragEnter += flpFavoritePlayers_DragEnter;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 18F);
			label2.Location = new Point(786, 78);
			label2.Name = "label2";
			label2.Size = new Size(218, 32);
			label2.TabIndex = 6;
			label2.Text = "My favorite players";
			// 
			// playerRankingBindingSource
			// 
			playerRankingBindingSource.DataSource = typeof(DataLayer.Models.PlayerRanking);
			// 
			// matchAttendanceBindingSource
			// 
			matchAttendanceBindingSource.DataSource = typeof(DataLayer.Models.MatchAttendance);
			// 
			// dgvPlayerRankings
			// 
			dgvPlayerRankings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPlayerRankings.Location = new Point(786, 307);
			dgvPlayerRankings.Name = "dgvPlayerRankings";
			dgvPlayerRankings.Size = new Size(371, 326);
			dgvPlayerRankings.TabIndex = 7;
			// 
			// dgvMatchAttendanceRanking
			// 
			dgvMatchAttendanceRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvMatchAttendanceRanking.Location = new Point(1177, 307);
			dgvMatchAttendanceRanking.Name = "dgvMatchAttendanceRanking";
			dgvMatchAttendanceRanking.Size = new Size(446, 326);
			dgvMatchAttendanceRanking.TabIndex = 8;
			// 
			// btnPrintPlayerRankings
			// 
			btnPrintPlayerRankings.Location = new Point(890, 649);
			btnPrintPlayerRankings.Name = "btnPrintPlayerRankings";
			btnPrintPlayerRankings.Size = new Size(154, 40);
			btnPrintPlayerRankings.TabIndex = 9;
			btnPrintPlayerRankings.Text = "Print player rankings";
			btnPrintPlayerRankings.UseVisualStyleBackColor = true;
			btnPrintPlayerRankings.Click += btnPrintPlayerRankings_Click;
			// 
			// btnPrintMatchRankings
			// 
			btnPrintMatchRankings.Location = new Point(1333, 649);
			btnPrintMatchRankings.Name = "btnPrintMatchRankings";
			btnPrintMatchRankings.Size = new Size(154, 40);
			btnPrintMatchRankings.TabIndex = 10;
			btnPrintMatchRankings.Text = "Print match rankings";
			btnPrintMatchRankings.UseVisualStyleBackColor = true;
			btnPrintMatchRankings.Click += btnPrintMatchRankings_Click;
			// 
			// btnSettings
			// 
			btnSettings.Location = new Point(1438, 21);
			btnSettings.Name = "btnSettings";
			btnSettings.Size = new Size(185, 36);
			btnSettings.TabIndex = 11;
			btnSettings.Text = "Settings";
			btnSettings.UseVisualStyleBackColor = true;
			btnSettings.Click += btnSettings_Click;
			// 
			// FIFA
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1676, 956);
			Controls.Add(btnSettings);
			Controls.Add(btnPrintMatchRankings);
			Controls.Add(btnPrintPlayerRankings);
			Controls.Add(dgvMatchAttendanceRanking);
			Controls.Add(dgvPlayerRankings);
			Controls.Add(pnlLoading);
			Controls.Add(label2);
			Controls.Add(flpFavoritePlayers);
			Controls.Add(flpTeamPlayers);
			Controls.Add(label1);
			Controls.Add(cmbTeams);
			Name = "FIFA";
			Text = "FIFA";
			FormClosing += FIFA_FormClosing;
			Load += FIFA_Load;
			pnlLoading.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)playerRankingBindingSource).EndInit();
			((System.ComponentModel.ISupportInitialize)matchAttendanceBindingSource).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvPlayerRankings).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvMatchAttendanceRanking).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox cmbTeams;
		private Label label1;
		private ProgressBar progressBar;
		private Panel pnlLoading;
		private Button btnRetry;
		private FlowLayoutPanel flpTeamPlayers;
		private FlowLayoutPanel flpFavoritePlayers;
		private Label label2;
		private BindingSource playerRankingBindingSource;
		private BindingSource matchAttendanceBindingSource;
		private DataGridView dgvPlayerRankings;
		private DataGridView dgvMatchAttendanceRanking;
		private Button btnPrintPlayerRankings;
		private Button btnPrintMatchRankings;
		private Button btnSettings;
	}
}