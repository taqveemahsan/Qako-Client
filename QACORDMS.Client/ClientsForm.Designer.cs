namespace QACORDMS.Client
{
    public partial class ClientsForm : Form
    {
        private System.Windows.Forms.DataGridView clientsGridView;
        private System.Windows.Forms.Button addClientButton;

        private void InitializeComponent()
        {
            this.clientsGridView = new System.Windows.Forms.DataGridView();
            this.addClientButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.clientsGridView)).BeginInit();
            this.SuspendLayout();

            // clientsGridView
            this.clientsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.clientsGridView.Location = new System.Drawing.Point(0, 0);
            this.clientsGridView.Name = "clientsGridView";
            this.clientsGridView.Size = new System.Drawing.Size(800, 400);
            this.clientsGridView.TabIndex = 0;
            this.clientsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clientsGridView_CellContentClick);

            this.clientsGridView.Columns.Add("Name", "Name");
            this.clientsGridView.Columns.Add("Email", "Email");
            this.clientsGridView.Columns.Add("Phone", "Phone");

            var addProjectButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn
            {
                Name = "AddProjectButton",
                Text = "Add Project",
                UseColumnTextForButtonValue = true
            };
            this.clientsGridView.Columns.Add(addProjectButtonColumn);

            var deleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            this.clientsGridView.Columns.Add(deleteButtonColumn);

            // addClientButton
            this.addClientButton.Location = new System.Drawing.Point(700, 410);
            this.addClientButton.Name = "addClientButton";
            this.addClientButton.Size = new System.Drawing.Size(75, 23);
            this.addClientButton.TabIndex = 1;
            this.addClientButton.Text = "Add Client";
            this.addClientButton.UseVisualStyleBackColor = true;
            this.addClientButton.Click += new System.EventHandler(this.addClientButton_Click);

            // ClientsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addClientButton);
            this.Controls.Add(this.clientsGridView);
            this.Name = "ClientsForm";
            this.Text = "Clients Management";
            ((System.ComponentModel.ISupportInitialize)(this.clientsGridView)).EndInit();
            this.ResumeLayout(false);
        }
    }
}