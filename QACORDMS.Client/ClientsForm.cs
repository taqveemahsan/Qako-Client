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

            // Add KeyDown event for searchTextBox to handle Enter key
            searchTextBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Prevent the "ding" sound on Enter
                    searchButton_Click(sender, e); // Trigger the search button click event
                }
            };

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
                var index = clientsGridView.Rows.Add(client.Name, client.Email, client.Phone, client.FolderSize);
                //clientsGridView.Rows[index].Cells["AddProjectButton"].Value = "Add Project";
                clientsGridView.Rows[index].Cells["Delete"].Value = "Delete";
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

        private void AdjustColumnWidths()
        {
            // Get the total available width of the DataGridView (excluding scrollbars or borders)
            int totalWidth = clientsGridView.ClientSize.Width;

            // Define the proportions for each column (total should sum to 1.0)
            float nameProportion = 0.30f; // 30% for Name
            float emailProportion = 0.25f; // 25% for Email
            float phoneProportion = 0.20f; // 20% for Phone
            float folderSizeProportion = 0.15f; // 15% for Folder Size
            float deleteProportion = 0.10f; // 10% for Delete button

            // Calculate the width for each column based on the proportions
            int nameWidth = Math.Max((int)(totalWidth * nameProportion), dataGridViewTextBoxColumn1.MinimumWidth);
            int emailWidth = Math.Max((int)(totalWidth * emailProportion), dataGridViewTextBoxColumn2.MinimumWidth);
            int phoneWidth = Math.Max((int)(totalWidth * phoneProportion), dataGridViewTextBoxColumn3.MinimumWidth);
            int folderSizeWidth = Math.Max((int)(totalWidth * folderSizeProportion), dataGridViewTextBoxColumn4.MinimumWidth);
            int deleteWidth = Math.Max((int)(totalWidth * deleteProportion), deleteButtonColumn.MinimumWidth);

            // Calculate total calculated width
            int totalCalculatedWidth = nameWidth + emailWidth + phoneWidth + folderSizeWidth + deleteWidth;

            // Adjust widths to exactly fit the totalWidth (to avoid scrollbars)
            if (totalCalculatedWidth != totalWidth)
            {
                // Scale all widths proportionally to fit exactly
                float scaleFactor = (float)totalWidth / totalCalculatedWidth;
                nameWidth = (int)(nameWidth * scaleFactor);
                emailWidth = (int)(emailWidth * scaleFactor);
                phoneWidth = (int)(phoneWidth * scaleFactor);
                folderSizeWidth = (int)(folderSizeWidth * scaleFactor);
                deleteWidth = totalWidth - (nameWidth + emailWidth + phoneWidth + folderSizeWidth); // Assign remaining width to Delete
            }

            // Set the column widths
            dataGridViewTextBoxColumn1.Width = nameWidth;
            dataGridViewTextBoxColumn2.Width = emailWidth;
            dataGridViewTextBoxColumn3.Width = phoneWidth;
            dataGridViewTextBoxColumn4.Width = folderSizeWidth;
            deleteButtonColumn.Width = deleteWidth;

            // Disable horizontal scrollbar
            clientsGridView.ScrollBars = ScrollBars.Vertical;
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            AdjustColumnWidths(); // Set initial column widths when the form loads
        }

        private void clientsGridView_SizeChanged(object sender, EventArgs e)
        {
            AdjustColumnWidths(); // Adjust column widths when the DataGridView is resized
        }
    }
}