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
        //private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            usersGridView = new DataGridView();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            deleteButtonColumn = new DataGridViewButtonColumn();
            //dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            addUserButton = new Button();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnPrevious = new Button();
            btnNext = new Button();
            lblPageInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
            SuspendLayout();
            // 
            // usersGridView
            // 
            usersGridView.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 102, 204);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            usersGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            usersGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usersGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, deleteButtonColumn });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            usersGridView.DefaultCellStyle = dataGridViewCellStyle3;
            usersGridView.EnableHeadersVisualStyles = false;
            usersGridView.GridColor = Color.FromArgb(200, 200, 200);
            usersGridView.Location = new Point(40, 90);
            usersGridView.Name = "usersGridView";
            usersGridView.RowHeadersVisible = false;
            usersGridView.RowHeadersWidth = 51;
            usersGridView.Size = new Size(980, 514);
            usersGridView.TabIndex = 0;
            usersGridView.CellContentClick += usersGridView_CellContentClick;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Email";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Username";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Roles";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 200;
            // 
            // deleteButtonColumn
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(245, 245, 245);
            deleteButtonColumn.DefaultCellStyle = dataGridViewCellStyle2;
            deleteButtonColumn.HeaderText = "Action";
            deleteButtonColumn.MinimumWidth = 6;
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.Width = 125;
            // 
            // dataGridViewTextBoxColumn1
            // 
            //dataGridViewTextBoxColumn1.HeaderText = "Full Name";
            //dataGridViewTextBoxColumn1.MinimumWidth = 6;
            //dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            //dataGridViewTextBoxColumn1.Width = 250;
            // 
            // addUserButton
            // 
            addUserButton.BackColor = Color.FromArgb(0, 102, 204);
            addUserButton.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            addUserButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            addUserButton.FlatStyle = FlatStyle.Flat;
            addUserButton.Font = new Font("Segoe UI", 10F);
            addUserButton.ForeColor = Color.White;
            addUserButton.Location = new Point(886, 610);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(134, 48);
            addUserButton.TabIndex = 3;
            addUserButton.Text = "Add User";
            addUserButton.UseVisualStyleBackColor = false;
            addUserButton.Click += addUserButton_Click;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(245, 245, 245);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(40, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, email, or username";
            txtSearch.Size = new Size(377, 34);
            txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(0, 102, 204);
            btnSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10F);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(423, 41);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 35);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.BackColor = Color.FromArgb(0, 102, 204);
            btnPrevious.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            btnPrevious.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            btnPrevious.FlatStyle = FlatStyle.Flat;
            btnPrevious.Font = new Font("Segoe UI", 10F);
            btnPrevious.ForeColor = Color.White;
            btnPrevious.Location = new Point(40, 610);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(100, 40);
            btnPrevious.TabIndex = 4;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.FromArgb(0, 102, 204);
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            btnNext.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 10F);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(150, 610);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(100, 40);
            btnNext.TabIndex = 5;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // lblPageInfo
            // 
            lblPageInfo.Font = new Font("Segoe UI", 12F);
            lblPageInfo.ForeColor = Color.FromArgb(0, 102, 204);
            lblPageInfo.Location = new Point(260, 610);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(200, 40);
            lblPageInfo.TabIndex = 6;
            lblPageInfo.Text = "Page 1 of 1";
            lblPageInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(173, 216, 230);
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
            ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}



