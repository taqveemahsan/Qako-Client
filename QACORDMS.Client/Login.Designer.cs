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
            chkRememberMe = new CheckBox();
            lnkForgotPassword = new LinkLabel(); // New Forgot Password link
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(240, 240, 240);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(60, 200);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Enter Username";
            txtUsername.Size = new Size(480, 40);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(240, 240, 240);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(60, 290);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.Size = new Size(420, 40);
            txtPassword.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 120, 215); // Slightly lighter blue for modern look
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(60, 400);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(480, 50);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(0, 120, 215);
            lblUsername.Location = new Point(56, 165);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(126, 32);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.ForeColor = Color.FromArgb(0, 120, 215);
            lblPassword.Location = new Point(56, 255);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(116, 32);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "Password";
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkRememberMe.Location = new Point(60, 355);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(120, 32);
            chkRememberMe.TabIndex = 3;
            chkRememberMe.Text = "Remember Me";
            chkRememberMe.ForeColor = Color.FromArgb(0, 120, 215);
            // 
            // lnkForgotPassword
            // 
            lnkForgotPassword.AutoSize = true;
            lnkForgotPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkForgotPassword.LinkColor = Color.FromArgb(0, 120, 215);
            lnkForgotPassword.ActiveLinkColor = Color.FromArgb(0, 100, 180);
            lnkForgotPassword.Location = new Point(380, 335);
            lnkForgotPassword.Name = "lnkForgotPassword";
            lnkForgotPassword.Size = new Size(120, 28);
            lnkForgotPassword.TabIndex = 5;
            lnkForgotPassword.TabStop = true;
            lnkForgotPassword.Text = "Forgot Password?";
            lnkForgotPassword.LinkClicked += lnkForgotPassword_LinkClicked;
            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.FixedSingle;
            panelContainer.Controls.Add(pictureBoxLogo);
            panelContainer.Controls.Add(lblUsername);
            panelContainer.Controls.Add(txtUsername);
            panelContainer.Controls.Add(lblPassword);
            panelContainer.Controls.Add(txtPassword);
            panelContainer.Controls.Add(btnTogglePassword);
            panelContainer.Controls.Add(btnLogin);
            panelContainer.Controls.Add(chkRememberMe);
            panelContainer.Controls.Add(lnkForgotPassword);
            panelContainer.Location = new Point(100, 80);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(600, 500);
            panelContainer.TabIndex = 0;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = Image.FromFile("logo.png");
            pictureBoxLogo.Location = new Point(60, 30);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(480, 120);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // btnTogglePassword
            // 
            btnTogglePassword.BackColor = Color.FromArgb(0, 120, 215);
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            btnTogglePassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
            btnTogglePassword.FlatStyle = FlatStyle.Flat;
            btnTogglePassword.Font = new Font("Segoe UI", 14F);
            btnTogglePassword.ForeColor = Color.White;
            btnTogglePassword.Location = new Point(485, 289);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(55, 42);
            btnTogglePassword.TabIndex = 0;
            btnTogglePassword.Text = "👁️";
            btnTogglePassword.UseVisualStyleBackColor = false;
            btnTogglePassword.Click += btnTogglePassword_Click;
            // 
            // Login
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 220, 240); // Softer blue background
            ClientSize = new Size(800, 650); // Increased form size
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - BAKERTILLY DMS";
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
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.LinkLabel lnkForgotPassword; // New Forgot Password link
    }
}