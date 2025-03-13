using QACORDMS.Client.Helpers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class ClientsForm : Form
    {
        public readonly QACOAPIHelper _apiHelper;

        public ClientsForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
            InitializeComponent();
            LoadClientsAsync().ConfigureAwait(false);
        }

    
        private async void addClientButton_Click(object sender, EventArgs e)
        {
            var addClientForm = new AddClientForm(_apiHelper);
            addClientForm.ShowDialog();
            await LoadClientsAsync();
        }

        private async Task LoadClientsAsync()
        {
            var clients = await _apiHelper.GetClientsAsync();
            clientsGridView.Rows.Clear();

            foreach (var client in clients)
            {
                var index = clientsGridView.Rows.Add(client.Name, client.Email, client.Phone);
                clientsGridView.Rows[index].Cells["AddProjectButton"].Value = "Add Project";
                clientsGridView.Rows[index].Tag = client.Id;
            }
        }

        private void OpenAddProjectForm(Guid clientId)
        {
            var addProjectForm = new AddNewProjectForm(clientId, _apiHelper);
            addProjectForm.ShowDialog();
        }


        private async void clientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == clientsGridView.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var clientId = (Guid)clientsGridView.Rows[e.RowIndex].Cells["Id"].Value;
                //await _apiHelper.DeleteClientAsync(clientId);
                await LoadClientsAsync();
            }
            if (e.ColumnIndex == clientsGridView.Columns["AddProjectButton"].Index && e.RowIndex >= 0)
            {
                var clientId = (Guid)clientsGridView.Rows[e.RowIndex].Tag;
                OpenAddProjectForm(clientId);
            }
        }

    }
}
