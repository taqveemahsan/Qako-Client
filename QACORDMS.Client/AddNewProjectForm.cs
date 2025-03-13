using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class AddNewProjectForm : Form
    {
        private readonly Guid _clientId;
        private readonly QACOAPIHelper _apiHelper;

        public AddNewProjectForm(Guid clientId, QACOAPIHelper apiHelper)
        {
            InitializeComponent();
            _clientId = clientId;
            _apiHelper = apiHelper;
        }

        private async void btnCreateProject_Click(object sender, EventArgs e)
        {
            if (cmbProjectType.SelectedItem == null)
            {
                MessageBox.Show("Please select a project type.");
                return;
            }

            var projectDto = new ClientProject
            {
                ClientId = _clientId,
                ProjectName = txtProjectName.Text,
                ProjectType = (ProjectType)Enum.Parse(typeof(ProjectType), cmbProjectType.SelectedItem.ToString()),
                StartDate = dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
                GoogleDriveFolderId = string.Empty
            };

            var response = await _apiHelper.CreateClientProjectAsync(projectDto);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Project created successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
        }
    }
}