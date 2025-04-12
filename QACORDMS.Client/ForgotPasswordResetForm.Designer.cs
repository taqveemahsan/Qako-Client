using System;
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
        private LinkLabel lnkResendOtp; // Added for Resend OTP
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
            txtOtp.BackColor = Color.FromArgb(240, 240, 240);
            txtOtp.BorderStyle = BorderStyle.FixedSingle;
            txtOtp.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOtp.Location = new Point(40, 89);
            txtOtp.Name = "txtOtp";
            txtOtp.PlaceholderText = "Enter OTP";
            txtOtp.Size = new Size(420, 39);
            txtOtp.TabIndex = 1;
            // 
            // txtNewPassword
            // 
            txtNewPassword.BackColor = Color.FromArgb(240, 240, 240);
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNewPassword.Location = new Point(40, 194);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.PlaceholderText = "Enter new password";
            txtNewPassword.Size = new Size(420, 39);
            txtNewPassword.TabIndex = 2;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BackColor = Color.FromArgb(240, 240, 240);
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConfirmPassword.Location = new Point(40, 279);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.PlaceholderText = "Confirm new password";
            txtConfirmPassword.Size = new Size(420, 39);
            txtConfirmPassword.TabIndex = 3;
            // 
            // lblOtp
            // 
            lblOtp.AutoSize = true;
            lblOtp.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOtp.ForeColor = Color.FromArgb(0, 120, 215);
            lblOtp.Location = new Point(40, 54);
            lblOtp.Name = "lblOtp";
            lblOtp.Size = new Size(57, 32);
            lblOtp.TabIndex = 0;
            lblOtp.Text = "OTP";
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNewPassword.ForeColor = Color.FromArgb(0, 120, 215);
            lblNewPassword.Location = new Point(40, 159);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(166, 32);
            lblNewPassword.TabIndex = 0;
            lblNewPassword.Text = "New Password";
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblConfirmPassword.ForeColor = Color.FromArgb(0, 120, 215);
            lblConfirmPassword.Location = new Point(40, 244);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(204, 32);
            lblConfirmPassword.TabIndex = 0;
            lblConfirmPassword.Text = "Confirm Password";
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(0, 120, 215);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(55, 360);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(190, 50);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset Password";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(200, 200, 200);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 180, 180);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(251, 360);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(190, 50);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // lnkResendOtp
            // 
            lnkResendOtp.ActiveLinkColor = Color.FromArgb(0, 100, 180);
            lnkResendOtp.AutoSize = true;
            lnkResendOtp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkResendOtp.LinkColor = Color.FromArgb(0, 120, 215);
            lnkResendOtp.Location = new Point(346, 131);
            lnkResendOtp.Name = "lnkResendOtp";
            lnkResendOtp.Size = new Size(114, 28);
            lnkResendOtp.TabIndex = 6;
            lnkResendOtp.TabStop = true;
            lnkResendOtp.Text = "Resend OTP";
            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.FixedSingle;
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
            panelContainer.Size = new Size(500, 448);
            panelContainer.TabIndex = 0;
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

        //partial class ForgotPasswordResetForm
        //{
        //    private System.ComponentModel.IContainer components = null;
        //    private TextBox txtOtp;
        //    private TextBox txtNewPassword;
        //    private TextBox txtConfirmPassword;
        //    private Label lblOtp;
        //    private Label lblNewPassword;
        //    private Label lblConfirmPassword;
        //    private Button btnReset;
        //    private Button btnCancel;
        //    private Panel panelContainer;

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing && (components != null))
        //        {
        //            components.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }

        //    private void InitializeComponent()
        //    {
        //        components = new System.ComponentModel.Container();
        //        txtOtp = new TextBox();
        //        txtNewPassword = new TextBox();
        //        txtConfirmPassword = new TextBox();
        //        lblOtp = new Label();
        //        lblNewPassword = new Label();
        //        lblConfirmPassword = new Label();
        //        btnReset = new Button();
        //        btnCancel = new Button();
        //        panelContainer = new Panel();
        //        panelContainer.SuspendLayout();
        //        SuspendLayout();
        //        // 
        //        // lblOtp
        //        // 
        //        lblOtp.AutoSize = true;
        //        lblOtp.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //        lblOtp.ForeColor = Color.FromArgb(0, 120, 215);
        //        lblOtp.Location = new Point(40, 50);
        //        lblOtp.Name = "lblOtp";
        //        lblOtp.Size = new Size(50, 32);
        //        lblOtp.TabIndex = 0;
        //        lblOtp.Text = "OTP";
        //        // 
        //        // txtOtp
        //        // 
        //        txtOtp.BackColor = Color.FromArgb(240, 240, 240);
        //        txtOtp.BorderStyle = BorderStyle.FixedSingle;
        //        txtOtp.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //        txtOtp.Location = new Point(40, 85);
        //        txtOtp.Name = "txtOtp";
        //        txtOtp.PlaceholderText = "Enter OTP";
        //        txtOtp.Size = new Size(420, 40);
        //        txtOtp.TabIndex = 1;
        //        // 
        //        // lblNewPassword
        //        // 
        //        lblNewPassword.AutoSize = true;
        //        lblNewPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //        lblNewPassword.ForeColor = Color.FromArgb(0, 120, 215);
        //        lblNewPassword.Location = new Point(40, 135);
        //        lblNewPassword.Name = "lblNewPassword";
        //        lblNewPassword.Size = new Size(130, 32);
        //        lblNewPassword.TabIndex = 0;
        //        lblNewPassword.Text = "New Password";
        //        // 
        //        // txtNewPassword
        //        // 
        //        txtNewPassword.BackColor = Color.FromArgb(240, 240, 240);
        //        txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
        //        txtNewPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //        txtNewPassword.Location = new Point(40, 170);
        //        txtNewPassword.Name = "txtNewPassword";
        //        txtNewPassword.PlaceholderText = "Enter new password";
        //        txtNewPassword.Size = new Size(420, 40);
        //        txtNewPassword.TabIndex = 2;
        //        txtNewPassword.PasswordChar = '*';
        //        // 
        //        // lblConfirmPassword
        //        // 
        //        lblConfirmPassword.AutoSize = true;
        //        lblConfirmPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //        lblConfirmPassword.ForeColor = Color.FromArgb(0, 120, 215);
        //        lblConfirmPassword.Location = new Point(40, 220);
        //        lblConfirmPassword.Name = "lblConfirmPassword";
        //        lblConfirmPassword.Size = new Size(160, 32);
        //        lblConfirmPassword.TabIndex = 0;
        //        lblConfirmPassword.Text = "Confirm Password";
        //        // 
        //        // txtConfirmPassword
        //        // 
        //        txtConfirmPassword.BackColor = Color.FromArgb(240, 240, 240);
        //        txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
        //        txtConfirmPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        //        txtConfirmPassword.Location = new Point(40, 255);
        //        txtConfirmPassword.Name = "txtConfirmPassword";
        //        txtConfirmPassword.PlaceholderText = "Confirm new password";
        //        txtConfirmPassword.Size = new Size(420, 40);
        //        txtConfirmPassword.TabIndex = 3;
        //        txtConfirmPassword.PasswordChar = '*';
        //        // 
        //        // btnReset
        //        // 
        //        btnReset.BackColor = Color.FromArgb(0, 120, 215);
        //        btnReset.FlatAppearance.BorderSize = 0;
        //        btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
        //        btnReset.FlatStyle = FlatStyle.Flat;
        //        btnReset.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
        //        btnReset.ForeColor = Color.White;
        //        btnReset.Location = new Point(40, 320);
        //        btnReset.Name = "btnReset";
        //        btnReset.Size = new Size(190, 50);
        //        btnReset.TabIndex = 4;
        //        btnReset.Text = "Reset Password";
        //        btnReset.UseVisualStyleBackColor = false;
        //        // 
        //        // btnCancel
        //        // 
        //        btnCancel.BackColor = Color.FromArgb(200, 200, 200);
        //        btnCancel.FlatAppearance.BorderSize = 0;
        //        btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 180, 180);
        //        btnCancel.FlatStyle = FlatStyle.Flat;
        //        btnCancel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
        //        btnCancel.ForeColor = Color.White;
        //        btnCancel.Location = new Point(270, 320);
        //        btnCancel.Name = "btnCancel";
        //        btnCancel.Size = new Size(190, 50);
        //        btnCancel.TabIndex = 5;
        //        btnCancel.Text = "Cancel";
        //        btnCancel.UseVisualStyleBackColor = false;
        //        // 
        //        // panelContainer
        //        // 
        //        panelContainer.BackColor = Color.White;
        //        panelContainer.BorderStyle = BorderStyle.FixedSingle;
        //        panelContainer.Controls.Add(txtOtp);
        //        panelContainer.Controls.Add(lblOtp);
        //        panelContainer.Controls.Add(txtNewPassword);
        //        panelContainer.Controls.Add(lblNewPassword);
        //        panelContainer.Controls.Add(txtConfirmPassword);
        //        panelContainer.Controls.Add(lblConfirmPassword);
        //        panelContainer.Controls.Add(btnReset);
        //        panelContainer.Controls.Add(btnCancel);
        //        panelContainer.Location = new Point(50, 50);
        //        panelContainer.Name = "panelContainer";
        //        panelContainer.Size = new Size(500, 400);
        //        panelContainer.TabIndex = 0;
        //        // 
        //        // ForgotPasswordResetForm
        //        // 
        //        AutoScaleDimensions = new SizeF(8F, 20F);
        //        AutoScaleMode = AutoScaleMode.Font;
        //        BackColor = Color.FromArgb(200, 220, 240);
        //        ClientSize = new Size(600, 500);
        //        Controls.Add(panelContainer);
        //        FormBorderStyle = FormBorderStyle.FixedDialog;
        //        MaximizeBox = false;
        //        MinimizeBox = false;
        //        Name = "ForgotPasswordResetForm";
        //        StartPosition = FormStartPosition.CenterParent;
        //        Text = "Forgot Password - Reset Password";
        //        panelContainer.ResumeLayout(false);
        //        panelContainer.PerformLayout();
        //        ResumeLayout(false);
        //    }
        //}
    }
}