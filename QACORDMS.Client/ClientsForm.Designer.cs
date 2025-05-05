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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            clientsGridView = new DataGridView();
            addProjectButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn = new DataGridViewButtonColumn();
            addClientButton = new Button();
            searchTextBox = new TextBox();
            searchButton = new Button();
            prevButton = new Button();
            nextButton = new Button();
            pageLabel = new Label();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)clientsGridView).BeginInit();
            SuspendLayout();
            // 
            // clientsGridView
            // 
            clientsGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 250);
            clientsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            clientsGridView.BackgroundColor = Color.White;
            clientsGridView.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(33, 150, 243);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            clientsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            clientsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            clientsGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, addProjectButtonColumn, deleteButtonColumn });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            clientsGridView.DefaultCellStyle = dataGridViewCellStyle3;
            clientsGridView.Location = new Point(20, 55);
            clientsGridView.Name = "clientsGridView";
            clientsGridView.RowHeadersVisible = false;
            clientsGridView.RowHeadersWidth = 51;
            clientsGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            clientsGridView.Size = new Size(860, 429);
            clientsGridView.TabIndex = 6;
            clientsGridView.CellContentClick += clientsGridView_CellContentClick;
            clientsGridView.SizeChanged += clientsGridView_SizeChanged; // Add event handler for resizing
                                                                        // 
                                                                        // addProjectButtonColumn
                                                                        // 
            addProjectButtonColumn.MinimumWidth = 80; // Minimum width
            addProjectButtonColumn.Name = "AddProjectButton";
            addProjectButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // We'll set the width manually
                                                                                       // 
                                                                                       // deleteButtonColumn
                                                                                       // 
            deleteButtonColumn.MinimumWidth = 80; // Minimum width
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // We'll set the width manually
                                                                                   // 
                                                                                   // addClientButton
                                                                                   // 
            addClientButton.BackColor = Color.FromArgb(33, 150, 243);
            addClientButton.FlatAppearance.BorderSize = 0;
            addClientButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            addClientButton.FlatStyle = FlatStyle.Flat;
            addClientButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addClientButton.ForeColor = Color.White;
            addClientButton.Location = new Point(750, 524);
            addClientButton.Name = "addClientButton";
            addClientButton.Size = new Size(130, 40);
            addClientButton.TabIndex = 5;
            addClientButton.Text = "Add Client";
            addClientButton.UseVisualStyleBackColor = false;
            addClientButton.Click += addClientButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.BackColor = Color.White;
            searchTextBox.BorderStyle = BorderStyle.None;
            searchTextBox.Font = new Font("Segoe UI", 9F);
            searchTextBox.Location = new Point(23, 23);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search by name, email, or phone...";
            searchTextBox.Size = new Size(700, 20);
            searchTextBox.TabIndex = 4;
            // 
            // searchButton
            // 
            searchButton.BackColor = Color.FromArgb(33, 150, 243);
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            searchButton.ForeColor = Color.White;
            searchButton.Location = new Point(730, 20);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(150, 29);
            searchButton.TabIndex = 3;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = false;
            searchButton.Click += searchButton_Click;
            // 
            // prevButton
            // 
            prevButton.BackColor = Color.FromArgb(33, 150, 243);
            prevButton.FlatAppearance.BorderSize = 0;
            prevButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            prevButton.FlatStyle = FlatStyle.Flat;
            prevButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            prevButton.ForeColor = Color.White;
            prevButton.Location = new Point(20, 490);
            prevButton.Name = "prevButton";
            prevButton.Size = new Size(100, 35);
            prevButton.TabIndex = 2;
            prevButton.Text = "Previous";
            prevButton.UseVisualStyleBackColor = false;
            prevButton.Click += prevButton_Click;
            // 
            // nextButton
            // 
            nextButton.BackColor = Color.FromArgb(33, 150, 243);
            nextButton.FlatAppearance.BorderSize = 0;
            nextButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            nextButton.FlatStyle = FlatStyle.Flat;
            nextButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            nextButton.ForeColor = Color.White;
            nextButton.Location = new Point(126, 490);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(100, 35);
            nextButton.TabIndex = 1;
            nextButton.Text = "Next";
            nextButton.UseVisualStyleBackColor = false;
            nextButton.Click += nextButton_Click;
            // 
            // pageLabel
            // 
            pageLabel.Font = new Font("Segoe UI", 8F);
            pageLabel.Location = new Point(232, 490);
            pageLabel.Name = "pageLabel";
            pageLabel.Size = new Size(100, 35);
            pageLabel.TabIndex = 0;
            pageLabel.Text = "Page 1 of 1";
            pageLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Name";
            dataGridViewTextBoxColumn1.MinimumWidth = 100; // Minimum width
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // We'll set the width manually
                                                                                           // 
                                                                                           // dataGridViewTextBoxColumn2
                                                                                           // 
            dataGridViewTextBoxColumn2.HeaderText = "Email";
            dataGridViewTextBoxColumn2.MinimumWidth = 100; // Minimum width
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // We'll set the width manually
                                                                                           // 
                                                                                           // dataGridViewTextBoxColumn3
                                                                                           // 
            dataGridViewTextBoxColumn3.HeaderText = "Phone";
            dataGridViewTextBoxColumn3.MinimumWidth = 80; // Minimum width
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // We'll set the width manually
                                                                                           // 
                                                                                           // ClientsForm
                                                                                           // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(900, 600);
            Controls.Add(pageLabel);
            Controls.Add(nextButton);
            Controls.Add(prevButton);
            Controls.Add(searchButton);
            Controls.Add(searchTextBox);
            Controls.Add(addClientButton);
            Controls.Add(clientsGridView);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ClientsForm";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clients Management";
            Load += ClientsForm_Load; // Add Load event handler to set initial column widths
            ((System.ComponentModel.ISupportInitialize)clientsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewButtonColumn addProjectButtonColumn;
        private DataGridViewButtonColumn deleteButtonColumn;
    }
}
