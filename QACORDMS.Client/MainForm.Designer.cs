namespace QACORDMS.Client
{
    partial class MainForm
    {
        private SplitContainer splitContainer;
        private MenuStrip menuStrip;
        private ToolStripMenuItem refreshMenuItem, uploadFileMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ContextMenuStrip clientContextMenu; // New for ClientListBox
        private ContextMenuStrip projectContextMenu; // New for ProjectListView
        private ToolStripMenuItem backMenuItem;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            refreshMenuItem = new ToolStripMenuItem();
            uploadFileMenuItem = new ToolStripMenuItem();
            backMenuItem = new ToolStripMenuItem();
            splitContainer = new SplitContainer();
            ClientListBox = new ListBox();
            clientContextMenu = new ContextMenuStrip(components);
            splitContainer1 = new SplitContainer();
            ProjectListView = new ListBox();
            projectContextMenu = new ContextMenuStrip(components);
            listView1 = new ListView();
            NewFolder = new ContextMenuStrip(components);
            imageList1 = new ImageList(components);
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            button1 = new Button();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(240, 248, 255);
            menuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            menuStrip.ImageScalingSize = new Size(28, 28);
            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1000, 27);
            menuStrip.TabIndex = 0;
            menuStrip.ItemClicked += menuStrip_ItemClicked;
            // 
            // refreshMenuItem
            // 
            refreshMenuItem.Name = "refreshMenuItem";
            refreshMenuItem.Size = new Size(65, 23);
            refreshMenuItem.Text = "Clients";
            refreshMenuItem.Click += refreshMenuItem_Click;
            // 
            // uploadFileMenuItem
            // 
            uploadFileMenuItem.Name = "uploadFileMenuItem";
            uploadFileMenuItem.Size = new Size(97, 23);
            uploadFileMenuItem.Text = "Upload File";
            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
            // 
            // backMenuItem
            // 
            backMenuItem.Name = "backMenuItem";
            backMenuItem.Size = new Size(53, 23);
            backMenuItem.Text = "Back";
            backMenuItem.Click += BackMenuItem_Click;
            // 
            // splitContainer
            // 
            splitContainer.BackColor = Color.FromArgb(245, 245, 245);
            splitContainer.Location = new Point(0, 40);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(ClientListBox);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(splitContainer1);
            splitContainer.Size = new Size(1000, 560);
            splitContainer.SplitterDistance = 200;
            splitContainer.SplitterWidth = 6;
            splitContainer.TabIndex = 1;
            // 
            // ClientListBox
            // 
            ClientListBox.BackColor = Color.White;
            ClientListBox.BorderStyle = BorderStyle.FixedSingle;
            ClientListBox.ContextMenuStrip = clientContextMenu;
            ClientListBox.Font = new Font("Segoe UI", 11F);
            ClientListBox.FormattingEnabled = true;
            ClientListBox.ItemHeight = 20;
            ClientListBox.Location = new Point(10, 10);
            ClientListBox.Name = "ClientListBox";
            ClientListBox.Size = new Size(180, 542);
            ClientListBox.TabIndex = 0;
            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;
            // 
            // clientContextMenu
            // 
            clientContextMenu.Font = new Font("Segoe UI", 9F);
            clientContextMenu.ImageScalingSize = new Size(28, 28);
            clientContextMenu.Name = "clientContextMenu";
            clientContextMenu.Size = new Size(61, 4);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ProjectListView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listView1);
            splitContainer1.Size = new Size(794, 560);
            splitContainer1.SplitterDistance = 248;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // ProjectListView
            // 
            ProjectListView.BackColor = Color.White;
            ProjectListView.BorderStyle = BorderStyle.FixedSingle;
            ProjectListView.ContextMenuStrip = projectContextMenu;
            ProjectListView.Font = new Font("Segoe UI", 11F);
            ProjectListView.FormattingEnabled = true;
            ProjectListView.ItemHeight = 20;
            ProjectListView.Location = new Point(10, 10);
            ProjectListView.Name = "ProjectListView";
            ProjectListView.Size = new Size(230, 542);
            ProjectListView.TabIndex = 1;
            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;
            // 
            // projectContextMenu
            // 
            projectContextMenu.Font = new Font("Segoe UI", 9F);
            projectContextMenu.ImageScalingSize = new Size(28, 28);
            projectContextMenu.Name = "projectContextMenu";
            projectContextMenu.Size = new Size(61, 4);
            // 
            // listView1
            // 
            listView1.BackColor = Color.White;
            listView1.ContextMenuStrip = NewFolder;
            listView1.Font = new Font("Segoe UI", 10F);
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.LargeImageList = imageList1;
            listView1.Location = new Point(10, 10);
            listView1.Name = "listView1";
            listView1.Size = new Size(518, 540);
            listView1.SmallImageList = imageList1;
            listView1.StateImageList = imageList1;
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.DoubleClick += ListView1_DoubleClick;
            listView1.MouseClick += listView1_MouseClick;
            // 
            // NewFolder
            // 
            NewFolder.Font = new Font("Segoe UI", 9F);
            NewFolder.ImageScalingSize = new Size(28, 28);
            NewFolder.Name = "NewFolder";
            NewFolder.Size = new Size(61, 4);
            NewFolder.Opening += contextMenuStrip1_Opening;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(24, 24);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(240, 248, 255);
            statusStrip.Font = new Font("Segoe UI", 9F);
            statusStrip.ImageScalingSize = new Size(28, 28);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 616);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1000, 24);
            statusStrip.TabIndex = 2;
            // 
            // statusLabel
            // 
            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(43, 19);
            statusLabel.Text = "Ready";
            // 
            // button1
            // 
            button1.Location = new Point(913, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1000, 640);
            Controls.Add(button1);
            Controls.Add(menuStrip);
            Controls.Add(splitContainer);
            Controls.Add(statusStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Google Drive Explorer";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ListBox ClientListBox;
        private SplitContainer splitContainer1;
        private ListBox ProjectListView;
        private ListView listView1;
        private ContextMenuStrip NewFolder;
        private System.ComponentModel.IContainer components;
        private ImageList imageList1;
        private Button button1;
    }
}














//namespace QACORDMS.Client
//{
//    partial class MainForm
//    {
//        private SplitContainer splitContainer;
//        private MenuStrip menuStrip;
//        private ToolStripMenuItem refreshMenuItem, uploadFileMenuItem;
//        private ToolStripDropDownButton actionsDropDown; // New dropdown button
//        private StatusStrip statusStrip;
//        private ToolStripStatusLabel statusLabel;

//        private void InitializeComponent()
//        {
//            components = new System.ComponentModel.Container();
//            menuStrip = new MenuStrip();
//            refreshMenuItem = new ToolStripMenuItem();
//            uploadFileMenuItem = new ToolStripMenuItem();
//            actionsDropDown = new ToolStripDropDownButton();
//            splitContainer = new SplitContainer();
//            ClientListBox = new ListBox();
//            splitContainer1 = new SplitContainer();
//            ProjectListView = new ListBox();
//            listView1 = new ListView();
//            NewFolder = new ContextMenuStrip(components);
//            imageList1 = new ImageList(components);
//            statusStrip = new StatusStrip();
//            statusLabel = new ToolStripStatusLabel();
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
//            menuStrip.BackColor = Color.FromArgb(240, 248, 255);
//            menuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
//            menuStrip.ImageScalingSize = new Size(28, 28);
//            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, actionsDropDown });
//            menuStrip.Location = new Point(0, 0);
//            menuStrip.Name = "menuStrip";
//            menuStrip.Size = new Size(1000, 46);
//            menuStrip.TabIndex = 0;
//            menuStrip.ItemClicked += menuStrip_ItemClicked;
//            // 
//            // refreshMenuItem
//            // 
//            refreshMenuItem.Name = "refreshMenuItem";
//            refreshMenuItem.Size = new Size(201, 42);
//            refreshMenuItem.Text = "Refresh Clients";
//            refreshMenuItem.Click += refreshMenuItem_Click;
//            // 
//            // uploadFileMenuItem
//            // 
//            uploadFileMenuItem.Name = "uploadFileMenuItem";
//            uploadFileMenuItem.Size = new Size(160, 42);
//            uploadFileMenuItem.Text = "Upload File";
//            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
//            // 
//            // actionsDropDown
//            // 
//            actionsDropDown.Name = "actionsDropDown";
//            actionsDropDown.Size = new Size(121, 36);
//            actionsDropDown.Text = "Actions";
//            // 
//            // splitContainer
//            // 
//            splitContainer.BackColor = Color.FromArgb(245, 245, 245);
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
//            splitContainer.Size = new Size(1000, 560);
//            splitContainer.SplitterDistance = 200;
//            splitContainer.SplitterWidth = 6;
//            splitContainer.TabIndex = 1;
//            // 
//            // ClientListBox
//            // 
//            ClientListBox.BackColor = Color.White;
//            ClientListBox.BorderStyle = BorderStyle.FixedSingle;
//            ClientListBox.Font = new Font("Segoe UI", 11F);
//            ClientListBox.FormattingEnabled = true;
//            ClientListBox.ItemHeight = 36;
//            ClientListBox.Location = new Point(10, 10);
//            ClientListBox.Name = "ClientListBox";
//            ClientListBox.Size = new Size(180, 542);
//            ClientListBox.TabIndex = 0;
//            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
//            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;
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
//            splitContainer1.Size = new Size(794, 560);
//            splitContainer1.SplitterDistance = 248;
//            splitContainer1.SplitterWidth = 6;
//            splitContainer1.TabIndex = 0;
//            // 
//            // ProjectListView
//            // 
//            ProjectListView.BackColor = Color.White;
//            ProjectListView.BorderStyle = BorderStyle.FixedSingle;
//            ProjectListView.Font = new Font("Segoe UI", 11F);
//            ProjectListView.FormattingEnabled = true;
//            ProjectListView.ItemHeight = 36;
//            ProjectListView.Location = new Point(10, 10);
//            ProjectListView.Name = "ProjectListView";
//            ProjectListView.Size = new Size(230, 542);
//            ProjectListView.TabIndex = 1;
//            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
//            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;
//            // 
//            // listView1
//            // 
//            listView1.BackColor = Color.White;
//            listView1.ContextMenuStrip = NewFolder;
//            listView1.Font = new Font("Segoe UI", 10F);
//            listView1.FullRowSelect = true;
//            listView1.GridLines = true;
//            listView1.LargeImageList = imageList1;
//            listView1.Location = new Point(10, 10);
//            listView1.Name = "listView1";
//            listView1.Size = new Size(518, 540);
//            listView1.SmallImageList = imageList1;
//            listView1.StateImageList = imageList1;
//            listView1.TabIndex = 0;
//            listView1.UseCompatibleStateImageBehavior = false;
//            listView1.View = View.Details;
//            listView1.MouseClick += listView1_MouseClick;
//            // 
//            // NewFolder
//            // 
//            NewFolder.Font = new Font("Segoe UI", 9F);
//            NewFolder.ImageScalingSize = new Size(28, 28);
//            NewFolder.Name = "NewFolder";
//            NewFolder.Size = new Size(61, 4);
//            NewFolder.Opening += contextMenuStrip1_Opening;
//            // 
//            // imageList1
//            // 
//            imageList1.ColorDepth = ColorDepth.Depth32Bit;
//            imageList1.ImageSize = new Size(24, 24);
//            imageList1.TransparentColor = Color.Transparent;
//            // 
//            // statusStrip
//            // 
//            statusStrip.BackColor = Color.FromArgb(240, 248, 255);
//            statusStrip.Font = new Font("Segoe UI", 9F);
//            statusStrip.ImageScalingSize = new Size(28, 28);
//            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
//            statusStrip.Location = new Point(0, 597);
//            statusStrip.Name = "statusStrip";
//            statusStrip.Size = new Size(1000, 43);
//            statusStrip.TabIndex = 2;
//            // 
//            // statusLabel
//            // 
//            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
//            statusLabel.Name = "statusLabel";
//            statusLabel.Size = new Size(73, 34);
//            statusLabel.Text = "Ready";
//            // 
//            // MainForm
//            // 
//            BackColor = Color.FromArgb(245, 245, 245);
//            ClientSize = new Size(1000, 640);
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
//    }
//}














////namespace QACORDMS.Client
////{
////    partial class MainForm
////    {
////        private SplitContainer splitContainer;
////        private MenuStrip menuStrip;
////        private ToolStripMenuItem refreshMenuItem, uploadFileMenuItem;
////        private StatusStrip statusStrip;
////        private ToolStripStatusLabel statusLabel;

////        private void InitializeComponent()
////        {
////            components = new System.ComponentModel.Container();
////            menuStrip = new MenuStrip();
////            refreshMenuItem = new ToolStripMenuItem();
////            uploadFileMenuItem = new ToolStripMenuItem();
////            splitContainer = new SplitContainer();
////            ClientListBox = new ListBox();
////            splitContainer1 = new SplitContainer();
////            ProjectListView = new ListBox();
////            listView1 = new ListView();
////            NewFolder = new ContextMenuStrip(components);
////            imageList1 = new ImageList(components);
////            statusStrip = new StatusStrip();
////            statusLabel = new ToolStripStatusLabel();
////            menuStrip.SuspendLayout();
////            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
////            splitContainer.Panel1.SuspendLayout();
////            splitContainer.Panel2.SuspendLayout();
////            splitContainer.SuspendLayout();
////            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
////            splitContainer1.Panel1.SuspendLayout();
////            splitContainer1.Panel2.SuspendLayout();
////            splitContainer1.SuspendLayout();
////            statusStrip.SuspendLayout();
////            SuspendLayout();
////            // 
////            // menuStrip
////            // 
////            menuStrip.BackColor = Color.FromArgb(240, 248, 255);
////            menuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
////            menuStrip.ImageScalingSize = new Size(28, 28);
////            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem });
////            menuStrip.Location = new Point(0, 0);
////            menuStrip.Name = "menuStrip";
////            menuStrip.Size = new Size(1000, 40);
////            menuStrip.TabIndex = 0;
////            menuStrip.ItemClicked += menuStrip_ItemClicked;
////            // 
////            // refreshMenuItem
////            // 
////            refreshMenuItem.Name = "refreshMenuItem";
////            refreshMenuItem.Size = new Size(201, 36);
////            refreshMenuItem.Text = "Refresh Clients";
////            refreshMenuItem.Click += refreshMenuItem_Click;
////            // 
////            // uploadFileMenuItem
////            // 
////            uploadFileMenuItem.Name = "uploadFileMenuItem";
////            uploadFileMenuItem.Size = new Size(160, 36);
////            uploadFileMenuItem.Text = "Upload File";
////            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
////            // 
////            // splitContainer
////            // 
////            splitContainer.BackColor = Color.FromArgb(245, 245, 245);
////            splitContainer.Location = new Point(0, 40);
////            splitContainer.Name = "splitContainer";
////            // 
////            // splitContainer.Panel1
////            // 
////            splitContainer.Panel1.Controls.Add(ClientListBox);
////            // 
////            // splitContainer.Panel2
////            // 
////            splitContainer.Panel2.Controls.Add(splitContainer1);
////            splitContainer.Size = new Size(1000, 560);
////            splitContainer.SplitterDistance = 200;
////            splitContainer.SplitterWidth = 6;
////            splitContainer.TabIndex = 1;
////            // 
////            // ClientListBox
////            // 
////            ClientListBox.BackColor = Color.White;
////            ClientListBox.BorderStyle = BorderStyle.FixedSingle;
////            ClientListBox.Font = new Font("Segoe UI", 11F);
////            ClientListBox.FormattingEnabled = true;
////            ClientListBox.ItemHeight = 36;
////            ClientListBox.Location = new Point(10, 10);
////            ClientListBox.Name = "ClientListBox";
////            ClientListBox.Size = new Size(180, 542);
////            ClientListBox.TabIndex = 0;
////            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
////            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;
////            // 
////            // splitContainer1
////            // 
////            splitContainer1.Dock = DockStyle.Fill;
////            splitContainer1.Location = new Point(0, 0);
////            splitContainer1.Name = "splitContainer1";
////            // 
////            // splitContainer1.Panel1
////            // 
////            splitContainer1.Panel1.Controls.Add(ProjectListView);
////            // 
////            // splitContainer1.Panel2
////            // 
////            splitContainer1.Panel2.Controls.Add(listView1);
////            splitContainer1.Size = new Size(794, 560);
////            splitContainer1.SplitterDistance = 248;
////            splitContainer1.SplitterWidth = 6;
////            splitContainer1.TabIndex = 0;
////            // 
////            // ProjectListView
////            // 
////            ProjectListView.BackColor = Color.White;
////            ProjectListView.BorderStyle = BorderStyle.FixedSingle;
////            ProjectListView.Font = new Font("Segoe UI", 11F);
////            ProjectListView.FormattingEnabled = true;
////            ProjectListView.ItemHeight = 36;
////            ProjectListView.Location = new Point(10, 10);
////            ProjectListView.Name = "ProjectListView";
////            ProjectListView.Size = new Size(230, 542);
////            ProjectListView.TabIndex = 1;
////            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
////            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;
////            // 
////            // listView1
////            // 
////            listView1.BackColor = Color.White;
////            listView1.ContextMenuStrip = NewFolder;
////            listView1.Font = new Font("Segoe UI", 10F);
////            listView1.FullRowSelect = true;
////            listView1.GridLines = true;
////            listView1.LargeImageList = imageList1;
////            listView1.Location = new Point(10, 10);
////            listView1.Name = "listView1";
////            listView1.Size = new Size(518, 540);
////            listView1.SmallImageList = imageList1;
////            listView1.StateImageList = imageList1;
////            listView1.TabIndex = 0;
////            listView1.UseCompatibleStateImageBehavior = false;
////            listView1.View = View.Details;
////            listView1.MouseClick += listView1_MouseClick;
////            // 
////            // NewFolder
////            // 
////            NewFolder.Font = new Font("Segoe UI", 9F);
////            NewFolder.ImageScalingSize = new Size(28, 28);
////            NewFolder.Name = "NewFolder";
////            NewFolder.Size = new Size(61, 4);
////            NewFolder.Opening += contextMenuStrip1_Opening;
////            // 
////            // imageList1
////            // 
////            imageList1.ColorDepth = ColorDepth.Depth32Bit;
////            imageList1.ImageSize = new Size(24, 24);
////            imageList1.TransparentColor = Color.Transparent;
////            // 
////            // statusStrip
////            // 
////            statusStrip.BackColor = Color.FromArgb(240, 248, 255);
////            statusStrip.Font = new Font("Segoe UI", 9F);
////            statusStrip.ImageScalingSize = new Size(28, 28);
////            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
////            statusStrip.Location = new Point(0, 597);
////            statusStrip.Name = "statusStrip";
////            statusStrip.Size = new Size(1000, 43);
////            statusStrip.TabIndex = 2;
////            // 
////            // statusLabel
////            // 
////            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
////            statusLabel.Name = "statusLabel";
////            statusLabel.Size = new Size(73, 34);
////            statusLabel.Text = "Ready";
////            // 
////            // MainForm
////            // 
////            BackColor = Color.FromArgb(245, 245, 245);
////            ClientSize = new Size(1000, 640);
////            Controls.Add(menuStrip);
////            Controls.Add(splitContainer);
////            Controls.Add(statusStrip);
////            FormBorderStyle = FormBorderStyle.FixedSingle;
////            MainMenuStrip = menuStrip;
////            MaximizeBox = false;
////            Name = "MainForm";
////            StartPosition = FormStartPosition.CenterScreen;
////            Text = "Google Drive Explorer";
////            menuStrip.ResumeLayout(false);
////            menuStrip.PerformLayout();
////            splitContainer.Panel1.ResumeLayout(false);
////            splitContainer.Panel2.ResumeLayout(false);
////            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
////            splitContainer.ResumeLayout(false);
////            splitContainer1.Panel1.ResumeLayout(false);
////            splitContainer1.Panel2.ResumeLayout(false);
////            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
////            splitContainer1.ResumeLayout(false);
////            statusStrip.ResumeLayout(false);
////            statusStrip.PerformLayout();
////            ResumeLayout(false);
////            PerformLayout();
////        }

////        private ListBox ClientListBox;
////        private SplitContainer splitContainer1;
////        private ListBox ProjectListView;
////        private ListView listView1;
////        private ContextMenuStrip NewFolder;
////        private System.ComponentModel.IContainer components;
////        private ImageList imageList1;
////    }
////}













////namespace QACORDMS.Client
////{
////    partial class MainForm
////    {
////        private SplitContainer splitContainer;
////        private TreeView projectTreeView;
////        private MenuStrip menuStrip;
////        private ToolStripMenuItem refreshMenuItem, uploadMenuItem, downloadMenuItem, deleteMenuItem , uploadFileMenuItem;
////        private StatusStrip statusStrip;
////        private ToolStripStatusLabel statusLabel;

////        private void InitializeComponent()
////        {
////            components = new System.ComponentModel.Container();
////            menuStrip = new MenuStrip();
////            refreshMenuItem = new ToolStripMenuItem();
////            uploadMenuItem = new ToolStripMenuItem();
////            downloadMenuItem = new ToolStripMenuItem();
////            deleteMenuItem = new ToolStripMenuItem();
////            splitContainer = new SplitContainer();
////            ClientListBox = new ListBox();
////            splitContainer1 = new SplitContainer();
////            ProjectListView = new ListBox();
////            listView1 = new ListView();
////            NewFolder = new ContextMenuStrip(components);
////            imageList1 = new ImageList(components);
////            projectTreeView = new TreeView();
////            statusStrip = new StatusStrip();
////            statusLabel = new ToolStripStatusLabel();
////            uploadFileMenuItem = new ToolStripMenuItem();
////            menuStrip.SuspendLayout();
////            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
////            splitContainer.Panel1.SuspendLayout();
////            splitContainer.Panel2.SuspendLayout();
////            splitContainer.SuspendLayout();
////            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
////            splitContainer1.Panel1.SuspendLayout();
////            splitContainer1.Panel2.SuspendLayout();
////            splitContainer1.SuspendLayout();
////            statusStrip.SuspendLayout();
////            SuspendLayout();
////            // 
////            // menuStrip
////            // 
////            menuStrip.ImageScalingSize = new Size(28, 28);
////            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadMenuItem, downloadMenuItem, deleteMenuItem });
////            menuStrip.Location = new Point(0, 0);
////            menuStrip.Name = "menuStrip";
////            menuStrip.Size = new Size(984, 38);
////            menuStrip.TabIndex = 0;
////            menuStrip.ItemClicked += menuStrip_ItemClicked;
////            // 
////            // refreshMenuItem
////            // 
////            refreshMenuItem.Name = "refreshMenuItem";
////            refreshMenuItem.Size = new Size(93, 34);
////            refreshMenuItem.Text = "Clients";
////            refreshMenuItem.Click += refreshMenuItem_Click;
////            // 
////            // uploadMenuItem
////            // 
////            uploadMenuItem.Name = "uploadMenuItem";
////            uploadMenuItem.Size = new Size(18, 34);
////            // 
////            // downloadMenuItem
////            // 
////            downloadMenuItem.Name = "downloadMenuItem";
////            downloadMenuItem.Size = new Size(18, 34);
////            // 
////            // deleteMenuItem
////            // 
////            deleteMenuItem.Name = "deleteMenuItem";
////            deleteMenuItem.Size = new Size(18, 34);
////            // 
////            // splitContainer
////            // 
////            splitContainer.Location = new Point(0, 25);
////            splitContainer.Margin = new Padding(3, 25, 3, 3);
////            splitContainer.Name = "splitContainer";
////            // 
////            // splitContainer.Panel1
////            // 
////            splitContainer.Panel1.Controls.Add(ClientListBox);
////            // 
////            // splitContainer.Panel2
////            // 
////            splitContainer.Panel2.Controls.Add(splitContainer1);
////            splitContainer.Size = new Size(984, 536);
////            splitContainer.SplitterDistance = 128;
////            splitContainer.TabIndex = 1;
////            // 
////            // ClientListBox
////            // 
////            ClientListBox.FormattingEnabled = true;
////            ClientListBox.ItemHeight = 30;
////            ClientListBox.Location = new Point(3, 3);
////            ClientListBox.Name = "ClientListBox";
////            ClientListBox.Size = new Size(138, 514);
////            ClientListBox.TabIndex = 0;
////            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
////            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;
////            // 
////            // splitContainer1
////            // 
////            splitContainer1.Dock = DockStyle.Fill;
////            splitContainer1.Location = new Point(0, 0);
////            splitContainer1.Name = "splitContainer1";
////            // 
////            // splitContainer1.Panel1
////            // 
////            splitContainer1.Panel1.Controls.Add(ProjectListView);
////            // 
////            // splitContainer1.Panel2
////            // 
////            splitContainer1.Panel2.Controls.Add(listView1);
////            splitContainer1.Size = new Size(852, 536);
////            splitContainer1.SplitterDistance = 165;
////            splitContainer1.TabIndex = 0;
////            // 
////            // ProjectListView
////            // 
////            ProjectListView.FormattingEnabled = true;
////            ProjectListView.ItemHeight = 30;
////            ProjectListView.Location = new Point(15, 10);
////            ProjectListView.Name = "ProjectListView";
////            ProjectListView.Size = new Size(147, 514);
////            ProjectListView.TabIndex = 1;
////            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
////            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;
////            // 
////            // listView1
////            // 
////            listView1.ContextMenuStrip = NewFolder;
////            listView1.LargeImageList = imageList1;
////            listView1.Location = new Point(-1, 10);
////            listView1.Name = "listView1";
////            listView1.Size = new Size(658, 523);
////            listView1.SmallImageList = imageList1;
////            listView1.StateImageList = imageList1;
////            listView1.TabIndex = 0;
////            listView1.TileSize = new Size(1, 1);
////            listView1.UseCompatibleStateImageBehavior = false;
////            listView1.View = View.SmallIcon;
////            listView1.MouseClick += listView1_MouseClick;
////            // 
////            // NewFolder
////            // 
////            NewFolder.ImageScalingSize = new Size(28, 28);
////            NewFolder.Name = "NewFolder";
////            NewFolder.Size = new Size(61, 4);
////            NewFolder.Opening += contextMenuStrip1_Opening;
////            // 
////            // imageList1
////            // 
////            imageList1.ColorDepth = ColorDepth.Depth32Bit;
////            imageList1.ImageSize = new Size(16, 16);
////            imageList1.TransparentColor = Color.Transparent;
////            // 
////            // projectTreeView
////            // 
////            projectTreeView.LineColor = Color.Empty;
////            projectTreeView.Location = new Point(0, 0);
////            projectTreeView.Name = "projectTreeView";
////            projectTreeView.Size = new Size(121, 97);
////            projectTreeView.TabIndex = 0;
////            // 
////            // statusStrip
////            // 
////            statusStrip.ImageScalingSize = new Size(28, 28);
////            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
////            statusStrip.Location = new Point(0, 522);
////            statusStrip.Name = "statusStrip";
////            statusStrip.Size = new Size(984, 39);
////            statusStrip.TabIndex = 2;
////            // 
////            // statusLabel
////            // 
////            statusLabel.Name = "statusLabel";
////            statusLabel.Size = new Size(69, 30);
////            statusLabel.Text = "Ready";
////            // 
////            // uploadFileMenuItem
////            // 
////            uploadFileMenuItem.Name = "uploadFileMenuItem";
////            uploadFileMenuItem.Size = new Size(32, 19);
////            uploadFileMenuItem.Text = "Upload File";
////            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
////            // 
////            // MainForm
////            // 
////            ClientSize = new Size(984, 561);
////            Controls.Add(menuStrip);
////            Controls.Add(splitContainer);
////            Controls.Add(statusStrip);
////            MainMenuStrip = menuStrip;
////            Name = "MainForm";
////            StartPosition = FormStartPosition.CenterScreen;
////            Text = "Google Drive Explorer";
////            menuStrip.ResumeLayout(false);
////            menuStrip.PerformLayout();
////            splitContainer.Panel1.ResumeLayout(false);
////            splitContainer.Panel2.ResumeLayout(false);
////            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
////            splitContainer.ResumeLayout(false);
////            splitContainer1.Panel1.ResumeLayout(false);
////            splitContainer1.Panel2.ResumeLayout(false);
////            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
////            splitContainer1.ResumeLayout(false);
////            statusStrip.ResumeLayout(false);
////            statusStrip.PerformLayout();
////            ResumeLayout(false);
////            PerformLayout();
////        }

////        private ListBox ClientListBox;
////        private SplitContainer splitContainer1;
////        private ListBox ProjectListView;
////        private ListView listView1;
////        private ContextMenuStrip NewFolder;
////        private System.ComponentModel.IContainer components;
////        private ImageList imageList1;
////    }
////}
