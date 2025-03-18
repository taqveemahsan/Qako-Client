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

            // usersGridView
            usersGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usersGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, deleteButtonColumn });
            usersGridView.Location = new Point(10, 50); // Fixed position with margin
            usersGridView.Margin = new Padding(4, 5, 4, 5);
            usersGridView.Name = "usersGridView";
            usersGridView.RowHeadersWidth = 51;
            usersGridView.Size = new Size(1047, 550); // Adjusted size to fit form
            usersGridView.TabIndex = 0;
            usersGridView.CellContentClick += usersGridView_CellContentClick;

            // deleteButtonColumn
            deleteButtonColumn.HeaderText = "Action";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.MinimumWidth = 6;
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Width = 125;

            // txtSearch
            txtSearch.Location = new Point(10, 10);
            txtSearch.Size = new Size(200, 20);
            txtSearch.TabIndex = 1;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, email, or username";

            // btnSearch
            btnSearch.Location = new Point(220, 10);
            btnSearch.Size = new Size(80, 25);
            btnSearch.TabIndex = 2;
            btnSearch.Name = "btnSearch";
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // addUserButton
            addUserButton.Location = new Point(900, 610); // Moved down and adjusted
            addUserButton.Margin = new Padding(4, 5, 4, 5);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(150, 35);
            addUserButton.TabIndex = 3;
            addUserButton.Text = "Add User";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += addUserButton_Click;

            // btnPrevious
            btnPrevious.Location = new Point(10, 610);
            btnPrevious.Size = new Size(80, 25);
            btnPrevious.TabIndex = 4;
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;

            // btnNext
            btnNext.Location = new Point(100, 610);
            btnNext.Size = new Size(80, 25);
            btnNext.TabIndex = 5;
            btnNext.Name = "btnNext";
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;

            // lblPageInfo
            lblPageInfo.Location = new Point(190, 610);
            lblPageInfo.Size = new Size(200, 25);
            lblPageInfo.TabIndex = 6;
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Text = "Page 1 of 1";
            lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

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

            // dataGridViewTextBoxColumn4
            dataGridViewTextBoxColumn4.HeaderText = "Role";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 150;

            // UserForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 660); // Adjusted height
            Controls.Add(lblPageInfo);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(addUserButton);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(usersGridView);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UserForm";
            Text = "Users Management";
            ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}













//namespace QACORDMS.Client
//{
//    partial class UserForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private System.Windows.Forms.DataGridView usersGridView;
//        private System.Windows.Forms.Button addUserButton;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
//        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
//        private DataGridViewButtonColumn deleteButtonColumn;
//        private System.Windows.Forms.TextBox txtSearch; // New: Search TextBox
//        private System.Windows.Forms.Button btnSearch; // New: Search Button
//        private System.Windows.Forms.Button btnPrevious; // New: Previous Button
//        private System.Windows.Forms.Button btnNext; // New: Next Button
//        private System.Windows.Forms.Label lblPageInfo; // New: Page Info Label

//        private void InitializeComponent()
//        {
//            usersGridView = new DataGridView();
//            deleteButtonColumn = new DataGridViewButtonColumn();
//            addUserButton = new Button();
//            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
//            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
//            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
//            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
//            txtSearch = new TextBox(); // New
//            btnSearch = new Button(); // New
//            btnPrevious = new Button(); // New
//            btnNext = new Button(); // New
//            lblPageInfo = new Label(); // New
//            ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
//            SuspendLayout();

//            // usersGridView
//            usersGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            usersGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, deleteButtonColumn });
//            usersGridView.Dock = DockStyle.Top;
//            usersGridView.Location = new Point(0, 40); // Adjusted for search bar
//            usersGridView.Margin = new Padding(4, 5, 4, 5);
//            usersGridView.Name = "usersGridView";
//            usersGridView.RowHeadersWidth = 51;
//            usersGridView.Size = new Size(1067, 575); // Height reduced to fit pagination
//            usersGridView.TabIndex = 0;
//            usersGridView.CellContentClick += usersGridView_CellContentClick;

//            // deleteButtonColumn
//            deleteButtonColumn.HeaderText = "Action";
//            deleteButtonColumn.Text = "Delete";
//            deleteButtonColumn.UseColumnTextForButtonValue = true;
//            deleteButtonColumn.MinimumWidth = 6;
//            deleteButtonColumn.Name = "Delete";
//            deleteButtonColumn.Width = 125;

//            // txtSearch (New)
//            txtSearch.Location = new Point(10, 10);
//            txtSearch.Size = new Size(200, 20);
//            txtSearch.TabIndex = 1;
//            txtSearch.Name = "txtSearch";
//            txtSearch.PlaceholderText = "Search by name, email, or username";

//            // btnSearch (New)
//            btnSearch.Location = new Point(220, 10);
//            btnSearch.Size = new Size(80, 25);
//            btnSearch.TabIndex = 2;
//            btnSearch.Name = "btnSearch";
//            btnSearch.Text = "Search";
//            btnSearch.UseVisualStyleBackColor = true;
//            btnSearch.Click += btnSearch_Click;

//            // addUserButton
//            addUserButton.Location = new Point(863, 631);
//            addUserButton.Margin = new Padding(4, 5, 4, 5);
//            addUserButton.Name = "addUserButton";
//            addUserButton.Size = new Size(170, 35);
//            addUserButton.TabIndex = 3;
//            addUserButton.Text = "Add User";
//            addUserButton.UseVisualStyleBackColor = true;
//            addUserButton.Click += addUserButton_Click;

//            // btnPrevious (New)
//            btnPrevious.Location = new Point(10, 631);
//            btnPrevious.Size = new Size(80, 25);
//            btnPrevious.TabIndex = 4;
//            btnPrevious.Name = "btnPrevious";
//            btnPrevious.Text = "Previous";
//            btnPrevious.UseVisualStyleBackColor = true;
//            btnPrevious.Click += btnPrevious_Click;

//            // btnNext (New)
//            btnNext.Location = new Point(100, 631);
//            btnNext.Size = new Size(80, 25);
//            btnNext.TabIndex = 5;
//            btnNext.Name = "btnNext";
//            btnNext.Text = "Next";
//            btnNext.UseVisualStyleBackColor = true;
//            btnNext.Click += btnNext_Click;

//            // lblPageInfo (New)
//            lblPageInfo.Location = new Point(190, 631);
//            lblPageInfo.Size = new Size(200, 25);
//            lblPageInfo.TabIndex = 6;
//            lblPageInfo.Name = "lblPageInfo";
//            lblPageInfo.Text = "Page 1 of 1";
//            lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

//            // dataGridViewTextBoxColumn1
//            dataGridViewTextBoxColumn1.HeaderText = "Full Name";
//            dataGridViewTextBoxColumn1.MinimumWidth = 6;
//            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
//            dataGridViewTextBoxColumn1.Width = 150;

//            // dataGridViewTextBoxColumn2
//            dataGridViewTextBoxColumn2.HeaderText = "Email";
//            dataGridViewTextBoxColumn2.MinimumWidth = 6;
//            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
//            dataGridViewTextBoxColumn2.Width = 150;

//            // dataGridViewTextBoxColumn3
//            dataGridViewTextBoxColumn3.HeaderText = "Username";
//            dataGridViewTextBoxColumn3.MinimumWidth = 6;
//            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
//            dataGridViewTextBoxColumn3.Width = 125;

//            // dataGridViewTextBoxColumn4
//            dataGridViewTextBoxColumn4.HeaderText = "Role";
//            dataGridViewTextBoxColumn4.MinimumWidth = 6;
//            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
//            dataGridViewTextBoxColumn4.Width = 125;

//            // UserForm
//            AutoScaleDimensions = new SizeF(8F, 20F);
//            AutoScaleMode = AutoScaleMode.Font;
//            ClientSize = new Size(1067, 692);
//            Controls.Add(lblPageInfo); // New
//            Controls.Add(btnNext); // New
//            Controls.Add(btnPrevious); // New
//            Controls.Add(addUserButton);
//            Controls.Add(btnSearch); // New
//            Controls.Add(txtSearch); // New
//            Controls.Add(usersGridView);
//            Margin = new Padding(4, 5, 4, 5);
//            Name = "UserForm";
//            Text = "Users Management";
//            ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
//            ResumeLayout(false);
//        }
//    }
//}