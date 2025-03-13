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

        private void InitializeComponent()
        {
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.companyTypeComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // nameTextBox
            this.nameTextBox.Location = new System.Drawing.Point(12, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(260, 20);
            this.nameTextBox.TabIndex = 0;
            this.nameTextBox.PlaceholderText = "Name";

            // emailTextBox
            this.emailTextBox.Location = new System.Drawing.Point(12, 38);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(260, 20);
            this.emailTextBox.TabIndex = 1;
            this.emailTextBox.PlaceholderText = "Email";

            // phoneTextBox
            this.phoneTextBox.Location = new System.Drawing.Point(12, 64);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(260, 20);
            this.phoneTextBox.TabIndex = 2;
            this.phoneTextBox.PlaceholderText = "Phone";

            // addressTextBox
            this.addressTextBox.Location = new System.Drawing.Point(12, 90);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(260, 20);
            this.addressTextBox.TabIndex = 3;
            this.addressTextBox.PlaceholderText = "Address";

            // companyTypeComboBox
            this.companyTypeComboBox.FormattingEnabled = true;
            this.companyTypeComboBox.Items.AddRange(new object[] {
                "Private Label",
                "Public Comp"});
            this.companyTypeComboBox.Location = new System.Drawing.Point(12, 116);
            this.companyTypeComboBox.Name = "companyTypeComboBox";
            this.companyTypeComboBox.Size = new System.Drawing.Size(260, 21);
            this.companyTypeComboBox.TabIndex = 4;

            // addButton
            this.addButton.Location = new System.Drawing.Point(197, 143);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add Client";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);

            // AddClientForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.companyTypeComboBox);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Name = "AddClientForm";
            this.Text = "Add New Client";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}