using System.Drawing;
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
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmail.Location = new Point(40, 100);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Enter your email";
            txtEmail.Size = new Size(420, 34);
            txtEmail.TabIndex = 1;
            // Add a bottom border to the TextBox for modern look (Blue)
            Panel txtEmailBorder = new Panel
            {
                BackColor = Color.FromArgb(0, 120, 215), // Blue border
                Size = new Size(420, 2),
                Location = new Point(40, 134)
            };
            panelContainer.Controls.Add(txtEmailBorder);

            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = Color.FromArgb(0, 120, 215); // Blue text
            lblEmail.Location = new Point(40, 60);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(71, 28);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email";

            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.FromArgb(0, 120, 215); // Blue
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180); // Slightly darker blue on hover
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(40, 160);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(207, 45);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            btnSubmit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 207, 45, 10, 10));

            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(173, 216, 230); // Light blue-gray to complement the blue theme
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 196, 210); // Slightly darker on hover
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.FromArgb(0, 120, 215); // Blue text to match the theme
            btnCancel.Location = new Point(253, 160);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(207, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(173, 216, 230);
            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 207, 45, 10, 10));

            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.None;
            panelContainer.Controls.Add(txtEmail);
            panelContainer.Controls.Add(lblEmail);
            panelContainer.Controls.Add(btnSubmit);
            panelContainer.Controls.Add(btnCancel);
            panelContainer.Location = new Point(50, 50);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(500, 250);
            panelContainer.TabIndex = 0;
            panelContainer.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 500, 250, 15, 15));
            panelContainer.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panelContainer.ClientRectangle,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid, // Blue border for shadow
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 120, 215), 1, ButtonBorderStyle.Solid);
            };

            // 
            // ForgotPasswordEmailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 220, 240); // Light blue background to match the theme
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

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}