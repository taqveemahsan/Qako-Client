namespace QACORDMS.Client
{
    partial class MainForm
    {
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            refreshMenuItem = new ToolStripMenuItem();
            uploadFileMenuItem = new ToolStripMenuItem();
            settingsMenuItem = new ToolStripMenuItem();
            projectLabel = new Label();
            clientsViewBox = new ListView();
            projectComboBox = new ComboBox();
            listView1 = new ListView();
            NewFolder = new ContextMenuStrip(components);
            imageList1 = new ImageList(components);
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            button1 = new Button();
            backButton = new Button();
            viewToolStrip = new ToolStrip();
            smallIconsButton = new ToolStripButton();
            largeIconsButton = new ToolStripButton();
            detailsButton = new ToolStripButton();
            addPermissionsButton = new Button();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            viewToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(0, 102, 204);
            menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            menuStrip.ForeColor = Color.White;
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, settingsMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(10, 2, 10, 2);
            menuStrip.RenderMode = ToolStripRenderMode.Professional;
            menuStrip.Size = new Size(1600, 33);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            menuStrip.ItemClicked += menuStrip_ItemClicked;
            // 
            // refreshMenuItem
            // 
            refreshMenuItem.Name = "refreshMenuItem";
            refreshMenuItem.Size = new Size(85, 29);
            refreshMenuItem.Text = "Clients";
            refreshMenuItem.Click += refreshMenuItem_Click;
            // 
            // uploadFileMenuItem
            // 
            uploadFileMenuItem.Name = "uploadFileMenuItem";
            uploadFileMenuItem.Size = new Size(126, 29);
            uploadFileMenuItem.Text = "Upload File";
            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
            // 
            // settingsMenuItem
            // 
            settingsMenuItem.Name = "settingsMenuItem";
            settingsMenuItem.Size = new Size(98, 29);
            settingsMenuItem.Text = "Settings";
            settingsMenuItem.Click += SettingsMenuItem_Click;
            // 
            // projectLabel
            // 
            projectLabel.AutoSize = true;
            projectLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            projectLabel.ForeColor = Color.FromArgb(0, 102, 204);
            projectLabel.Location = new Point(15, 49);
            projectLabel.Name = "projectLabel";
            projectLabel.Size = new Size(88, 28);
            projectLabel.TabIndex = 10;
            projectLabel.Text = "Projects";
            // 
            // clientsViewBox
            // 
            clientsViewBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            clientsViewBox.BackColor = Color.White;
            clientsViewBox.Font = new Font("Segoe UI", 11F);
            clientsViewBox.FullRowSelect = true;
            clientsViewBox.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            clientsViewBox.Location = new Point(15, 131);
            clientsViewBox.Name = "clientsViewBox";
            clientsViewBox.Size = new Size(280, 809);
            clientsViewBox.TabIndex = 2;
            clientsViewBox.UseCompatibleStateImageBehavior = false;
            clientsViewBox.View = View.Details;
            clientsViewBox.SelectedIndexChanged += ClientsViewBox_SelectedIndexChanged;
            clientsViewBox.DoubleClick += ClientsViewBox_DoubleClick;
            // 
            // projectComboBox
            // 
            projectComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            projectComboBox.BackColor = Color.FromArgb(245, 245, 245);
            projectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            projectComboBox.FlatStyle = FlatStyle.Flat;
            projectComboBox.Font = new Font("Segoe UI", 12F);
            projectComboBox.Location = new Point(15, 80);
            projectComboBox.Name = "projectComboBox";
            projectComboBox.Size = new Size(1337, 36);
            projectComboBox.TabIndex = 1;
            projectComboBox.SelectedIndexChanged += ProjectComboBox_SelectedIndexChanged;
            // 
            // listView1
            // 
            listView1.Alignment = ListViewAlignment.SnapToGrid;
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.BackColor = Color.White;
            listView1.BorderStyle = BorderStyle.None;
            listView1.ContextMenuStrip = NewFolder;
            listView1.Font = new Font("Segoe UI", 12F);
            listView1.FullRowSelect = true;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.LargeImageList = imageList1;
            listView1.Location = new Point(305, 131);
            listView1.Name = "listView1";
            listView1.Size = new Size(1270, 809);
            listView1.SmallImageList = imageList1;
            listView1.StateImageList = imageList1;
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.DoubleClick += ListView1_DoubleClick;
            listView1.MouseClick += listView1_MouseClick;
            // 
            // NewFolder
            // 
            NewFolder.BackColor = Color.FromArgb(173, 216, 230);
            NewFolder.Font = new Font("Segoe UI", 11F);
            NewFolder.ForeColor = Color.FromArgb(0, 102, 204);
            NewFolder.ImageScalingSize = new Size(24, 24);
            NewFolder.Name = "NewFolder";
            NewFolder.Size = new Size(61, 4);
            NewFolder.Opening += contextMenuStrip1_Opening;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(48, 48);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(0, 102, 204);
            statusStrip.Font = new Font("Segoe UI", 11F);
            statusStrip.ForeColor = Color.White;
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 969);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(10, 0, 10, 0);
            statusStrip.Size = new Size(1600, 31);
            statusStrip.TabIndex = 7;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(62, 25);
            statusLabel.Text = "Ready";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(0, 102, 204);
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1484, 36);
            button1.Name = "button1";
            button1.Size = new Size(91, 35);
            button1.TabIndex = 4;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // backButton
            // 
            backButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            backButton.BackColor = Color.FromArgb(0, 102, 204);
            backButton.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            backButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            backButton.ForeColor = Color.White;
            backButton.Location = new Point(1401, 36);
            backButton.Name = "backButton";
            backButton.Size = new Size(77, 35);
            backButton.TabIndex = 5;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += BackMenuItem_Click;
            // 
            // viewToolStrip
            // 
            viewToolStrip.BackColor = Color.FromArgb(173, 216, 230);
            viewToolStrip.Dock = DockStyle.Bottom;
            viewToolStrip.ImageScalingSize = new Size(24, 24);
            viewToolStrip.Items.AddRange(new ToolStripItem[] { smallIconsButton, largeIconsButton, detailsButton });
            viewToolStrip.Location = new Point(0, 943);
            viewToolStrip.Name = "viewToolStrip";
            viewToolStrip.RenderMode = ToolStripRenderMode.Professional;
            viewToolStrip.Size = new Size(1600, 26);
            viewToolStrip.TabIndex = 8;
            // 
            // smallIconsButton
            // 
            smallIconsButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            smallIconsButton.Margin = new Padding(5, 1, 5, 1);
            smallIconsButton.Name = "smallIconsButton";
            smallIconsButton.Size = new Size(88, 24);
            smallIconsButton.Text = "Small Icons";
            smallIconsButton.Click += smallIconsButton_Click;
            // 
            // largeIconsButton
            // 
            largeIconsButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            largeIconsButton.Margin = new Padding(5, 1, 5, 1);
            largeIconsButton.Name = "largeIconsButton";
            largeIconsButton.Size = new Size(88, 24);
            largeIconsButton.Text = "Large Icons";
            largeIconsButton.Click += largeIconsButton_Click;
            // 
            // detailsButton
            // 
            detailsButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            detailsButton.Margin = new Padding(5, 1, 5, 1);
            detailsButton.Name = "detailsButton";
            detailsButton.Size = new Size(59, 24);
            detailsButton.Text = "Details";
            detailsButton.Click += detailsButton_Click;
            // 
            // addPermissionsButton
            // 
            addPermissionsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addPermissionsButton.BackColor = Color.FromArgb(0, 102, 204);
            addPermissionsButton.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            addPermissionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            addPermissionsButton.FlatStyle = FlatStyle.Flat;
            addPermissionsButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            addPermissionsButton.ForeColor = Color.White;
            addPermissionsButton.Location = new Point(1358, 80);
            addPermissionsButton.Name = "addPermissionsButton";
            addPermissionsButton.Size = new Size(217, 36);
            addPermissionsButton.TabIndex = 6;
            addPermissionsButton.Text = "Add Permissions";
            addPermissionsButton.UseVisualStyleBackColor = false;
            addPermissionsButton.Visible = false;
            addPermissionsButton.Click += AddPermissionsButton_Click;
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(173, 216, 230);
            ClientSize = new Size(1600, 1000);
            Controls.Add(projectLabel);
            Controls.Add(viewToolStrip);
            Controls.Add(clientsViewBox);
            Controls.Add(projectComboBox);
            Controls.Add(listView1);
            Controls.Add(button1);
            Controls.Add(backButton);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            Controls.Add(addPermissionsButton);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BAKERTILLY DMS";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            viewToolStrip.ResumeLayout(false);
            viewToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label projectLabel;
        private ListView clientsViewBox;
        private ComboBox projectComboBox; // Keeping projectComboBox
        private ListView listView1;
        private ContextMenuStrip NewFolder;
        private System.ComponentModel.IContainer components;
        private ImageList imageList1;
        private Button button1;
        private Button backButton;
        private MenuStrip menuStrip;
        private ToolStripMenuItem refreshMenuItem;
        private ToolStripMenuItem uploadFileMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStrip viewToolStrip;
        private ToolStripButton smallIconsButton;
        private ToolStripButton largeIconsButton;
        private ToolStripButton detailsButton;

        private ContextMenuStrip CreateClientsContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            var refreshItem = new ToolStripMenuItem("Refresh Clients");
            refreshItem.Click += (s, e) => LoadClientsAsync();
            contextMenu.Items.Add(refreshItem);
            return contextMenu;
        }
    }
}