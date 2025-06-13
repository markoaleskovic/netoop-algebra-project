using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Models;
using DataLayer.Services;

namespace FIFAWFORMS
{
	public partial class PlayerControl : UserControl
	{
		public Player player { get; private set; }
		public bool isFavorite { get; set; } = false;
		public bool IsSelected { get; set; }

		public event EventHandler TransferRequested;
		public event EventHandler MoveToFavorites;
		public event EventHandler MoveToTeam;
		public PlayerControl(Player player)
		{
			InitializeComponent();
			this.player = player;
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			InitializeUI();
			SetupMouseEvents();
			CreateContextMenu();
		}

		private void InitializeUI()
		{
			lbName.Text = player.Name;
			lbPosition.Text = player.Position;
			ToggleFavorite(isFavorite);

			try
			{
				string imgPath = Path.Combine("PlayerImages", player.Name + ".png");
				string defaultImagePath = Path.Combine(Application.StartupPath, "Resources", "DefaultPlayerImage.jpg");
			}
			catch (Exception ex)
			{
				FileService.LogError(ex);
				MessageBox.Show("Error loading player image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SetupMouseEvents()
		{
			void AttachHandler(Control control)
			{
				control.MouseDown += HandleMouse;
				foreach (Control child in control.Controls)
				{
					AttachHandler(child);
				}
			}

			AttachHandler(this);
		}


		//nothing works
		private void HandleMouse(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Control current = sender as Control;
				PlayerControl playerControl = null;

				while (current != null && !(current is PlayerControl))
				{
					current = current.Parent;
				}
				playerControl = current as PlayerControl;

				if (playerControl != null)
				{
					if (ModifierKeys.HasFlag(Keys.Control))
					{
						playerControl.ToggleSelection();
					}
					else
					{
						playerControl.DoDragDrop(playerControl, DragDropEffects.Move);
					}
				}
			}
		}

		public void ToggleFavorite(bool t)
		{
			isFavorite = t;
			lbFavorite.Text = isFavorite ? "★" : "";
		}


		private void ToggleSelection()
		{
			this.BackColor = IsSelected ? Color.LightBlue : Color.Transparent;
			IsSelected = !IsSelected;

			// Propagate to child controls
			foreach (Control child in this.Controls)
			{
				if (child is Label || child is PictureBox)
				{
					child.BackColor = this.BackColor;
				}
			}

			this.Invalidate();
			Debug.WriteLine($"Selection: {IsSelected} | {player.Name}");
		}

		private void CreateContextMenu()
		{
			var menu = new ContextMenuStrip();

			var transferMenuItem = new ToolStripMenuItem("Transfer Selected", null,
				(s, e) => TransferRequested?.Invoke(this, EventArgs.Empty));

			var moveToFavMenuItem = new ToolStripMenuItem("Move to Favorites", null,
				(s, e) => MoveToFavorites?.Invoke(this, EventArgs.Empty));

			var moveToOthersMenuItem = new ToolStripMenuItem("Move to Team", null,
				(s, e) => MoveToTeam?.Invoke(this, EventArgs.Empty));

			menu.Items.Add(transferMenuItem);
			menu.Items.Add(moveToFavMenuItem);
			menu.Items.Add(moveToOthersMenuItem);

			this.ContextMenuStrip = menu;
		}

	}
}
