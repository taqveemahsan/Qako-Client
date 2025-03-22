namespace QACORDMS.Client
{
    partial class Login
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

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblUsername = new Label();
            lblPassword = new Label();
            panelContainer = new Panel();
            pictureBoxLogo = new PictureBox();
            btnTogglePassword = new Button();
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();

            // txtUsername
            txtUsername.BackColor = Color.FromArgb(245, 245, 245);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(50, 180);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Enter Username";
            txtUsername.Size = new Size(450, 40);
            txtUsername.TabIndex = 3;
            txtUsername.Padding = new Padding(8);

            // txtPassword
            txtPassword.BackColor = Color.FromArgb(245, 245, 245);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(50, 270);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.Size = new Size(390, 40);
            txtPassword.TabIndex = 5;
            txtPassword.Padding = new Padding(8);

            // btnLogin
            btnLogin.BackColor = Color.FromArgb(0, 102, 204);
            btnLogin.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 350);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(450, 50);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(0, 102, 204);
            lblUsername.Location = new Point(50, 150);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(103, 28);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username:";

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.ForeColor = Color.FromArgb(0, 102, 204);
            lblPassword.Location = new Point(50, 240);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(97, 28);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password:";

            // pictureBoxLogo
            pictureBoxLogo.ImageLocation = "C:\\Projects\\Qako\\Qako-Client\\QACORDMS.Client\\logo.png";
            pictureBoxLogo.Location = new Point(50, 30);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(450, 100);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 1;
            pictureBoxLogo.TabStop = false;

            // btnTogglePassword
            btnTogglePassword.BackColor = Color.FromArgb(0, 102, 204);
            btnTogglePassword.FlatAppearance.BorderColor = Color.FromArgb(0, 51, 153);
            btnTogglePassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 51, 153);
            btnTogglePassword.FlatStyle = FlatStyle.Flat;
            btnTogglePassword.Font = new Font("Segoe UI", 14F);
            btnTogglePassword.ForeColor = Color.White;
            btnTogglePassword.Location = new Point(445, 270);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(55, 40);
            btnTogglePassword.TabIndex = 6;
            btnTogglePassword.Text = "👁️";
            btnTogglePassword.UseVisualStyleBackColor = false;
            btnTogglePassword.Click += btnTogglePassword_Click;

            // panelContainer
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.FixedSingle;
            panelContainer.Controls.Add(pictureBoxLogo);
            panelContainer.Controls.Add(lblUsername);
            panelContainer.Controls.Add(txtUsername);
            panelContainer.Controls.Add(lblPassword);
            panelContainer.Controls.Add(txtPassword);
            panelContainer.Controls.Add(btnTogglePassword);
            panelContainer.Controls.Add(btnLogin);
            panelContainer.Location = new Point(75, 75);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(550, 450);
            panelContainer.TabIndex = 0;

            // Login
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(173, 216, 230);
            ClientSize = new Size(700, 600);
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - QACO DMS";
            Load += Login_Load;
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button btnTogglePassword;
    }
}




//namespace QACORDMS.Client
//{
//    partial class Login
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

//        private void InitializeComponent()
//        {
//            txtUsername = new TextBox();
//            txtPassword = new TextBox();
//            btnLogin = new Button();
//            lblUsername = new Label();
//            lblPassword = new Label();
//            lblTitle = new Label();
//            panelContainer = new Panel();
//            panelContainer.SuspendLayout();
//            SuspendLayout();
//            // 
//            // txtUsername
//            // 
//            txtUsername.BorderStyle = BorderStyle.FixedSingle;
//            txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
//            txtUsername.Location = new Point(35, 98);
//            txtUsername.Name = "txtUsername";
//            txtUsername.Size = new Size(368, 25);
//            txtUsername.TabIndex = 3;
//            // 
//            // txtPassword
//            // 
//            txtPassword.BorderStyle = BorderStyle.FixedSingle;
//            txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
//            txtPassword.Location = new Point(35, 158);
//            txtPassword.Name = "txtPassword";
//            txtPassword.PasswordChar = '*';
//            txtPassword.Size = new Size(368, 25);
//            txtPassword.TabIndex = 5;
//            // 
//            // btnLogin
//            // 
//            btnLogin.BackColor = Color.FromArgb(0, 64, 128);
//            btnLogin.FlatStyle = FlatStyle.Flat;
//            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
//            btnLogin.ForeColor = Color.White;
//            btnLogin.Location = new Point(175, 210);
//            btnLogin.Name = "btnLogin";
//            btnLogin.Size = new Size(105, 30);
//            btnLogin.TabIndex = 6;
//            btnLogin.Text = "Login";
//            btnLogin.UseVisualStyleBackColor = false;
//            btnLogin.Click += btnLogin_Click;
//            // 
//            // lblUsername
//            // 
//            lblUsername.AutoSize = true;
//            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
//            lblUsername.Location = new Point(35, 75);
//            lblUsername.Name = "lblUsername";
//            lblUsername.Size = new Size(74, 19);
//            lblUsername.TabIndex = 2;
//            lblUsername.Text = "Username:";
//            // 
//            // lblPassword
//            // 
//            lblPassword.AutoSize = true;
//            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
//            lblPassword.Location = new Point(35, 135);
//            lblPassword.Name = "lblPassword";
//            lblPassword.Size = new Size(70, 19);
//            lblPassword.TabIndex = 4;
//            lblPassword.Text = "Password:";
//            // 
//            // lblTitle
//            // 
//            lblTitle.AutoSize = true;
//            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
//            lblTitle.ForeColor = Color.FromArgb(0, 64, 128);
//            lblTitle.Location = new Point(158, 30);
//            lblTitle.Name = "lblTitle";
//            lblTitle.Size = new Size(222, 30);
//            lblTitle.TabIndex = 1;
//            lblTitle.Text = "Login to QACO DMS";
//            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
//            // 
//            // panelContainer
//            // 
//            panelContainer.BackColor = Color.White;
//            panelContainer.BorderStyle = BorderStyle.FixedSingle;
//            panelContainer.Controls.Add(lblTitle);
//            panelContainer.Controls.Add(lblUsername);
//            panelContainer.Controls.Add(txtUsername);
//            panelContainer.Controls.Add(lblPassword);
//            panelContainer.Controls.Add(txtPassword);
//            panelContainer.Controls.Add(btnLogin);
//            panelContainer.Location = new Point(44, 38);
//            panelContainer.Margin = new Padding(3, 2, 3, 2);
//            panelContainer.Name = "panelContainer";
//            panelContainer.Size = new Size(438, 300);
//            panelContainer.TabIndex = 0;
//            // 
//            // Login
//            // 
//            AcceptButton = btnLogin;
//            AutoScaleDimensions = new SizeF(7F, 15F);
//            AutoScaleMode = AutoScaleMode.Font;
//            BackColor = Color.FromArgb(240, 240, 240);
//            ClientSize = new Size(525, 375);
//            Controls.Add(panelContainer);
//            FormBorderStyle = FormBorderStyle.FixedDialog;
//            MaximizeBox = false;
//            MinimizeBox = false;
//            Name = "Login";
//            StartPosition = FormStartPosition.CenterScreen;
//            Text = "Login - QACO DMS";
//            Load += Login_Load;
//            panelContainer.ResumeLayout(false);
//            panelContainer.PerformLayout();
//            ResumeLayout(false);
//        }

//        #endregion

//        private System.Windows.Forms.TextBox txtUsername;
//        private System.Windows.Forms.TextBox txtPassword;
//        private System.Windows.Forms.Button btnLogin;
//        private System.Windows.Forms.Label lblUsername;
//        private System.Windows.Forms.Label lblPassword;
//        private System.Windows.Forms.Label lblTitle;
//        private System.Windows.Forms.Panel panelContainer;
//    }
//}