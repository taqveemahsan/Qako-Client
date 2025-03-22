namespace QACORDMS.Client
{
    partial class ClientsForm : Form
    {
        private System.Windows.Forms.DataGridView clientsGridView;
        private System.Windows.Forms.Button addClientButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label pageLabel;

        private void InitializeComponent()
        {
            this.clientsGridView = new System.Windows.Forms.DataGridView();
            this.addClientButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.pageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.clientsGridView)).BeginInit();
            this.SuspendLayout();

            // Form Styling
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Padding = new Padding(20);
            this.Text = "Clients Management";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // clientsGridView
            this.clientsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsGridView.Location = new System.Drawing.Point(20, 70);
            this.clientsGridView.Size = new System.Drawing.Size(860, 400);
            this.clientsGridView.BackgroundColor = Color.White;
            this.clientsGridView.BorderStyle = BorderStyle.None;
            this.clientsGridView.AllowUserToAddRows = false;
            this.clientsGridView.RowHeadersVisible = false;
            this.clientsGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.clientsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clientsGridView_CellContentClick);

            this.clientsGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            this.clientsGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.clientsGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.clientsGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.clientsGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // Add Columns
            this.clientsGridView.Columns.Add("Name", "Name");
            this.clientsGridView.Columns["Name"].Width = 200;
            this.clientsGridView.Columns.Add("Email", "Email");
            this.clientsGridView.Columns["Email"].Width = 250;
            this.clientsGridView.Columns.Add("Phone", "Phone");
            this.clientsGridView.Columns["Phone"].Width = 150;

            var addProjectButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn
            {
                Name = "AddProjectButton", // Ensure name matches
                HeaderText = "Add Project",
                Text = "Add Project",
                UseColumnTextForButtonValue = true, // This ensures button text shows
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White }
            };
            this.clientsGridView.Columns.Add(addProjectButtonColumn);
            this.clientsGridView.Columns["AddProjectButton"].Width = 120;

            var deleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn
            {
                Name = "Delete", // Ensure name matches
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true, // This ensures button text shows
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White }
            };
            this.clientsGridView.Columns.Add(deleteButtonColumn);
            this.clientsGridView.Columns["Delete"].Width = 100;

            // searchTextBox
            this.searchTextBox.Location = new System.Drawing.Point(20, 20);
            this.searchTextBox.Size = new System.Drawing.Size(700, 35);
            this.searchTextBox.PlaceholderText = "Search by name, email, or phone...";
            this.searchTextBox.Font = new Font("Segoe UI", 12F);
            this.searchTextBox.BackColor = Color.White;
            this.searchTextBox.BorderStyle = BorderStyle.None;

            // searchButton
            this.searchButton.Location = new System.Drawing.Point(730, 20);
            this.searchButton.Size = new System.Drawing.Size(150, 35);
            this.searchButton.Text = "Search";
            this.searchButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.searchButton.BackColor = Color.FromArgb(33, 150, 243);
            this.searchButton.ForeColor = Color.White;
            this.searchButton.FlatStyle = FlatStyle.Flat;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);

            // addClientButton
            this.addClientButton.Location = new System.Drawing.Point(750, 540);
            this.addClientButton.Size = new System.Drawing.Size(130, 40);
            this.addClientButton.Text = "Add Client";
            this.addClientButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.addClientButton.BackColor = Color.FromArgb(33, 150, 243);
            this.addClientButton.ForeColor = Color.White;
            this.addClientButton.FlatStyle = FlatStyle.Flat;
            this.addClientButton.FlatAppearance.BorderSize = 0;
            this.addClientButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            this.addClientButton.Click += new System.EventHandler(this.addClientButton_Click);

            // prevButton
            this.prevButton.Location = new System.Drawing.Point(20, 490);
            this.prevButton.Size = new System.Drawing.Size(100, 35);
            this.prevButton.Text = "Previous";
            this.prevButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.prevButton.BackColor = Color.FromArgb(33, 150, 243);
            this.prevButton.ForeColor = Color.White;
            this.prevButton.FlatStyle = FlatStyle.Flat;
            this.prevButton.FlatAppearance.BorderSize = 0;
            this.prevButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);

            // pageLabel
            this.pageLabel.Location = new System.Drawing.Point(130, 490);
            this.pageLabel.Size = new System.Drawing.Size(200, 35);
            this.pageLabel.Text = "Page 1 of 1";
            this.pageLabel.Font = new Font("Segoe UI", 12F);
            this.pageLabel.TextAlign = ContentAlignment.MiddleCenter;

            // nextButton
            this.nextButton.Location = new System.Drawing.Point(340, 490);
            this.nextButton.Size = new System.Drawing.Size(100, 35);
            this.nextButton.Text = "Next";
            this.nextButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.nextButton.BackColor = Color.FromArgb(33, 150, 243);
            this.nextButton.ForeColor = Color.White;
            this.nextButton.FlatStyle = FlatStyle.Flat;
            this.nextButton.FlatAppearance.BorderSize = 0;
            this.nextButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);

            // Add controls to form
            this.Controls.Add(this.pageLabel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.addClientButton);
            this.Controls.Add(this.clientsGridView);

            ((System.ComponentModel.ISupportInitialize)(this.clientsGridView)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

















//namespace QACORDMS.Client
//{
//    partial class ClientsForm : Form
//    {
//        private System.Windows.Forms.DataGridView clientsGridView;
//        private System.Windows.Forms.Button addClientButton;
//        private System.Windows.Forms.TextBox searchTextBox;
//        private System.Windows.Forms.Button searchButton;
//        private System.Windows.Forms.Button prevButton;
//        private System.Windows.Forms.Button nextButton;
//        private System.Windows.Forms.Label pageLabel;

//        private void InitializeComponent()
//        {
//            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
//            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
//            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
//            clientsGridView = new DataGridView();
//            addProjectButtonColumn = new DataGridViewButtonColumn();
//            deleteButtonColumn = new DataGridViewButtonColumn();
//            addClientButton = new Button();
//            searchTextBox = new TextBox();
//            searchButton = new Button();
//            prevButton = new Button();
//            nextButton = new Button();
//            pageLabel = new Label();
//            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
//            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
//            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
//            ((System.ComponentModel.ISupportInitialize)clientsGridView).BeginInit();
//            SuspendLayout();
//            // 
//            // clientsGridView
//            // 
//            clientsGridView.AllowUserToAddRows = false;
//            dataGridViewCellStyle4.BackColor = Color.FromArgb(250, 250, 250);
//            clientsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
//            clientsGridView.BackgroundColor = Color.White;
//            clientsGridView.BorderStyle = BorderStyle.None;
//            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle5.BackColor = Color.FromArgb(33, 150, 243);
//            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
//            dataGridViewCellStyle5.ForeColor = Color.White;
//            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
//            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
//            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
//            clientsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
//            clientsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            clientsGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, addProjectButtonColumn, deleteButtonColumn });
//            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle6.BackColor = SystemColors.Window;
//            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F);
//            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
//            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
//            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
//            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
//            clientsGridView.DefaultCellStyle = dataGridViewCellStyle6;
//            clientsGridView.Location = new Point(20, 70);
//            clientsGridView.Name = "clientsGridView";
//            clientsGridView.RowHeadersVisible = false;
//            clientsGridView.RowHeadersWidth = 51;
//            clientsGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            clientsGridView.Size = new Size(860, 400);
//            clientsGridView.TabIndex = 6;
//            clientsGridView.CellContentClick += clientsGridView_CellContentClick;
//            // 
//            // addProjectButtonColumn
//            // 
//            addProjectButtonColumn.MinimumWidth = 6;
//            addProjectButtonColumn.Name = "addProjectButtonColumn";
//            addProjectButtonColumn.Width = 125;
//            // 
//            // deleteButtonColumn
//            // 
//            deleteButtonColumn.MinimumWidth = 6;
//            deleteButtonColumn.Name = "deleteButtonColumn";
//            deleteButtonColumn.Width = 125;
//            // 
//            // addClientButton
//            // 
//            addClientButton.BackColor = Color.FromArgb(33, 150, 243);
//            addClientButton.FlatAppearance.BorderSize = 0;
//            addClientButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
//            addClientButton.FlatStyle = FlatStyle.Flat;
//            addClientButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            addClientButton.ForeColor = Color.White;
//            addClientButton.Location = new Point(750, 540);
//            addClientButton.Name = "addClientButton";
//            addClientButton.Size = new Size(130, 40);
//            addClientButton.TabIndex = 5;
//            addClientButton.Text = "Add Client";
//            addClientButton.UseVisualStyleBackColor = false;
//            addClientButton.Click += addClientButton_Click;
//            // 
//            // searchTextBox
//            // 
//            searchTextBox.BackColor = Color.White;
//            searchTextBox.BorderStyle = BorderStyle.None;
//            searchTextBox.Font = new Font("Segoe UI", 12F);
//            searchTextBox.Location = new Point(20, 20);
//            searchTextBox.Name = "searchTextBox";
//            searchTextBox.PlaceholderText = "Search by name, email, or phone...";
//            searchTextBox.Size = new Size(700, 27);
//            searchTextBox.TabIndex = 4;
//            // 
//            // searchButton
//            // 
//            searchButton.BackColor = Color.FromArgb(33, 150, 243);
//            searchButton.FlatAppearance.BorderSize = 0;
//            searchButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
//            searchButton.FlatStyle = FlatStyle.Flat;
//            searchButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            searchButton.ForeColor = Color.White;
//            searchButton.Location = new Point(730, 17);
//            searchButton.Name = "searchButton";
//            searchButton.Size = new Size(150, 35);
//            searchButton.TabIndex = 3;
//            searchButton.Text = "Search";
//            searchButton.UseVisualStyleBackColor = false;
//            searchButton.Click += searchButton_Click;
//            // 
//            // prevButton
//            // 
//            prevButton.BackColor = Color.FromArgb(33, 150, 243);
//            prevButton.FlatAppearance.BorderSize = 0;
//            prevButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
//            prevButton.FlatStyle = FlatStyle.Flat;
//            prevButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            prevButton.ForeColor = Color.White;
//            prevButton.Location = new Point(20, 503);
//            prevButton.Name = "prevButton";
//            prevButton.Size = new Size(100, 35);
//            prevButton.TabIndex = 2;
//            prevButton.Text = "Previous";
//            prevButton.UseVisualStyleBackColor = false;
//            prevButton.Click += prevButton_Click;
//            // 
//            // nextButton
//            // 
//            nextButton.BackColor = Color.FromArgb(33, 150, 243);
//            nextButton.FlatAppearance.BorderSize = 0;
//            nextButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
//            nextButton.FlatStyle = FlatStyle.Flat;
//            nextButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            nextButton.ForeColor = Color.White;
//            nextButton.Location = new Point(340, 503);
//            nextButton.Name = "nextButton";
//            nextButton.Size = new Size(100, 35);
//            nextButton.TabIndex = 1;
//            nextButton.Text = "Next";
//            nextButton.UseVisualStyleBackColor = false;
//            nextButton.Click += nextButton_Click;
//            // 
//            // pageLabel
//            // 
//            pageLabel.Font = new Font("Segoe UI", 12F);
//            pageLabel.Location = new Point(130, 503);
//            pageLabel.Name = "pageLabel";
//            pageLabel.Size = new Size(200, 35);
//            pageLabel.TabIndex = 0;
//            pageLabel.Text = "Page 1 of 1";
//            pageLabel.TextAlign = ContentAlignment.MiddleCenter;
//            // 
//            // dataGridViewTextBoxColumn1
//            // 
//            dataGridViewTextBoxColumn1.HeaderText = "Name";
//            dataGridViewTextBoxColumn1.MinimumWidth = 6;
//            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
//            dataGridViewTextBoxColumn1.Width = 125;
//            // 
//            // dataGridViewTextBoxColumn2
//            // 
//            dataGridViewTextBoxColumn2.HeaderText = "Email";
//            dataGridViewTextBoxColumn2.MinimumWidth = 6;
//            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
//            dataGridViewTextBoxColumn2.Width = 125;
//            // 
//            // dataGridViewTextBoxColumn3
//            // 
//            dataGridViewTextBoxColumn3.HeaderText = "Phone";
//            dataGridViewTextBoxColumn3.MinimumWidth = 6;
//            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
//            dataGridViewTextBoxColumn3.Width = 125;
//            // 
//            // ClientsForm
//            // 
//            BackColor = Color.FromArgb(245, 245, 245);
//            ClientSize = new Size(900, 600);
//            Controls.Add(pageLabel);
//            Controls.Add(nextButton);
//            Controls.Add(prevButton);
//            Controls.Add(searchButton);
//            Controls.Add(searchTextBox);
//            Controls.Add(addClientButton);
//            Controls.Add(clientsGridView);
//            FormBorderStyle = FormBorderStyle.FixedDialog;
//            MaximizeBox = false;
//            Name = "ClientsForm";
//            Padding = new Padding(20);
//            StartPosition = FormStartPosition.CenterScreen;
//            Text = "Clients Management";
//            ((System.ComponentModel.ISupportInitialize)clientsGridView).EndInit();
//            ResumeLayout(false);
//            PerformLayout();
//        }
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
//        private DataGridViewButtonColumn addProjectButtonColumn;
//        private DataGridViewButtonColumn deleteButtonColumn;
//    }
//}
















//namespace QACORDMS.Client
//{
//    public partial class ClientsForm : Form
//    {
//        private System.Windows.Forms.DataGridView clientsGridView;
//        private System.Windows.Forms.Button addClientButton;

//        private void InitializeComponent()
//        {
//            this.clientsGridView = new System.Windows.Forms.DataGridView();
//            this.addClientButton = new System.Windows.Forms.Button();
//            ((System.ComponentModel.ISupportInitialize)(this.clientsGridView)).BeginInit();
//            this.SuspendLayout();

//            // clientsGridView
//            this.clientsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.clientsGridView.Dock = System.Windows.Forms.DockStyle.Top;
//            this.clientsGridView.Location = new System.Drawing.Point(0, 0);
//            this.clientsGridView.Name = "clientsGridView";
//            this.clientsGridView.Size = new System.Drawing.Size(800, 400);
//            this.clientsGridView.TabIndex = 0;
//            this.clientsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clientsGridView_CellContentClick);

//            this.clientsGridView.Columns.Add("Name", "Name");
//            this.clientsGridView.Columns.Add("Email", "Email");
//            this.clientsGridView.Columns.Add("Phone", "Phone");

//            var addProjectButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn
//            {
//                Name = "AddProjectButton",
//                Text = "Add Project",
//                UseColumnTextForButtonValue = true
//            };
//            this.clientsGridView.Columns.Add(addProjectButtonColumn);

//            var deleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn
//            {
//                Name = "Delete",
//                Text = "Delete",
//                UseColumnTextForButtonValue = true
//            };
//            this.clientsGridView.Columns.Add(deleteButtonColumn);

//            // addClientButton
//            this.addClientButton.Location = new System.Drawing.Point(700, 410);
//            this.addClientButton.Name = "addClientButton";
//            this.addClientButton.Size = new System.Drawing.Size(75, 23);
//            this.addClientButton.TabIndex = 1;
//            this.addClientButton.Text = "Add Client";
//            this.addClientButton.UseVisualStyleBackColor = true;
//            this.addClientButton.Click += new System.EventHandler(this.addClientButton_Click);

//            // ClientsForm
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(800, 450);
//            this.Controls.Add(this.addClientButton);
//            this.Controls.Add(this.clientsGridView);
//            this.Name = "ClientsForm";
//            this.Text = "Clients Management";
//            ((System.ComponentModel.ISupportInitialize)(this.clientsGridView)).EndInit();
//            this.ResumeLayout(false);
//        }
//    }
//}