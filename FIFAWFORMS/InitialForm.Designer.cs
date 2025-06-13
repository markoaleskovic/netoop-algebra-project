namespace FIFAWFORMS
{
	partial class InitialForm
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
			btnMens = new Button();
			btnWomens = new Button();
			btnEnglish = new Button();
			btnCroatian = new Button();
			label1 = new Label();
			label2 = new Label();
			btnStart = new Button();
			SuspendLayout();
			// 
			// btnMens
			// 
			btnMens.Location = new Point(179, 107);
			btnMens.Name = "btnMens";
			btnMens.Size = new Size(134, 102);
			btnMens.TabIndex = 0;
			btnMens.Text = "Mens League";
			btnMens.UseVisualStyleBackColor = true;
			btnMens.Click += btnMens_Click;
			// 
			// btnWomens
			// 
			btnWomens.Location = new Point(433, 107);
			btnWomens.Name = "btnWomens";
			btnWomens.Size = new Size(134, 102);
			btnWomens.TabIndex = 1;
			btnWomens.Text = "Womens League";
			btnWomens.UseVisualStyleBackColor = true;
			btnWomens.Click += btnWomens_Click;
			// 
			// btnEnglish
			// 
			btnEnglish.Location = new Point(179, 251);
			btnEnglish.Name = "btnEnglish";
			btnEnglish.Size = new Size(134, 102);
			btnEnglish.TabIndex = 2;
			btnEnglish.Text = "English";
			btnEnglish.UseVisualStyleBackColor = true;
			btnEnglish.Click += btnEnglish_Click;
			// 
			// btnCroatian
			// 
			btnCroatian.Location = new Point(433, 251);
			btnCroatian.Name = "btnCroatian";
			btnCroatian.Size = new Size(134, 102);
			btnCroatian.TabIndex = 3;
			btnCroatian.Text = "Croatian";
			btnCroatian.UseVisualStyleBackColor = true;
			btnCroatian.Click += btnCroatian_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.Location = new Point(253, 9);
			label1.Name = "label1";
			label1.Size = new Size(259, 45);
			label1.TabIndex = 4;
			label1.Text = "Fifa Team Viewer";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label2.Location = new Point(199, 63);
			label2.Name = "label2";
			label2.Size = new Size(349, 30);
			label2.TabIndex = 5;
			label2.Text = "Please select a league and language";
			// 
			// btnStart
			// 
			btnStart.Location = new Point(253, 393);
			btnStart.Name = "btnStart";
			btnStart.Size = new Size(259, 45);
			btnStart.TabIndex = 6;
			btnStart.Text = "Start";
			btnStart.UseVisualStyleBackColor = true;
			btnStart.Click += btnStart_Click;
			// 
			// InitialForm
			// 
			AcceptButton = btnStart;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.SkyBlue;
			ClientSize = new Size(800, 450);
			Controls.Add(btnStart);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(btnCroatian);
			Controls.Add(btnEnglish);
			Controls.Add(btnWomens);
			Controls.Add(btnMens);
			Name = "InitialForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "InitialForm";
			Load += InitialForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnMens;
		private Button btnWomens;
		private Button btnEnglish;
		private Button btnCroatian;
		private Label label1;
		private Label label2;
		private Button btnStart;
	}
}