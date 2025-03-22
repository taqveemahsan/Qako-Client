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
            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem });
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
            // projectLabel
            // 
            projectLabel.AutoSize = true;
            projectLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            projectLabel.ForeColor = Color.FromArgb(0, 102, 204);
            projectLabel.Location = new Point(15, 78);
            projectLabel.Name = "projectLabel";
            projectLabel.Size = new Size(88, 28);
            projectLabel.TabIndex = 10;
            projectLabel.Text = "Projects";
            // 
            // clientsViewBox
            // 
            clientsViewBox.BackColor = Color.White;
            clientsViewBox.Font = new Font("Segoe UI", 11F);
            clientsViewBox.FullRowSelect = true;
            clientsViewBox.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            clientsViewBox.Location = new Point(15, 151);
            clientsViewBox.MultiSelect = false;
            clientsViewBox.Name = "clientsViewBox";
            clientsViewBox.Size = new Size(280, 789);
            clientsViewBox.TabIndex = 2;
            clientsViewBox.UseCompatibleStateImageBehavior = false;
            clientsViewBox.View = View.Details;
            clientsViewBox.SelectedIndexChanged += ClientsViewBox_SelectedIndexChanged;
            clientsViewBox.DoubleClick += ClientsViewBox_DoubleClick;
            // 
            // projectComboBox
            // 
            projectComboBox.BackColor = Color.FromArgb(245, 245, 245);
            projectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            projectComboBox.FlatStyle = FlatStyle.Flat;
            projectComboBox.Font = new Font("Segoe UI", 12F);
            projectComboBox.Location = new Point(15, 109);
            projectComboBox.Name = "projectComboBox";
            projectComboBox.Size = new Size(1560, 36);
            projectComboBox.TabIndex = 1;
            projectComboBox.SelectedIndexChanged += ProjectComboBox_SelectedIndexChanged;
            // 
            // listView1
            // 
            listView1.Alignment = ListViewAlignment.SnapToGrid;
            listView1.BackColor = Color.White;
            listView1.BorderStyle = BorderStyle.None;
            listView1.ContextMenuStrip = NewFolder;
            listView1.Font = new Font("Segoe UI", 12F);
            listView1.FullRowSelect = true;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.LargeImageList = imageList1;
            listView1.Location = new Point(305, 151);
            listView1.Name = "listView1";
            listView1.Size = new Size(1270, 789);
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
            button1.BackColor = Color.FromArgb(0, 102, 204);
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1435, 44);
            button1.Name = "button1";
            button1.Size = new Size(140, 56);
            button1.TabIndex = 4;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(0, 102, 204);
            backButton.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            backButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            backButton.ForeColor = Color.White;
            backButton.Location = new Point(1289, 44);
            backButton.Name = "backButton";
            backButton.Size = new Size(140, 56);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Google Drive Explorer";
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


//namespace QACORDMS.Client
//{
//    partial class MainForm
//    {
//        private void InitializeComponent()
//        {
//            components = new System.ComponentModel.Container();
//            menuStrip = new MenuStrip();
//            refreshMenuItem = new ToolStripMenuItem();
//            uploadFileMenuItem = new ToolStripMenuItem();
//            backMenuItem = new ToolStripMenuItem();
//            splitContainer = new SplitContainer();
//            ClientListBox = new ListBox();
//            clientContextMenu = new ContextMenuStrip(components);
//            splitContainer1 = new SplitContainer();
//            ProjectListView = new ListBox();
//            projectContextMenu = new ContextMenuStrip(components);
//            listView1 = new ListView();
//            NewFolder = new ContextMenuStrip(components);
//            imageList1 = new ImageList(components);
//            statusStrip = new StatusStrip();
//            statusLabel = new ToolStripStatusLabel();
//            button1 = new Button();
//            menuStrip.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
//            splitContainer.Panel1.SuspendLayout();
//            splitContainer.Panel2.SuspendLayout();
//            splitContainer.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
//            splitContainer1.Panel1.SuspendLayout();
//            splitContainer1.Panel2.SuspendLayout();
//            splitContainer1.SuspendLayout();
//            statusStrip.SuspendLayout();
//            SuspendLayout();
//            // 
//            // menuStrip
//            // 
//            menuStrip.BackColor = Color.FromArgb(30, 144, 255);
//            menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
//            menuStrip.ForeColor = Color.White;
//            menuStrip.ImageScalingSize = new Size(28, 28);
//            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
//            menuStrip.Location = new Point(0, 0);
//            menuStrip.Name = "menuStrip";
//            menuStrip.Padding = new Padding(15, 0, 15, 0);
//            menuStrip.RenderMode = ToolStripRenderMode.Professional;
//            menuStrip.Size = new Size(1600, 40);
//            menuStrip.TabIndex = 0;
//            menuStrip.ItemClicked += menuStrip_ItemClicked;
//            // 
//            // refreshMenuItem
//            // 
//            refreshMenuItem.Name = "refreshMenuItem";
//            refreshMenuItem.Size = new Size(121, 40);
//            refreshMenuItem.Text = " Clients";
//            refreshMenuItem.Click += refreshMenuItem_Click;
//            // 
//            // uploadFileMenuItem
//            // 
//            uploadFileMenuItem.Name = "uploadFileMenuItem";
//            uploadFileMenuItem.Size = new Size(177, 40);
//            uploadFileMenuItem.Text = " Upload File";
//            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
//            // 
//            // backMenuItem
//            // 
//            backMenuItem.Name = "backMenuItem";
//            backMenuItem.Size = new Size(98, 40);
//            backMenuItem.Text = " Back";
//            backMenuItem.Click += BackMenuItem_Click;
//            // 
//            // splitContainer
//            // 
//            splitContainer.BackColor = Color.FromArgb(240, 248, 255);
//            splitContainer.Location = new Point(0, 40);
//            splitContainer.Name = "splitContainer";
//            // 
//            // splitContainer.Panel1
//            // 
//            splitContainer.Panel1.Controls.Add(ClientListBox);
//            // 
//            // splitContainer.Panel2
//            // 
//            splitContainer.Panel2.Controls.Add(splitContainer1);
//            splitContainer.Size = new Size(1600, 920);
//            splitContainer.SplitterDistance = 340;
//            splitContainer.SplitterWidth = 3;
//            splitContainer.TabIndex = 1;
//            // 
//            // ClientListBox
//            // 
//            ClientListBox.BackColor = Color.White;
//            ClientListBox.BorderStyle = BorderStyle.FixedSingle;
//            ClientListBox.ContextMenuStrip = clientContextMenu;
//            ClientListBox.Font = new Font("Segoe UI", 14F);
//            ClientListBox.FormattingEnabled = true;
//            ClientListBox.ItemHeight = 45;
//            ClientListBox.Location = new Point(12, 20);
//            ClientListBox.Name = "ClientListBox";
//            ClientListBox.Size = new Size(325, 857);
//            ClientListBox.TabIndex = 0;
//            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
//            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;
//            // 
//            // clientContextMenu
//            // 
//            clientContextMenu.BackColor = Color.FromArgb(240, 248, 255);
//            clientContextMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            clientContextMenu.ForeColor = Color.FromArgb(30, 144, 255);
//            clientContextMenu.ImageScalingSize = new Size(28, 28);
//            clientContextMenu.Name = "clientContextMenu";
//            clientContextMenu.Size = new Size(61, 4);
//            // 
//            // splitContainer1
//            // 
//            splitContainer1.Dock = DockStyle.Fill;
//            splitContainer1.Location = new Point(0, 0);
//            splitContainer1.Name = "splitContainer1";
//            // 
//            // splitContainer1.Panel1
//            // 
//            splitContainer1.Panel1.Controls.Add(ProjectListView);
//            // 
//            // splitContainer1.Panel2
//            // 
//            splitContainer1.Panel2.Controls.Add(listView1);
//            splitContainer1.Panel2.Controls.Add(button1);
//            splitContainer1.Size = new Size(1257, 920);
//            splitContainer1.SplitterDistance = 380;
//            splitContainer1.SplitterWidth = 3;
//            splitContainer1.TabIndex = 0;
//            // 
//            // ProjectListView
//            // 
//            ProjectListView.BackColor = Color.White;
//            ProjectListView.BorderStyle = BorderStyle.FixedSingle;
//            ProjectListView.ContextMenuStrip = projectContextMenu;
//            ProjectListView.Font = new Font("Segoe UI", 14F);
//            ProjectListView.FormattingEnabled = true;
//            ProjectListView.ItemHeight = 45;
//            ProjectListView.Location = new Point(3, 20);
//            ProjectListView.Name = "ProjectListView";
//            ProjectListView.Size = new Size(374, 857);
//            ProjectListView.TabIndex = 1;
//            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
//            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;
//            // 
//            // projectContextMenu
//            // 
//            projectContextMenu.BackColor = Color.FromArgb(240, 248, 255);
//            projectContextMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            projectContextMenu.ForeColor = Color.FromArgb(30, 144, 255);
//            projectContextMenu.ImageScalingSize = new Size(28, 28);
//            projectContextMenu.Name = "projectContextMenu";
//            projectContextMenu.Size = new Size(61, 4);
//            // 
//            // listView1
//            // 
//            listView1.BackColor = Color.White;
//            listView1.ContextMenuStrip = NewFolder;
//            listView1.Font = new Font("Segoe UI", 13F);
//            listView1.FullRowSelect = true;
//            listView1.GridLines = true;
//            listView1.LargeImageList = imageList1;
//            listView1.Location = new Point(3, 69);
//            listView1.Name = "listView1";
//            listView1.Size = new Size(859, 808);
//            listView1.SmallImageList = imageList1;
//            listView1.StateImageList = imageList1;
//            listView1.TabIndex = 0;
//            listView1.UseCompatibleStateImageBehavior = false;
//            listView1.View = View.Details;
//            listView1.DoubleClick += ListView1_DoubleClick;
//            listView1.MouseClick += listView1_MouseClick;
//            // 
//            // NewFolder
//            // 
//            NewFolder.BackColor = Color.FromArgb(240, 248, 255);
//            NewFolder.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            NewFolder.ForeColor = Color.FromArgb(30, 144, 255);
//            NewFolder.ImageScalingSize = new Size(28, 28);
//            NewFolder.Name = "NewFolder";
//            NewFolder.Size = new Size(61, 4);
//            NewFolder.Opening += contextMenuStrip1_Opening;
//            // 
//            // imageList1
//            // 
//            imageList1.ColorDepth = ColorDepth.Depth32Bit;
//            imageList1.ImageSize = new Size(32, 32);
//            imageList1.TransparentColor = Color.Transparent;
//            // 
//            // statusStrip
//            // 
//            statusStrip.BackColor = Color.FromArgb(30, 144, 255);
//            statusStrip.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            statusStrip.ForeColor = Color.White;
//            statusStrip.ImageScalingSize = new Size(28, 28);
//            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
//            statusStrip.Location = new Point(0, 953);
//            statusStrip.Name = "statusStrip";
//            statusStrip.Padding = new Padding(15, 0, 15, 0);
//            statusStrip.Size = new Size(1600, 47);
//            statusStrip.TabIndex = 2;
//            // 
//            // statusLabel
//            // 
//            statusLabel.Name = "statusLabel";
//            statusLabel.Size = new Size(96, 38);
//            statusLabel.Text = "Ready";
//            // 
//            // button1
//            // 
//            button1.BackColor = Color.FromArgb(255, 69, 0);
//            button1.FlatAppearance.BorderSize = 0;
//            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 60, 0);
//            button1.FlatStyle = FlatStyle.Flat;
//            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            button1.ForeColor = Color.White;
//            button1.Location = new Point(712, 15);
//            button1.Name = "button1";
//            button1.Size = new Size(150, 48);
//            button1.TabIndex = 3;
//            button1.Text = "Logout";
//            button1.UseVisualStyleBackColor = false;
//            button1.Click += button1_Click;
//            // 
//            // MainForm
//            // 
//            BackColor = Color.FromArgb(240, 248, 255);
//            ClientSize = new Size(1600, 1000);
//            Controls.Add(menuStrip);
//            Controls.Add(splitContainer);
//            Controls.Add(statusStrip);
//            FormBorderStyle = FormBorderStyle.FixedSingle;
//            MainMenuStrip = menuStrip;
//            MaximizeBox = false;
//            Name = "MainForm";
//            StartPosition = FormStartPosition.CenterScreen;
//            Text = "Google Drive Explorer";
//            menuStrip.ResumeLayout(false);
//            menuStrip.PerformLayout();
//            splitContainer.Panel1.ResumeLayout(false);
//            splitContainer.Panel2.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
//            splitContainer.ResumeLayout(false);
//            splitContainer1.Panel1.ResumeLayout(false);
//            splitContainer1.Panel2.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
//            splitContainer1.ResumeLayout(false);
//            statusStrip.ResumeLayout(false);
//            statusStrip.PerformLayout();
//            ResumeLayout(false);
//            PerformLayout();
//        }

//        private ListBox ClientListBox;
//        private SplitContainer splitContainer1;
//        private ListBox ProjectListView;
//        private ListView listView1;
//        private ContextMenuStrip NewFolder;
//        private System.ComponentModel.IContainer components;
//        private ImageList imageList1;
//        private Button button1;
//        private SplitContainer splitContainer;
//        private MenuStrip menuStrip;
//        private ToolStripMenuItem refreshMenuItem, uploadFileMenuItem, backMenuItem;
//        private StatusStrip statusStrip;
//        private ToolStripStatusLabel statusLabel;
//        private ContextMenuStrip clientContextMenu;
//        private ContextMenuStrip projectContextMenu;
//    }
//}
