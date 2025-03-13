namespace QACORDMS.Client
{
    partial class AddNewProjectForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cmbProjectType;
        private System.Windows.Forms.Button btnCreateProject;

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
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cmbProjectType = new System.Windows.Forms.ComboBox();
            this.btnCreateProject = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.txtProjectName.Location = new System.Drawing.Point(30, 30);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(200, 20);
            this.txtProjectName.TabIndex = 0;
            this.txtProjectName.PlaceholderText = "Project Name";

            this.dtpStartDate.Location = new System.Drawing.Point(30, 70);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;

            this.dtpEndDate.Location = new System.Drawing.Point(30, 110);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 2;

            this.cmbProjectType.Location = new System.Drawing.Point(30, 150);
            this.cmbProjectType.Name = "cmbProjectType";
            this.cmbProjectType.Size = new System.Drawing.Size(200, 20);
            this.cmbProjectType.TabIndex = 3;
            this.cmbProjectType.Items.AddRange(new object[] { "Tax", "Audit" });

            this.btnCreateProject.Location = new System.Drawing.Point(30, 190);
            this.btnCreateProject.Name = "btnCreateProject";
            this.btnCreateProject.Size = new System.Drawing.Size(200, 30);
            this.btnCreateProject.TabIndex = 4;
            this.btnCreateProject.Text = "Create Project";
            this.btnCreateProject.UseVisualStyleBackColor = true;
            this.btnCreateProject.Click += new System.EventHandler(this.btnCreateProject_Click);

            this.ClientSize = new System.Drawing.Size(284, 250);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.cmbProjectType);
            this.Controls.Add(this.btnCreateProject);
            this.Name = "AddNewProjectForm";
            this.Text = "Add New Project";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}