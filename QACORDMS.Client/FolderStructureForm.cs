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
    public partial class FolderStructureForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private MenuStrip menuStrip;
        private ToolStripMenuItem refreshMenuItem;
        private ListView folderListView;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private Button closeButton;

        public FolderStructureForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            InitializeComponent();
            LoadFolderStructureAsync();
        }

        private async void LoadFolderStructureAsync()
        {
            try
            {
                statusLabel.Text = "Loading folder structure...";
                var response = await _apiHelper.GetFolderStructureListAsync("", 1, 100); // Fetch all folders
                var folderStructures = response.FolderStructures;

                folderListView.Items.Clear();
                foreach (var folder in folderStructures)
                {
                    var item = new ListViewItem(folder.FolderName);
                    item.SubItems.Add(folder.ClientName ?? "N/A");
                    item.SubItems.Add(folder.ClientType ?? "N/A");
                    item.SubItems.Add(folder.GoogleDriveFolderId);
                    item.SubItems.Add(folder.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"));
                    folderListView.Items.Add(item);
                }

                statusLabel.Text = $"Loaded {folderStructures.Count} folders.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load folder structure: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading folder structure.";
            }
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            LoadFolderStructureAsync();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
