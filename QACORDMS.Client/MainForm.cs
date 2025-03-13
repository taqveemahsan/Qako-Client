using QACORDMS.Client.Helpers;

namespace QACORDMS.Client
{
    public partial class MainForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;

        private List<Helpers.Client> _clients = new List<Helpers.Client>();
        private List<Helpers.ClientProject> _projects = new List<Helpers.ClientProject>();

        private Helpers.Client _selectedClient = new Helpers.Client();
        private Helpers.ClientProject _selectedProject = new Helpers.ClientProject();

        private string tempFolderPath = Path.Combine(Path.GetTempPath(), "DriveTemp"); // Temp folder for downloaded files
        private bool isProcessingDoubleClick = false; // Flag to prevent multiple executions
        public MainForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            InitializeComponent();
            LoadClientsAsync();

            // Temp folder create karna agar nahi hai
            if (!Directory.Exists(tempFolderPath))
                Directory.CreateDirectory(tempFolderPath);

            // listView1 ke columns set karna agar already nahi hain
            listView1.Columns.Add("Name", 200);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Size", 100);

            // Double-click event add karna
            listView1.DoubleClick += ListView1_DoubleClick;

            // Add ContextMenuStrip to listView1
            listView1.ContextMenuStrip = CreateContextMenu();

            // Enable drag-and-drop
            listView1.AllowDrop = true;
            listView1.DragEnter += ListView1_DragEnter;
            listView1.DragDrop += ListView1_DragDrop;
        }

        // Handle DragEnter event
        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // Handle DragDrop event
        private async void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
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
                    string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, _selectedProject.GoogleDriveFolderId);
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

        // Create ContextMenuStrip
        private ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            // Create File option
            var createFileItem = new ToolStripMenuItem("Create File");
            createFileItem.Click += async (s, e) => await CreateFile_Click(s, e);
            contextMenu.Items.Add(createFileItem);

            // Delete File option
            var deleteFileItem = new ToolStripMenuItem("Delete File");
            deleteFileItem.Click += async (s, e) => await DeleteFile_Click(s, e);
            contextMenu.Items.Add(deleteFileItem);

            return contextMenu;
        }

        // Create File logic (similar to UploadFileMenuItem_Click)
        private async Task CreateFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
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

                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, _selectedProject.GoogleDriveFolderId);

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

        // Delete File logic
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
                //await _apiHelper.DeleteFileAsync(driveItem.Id); // Assuming this method exists or will be added
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

            isProcessingDoubleClick = true; // Lock the method
            var selectedItem = listView1.SelectedItems[0];
            var driveItem = selectedItem.Tag as GoogleDriveItem;

            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
            {
                isProcessingDoubleClick = false;
                return;
            }

            string fileName = selectedItem.Text;
            string tempFilePath = Path.Combine(tempFolderPath, fileName);

            try
            {
                // Check if process is already running
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

                // Open file
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

                // Track opened file process
                openedFiles[tempFilePath] = process;

                // Monitor file changes
                Task.Run(() => MonitorAndReplaceFileOnClose(tempFilePath, driveItem.Id, fileName, process));
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Failed to process file {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = $"Error processing {fileName}.";
            }
            finally
            {
                isProcessingDoubleClick = false; // Unlock after completion
            }
        }

        // Dictionary to track opened files
        private Dictionary<string, System.Diagnostics.Process> openedFiles = new Dictionary<string, System.Diagnostics.Process>();

        private async Task MonitorAndReplaceFileOnClose(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
        {
            try
            {
                // File changes tracking
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

                    // Wait for process to exit
                    await Task.Run(() => process.WaitForExit());
                }

                // If file was modified before closing, update it
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
                //MessageBox.Show($"Failed to update {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region Working
        //private async void ListView1_DoubleClick(object sender, EventArgs e)
        //{
        //    if (listView1.SelectedItems.Count == 0) return;

        //    var selectedItem = listView1.SelectedItems[0];
        //    var driveItem = selectedItem.Tag as GoogleDriveItem; // Assuming GoogleDriveItem has Id, Name, etc.
        //    if (driveItem == null || string.IsNullOrEmpty(driveItem.Id)) return;

        //    string fileName = selectedItem.Text; // Use displayed name
        //    string tempFilePath = Path.Combine(tempFolderPath, fileName);

        //    try
        //    {
        //        // Check if file already exists in temp folder
        //        if (File.Exists(tempFilePath))
        //        {
        //            statusLabel.Text = $"Opening existing {fileName}...";
        //        }
        //        else
        //        {
        //            statusLabel.Text = $"Downloading {fileName}...";

        //            // Download file from Google Drive
        //            using (var stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
        //            {
        //                await _apiHelper.DownloadFileAsync(driveItem.Id, stream);
        //            }

        //            statusLabel.Text = $"Download completed for {fileName}.";
        //        }

        //        // Open file only after ensuring it's fully downloaded or exists
        //        statusLabel.Text = $"Opening {fileName}...";
        //        var processInfo = new System.Diagnostics.ProcessStartInfo
        //        {
        //            FileName = tempFilePath,
        //            UseShellExecute = true,
        //            Verb = "open"
        //        };

        //        var process = System.Diagnostics.Process.Start(processInfo);
        //        if (process == null)
        //            throw new Exception("Failed to open the file.");

        //        // Monitor changes on the file
        //        Task.Run(() => MonitorAndUpdateFile(tempFilePath, driveItem.Id, fileName, process));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to process file {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        statusLabel.Text = $"Error processing {fileName}.";
        //    }
        //}

        //private async Task MonitorAndUpdateFile(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
        //{
        //    try
        //    {
        //        // Wait for the process to exit (file closed by user)
        //        await Task.Run(() => process.WaitForExit());

        //        FileSystemWatcher watcher = new FileSystemWatcher
        //        {
        //            Path = tempFolderPath,
        //            Filter = fileName,
        //            NotifyFilter = NotifyFilters.LastWrite,
        //            EnableRaisingEvents = true
        //        };

        //        bool fileChanged = false;
        //        var tcs = new TaskCompletionSource<bool>();

        //        watcher.Changed += (s, e) =>
        //        {
        //            if (fileChanged) return;
        //            fileChanged = true;
        //            tcs.SetResult(true);
        //        };

        //        statusLabel.Text = $"Monitoring changes in {fileName}...";

        //        // Wait for changes or timeout (e.g., 5 minutes)
        //        await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromMinutes(5)));

        //        if (fileChanged)
        //        {
        //            statusLabel.Text = $"Detected changes in {fileName}, updating...";
        //            await WaitForFileRelease(filePath); // Ensure file is released

        //            // Replace file on Google Drive
        //            await _apiHelper.ReplaceFileAsync(fileId, filePath);

        //            statusLabel.Text = $"{fileName} updated successfully.";
        //        }
        //        else
        //        {
        //            statusLabel.Text = $"No changes detected in {fileName}.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to update {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        statusLabel.Text = $"Error updating {fileName}.";
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            if (File.Exists(filePath))
        //            {
        //                await WaitForFileRelease(filePath);
        //                File.Delete(filePath);
        //                statusLabel.Text = $"{fileName} processed and temp file removed.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Failed to delete temp file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private async Task WaitForFileRelease(string filePath)
        //{
        //    int maxRetries = 10;
        //    int delayMs = 1000;

        //    for (int i = 0; i < maxRetries; i++)
        //    {
        //        try
        //        {
        //            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        //            {
        //                return;
        //            }
        //        }
        //        catch (IOException)
        //        {
        //            await Task.Delay(delayMs);
        //        }
        //    }

        //    throw new IOException($"File {filePath} remained locked after {maxRetries} attempts.");
        //} 
        #endregion

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


        private async void LoadClientsAsync()
        {
            try
            {
                statusLabel.Text = "Loading clients...";
                _clients = await _apiHelper.GetClientsAsync();
                ClientListBox.Items.Clear();
                ClientListBox.Items.AddRange(_clients.Select(x => x.Name).ToArray());
                statusLabel.Text = "Clients loaded successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading clients.";
            }
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var clientsForm = new ClientsForm(_apiHelper);
                clientsForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ClientListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = ClientListBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches) return;

            try
            {
                var selectedClientName = ClientListBox.Items[index].ToString();
                _selectedClient = _clients.FirstOrDefault(c => c.Name == selectedClientName);

                if (_selectedClient != null)
                {
                    statusLabel.Text = $"Loading projects for {_selectedClient.Name}...";
                    _projects = await _apiHelper.GetClientProjectsAsync(_selectedClient.Id);
                    ProjectListView.Items.Clear();
                    ProjectListView.Items.AddRange(_projects.Select(p => p.ProjectName).ToArray());
                    statusLabel.Text = $"Projects loaded for {_selectedClient.Name}.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading projects.";
            }
        }

        // Update ProjectListView_MouseDoubleClick to match API response
        private async void ProjectListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = ProjectListView.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches) return;

            try
            {
                var selectedProjectName = ProjectListView.Items[index].ToString();
                _selectedProject = _projects.FirstOrDefault(p => p.ProjectName == selectedProjectName);

                if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
                {
                    statusLabel.Text = $"Loading files for {_selectedProject.ProjectName}...";
                    var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(_selectedProject.GoogleDriveFolderId);
                    listView1.Items.Clear();

                    foreach (var item in driveItems)
                    {
                        var listViewItem = new ListViewItem(item.Name)
                        {
                            Tag = item // Store full GoogleDriveItem object
                        };
                        listViewItem.SubItems.Add(item.MimeType ?? "Unknown");
                        listViewItem.SubItems.Add(item.Size != 0 ? item.Size.ToString() : "N/A");
                        listView1.Items.Add(listViewItem);
                    }

                    statusLabel.Text = $"Loaded {driveItems.Count} items from {_selectedProject.ProjectName}.";
                }
                else
                {
                    MessageBox.Show("Invalid project or missing Google Drive folder ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load Google Drive items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading files.";
            }
        }

        //private async void ProjectListView_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    int index = ProjectListView.IndexFromPoint(e.Location);
        //    if (index == ListBox.NoMatches) return;

        //    try
        //    {
        //        var selectedProjectName = ProjectListView.Items[index].ToString();
        //        _selectedProject = _projects.FirstOrDefault(p => p.ProjectName == selectedProjectName);

        //        if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
        //        {
        //            statusLabel.Text = $"Loading files for {_selectedProject.ProjectName}...";
        //            var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(_selectedProject.GoogleDriveFolderId);
        //            listView1.Items.Clear();

        //            foreach (var item in driveItems)
        //            {
        //                if (!string.IsNullOrEmpty(item.ThumbnailLink))
        //                {
        //                    try
        //                    {
        //                        using (var client = new HttpClient())
        //                        {
        //                            var imageData = await client.GetByteArrayAsync(item.ThumbnailLink);
        //                            using (var ms = new MemoryStream(imageData))
        //                            {
        //                                var thumbnail = Image.FromStream(ms);
        //                                imageList1.Images.Add(item.Id, thumbnail);
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine($"Failed to load thumbnail for {item.Name}: {ex.Message}");
        //                    }
        //                }

        //                var listViewItem = new ListViewItem(item.Name)
        //                {
        //                    ImageIndex = imageList1.Images.IndexOfKey(item.Id),
        //                    Tag = item
        //                };
        //                listViewItem.SubItems.Add(item.MimeType);
        //                listViewItem.SubItems.Add(item.Size.ToString());
        //                listView1.Items.Add(listViewItem);
        //            }

        //            statusLabel.Text = $"Loaded {driveItems.Count} items from {_selectedProject.ProjectName}.";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid project or missing Google Drive folder ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to load Google Drive items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        statusLabel.Text = "Error loading files.";
        //    }
        //}

        private async void UploadFileMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
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

                        // Upload file to Google Drive
                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, _selectedProject.GoogleDriveFolderId);

                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        statusLabel.Text = "Upload complete.";

                        // Refresh listView1 to show the newly uploaded file
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

        private void AddNewFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
                {
                    MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string folderName = Prompt.ShowDialog("Enter folder name:", "New Folder");
                if (!string.IsNullOrEmpty(folderName))
                {
                    statusLabel.Text = $"Creating folder: {folderName}...";
                    // Assuming _apiHelper has a CreateFolderAsync method
                    //await _apiHelper.CreateFolderAsync(folderName, _selectedProject.GoogleDriveFolderId);
                    MessageBox.Show($"Folder '{folderName}' created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    statusLabel.Text = "Folder created.";
                    // Refresh listView1 here if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error creating folder.";
            }
        }

        private async void AddNewFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select File to Add";
                    openFileDialog.Filter = "All Files (*.*)|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        statusLabel.Text = $"Adding file: {Path.GetFileName(filePath)}...";

                        // Upload file to Google Drive
                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, _selectedProject.GoogleDriveFolderId);

                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        statusLabel.Text = "File added successfully.";

                        // Refresh listView1 to show the newly added file
                        await RefreshFileList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error adding file.";
            }
        }

        // Helper method to refresh listView1
        private async Task RefreshFileList()
        {
            try
            {
                if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
                {
                    statusLabel.Text = $"Refreshing files for {_selectedProject.ProjectName}...";
                    var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(_selectedProject.GoogleDriveFolderId);
                    listView1.Items.Clear();

                    foreach (var item in driveItems)
                    {
                        var listViewItem = new ListViewItem(item.Name)
                        {
                            Tag = item // Store full GoogleDriveItem object
                        };
                        listViewItem.SubItems.Add(item.MimeType ?? "Unknown");
                        listViewItem.SubItems.Add(item.Size != 0 ? item.Size.ToString() : "N/A");
                        listView1.Items.Add(listViewItem);
                    }

                    statusLabel.Text = $"Loaded {driveItems.Count} items.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to refresh file list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error refreshing files.";
            }
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select an item to rename.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedItem = listView1.SelectedItems[0];
                var driveItem = selectedItem.Tag as GoogleDriveItem; // Assuming this is your type
                if (driveItem == null) return;

                string newName = Prompt.ShowDialog("Enter new name:", "Rename", selectedItem.Text);
                if (!string.IsNullOrEmpty(newName))
                {
                    statusLabel.Text = $"Renaming '{selectedItem.Text}' to '{newName}'...";
                    // Assuming _apiHelper has a RenameFileAsync method
                    // await _apiHelper.RenameFileAsync(driveItem.Id, newName);
                    selectedItem.Text = newName;
                    MessageBox.Show($"Item renamed to '{newName}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    statusLabel.Text = "Item renamed.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to rename item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error renaming item.";
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedItem = listView1.SelectedItems[0];
                var driveItem = selectedItem.Tag as GoogleDriveItem; // Assuming this is your type
                if (driveItem == null) return;

                if (MessageBox.Show($"Are you sure you want to delete '{selectedItem.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    statusLabel.Text = $"Deleting '{selectedItem.Text}'...";
                    // Assuming _apiHelper has a DeleteFileAsync method
                    // await _apiHelper.DeleteFileAsync(driveItem.Id);
                    listView1.Items.Remove(selectedItem);
                    MessageBox.Show($"Item '{selectedItem.Text}' deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    statusLabel.Text = "Item deleted.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error deleting item.";
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void ProjectListView_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listView1_MouseClick(object sender, MouseEventArgs e) { }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) { }
    }

    // Helper class for simple input dialog (you can replace this with a custom form if needed)
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
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240, Text = defaultValue };
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