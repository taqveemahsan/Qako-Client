namespace QACORDMS.Client
{
    partial class FolderStructureForm
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
        /// 

        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            refreshMenuItem = new ToolStripMenuItem();
            folderListView = new ListView();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            closeButton = new Button();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(0, 102, 204);
            menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            menuStrip.ForeColor = Color.White;
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(10, 2, 10, 2);
            menuStrip.RenderMode = ToolStripRenderMode.Professional;
            menuStrip.Size = new Size(800, 33);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // refreshMenuItem
            // 
            refreshMenuItem.Name = "refreshMenuItem";
            refreshMenuItem.Size = new Size(92, 29);
            refreshMenuItem.Text = "Refresh";
            refreshMenuItem.Click += RefreshMenuItem_Click;
            // 
            // folderListView
            // 
            folderListView.BackColor = Color.White;
            folderListView.Font = new Font("Segoe UI", 11F);
            folderListView.FullRowSelect = true;
            folderListView.HeaderStyle = ColumnHeaderStyle.Clickable; // Make headers clickable
            folderListView.Location = new Point(12, 40);
            folderListView.Name = "folderListView";
            folderListView.Size = new Size(776, 424);
            folderListView.TabIndex = 1;
            folderListView.UseCompatibleStateImageBehavior = false;
            folderListView.View = View.Details;
            // Add columns
            folderListView.Columns.Add("Folder Name", 150);
            folderListView.Columns.Add("Client Name", 150);
            folderListView.Columns.Add("Client Type", 120);
            folderListView.Columns.Add("Google Drive Folder ID", 200);
            folderListView.Columns.Add("Created On", 150);
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(0, 102, 204);
            statusStrip.Font = new Font("Segoe UI", 11F);
            statusStrip.ForeColor = Color.White;
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 519);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(10, 0, 10, 0);
            statusStrip.Size = new Size(800, 31);
            statusStrip.TabIndex = 2;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(62, 25);
            statusLabel.Text = "Ready";
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.FromArgb(0, 102, 204);
            closeButton.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(648, 470);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(140, 36);
            closeButton.TabIndex = 3;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += CloseButton_Click;
            // 
            // FolderStructureForm
            // 
            BackColor = Color.FromArgb(173, 216, 230);
            ClientSize = new Size(800, 550);
            Controls.Add(menuStrip);
            Controls.Add(folderListView);
            Controls.Add(statusStrip);
            Controls.Add(closeButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FolderStructureForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Folder Structure";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}