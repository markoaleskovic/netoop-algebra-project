namespace FIFAWFORMS
{
	partial class PlayerControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControl));
			picPlayer = new PictureBox();
			lbName = new Label();
			label1 = new Label();
			lbPosition = new Label();
			lbFavorite = new Label();
			lblNumber = new Label();
			((System.ComponentModel.ISupportInitialize)picPlayer).BeginInit();
			SuspendLayout();
			// 
			// picPlayer
			// 
			picPlayer.Image = (Image)resources.GetObject("picPlayer.Image");
			picPlayer.InitialImage = (Image)resources.GetObject("picPlayer.InitialImage");
			picPlayer.Location = new Point(1, 50);
			picPlayer.Name = "picPlayer";
			picPlayer.Size = new Size(100, 100);
			picPlayer.TabIndex = 0;
			picPlayer.TabStop = false;
			// 
			// lbName
			// 
			lbName.AutoSize = true;
			lbName.Location = new Point(3, 10);
			lbName.Name = "lbName";
			lbName.Size = new Size(39, 15);
			lbName.TabIndex = 1;
			lbName.Text = "Name";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(52, 35);
			label1.Name = "label1";
			label1.Size = new Size(0, 15);
			label1.TabIndex = 3;
			// 
			// lbPosition
			// 
			lbPosition.AutoSize = true;
			lbPosition.Location = new Point(7, 35);
			lbPosition.Name = "lbPosition";
			lbPosition.Size = new Size(53, 15);
			lbPosition.TabIndex = 4;
			lbPosition.Text = "Poisition";
			// 
			// lbFavorite
			// 
			lbFavorite.AutoSize = true;
			lbFavorite.Location = new Point(87, 35);
			lbFavorite.Name = "lbFavorite";
			lbFavorite.Size = new Size(12, 15);
			lbFavorite.TabIndex = 5;
			lbFavorite.Text = "*";
			// 
			// lblNumber
			// 
			lblNumber.AutoSize = true;
			lblNumber.Location = new Point(70, 35);
			lblNumber.Name = "lblNumber";
			lblNumber.Size = new Size(0, 15);
			lblNumber.TabIndex = 6;
			// 
			// PlayerControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			BackColor = Color.PaleGreen;
			Controls.Add(lblNumber);
			Controls.Add(lbFavorite);
			Controls.Add(lbPosition);
			Controls.Add(label1);
			Controls.Add(lbName);
			Controls.Add(picPlayer);
			Name = "PlayerControl";
			Size = new Size(104, 153);
			((System.ComponentModel.ISupportInitialize)picPlayer).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox picPlayer;
		private Label lbName;
		private Label label1;
		private Label lbPosition;
		private Label lbFavorite;
		private Label lblNumber;
	}
}
