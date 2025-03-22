namespace QACORDMS.Client
{
    partial class AddPermissionForm
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

        #region Windows Form Designer generated code

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.DataGridViewButtonColumn colExpiry; // New: Expiry Column
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch; // New: Search TextBox
        private System.Windows.Forms.Button btnSearch; // New: Search Button
        private System.Windows.Forms.Button btnPrevious; // New: Previous Button
        private System.Windows.Forms.Button btnNext; // New: Next Button
        private System.Windows.Forms.Label lblPageInfo; // New: Page Info Label

        private void InitializeComponent()
        {
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.colCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colExpiry = new System.Windows.Forms.DataGridViewButtonColumn(); // New
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox(); // New
            this.btnSearch = new System.Windows.Forms.Button(); // New
            this.btnPrevious = new System.Windows.Forms.Button(); // New
            this.btnNext = new System.Windows.Forms.Button(); // New
            this.lblPageInfo = new System.Windows.Forms.Label(); // New
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.lblTitle.Location = new System.Drawing.Point(200, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 25);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Manage Project Permissions";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtSearch (New)
            this.txtSearch.Location = new System.Drawing.Point(20, 40);
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search by username or email";

            // btnSearch (New)
            this.btnSearch.Location = new System.Drawing.Point(230, 40);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += btnSearch_Click;

            // dgvUsers
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colCheckbox,
                this.colUsername,
                this.colEmail,
                this.colExpiry, // New
                this.colDelete});
            this.dgvUsers.Location = new System.Drawing.Point(20, 80);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.Size = new System.Drawing.Size(560, 260); // Adjusted height
            this.dgvUsers.TabIndex = 2;
            this.dgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);

            // colCheckbox
            this.colCheckbox.HeaderText = "Access";
            this.colCheckbox.Name = "colCheckbox";
            this.colCheckbox.Width = 60;

            // colUsername
            this.colUsername.HeaderText = "Username";
            this.colUsername.Name = "colUsername";
            this.colUsername.Width = 150;

            // colEmail
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Width = 150;

            // colExpiry (New)
            this.colExpiry.HeaderText = "Expiry";
            this.colExpiry.Name = "colExpiry";
            this.colExpiry.Text = "Set Expiry";
            this.colExpiry.UseColumnTextForButtonValue = true;
            this.colExpiry.Width = 100;

            // colDelete
            this.colDelete.HeaderText = "Action";
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "Delete";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 100;

            // btnPrevious (New)
            this.btnPrevious.Location = new System.Drawing.Point(20, 350);
            this.btnPrevious.Size = new System.Drawing.Size(80, 25);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += btnPrevious_Click;

            // btnNext (New)
            this.btnNext.Location = new System.Drawing.Point(110, 350);
            this.btnNext.Size = new System.Drawing.Size(80, 25);
            this.btnNext.TabIndex = 4;
            this.btnNext.Name = "btnNext";
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += btnNext_Click;

            // lblPageInfo (New)
            this.lblPageInfo.Location = new System.Drawing.Point(200, 350);
            this.lblPageInfo.Size = new System.Drawing.Size(200, 25);
            this.lblPageInfo.TabIndex = 5;
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Text = "Page 1 of 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(480, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // AddPermissionForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400); // Adjusted height
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPermissionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Permissions";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}












//namespace QACORDMS.Client
//{
//    partial class AddPermissionForm
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

//        #region Windows Form Designer generated code

//        private System.Windows.Forms.DataGridView dgvUsers;
//        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheckbox;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
//        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
//        private System.Windows.Forms.DataGridViewButtonColumn colExpiry; // New: Expiry Column
//        private System.Windows.Forms.Button btnSave;
//        private System.Windows.Forms.Label lblTitle;
//        private System.Windows.Forms.TextBox txtSearch; // New: Search TextBox
//        private System.Windows.Forms.Button btnSearch; // New: Search Button
//        private System.Windows.Forms.Button btnPrevious; // New: Previous Button
//        private System.Windows.Forms.Button btnNext; // New: Next Button
//        private System.Windows.Forms.Label lblPageInfo; // New: Page Info Label

//        private void InitializeComponent()
//        {
//            this.dgvUsers = new System.Windows.Forms.DataGridView();
//            this.colCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
//            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
//            this.colExpiry = new System.Windows.Forms.DataGridViewButtonColumn(); // New
//            this.btnSave = new System.Windows.Forms.Button();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.txtSearch = new System.Windows.Forms.TextBox(); // New
//            this.btnSearch = new System.Windows.Forms.Button(); // New
//            this.btnPrevious = new System.Windows.Forms.Button(); // New
//            this.btnNext = new System.Windows.Forms.Button(); // New
//            this.lblPageInfo = new System.Windows.Forms.Label(); // New
//            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
//            this.SuspendLayout();

//            // lblTitle
//            this.lblTitle.AutoSize = true;
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
//            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
//            this.lblTitle.Location = new System.Drawing.Point(200, 10);
//            this.lblTitle.Name = "lblTitle";
//            this.lblTitle.Size = new System.Drawing.Size(0, 25);
//            this.lblTitle.TabIndex = 2;
//            this.lblTitle.Text = "Manage Project Permissions";
//            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

//            // txtSearch (New)
//            this.txtSearch.Location = new System.Drawing.Point(20, 40);
//            this.txtSearch.Size = new System.Drawing.Size(200, 20);
//            this.txtSearch.TabIndex = 0;
//            this.txtSearch.Name = "txtSearch";
//            this.txtSearch.PlaceholderText = "Search by username or email";

//            // btnSearch (New)
//            this.btnSearch.Location = new System.Drawing.Point(230, 40);
//            this.btnSearch.Size = new System.Drawing.Size(80, 25);
//            this.btnSearch.TabIndex = 1;
//            this.btnSearch.Name = "btnSearch";
//            this.btnSearch.Text = "Search";
//            this.btnSearch.UseVisualStyleBackColor = true;
//            this.btnSearch.Click += btnSearch_Click;

//            // dgvUsers
//            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
//                this.colCheckbox,
//                this.colUsername,
//                this.colEmail,
//                this.colExpiry, // New
//                this.colDelete});
//            this.dgvUsers.Location = new System.Drawing.Point(20, 80);
//            this.dgvUsers.Name = "dgvUsers";
//            this.dgvUsers.RowHeadersVisible = false;
//            this.dgvUsers.Size = new System.Drawing.Size(560, 260); // Adjusted height
//            this.dgvUsers.TabIndex = 2;
//            this.dgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);

//            // colCheckbox
//            this.colCheckbox.HeaderText = "Access";
//            this.colCheckbox.Name = "colCheckbox";
//            this.colCheckbox.Width = 60;

//            // colUsername
//            this.colUsername.HeaderText = "Username";
//            this.colUsername.Name = "colUsername";
//            this.colUsername.Width = 150;

//            // colEmail
//            this.colEmail.HeaderText = "Email";
//            this.colEmail.Name = "colEmail";
//            this.colEmail.Width = 150;

//            // colExpiry (New)
//            this.colExpiry.HeaderText = "Expiry";
//            this.colExpiry.Name = "colExpiry";
//            this.colExpiry.Text = "Set Expiry";
//            this.colExpiry.UseColumnTextForButtonValue = true;
//            this.colExpiry.Width = 100;

//            // colDelete
//            this.colDelete.HeaderText = "Action";
//            this.colDelete.Name = "colDelete";
//            this.colDelete.Text = "Delete";
//            this.colDelete.UseColumnTextForButtonValue = true;
//            this.colDelete.Width = 100;

//            // btnPrevious (New)
//            this.btnPrevious.Location = new System.Drawing.Point(20, 350);
//            this.btnPrevious.Size = new System.Drawing.Size(80, 25);
//            this.btnPrevious.TabIndex = 3;
//            this.btnPrevious.Name = "btnPrevious";
//            this.btnPrevious.Text = "Previous";
//            this.btnPrevious.UseVisualStyleBackColor = true;
//            this.btnPrevious.Click += btnPrevious_Click;

//            // btnNext (New)
//            this.btnNext.Location = new System.Drawing.Point(110, 350);
//            this.btnNext.Size = new System.Drawing.Size(80, 25);
//            this.btnNext.TabIndex = 4;
//            this.btnNext.Name = "btnNext";
//            this.btnNext.Text = "Next";
//            this.btnNext.UseVisualStyleBackColor = true;
//            this.btnNext.Click += btnNext_Click;

//            // lblPageInfo (New)
//            this.lblPageInfo.Location = new System.Drawing.Point(200, 350);
//            this.lblPageInfo.Size = new System.Drawing.Size(200, 25);
//            this.lblPageInfo.TabIndex = 5;
//            this.lblPageInfo.Name = "lblPageInfo";
//            this.lblPageInfo.Text = "Page 1 of 1";
//            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

//            // btnSave
//            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
//            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.btnSave.ForeColor = System.Drawing.Color.White;
//            this.btnSave.Location = new System.Drawing.Point(480, 350);
//            this.btnSave.Name = "btnSave";
//            this.btnSave.Size = new System.Drawing.Size(100, 35);
//            this.btnSave.TabIndex = 6;
//            this.btnSave.Text = "Save";
//            this.btnSave.UseVisualStyleBackColor = false;
//            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

//            // AddPermissionForm
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(600, 400); // Adjusted height
//            this.Controls.Add(this.lblTitle);
//            this.Controls.Add(this.btnSave);
//            this.Controls.Add(this.lblPageInfo);
//            this.Controls.Add(this.btnNext);
//            this.Controls.Add(this.btnPrevious);
//            this.Controls.Add(this.btnSearch);
//            this.Controls.Add(this.txtSearch);
//            this.Controls.Add(this.dgvUsers);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "AddPermissionForm";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "Manage Permissions";
//            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }

//        #endregion
//    }
//}
