using Microsoft.VisualBasic.ApplicationServices;
using QACORDMS.Client.Helpers;
using System;
using System.Collections;
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
        private ToolStripMenuItem settingsMenuItem;
        private int sortColumn = -1;
        private SortOrder sortOrder = SortOrder.Ascending;

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

            WindowState = FormWindowState.Maximized;
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
                UpdateStatusLabel("Logged in as Partner");
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

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sortColumn)
            {
                sortOrder = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                sortColumn = e.Column;
                sortOrder = SortOrder.Ascending;
            }

            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
            listView1.Sort();
        }

        private async void LoadClientsAsync()
        {
            await RunWithLoader(async () =>
            {
                try
                {
                    UpdateStatusLabel("Loading clients...");
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

                    // Update UI on the UI thread
                    if (clientsViewBox.InvokeRequired)
                    {
                        clientsViewBox.Invoke(new Action(() =>
                        {
                            clientsViewBox.Items.Clear();
                            foreach (var client in _clients)
                            {
                                var item = new ListViewItem(client.Name) { Tag = client };
                                clientsViewBox.Items.Add(item);
                            }
                            if (clientsViewBox.Items.Count > 0)
                                clientsViewBox.Items[0].Selected = true;
                        }));
                    }
                    else
                    {
                        clientsViewBox.Items.Clear();
                        foreach (var client in _clients)
                        {
                            var item = new ListViewItem(client.Name) { Tag = client };
                            clientsViewBox.Items.Add(item);
                        }
                        if (clientsViewBox.Items.Count > 0)
                            clientsViewBox.Items[0].Selected = true;
                    }

                    UpdateStatusLabel("Clients loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error loading clients.");
                }
            });
        }

        private async void ClientsViewBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RunWithLoader(async () =>
            {
                try
                {
                    if (clientsViewBox.SelectedItems.Count == 0) return;

                    var selectedItem = clientsViewBox.SelectedItems[0];
                    _selectedClient = (Helpers.Client)selectedItem.Tag;
                    if (_selectedClient != null)
                    {
                        UpdateStatusLabel($"Loading projects for {_selectedClient.Name}...");
                        _projects = await _apiHelper.GetClientProjectsAsync(_selectedClient.Id);

                        // Update UI on the UI thread
                        if (projectComboBox.InvokeRequired)
                        {
                            projectComboBox.Invoke(new Action(() =>
                            {
                                projectComboBox.Items.Clear();
                                projectComboBox.Items.AddRange(_projects.Select(p => p.ProjectName).ToArray());
                                if (projectComboBox.Items.Count > 0)
                                    projectComboBox.SelectedIndex = 0;
                            }));
                        }
                        else
                        {
                            projectComboBox.Items.Clear();
                            projectComboBox.Items.AddRange(_projects.Select(p => p.ProjectName).ToArray());
                            if (projectComboBox.Items.Count > 0)
                                projectComboBox.SelectedIndex = 0;
                        }

                        UpdateStatusLabel($"Projects loaded for {_selectedClient.Name}.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error loading projects.");
                }
            });
        }

        private void ClientsViewBox_DoubleClick(object sender, EventArgs e)
        {
            ClientsViewBox_SelectedIndexChanged(sender, e);
        }

        private async void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RunWithLoader(async () =>
            {
                try
                {
                    var selectedProjectName = projectComboBox.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(selectedProjectName)) return;

                    _selectedProject = _projects.FirstOrDefault(p => p.ProjectName == selectedProjectName);
                    if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
                    {
                        UpdateStatusLabel($"Loading files for {_selectedProject.ProjectName}...");
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
                    UpdateStatusLabel("Error loading files.");
                }
            });
        }

        private async Task LoadFolderContents(string folderId)
        {
            try
            {
                var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(folderId);

                // Update UI on the UI thread
                if (listView1.InvokeRequired)
                {
                    listView1.Invoke(new Action(() =>
                    {
                        listView1.Items.Clear();

                        foreach (var item in driveItems)
                        {
                            if (!string.IsNullOrEmpty(item.ThumbnailLink) && !imageList1.Images.ContainsKey(item.MimeType ?? "Unknown"))
                            {
                                try
                                {
                                    using (var client = new HttpClient())
                                    {
                                        var imageData = client.GetByteArrayAsync(item.ThumbnailLink).Result; // Use .Result to ensure synchronous execution in UI thread
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

                        if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
                        {
                            int x = 10, y = 20;
                            foreach (ListViewItem item in listView1.Items)
                            {
                                item.Position = new Point(x, y);
                                x += 150;
                                if (x > listView1.Width - 150)
                                {
                                    x = 10;
                                    y += 150;
                                }
                            }
                        }
                    }));
                }
                else
                {
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

                    if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
                    {
                        int x = 10, y = 20;
                        foreach (ListViewItem item in listView1.Items)
                        {
                            item.Position = new Point(x, y);
                            x += 150;
                            if (x > listView1.Width - 150)
                            {
                                x = 10;
                                y += 150;
                            }
                        }
                    }
                }

                UpdateStatusLabel($"Loaded {driveItems.Count} items.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel("Error loading folder contents.");
            }
        }

        private async void BackMenuItem_Click(object sender, EventArgs e)
        {
            if (FolderHistory.Count > 0)
            {
                await RunWithLoader(async () =>
                {
                    try
                    {
                        CurrentFolderId = FolderHistory.Pop();
                        await LoadFolderContents(CurrentFolderId);
                        UpdateStatusLabel("Navigated back.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to go back: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusLabel("Error navigating back.");
                    }
                });
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

        private async void refreshMenuItem_Click(object sender, EventArgs e)
        {
            await RunWithLoader(async () =>
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
            });
        }

        private async void UploadFileMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await RunWithLoader(async () =>
            {
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select File to Upload";
                        openFileDialog.Filter = "All Files (*.*)|*.*";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog.FileName;
                            UpdateStatusLabel($"Uploading: {Path.GetFileName(filePath)}...");
                            string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
                            MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateStatusLabel("Upload complete.");
                            await RefreshFileList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error uploading file.");
                }
            });
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

        private async void ListView1_DragEnter(object sender, DragEventArgs e)
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

            await RunWithLoader(async () =>
            {
                try
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files == null || files.Length == 0) return;

                    foreach (string filePath in files)
                    {
                        UpdateStatusLabel($"Uploading: {Path.GetFileName(filePath)}...");
                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
                        UpdateStatusLabel($"Uploaded: {Path.GetFileName(filePath)}");
                    }

                    MessageBox.Show($"{files.Length} file(s) uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RefreshFileList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to upload file(s): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error uploading file(s).");
                }
            });
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip
            {
                BackColor = Color.FromArgb(173, 216, 230),
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(0, 102, 204),
                ImageScalingSize = new Size(24, 24)
            };

            var newMenu = new ToolStripMenuItem("New");
            newMenu.DropDownItems.Add("Folder", null, async (s, e) => await CreateFolder_Click(s, e));
            newMenu.DropDownItems.Add("Text Document", null, async (s, e) => await CreateNewFile("txt", "Text Document"));
            newMenu.DropDownItems.Add("Microsoft Word Document", null, async (s, e) => await CreateNewFile("docx", "Microsoft Word Document"));
            newMenu.DropDownItems.Add("Microsoft Excel Worksheet", null, async (s, e) => await CreateNewFile("xlsx", "Microsoft Excel Worksheet"));
            newMenu.DropDownItems.Add("Microsoft PowerPoint Presentation", null, async (s, e) => await CreateNewFile("pptx", "Microsoft PowerPoint Presentation"));
            newMenu.DropDownItems.Add("Bitmap Image", null, async (s, e) => await CreateNewFile("bmp", "Bitmap Image"));
            newMenu.DropDownItems.Add("WinRAR Archive", null, async (s, e) => await CreateNewFile("rar", "WinRAR Archive"));
            newMenu.DropDownItems.Add("WinRAR ZIP Archive", null, async (s, e) => await CreateNewFile("zip", "WinRAR ZIP Archive"));
            newMenu.DropDownItems.Add("Microsoft Access Database", null, async (s, e) => await CreateNewFile("accdb", "Microsoft Access Database"));
            newMenu.DropDownItems.Add("Microsoft Publisher Document", null, async (s, e) => await CreateNewFile("pub", "Microsoft Publisher Document"));

            contextMenu.Items.Add(newMenu);

            var deleteFileItem = new ToolStripMenuItem("Delete File");
            deleteFileItem.Click += async (s, e) => await DeleteFile_Click(s, e);
            contextMenu.Items.Add(deleteFileItem);

            return contextMenu;
        }

        private async Task CreateNewFile(string extension, string fileType)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await RunWithLoader(async () =>
            {
                try
                {
                    string fileName = Prompt.ShowDialog($"Enter {fileType} name:", $"New {fileType}");
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if (!fileName.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase))
                            fileName += $".{extension}";

                        string tempFilePath = Path.Combine(tempFolderPath, fileName);
                        File.WriteAllText(tempFilePath, "");

                        UpdateStatusLabel($"Uploading: {fileName}...");
                        string uploadedFileId = await _apiHelper.UploadFileAsync(tempFilePath, CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
                        MessageBox.Show($"File '{fileName}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateStatusLabel("Upload complete.");

                        if (File.Exists(tempFilePath))
                            File.Delete(tempFilePath);

                        await RefreshFileList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create and upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error creating and uploading file.");
                }
            });
        }

        private async Task CreateFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await RunWithLoader(async () =>
            {
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select File to Upload";
                        openFileDialog.Filter = "All Files (*.*)|*.*";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog.FileName;
                            UpdateStatusLabel($"Uploading: {Path.GetFileName(filePath)}...");
                            string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
                            MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateStatusLabel("Upload complete.");
                            await RefreshFileList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error uploading file.");
                }
            });
        }

        private async Task CreateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
            {
                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await RunWithLoader(async () =>
            {
                try
                {
                    string folderName = Prompt.ShowDialog("Enter folder name:", "New Folder");
                    if (!string.IsNullOrEmpty(folderName))
                    {
                        UpdateStatusLabel($"Creating folder: {folderName}...");
                        string folderId = await _apiHelper.CreateFolderAsync(folderName, CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
                        UpdateStatusLabel($"Folder '{folderName}' created.");
                        MessageBox.Show($"Folder '{folderName}' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error creating folder.");
                }
            });
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

            await RunWithLoader(async () =>
            {
                try
                {
                    UpdateStatusLabel($"Deleting {selectedItem.Text}...");
                    //await _apiHelper.DeleteFileAsync(driveItem.Id);
                    UpdateStatusLabel("File deleted successfully.");
                    await RefreshFileList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error deleting file.");
                }
            });
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
                await RunWithLoader(async () =>
                {
                    try
                    {
                        FolderHistory.Push(CurrentFolderId);
                        CurrentFolderId = driveItem.Id;
                        await LoadFolderContents(CurrentFolderId);
                        UpdateStatusLabel($"Navigated to folder: {driveItem.Name}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusLabel("Error navigating to folder.");
                    }
                    finally
                    {
                        isProcessingDoubleClick = false;
                    }
                });
            }
            else
            {
                string fileName = selectedItem.Text;
                string tempFilePath = Path.Combine(tempFolderPath, fileName);

                await RunWithLoader(async () =>
                {
                    try
                    {
                        if (openedFiles.TryGetValue(tempFilePath, out var existingProcess) && !existingProcess.HasExited)
                        {
                            UpdateStatusLabel($"{fileName} is already open.");
                            isProcessingDoubleClick = false;
                            return;
                        }

                        if (!File.Exists(tempFilePath))
                        {
                            UpdateStatusLabel($"Downloading {fileName}...");
                            using (var stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                            {
                                await _apiHelper.DownloadFileAsync(driveItem.Id, stream);
                            }
                            UpdateStatusLabel($"Download completed for {fileName}.");
                        }
                        else
                        {
                            UpdateStatusLabel($"Opening existing {fileName}...");
                        }

                        UpdateStatusLabel($"Opening {fileName}...");
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
                        UpdateStatusLabel($"Error processing {fileName}.");
                    }
                    finally
                    {
                        isProcessingDoubleClick = false;
                    }
                });
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

                    UpdateStatusLabel($"Monitoring changes in {fileName}...");
                    await Task.Run(() => process.WaitForExit());
                }

                if (fileChanged)
                {
                    await RunWithLoader(async () =>
                    {
                        UpdateStatusLabel($"Detected changes in {fileName}, updating...");
                        await WaitForFileRelease(filePath);
                        await _apiHelper.ReplaceFileAsync(fileId, filePath);
                        UpdateStatusLabel($"{fileName} updated successfully.");
                    });
                }
                else
                {
                    UpdateStatusLabel($"No changes detected in {fileName}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel($"Error updating {fileName}.");
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    await WaitForFileRelease(filePath);
                    File.Delete(filePath);
                    openedFiles.Remove(filePath);
                    UpdateStatusLabel($"{fileName} processed and temp file removed.");
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
                await RunWithLoader(async () =>
                {
                    UpdateStatusLabel($"Refreshing files for {_selectedProject.ProjectName}...");
                    await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SessionHelper.CurrentUser = null;

            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
            if (File.Exists(tokenPath))
            {
                File.Delete(tokenPath);
            }

            var loginForm = new Login(_apiHelper);

            this.Hide();
            loginForm.ShowDialog();

            if (loginForm.DialogResult == DialogResult.OK)
            {
                var newMainForm = new MainForm(_apiHelper, SessionHelper.CurrentUser.Role);
                this.Close();
                Application.Run(newMainForm);
            }
            else
            {
                this.Close();
                Application.Exit();
            }
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

        // Helper method to safely update statusLabel
        private void UpdateStatusLabel(string message)
        {
            // Use the parent StatusStrip (or ToolStrip) to check InvokeRequired
            var statusStrip = statusLabel.GetCurrentParent() as StatusStrip;
            if (statusStrip == null)
            {
                // Fallback to the form itself if the parent is not found
                statusStrip = this.Controls.OfType<StatusStrip>().FirstOrDefault();
            }

            if (statusStrip != null)
            {
                if (statusStrip.InvokeRequired)
                {
                    statusStrip.Invoke(new Action<string>(UpdateStatusLabel), message);
                }
                else
                {
                    statusLabel.Text = message;
                }
            }
            else
            {
                // Fallback to the form's Invoke if StatusStrip is not found
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(UpdateStatusLabel), message);
                }
                else
                {
                    statusLabel.Text = message;
                }
            }
        }

        // Loader Methods
        private void ShowLoader()
        {
            if (loaderPanel == null) return;

            if (loaderPanel.InvokeRequired)
            {
                loaderPanel.Invoke(new Action(ShowLoader));
            }
            else
            {
                loaderPanel.Visible = true;
                loaderPanel.BringToFront();
                Application.DoEvents(); // Ensure the UI updates immediately
            }
        }

        private void HideLoader()
        {
            if (loaderPanel == null) return;

            if (loaderPanel.InvokeRequired)
            {
                loaderPanel.Invoke(new Action(HideLoader));
            }
            else
            {
                loaderPanel.Visible = false;
                Application.DoEvents(); // Ensure the UI updates immediately
            }
        }

        private async Task RunWithLoader(Func<Task> action)
        {
            try
            {
                ShowLoader();
                await action();
            }
            finally
            {
                HideLoader();
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

    public class ListViewItemComparer : IComparer
    {
        private int column;
        private SortOrder order;

        public ListViewItemComparer(int column, SortOrder order)
        {
            this.column = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = 0;
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;

            switch (column)
            {
                case 0: // Name column
                    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
                    break;
                case 1: // Type column
                    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
                    break;
                case 2: // Size column
                    long sizeX = ParseSize(itemX.SubItems[column].Text);
                    long sizeY = ParseSize(itemY.SubItems[column].Text);
                    returnVal = sizeX.CompareTo(sizeY);
                    break;
            }

            if (order == SortOrder.Descending)
                returnVal = -returnVal;

            return returnVal;
        }

        private long ParseSize(string sizeText)
        {
            if (string.IsNullOrEmpty(sizeText))
                return 0;

            string[] parts = sizeText.Split(' ');
            if (parts.Length > 0 && long.TryParse(parts[0], out long size))
            {
                return size;
            }
            return 0;
        }
    }
}


















//using Microsoft.VisualBasic.ApplicationServices;
//using QACORDMS.Client.Helpers;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Windows.Forms;

//namespace QACORDMS.Client
//{
//    public partial class MainForm : Form
//    {
//        private readonly QACOAPIHelper _apiHelper;
//        private readonly string _userRole;
//        private List<Helpers.Client> _clients = new List<Helpers.Client>();
//        private List<Helpers.ClientProject> _projects = new List<Helpers.ClientProject>();
//        private Helpers.Client _selectedClient = new Helpers.Client();
//        private Helpers.ClientProject _selectedProject = new Helpers.ClientProject();
//        private string tempFolderPath = Path.Combine(Path.GetTempPath(), "DriveTemp");
//        private bool isProcessingDoubleClick = false;
//        private string CurrentFolderId { get; set; }
//        private Stack<string> FolderHistory { get; set; } = new Stack<string>();
//        private ToolStripMenuItem addUserMenuItem;
//        private Dictionary<string, System.Diagnostics.Process> openedFiles = new Dictionary<string, System.Diagnostics.Process>();
//        private Button addPermissionsButton;
//        private ToolStripMenuItem settingsMenuItem;
//        private int sortColumn = -1;
//        private SortOrder sortOrder = SortOrder.Ascending;

//        public MainForm(QACOAPIHelper apiHelper, string userRole = null)
//        {
//            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
//            _userRole = userRole;
//            InitializeComponent();

//            clientsViewBox.Columns.Add("Client Name", 260);
//            listView1.Columns.Add("Name", 400);
//            listView1.Columns.Add("Type", 200);
//            listView1.Columns.Add("Size", 150);

//            LoadClientsAsync();

//            if (!Directory.Exists(tempFolderPath))
//                Directory.CreateDirectory(tempFolderPath);

//            listView1.DoubleClick += ListView1_DoubleClick;
//            listView1.ContextMenuStrip = CreateContextMenu();
//            listView1.AllowDrop = true;
//            listView1.DragEnter += ListView1_DragEnter;
//            listView1.DragDrop += ListView1_DragDrop;

//            WindowState = FormWindowState.Maximized;
//            CustomizeUIForRole();
//        }

//        private void CustomizeUIForRole()
//        {
//            addUserMenuItem = new ToolStripMenuItem("Add User");
//            addUserMenuItem.Click += AddUserMenuItem_Click;
//            menuStrip.Items.Add(addUserMenuItem);

//            if (_userRole == "Partner")
//            {
//                addUserMenuItem.Visible = true;
//                statusLabel.Text = "Logged in as Partner";
//            }
//            else
//            {
//                addUserMenuItem.Visible = false;
//            }
//            if (_userRole == "AuditManager" || _userRole == "TaxManager" || _userRole == "Partner"
//                || _userRole == "CorporateManager" || _userRole == "AdvisoryManager" || _userRole == "ERPManager" || _userRole == "BookkeepingManager"
//                || _userRole == "OtherManager")
//            {
//                addPermissionsButton.Visible = true;
//            }
//            else
//            {
//                addPermissionsButton.Visible = false;
//            }
//        }

//        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
//        {
//            if (e.Column == sortColumn)
//            {
//                sortOrder = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
//            }
//            else
//            {
//                sortColumn = e.Column;
//                sortOrder = SortOrder.Ascending;
//            }

//            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
//            listView1.Sort();
//        }

//        private async void LoadClientsAsync()
//        {
//            try
//            {
//                statusLabel.Text = "Loading clients...";
//                _clients.Clear();
//                int page = 1;
//                const int pageSize = 100;
//                bool hasMoreData = true;

//                while (hasMoreData)
//                {
//                    var res = await _apiHelper.GetClientsAsync("", page, pageSize);
//                    var clients = res.Clients;
//                    var totalCount = res.TotalCount;
//                    var totalPages = res.TotalPages;

//                    _clients.AddRange(clients);
//                    hasMoreData = page < totalPages;
//                    page++;
//                }

//                clientsViewBox.Items.Clear();
//                foreach (var client in _clients)
//                {
//                    var item = new ListViewItem(client.Name) { Tag = client };
//                    clientsViewBox.Items.Add(item);
//                }
//                statusLabel.Text = "Clients loaded successfully.";
//                if (clientsViewBox.Items.Count > 0)
//                    clientsViewBox.Items[0].Selected = true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error loading clients.";
//            }
//        }

//        private async void ClientsViewBox_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                if (clientsViewBox.SelectedItems.Count == 0) return;

//                var selectedItem = clientsViewBox.SelectedItems[0];
//                _selectedClient = (Helpers.Client)selectedItem.Tag;
//                if (_selectedClient != null)
//                {
//                    statusLabel.Text = $"Loading projects for {_selectedClient.Name}...";
//                    _projects = await _apiHelper.GetClientProjectsAsync(_selectedClient.Id);
//                    projectComboBox.Items.Clear();
//                    projectComboBox.Items.AddRange(_projects.Select(p => p.ProjectName).ToArray());
//                    statusLabel.Text = $"Projects loaded for {_selectedClient.Name}.";
//                    if (projectComboBox.Items.Count > 0)
//                        projectComboBox.SelectedIndex = 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to load projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error loading projects.";
//            }
//        }

//        private void ClientsViewBox_DoubleClick(object sender, EventArgs e)
//        {
//            ClientsViewBox_SelectedIndexChanged(sender, e);
//        }

//        private async void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                var selectedProjectName = projectComboBox.SelectedItem?.ToString();
//                if (string.IsNullOrEmpty(selectedProjectName)) return;

//                _selectedProject = _projects.FirstOrDefault(p => p.ProjectName == selectedProjectName);
//                if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
//                {
//                    statusLabel.Text = $"Loading files for {_selectedProject.ProjectName}...";
//                    CurrentFolderId = _selectedProject.GoogleDriveFolderId;
//                    FolderHistory.Clear();
//                    await LoadFolderContents(CurrentFolderId);
//                }
//                else
//                {
//                    MessageBox.Show("Invalid project or missing Google Drive folder ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to load files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error loading files.";
//            }
//        }

//        private async Task LoadFolderContents(string folderId)
//        {
//            try
//            {
//                var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(folderId);
//                listView1.Items.Clear();

//                foreach (var item in driveItems)
//                {
//                    if (!string.IsNullOrEmpty(item.ThumbnailLink) && !imageList1.Images.ContainsKey(item.MimeType ?? "Unknown"))
//                    {
//                        try
//                        {
//                            using (var client = new HttpClient())
//                            {
//                                var imageData = await client.GetByteArrayAsync(item.ThumbnailLink);
//                                using (var ms = new MemoryStream(imageData))
//                                {
//                                    var thumbnail = Image.FromStream(ms);
//                                    imageList1.Images.Add(item.MimeType ?? "Unknown", thumbnail);
//                                }
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Console.WriteLine($"Failed to load thumbnail for {item.Name}: {ex.Message}");
//                        }
//                    }

//                    var listViewItem = new ListViewItem(item.Name)
//                    {
//                        ImageKey = item.MimeType ?? "Unknown",
//                        ImageIndex = imageList1.Images.IndexOfKey(item.MimeType ?? "Unknown"),
//                        Tag = item
//                    };

//                    listViewItem.SubItems.Add(item.FileExtension ?? "Folder");
//                    listViewItem.SubItems.Add(item.Size + " KB");

//                    listView1.Items.Add(listViewItem);
//                }

//                if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
//                {
//                    int x = 10, y = 20;
//                    foreach (ListViewItem item in listView1.Items)
//                    {
//                        item.Position = new Point(x, y);
//                        x += 150;
//                        if (x > listView1.Width - 150)
//                        {
//                            x = 10;
//                            y += 150;
//                        }
//                    }
//                }

//                statusLabel.Text = $"Loaded {driveItems.Count} items.";
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error loading folder contents.";
//            }
//        }

//        private async void BackMenuItem_Click(object sender, EventArgs e)
//        {
//            if (FolderHistory.Count > 0)
//            {
//                try
//                {
//                    CurrentFolderId = FolderHistory.Pop();
//                    await LoadFolderContents(CurrentFolderId);
//                    statusLabel.Text = "Navigated back.";
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Failed to go back: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    statusLabel.Text = "Error navigating back.";
//                }
//            }
//            else
//            {
//                MessageBox.Show("No previous folder to go back to.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        private void SettingsMenuItem_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var folderStructureForm = new FolderStructureForm(_apiHelper);
//                folderStructureForm.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to open Folder Structure form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private async void refreshMenuItem_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var clientsForm = new ClientsForm(_apiHelper);
//                clientsForm.ShowDialog();
//                LoadClientsAsync();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private async void UploadFileMenuItem_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
//            {
//                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                using (OpenFileDialog openFileDialog = new OpenFileDialog())
//                {
//                    openFileDialog.Title = "Select File to Upload";
//                    openFileDialog.Filter = "All Files (*.*)|*.*";

//                    if (openFileDialog.ShowDialog() == DialogResult.OK)
//                    {
//                        string filePath = openFileDialog.FileName;
//                        statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
//                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
//                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        statusLabel.Text = "Upload complete.";
//                        await RefreshFileList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error uploading file.";
//            }
//        }

//        private void AddUserMenuItem_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var userForm = new UserForm(_apiHelper);
//                userForm.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to open User form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void ListView1_DragEnter(object sender, DragEventArgs e)
//        {
//            if (e.Data.GetDataPresent(DataFormats.FileDrop) && !string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
//                e.Effect = DragDropEffects.Copy;
//            else
//                e.Effect = DragDropEffects.None;
//        }

//        private async void ListView1_DragDrop(object sender, DragEventArgs e)
//        {
//            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
//            {
//                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
//                if (files == null || files.Length == 0) return;

//                foreach (string filePath in files)
//                {
//                    statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
//                    string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
//                    statusLabel.Text = $"Uploaded: {Path.GetFileName(filePath)}";
//                }

//                MessageBox.Show($"{files.Length} file(s) uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                await RefreshFileList();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to upload file(s): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error uploading file(s).";
//            }
//        }

//        private ContextMenuStrip CreateContextMenu()
//        {
//            var contextMenu = new ContextMenuStrip
//            {
//                BackColor = Color.FromArgb(173, 216, 230),
//                Font = new Font("Segoe UI", 11F),
//                ForeColor = Color.FromArgb(0, 102, 204),
//                ImageScalingSize = new Size(24, 24)
//            };

//            // Create the "New" submenu
//            var newMenu = new ToolStripMenuItem("New");

//            // Add options to the "New" submenu (similar to Windows Explorer)
//            newMenu.DropDownItems.Add("Folder", null, async (s, e) => await CreateFolder_Click(s, e));
//            newMenu.DropDownItems.Add("Text Document", null, async (s, e) => await CreateNewFile("txt", "Text Document"));
//            newMenu.DropDownItems.Add("Microsoft Word Document", null, async (s, e) => await CreateNewFile("docx", "Microsoft Word Document"));
//            newMenu.DropDownItems.Add("Microsoft Excel Worksheet", null, async (s, e) => await CreateNewFile("xlsx", "Microsoft Excel Worksheet"));
//            newMenu.DropDownItems.Add("Microsoft PowerPoint Presentation", null, async (s, e) => await CreateNewFile("pptx", "Microsoft PowerPoint Presentation"));
//            newMenu.DropDownItems.Add("Bitmap Image", null, async (s, e) => await CreateNewFile("bmp", "Bitmap Image"));
//            newMenu.DropDownItems.Add("WinRAR Archive", null, async (s, e) => await CreateNewFile("rar", "WinRAR Archive"));
//            newMenu.DropDownItems.Add("WinRAR ZIP Archive", null, async (s, e) => await CreateNewFile("zip", "WinRAR ZIP Archive"));
//            newMenu.DropDownItems.Add("Microsoft Access Database", null, async (s, e) => await CreateNewFile("accdb", "Microsoft Access Database"));
//            newMenu.DropDownItems.Add("Microsoft Publisher Document", null, async (s, e) => await CreateNewFile("pub", "Microsoft Publisher Document"));

//            // Add the "New" submenu to the context menu
//            contextMenu.Items.Add(newMenu);

//            // Existing "Delete File" option
//            var deleteFileItem = new ToolStripMenuItem("Delete File");
//            deleteFileItem.Click += async (s, e) => await DeleteFile_Click(s, e);
//            contextMenu.Items.Add(deleteFileItem);

//            return contextMenu;
//        }

//        private async Task CreateNewFile(string extension, string fileType)
//        {
//            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
//            {
//                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                string fileName = Prompt.ShowDialog($"Enter {fileType} name:", $"New {fileType}");
//                if (!string.IsNullOrEmpty(fileName))
//                {
//                    if (!fileName.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase))
//                        fileName += $".{extension}";

//                    // Create a temporary file
//                    string tempFilePath = Path.Combine(tempFolderPath, fileName);
//                    File.WriteAllText(tempFilePath, ""); // Create an empty file

//                    // Upload to Google Drive
//                    statusLabel.Text = $"Uploading: {fileName}...";
//                    string uploadedFileId = await _apiHelper.UploadFileAsync(tempFilePath, CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
//                    MessageBox.Show($"File '{fileName}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    statusLabel.Text = "Upload complete.";

//                    // Clean up the temp file
//                    if (File.Exists(tempFilePath))
//                        File.Delete(tempFilePath);

//                    // Refresh the file list
//                    await RefreshFileList();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to create and upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error creating and uploading file.";
//            }
//        }

//        private async Task CreateFile_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
//            {
//                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                using (OpenFileDialog openFileDialog = new OpenFileDialog())
//                {
//                    openFileDialog.Title = "Select File to Upload";
//                    openFileDialog.Filter = "All Files (*.*)|*.*";

//                    if (openFileDialog.ShowDialog() == DialogResult.OK)
//                    {
//                        string filePath = openFileDialog.FileName;
//                        statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
//                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
//                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        statusLabel.Text = "Upload complete.";
//                        await RefreshFileList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error uploading file.";
//            }
//        }

//        private async Task CreateFolder_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
//            {
//                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                string folderName = Prompt.ShowDialog("Enter folder name:", "New Folder");
//                if (!string.IsNullOrEmpty(folderName))
//                {
//                    statusLabel.Text = $"Creating folder: {folderName}...";
//                    string folderId = await _apiHelper.CreateFolderAsync(folderName, CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
//                    statusLabel.Text = $"Folder '{folderName}' created.";
//                    MessageBox.Show($"Folder '{folderName}' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to create folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error creating folder.";
//            }
//        }

//        private async Task DeleteFile_Click(object sender, EventArgs e)
//        {
//            if (listView1.SelectedItems.Count == 0)
//            {
//                MessageBox.Show("Please select a file to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            var selectedItem = listView1.SelectedItems[0];
//            var driveItem = selectedItem.Tag as GoogleDriveItem;
//            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
//            {
//                MessageBox.Show("Invalid file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            var result = MessageBox.Show($"Are you sure you want to delete '{selectedItem.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//            if (result != DialogResult.Yes) return;

//            try
//            {
//                statusLabel.Text = $"Deleting {selectedItem.Text}...";
//                //await _apiHelper.DeleteFileAsync(driveItem.Id);
//                statusLabel.Text = "File deleted successfully.";
//                await RefreshFileList();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to delete file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = "Error deleting file.";
//            }
//        }

//        private async void ListView1_DoubleClick(object sender, EventArgs e)
//        {
//            if (listView1.SelectedItems.Count == 0 || isProcessingDoubleClick) return;

//            isProcessingDoubleClick = true;
//            var selectedItem = listView1.SelectedItems[0];
//            var driveItem = selectedItem.Tag as GoogleDriveItem;

//            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
//            {
//                isProcessingDoubleClick = false;
//                return;
//            }

//            if (driveItem.MimeType == "application/vnd.google-apps.folder")
//            {
//                try
//                {
//                    FolderHistory.Push(CurrentFolderId);
//                    CurrentFolderId = driveItem.Id;
//                    await LoadFolderContents(CurrentFolderId);
//                    statusLabel.Text = $"Navigated to folder: {driveItem.Name}";
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    statusLabel.Text = "Error navigating to folder.";
//                }
//                finally
//                {
//                    isProcessingDoubleClick = false;
//                }
//            }
//            else
//            {
//                string fileName = selectedItem.Text;
//                string tempFilePath = Path.Combine(tempFolderPath, fileName);

//                try
//                {
//                    if (openedFiles.TryGetValue(tempFilePath, out var existingProcess) && !existingProcess.HasExited)
//                    {
//                        statusLabel.Text = $"{fileName} is already open.";
//                        isProcessingDoubleClick = false;
//                        return;
//                    }

//                    if (!File.Exists(tempFilePath))
//                    {
//                        statusLabel.Text = $"Downloading {fileName}...";
//                        using (var stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
//                        {
//                            await _apiHelper.DownloadFileAsync(driveItem.Id, stream);
//                        }
//                        statusLabel.Text = $"Download completed for {fileName}.";
//                    }
//                    else
//                    {
//                        statusLabel.Text = $"Opening existing {fileName}...";
//                    }

//                    statusLabel.Text = $"Opening {fileName}...";
//                    var processInfo = new System.Diagnostics.ProcessStartInfo
//                    {
//                        FileName = tempFilePath,
//                        UseShellExecute = true,
//                        Verb = "open"
//                    };

//                    var process = System.Diagnostics.Process.Start(processInfo);
//                    if (process == null)
//                        throw new Exception("Failed to open the file.");

//                    openedFiles[tempFilePath] = process;
//                    Task.Run(() => MonitorAndReplaceFileOnClose(tempFilePath, driveItem.Id, fileName, process));
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error processing {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    statusLabel.Text = $"Error processing {fileName}.";
//                }
//                finally
//                {
//                    isProcessingDoubleClick = false;
//                }
//            }
//        }

//        private async Task MonitorAndReplaceFileOnClose(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
//        {
//            try
//            {
//                bool fileChanged = false;
//                var fileChangedTcs = new TaskCompletionSource<bool>();

//                using (FileSystemWatcher watcher = new FileSystemWatcher(tempFolderPath, fileName))
//                {
//                    watcher.NotifyFilter = NotifyFilters.LastWrite;
//                    watcher.Changed += (s, e) =>
//                    {
//                        fileChanged = true;
//                        fileChangedTcs.TrySetResult(true);
//                    };
//                    watcher.EnableRaisingEvents = true;

//                    statusLabel.Text = $"Monitoring changes in {fileName}...";
//                    await Task.Run(() => process.WaitForExit());
//                }

//                if (fileChanged)
//                {
//                    statusLabel.Text = $"Detected changes in {fileName}, updating...";
//                    await WaitForFileRelease(filePath);
//                    await _apiHelper.ReplaceFileAsync(fileId, filePath);
//                    statusLabel.Text = $"{fileName} updated successfully.";
//                }
//                else
//                {
//                    statusLabel.Text = $"No changes detected in {fileName}.";
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error updating {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                statusLabel.Text = $"Error updating {fileName}.";
//            }
//            finally
//            {
//                if (File.Exists(filePath))
//                {
//                    await WaitForFileRelease(filePath);
//                    File.Delete(filePath);
//                    openedFiles.Remove(filePath);
//                    statusLabel.Text = $"{fileName} processed and temp file removed.";
//                }
//            }
//        }

//        private async Task WaitForFileRelease(string filePath)
//        {
//            int maxRetries = 10;
//            int delayMs = 1000;

//            for (int i = 0; i < maxRetries; i++)
//            {
//                try
//                {
//                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
//                    {
//                        return;
//                    }
//                }
//                catch (IOException)
//                {
//                    await Task.Delay(delayMs);
//                }
//            }

//            throw new IOException($"File {filePath} remained locked after {maxRetries} attempts.");
//        }

//        protected override void OnFormClosing(FormClosingEventArgs e)
//        {
//            base.OnFormClosing(e);
//            if (Directory.Exists(tempFolderPath))
//            {
//                try
//                {
//                    Directory.Delete(tempFolderPath, true);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Failed to clean up temp folder: {ex.Message}", "Cleanup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }
//        }

//        private async Task RefreshFileList()
//        {
//            if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
//            {
//                statusLabel.Text = $"Refreshing files for {_selectedProject.ProjectName}...";
//                await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
//            }
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            SessionHelper.CurrentUser = null;

//            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
//            if (File.Exists(tokenPath))
//            {
//                File.Delete(tokenPath);
//            }

//            var loginForm = new Login(_apiHelper);

//            this.Hide();
//            loginForm.ShowDialog();

//            if (loginForm.DialogResult == DialogResult.OK)
//            {
//                var newMainForm = new MainForm(_apiHelper, SessionHelper.CurrentUser.Role);
//                this.Close();
//                Application.Run(newMainForm);
//            }
//            else
//            {
//                this.Close();
//                Application.Exit();
//            }
//        }

//        private void smallIconsButton_Click(object sender, EventArgs e)
//        {
//            listView1.View = View.SmallIcon;
//            listView1.HeaderStyle = ColumnHeaderStyle.None;
//            LoadFolderContents(CurrentFolderId);
//        }

//        private void largeIconsButton_Click(object sender, EventArgs e)
//        {
//            listView1.View = View.LargeIcon;
//            listView1.HeaderStyle = ColumnHeaderStyle.None;
//            LoadFolderContents(CurrentFolderId);
//        }

//        private void detailsButton_Click(object sender, EventArgs e)
//        {
//            listView1.View = View.Details;
//            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
//        }

//        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
//        private void listView1_MouseClick(object sender, MouseEventArgs e) { }
//        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) { }

//        private void AddPermissionsButton_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (_selectedProject == null || _selectedProject.Id == Guid.Empty)
//                {
//                    MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                var permissionForm = new AddPermissionForm(_apiHelper, _selectedProject.Id);
//                permissionForm.ShowDialog();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to open Permissions form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }

//    public static class Prompt
//    {
//        public static string ShowDialog(string text, string caption, string defaultValue = "")
//        {
//            Form prompt = new Form()
//            {
//                Width = 300,
//                Height = 150,
//                FormBorderStyle = FormBorderStyle.FixedDialog,
//                Text = caption,
//                StartPosition = FormStartPosition.CenterScreen,
//                MaximizeBox = false,
//                MinimizeBox = false
//            };
//            Label textLabel = new Label() { Left = 20, Top = 20, Text = text };
//            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 20, Top = 50, Width = 240, Text = defaultValue };
//            Button confirmation = new Button() { Text = "OK", Left = 160, Width = 100, Top = 80, DialogResult = DialogResult.OK };
//            confirmation.Click += (sender, e) => { prompt.Close(); };
//            prompt.Controls.Add(textBox);
//            prompt.Controls.Add(confirmation);
//            prompt.Controls.Add(textLabel);
//            prompt.AcceptButton = confirmation;

//            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
//        }
//    }

//    public class ListViewItemComparer : IComparer
//    {
//        private int column;
//        private SortOrder order;

//        public ListViewItemComparer(int column, SortOrder order)
//        {
//            this.column = column;
//            this.order = order;
//        }

//        public int Compare(object x, object y)
//        {
//            int returnVal = 0;
//            ListViewItem itemX = (ListViewItem)x;
//            ListViewItem itemY = (ListViewItem)y;

//            switch (column)
//            {
//                case 0: // Name column
//                    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
//                    break;
//                case 1: // Type column
//                    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
//                    break;
//                case 2: // Size column
//                    long sizeX = ParseSize(itemX.SubItems[column].Text);
//                    long sizeY = ParseSize(itemY.SubItems[column].Text);
//                    returnVal = sizeX.CompareTo(sizeY);
//                    break;
//            }

//            if (order == SortOrder.Descending)
//                returnVal = -returnVal;

//            return returnVal;
//        }

//        private long ParseSize(string sizeText)
//        {
//            if (string.IsNullOrEmpty(sizeText))
//                return 0;

//            string[] parts = sizeText.Split(' ');
//            if (parts.Length > 0 && long.TryParse(parts[0], out long size))
//            {
//                return size;
//            }
//            return 0;
//        }
//    }
//}






















////using Microsoft.VisualBasic.ApplicationServices;
////using QACORDMS.Client.Helpers;
////using System;
////using System.Collections;
////using System.Collections.Generic;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Net.Http;
////using System.Windows.Forms;

////namespace QACORDMS.Client
////{
////    public partial class MainForm : Form
////    {
////        private readonly QACOAPIHelper _apiHelper;
////        private readonly string _userRole;
////        private List<Helpers.Client> _clients = new List<Helpers.Client>();
////        private List<Helpers.ClientProject> _projects = new List<Helpers.ClientProject>();
////        private Helpers.Client _selectedClient = new Helpers.Client();
////        private Helpers.ClientProject _selectedProject = new Helpers.ClientProject();
////        private string tempFolderPath = Path.Combine(Path.GetTempPath(), "DriveTemp");
////        private bool isProcessingDoubleClick = false;
////        private string CurrentFolderId { get; set; }
////        private Stack<string> FolderHistory { get; set; } = new Stack<string>();
////        private ToolStripMenuItem addUserMenuItem;
////        private Dictionary<string, System.Diagnostics.Process> openedFiles = new Dictionary<string, System.Diagnostics.Process>();
////        private Button addPermissionsButton;
////        private ToolStripMenuItem settingsMenuItem; // Add this field

////        private int sortColumn = -1; // -1 means no column is sorted initially
////        private SortOrder sortOrder = SortOrder.Ascending; // Default sort order

////        public MainForm(QACOAPIHelper apiHelper, string userRole = null)
////        {
////            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
////            _userRole = userRole;
////            InitializeComponent();

////            clientsViewBox.Columns.Add("Client Name", 260);

////            listView1.Columns.Add("Name", 400);
////            listView1.Columns.Add("Type", 200);
////            listView1.Columns.Add("Size", 150);

////            LoadClientsAsync();

////            if (!Directory.Exists(tempFolderPath))
////                Directory.CreateDirectory(tempFolderPath);

////            listView1.DoubleClick += ListView1_DoubleClick;
////            listView1.ContextMenuStrip = CreateContextMenu();
////            listView1.AllowDrop = true;
////            listView1.DragEnter += ListView1_DragEnter;
////            listView1.DragDrop += ListView1_DragDrop;

////            WindowState = FormWindowState.Maximized; // Set form to maximized state

////            CustomizeUIForRole();
////        }

////        private void CustomizeUIForRole()
////        {
////            addUserMenuItem = new ToolStripMenuItem("Add User");
////            addUserMenuItem.Click += AddUserMenuItem_Click;
////            menuStrip.Items.Add(addUserMenuItem);

////            if (_userRole == "Partner")
////            {
////                addUserMenuItem.Visible = true;
////                statusLabel.Text = "Logged in as Partner";
////            }
////            else
////            {
////                addUserMenuItem.Visible = false;
////            }
////            if (_userRole == "AuditManager" || _userRole == "TaxManager" || _userRole == "Partner"
////                || _userRole == "CorporateManager" || _userRole == "AdvisoryManager" || _userRole == "ERPManager" || _userRole == "BookkeepingManager"
////                || _userRole == "OtherManager")
////            {
////                addPermissionsButton.Visible = true;
////            }
////            else
////            {
////                addPermissionsButton.Visible = false;
////            }
////        }

////        // Event handler for column click (sorting)
////        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
////        {
////            // If the same column is clicked, toggle the sort order
////            if (e.Column == sortColumn)
////            {
////                sortOrder = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
////            }
////            else
////            {
////                // If a different column is clicked, reset to ascending
////                sortColumn = e.Column;
////                sortOrder = SortOrder.Ascending;
////            }

////            // Set the ListViewItemSorter property to a new instance of ListViewItemComparer
////            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);

////            // Call Sort to apply the sorting
////            listView1.Sort();
////        }

////        private async void LoadClientsAsync()
////        {
////            try
////            {
////                statusLabel.Text = "Loading clients...";
////                _clients.Clear();
////                int page = 1;
////                const int pageSize = 100;
////                bool hasMoreData = true;

////                while (hasMoreData)
////                {
////                    var res = await _apiHelper.GetClientsAsync("", page, pageSize);
////                    var clients = res.Clients;
////                    var totalCount = res.TotalCount;
////                    var totalPages = res.TotalPages;

////                    _clients.AddRange(clients);
////                    hasMoreData = page < totalPages;
////                    page++;
////                }

////                clientsViewBox.Items.Clear();
////                foreach (var client in _clients)
////                {
////                    var item = new ListViewItem(client.Name) { Tag = client };
////                    clientsViewBox.Items.Add(item);
////                }
////                statusLabel.Text = "Clients loaded successfully.";
////                if (clientsViewBox.Items.Count > 0)
////                    clientsViewBox.Items[0].Selected = true;
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error loading clients.";
////            }
////        }

////        private async void ClientsViewBox_SelectedIndexChanged(object sender, EventArgs e)
////        {
////            try
////            {
////                if (clientsViewBox.SelectedItems.Count == 0) return;

////                var selectedItem = clientsViewBox.SelectedItems[0];
////                _selectedClient = (Helpers.Client)selectedItem.Tag;
////                if (_selectedClient != null)
////                {
////                    statusLabel.Text = $"Loading projects for {_selectedClient.Name}...";
////                    _projects = await _apiHelper.GetClientProjectsAsync(_selectedClient.Id);
////                    projectComboBox.Items.Clear();
////                    projectComboBox.Items.AddRange(_projects.Select(p => p.ProjectName).ToArray());
////                    statusLabel.Text = $"Projects loaded for {_selectedClient.Name}.";
////                    if (projectComboBox.Items.Count > 0)
////                        projectComboBox.SelectedIndex = 0;
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to load projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error loading projects.";
////            }
////        }

////        private void ClientsViewBox_DoubleClick(object sender, EventArgs e)
////        {
////            ClientsViewBox_SelectedIndexChanged(sender, e);
////        }

////        private async void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
////        {
////            try
////            {
////                var selectedProjectName = projectComboBox.SelectedItem?.ToString();
////                if (string.IsNullOrEmpty(selectedProjectName)) return;

////                _selectedProject = _projects.FirstOrDefault(p => p.ProjectName == selectedProjectName);
////                if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
////                {
////                    statusLabel.Text = $"Loading files for {_selectedProject.ProjectName}...";
////                    CurrentFolderId = _selectedProject.GoogleDriveFolderId;
////                    FolderHistory.Clear();
////                    await LoadFolderContents(CurrentFolderId);
////                }
////                else
////                {
////                    MessageBox.Show("Invalid project or missing Google Drive folder ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to load files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error loading files.";
////            }
////        }

////        private async Task LoadFolderContents(string folderId)
////        {
////            try
////            {
////                var driveItems = await _apiHelper.GetGoogleDriveFilesAsync(folderId);
////                listView1.Items.Clear();

////                foreach (var item in driveItems)
////                {
////                    if (!string.IsNullOrEmpty(item.ThumbnailLink) && !imageList1.Images.ContainsKey(item.MimeType ?? "Unknown"))
////                    {
////                        try
////                        {
////                            using (var client = new HttpClient())
////                            {
////                                var imageData = await client.GetByteArrayAsync(item.ThumbnailLink);
////                                using (var ms = new MemoryStream(imageData))
////                                {
////                                    var thumbnail = Image.FromStream(ms);
////                                    imageList1.Images.Add(item.MimeType ?? "Unknown", thumbnail);
////                                }
////                            }
////                        }
////                        catch (Exception ex)
////                        {
////                            Console.WriteLine($"Failed to load thumbnail for {item.Name}: {ex.Message}");
////                        }
////                    }

////                    var listViewItem = new ListViewItem(item.Name)
////                    {
////                        ImageKey = item.MimeType ?? "Unknown",
////                        ImageIndex = imageList1.Images.IndexOfKey(item.MimeType ?? "Unknown"),
////                        Tag = item
////                    };

////                    listViewItem.SubItems.Add(item.FileExtension ?? "Folder");
////                    listViewItem.SubItems.Add(item.Size + " KB");

////                    listView1.Items.Add(listViewItem);
////                }

////                //if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
////                //{
////                //    foreach (ListViewItem item in listView1.Items)
////                //    {
////                //        item.Position = new Point(item.Position.X + 10, item.Position.Y + 20);
////                //    }
////                //}
////                //else if (listView1.View == View.Details)
////                //{
////                //    foreach (ListViewItem item in listView1.Items)
////                //    {
////                //        item.Position = new Point(0, 0); // Reset position for details view
////                //    }
////                //}

////                if (listView1.View == View.LargeIcon || listView1.View == View.SmallIcon)
////                {
////                    int x = 10, y = 20; // Starting position
////                    foreach (ListViewItem item in listView1.Items)
////                    {
////                        item.Position = new Point(x, y);
////                        x += 150; // Adjust spacing between icons
////                        if (x > listView1.Width - 150) // Move to next row if width exceeds
////                        {
////                            x = 10;
////                            y += 150;
////                        }
////                    }
////                }



////                statusLabel.Text = $"Loaded {driveItems.Count} items.";
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error loading folder contents.";
////            }
////        }

////        private async void BackMenuItem_Click(object sender, EventArgs e)
////        {
////            if (FolderHistory.Count > 0)
////            {
////                try
////                {
////                    CurrentFolderId = FolderHistory.Pop();
////                    await LoadFolderContents(CurrentFolderId);
////                    statusLabel.Text = "Navigated back.";
////                }
////                catch (Exception ex)
////                {
////                    MessageBox.Show($"Failed to go back: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                    statusLabel.Text = "Error navigating back.";
////                }
////            }
////            else
////            {
////                MessageBox.Show("No previous folder to go back to.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
////            }
////        }
////        private void SettingsMenuItem_Click(object sender, EventArgs e)
////        {
////            try
////            {
////                var folderStructureForm = new FolderStructureForm(_apiHelper);
////                folderStructureForm.ShowDialog();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open Folder Structure form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        //private void refreshMenuItem_Click(object sender, EventArgs e)
////        //{
////        //    try
////        //    {
////        //        var clientsForm = new ClientsForm(_apiHelper);
////        //        clientsForm.Show();
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////        //    }
////        //}

////        private async void refreshMenuItem_Click(object sender, EventArgs e)
////        {
////            try
////            {
////                var clientsForm = new ClientsForm(_apiHelper);
////                clientsForm.ShowDialog();
////                LoadClientsAsync();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        private async void UploadFileMenuItem_Click(object sender, EventArgs e)
////        {
////            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
////            {
////                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }

////            try
////            {
////                using (OpenFileDialog openFileDialog = new OpenFileDialog())
////                {
////                    openFileDialog.Title = "Select File to Upload";
////                    openFileDialog.Filter = "All Files (*.*)|*.*";

////                    if (openFileDialog.ShowDialog() == DialogResult.OK)
////                    {
////                        string filePath = openFileDialog.FileName;
////                        statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
////                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
////                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                        statusLabel.Text = "Upload complete.";
////                        await RefreshFileList();
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error uploading file.";
////            }
////        }

////        private void AddUserMenuItem_Click(object sender, EventArgs e)
////        {
////            try
////            {
////                var userForm = new UserForm(_apiHelper);
////                userForm.ShowDialog();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open User form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        private void ListView1_DragEnter(object sender, DragEventArgs e)
////        {
////            if (e.Data.GetDataPresent(DataFormats.FileDrop) && !string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
////                e.Effect = DragDropEffects.Copy;
////            else
////                e.Effect = DragDropEffects.None;
////        }

////        private async void ListView1_DragDrop(object sender, DragEventArgs e)
////        {
////            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
////            {
////                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }

////            try
////            {
////                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
////                if (files == null || files.Length == 0) return;

////                foreach (string filePath in files)
////                {
////                    statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
////                    string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
////                    statusLabel.Text = $"Uploaded: {Path.GetFileName(filePath)}";
////                }

////                MessageBox.Show($"{files.Length} file(s) uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                await RefreshFileList();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to upload file(s): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error uploading file(s).";
////            }
////        }

////        private ContextMenuStrip CreateContextMenu()
////        {
////            var contextMenu = new ContextMenuStrip();
////            var createFileItem = new ToolStripMenuItem("Create File");
////            createFileItem.Click += async (s, e) => await CreateFile_Click(s, e);
////            contextMenu.Items.Add(createFileItem);

////            var createFolderItem = new ToolStripMenuItem("Create Folder");
////            createFolderItem.Click += async (s, e) => await CreateFolder_Click(s, e);
////            contextMenu.Items.Add(createFolderItem);

////            var deleteFileItem = new ToolStripMenuItem("Delete File");
////            deleteFileItem.Click += async (s, e) => await DeleteFile_Click(s, e);
////            contextMenu.Items.Add(deleteFileItem);

////            return contextMenu;
////        }

////        private async Task CreateFile_Click(object sender, EventArgs e)
////        {
////            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
////            {
////                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }

////            try
////            {
////                using (OpenFileDialog openFileDialog = new OpenFileDialog())
////                {
////                    openFileDialog.Title = "Select File to Upload";
////                    openFileDialog.Filter = "All Files (*.*)|*.*";

////                    if (openFileDialog.ShowDialog() == DialogResult.OK)
////                    {
////                        string filePath = openFileDialog.FileName;
////                        statusLabel.Text = $"Uploading: {Path.GetFileName(filePath)}...";
////                        string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, CurrentFolderId);
////                        MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                        statusLabel.Text = "Upload complete.";
////                        await RefreshFileList();
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to upload file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error uploading file.";
////            }
////        }

////        private async Task CreateFolder_Click(object sender, EventArgs e)
////        {
////            if (string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId))
////            {
////                MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }

////            try
////            {
////                string folderName = Prompt.ShowDialog("Enter folder name:", "New Folder");
////                if (!string.IsNullOrEmpty(folderName))
////                {
////                    statusLabel.Text = $"Creating folder: {folderName}...";
////                    string folderId = await _apiHelper.CreateFolderAsync(folderName, CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
////                    statusLabel.Text = $"Folder '{folderName}' created.";
////                    MessageBox.Show($"Folder '{folderName}' created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                    await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to create folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error creating folder.";
////            }
////        }

////        private async Task DeleteFile_Click(object sender, EventArgs e)
////        {
////            if (listView1.SelectedItems.Count == 0)
////            {
////                MessageBox.Show("Please select a file to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }

////            var selectedItem = listView1.SelectedItems[0];
////            var driveItem = selectedItem.Tag as GoogleDriveItem;
////            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
////            {
////                MessageBox.Show("Invalid file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return;
////            }

////            var result = MessageBox.Show($"Are you sure you want to delete '{selectedItem.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
////            if (result != DialogResult.Yes) return;

////            try
////            {
////                statusLabel.Text = $"Deleting {selectedItem.Text}...";
////                //await _apiHelper.DeleteFileAsync(driveItem.Id);
////                statusLabel.Text = "File deleted successfully.";
////                await RefreshFileList();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to delete file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = "Error deleting file.";
////            }
////        }

////        private async void ListView1_DoubleClick(object sender, EventArgs e)
////        {
////            if (listView1.SelectedItems.Count == 0 || isProcessingDoubleClick) return;

////            isProcessingDoubleClick = true;
////            var selectedItem = listView1.SelectedItems[0];
////            var driveItem = selectedItem.Tag as GoogleDriveItem;

////            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
////            {
////                isProcessingDoubleClick = false;
////                return;
////            }

////            if (driveItem.MimeType == "application/vnd.google-apps.folder")
////            {
////                try
////                {
////                    FolderHistory.Push(CurrentFolderId);
////                    CurrentFolderId = driveItem.Id;
////                    await LoadFolderContents(CurrentFolderId);
////                    statusLabel.Text = $"Navigated to folder: {driveItem.Name}";
////                }
////                catch (Exception ex)
////                {
////                    MessageBox.Show($"Failed to load folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                    statusLabel.Text = "Error navigating to folder.";
////                }
////                finally
////                {
////                    isProcessingDoubleClick = false;
////                }
////            }
////            else
////            {
////                string fileName = selectedItem.Text;
////                string tempFilePath = Path.Combine(tempFolderPath, fileName);

////                try
////                {
////                    if (openedFiles.TryGetValue(tempFilePath, out var existingProcess) && !existingProcess.HasExited)
////                    {
////                        statusLabel.Text = $"{fileName} is already open.";
////                        isProcessingDoubleClick = false;
////                        return;
////                    }

////                    if (!File.Exists(tempFilePath))
////                    {
////                        statusLabel.Text = $"Downloading {fileName}...";
////                        using (var stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
////                        {
////                            await _apiHelper.DownloadFileAsync(driveItem.Id, stream);
////                        }
////                        statusLabel.Text = $"Download completed for {fileName}.";
////                    }
////                    else
////                    {
////                        statusLabel.Text = $"Opening existing {fileName}...";
////                    }

////                    statusLabel.Text = $"Opening {fileName}...";
////                    var processInfo = new System.Diagnostics.ProcessStartInfo
////                    {
////                        FileName = tempFilePath,
////                        UseShellExecute = true,
////                        Verb = "open"
////                    };

////                    var process = System.Diagnostics.Process.Start(processInfo);
////                    if (process == null)
////                        throw new Exception("Failed to open the file.");

////                    openedFiles[tempFilePath] = process;
////                    Task.Run(() => MonitorAndReplaceFileOnClose(tempFilePath, driveItem.Id, fileName, process));
////                }
////                catch (Exception ex)
////                {
////                    MessageBox.Show($"Error processing {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                    statusLabel.Text = $"Error processing {fileName}.";
////                }
////                finally
////                {
////                    isProcessingDoubleClick = false;
////                }
////            }
////        }

////        private async Task MonitorAndReplaceFileOnClose(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
////        {
////            try
////            {
////                bool fileChanged = false;
////                var fileChangedTcs = new TaskCompletionSource<bool>();

////                using (FileSystemWatcher watcher = new FileSystemWatcher(tempFolderPath, fileName))
////                {
////                    watcher.NotifyFilter = NotifyFilters.LastWrite;
////                    watcher.Changed += (s, e) =>
////                    {
////                        fileChanged = true;
////                        fileChangedTcs.TrySetResult(true);
////                    };
////                    watcher.EnableRaisingEvents = true;

////                    statusLabel.Text = $"Monitoring changes in {fileName}...";
////                    await Task.Run(() => process.WaitForExit());
////                }

////                if (fileChanged)
////                {
////                    statusLabel.Text = $"Detected changes in {fileName}, updating...";
////                    await WaitForFileRelease(filePath);
////                    await _apiHelper.ReplaceFileAsync(fileId, filePath);
////                    statusLabel.Text = $"{fileName} updated successfully.";
////                }
////                else
////                {
////                    statusLabel.Text = $"No changes detected in {fileName}.";
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Error updating {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                statusLabel.Text = $"Error updating {fileName}.";
////            }
////            finally
////            {
////                if (File.Exists(filePath))
////                {
////                    await WaitForFileRelease(filePath);
////                    File.Delete(filePath);
////                    openedFiles.Remove(filePath);
////                    statusLabel.Text = $"{fileName} processed and temp file removed.";
////                }
////            }
////        }

////        private async Task WaitForFileRelease(string filePath)
////        {
////            int maxRetries = 10;
////            int delayMs = 1000;

////            for (int i = 0; i < maxRetries; i++)
////            {
////                try
////                {
////                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
////                    {
////                        return;
////                    }
////                }
////                catch (IOException)
////                {
////                    await Task.Delay(delayMs);
////                }
////            }

////            throw new IOException($"File {filePath} remained locked after {maxRetries} attempts.");
////        }

////        protected override void OnFormClosing(FormClosingEventArgs e)
////        {
////            base.OnFormClosing(e);
////            if (Directory.Exists(tempFolderPath))
////            {
////                try
////                {
////                    Directory.Delete(tempFolderPath, true);
////                }
////                catch (Exception ex)
////                {
////                    MessageBox.Show($"Failed to clean up temp folder: {ex.Message}", "Cleanup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                }
////            }
////        }

////        private async Task RefreshFileList()
////        {
////            if (_selectedProject != null && !string.IsNullOrEmpty(_selectedProject.GoogleDriveFolderId))
////            {
////                statusLabel.Text = $"Refreshing files for {_selectedProject.ProjectName}...";
////                await LoadFolderContents(CurrentFolderId ?? _selectedProject.GoogleDriveFolderId);
////            }
////        }

////        private void button1_Click(object sender, EventArgs e)
////        {
////            SessionHelper.CurrentUser = null;

////            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
////            if (File.Exists(tokenPath))
////            {
////                File.Delete(tokenPath);
////            }

////            var loginForm = new Login(_apiHelper);

////            this.Hide();
////            loginForm.ShowDialog();

////            if (loginForm.DialogResult == DialogResult.OK)
////            {
////                var newMainForm = new MainForm(_apiHelper, SessionHelper.CurrentUser.Role);
////                this.Close();
////                Application.Run(newMainForm);
////            }
////            else
////            {
////                this.Close();
////                Application.Exit();
////            }
////        }

////        //private void button1_Click(object sender, EventArgs e)
////        //{
////        //    SessionHelper.CurrentUser = null;
////        //    var loginForm = new Login(_apiHelper);
////        //    this.Hide();
////        //    loginForm.Show();
////        //    this.Close();
////        //}

////        private void smallIconsButton_Click(object sender, EventArgs e)
////        {
////            listView1.View = View.SmallIcon;
////            listView1.HeaderStyle = ColumnHeaderStyle.None;
////            LoadFolderContents(CurrentFolderId);
////        }

////        private void largeIconsButton_Click(object sender, EventArgs e)
////        {
////            listView1.View = View.LargeIcon;
////            listView1.HeaderStyle = ColumnHeaderStyle.None;
////            LoadFolderContents(CurrentFolderId);
////        }

////        private void detailsButton_Click(object sender, EventArgs e)
////        {
////            listView1.View = View.Details;
////            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
////        }

////        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
////        private void listView1_MouseClick(object sender, MouseEventArgs e) { }
////        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) { }

////        private void AddPermissionsButton_Click(object sender, EventArgs e)
////        {
////            try
////            {
////                if (_selectedProject == null || _selectedProject.Id == Guid.Empty)
////                {
////                    MessageBox.Show("Please select a project first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                    return;
////                }

////                var permissionForm = new AddPermissionForm(_apiHelper, _selectedProject.Id);
////                permissionForm.ShowDialog();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open Permissions form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }
////    }

////    public static class Prompt
////    {
////        public static string ShowDialog(string text, string caption, string defaultValue = "")
////        {
////            Form prompt = new Form()
////            {
////                Width = 300,
////                Height = 150,
////                FormBorderStyle = FormBorderStyle.FixedDialog,
////                Text = caption,
////                StartPosition = FormStartPosition.CenterScreen,
////                MaximizeBox = false,
////                MinimizeBox = false
////            };
////            Label textLabel = new Label() { Left = 20, Top = 20, Text = text };
////            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 20, Top = 50, Width = 240, Text = defaultValue };
////            Button confirmation = new Button() { Text = "OK", Left = 160, Width = 100, Top = 80, DialogResult = DialogResult.OK };
////            confirmation.Click += (sender, e) => { prompt.Close(); };
////            prompt.Controls.Add(textBox);
////            prompt.Controls.Add(confirmation);
////            prompt.Controls.Add(textLabel);
////            prompt.AcceptButton = confirmation;

////            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
////        }
////    }

////    // Custom comparer class for sorting ListView items
////    public class ListViewItemComparer : IComparer
////    {
////        private int column;
////        private SortOrder order;

////        public ListViewItemComparer(int column, SortOrder order)
////        {
////            this.column = column;
////            this.order = order;
////        }

////        public int Compare(object x, object y)
////        {
////            int returnVal = 0;
////            ListViewItem itemX = (ListViewItem)x;
////            ListViewItem itemY = (ListViewItem)y;

////            // Compare based on the column
////            switch (column)
////            {
////                case 0: // Name column
////                    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
////                    break;
////                case 1: // Type column
////                    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
////                    break;
////                case 2: // Size column
////                    // Parse size (e.g., "128996 KB" to 128996)
////                    long sizeX = ParseSize(itemX.SubItems[column].Text);
////                    long sizeY = ParseSize(itemY.SubItems[column].Text);
////                    returnVal = sizeX.CompareTo(sizeY);
////                    break;
////            }

////            // If descending order, reverse the result
////            if (order == SortOrder.Descending)
////                returnVal = -returnVal;

////            return returnVal;
////        }

////        // Helper method to parse size strings (e.g., "128996 KB" to 128996)
////        private long ParseSize(string sizeText)
////        {
////            if (string.IsNullOrEmpty(sizeText))
////                return 0;

////            // Split the size string (e.g., "128996 KB" -> "128996")
////            string[] parts = sizeText.Split(' ');
////            if (parts.Length > 0 && long.TryParse(parts[0], out long size))
////            {
////                return size;
////            }
////            return 0;
////        }
////    }
////}