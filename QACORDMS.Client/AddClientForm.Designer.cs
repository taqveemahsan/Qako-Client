namespace QACORDMS.Client
{
    partial class AddClientForm
    {
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.ComboBox companyTypeComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label companyTypeLabel;
        private Panel loaderOverlay;
        private PictureBox loaderPictureBox;
        private Label loaderLabel;

        private void InitializeComponent()
        {
            nameTextBox = new TextBox();
            emailTextBox = new TextBox();
            phoneTextBox = new TextBox();
            addressTextBox = new TextBox();
            companyTypeComboBox = new ComboBox();
            addButton = new Button();
            titleLabel = new Label();
            nameLabel = new Label();
            emailLabel = new Label();
            phoneLabel = new Label();
            addressLabel = new Label();
            companyTypeLabel = new Label();
            loaderOverlay = new Panel();
            loaderPictureBox = new PictureBox();
            loaderLabel = new Label();
            SuspendLayout();
            // 
            // nameTextBox
            // 
            nameTextBox.BackColor = Color.White;
            nameTextBox.BorderStyle = BorderStyle.None;
            nameTextBox.Font = new Font("Segoe UI", 12F);
            nameTextBox.Location = new Point(30, 95);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Enter Client Name";
            nameTextBox.Size = new Size(560, 27);
            nameTextBox.TabIndex = 11;
            // 
            // emailTextBox
            // 
            emailTextBox.BackColor = Color.White;
            emailTextBox.BorderStyle = BorderStyle.None;
            emailTextBox.Font = new Font("Segoe UI", 12F);
            emailTextBox.Location = new Point(30, 165);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.PlaceholderText = "Enter Email Address";
            emailTextBox.Size = new Size(560, 27);
            emailTextBox.TabIndex = 10;
            // 
            // phoneTextBox
            // 
            phoneTextBox.BackColor = Color.White;
            phoneTextBox.BorderStyle = BorderStyle.None;
            phoneTextBox.Font = new Font("Segoe UI", 12F);
            phoneTextBox.Location = new Point(30, 235);
            phoneTextBox.Name = "phoneTextBox";
            phoneTextBox.PlaceholderText = "Enter Phone Number";
            phoneTextBox.Size = new Size(560, 27);
            phoneTextBox.TabIndex = 9;
            // 
            // addressTextBox
            // 
            addressTextBox.BackColor = Color.White;
            addressTextBox.BorderStyle = BorderStyle.None;
            addressTextBox.Font = new Font("Segoe UI", 12F);
            addressTextBox.Location = new Point(30, 305);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.PlaceholderText = "Enter Address";
            addressTextBox.Size = new Size(560, 27);
            addressTextBox.TabIndex = 8;
            // 
            // companyTypeComboBox
            // 
            companyTypeComboBox.BackColor = Color.White;
            companyTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            companyTypeComboBox.FlatStyle = FlatStyle.Flat;
            companyTypeComboBox.Font = new Font("Segoe UI", 12F);
            companyTypeComboBox.Items.AddRange(new object[] { "Private Ltd Companies", "Public Ltd Companies", "Foreign Companies", "Partnership Firms", "Non Profit Organizations", "NBFC", "PICS", "Provident & Gratuity Funds", "Individuals/Sole Proprietors","Others" });
            companyTypeComboBox.Location = new Point(30, 375);
            companyTypeComboBox.Name = "companyTypeComboBox";
            companyTypeComboBox.Size = new Size(250, 36);
            companyTypeComboBox.TabIndex = 7;
            // 
            // addButton
            // 
            addButton.BackColor = Color.FromArgb(33, 150, 243);
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(470, 375);
            addButton.Name = "addButton";
            addButton.Size = new Size(120, 35);
            addButton.TabIndex = 6;
            addButton.Text = "Add Client";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 150, 243);
            titleLabel.Location = new Point(28, 17);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(560, 40);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Add New Client";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            nameLabel.Font = new Font("Segoe UI", 10F);
            nameLabel.ForeColor = Color.FromArgb(50, 50, 50);
            nameLabel.Location = new Point(30, 70);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(100, 20);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Client Name";
            // 
            // emailLabel
            // 
            emailLabel.Font = new Font("Segoe UI", 10F);
            emailLabel.ForeColor = Color.FromArgb(50, 50, 50);
            emailLabel.Location = new Point(30, 140);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(100, 20);
            emailLabel.TabIndex = 2;
            emailLabel.Text = "Email Address";
            // 
            // phoneLabel
            // 
            phoneLabel.Font = new Font("Segoe UI", 10F);
            phoneLabel.ForeColor = Color.FromArgb(50, 50, 50);
            phoneLabel.Location = new Point(30, 210);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(100, 20);
            phoneLabel.TabIndex = 3;
            phoneLabel.Text = "Phone Number";
            // 
            // addressLabel
            // 
            addressLabel.Font = new Font("Segoe UI", 10F);
            addressLabel.ForeColor = Color.FromArgb(50, 50, 50);
            addressLabel.Location = new Point(30, 280);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(100, 20);
            addressLabel.TabIndex = 4;
            addressLabel.Text = "Address";
            // 
            // companyTypeLabel
            // 
            companyTypeLabel.Font = new Font("Segoe UI", 10F);
            companyTypeLabel.ForeColor = Color.FromArgb(50, 50, 50);
            companyTypeLabel.Location = new Point(30, 350);
            companyTypeLabel.Name = "companyTypeLabel";
            companyTypeLabel.Size = new Size(100, 20);
            companyTypeLabel.TabIndex = 5;
            companyTypeLabel.Text = "Company Type";
            // 
            // loaderOverlay
            // 
            loaderOverlay.BackColor = Color.FromArgb(120, 255, 255, 255); // semi-transparent white
            loaderOverlay.Dock = DockStyle.Fill;
            loaderOverlay.Visible = false;
            loaderOverlay.BringToFront();
            loaderOverlay.Controls.Add(loaderPictureBox);
            loaderOverlay.Controls.Add(loaderLabel);
            // loaderPictureBox
            loaderPictureBox.Size = new Size(64, 64);
            loaderPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            loaderPictureBox.Image = Image.FromFile("Images/spinner.gif");
            // loaderLabel
            loaderLabel.Text = "Creating client...";
            loaderLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            loaderLabel.ForeColor = Color.FromArgb(33, 150, 243);
            loaderLabel.AutoSize = true;
            // Add loaderOverlay last so it sits on top
            Controls.Add(loaderOverlay);
            // Center loader on form
            this.Load += (s, e) => CenterLoader();
            this.Resize += (s, e) => CenterLoader();
            // 
            // AddClientForm
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(620, 450);
            Controls.Add(titleLabel);
            Controls.Add(nameLabel);
            Controls.Add(emailLabel);
            Controls.Add(phoneLabel);
            Controls.Add(addressLabel);
            Controls.Add(companyTypeLabel);
            Controls.Add(addButton);
            Controls.Add(companyTypeComboBox);
            Controls.Add(addressTextBox);
            Controls.Add(phoneTextBox);
            Controls.Add(emailTextBox);
            Controls.Add(nameTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddClientForm";
            Padding = new Padding(30);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New Client";
            ResumeLayout(false);
            PerformLayout();
        }
        private void CenterLoader()
        {
            if (loaderPictureBox != null && loaderLabel != null && loaderOverlay.Visible)
            {
                loaderPictureBox.Location = new Point((this.ClientSize.Width - loaderPictureBox.Width) / 2, (this.ClientSize.Height - loaderPictureBox.Height) / 2 - 30);
                loaderLabel.Location = new Point((this.ClientSize.Width - loaderLabel.Width) / 2, (this.ClientSize.Height) / 2 + 40);
            }
        }
    }
}

















//namespace QACORDMS.Client
//{
//    partial class AddClientForm
//    {
//        private System.Windows.Forms.TextBox nameTextBox;
//        private System.Windows.Forms.TextBox emailTextBox;
//        private System.Windows.Forms.TextBox phoneTextBox;
//        private System.Windows.Forms.TextBox addressTextBox;
//        private System.Windows.Forms.ComboBox companyTypeComboBox;
//        private System.Windows.Forms.Button addButton;
//        private System.Windows.Forms.Label titleLabel; // Added for header

//        private void InitializeComponent()
//        {
//            this.nameTextBox = new System.Windows.Forms.TextBox();
//            this.emailTextBox = new System.Windows.Forms.TextBox();
//            this.phoneTextBox = new System.Windows.Forms.TextBox();
//            this.addressTextBox = new System.Windows.Forms.TextBox();
//            this.companyTypeComboBox = new System.Windows.Forms.ComboBox();
//            this.addButton = new System.Windows.Forms.Button();
//            this.titleLabel = new System.Windows.Forms.Label(); // New label
//            this.SuspendLayout();

//            // titleLabel (Header)
//            this.titleLabel.Location = new System.Drawing.Point(20, 20);
//            this.titleLabel.Name = "titleLabel";
//            this.titleLabel.Size = new System.Drawing.Size(460, 40);
//            this.titleLabel.TabIndex = 0;
//            this.titleLabel.Text = "Add New Client";
//            this.titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
//            this.titleLabel.ForeColor = Color.FromArgb(30, 144, 255); // Dodger Blue
//            this.titleLabel.TextAlign = ContentAlignment.MiddleLeft;

//            // nameTextBox
//            this.nameTextBox.Location = new System.Drawing.Point(20, 70);
//            this.nameTextBox.Name = "nameTextBox";
//            this.nameTextBox.Size = new System.Drawing.Size(460, 35); // Larger for bold look
//            this.nameTextBox.TabIndex = 1;
//            this.nameTextBox.PlaceholderText = "Enter Client Name";
//            this.nameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            this.nameTextBox.BackColor = Color.FromArgb(245, 245, 245); // Soft gray
//            this.nameTextBox.BorderStyle = BorderStyle.FixedSingle; // Subtle border

//            // emailTextBox
//            this.emailTextBox.Location = new System.Drawing.Point(20, 115);
//            this.emailTextBox.Name = "emailTextBox";
//            this.emailTextBox.Size = new System.Drawing.Size(460, 35);
//            this.emailTextBox.TabIndex = 2;
//            this.emailTextBox.PlaceholderText = "Enter Email Address";
//            this.emailTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            this.emailTextBox.BackColor = Color.FromArgb(245, 245, 245);
//            this.emailTextBox.BorderStyle = BorderStyle.FixedSingle;

//            // phoneTextBox
//            this.phoneTextBox.Location = new System.Drawing.Point(20, 160);
//            this.phoneTextBox.Name = "phoneTextBox";
//            this.phoneTextBox.Size = new System.Drawing.Size(460, 35);
//            this.phoneTextBox.TabIndex = 3;
//            this.phoneTextBox.PlaceholderText = "Enter Phone Number";
//            this.phoneTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            this.phoneTextBox.BackColor = Color.FromArgb(245, 245, 245);
//            this.phoneTextBox.BorderStyle = BorderStyle.FixedSingle;

//            // addressTextBox
//            this.addressTextBox.Location = new System.Drawing.Point(20, 205);
//            this.addressTextBox.Name = "addressTextBox";
//            this.addressTextBox.Size = new System.Drawing.Size(460, 35);
//            this.addressTextBox.TabIndex = 4;
//            this.addressTextBox.PlaceholderText = "Enter Address";
//            this.addressTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
//            this.addressTextBox.BackColor = Color.FromArgb(245, 245, 245);
//            this.addressTextBox.BorderStyle = BorderStyle.FixedSingle;

//            // companyTypeComboBox
//            this.companyTypeComboBox.FormattingEnabled = true;
//            this.companyTypeComboBox.Items.AddRange(new object[] {
//                "Private Label",
//                "Public Company"
//            });
//            this.companyTypeComboBox.Location = new System.Drawing.Point(20, 250);
//            this.companyTypeComboBox.Name = "companyTypeComboBox";
//            this.companyTypeComboBox.Size = new System.Drawing.Size(460, 40); // Taller for style
//            this.companyTypeComboBox.TabIndex = 5;
//            this.companyTypeComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            this.companyTypeComboBox.BackColor = Color.FromArgb(245, 245, 245);
//            this.companyTypeComboBox.FlatStyle = FlatStyle.Flat; // Modern flat look
//            this.companyTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Cleaner UI

//            // addButton
//            this.addButton.Location = new System.Drawing.Point(330, 305); // Adjusted for new layout
//            this.addButton.Name = "addButton";
//            this.addButton.Size = new System.Drawing.Size(150, 50); // Larger, bold button
//            this.addButton.TabIndex = 6;
//            this.addButton.Text = "Add Client";
//            this.addButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//            this.addButton.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
//            this.addButton.ForeColor = Color.White;
//            this.addButton.FlatStyle = FlatStyle.Flat;
//            this.addButton.FlatAppearance.BorderSize = 0; // No border
//            this.addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210); // Hover effect
//            this.addButton.UseVisualStyleBackColor = false;
//            this.addButton.Click += new System.EventHandler(this.addButton_Click);

//            // AddClientForm
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(500, 375); // Larger form size
//            this.Controls.Add(this.titleLabel); // Added header
//            this.Controls.Add(this.addButton);
//            this.Controls.Add(this.companyTypeComboBox);
//            this.Controls.Add(this.addressTextBox);
//            this.Controls.Add(this.phoneTextBox);
//            this.Controls.Add(this.emailTextBox);
//            this.Controls.Add(this.nameTextBox);
//            this.BackColor = Color.FromArgb(240, 248, 255); // Alice Blue for vibrancy
//            this.FormBorderStyle = FormBorderStyle.FixedSingle;
//            this.Name = "AddClientForm";
//            this.Text = "Add New Client";
//            this.StartPosition = FormStartPosition.CenterScreen;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }
//    }
//}