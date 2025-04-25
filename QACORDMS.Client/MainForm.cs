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
        private List<Helpers.Client> _filteredClients = new List<Helpers.Client>(); // To store filtered clients for search
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
        private int _rotationAngle = 0;
        private System.Windows.Forms.Timer _animationTimer;

        public MainForm(QACOAPIHelper apiHelper, string userRole = null)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            _userRole = userRole;
            InitializeComponent();

            // Initialize animation timer for custom loader
            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 50;
            _animationTimer.Tick += (s, e) =>
            {
                _rotationAngle = (_rotationAngle + 10) % 360;
                loaderPictureBox.Invalidate();
            };

            // Set Paint event for custom drawing
            loaderPictureBox.Paint += LoaderPictureBox_Paint;

            //listView1.Columns.Add("Name", 400);
            //listView1.Columns.Add("Type", 200);
            //listView1.Columns.Add("Size", 150);

            LoadClientsAsync();

            if (!Directory.Exists(tempFolderPath))
                Directory.CreateDirectory(tempFolderPath);

            listView1.DoubleClick += ListView1_DoubleClick;
            listView1.ContextMenuStrip = CreateContextMenu();
            listView1.AllowDrop = true;
            listView1.DragEnter += ListView1_DragEnter;
            listView1.DragDrop += ListView1_DragDrop;

            // Add context menu to clientsViewBox
            clientsViewBox.ContextMenuStrip = CreateClientsContextMenu();

            // Add KeyDown event for searchTextBox to handle Enter key
            searchTextBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Prevent the "ding" sound on Enter
                    SearchButton_Click(sender, e); // Trigger the search button click event
                }
            };

            WindowState = FormWindowState.Maximized;
            CustomizeUIForRole();
        }

        private void LoaderPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int size = Math.Min(loaderPictureBox.Width, loaderPictureBox.Height) - 10;
            int x = (loaderPictureBox.Width - size) / 2;
            int y = (loaderPictureBox.Height - size) / 2;
            int thickness = 8;

            using (Pen bgPen = new Pen(Color.FromArgb(100, 255, 255, 255), thickness))
            {
                g.DrawArc(bgPen, x, y, size, size, 0, 360);
            }

            using (Pen arcPen = new Pen(Color.White, thickness))
            {
                g.DrawArc(arcPen, x, y, size, size, _rotationAngle, 90);
            }
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

                    // Initialize filtered clients with all clients
                    _filteredClients.Clear();
                    _filteredClients.AddRange(_clients);

                    UpdateClientsViewBox(_filteredClients);
                    UpdateStatusLabel("Clients loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error loading clients.");
                }
            });
        }

        private void UpdateClientsViewBox(List<Helpers.Client> clientsToDisplay)
        {
            if (clientsViewBox.InvokeRequired)
            {
                clientsViewBox.Invoke(new Action<List<Helpers.Client>>(UpdateClientsViewBox), clientsToDisplay);
            }
            else
            {
                clientsViewBox.Nodes.Clear();

                var groupedClients = clientsToDisplay
                    .GroupBy(c => c.CompanyType)
                    .OrderBy(g => g.Key);

                foreach (var group in groupedClients)
                {
                    string companyTypeName = Enum.GetName(typeof(CompanyType), group.Key);
                    var companyNode = new TreeNode(companyTypeName)
                    {
                        Tag = group.Key
                    };

                    foreach (var client in group.OrderBy(c => c.Name))
                    {
                        var clientNode = new TreeNode(client.Name)
                        {
                            Tag = client
                        };
                        companyNode.Nodes.Add(clientNode);
                    }

                    clientsViewBox.Nodes.Add(companyNode);
                }

                if (clientsViewBox.Nodes.Count > 0)
                {
                    clientsViewBox.Nodes[0].Expand();
                    if (clientsViewBox.Nodes[0].Nodes.Count > 0)
                    {
                        clientsViewBox.SelectedNode = clientsViewBox.Nodes[0].Nodes[0];
                    }
                }
            }
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                // If search query is empty, show all clients
                _filteredClients.Clear();
                _filteredClients.AddRange(_clients);
                UpdateClientsViewBox(_filteredClients);
                UpdateStatusLabel("Showing all clients.");
                return;
            }

            await RunWithLoader(async () =>
            {
                try
                {
                    UpdateStatusLabel($"Searching for: {searchQuery}...");

                    // Filter clients based on the search query
                    _filteredClients = _clients
                        .Where(c => c.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    UpdateClientsViewBox(_filteredClients);

                    if (_filteredClients.Count == 0)
                    {
                        MessageBox.Show($"No clients found matching '{searchQuery}'.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateStatusLabel("No clients found.");
                    }
                    else
                    {
                        UpdateStatusLabel($"Found {_filteredClients.Count} client(s) matching '{searchQuery}'.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error during search.");
                }
            });
        }

        private async void ClientsViewBox_AfterSelect(object sender, TreeViewEventArgs e)
        {
            await RunWithLoader(async () =>
            {
                try
                {
                    // Only proceed if a client node (not a group node) is selected
                    if (e.Node.Tag is Helpers.Client selectedClient)
                    {
                        _selectedClient = selectedClient;
                        UpdateStatusLabel($"Loading projects for {_selectedClient.Name}...");
                        _projects = await _apiHelper.GetClientProjectsAsync(_selectedClient.Id);

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
            if (clientsViewBox.SelectedNode != null)
            {
                ClientsViewBox_AfterSelect(sender, new TreeViewEventArgs(clientsViewBox.SelectedNode));
            }
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
                                        var imageData = client.GetByteArrayAsync(item.ThumbnailLink).Result;
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
                            listViewItem.SubItems.Add(ConvertSize(item.Size));

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
                        listViewItem.SubItems.Add(ConvertSize(item.Size));

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
                            //MessageBox.Show($"File '{Path.GetFileName(filePath)}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            contextMenu.Items.Add(newMenu);

            var uploadFileContextItem = new ToolStripMenuItem("Upload File")
            {
                BackColor = Color.FromArgb(173, 216, 230),
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(0, 102, 204)
            };
            uploadFileContextItem.Click += UploadFileMenuItem_Click;
            contextMenu.Items.Add(uploadFileContextItem);

            var deleteFileItem = new ToolStripMenuItem("Delete File");
            deleteFileItem.Click += async (s, e) => await DeleteFile_Click(s, e);
            contextMenu.Items.Add(deleteFileItem);

            // Add Rename menu item for listView1
            var renameFileItem = new ToolStripMenuItem("Rename");
            renameFileItem.Click += async (s, e) => await RenameFile_Click(s, e);
            contextMenu.Items.Add(renameFileItem);

            return contextMenu;
        }

        private ContextMenuStrip CreateClientsContextMenu()
        {
            var contextMenu = new ContextMenuStrip
            {
                BackColor = Color.FromArgb(173, 216, 230),
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(0, 102, 204),
                ImageScalingSize = new Size(24, 24)
            };

            // Add Rename menu item for clientsViewBox (only show for client nodes)
            contextMenu.Opening += (sender, e) =>
            {
                var menu = sender as ContextMenuStrip;
                menu.Items.Clear();

                // Only show the Rename option if a client node is selected
                if (clientsViewBox.SelectedNode?.Tag is Helpers.Client)
                {
                    var renameClientItem = new ToolStripMenuItem("Rename");
                    renameClientItem.Click += async (s, e) => await RenameClient_Click(s, e);
                    menu.Items.Add(renameClientItem);
                }
            };

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
                        //MessageBox.Show($"File '{fileName}' uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async Task RenameClient_Click(object sender, EventArgs e)
        {
            if (clientsViewBox.SelectedNode == null || !(clientsViewBox.SelectedNode.Tag is Helpers.Client))
            {
                MessageBox.Show("Please select a client to rename.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedNode = clientsViewBox.SelectedNode;
            var client = selectedNode.Tag as Helpers.Client;
            if (client == null || client.Id == Guid.Empty)
            {
                MessageBox.Show("Invalid client selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newName = Prompt.ShowDialog("Enter new client name:", "Rename Client", client.Name);
            if (string.IsNullOrEmpty(newName) || newName == client.Name)
                return;

            await RunWithLoader(async () =>
            {
                try
                {
                    UpdateStatusLabel($"Renaming client '{client.Name}' to '{newName}'...");

                    var clientUpdate = new Helpers.ClientDto()
                    {
                        Id = client.Id,
                        Name = newName,
                        CompanyType = (int)client.CompanyType,
                        Address = "",
                        Email = "",
                        Phone = ""
                    };

                    await _apiHelper.UpdateClientAsync(clientUpdate);

                    // Update the local client list
                    var clientToUpdate = _clients.FirstOrDefault(c => c.Id == client.Id);
                    if (clientToUpdate != null)
                    {
                        clientToUpdate.Name = newName;
                    }

                    // Update the filtered list as well
                    var filteredClientToUpdate = _filteredClients.FirstOrDefault(c => c.Id == client.Id);
                    if (filteredClientToUpdate != null)
                    {
                        filteredClientToUpdate.Name = newName;
                    }

                    // Update the node's text
                    selectedNode.Text = newName;

                    UpdateStatusLabel($"Client renamed to '{newName}' successfully.");
                    MessageBox.Show($"Client renamed to '{newName}' successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to rename client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error renaming client.");
                }
            });
        }

        private async Task RenameFile_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file or folder to rename.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listView1.SelectedItems[0];
            var driveItem = selectedItem.Tag as GoogleDriveItem;
            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id))
            {
                MessageBox.Show("Invalid item selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newName = Prompt.ShowDialog("Enter new name:", "Rename Item", driveItem.Name);
            if (string.IsNullOrEmpty(newName) || newName == driveItem.Name)
                return;

            await RunWithLoader(async () =>
            {
                try
                {
                    UpdateStatusLabel($"Renaming '{driveItem.Name}' to '{newName}'...");

                    // Assuming the API helper has a method to rename a file/folder in Google Drive
                    //await _apiHelper.RenameFileAsync(driveItem.Id, newName);

                    // Refresh the file list
                    await LoadFolderContents(CurrentFolderId);
                    UpdateStatusLabel($"Item renamed to '{newName}' successfully.");
                    MessageBox.Show($"Item renamed to '{newName}' successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to rename item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error renaming item.");
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
            var dialogResult = loginForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                var newMainForm = new MainForm(_apiHelper, SessionHelper.CurrentUser.Role);
                newMainForm.Show();
                this.Close();
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

        private void UpdateStatusLabel(string message)
        {
            var statusStrip = statusLabel.GetCurrentParent() as StatusStrip;
            if (statusStrip == null)
            {
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
                _animationTimer.Start();
                Application.DoEvents();
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
                Application.DoEvents();
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustControlsLayout();
        }

        private void AdjustControlsLayout()
        {
            int formWidth = this.ClientSize.Width;
            int backButtonRight = backButton.Right;
            int logoutButtonWidth = button1.Width;
            int searchButtonWidth = searchButton.Width;
            int margin = 10;

            button1.Location = new Point(formWidth - logoutButtonWidth - margin, button1.Location.Y);
            searchTextBox.Location = new Point(backButtonRight + margin, searchTextBox.Location.Y);
            searchTextBox.Width = formWidth - backButtonRight - logoutButtonWidth - searchButtonWidth - (margin * 4);
            searchButton.Location = new Point(searchTextBox.Right + margin, searchButton.Location.Y);
        }

        private string ConvertSize(long? sizeInBytes)
        {
            if (!sizeInBytes.HasValue || sizeInBytes == 0)
                return "0 B";

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = sizeInBytes.Value;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }
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

public class FileItem
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; set; }
    public DateTime DateModified { get; set; }
    public string LastModifiedBy { get; set; }
}