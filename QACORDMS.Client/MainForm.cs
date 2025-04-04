using Microsoft.VisualBasic.ApplicationServices;
using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class MainForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private readonly string _userRole;
        private List<Helpers.Client> _clients = new List<Helpers.Client>();
        private List<Helpers.ClientProject> _projects = new List<Helpers.ClientProject>();
        private Helpers.Client _selectedClient = new Helpers.Client();
        private Helpers.ClientProject _selectedProject = new Helpers.ClientProject();
        private string tempFolderPath = Path.Combine(Path.GetTempPath(), "DriveTemp");
        private bool isProcessingDoubleClick = false;
        private string CurrentFolderId { get; set; }
        private Stack<string> FolderHistory { get; set; } = new Stack<string>();
        private ToolStripMenuItem addUserMenuItem;
        private Dictionary<string, System.Diagnostics.Process> openedFiles = new Dictionary<string, System.Diagnostics.Process>();
        private Button addPermissionsButton;
        private ToolStripMenuItem settingsMenuItem; // Add this field

        public MainForm(QACOAPIHelper apiHelper, string userRole = null)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            _userRole = userRole;
            InitializeComponent();

            clientsViewBox.Columns.Add("Client Name", 260);

            listView1.Columns.Add("Name", 400);
            listView1.Columns.Add("Type", 200);
            listView1.Columns.Add("Size", 150);

            LoadClientsAsync();

            if (!Directory.Exists(tempFolderPath))
                Directory.CreateDirectory(tempFolderPath);

            listView1.DoubleClick += ListView1_DoubleClick;
            listView1.ContextMenuStrip = CreateContextMenu();
            listView1.AllowDrop = true;
            listView1.DragEnter += ListView1_DragEnter;
            listView1.DragDrop += ListView1_DragDrop;

            CustomizeUIForRole();
        }

        private void CustomizeUIForRole()
        {
            addUserMenuItem = new ToolStripMenuItem("Add User");
            addUserMenuItem.Click += AddUserMenuItem_Click;
            menuStrip.Items.Add(addUserMenuItem);

            if (_userRole == "Partner")
            {
                addUserMenuItem.Visible = true;
                statusLabel.Text = "Logged in as Partner";
            }
            else
            {
                addUserMenuItem.Visible = false;
            }
            if (_userRole == "AuditManager" || _userRole == "TaxManager" || _userRole == "Partner"
                || _userRole == "CorporateManager" || _userRole == "AdvisoryManager" || _userRole == "ERPManager" || _userRole == "BookkeepingManager"
                || _userRole == "OtherManager")
            {
                addPermissionsButton.Visible = true;
            }
            else
            {
                addPermissionsButton.Visible = false;
            }
        }

        private async void LoadClientsAsync()
        {
            try
            {
                statusLabel.Text = "Loading clients...";
                _clients.Clear();
                int page = 1;
                const int pageSize = 100;
                bool hasMoreData = true;

                while (hasMoreData)
                {
                    var res = await _apiHelper.GetClientsAsync("", page, pageSize);
                    var clients = res.Clients;
                    var totalCount = res.TotalCount;
                    var totalPages = res.TotalPages;

                    _clients.AddRange(clients);
                    hasMoreData = page < totalPages;
                    page++;
                }

                clientsViewBox.Items.Clear();
                foreach (var client in _clients)
                {
                    var item = new ListViewItem(client.Name) { Tag = client };
                    clientsViewBox.Items.Add(item);
                }
                statusLabel.Text = "Clients loaded successfully.";
                if (clientsViewBox.Items.Count > 0)
                    clientsViewBox.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading clients.";
            }
        }

        private async void ClientsViewBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clientsViewBox.SelectedItems.Count == 0) return;

                var selectedItem = clientsViewBox.SelectedItems[0];
                _selectedClient = (Helpers.Client)selectedItem.Tag;
                if (_selectedClient != null)
                {
                    statusLabel.Text = $"Loading projects for {_selectedClient.Name}...";
                    _projects = await _apiHelper.GetClientProjectsAsync(_selectedClient.Id);
                    projectComboBox.Items.Clear();
                    projectComboBox.Items.AddRange(_projects.Select(p => p.ProjectName).ToArray());
                    statusLabel.Text = $"Projects loaded for {_selectedClient.Name}.";
                    if (projectComboBox.Items.Count > 0)
                        projectComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading projects.";
            }
        }

        private void ClientsViewBox_DoubleClick(object sender, EventArgs e)
        {
            ClientsViewBox_SelectedIndexChanged(sender, e);
        }

        private async void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedProjectName = projectComboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedProjectName)) return;

                _selectedProject = _projects.FirstOrDefault(p => p.ProjectName == selectedProjectName);
                if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
                {
                    statusLabel.Text = $"Loading files for {_selectedProject.ProjectName}...";
                    CurrentFolderId = _selectedProject.GoogleDriveFolderId;
                    FolderHistory.Clear();
                    await LoadFolderContents(CurrentFolderId);
                }
                else
                {
                    MessageBox.Show("Invalid project or missing Google Drive folder ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading files.";
            }
        }

        private async Task LoadFolderContents(string folderId)
        {
            try
            {
                var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(folderId);
                listView1.Items.Clear();

                foreach (var item in driveItems)
                {
                    if (!string.IsNullOrEmpty(item.ThumbnailLink) && !imageList1.Images.ContainsKey(item.MimeType ?? "Unknown"))
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                var imageData = await client.GetByteArrayAsync(item.ThumbnailLink);
                                using (var ms = new MemoryStream(imageData))
                                {
                                    var thumbnail = Image.FromStream(ms);
                                    imageList1.Images.Add(item.MimeType ?? "Unknown", thumbnail);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to load thumbnail for {item.Name}: {ex.Message}");
                        }
                    }

                    var listViewItem = new ListViewItem(item.Name)
                    {
                        ImageKey = item.MimeType ?? "Unknown",
                        ImageIndex = imageList1.Images.IndexOfKey(item.MimeType ?? "Unknown"),
                        Tag = item
                    };

                    listViewItem.SubItems.Add(item.FileExtension ?? "Folder");
                    listViewItem.SubItems.Add(item.Size + " KB");

                    listView1.Items.Add(listViewItem);
                }

                //if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
                //{
                //    foreach (ListViewItem item in listView1.Items)
                //    {
                //        item.Position = new Point(item.Position.X + 10, item.Position.Y + 20);
                //    }
                //}
                //else if (listView1.View == View.Details)
                //{
                //    foreach (ListViewItem item in listView1.Items)
                //    {
                //        item.Position = new Point(0, 0); // Reset position for details view
                //    }
                //}

                if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
                {
                    int x = 10, y = 20; // Starting position
                    foreach (ListViewItem item in listView1.Items)
                    {
                        item.Position = new Point(x, y);
                        x += 150; // Adjust spacing between icons
                        if (x > listView1.Width - 150) // Move to next row if width exceeds
                        {
                            x = 10;
                            y += 150;
                        }
                    }
                }



                statusLabel.Text = $"Loaded {driveItems.Count} items.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading folder contents.";
            }
        }

        private async void BackMenuItem_Click(object sender, EventArgs e)
        {
            if (FolderHistory.Count > 0)
            {
                try
                {
                    CurrentFolderId = FolderHistory.Pop();
                    await LoadFolderContents(CurrentFolderId);
                    statusLabel.Text = "Navigated back.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to go back: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = "Error navigating back.";
                }
            }
            else
            {
                MessageBox.Show("No previous folder to go back to.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var folderStructureForm = new FolderStructureForm(_apiHelper);
                folderStructureForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Folder Structure form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void refreshMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var clientsForm = new ClientsForm(_apiHelper);
        //        clientsForm.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private async void refreshMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var clientsForm = new ClientsForm(_apiHelper);
                clientsForm.ShowDialog();
                LoadClientsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UploadFileMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select File to Upload";
                    openFileDialog.Filter = "All Files (*.*)|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        statusLabel.Text = "Upload complete.";
                        await RefreshFileList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error uploading file.";
            }
        }

        private void AddUserMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var userForm = new UserForm(_apiHelper);
                userForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open User form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && !string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private async void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files == null || files.Length == 0) return;

                foreach (string filePath in files)
                {
                    statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
                    string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
                    statusLabel.Text = $"Uploaded: {Path.GetFileName(filePath)}";
                }

                MessageBox.Show($"{files.Length} file(s) uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to upload file(s): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error uploading file(s).";
            }
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            var createFileItem = new ToolStripMenuItem("Create File");
            createFileItem.Click += async (s, e) => await CreateFile_Click(s, e);
            contextMenu.Items.Add(createFileItem);

            var createFolderItem = new ToolStripMenuItem("Create Folder");
            createFolderItem.Click += async (s, e) => await CreateFolder_Click(s, e);
            contextMenu.Items.Add(createFolderItem);

            var deleteFileItem = new ToolStripMenuItem("Delete File");
            deleteFileItem.Click += async (s, e) => await DeleteFile_Click(s, e);
            contextMenu.Items.Add(deleteFileItem);

            return contextMenu;
        }

        private async Task CreateFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select File to Upload";
                    openFileDialog.Filter = "All Files (*.*)|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        statusLabel.Text = "Upload complete.";
                        await RefreshFileList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error uploading file.";
            }
        }

        private async Task CreateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string folderName = Prompt.ShowDialog("Enter folder name:", "New Folder");
                if (!string.IsNullOrEmpty(folderName))
                {
                    statusLabel.Text = $"Creating folder: {folderName}...";
                    string folderId = await _apiHelper.CreateFolderAsync(folderName, CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
                    statusLabel.Text = $"Folder '{folderName}' created.";
                    MessageBox.Show($"Folder '{folderName}' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error creating folder.";
            }
        }

        private async Task DeleteFile_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listView1.SelectedItems[0];
            var driveItem = selectedItem.Tag as GoogleDriveItem;
            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
            {
                MessageBox.Show("Invalid file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{selectedItem.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                statusLabel.Text = $"Deleting {selectedItem.Text}...";
                //await _apiHelper.DeleteFileAsync(driveItem.Id);
                statusLabel.Text = "File deleted successfully.";
                await RefreshFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error deleting file.";
            }
        }

        private async void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0 || isProcessingDoubleClick) return;

            isProcessingDoubleClick = true;
            var selectedItem = listView1.SelectedItems[0];
            var driveItem = selectedItem.Tag as GoogleDriveItem;

            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
            {
                isProcessingDoubleClick = false;
                return;
            }

            if (driveItem.MimeType == "application/vnd.google-apps.folder")
            {
                try
                {
                    FolderHistory.Push(CurrentFolderId);
                    CurrentFolderId = driveItem.Id;
                    await LoadFolderContents(CurrentFolderId);
                    statusLabel.Text = $"Navigated to folder: {driveItem.Name}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = "Error navigating to folder.";
                }
                finally
                {
                    isProcessingDoubleClick = false;
                }
            }
            else
            {
                string fileName = selectedItem.Text;
                string tempFilePath = Path.Combine(tempFolderPath, fileName);

                try
                {
                    if (openedFiles.TryGetValue(tempFilePath, out var existingProcess) && !existingProcess.HasExited)
                    {
                        statusLabel.Text = $"{fileName} is already open.";
                        isProcessingDoubleClick = false;
                        return;
                    }

                    if (!File.Exists(tempFilePath))
                    {
                        statusLabel.Text = $"Downloading {fileName}...";
                        using (var stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                        {
                            await _apiHelper.DownloadFileAsync(driveItem.Id, stream);
                        }
                        statusLabel.Text = $"Download completed for {fileName}.";
                    }
                    else
                    {
                        statusLabel.Text = $"Opening existing {fileName}...";
                    }

                    statusLabel.Text = $"Opening {fileName}...";
                    var processInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = tempFilePath,
                        UseShellExecute = true,
                        Verb = "open"
                    };

                    var process = System.Diagnostics.Process.Start(processInfo);
                    if (process == null)
                        throw new Exception("Failed to open the file.");

                    openedFiles[tempFilePath] = process;
                    Task.Run(() => MonitorAndReplaceFileOnClose(tempFilePath, driveItem.Id, fileName, process));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = $"Error processing {fileName}.";
                }
                finally
                {
                    isProcessingDoubleClick = false;
                }
            }
        }

        private async Task MonitorAndReplaceFileOnClose(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
        {
            try
            {
                bool fileChanged = false;
                var fileChangedTcs = new TaskCompletionSource<bool>();

                using (FileSystemWatcher watcher = new FileSystemWatcher(tempFolderPath, fileName))
                {
                    watcher.NotifyFilter = NotifyFilters.LastWrite;
                    watcher.Changed += (s, e) =>
                    {
                        fileChanged = true;
                        fileChangedTcs.TrySetResult(true);
                    };
                    watcher.EnableRaisingEvents = true;

                    statusLabel.Text = $"Monitoring changes in {fileName}...";
                    await Task.Run(() => process.WaitForExit());
                }

                if (fileChanged)
                {
                    statusLabel.Text = $"Detected changes in {fileName}, updating...";
                    await WaitForFileRelease(filePath);
                    await _apiHelper.ReplaceFileAsync(fileId, filePath);
                    statusLabel.Text = $"{fileName} updated successfully.";
                }
                else
                {
                    statusLabel.Text = $"No changes detected in {fileName}.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = $"Error updating {fileName}.";
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    await WaitForFileRelease(filePath);
                    File.Delete(filePath);
                    openedFiles.Remove(filePath);
                    statusLabel.Text = $"{fileName} processed and temp file removed.";
                }
            }
        }

        private async Task WaitForFileRelease(string filePath)
        {
            int maxRetries = 10;
            int delayMs = 1000;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        return;
                    }
                }
                catch (IOException)
                {
                    await Task.Delay(delayMs);
                }
            }

            throw new IOException($"File {filePath} remained locked after {maxRetries} attempts.");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (Directory.Exists(tempFolderPath))
            {
                try
                {
                    Directory.Delete(tempFolderPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to clean up temp folder: {ex.Message}", "Cleanup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async Task RefreshFileList()
        {
            if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
            {
                statusLabel.Text = $"Refreshing files for {_selectedProject.ProjectName}...";
                await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SessionHelper.CurrentUser = null;
            var loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        private void smallIconsButton_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            LoadFolderContents(CurrentFolderId);
        }

        private void largeIconsButton_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            LoadFolderContents(CurrentFolderId);
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void listView1_MouseClick(object sender, MouseEventArgs e) { }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) { }

        private void AddPermissionsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProject == null || _selectedProject.Id == Guid.Empty)
                {
                    MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var permissionForm = new AddPermissionForm(_apiHelper, _selectedProject.Id);
                permissionForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Permissions form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text };
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 20, Top = 50, Width = 240, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 160, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}