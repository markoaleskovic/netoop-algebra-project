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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FIFA));
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
			resources.ApplyResources(cmbTeams, "cmbTeams");
			cmbTeams.FormattingEnabled = true;
			cmbTeams.Name = "cmbTeams";
			cmbTeams.ValueMember = "Team";
			cmbTeams.SelectedIndexChanged += cmbTeams_SelectedIndexChanged;
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// progressBar
			// 
			resources.ApplyResources(progressBar, "progressBar");
			progressBar.MarqueeAnimationSpeed = 30;
			progressBar.Name = "progressBar";
			// 
			// pnlLoading
			// 
			pnlLoading.Controls.Add(progressBar);
			pnlLoading.Controls.Add(btnRetry);
			resources.ApplyResources(pnlLoading, "pnlLoading");
			pnlLoading.Name = "pnlLoading";
			// 
			// btnRetry
			// 
			resources.ApplyResources(btnRetry, "btnRetry");
			btnRetry.Name = "btnRetry";
			btnRetry.UseVisualStyleBackColor = true;
			btnRetry.Click += btnRetry_Click;
			// 
			// flpTeamPlayers
			// 
			resources.ApplyResources(flpTeamPlayers, "flpTeamPlayers");
			flpTeamPlayers.BorderStyle = BorderStyle.FixedSingle;
			flpTeamPlayers.Name = "flpTeamPlayers";
			flpTeamPlayers.DragDrop += flpTeamPlayers_DragDrop;
			flpTeamPlayers.DragEnter += flpTeamPlayers_DragEnter;
			// 
			// flpFavoritePlayers
			// 
			flpFavoritePlayers.AllowDrop = true;
			flpFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
			resources.ApplyResources(flpFavoritePlayers, "flpFavoritePlayers");
			flpFavoritePlayers.Name = "flpFavoritePlayers";
			flpFavoritePlayers.DragDrop += flpFavoritePlayers_DragDrop;
			flpFavoritePlayers.DragEnter += flpFavoritePlayers_DragEnter;
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
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
			resources.ApplyResources(dgvPlayerRankings, "dgvPlayerRankings");
			dgvPlayerRankings.Name = "dgvPlayerRankings";
			// 
			// dgvMatchAttendanceRanking
			// 
			dgvMatchAttendanceRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			resources.ApplyResources(dgvMatchAttendanceRanking, "dgvMatchAttendanceRanking");
			dgvMatchAttendanceRanking.Name = "dgvMatchAttendanceRanking";
			// 
			// btnPrintPlayerRankings
			// 
			resources.ApplyResources(btnPrintPlayerRankings, "btnPrintPlayerRankings");
			btnPrintPlayerRankings.Name = "btnPrintPlayerRankings";
			btnPrintPlayerRankings.UseVisualStyleBackColor = true;
			btnPrintPlayerRankings.Click += btnPrintPlayerRankings_Click;
			// 
			// btnPrintMatchRankings
			// 
			resources.ApplyResources(btnPrintMatchRankings, "btnPrintMatchRankings");
			btnPrintMatchRankings.Name = "btnPrintMatchRankings";
			btnPrintMatchRankings.UseVisualStyleBackColor = true;
			btnPrintMatchRankings.Click += btnPrintMatchRankings_Click;
			// 
			// btnSettings
			// 
			resources.ApplyResources(btnSettings, "btnSettings");
			btnSettings.Name = "btnSettings";
			btnSettings.UseVisualStyleBackColor = true;
			btnSettings.Click += btnSettings_Click;
			// 
			// FIFA
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
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