﻿//namespace QACORDMS.Client
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
//            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
//            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
//            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
//            dataGridViewCellStyle1.BackColor = Color.FromArgb(245, 245, 245);
//            clientsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
//            clientsGridView.BackgroundColor = Color.White;
//            clientsGridView.BorderStyle = BorderStyle.None;
//            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 120, 215);
//            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
//            dataGridViewCellStyle2.ForeColor = Color.White;
//            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
//            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
//            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
//            clientsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
//            clientsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            clientsGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, addProjectButtonColumn, deleteButtonColumn });
//            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle3.BackColor = SystemColors.Window;
//            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
//            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
//            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(173, 216, 230);
//            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
//            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
//            clientsGridView.DefaultCellStyle = dataGridViewCellStyle3;
//            clientsGridView.EnableHeadersVisualStyles = false;
//            clientsGridView.Location = new Point(20, 70);
//            clientsGridView.Name = "clientsGridView";
//            clientsGridView.RowHeadersVisible = false;
//            clientsGridView.RowHeadersWidth = 51;
//            clientsGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            clientsGridView.Size = new Size(860, 400);
//            clientsGridView.TabIndex = 7;
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
//            addClientButton.BackColor = Color.FromArgb(0, 120, 215);
//            addClientButton.FlatAppearance.BorderSize = 0;
//            addClientButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
//            addClientButton.FlatStyle = FlatStyle.Flat;
//            addClientButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            addClientButton.ForeColor = Color.White;
//            addClientButton.Location = new Point(730, 506);
//            addClientButton.Name = "addClientButton";
//            addClientButton.Size = new Size(150, 47);
//            addClientButton.TabIndex = 6;
//            addClientButton.Text = "Add Client";
//            addClientButton.UseVisualStyleBackColor = false;
//            addClientButton.Click += addClientButton_Click;
//            // 
//            // searchTextBox
//            // 
//            searchTextBox.BackColor = Color.White;
//            searchTextBox.BorderStyle = BorderStyle.None;
//            searchTextBox.Font = new Font("Segoe UI", 10F);
//            searchTextBox.ForeColor = Color.Yellow;
//            searchTextBox.Location = new Point(20, 23);
//            searchTextBox.Name = "searchTextBox";
//            searchTextBox.PlaceholderText = "Search by name, email, or phone...";
//            searchTextBox.Size = new Size(700, 23);
//            searchTextBox.TabIndex = 5;
//            // 
//            // searchButton
//            // 
//            searchButton.BackColor = Color.FromArgb(0, 120, 215);
//            searchButton.FlatAppearance.BorderSize = 0;
//            searchButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
//            searchButton.FlatStyle = FlatStyle.Flat;
//            searchButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
//            searchButton.ForeColor = Color.White;
//            searchButton.Location = new Point(730, 23);
//            searchButton.Name = "searchButton";
//            searchButton.Size = new Size(150, 27);
//            searchButton.TabIndex = 4;
//            searchButton.Text = "Search";
//            searchButton.UseVisualStyleBackColor = false;
//            searchButton.Click += searchButton_Click;
//            // 
//            // prevButton
//            // 
//            prevButton.BackColor = Color.FromArgb(0, 120, 215);
//            prevButton.FlatAppearance.BorderSize = 0;
//            prevButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
//            prevButton.FlatStyle = FlatStyle.Flat;
//            prevButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
//            prevButton.ForeColor = Color.White;
//            prevButton.Location = new Point(279, 473);
//            prevButton.Name = "prevButton";
//            prevButton.Size = new Size(77, 35);
//            prevButton.TabIndex = 3;
//            prevButton.Text = "Previous";
//            prevButton.UseVisualStyleBackColor = false;
//            prevButton.Click += prevButton_Click;
//            // 
//            // nextButton
//            // 
//            nextButton.BackColor = Color.FromArgb(0, 120, 215);
//            nextButton.FlatAppearance.BorderSize = 0;
//            nextButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
//            nextButton.FlatStyle = FlatStyle.Flat;
//            nextButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
//            nextButton.ForeColor = Color.White;
//            nextButton.Location = new Point(455, 473);
//            nextButton.Name = "nextButton";
//            nextButton.Size = new Size(61, 35);
//            nextButton.TabIndex = 2;
//            nextButton.Text = "Next";
//            nextButton.UseVisualStyleBackColor = false;
//            nextButton.Click += nextButton_Click;
//            // 
//            // pageLabel
//            // 
//            pageLabel.Font = new Font("Segoe UI", 8F);
//            pageLabel.ForeColor = Color.FromArgb(0, 120, 215);
//            pageLabel.Location = new Point(362, 473);
//            pageLabel.Name = "pageLabel";
//            pageLabel.Size = new Size(87, 35);
//            pageLabel.TabIndex = 1;
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
//            BackColor = Color.FromArgb(200, 220, 240);
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

//        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
//        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
//        private DataGridViewButtonColumn addProjectButtonColumn;
//        private DataGridViewButtonColumn deleteButtonColumn;
//    }
//}










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
            // 
            // addProjectButtonColumn
            // 
            addProjectButtonColumn.MinimumWidth = 6;
            addProjectButtonColumn.Name = "AddProjectButton";
            addProjectButtonColumn.Width = 125;
            // 
            // deleteButtonColumn
            // 
            deleteButtonColumn.MinimumWidth = 6;
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Width = 125;
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
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Email";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Phone";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;
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
