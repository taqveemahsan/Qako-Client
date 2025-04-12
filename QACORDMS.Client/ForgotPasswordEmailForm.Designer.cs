using System;
using System.Windows.Forms;

namespace QACORDMS.Client
{

    partial class ForgotPasswordEmailForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtEmail;
        private Label lblEmail;
        private Button btnSubmit;
        private Button btnCancel;
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
            txtEmail = new TextBox();
            lblEmail = new Label();
            btnSubmit = new Button();
            btnCancel = new Button();
            panelContainer = new Panel();
            panelContainer.SuspendLayout();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(240, 240, 240);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmail.Location = new Point(40, 85);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Enter your email";
            txtEmail.Size = new Size(420, 39);
            txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = Color.FromArgb(0, 120, 215);
            lblEmail.Location = new Point(39, 50);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(71, 32);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email";
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.FromArgb(0, 120, 215);
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(40, 150);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(207, 50);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(200, 200, 200);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 180, 180);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(253, 150);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(207, 50);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.FixedSingle;
            panelContainer.Controls.Add(txtEmail);
            panelContainer.Controls.Add(lblEmail);
            panelContainer.Controls.Add(btnSubmit);
            panelContainer.Controls.Add(btnCancel);
            panelContainer.Location = new Point(50, 50);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(500, 250);
            panelContainer.TabIndex = 0;
            // 
            // ForgotPasswordEmailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 220, 240);
            ClientSize = new Size(600, 350);
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ForgotPasswordEmailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Forgot Password - Enter Email";
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ResumeLayout(false);
        }
    }
}