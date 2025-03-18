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
        private System.Windows.Forms.Label titleLabel; // Added for header

        private void InitializeComponent()
        {
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.companyTypeComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label(); // New label
            this.SuspendLayout();

            // titleLabel (Header)
            this.titleLabel.Location = new System.Drawing.Point(20, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(460, 40);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Add New Client";
            this.titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            this.titleLabel.TextAlign = ContentAlignment.MiddleLeft;

            // nameTextBox
            this.nameTextBox.Location = new System.Drawing.Point(20, 70);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(460, 35); // Larger for bold look
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.PlaceholderText = "Enter Client Name";
            this.nameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.nameTextBox.BackColor = Color.FromArgb(245, 245, 245); // Soft gray
            this.nameTextBox.BorderStyle = BorderStyle.FixedSingle; // Subtle border

            // emailTextBox
            this.emailTextBox.Location = new System.Drawing.Point(20, 115);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(460, 35);
            this.emailTextBox.TabIndex = 2;
            this.emailTextBox.PlaceholderText = "Enter Email Address";
            this.emailTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.emailTextBox.BackColor = Color.FromArgb(245, 245, 245);
            this.emailTextBox.BorderStyle = BorderStyle.FixedSingle;

            // phoneTextBox
            this.phoneTextBox.Location = new System.Drawing.Point(20, 160);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(460, 35);
            this.phoneTextBox.TabIndex = 3;
            this.phoneTextBox.PlaceholderText = "Enter Phone Number";
            this.phoneTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.phoneTextBox.BackColor = Color.FromArgb(245, 245, 245);
            this.phoneTextBox.BorderStyle = BorderStyle.FixedSingle;

            // addressTextBox
            this.addressTextBox.Location = new System.Drawing.Point(20, 205);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(460, 35);
            this.addressTextBox.TabIndex = 4;
            this.addressTextBox.PlaceholderText = "Enter Address";
            this.addressTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.addressTextBox.BackColor = Color.FromArgb(245, 245, 245);
            this.addressTextBox.BorderStyle = BorderStyle.FixedSingle;

            // companyTypeComboBox
            this.companyTypeComboBox.FormattingEnabled = true;
            this.companyTypeComboBox.Items.AddRange(new object[] {
                "Private Label",
                "Public Company"
            });
            this.companyTypeComboBox.Location = new System.Drawing.Point(20, 250);
            this.companyTypeComboBox.Name = "companyTypeComboBox";
            this.companyTypeComboBox.Size = new System.Drawing.Size(460, 40); // Taller for style
            this.companyTypeComboBox.TabIndex = 5;
            this.companyTypeComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.companyTypeComboBox.BackColor = Color.FromArgb(245, 245, 245);
            this.companyTypeComboBox.FlatStyle = FlatStyle.Flat; // Modern flat look
            this.companyTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Cleaner UI

            // addButton
            this.addButton.Location = new System.Drawing.Point(330, 305); // Adjusted for new layout
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(150, 50); // Larger, bold button
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Add Client";
            this.addButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.addButton.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            this.addButton.ForeColor = Color.White;
            this.addButton.FlatStyle = FlatStyle.Flat;
            this.addButton.FlatAppearance.BorderSize = 0; // No border
            this.addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 120, 210); // Hover effect
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);

            // AddClientForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 375); // Larger form size
            this.Controls.Add(this.titleLabel); // Added header
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.companyTypeComboBox);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.nameTextBox);
            this.BackColor = Color.FromArgb(240, 248, 255); // Alice Blue for vibrancy
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "AddClientForm";
            this.Text = "Add New Client";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
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

//        private void InitializeComponent()
//        {
//            this.nameTextBox = new System.Windows.Forms.TextBox();
//            this.emailTextBox = new System.Windows.Forms.TextBox();
//            this.phoneTextBox = new System.Windows.Forms.TextBox();
//            this.addressTextBox = new System.Windows.Forms.TextBox();
//            this.companyTypeComboBox = new System.Windows.Forms.ComboBox();
//            this.addButton = new System.Windows.Forms.Button();
//            this.SuspendLayout();

//            // nameTextBox
//            this.nameTextBox.Location = new System.Drawing.Point(12, 12);
//            this.nameTextBox.Name = "nameTextBox";
//            this.nameTextBox.Size = new System.Drawing.Size(260, 20);
//            this.nameTextBox.TabIndex = 0;
//            this.nameTextBox.PlaceholderText = "Name";

//            // emailTextBox
//            this.emailTextBox.Location = new System.Drawing.Point(12, 38);
//            this.emailTextBox.Name = "emailTextBox";
//            this.emailTextBox.Size = new System.Drawing.Size(260, 20);
//            this.emailTextBox.TabIndex = 1;
//            this.emailTextBox.PlaceholderText = "Email";

//            // phoneTextBox
//            this.phoneTextBox.Location = new System.Drawing.Point(12, 64);
//            this.phoneTextBox.Name = "phoneTextBox";
//            this.phoneTextBox.Size = new System.Drawing.Size(260, 20);
//            this.phoneTextBox.TabIndex = 2;
//            this.phoneTextBox.PlaceholderText = "Phone";

//            // addressTextBox
//            this.addressTextBox.Location = new System.Drawing.Point(12, 90);
//            this.addressTextBox.Name = "addressTextBox";
//            this.addressTextBox.Size = new System.Drawing.Size(260, 20);
//            this.addressTextBox.TabIndex = 3;
//            this.addressTextBox.PlaceholderText = "Address";

//            // companyTypeComboBox
//            this.companyTypeComboBox.FormattingEnabled = true;
//            this.companyTypeComboBox.Items.AddRange(new object[] {
//                "Private Label",
//                "Public Comp"});
//            this.companyTypeComboBox.Location = new System.Drawing.Point(12, 116);
//            this.companyTypeComboBox.Name = "companyTypeComboBox";
//            this.companyTypeComboBox.Size = new System.Drawing.Size(260, 21);
//            this.companyTypeComboBox.TabIndex = 4;

//            // addButton
//            this.addButton.Location = new System.Drawing.Point(197, 143);
//            this.addButton.Name = "addButton";
//            this.addButton.Size = new System.Drawing.Size(75, 23);
//            this.addButton.TabIndex = 5;
//            this.addButton.Text = "Add Client";
//            this.addButton.UseVisualStyleBackColor = true;
//            this.addButton.Click += new System.EventHandler(this.addButton_Click);

//            // AddClientForm
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(284, 181);
//            this.Controls.Add(this.addButton);
//            this.Controls.Add(this.companyTypeComboBox);
//            this.Controls.Add(this.addressTextBox);
//            this.Controls.Add(this.phoneTextBox);
//            this.Controls.Add(this.emailTextBox);
//            this.Controls.Add(this.nameTextBox);
//            this.Name = "AddClientForm";
//            this.Text = "Add New Client";
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }
//    }
//}