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
        private System.Windows.Forms.DataGridViewButtonColumn colExpiry;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageInfo;

        private void InitializeComponent()
        {
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.colCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiry = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTitle.Location = new System.Drawing.Point(40, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 30);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Manage Project Permissions";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(40, 80);
            this.txtSearch.Size = new System.Drawing.Size(300, 35);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search by username or email";
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Padding = new System.Windows.Forms.Padding(5);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(350, 80);
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Text = "Search";
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += btnSearch_Click;

            // dgvUsers
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colCheckbox,
                this.colUsername,
                this.colEmail,
                this.colExpiry});
            this.dgvUsers.Location = new System.Drawing.Point(40, 130);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.Size = new System.Drawing.Size(720, 350);
            this.dgvUsers.TabIndex = 2;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.GridColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.dgvUsers.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);

            // colCheckbox
            this.colCheckbox.HeaderText = "Access";
            this.colCheckbox.Name = "colCheckbox";
            this.colCheckbox.Width = 80;

            // colUsername
            this.colUsername.HeaderText = "Username";
            this.colUsername.Name = "colUsername";
            this.colUsername.Width = 200;

            // colEmail
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Width = 200;

            // colExpiry
            this.colExpiry.HeaderText = "Expiry";
            this.colExpiry.Name = "colExpiry";
            this.colExpiry.Text = "Set Expiry";
            this.colExpiry.UseColumnTextForButtonValue = true;
            this.colExpiry.Width = 120;
            this.colExpiry.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // btnPrevious
            this.btnPrevious.Location = new System.Drawing.Point(40, 500);
            this.btnPrevious.Size = new System.Drawing.Size(100, 40);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += btnPrevious_Click;

            // btnNext
            this.btnNext.Location = new System.Drawing.Point(150, 500);
            this.btnNext.Size = new System.Drawing.Size(100, 40);
            this.btnNext.TabIndex = 4;
            this.btnNext.Name = "btnNext";
            this.btnNext.Text = "Next";
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += btnNext_Click;

            // lblPageInfo
            this.lblPageInfo.Location = new System.Drawing.Point(260, 500);
            this.lblPageInfo.Size = new System.Drawing.Size(200, 40);
            this.lblPageInfo.TabIndex = 5;
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Text = "Page 1 of 1";
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(660, 500);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 40);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // AddPermissionForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
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
            this.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}