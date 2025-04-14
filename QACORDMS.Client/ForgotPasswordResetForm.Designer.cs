using System.Drawing;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    partial class ForgotPasswordResetForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtOtp;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Label lblOtp;
        private Label lblNewPassword;
        private Label lblConfirmPassword;
        private Button btnReset;
        private Button btnCancel;
        private LinkLabel lnkResendOtp;
        private Panel panelContainer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtOtp = new TextBox();
            txtNewPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            lblOtp = new Label();
            lblNewPassword = new Label();
            lblConfirmPassword = new Label();
            btnReset = new Button();
            btnCancel = new Button();
            lnkResendOtp = new LinkLabel();
            panelContainer = new Panel();
            panelContainer.SuspendLayout();
            SuspendLayout();

            // 
            // txtOtp
            // 
            txtOtp.BackColor = Color.White;
            txtOtp.BorderStyle = BorderStyle.None;
            txtOtp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOtp.Location = new Point(40, 90);
            txtOtp.Name = "txtOtp";
            txtOtp.PlaceholderText = "Enter OTP";
            txtOtp.Size = new Size(420, 34);
            txtOtp.TabIndex = 1;
            // Add a bottom border to the TextBox for modern look (Blue)
            Panel txtOtpBorder = new Panel
            {
                BackColor = Color.FromArgb(0, 120, 215),
                Size = new Size(420, 2),
                Location = new Point(40, 124)
            };
            panelContainer.Controls.Add(txtOtpBorder);

            // 
            // txtNewPassword
            // 
            txtNewPassword.BackColor = Color.White;
            txtNewPassword.BorderStyle = BorderStyle.None;
            txtNewPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNewPassword.Location = new Point(40, 180);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.PlaceholderText = "Enter new password";
            txtNewPassword.Size = new Size(420, 34);
            txtNewPassword.TabIndex = 2;
            // Add a bottom border to the TextBox for modern look (Blue)
            Panel txtNewPasswordBorder = new Panel
            {
                BackColor = Color.FromArgb(0, 120, 215),
                Size = new Size(420, 2),
                Location = new Point(40, 214)
            };
            panelContainer.Controls.Add(txtNewPasswordBorder);

            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BackColor = Color.White;
            txtConfirmPassword.BorderStyle = BorderStyle.None;
            txtConfirmPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConfirmPassword.Location = new Point(40, 270);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.PlaceholderText = "Confirm new password";
            txtConfirmPassword.Size = new Size(420, 34);
            txtConfirmPassword.TabIndex = 3;
            // Add a bottom border to the TextBox for modern look (Blue)
            Panel txtConfirmPasswordBorder = new Panel
            {
                BackColor = Color.FromArgb(0, 120, 215),
                Size = new Size(420, 2),
                Location = new Point(40, 304)
            };
            panelContainer.Controls.Add(txtConfirmPasswordBorder);

            // 
            // lblOtp
            // 
            lblOtp.AutoSize = true;
            lblOtp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOtp.ForeColor = Color.FromArgb(0, 120, 215);
            lblOtp.Location = new Point(40, 50);
            lblOtp.Name = "lblOtp";
            lblOtp.Size = new Size(57, 28);
            lblOtp.TabIndex = 0;
            lblOtp.Text = "OTP";

            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNewPassword.ForeColor = Color.FromArgb(0, 120, 215);
            lblNewPassword.Location = new Point(40, 140);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(166, 28);
            lblNewPassword.TabIndex = 0;
            lblNewPassword.Text = "New Password";

            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblConfirmPassword.ForeColor = Color.FromArgb(0, 120, 215);
            lblConfirmPassword.Location = new Point(40, 230);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(204, 28);
            lblConfirmPassword.TabIndex = 0;
            lblConfirmPassword.Text = "Confirm Password";

            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(0, 120, 215);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(40, 340);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(207, 45);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset Password";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            btnReset.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 207, 45, 10, 10));

            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(173, 216, 230);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 196, 210);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.FromArgb(0, 120, 215);
            btnCancel.Location = new Point(253, 340);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(207, 45);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(173, 216, 230);
            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 207, 45, 10, 10));

            // 
            // lnkResendOtp
            // 
            lnkResendOtp.ActiveLinkColor = Color.FromArgb(0, 100, 180);
            lnkResendOtp.AutoSize = true;
            lnkResendOtp.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkResendOtp.LinkColor = Color.FromArgb(0, 120, 215);
            lnkResendOtp.VisitedLinkColor = Color.FromArgb(0, 120, 215);
            lnkResendOtp.Location = new Point(346, 130);
            lnkResendOtp.Name = "lnkResendOtp";
            lnkResendOtp.Size = new Size(114, 23);
            lnkResendOtp.TabIndex = 6;
            lnkResendOtp.TabStop = true;
            lnkResendOtp.Text = "Resend OTP";

            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.None;
            panelContainer.Controls.Add(txtOtp);
            panelContainer.Controls.Add(lblOtp);
            panelContainer.Controls.Add(txtNewPassword);
            panelContainer.Controls.Add(lblNewPassword);
            panelContainer.Controls.Add(txtConfirmPassword);
            panelContainer.Controls.Add(lblConfirmPassword);
            panelContainer.Controls.Add(lnkResendOtp);
            panelContainer.Controls.Add(btnReset);
            panelContainer.Controls.Add(btnCancel);
            panelContainer.Location = new Point(50, 40);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(500, 420);
            panelContainer.TabIndex = 0;
            panelContainer.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 500, 420, 15, 15));
            panelContainer.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panelContainer.ClientRectangle,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid);
            };

            // 
            // ForgotPasswordResetForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 220, 240);
            ClientSize = new Size(600, 500);
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ForgotPasswordResetForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Forgot Password - Reset Password";
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ResumeLayout(false);
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}