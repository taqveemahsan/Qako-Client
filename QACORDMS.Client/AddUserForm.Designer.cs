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

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelContainer;

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRoles = new Label();
            clbRoles = new CheckedListBox();
            btnSave = new Button();
            panelContainer = new Panel();
            panelContainer.SuspendLayout();
            SuspendLayout();

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblUsername.ForeColor = Color.FromArgb(0, 102, 204);
            lblUsername.Location = new Point(30, 30);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(103, 28);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";

            // txtUsername
            txtUsername.BackColor = Color.FromArgb(245, 245, 245);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(30, 60);
            txtUsername.Margin = new Padding(4, 5, 4, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(450, 34);
            txtUsername.TabIndex = 1;
            txtUsername.PlaceholderText = "Enter username";

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblEmail.ForeColor = Color.FromArgb(0, 102, 204);
            lblEmail.Location = new Point(30, 110);
            lblEmail.Margin = new Padding(4, 0, 4, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(63, 28);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.BackColor = Color.FromArgb(245, 245, 245);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 12F);
            txtEmail.Location = new Point(30, 140);
            txtEmail.Margin = new Padding(4, 5, 4, 5);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(450, 34);
            txtEmail.TabIndex = 3;
            txtEmail.PlaceholderText = "Enter email";

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblPassword.ForeColor = Color.FromArgb(0, 102, 204);
            lblPassword.Location = new Point(30, 190);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(97, 28);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password:";

            // txtPassword
            txtPassword.BackColor = Color.FromArgb(245, 245, 245);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(30, 220);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(450, 34);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.PlaceholderText = "Enter password";

            // lblRoles
            lblRoles.AutoSize = true;
            lblRoles.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblRoles.ForeColor = Color.FromArgb(0, 102, 204);
            lblRoles.Location = new Point(30, 270);
            lblRoles.Margin = new Padding(4, 0, 4, 0);
            lblRoles.Name = "lblRoles";
            lblRoles.Size = new Size(62, 28);
            lblRoles.TabIndex = 6;
            lblRoles.Text = "Roles:";

            // clbRoles
            clbRoles.BackColor = Color.FromArgb(245, 245, 245);
            clbRoles.BorderStyle = BorderStyle.FixedSingle;
            clbRoles.Font = new Font("Segoe UI", 12F);
            clbRoles.Location = new Point(30, 300);
            clbRoles.Margin = new Padding(4, 5, 4, 5);
            clbRoles.Name = "clbRoles";
            clbRoles.Size = new Size(450, 100);
            clbRoles.TabIndex = 7;

            // btnSave
            btnSave.BackColor = Color.FromArgb(0, 102, 204);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(150, 420);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 40);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 200, 40, 10, 10));
            btnSave.Click += btnSave_Click;

            // panelContainer
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.None;
            panelContainer.Controls.Add(btnSave);
            panelContainer.Controls.Add(clbRoles);
            panelContainer.Controls.Add(lblRoles);
            panelContainer.Controls.Add(txtPassword);
            panelContainer.Controls.Add(lblPassword);
            panelContainer.Controls.Add(txtEmail);
            panelContainer.Controls.Add(lblEmail);
            panelContainer.Controls.Add(txtUsername);
            panelContainer.Controls.Add(lblUsername);
            panelContainer.Location = new Point(50, 30);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(510, 480);
            panelContainer.TabIndex = 0;
            panelContainer.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 510, 480, 15, 15));

            // AddUserForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(173, 216, 230);
            ClientSize = new Size(610, 540);
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddUserForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New User";
            Load += new EventHandler(AddUserForm_Load); // Added Load event to attach Paint handler
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ResumeLayout(false);
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        #endregion
    }
}