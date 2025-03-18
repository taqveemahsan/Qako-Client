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

            // menuStrip (Gradient + Bold)
            menuStrip.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue base
            menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            menuStrip.RenderMode = ToolStripRenderMode.Professional;
            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1600, 40); // Taller for bold look
            menuStrip.TabIndex = 0;
            menuStrip.ItemClicked += menuStrip_ItemClicked;
            menuStrip.Padding = new Padding(15, 0, 15, 0); // Spacious padding
            menuStrip.ForeColor = Color.White; // White text for contrast

            // refreshMenuItem (With Icon)
            refreshMenuItem.Name = "refreshMenuItem";
            refreshMenuItem.Size = new Size(80, 36);
            refreshMenuItem.Text = " Clients";
            refreshMenuItem.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            refreshMenuItem.Click += refreshMenuItem_Click;

            // uploadFileMenuItem (With Icon)
            uploadFileMenuItem.Name = "uploadFileMenuItem";
            uploadFileMenuItem.Size = new Size(110, 36);
            uploadFileMenuItem.Text = " Upload File";
            uploadFileMenuItem.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            uploadFileMenuItem.Click += UploadFileMenuItem_Click;

            // backMenuItem (With Icon)
            backMenuItem.Name = "backMenuItem";
            backMenuItem.Size = new Size(70, 36);
            backMenuItem.Text = " Back";
            backMenuItem.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            backMenuItem.Click += BackMenuItem_Click;

            // splitContainer (Subtle Gradient Background)
            splitContainer.BackColor = Color.FromArgb(240, 248, 255); // Alice Blue
            splitContainer.Location = new Point(0, 40);
            splitContainer.Name = "splitContainer";
            splitContainer.Panel1.Controls.Add(ClientListBox);
            splitContainer.Panel2.Controls.Add(splitContainer1);
            splitContainer.Size = new Size(1600, 920); // Adjusted for taller menu
            splitContainer.SplitterDistance = 340; // Wider left panel
            splitContainer.SplitterWidth = 3; // Stylish splitter
            splitContainer.TabIndex = 1;

            // ClientListBox (Shadow + Vibrant)
            ClientListBox.BackColor = Color.White;
            ClientListBox.BorderStyle = BorderStyle.FixedSingle; // Subtle border
            ClientListBox.ContextMenuStrip = clientContextMenu;
            ClientListBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            ClientListBox.FormattingEnabled = true;
            ClientListBox.ItemHeight = 32; // Taller items
            ClientListBox.Location = new Point(20, 20); // More padding
            ClientListBox.Name = "ClientListBox";
            ClientListBox.Size = new Size(300, 880); // Adjusted for new height
            ClientListBox.TabIndex = 0;
            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;

            // clientContextMenu (Bold Styling)
            clientContextMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            clientContextMenu.ImageScalingSize = new Size(28, 28);
            clientContextMenu.Name = "clientContextMenu";
            clientContextMenu.Size = new Size(61, 4);
            clientContextMenu.BackColor = Color.FromArgb(240, 248, 255);
            clientContextMenu.ForeColor = Color.FromArgb(30, 144, 255);

            // splitContainer1
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Panel1.Controls.Add(ProjectListView);
            splitContainer1.Panel2.Controls.Add(listView1);
            splitContainer1.Size = new Size(1257, 920); // Adjusted for new width
            splitContainer1.SplitterDistance = 380; // Wider middle panel
            splitContainer1.SplitterWidth = 3;
            splitContainer1.TabIndex = 0;

            // ProjectListView (Shadow + Vibrant)
            ProjectListView.BackColor = Color.White;
            ProjectListView.BorderStyle = BorderStyle.FixedSingle;
            ProjectListView.ContextMenuStrip = projectContextMenu;
            ProjectListView.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            ProjectListView.FormattingEnabled = true;
            ProjectListView.ItemHeight = 32;
            ProjectListView.Location = new Point(20, 20); // More padding
            ProjectListView.Name = "ProjectListView";
            ProjectListView.Size = new Size(340, 880); // Adjusted for new height
            ProjectListView.TabIndex = 1;
            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;

            // projectContextMenu
            projectContextMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            projectContextMenu.ImageScalingSize = new Size(28, 28);
            projectContextMenu.Name = "projectContextMenu";
            projectContextMenu.Size = new Size(61, 4);
            projectContextMenu.BackColor = Color.FromArgb(240, 248, 255);
            projectContextMenu.ForeColor = Color.FromArgb(30, 144, 255);

            // listView1 (Modern + Icons)
            listView1.BackColor = Color.White;
            listView1.ContextMenuStrip = NewFolder;
            listView1.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
            listView1.FullRowSelect = true;
            listView1.GridLines = true; // Subtle grid for style
            listView1.LargeImageList = imageList1;
            listView1.Location = new Point(20, 20); // More padding
            listView1.Name = "listView1";
            listView1.Size = new Size(897, 880); // Adjusted for new width and height
            listView1.SmallImageList = imageList1;
            listView1.StateImageList = imageList1;
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.DoubleClick += ListView1_DoubleClick;
            listView1.MouseClick += listView1_MouseClick;

            // NewFolder (Context Menu)
            NewFolder.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            NewFolder.ImageScalingSize = new Size(28, 28);
            NewFolder.Name = "NewFolder";
            NewFolder.Size = new Size(61, 4);
            NewFolder.Opening += contextMenuStrip1_Opening;
            NewFolder.BackColor = Color.FromArgb(240, 248, 255);
            NewFolder.ForeColor = Color.FromArgb(30, 144, 255);

            // imageList1 (Larger Icons)
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(32, 32); // Bold icons
            imageList1.TransparentColor = Color.Transparent;

            // statusStrip (Gradient + Bold)
            statusStrip.BackColor = Color.FromArgb(30, 144, 255); // Matching menu
            statusStrip.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            statusStrip.ImageScalingSize = new Size(28, 28);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 960); // Adjusted for new height
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1600, 40); // Taller for bold look
            statusStrip.TabIndex = 2;
            statusStrip.Padding = new Padding(15, 0, 15, 0);
            statusStrip.ForeColor = Color.White;

            // statusLabel
            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.None;
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(50, 30);
            statusLabel.Text = "Ready";

            // button1 (Logout - Bold Graphics)
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.FromArgb(255, 69, 0); // Vibrant orange-red
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.Location = new Point(1440, 6); // Adjusted for new width
            button1.Name = "button1";
            button1.Size = new Size(150, 45); // Larger, eye-catching button
            button1.TabIndex = 3;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 60, 0); // Hover effect

            // MainForm (Rich Background)
            BackColor = Color.FromArgb(240, 248, 255); // Alice Blue for vibrancy
            ClientSize = new Size(1600, 1000); // Requested size
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
        private SplitContainer splitContainer;
        private MenuStrip menuStrip;
        private ToolStripMenuItem refreshMenuItem, uploadFileMenuItem, backMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ContextMenuStrip clientContextMenu;
        private ContextMenuStrip projectContextMenu;
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

//            // menuStrip
//            menuStrip.BackColor = Color.FromArgb(245, 245, 245); // Softer gray
//            menuStrip.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular);
//            menuStrip.RenderMode = ToolStripRenderMode.Professional; // Smooth rendering
//            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
//            menuStrip.Location = new Point(0, 0);
//            menuStrip.Name = "menuStrip";
//            menuStrip.Size = new Size(1600, 36); // Slightly taller for elegance
//            menuStrip.TabIndex = 0;
//            menuStrip.ItemClicked += menuStrip_ItemClicked;
//            menuStrip.Padding = new Padding(10, 0, 10, 0); // Better spacing

//            // refreshMenuItem
//            refreshMenuItem.Name = "refreshMenuItem";
//            refreshMenuItem.Size = new Size(65, 32);
//            refreshMenuItem.Text = "Clients";
//            refreshMenuItem.Click += refreshMenuItem_Click;

//            // uploadFileMenuItem
//            uploadFileMenuItem.Name = "uploadFileMenuItem";
//            uploadFileMenuItem.Size = new Size(97, 32);
//            uploadFileMenuItem.Text = "Upload File";
//            uploadFileMenuItem.Click += UploadFileMenuItem_Click;

//            // backMenuItem
//            backMenuItem.Name = "backMenuItem";
//            backMenuItem.Size = new Size(53, 32);
//            backMenuItem.Text = "Back";
//            backMenuItem.Click += BackMenuItem_Click;

//            // splitContainer
//            splitContainer.BackColor = Color.FromArgb(250, 250, 250); // Cleaner white
//            splitContainer.Location = new Point(0, 36);
//            splitContainer.Name = "splitContainer";
//            splitContainer.Panel1.Controls.Add(ClientListBox);
//            splitContainer.Panel2.Controls.Add(splitContainer1);
//            splitContainer.Size = new Size(1600, 928); // Adjusted for taller menu
//            splitContainer.SplitterDistance = 320; // Slightly wider left panel
//            splitContainer.SplitterWidth = 2; // Ultra-thin splitter
//            splitContainer.TabIndex = 1;

//            // ClientListBox
//            ClientListBox.BackColor = Color.White;
//            ClientListBox.BorderStyle = BorderStyle.None;
//            ClientListBox.ContextMenuStrip = clientContextMenu;
//            ClientListBox.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
//            ClientListBox.FormattingEnabled = true;
//            ClientListBox.ItemHeight = 28; // Taller items for elegance
//            ClientListBox.Location = new Point(16, 16); // More padding
//            ClientListBox.Name = "ClientListBox";
//            ClientListBox.Size = new Size(288, 896); // Adjusted for new height
//            ClientListBox.TabIndex = 0;
//            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
//            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;

//            // clientContextMenu
//            clientContextMenu.Font = new Font("Segoe UI", 11F);
//            clientContextMenu.ImageScalingSize = new Size(24, 24);
//            clientContextMenu.Name = "clientContextMenu";
//            clientContextMenu.Size = new Size(61, 4);
//            clientContextMenu.BackColor = Color.FromArgb(245, 245, 245); // Matching menu

//            // splitContainer1
//            splitContainer1.Dock = DockStyle.Fill;
//            splitContainer1.Location = new Point(0, 0);
//            splitContainer1.Name = "splitContainer1";
//            splitContainer1.Panel1.Controls.Add(ProjectListView);
//            splitContainer1.Panel2.Controls.Add(listView1);
//            splitContainer1.Size = new Size(1278, 928); // Adjusted for new width
//            splitContainer1.SplitterDistance = 360; // Wider middle panel
//            splitContainer1.SplitterWidth = 2;
//            splitContainer1.TabIndex = 0;

//            // ProjectListView
//            ProjectListView.BackColor = Color.White;
//            ProjectListView.BorderStyle = BorderStyle.None;
//            ProjectListView.ContextMenuStrip = projectContextMenu;
//            ProjectListView.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
//            ProjectListView.FormattingEnabled = true;
//            ProjectListView.ItemHeight = 28;
//            ProjectListView.Location = new Point(16, 16); // More padding
//            ProjectListView.Name = "ProjectListView";
//            ProjectListView.Size = new Size(328, 896); // Adjusted for new height
//            ProjectListView.TabIndex = 1;
//            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
//            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;

//            // projectContextMenu
//            projectContextMenu.Font = new Font("Segoe UI", 11F);
//            projectContextMenu.ImageScalingSize = new Size(24, 24);
//            projectContextMenu.Name = "projectContextMenu";
//            projectContextMenu.Size = new Size(61, 4);
//            projectContextMenu.BackColor = Color.FromArgb(245, 245, 245);

//            // listView1
//            listView1.BackColor = Color.White;
//            listView1.ContextMenuStrip = NewFolder;
//            listView1.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            listView1.FullRowSelect = true;
//            listView1.GridLines = false;
//            listView1.LargeImageList = imageList1;
//            listView1.Location = new Point(16, 16); // More padding
//            listView1.Name = "listView1";
//            listView1.Size = new Size(896, 896); // Adjusted for new width and height
//            listView1.SmallImageList = imageList1;
//            listView1.StateImageList = imageList1;
//            listView1.TabIndex = 0;
//            listView1.UseCompatibleStateImageBehavior = false;
//            listView1.View = View.Details;
//            listView1.DoubleClick += ListView1_DoubleClick;
//            listView1.MouseClick += listView1_MouseClick;

//            // NewFolder (Context Menu)
//            NewFolder.Font = new Font("Segoe UI", 11F);
//            NewFolder.ImageScalingSize = new Size(24, 24);
//            NewFolder.Name = "NewFolder";
//            NewFolder.Size = new Size(61, 4);
//            NewFolder.Opening += contextMenuStrip1_Opening;
//            NewFolder.BackColor = Color.FromArgb(245, 245, 245);

//            // imageList1
//            imageList1.ColorDepth = ColorDepth.Depth32Bit;
//            imageList1.ImageSize = new Size(28, 28); // Slightly larger icons
//            imageList1.TransparentColor = Color.Transparent;

//            // statusStrip
//            statusStrip.BackColor = Color.FromArgb(245, 245, 245);
//            statusStrip.Font = new Font("Segoe UI", 11F);
//            statusStrip.ImageScalingSize = new Size(24, 24);
//            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
//            statusStrip.Location = new Point(0, 964); // Adjusted for new height
//            statusStrip.Name = "statusStrip";
//            statusStrip.Size = new Size(1600, 36); // Taller for elegance
//            statusStrip.TabIndex = 2;
//            statusStrip.Padding = new Padding(10, 0, 10, 0); // Better spacing

//            // statusLabel
//            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.None;
//            statusLabel.Name = "statusLabel";
//            statusLabel.Size = new Size(43, 25);
//            statusLabel.Text = "Ready";

//            // button1 (Logout)
//            button1.FlatStyle = FlatStyle.Flat;
//            button1.BackColor = Color.FromArgb(0, 120, 215); // Windows 11 blue
//            button1.ForeColor = Color.White;
//            button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold); // Bold for emphasis
//            button1.Location = new Point(1460, 6); // Adjusted for new width
//            button1.Name = "button1";
//            button1.Size = new Size(130, 40); // Larger button
//            button1.TabIndex = 3;
//            button1.Text = "Logout";
//            button1.UseVisualStyleBackColor = false;
//            button1.Click += button1_Click;
//            button1.FlatAppearance.BorderSize = 0; // No border for modern look
//            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 195); // Hover effect

//            // MainForm
//            BackColor = Color.FromArgb(250, 250, 250); // Cleaner white
//            ClientSize = new Size(1600, 1000); // Requested size
//            Controls.Add(button1);
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

//            // menuStrip
//            menuStrip.BackColor = Color.FromArgb(243, 243, 243); // Light gray (Windows 11 style)
//            menuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
//            menuStrip.RenderMode = ToolStripRenderMode.System;
//            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
//            menuStrip.Location = new Point(0, 0);
//            menuStrip.Name = "menuStrip";
//            menuStrip.Size = new Size(1600, 32); // Adjusted for new width
//            menuStrip.TabIndex = 0;
//            menuStrip.ItemClicked += menuStrip_ItemClicked;

//            // refreshMenuItem
//            refreshMenuItem.Name = "refreshMenuItem";
//            refreshMenuItem.Size = new Size(65, 28);
//            refreshMenuItem.Text = "Clients";
//            refreshMenuItem.Click += refreshMenuItem_Click;

//            // uploadFileMenuItem
//            uploadFileMenuItem.Name = "uploadFileMenuItem";
//            uploadFileMenuItem.Size = new Size(97, 28);
//            uploadFileMenuItem.Text = "Upload File";
//            uploadFileMenuItem.Click += UploadFileMenuItem_Click;

//            // backMenuItem
//            backMenuItem.Name = "backMenuItem";
//            backMenuItem.Size = new Size(53, 28);
//            backMenuItem.Text = "Back";
//            backMenuItem.Click += BackMenuItem_Click;

//            // splitContainer
//            splitContainer.BackColor = Color.FromArgb(249, 249, 249); // Subtle off-white
//            splitContainer.Location = new Point(0, 32);
//            splitContainer.Name = "splitContainer";
//            splitContainer.Panel1.Controls.Add(ClientListBox);
//            splitContainer.Panel2.Controls.Add(splitContainer1);
//            splitContainer.Size = new Size(1600, 936); // Adjusted for 1600x1000 (minus menu and status strip)
//            splitContainer.SplitterDistance = 300; // Wider left panel
//            splitContainer.SplitterWidth = 4;
//            splitContainer.TabIndex = 1;

//            // ClientListBox
//            ClientListBox.BackColor = Color.White;
//            ClientListBox.BorderStyle = BorderStyle.None;
//            ClientListBox.ContextMenuStrip = clientContextMenu;
//            ClientListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            ClientListBox.FormattingEnabled = true;
//            ClientListBox.ItemHeight = 24;
//            ClientListBox.Location = new Point(12, 12);
//            ClientListBox.Name = "ClientListBox";
//            ClientListBox.Size = new Size(276, 912); // Adjusted for larger height
//            ClientListBox.TabIndex = 0;
//            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
//            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;

//            // clientContextMenu
//            clientContextMenu.Font = new Font("Segoe UI", 10F);
//            clientContextMenu.ImageScalingSize = new Size(24, 24);
//            clientContextMenu.Name = "clientContextMenu";
//            clientContextMenu.Size = new Size(61, 4);

//            // splitContainer1
//            splitContainer1.Dock = DockStyle.Fill;
//            splitContainer1.Location = new Point(0, 0);
//            splitContainer1.Name = "splitContainer1";
//            splitContainer1.Panel1.Controls.Add(ProjectListView);
//            splitContainer1.Panel2.Controls.Add(listView1);
//            splitContainer1.Size = new Size(1296, 936); // Adjusted for larger form
//            splitContainer1.SplitterDistance = 350; // Wider middle panel
//            splitContainer1.SplitterWidth = 4;
//            splitContainer1.TabIndex = 0;

//            // ProjectListView
//            ProjectListView.BackColor = Color.White;
//            ProjectListView.BorderStyle = BorderStyle.None;
//            ProjectListView.ContextMenuStrip = projectContextMenu;
//            ProjectListView.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            ProjectListView.FormattingEnabled = true;
//            ProjectListView.ItemHeight = 24;
//            ProjectListView.Location = new Point(12, 12);
//            ProjectListView.Name = "ProjectListView";
//            ProjectListView.Size = new Size(326, 912); // Adjusted for larger height
//            ProjectListView.TabIndex = 1;
//            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
//            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;

//            // projectContextMenu
//            projectContextMenu.Font = new Font("Segoe UI", 10F);
//            projectContextMenu.ImageScalingSize = new Size(24, 24);
//            projectContextMenu.Name = "projectContextMenu";
//            projectContextMenu.Size = new Size(61, 4);

//            // listView1
//            listView1.BackColor = Color.White;
//            listView1.ContextMenuStrip = NewFolder;
//            listView1.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
//            listView1.FullRowSelect = true;
//            listView1.GridLines = false;
//            listView1.LargeImageList = imageList1;
//            listView1.Location = new Point(12, 12);
//            listView1.Name = "listView1";
//            listView1.Size = new Size(930, 912); // Adjusted for larger width and height
//            listView1.SmallImageList = imageList1;
//            listView1.StateImageList = imageList1;
//            listView1.TabIndex = 0;
//            listView1.UseCompatibleStateImageBehavior = false;
//            listView1.View = View.Details;
//            listView1.DoubleClick += ListView1_DoubleClick;
//            listView1.MouseClick += listView1_MouseClick;

//            // NewFolder
//            NewFolder.Font = new Font("Segoe UI", 10F);
//            NewFolder.ImageScalingSize = new Size(24, 24);
//            NewFolder.Name = "NewFolder";
//            NewFolder.Size = new Size(61, 4);
//            NewFolder.Opening += contextMenuStrip1_Opening;

//            // imageList1
//            imageList1.ColorDepth = ColorDepth.Depth32Bit;
//            imageList1.ImageSize = new Size(24, 24);
//            imageList1.TransparentColor = Color.Transparent;

//            // statusStrip
//            statusStrip.BackColor = Color.FromArgb(243, 243, 243);
//            statusStrip.Font = new Font("Segoe UI", 10F);
//            statusStrip.ImageScalingSize = new Size(24, 24);
//            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
//            statusStrip.Location = new Point(0, 968); // Adjusted for new height
//            statusStrip.Name = "statusStrip";
//            statusStrip.Size = new Size(1600, 32); // Adjusted for new width
//            statusStrip.TabIndex = 2;

//            // statusLabel
//            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.None;
//            statusLabel.Name = "statusLabel";
//            statusLabel.Size = new Size(43, 23);
//            statusLabel.Text = "Ready";

//            // button1 (Logout)
//            button1.FlatStyle = FlatStyle.Flat;
//            button1.BackColor = Color.FromArgb(0, 120, 215); // Windows 11 blue
//            button1.ForeColor = Color.White;
//            button1.Location = new Point(1480, 6); // Adjusted for new width
//            button1.Name = "button1";
//            button1.Size = new Size(110, 36); // Slightly larger button
//            button1.TabIndex = 3;
//            button1.Text = "Logout";
//            button1.UseVisualStyleBackColor = false;
//            button1.Click += button1_Click;

//            // MainForm
//            BackColor = Color.FromArgb(249, 249, 249); // Subtle off-white
//            ClientSize = new Size(1600, 1000); // New form size as requested
//            Controls.Add(button1);
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

//            // menuStrip
//            menuStrip.BackColor = Color.FromArgb(243, 243, 243); // Light gray (Windows 11 style)
//            menuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
//            menuStrip.RenderMode = ToolStripRenderMode.System; // Modern rendering
//            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
//            menuStrip.Location = new Point(0, 0);
//            menuStrip.Name = "menuStrip";
//            menuStrip.Size = new Size(1280, 32); // Wider menu for larger form
//            menuStrip.TabIndex = 0;
//            menuStrip.ItemClicked += menuStrip_ItemClicked;

//            // refreshMenuItem
//            refreshMenuItem.Name = "refreshMenuItem";
//            refreshMenuItem.Size = new Size(65, 28);
//            refreshMenuItem.Text = "Clients";
//            refreshMenuItem.Click += refreshMenuItem_Click;

//            // uploadFileMenuItem
//            uploadFileMenuItem.Name = "uploadFileMenuItem";
//            uploadFileMenuItem.Size = new Size(97, 28);
//            uploadFileMenuItem.Text = "Upload File";
//            uploadFileMenuItem.Click += UploadFileMenuItem_Click;

//            // backMenuItem
//            backMenuItem.Name = "backMenuItem";
//            backMenuItem.Size = new Size(53, 28);
//            backMenuItem.Text = "Back";
//            backMenuItem.Click += BackMenuItem_Click;

//            // splitContainer
//            splitContainer.BackColor = Color.FromArgb(249, 249, 249); // Subtle off-white
//            splitContainer.Location = new Point(0, 32);
//            splitContainer.Name = "splitContainer";
//            splitContainer.Panel1.Controls.Add(ClientListBox);
//            splitContainer.Panel2.Controls.Add(splitContainer1);
//            splitContainer.Size = new Size(1280, 688); // Larger height and width
//            splitContainer.SplitterDistance = 250; // Wider left panel
//            splitContainer.SplitterWidth = 4; // Thinner splitter
//            splitContainer.TabIndex = 1;

//            // ClientListBox
//            ClientListBox.BackColor = Color.White;
//            ClientListBox.BorderStyle = BorderStyle.None; // Flat look
//            ClientListBox.ContextMenuStrip = clientContextMenu;
//            ClientListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            ClientListBox.FormattingEnabled = true;
//            ClientListBox.ItemHeight = 24;
//            ClientListBox.Location = new Point(12, 12);
//            ClientListBox.Name = "ClientListBox";
//            ClientListBox.Size = new Size(226, 664); // Adjusted for larger form
//            ClientListBox.TabIndex = 0;
//            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
//            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;

//            // clientContextMenu
//            clientContextMenu.Font = new Font("Segoe UI", 10F);
//            clientContextMenu.ImageScalingSize = new Size(24, 24);
//            clientContextMenu.Name = "clientContextMenu";
//            clientContextMenu.Size = new Size(61, 4);

//            // splitContainer1
//            splitContainer1.Dock = DockStyle.Fill;
//            splitContainer1.Location = new Point(0, 0);
//            splitContainer1.Name = "splitContainer1";
//            splitContainer1.Panel1.Controls.Add(ProjectListView);
//            splitContainer1.Panel2.Controls.Add(listView1);
//            splitContainer1.Size = new Size(1026, 688); // Adjusted for larger form
//            splitContainer1.SplitterDistance = 300; // Wider middle panel
//            splitContainer1.SplitterWidth = 4;
//            splitContainer1.TabIndex = 0;

//            // ProjectListView
//            ProjectListView.BackColor = Color.White;
//            ProjectListView.BorderStyle = BorderStyle.None; // Flat look
//            ProjectListView.ContextMenuStrip = projectContextMenu;
//            ProjectListView.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            ProjectListView.FormattingEnabled = true;
//            ProjectListView.ItemHeight = 24;
//            ProjectListView.Location = new Point(12, 12);
//            ProjectListView.Name = "ProjectListView";
//            ProjectListView.Size = new Size(276, 664); // Adjusted for larger form
//            ProjectListView.TabIndex = 1;
//            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
//            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;

//            // projectContextMenu
//            projectContextMenu.Font = new Font("Segoe UI", 10F);
//            projectContextMenu.ImageScalingSize = new Size(24, 24);
//            projectContextMenu.Name = "projectContextMenu";
//            projectContextMenu.Size = new Size(61, 4);

//            // listView1
//            listView1.BackColor = Color.White;
//            listView1.ContextMenuStrip = NewFolder;
//            listView1.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
//            listView1.FullRowSelect = true;
//            listView1.GridLines = false; // Modern look without gridlines
//            listView1.LargeImageList = imageList1;
//            listView1.Location = new Point(12, 12);
//            listView1.Name = "listView1";
//            listView1.Size = new Size(698, 664); // Adjusted for larger form
//            listView1.SmallImageList = imageList1;
//            listView1.StateImageList = imageList1;
//            listView1.TabIndex = 0;
//            listView1.UseCompatibleStateImageBehavior = false;
//            listView1.View = View.Details;
//            listView1.DoubleClick += ListView1_DoubleClick;
//            listView1.MouseClick += listView1_MouseClick;

//            // NewFolder
//            NewFolder.Font = new Font("Segoe UI", 10F);
//            NewFolder.ImageScalingSize = new Size(24, 24);
//            NewFolder.Name = "NewFolder";
//            NewFolder.Size = new Size(61, 4);
//            NewFolder.Opening += contextMenuStrip1_Opening;

//            // imageList1
//            imageList1.ColorDepth = ColorDepth.Depth32Bit;
//            imageList1.ImageSize = new Size(24, 24);
//            imageList1.TransparentColor = Color.Transparent;

//            // statusStrip
//            statusStrip.BackColor = Color.FromArgb(243, 243, 243); // Light gray
//            statusStrip.Font = new Font("Segoe UI", 10F);
//            statusStrip.ImageScalingSize = new Size(24, 24);
//            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
//            statusStrip.Location = new Point(0, 720);
//            statusStrip.Name = "statusStrip";
//            statusStrip.Size = new Size(1280, 28);
//            statusStrip.TabIndex = 2;

//            // statusLabel
//            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.None; // Flat look
//            statusLabel.Name = "statusLabel";
//            statusLabel.Size = new Size(43, 23);
//            statusLabel.Text = "Ready";

//            // button1 (Logout)
//            button1.FlatStyle = FlatStyle.Flat; // Modern flat button
//            button1.BackColor = Color.FromArgb(0, 120, 215); // Windows 11 blue
//            button1.ForeColor = Color.White;
//            button1.Location = new Point(1170, 6); // Adjusted for larger form
//            button1.Name = "button1";
//            button1.Size = new Size(100, 32);
//            button1.TabIndex = 3;
//            button1.Text = "Logout";
//            button1.UseVisualStyleBackColor = false;
//            button1.Click += button1_Click;

//            // MainForm
//            BackColor = Color.FromArgb(249, 249, 249); // Subtle off-white
//            ClientSize = new Size(1280, 748); // Larger form size (1280x748)
//            Controls.Add(button1);
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














//namespace QACORDMS.Client
//{
//    partial class MainForm
//    {
//        private SplitContainer splitContainer;
//        private MenuStrip menuStrip;
//        private ToolStripMenuItem refreshMenuItem, uploadFileMenuItem;
//        private StatusStrip statusStrip;
//        private ToolStripStatusLabel statusLabel;
//        private ContextMenuStrip clientContextMenu; // New for ClientListBox
//        private ContextMenuStrip projectContextMenu; // New for ProjectListView
//        private ToolStripMenuItem backMenuItem;

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
//            menuStrip.BackColor = Color.FromArgb(240, 248, 255);
//            menuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
//            menuStrip.ImageScalingSize = new Size(28, 28);
//            menuStrip.Items.AddRange(new ToolStripItem[] { refreshMenuItem, uploadFileMenuItem, backMenuItem });
//            menuStrip.Location = new Point(0, 0);
//            menuStrip.Name = "menuStrip";
//            menuStrip.Size = new Size(1000, 27);
//            menuStrip.TabIndex = 0;
//            menuStrip.ItemClicked += menuStrip_ItemClicked;
//            // 
//            // refreshMenuItem
//            // 
//            refreshMenuItem.Name = "refreshMenuItem";
//            refreshMenuItem.Size = new Size(65, 23);
//            refreshMenuItem.Text = "Clients";
//            refreshMenuItem.Click += refreshMenuItem_Click;
//            // 
//            // uploadFileMenuItem
//            // 
//            uploadFileMenuItem.Name = "uploadFileMenuItem";
//            uploadFileMenuItem.Size = new Size(97, 23);
//            uploadFileMenuItem.Text = "Upload File";
//            uploadFileMenuItem.Click += UploadFileMenuItem_Click;
//            // 
//            // backMenuItem
//            // 
//            backMenuItem.Name = "backMenuItem";
//            backMenuItem.Size = new Size(53, 23);
//            backMenuItem.Text = "Back";
//            backMenuItem.Click += BackMenuItem_Click;
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
//            ClientListBox.ContextMenuStrip = clientContextMenu;
//            ClientListBox.Font = new Font("Segoe UI", 11F);
//            ClientListBox.FormattingEnabled = true;
//            ClientListBox.ItemHeight = 20;
//            ClientListBox.Location = new Point(10, 10);
//            ClientListBox.Name = "ClientListBox";
//            ClientListBox.Size = new Size(180, 542);
//            ClientListBox.TabIndex = 0;
//            ClientListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
//            ClientListBox.MouseDoubleClick += ClientListBox_MouseDoubleClick;
//            // 
//            // clientContextMenu
//            // 
//            clientContextMenu.Font = new Font("Segoe UI", 9F);
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
//            splitContainer1.Size = new Size(794, 560);
//            splitContainer1.SplitterDistance = 248;
//            splitContainer1.SplitterWidth = 6;
//            splitContainer1.TabIndex = 0;
//            // 
//            // ProjectListView
//            // 
//            ProjectListView.BackColor = Color.White;
//            ProjectListView.BorderStyle = BorderStyle.FixedSingle;
//            ProjectListView.ContextMenuStrip = projectContextMenu;
//            ProjectListView.Font = new Font("Segoe UI", 11F);
//            ProjectListView.FormattingEnabled = true;
//            ProjectListView.ItemHeight = 20;
//            ProjectListView.Location = new Point(10, 10);
//            ProjectListView.Name = "ProjectListView";
//            ProjectListView.Size = new Size(230, 542);
//            ProjectListView.TabIndex = 1;
//            ProjectListView.SelectedIndexChanged += ProjectListView_SelectedIndexChanged;
//            ProjectListView.MouseDoubleClick += ProjectListView_MouseDoubleClick;
//            // 
//            // projectContextMenu
//            // 
//            projectContextMenu.Font = new Font("Segoe UI", 9F);
//            projectContextMenu.ImageScalingSize = new Size(28, 28);
//            projectContextMenu.Name = "projectContextMenu";
//            projectContextMenu.Size = new Size(61, 4);
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
//            listView1.DoubleClick += ListView1_DoubleClick;
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
//            statusStrip.Location = new Point(0, 616);
//            statusStrip.Name = "statusStrip";
//            statusStrip.Size = new Size(1000, 24);
//            statusStrip.TabIndex = 2;
//            // 
//            // statusLabel
//            // 
//            statusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
//            statusLabel.Name = "statusLabel";
//            statusLabel.Size = new Size(43, 19);
//            statusLabel.Text = "Ready";
//            // 
//            // button1
//            // 
//            button1.Location = new Point(913, 12);
//            button1.Name = "button1";
//            button1.Size = new Size(75, 23);
//            button1.TabIndex = 3;
//            button1.Text = "Logout";
//            button1.UseVisualStyleBackColor = true;
//            button1.Click += button1_Click;
//            // 
//            // MainForm
//            // 
//            BackColor = Color.FromArgb(245, 245, 245);
//            ClientSize = new Size(1000, 640);
//            Controls.Add(button1);
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
//    }
//}














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
