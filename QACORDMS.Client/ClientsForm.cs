using QACORDMS.Client.Helpers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class ClientsForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private int currentPage = 1;
        private const int pageSize = 10;
        private string searchQuery = "";
        private int totalPages = 1;

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
            var response = await _apiHelper.GetClientsAsync(searchQuery, currentPage, pageSize);
            this.totalPages = response.TotalPages;
            clientsGridView.Rows.Clear();

            foreach (var client in response.Clients)
            {
                var index = clientsGridView.Rows.Add(client.Name, client.Email, client.Phone);
                clientsGridView.Rows[index].Cells["AddProjectButton"].Value = "Add Project"; // Ensure text is set
                clientsGridView.Rows[index].Cells["Delete"].Value = "Delete"; // Ensure text is set
                clientsGridView.Rows[index].Tag = client.Id;
            }

            pageLabel.Text = $"Page {currentPage} of {totalPages}";
            prevButton.Enabled = currentPage > 1;
            nextButton.Enabled = currentPage < totalPages;
        }

        private void OpenAddProjectForm(Guid clientId)
        {
            var addProjectForm = new AddNewProjectForm(clientId, _apiHelper);
            addProjectForm.ShowDialog();
        }

        private async void clientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Null check for columns
            if (clientsGridView.Columns["Delete"] == null || clientsGridView.Columns["AddProjectButton"] == null)
            {
                MessageBox.Show("Error: Delete or AddProjectButton column not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (e.ColumnIndex == clientsGridView.Columns["Delete"].Index)
            {
                var clientId = (Guid)clientsGridView.Rows[e.RowIndex].Tag;
                var result = MessageBox.Show("Are you sure you want to delete this client?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var success = await _apiHelper.DeleteClientAsync(clientId);
                    if (success)
                    {
                        MessageBox.Show("Client deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadClientsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (e.ColumnIndex == clientsGridView.Columns["AddProjectButton"].Index)
            {
                var clientId = (Guid)clientsGridView.Rows[e.RowIndex].Tag;
                OpenAddProjectForm(clientId);
            }
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            searchQuery = searchTextBox.Text.Trim();
            currentPage = 1;
            await LoadClientsAsync();
        }

        private async void prevButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await LoadClientsAsync();
            }
        }

        private async void nextButton_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await LoadClientsAsync();
            }
        }
    }
}















//using QACORDMS.Client.Helpers;
//using System;
//using System.Drawing;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace QACORDMS.Client
//{
//    public partial class ClientsForm : Form
//    {
//        public readonly QACOAPIHelper _apiHelper;

//        public ClientsForm(QACOAPIHelper apiHelper)
//        {
//            _apiHelper = apiHelper;
//            InitializeComponent();
//            LoadClientsAsync().ConfigureAwait(false);
//        }


//        private async void addClientButton_Click(object sender, EventArgs e)
//        {
//            var addClientForm = new AddClientForm(_apiHelper);
//            addClientForm.ShowDialog();
//            await LoadClientsAsync();
//        }

//        private async Task LoadClientsAsync()
//        {
//            var clients = await _apiHelper.GetClientsAsync();
//            clientsGridView.Rows.Clear();

//            foreach (var client in clients)
//            {
//                var index = clientsGridView.Rows.Add(client.Name, client.Email, client.Phone);
//                clientsGridView.Rows[index].Cells["AddProjectButton"].Value = "Add Project";
//                clientsGridView.Rows[index].Tag = client.Id;
//            }
//        }

//        private void OpenAddProjectForm(Guid clientId)
//        {
//            var addProjectForm = new AddNewProjectForm(clientId, _apiHelper);
//            addProjectForm.ShowDialog();
//        }


//        private async void clientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.ColumnIndex == clientsGridView.Columns["Delete"].Index && e.RowIndex >= 0)
//            {
//                var clientId = (Guid)clientsGridView.Rows[e.RowIndex].Cells["Id"].Value;
//                //await _apiHelper.DeleteClientAsync(clientId);
//                await LoadClientsAsync();
//            }
//            if (e.ColumnIndex == clientsGridView.Columns["AddProjectButton"].Index && e.RowIndex >= 0)
//            {
//                var clientId = (Guid)clientsGridView.Rows[e.RowIndex].Tag;
//                OpenAddProjectForm(clientId);
//            }
//        }

//    }
//}
