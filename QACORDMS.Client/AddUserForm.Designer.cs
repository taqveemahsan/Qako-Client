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

//        private System.Windows.Forms.Label lblUsername;
//        private System.Windows.Forms.TextBox txtUsername;
//        private System.Windows.Forms.Label lblEmail;
//        private System.Windows.Forms.TextBox txtEmail;
//        private System.Windows.Forms.Label lblPassword;
//        private System.Windows.Forms.TextBox txtPassword;
//        private System.Windows.Forms.Label lblRoles;
//        private System.Windows.Forms.CheckedListBox clbRoles;
//        private System.Windows.Forms.Button btnSave;

//        private void InitializeComponent()
//        {
//            lblUsername = new Label();
//            txtUsername = new TextBox();
//            lblEmail = new Label();
//            txtEmail = new TextBox();
//            lblPassword = new Label();
//            txtPassword = new TextBox();
//            lblRoles = new Label();
//            clbRoles = new CheckedListBox();
//            btnSave = new Button();
//            SuspendLayout();
//            // 
//            // lblUsername
//            // 
//            lblUsername.AutoSize = true;
//            lblUsername.Font = new Font("Segoe UI", 12F);
//            lblUsername.ForeColor = Color.FromArgb(0, 102, 204);
//            lblUsername.Location = new Point(56, 54);
//            lblUsername.Margin = new Padding(4, 0, 4, 0);
//            lblUsername.Name = "lblUsername";
//            lblUsername.Size = new Size(103, 28);
//            lblUsername.TabIndex = 0;
//            lblUsername.Text = "Username:";
//            // 
//            // txtUsername
//            // 
//            txtUsername.BackColor = Color.FromArgb(245, 245, 245);
//            txtUsername.BorderStyle = BorderStyle.FixedSingle;
//            txtUsername.Font = new Font("Segoe UI", 12F);
//            txtUsername.Location = new Point(56, 89);
//            txtUsername.Margin = new Padding(4, 5, 4, 5);
//            txtUsername.Name = "txtUsername";
//            txtUsername.Size = new Size(533, 34);
//            txtUsername.TabIndex = 1;
//            // 
//            // lblEmail
//            // 
//            lblEmail.AutoSize = true;
//            lblEmail.Font = new Font("Segoe UI", 12F);
//            lblEmail.ForeColor = Color.FromArgb(0, 102, 204);
//            lblEmail.Location = new Point(56, 132);
//            lblEmail.Margin = new Padding(4, 0, 4, 0);
//            lblEmail.Name = "lblEmail";
//            lblEmail.Size = new Size(63, 28);
//            lblEmail.TabIndex = 2;
//            lblEmail.Text = "Email:";
//            // 
//            // txtEmail
//            // 
//            txtEmail.BackColor = Color.FromArgb(245, 245, 245);
//            txtEmail.BorderStyle = BorderStyle.FixedSingle;
//            txtEmail.Font = new Font("Segoe UI", 12F);
//            txtEmail.Location = new Point(56, 165);
//            txtEmail.Margin = new Padding(4, 5, 4, 5);
//            txtEmail.Name = "txtEmail";
//            txtEmail.Size = new Size(533, 34);
//            txtEmail.TabIndex = 3;
//            // 
//            // lblPassword
//            // 
//            lblPassword.AutoSize = true;
//            lblPassword.Font = new Font("Segoe UI", 12F);
//            lblPassword.ForeColor = Color.FromArgb(0, 102, 204);
//            lblPassword.Location = new Point(56, 209);
//            lblPassword.Margin = new Padding(4, 0, 4, 0);
//            lblPassword.Name = "lblPassword";
//            lblPassword.Size = new Size(97, 28);
//            lblPassword.TabIndex = 4;
//            lblPassword.Text = "Password:";
//            // 
//            // txtPassword
//            // 
//            txtPassword.BackColor = Color.FromArgb(245, 245, 245);
//            txtPassword.BorderStyle = BorderStyle.FixedSingle;
//            txtPassword.Font = new Font("Segoe UI", 12F);
//            txtPassword.Location = new Point(56, 242);
//            txtPassword.Margin = new Padding(4, 5, 4, 5);
//            txtPassword.Name = "txtPassword";
//            txtPassword.Size = new Size(533, 34);
//            txtPassword.TabIndex = 5;
//            txtPassword.UseSystemPasswordChar = true;
//            // 
//            // lblRoles
//            // 
//            lblRoles.AutoSize = true;
//            lblRoles.Font = new Font("Segoe UI", 12F);
//            lblRoles.ForeColor = Color.FromArgb(0, 102, 204);
//            lblRoles.Location = new Point(56, 289);
//            lblRoles.Margin = new Padding(4, 0, 4, 0);
//            lblRoles.Name = "lblRoles";
//            lblRoles.Size = new Size(62, 28);
//            lblRoles.TabIndex = 6;
//            lblRoles.Text = "Roles:";
//            // 
//            // clbRoles
//            // 
//            clbRoles.BackColor = Color.FromArgb(245, 245, 245);
//            clbRoles.BorderStyle = BorderStyle.FixedSingle;
//            clbRoles.Font = new Font("Segoe UI", 12F);
//            clbRoles.Location = new Point(56, 322);
//            clbRoles.Margin = new Padding(4, 5, 4, 5);
//            clbRoles.Name = "clbRoles";
//            clbRoles.Size = new Size(533, 147);
//            clbRoles.TabIndex = 7;
//            // 
//            // btnSave
//            // 
//            btnSave.BackColor = Color.FromArgb(0, 102, 204);
//            btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
//            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
//            btnSave.FlatStyle = FlatStyle.Flat;
//            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            btnSave.ForeColor = Color.White;
//            btnSave.Location = new Point(218, 482);
//            btnSave.Margin = new Padding(4, 5, 4, 5);
//            btnSave.Name = "btnSave";
//            btnSave.Size = new Size(198, 41);
//            btnSave.TabIndex = 8;
//            btnSave.Text = "Save";
//            btnSave.UseVisualStyleBackColor = false;
//            btnSave.Click += btnSave_Click;
//            // 
//            // AddUserForm
//            // 
//            AutoScaleDimensions = new SizeF(8F, 20F);
//            AutoScaleMode = AutoScaleMode.Font;
//            BackColor = Color.FromArgb(173, 216, 230);
//            ClientSize = new Size(640, 571);
//            Controls.Add(btnSave);
//            Controls.Add(clbRoles);
//            Controls.Add(lblRoles);
//            Controls.Add(txtPassword);
//            Controls.Add(lblPassword);
//            Controls.Add(txtEmail);
//            Controls.Add(lblEmail);
//            Controls.Add(txtUsername);
//            Controls.Add(lblUsername);
//            FormBorderStyle = FormBorderStyle.FixedDialog;
//            Margin = new Padding(4, 5, 4, 5);
//            MaximizeBox = false;
//            MinimizeBox = false;
//            Name = "AddUserForm";
//            StartPosition = FormStartPosition.CenterScreen;
//            Text = "Add New User";
//            ResumeLayout(false);
//            PerformLayout();
//        }

//        #endregion
//    }
//}






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
//        private System.Windows.Forms.Label lblRoles; // Updated label name
//        private System.Windows.Forms.CheckedListBox clbRoles; // Changed from ComboBox to CheckedListBox
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
//            this.lblRoles = new System.Windows.Forms.Label();
//            this.clbRoles = new System.Windows.Forms.CheckedListBox();
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

//            // lblRoles (Updated label name)
//            this.lblRoles.AutoSize = true;
//            this.lblRoles.Location = new System.Drawing.Point(40, 440);
//            this.lblRoles.Name = "lblRoles";
//            this.lblRoles.Size = new System.Drawing.Size(35, 13);
//            this.lblRoles.TabIndex = 10;
//            this.lblRoles.Text = "Roles:"; // Changed "Role" to "Roles"
//            this.lblRoles.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.lblRoles.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);

//            // clbRoles (Changed from ComboBox to CheckedListBox)
//            this.clbRoles.Location = new System.Drawing.Point(40, 470);
//            this.clbRoles.Name = "clbRoles";
//            this.clbRoles.Size = new System.Drawing.Size(400, 100); // Increased height to show multiple roles
//            this.clbRoles.TabIndex = 11;
//            this.clbRoles.Font = new System.Drawing.Font("Segoe UI", 12F);
//            this.clbRoles.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
//            this.clbRoles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

//            // btnSave
//            this.btnSave.Location = new System.Drawing.Point(40, 590); // Adjusted position due to increased height of clbRoles
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
//            this.ClientSize = new System.Drawing.Size(480, 650); // Increased form height to accommodate CheckedListBox
//            this.Controls.Add(this.btnSave);
//            this.Controls.Add(this.clbRoles);
//            this.Controls.Add(this.lblRoles);
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