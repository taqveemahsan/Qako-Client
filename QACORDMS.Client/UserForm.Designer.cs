namespace QACORDMS.Client
{
    partial class UserForm
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

        private System.Windows.Forms.DataGridView usersGridView;
        private System.Windows.Forms.Button addUserButton;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewButtonColumn deleteButtonColumn;

        private void InitializeComponent()
        {
            usersGridView = new DataGridView();
            deleteButtonColumn = new DataGridViewButtonColumn();
            addUserButton = new Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
            SuspendLayout();

            // usersGridView
            usersGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usersGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, deleteButtonColumn });
            usersGridView.Dock = DockStyle.Top;
            usersGridView.Location = new Point(0, 0);
            usersGridView.Margin = new Padding(4, 5, 4, 5);
            usersGridView.Name = "usersGridView";
            usersGridView.RowHeadersWidth = 51;
            usersGridView.Size = new Size(1067, 615);
            usersGridView.TabIndex = 0;
            usersGridView.CellContentClick += usersGridView_CellContentClick;

            // deleteButtonColumn
            deleteButtonColumn.HeaderText = "Action";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.MinimumWidth = 6;
            deleteButtonColumn.Name = "Delete"; // Name "Delete" rakha taaki logic match kare
            deleteButtonColumn.Width = 125;

            // addUserButton
            addUserButton.Location = new Point(863, 631);
            addUserButton.Margin = new Padding(4, 5, 4, 5);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(170, 35);
            addUserButton.TabIndex = 1;
            addUserButton.Text = "Add User";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += addUserButton_Click;

            // dataGridViewTextBoxColumn1
            dataGridViewTextBoxColumn1.HeaderText = "Full Name"; // Name ko Full Name kiya
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 150;

            // dataGridViewTextBoxColumn2
            dataGridViewTextBoxColumn2.HeaderText = "Email";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 150;

            // dataGridViewTextBoxColumn3
            dataGridViewTextBoxColumn3.HeaderText = "Username"; // Phone ko Username se replace
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;

            // dataGridViewTextBoxColumn4
            dataGridViewTextBoxColumn4.HeaderText = "Role"; // Role same rakha
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 125;

            // UserForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 692);
            Controls.Add(addUserButton);
            Controls.Add(usersGridView);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UserForm";
            Text = "Users Management";
            ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
            ResumeLayout(false);
        }
    }
}