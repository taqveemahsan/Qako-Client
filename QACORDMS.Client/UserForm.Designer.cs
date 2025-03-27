namespace QACORDMS.Client
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.DataGridView usersGridView;
        private System.Windows.Forms.Button addUserButton;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewButtonColumn deleteButtonColumn;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageInfo;

        private void InitializeComponent()
        {
            usersGridView = new DataGridView();
            deleteButtonColumn = new DataGridViewButtonColumn();
            addUserButton = new Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnPrevious = new Button();
            btnNext = new Button();
            lblPageInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
            SuspendLayout();

            // txtSearch
            txtSearch.Location = new Point(40, 40);
            txtSearch.Size = new Size(300, 35);
            txtSearch.TabIndex = 1;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, email, or username";
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtSearch.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtSearch.Padding = new System.Windows.Forms.Padding(5);

            // btnSearch
            btnSearch.Location = new Point(350, 40);
            btnSearch.Size = new Size(100, 35);
            btnSearch.TabIndex = 2;
            btnSearch.Name = "btnSearch";
            btnSearch.Text = "Search";
            btnSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            btnSearch.ForeColor = System.Drawing.Color.White;
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;

            // usersGridView
            usersGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usersGridView.Columns.AddRange(new DataGridViewColumn[] {
                dataGridViewTextBoxColumn1,
                dataGridViewTextBoxColumn2,
                dataGridViewTextBoxColumn3,
                dataGridViewTextBoxColumn4,
                deleteButtonColumn });
            usersGridView.Location = new Point(40, 90);
            usersGridView.Name = "usersGridView";
            usersGridView.RowHeadersVisible = false;
            usersGridView.Size = new Size(980, 500);
            usersGridView.TabIndex = 0;
            usersGridView.BackgroundColor = System.Drawing.Color.White;
            usersGridView.GridColor = System.Drawing.Color.FromArgb(200, 200, 200);
            usersGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            usersGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            usersGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            usersGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            usersGridView.EnableHeadersVisualStyles = false;
            usersGridView.CellContentClick += usersGridView_CellContentClick;

            // dataGridViewTextBoxColumn1
            dataGridViewTextBoxColumn1.HeaderText = "Full Name";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 250;

            // dataGridViewTextBoxColumn2
            dataGridViewTextBoxColumn2.HeaderText = "Email";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 300;

            // dataGridViewTextBoxColumn3
            dataGridViewTextBoxColumn3.HeaderText = "Username";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 200;

            // dataGridViewTextBoxColumn4 (Updated for multiple roles)
            dataGridViewTextBoxColumn4.HeaderText = "Roles"; // Changed from "Role" to "Roles"
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 200; // Increased width to accommodate multiple roles

            // deleteButtonColumn
            deleteButtonColumn.HeaderText = "Action";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.MinimumWidth = 6;
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Width = 125;
            deleteButtonColumn.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // btnPrevious
            btnPrevious.Location = new Point(40, 610);
            btnPrevious.Size = new Size(100, 40);
            btnPrevious.TabIndex = 4;
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Text = "Previous";
            btnPrevious.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnPrevious.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            btnPrevious.ForeColor = System.Drawing.Color.White;
            btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;

            // btnNext
            btnNext.Location = new Point(150, 610);
            btnNext.Size = new Size(100, 40);
            btnNext.TabIndex = 5;
            btnNext.Name = "btnNext";
            btnNext.Text = "Next";
            btnNext.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnNext.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            btnNext.ForeColor = System.Drawing.Color.White;
            btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;

            // lblPageInfo
            lblPageInfo.Location = new Point(260, 610);
            lblPageInfo.Size = new Size(200, 40);
            lblPageInfo.TabIndex = 6;
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Text = "Page 1 of 1";
            lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // addUserButton
            addUserButton.Location = new Point(920, 610);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(100, 40);
            addUserButton.TabIndex = 3;
            addUserButton.Text = "Add User";
            addUserButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            addUserButton.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            addUserButton.ForeColor = System.Drawing.Color.White;
            addUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            addUserButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            addUserButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            addUserButton.UseVisualStyleBackColor = false;
            addUserButton.Click += addUserButton_Click;

            // UserForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 700);
            Controls.Add(lblPageInfo);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(addUserButton);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(usersGridView);
            Name = "UserForm";
            Text = "Users Management";
            BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}



