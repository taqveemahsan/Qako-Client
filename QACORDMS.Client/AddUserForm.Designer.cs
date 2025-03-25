namespace QACORDMS.Client
{
    partial class AddUserForm
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

        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblRoles; // Updated label name
        private System.Windows.Forms.CheckedListBox clbRoles; // Changed from ComboBox to CheckedListBox
        private System.Windows.Forms.Button btnSave;

        private void InitializeComponent()
        {
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRoles = new System.Windows.Forms.Label();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblFirstName
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(40, 40);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(55, 13);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "First Name:";
            this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

            // txtFirstName
            this.txtFirstName.Location = new System.Drawing.Point(40, 70);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(400, 35);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFirstName.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.Padding = new System.Windows.Forms.Padding(5);

            // lblLastName
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(40, 120);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(55, 13);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name:";
            this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLastName.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

            // txtLastName
            this.txtLastName.Location = new System.Drawing.Point(40, 150);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(400, 35);
            this.txtLastName.TabIndex = 3;
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLastName.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.Padding = new System.Windows.Forms.Padding(5);

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(40, 200);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username:";
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(40, 230);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(400, 35);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Padding = new System.Windows.Forms.Padding(5);

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(40, 280);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(40, 310);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 35);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Padding = new System.Windows.Forms.Padding(5);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(40, 360);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(50, 13);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(40, 390);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(400, 35);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Padding = new System.Windows.Forms.Padding(5);

            // lblRoles (Updated label name)
            this.lblRoles.AutoSize = true;
            this.lblRoles.Location = new System.Drawing.Point(40, 440);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(35, 13);
            this.lblRoles.TabIndex = 10;
            this.lblRoles.Text = "Roles:"; // Changed "Role" to "Roles"
            this.lblRoles.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblRoles.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

            // clbRoles (Changed from ComboBox to CheckedListBox)
            this.clbRoles.Location = new System.Drawing.Point(40, 470);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(400, 100); // Increased height to show multiple roles
            this.clbRoles.TabIndex = 11;
            this.clbRoles.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.clbRoles.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.clbRoles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(40, 590); // Adjusted position due to increased height of clbRoles
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(400, 40);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // AddUserForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 650); // Increased form height to accommodate CheckedListBox
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.clbRoles);
            this.Controls.Add(this.lblRoles);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New User";
            this.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}














//namespace QACORDMS.Client
//{
//    partial class AddUserForm
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

//        private System.Windows.Forms.Label lblFirstName;
//        private System.Windows.Forms.TextBox txtFirstName;
//        private System.Windows.Forms.Label lblLastName;
//        private System.Windows.Forms.TextBox txtLastName;
//        private System.Windows.Forms.Label lblUsername;
//        private System.Windows.Forms.TextBox txtUsername;
//        private System.Windows.Forms.Label lblEmail;
//        private System.Windows.Forms.TextBox txtEmail;
//        private System.Windows.Forms.Label lblPassword;
//        private System.Windows.Forms.TextBox txtPassword;
//        private System.Windows.Forms.Label lblRole;
//        private System.Windows.Forms.ComboBox cmbRole;
//        private System.Windows.Forms.Button btnSave;

//        private void InitializeComponent()
//        {
//            this.lblFirstName = new System.Windows.Forms.Label();
//            this.txtFirstName = new System.Windows.Forms.TextBox();
//            this.lblLastName = new System.Windows.Forms.Label();
//            this.txtLastName = new System.Windows.Forms.TextBox();
//            this.lblUsername = new System.Windows.Forms.Label();
//            this.txtUsername = new System.Windows.Forms.TextBox();
//            this.lblEmail = new System.Windows.Forms.Label();
//            this.txtEmail = new System.Windows.Forms.TextBox();
//            this.lblPassword = new System.Windows.Forms.Label();
//            this.txtPassword = new System.Windows.Forms.TextBox();
//            this.lblRole = new System.Windows.Forms.Label();
//            this.cmbRole = new System.Windows.Forms.ComboBox();
//            this.btnSave = new System.Windows.Forms.Button();
//            this.SuspendLayout();

//            // lblFirstName
//            this.lblFirstName.AutoSize = true;
//            this.lblFirstName.Location = new System.Drawing.Point(40, 40);
//            this.lblFirstName.Name = "lblFirstName";
//            this.lblFirstName.Size = new System.Drawing.Size(55, 13);
//            this.lblFirstName.TabIndex = 0;
//            this.lblFirstName.Text = "First Name:";
//            this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // txtFirstName
//            this.txtFirstName.Location = new System.Drawing.Point(40, 70);
//            this.txtFirstName.Name = "txtFirstName";
//            this.txtFirstName.Size = new System.Drawing.Size(400, 35);
//            this.txtFirstName.TabIndex = 1;
//            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.txtFirstName.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.txtFirstName.Padding = new System.Windows.Forms.Padding(5);

//            // lblLastName
//            this.lblLastName.AutoSize = true;
//            this.lblLastName.Location = new System.Drawing.Point(40, 120);
//            this.lblLastName.Name = "lblLastName";
//            this.lblLastName.Size = new System.Drawing.Size(55, 13);
//            this.lblLastName.TabIndex = 2;
//            this.lblLastName.Text = "Last Name:";
//            this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblLastName.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // txtLastName
//            this.txtLastName.Location = new System.Drawing.Point(40, 150);
//            this.txtLastName.Name = "txtLastName";
//            this.txtLastName.Size = new System.Drawing.Size(400, 35);
//            this.txtLastName.TabIndex = 3;
//            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.txtLastName.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.txtLastName.Padding = new System.Windows.Forms.Padding(5);

//            // lblUsername
//            this.lblUsername.AutoSize = true;
//            this.lblUsername.Location = new System.Drawing.Point(40, 200);
//            this.lblUsername.Name = "lblUsername";
//            this.lblUsername.Size = new System.Drawing.Size(55, 13);
//            this.lblUsername.TabIndex = 4;
//            this.lblUsername.Text = "Username:";
//            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // txtUsername
//            this.txtUsername.Location = new System.Drawing.Point(40, 230);
//            this.txtUsername.Name = "txtUsername";
//            this.txtUsername.Size = new System.Drawing.Size(400, 35);
//            this.txtUsername.TabIndex = 5;
//            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.txtUsername.Padding = new System.Windows.Forms.Padding(5);

//            // lblEmail
//            this.lblEmail.AutoSize = true;
//            this.lblEmail.Location = new System.Drawing.Point(40, 280);
//            this.lblEmail.Name = "lblEmail";
//            this.lblEmail.Size = new System.Drawing.Size(35, 13);
//            this.lblEmail.TabIndex = 6;
//            this.lblEmail.Text = "Email:";
//            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // txtEmail
//            this.txtEmail.Location = new System.Drawing.Point(40, 310);
//            this.txtEmail.Name = "txtEmail";
//            this.txtEmail.Size = new System.Drawing.Size(400, 35);
//            this.txtEmail.TabIndex = 7;
//            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.txtEmail.Padding = new System.Windows.Forms.Padding(5);

//            // lblPassword
//            this.lblPassword.AutoSize = true;
//            this.lblPassword.Location = new System.Drawing.Point(40, 360);
//            this.lblPassword.Name = "lblPassword";
//            this.lblPassword.Size = new System.Drawing.Size(50, 13);
//            this.lblPassword.TabIndex = 8;
//            this.lblPassword.Text = "Password:";
//            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // txtPassword
//            this.txtPassword.Location = new System.Drawing.Point(40, 390);
//            this.txtPassword.Name = "txtPassword";
//            this.txtPassword.Size = new System.Drawing.Size(400, 35);
//            this.txtPassword.TabIndex = 9;
//            this.txtPassword.UseSystemPasswordChar = true;
//            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.txtPassword.Padding = new System.Windows.Forms.Padding(5);

//            // lblRole
//            this.lblRole.AutoSize = true;
//            this.lblRole.Location = new System.Drawing.Point(40, 440);
//            this.lblRole.Name = "lblRole";
//            this.lblRole.Size = new System.Drawing.Size(35, 13);
//            this.lblRole.TabIndex = 10;
//            this.lblRole.Text = "Role:";
//            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // cmbRole
//            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbRole.Location = new System.Drawing.Point(40, 470);
//            this.cmbRole.Name = "cmbRole";
//            this.cmbRole.Size = new System.Drawing.Size(400, 35);
//            this.cmbRole.TabIndex = 11;
//            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.cmbRole.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.cmbRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

//            // btnSave
//            this.btnSave.Location = new System.Drawing.Point(40, 520);
//            this.btnSave.Name = "btnSave";
//            this.btnSave.Size = new System.Drawing.Size(400, 40);
//            this.btnSave.TabIndex = 12;
//            this.btnSave.Text = "Save";
//            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
//            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
//            this.btnSave.ForeColor = System.Drawing.Color.White;
//            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 51, 153);
//            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 51, 153);
//            this.btnSave.UseVisualStyleBackColor = false;
//            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

//            // AddUserForm
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(480, 600);
//            this.Controls.Add(this.btnSave);
//            this.Controls.Add(this.cmbRole);
//            this.Controls.Add(this.lblRole);
//            this.Controls.Add(this.txtPassword);
//            this.Controls.Add(this.lblPassword);
//            this.Controls.Add(this.txtEmail);
//            this.Controls.Add(this.lblEmail);
//            this.Controls.Add(this.txtUsername);
//            this.Controls.Add(this.lblUsername);
//            this.Controls.Add(this.txtLastName);
//            this.Controls.Add(this.lblLastName);
//            this.Controls.Add(this.txtFirstName);
//            this.Controls.Add(this.lblFirstName);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "AddUserForm";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "Add New User";
//            this.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }

//        #endregion
//    }
//}