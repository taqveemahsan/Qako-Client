using Microsoft.VisualBasic.ApplicationServices;
using QACORDMS.Client.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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

        private const int HDM_GETITEM = 0x1203; // HDM_GETITEMW
        private const int HDM_SETITEM = 0x1204; // HDM_SETITEMW
        private const int HDF_SORTUP = 0x0400;
        private const int HDF_SORTDOWN = 0x0200;
        private const int HDI_TEXT = 0x0002;
        private const int HDI_FORMAT = 0x0004;
        private const int HDF_LEFT = 0x0000;
        private const int HDF_CENTER = 0x0002;
        private const int HDF_RIGHT = 0x0001;

        private List<Image> _loadingFrames;
        private readonly HashSet<string> _createdFolders = new HashSet<string>();


        public MainForm(QACOAPIHelper apiHelper, string userRole = null)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            _userRole = userRole;
            InitializeComponent();

            //LoadAnimationFrames();

            // Initialize animation timer for custom loader
            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 50;
            _animationTimer.Tick += (s, e) =>
            {
                _rotationAngle = (_rotationAngle + 10) % 360;
                loaderPictureBox.Invalidate();
            };

            // Set Paint event for custom drawing
            //loaderPictureBox.Paint += LoaderPictureBox_Paint;

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

            CenterLoaderControls();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HDITEM
        {
            public int mask;
            public int cxy;
            public IntPtr pszText;
            public IntPtr hbm;
            public int cchTextMax;
            public int fmt;
            public IntPtr lParam;
            public int iImage;
            public int iOrder;
            public uint type;
            public IntPtr pvFilter;
            public uint state;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref HDITEM lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

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

        //private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    if (e.Column == sortColumn)
        //    {
        //        sortOrder = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
        //    }
        //    else
        //    {
        //        sortColumn = e.Column;
        //        sortOrder = SortOrder.Ascending;
        //    }

        //    listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
        //    listView1.Sort();
        //}

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine the new sort direction
            if (e.Column == sortColumn)
            {
                sortOrder = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Clear the sort arrow from the previous column
                if (sortColumn != -1)
                {
                    SetSortArrow(sortColumn, SortOrder.None);
                }
                sortColumn = e.Column;
                sortOrder = SortOrder.Ascending;
            }

            // Set the sort arrow on the current column
            SetSortArrow(sortColumn, sortOrder);

            // Perform the sort
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
            listView1.Sort();
        }

        private void SetSortArrow(int columnIndex, SortOrder sortOrder)
        {
            // Get the handle to the ListView's header control
            IntPtr header = GetDlgItem(listView1.Handle, 0); // 0 gets the header control

            // Initialize HDITEM to retrieve the current header item's text and format
            HDITEM hdItem = new HDITEM
            {
                mask = HDI_TEXT | HDI_FORMAT, // Request text and format
                pszText = Marshal.AllocHGlobal(260 * sizeof(char)), // Allocate buffer for text (MAX_PATH = 260)
                cchTextMax = 260,
                fmt = 0
            };

            try
            {
                // Retrieve the current header item (including text and format)
                SendMessage(header, HDM_GETITEM, (IntPtr)columnIndex, ref hdItem);

                // Preserve the existing format (e.g., alignment) and only modify the sort arrow
                hdItem.mask = HDI_FORMAT; // We’re updating the format
                hdItem.fmt &= ~(HDF_SORTUP | HDF_SORTDOWN); // Clear existing sort arrows
                hdItem.fmt |= sortOrder switch
                {
                    SortOrder.Ascending => HDF_SORTUP,
                    SortOrder.Descending => HDF_SORTDOWN,
                    _ => 0
                };

                // Ensure text alignment is preserved (optional, in case alignment was lost)
                if ((hdItem.fmt & HDF_LEFT) == 0 && (hdItem.fmt & HDF_RIGHT) == 0 && (hdItem.fmt & HDF_CENTER) == 0)
                {
                    hdItem.fmt |= HDF_LEFT; // Default to left alignment if none is set
                }

                // Apply the updated header item with the sort arrow
                SendMessage(header, HDM_SETITEM, (IntPtr)columnIndex, ref hdItem);
            }
            finally
            {
                // Free the allocated memory for pszText
                if (hdItem.pszText != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(hdItem.pszText);
                }
            }
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

        private string FormatCompanyTypeName(string enumName)
        {
            // Custom mappings for specific enum values that don't split nicely
            var customMappings = new Dictionary<string, string>
            {
                { "PrivateLable", "Private Label" },
                { "PublicComp", "Public Company" },
                { "ForeignCompanies", "Foreign Companies" },
                { "PartnershipFirms", "Partnership Firms" },
                { "NonProfitOrganizations", "Non-Profit Organizations" },
                { "NBFC", "NBFC" }, // Keep as-is since it's an acronym
                { "PICS", "PICS" }, // Keep as-is since it's an acronym
                { "ProvidentGratuityFunds", "Provident & Gratuity Funds" },
                { "IndividualsSoleProprietors", "Individuals & Sole Proprietors" },
                { "Others", "Others" }
            };

            // If the enum name has a custom mapping, return it
            if (customMappings.TryGetValue(enumName, out var formattedName))
            {
                return formattedName;
            }

            // Fallback: Split camel-case names and join with spaces
            // e.g., "ThisIsATest" -> "This Is A Test"
            var result = System.Text.RegularExpressions.Regex.Replace(enumName, "([a-z])([A-Z])", "$1 $2");
            return result;
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
                    string formattedCompanyTypeName = FormatCompanyTypeName(companyTypeName); // Format the name
                    var companyNode = new TreeNode(formattedCompanyTypeName)
                    {
                        Tag = group.Key // Store the original CompanyType for reference
                    };

                    foreach (var client in group.OrderBy(c => c.Name))
                    {
                        string folderSize = string.IsNullOrEmpty(client.FolderSize) ? "0KB" : client.FolderSize;
                        string nodeText = $"{client.Name} ({folderSize})";
                        var clientNode = new TreeNode(nodeText)
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
        //private void UpdateClientsViewBox(List<Helpers.Client> clientsToDisplay)
        //{
        //    if (clientsViewBox.InvokeRequired)
        //    {
        //        clientsViewBox.Invoke(new Action<List<Helpers.Client>>(UpdateClientsViewBox), clientsToDisplay);
        //    }
        //    else
        //    {
        //        clientsViewBox.Nodes.Clear();

        //        var groupedClients = clientsToDisplay
        //            .GroupBy(c => c.CompanyType)
        //            .OrderBy(g => g.Key);

        //        foreach (var group in groupedClients)
        //        {
        //            string companyTypeName = Enum.GetName(typeof(CompanyType), group.Key);
        //            string formattedCompanyTypeName = FormatCompanyTypeName(companyTypeName); // Format the name
        //            var companyNode = new TreeNode(formattedCompanyTypeName)
        //            {
        //                Tag = group.Key // Store the original CompanyType for reference
        //            };

        //            foreach (var client in group.OrderBy(c => c.Name))
        //            {
        //                var clientNode = new TreeNode(client.Name)
        //                {
        //                    Tag = client
        //                };
        //                companyNode.Nodes.Add(clientNode);
        //            }

        //            clientsViewBox.Nodes.Add(companyNode);
        //        }

        //        if (clientsViewBox.Nodes.Count > 0)
        //        {
        //            clientsViewBox.Nodes[0].Expand();
        //            if (clientsViewBox.Nodes[0].Nodes.Count > 0)
        //            {
        //                clientsViewBox.SelectedNode = clientsViewBox.Nodes[0].Nodes[0];
        //            }
        //        }
        //    }
        //}

        //private void UpdateClientsViewBox(List<Helpers.Client> clientsToDisplay)
        //{
        //    if (clientsViewBox.InvokeRequired)
        //    {
        //        clientsViewBox.Invoke(new Action<List<Helpers.Client>>(UpdateClientsViewBox), clientsToDisplay);
        //    }
        //    else
        //    {
        //        clientsViewBox.Nodes.Clear();

        //        var groupedClients = clientsToDisplay
        //            .GroupBy(c => c.CompanyType)
        //            .OrderBy(g => g.Key);

        //        foreach (var group in groupedClients)
        //        {
        //            string companyTypeName = Enum.GetName(typeof(CompanyType), group.Key);
        //            var companyNode = new TreeNode(companyTypeName)
        //            {
        //                Tag = group.Key
        //            };

        //            foreach (var client in group.OrderBy(c => c.Name))
        //            {
        //                var clientNode = new TreeNode(client.Name)
        //                {
        //                    Tag = client
        //                };
        //                companyNode.Nodes.Add(clientNode);
        //            }

        //            clientsViewBox.Nodes.Add(companyNode);
        //        }

        //        if (clientsViewBox.Nodes.Count > 0)
        //        {
        //            clientsViewBox.Nodes[0].Expand();
        //            if (clientsViewBox.Nodes[0].Nodes.Count > 0)
        //            {
        //                clientsViewBox.SelectedNode = clientsViewBox.Nodes[0].Nodes[0];
        //            }
        //        }
        //    }
        //}

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
                            // Load thumbnail if available and not already in imageList1
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

                            // Create the ListViewItem with the Name column
                            var listViewItem = new ListViewItem(item.Name)
                            {
                                ImageKey = item.MimeType ?? "Unknown",
                                ImageIndex = imageList1.Images.IndexOfKey(item.MimeType ?? "Unknown"),
                                Tag = item
                            };

                            // Map the remaining columns
                            listViewItem.SubItems.Add(item.IsFolder ? "Folder" : (item.FileExtension ?? "File")); // Type
                            listViewItem.SubItems.Add(ConvertSize(item.Size)); // Size
                            //listViewItem.SubItems.Add(item.CreatedOn?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"); // Date Created
                            //listViewItem.SubItems.Add(item.CreatedBy?.ToString() ?? "N/A"); // Created By
                            //listViewItem.SubItems.Add(item.ModifiedOn?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"); // Date Modified
                            //listViewItem.SubItems.Add(item.ModifiedBy?.ToString() ?? "N/A"); // Last Modified By
                            listViewItem.SubItems.Add(item.CreatedOn.HasValue && item.CreatedOn.Value > DateTime.MinValue ? item.CreatedOn.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""); // Date Created
                            listViewItem.SubItems.Add(string.IsNullOrEmpty(item.CreatedBy) || item.CreatedBy == Guid.Empty.ToString() ? "" : item.CreatedBy); // Created By
                            listViewItem.SubItems.Add(item.ModifiedOn.HasValue && item.ModifiedOn.Value > DateTime.MinValue ? item.ModifiedOn.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""); // Date Modified
                            listViewItem.SubItems.Add(string.IsNullOrEmpty(item.ModifiedBy) || item.ModifiedBy == Guid.Empty.ToString() ? "" : item.ModifiedBy); // Last Modified By

                            listView1.Items.Add(listViewItem);
                        }

                        // Position items if in LargeIcon or SmallIcon view
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
                        // Load thumbnail if available and not already in imageList1
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

                        // Create the ListViewItem with the Name column
                        var listViewItem = new ListViewItem(item.Name)
                        {
                            ImageKey = item.MimeType ?? "Unknown",
                            ImageIndex = imageList1.Images.IndexOfKey(item.MimeType ?? "Unknown"),
                            Tag = item
                        };

                        // Map the remaining columns
                        listViewItem.SubItems.Add(item.IsFolder ? "Folder" : (item.FileExtension ?? "File")); // Type
                        listViewItem.SubItems.Add(ConvertSize(item.Size)); // Size
                        listViewItem.SubItems.Add(item.CreatedOn.HasValue && item.CreatedOn.Value > DateTime.MinValue ? item.CreatedOn.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""); // Date Created
                        listViewItem.SubItems.Add(string.IsNullOrEmpty(item.CreatedBy) || (item.CreatedBy != null && item.CreatedBy.ToLower() == Guid.Empty.ToString().ToLower()) ? "" : item.CreatedBy); // Created By
                        listViewItem.SubItems.Add(item.ModifiedOn.HasValue && item.ModifiedOn.Value > DateTime.MinValue ? item.ModifiedOn.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""); // Date Modified
                        listViewItem.SubItems.Add(string.IsNullOrEmpty(item.ModifiedBy) || (item.ModifiedBy != null && item.ModifiedBy.ToLower() == Guid.Empty.ToString().ToLower()) ? "" : item.ModifiedBy); // Last Modified By

                        listView1.Items.Add(listViewItem);
                    }

                    // Position items if in LargeIcon or SmallIcon view
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

        //private async void refreshMenuItem_Click(object sender, EventArgs e)
        //{
        //    await RunWithLoader(async () =>
        //    {
        //        try
        //        {
        //            var clientsForm = new ClientsForm(_apiHelper);
        //            clientsForm.ShowDialog();
        //            LoadClientsAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Failed to open Clients form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    });
        //}

        private async void refreshMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateStatusLabel("Refreshing clients...");
                await RunWithLoader(async () =>
                {
                    // Fetch clients from API (example implementation)
                    var response = await _apiHelper.GetClientsAsync(); // Assume this method exists
                    var clients = response.Clients;

                    // Clear existing nodes
                    clientsViewBox.Nodes.Clear();

                    // Populate TreeView with clients and folder sizes
                    foreach (var client in clients)
                    {
                        string nodeText = $"{client.Name} ({client.FolderSize})";
                        TreeNode node = new TreeNode(nodeText)
                        {
                            Tag = client // Store client object for later use
                        };
                        clientsViewBox.Nodes.Add(node);
                    }

                    UpdateStatusLabel($"Loaded {clients.Count} clients.");
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to refresh clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel("Error refreshing clients.");
            }
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

        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            bool hasFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
            bool hasValidProject = !string.IsNullOrEmpty(_selectedProject?.GoogleDriveFolderId);

            UpdateStatusLabel($"DragEnter: HasFileDrop={hasFileDrop}, HasValidProject={hasValidProject}");

            if (!hasValidProject)
            {
                UpdateStatusLabel("Please select a project before dragging files.");
            }

            if (hasFileDrop && hasValidProject)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private async void ListView1_DragDrop(object sender, DragEventArgs e)
        {

            await RunWithLoader(async () =>
            {
                try
                {
                    string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (paths == null || paths.Length == 0)
                    {
                        UpdateStatusLabel("No valid files or folders dropped.");
                        return;
                    }

                    UpdateStatusLabel($"Dropped {paths.Length} item(s).");

                    foreach (string path in paths)
                    {
                        if (File.Exists(path)) // Handle file
                        {
                            UpdateStatusLabel($"Uploading file: {Path.GetFileName(path)}...");
                            string uploadedFileId = await _apiHelper.UploadFileAsync(path, CurrentFolderId);
                            UpdateStatusLabel($"Uploaded file: {Path.GetFileName(path)}");
                        }
                        else if (Directory.Exists(path)) // Handle folder
                        {
                            UpdateStatusLabel($"Creating folder: {Path.GetFileName(path)}...");
                            string folderId = await _apiHelper.CreateFolderAsync(Path.GetFileName(path), CurrentFolderId);
                            UpdateStatusLabel($"Created folder: {Path.GetFileName(path)}");
                            await Task.Delay(500);

                            // Optionally, recursively upload folder contents
                            await UploadFolderContents(path, folderId);
                        }
                        else
                        {
                            UpdateStatusLabel($"Invalid item: {path}");
                        }
                    }

                    //MessageBox.Show($"{paths.Length} item(s) processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RefreshFileList();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Failed to process dropped item(s): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error processing dropped item(s).");
                }
            });
        }

        private async Task UploadFolderContents(string folderPath, string parentFolderId)
        {
            try
            {
                // Upload files in the folder
                foreach (string filePath in Directory.GetFiles(folderPath))
                {
                    UpdateStatusLabel($"Uploading file: {Path.GetFileName(filePath)}...");
                    string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, parentFolderId);
                    UpdateStatusLabel($"Uploaded file: {Path.GetFileName(filePath)}");
                }

                // Recursively handle subfolders
                foreach (string subFolderPath in Directory.GetDirectories(folderPath))
                {
                    if (_createdFolders.Contains(subFolderPath)) continue;

                    string subFolderName = Path.GetFileName(subFolderPath);
                    UpdateStatusLabel($"Creating subfolder: {subFolderName}...");

                    string subFolderId = await _apiHelper.CreateFolderAsync(subFolderName, parentFolderId);
                    _createdFolders.Add(subFolderPath);
                    await Task.Delay(500);

                    UpdateStatusLabel($"Created subfolder: {subFolderName}");

                    await UploadFolderContents(subFolderPath, subFolderId);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Failed to upload folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel("Error uploading folder contents.");
            }
        }


        //private async Task UploadFolderContents(string folderPath, string parentFolderId)
        //{
        //    try
        //    {
        //        // Upload files in the folder
        //        foreach (string filePath in Directory.GetFiles(folderPath))
        //        {
        //            UpdateStatusLabel($"Uploading file: {Path.GetFileName(filePath)}...");
        //            string uploadedFileId = await _apiHelper.UploadFileAsync(filePath, parentFolderId);
        //            UpdateStatusLabel($"Uploaded file: {Path.GetFileName(filePath)}");
        //            await Task.Delay(1000);
        //        }

        //        // Recursively handle subfolders
        //        foreach (string subFolderPath in Directory.GetDirectories(folderPath))
        //        {
        //            string subFolderName = Path.GetFileName(subFolderPath);
        //            UpdateStatusLabel($"Creating subfolder: {subFolderName}...");
        //            string subFolderId = await _apiHelper.CreateFolderAsync(subFolderName, parentFolderId);
        //            UpdateStatusLabel($"Created subfolder: {subFolderName}");
        //            await Task.Delay(1000);
        //            await UploadFolderContents(subFolderPath, subFolderId);
        //            await Task.Delay(1000);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to upload folder contents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        UpdateStatusLabel("Error uploading folder contents.");
        //    }
        //}

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

                    var response = await _apiHelper.UpdateClientAsync(clientUpdate);

                    // Check if the response was successful
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMessage = "An error occurred while renaming the client.";
                        try
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                            if (jsonResponse != null && jsonResponse.ContainsKey("message"))
                            {
                                errorMessage = jsonResponse["message"]; // e.g., "A client with this name already exists."
                            }
                            else
                            {
                                errorMessage = $"HTTP {response.StatusCode}: {errorContent}";
                            }
                        }
                        catch
                        {
                            errorMessage = $"HTTP {response.StatusCode}: Unable to parse error details.";
                        }

                        MessageBox.Show($"Failed to rename client: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusLabel("Error renaming client.");
                        return;
                    }

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to rename client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error renaming client.");
                }
            });
        }
        //private async Task RenameClient_Click(object sender, EventArgs e)
        //{
        //    if (clientsViewBox.SelectedNode == null || !(clientsViewBox.SelectedNode.Tag is Helpers.Client))
        //    {
        //        MessageBox.Show("Please select a client to rename.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    var selectedNode = clientsViewBox.SelectedNode;
        //    var client = selectedNode.Tag as Helpers.Client;
        //    if (client == null || client.Id == Guid.Empty)
        //    {
        //        MessageBox.Show("Invalid client selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    string newName = Prompt.ShowDialog("Enter new client name:", "Rename Client", client.Name);
        //    if (string.IsNullOrEmpty(newName) || newName == client.Name)
        //        return;

        //    await RunWithLoader(async () =>
        //    {
        //        try
        //        {
        //            UpdateStatusLabel($"Renaming client '{client.Name}' to '{newName}'...");

        //            var clientUpdate = new Helpers.ClientDto()
        //            {
        //                Id = client.Id,
        //                Name = newName,
        //                CompanyType = (int)client.CompanyType,
        //                Address = "",
        //                Email = "",
        //                Phone = ""
        //            };

        //            await _apiHelper.UpdateClientAsync(clientUpdate);

        //            // Update the local client list
        //            var clientToUpdate = _clients.FirstOrDefault(c => c.Id == client.Id);
        //            if (clientToUpdate != null)
        //            {
        //                clientToUpdate.Name = newName;
        //            }

        //            // Update the filtered list as well
        //            var filteredClientToUpdate = _filteredClients.FirstOrDefault(c => c.Id == client.Id);
        //            if (filteredClientToUpdate != null)
        //            {
        //                filteredClientToUpdate.Name = newName;
        //            }

        //            // Update the node's text
        //            selectedNode.Text = newName;

        //            UpdateStatusLabel($"Client renamed to '{newName}' successfully.");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Failed to rename client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            UpdateStatusLabel("Error renaming client.");
        //        }
        //    });
        //}
        private async Task RenameFile_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file or folder to rename.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listView1.SelectedItems[0];
            var driveItem = selectedItem.Tag as GoogleDriveItem;
            if (driveItem == null || string.IsNullOrEmpty(driveItem.Id)) // Updated to use GoogleId as per previous model change
            {
                MessageBox.Show("Invalid item selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Extract the base name (without extension for files) to show in the dialog
            string originalExtension = driveItem.IsFolder ? "" : Path.GetExtension(driveItem.Name);
            string baseName = driveItem.IsFolder ? driveItem.Name : Path.GetFileNameWithoutExtension(driveItem.Name);

            string newBaseName = Prompt.ShowDialog("Enter new name:", "Rename Item", baseName);
            if (string.IsNullOrEmpty(newBaseName) || newBaseName == baseName)
                return;

            // Append the original extension for files
            string newName = driveItem.IsFolder ? newBaseName : $"{newBaseName}{originalExtension}";
            if (newName == driveItem.Name) // In case the name didn't actually change after adding extension
                return;

            await RunWithLoader(async () =>
            {
                try
                {
                    UpdateStatusLabel($"Renaming '{driveItem.Name}' to '{newName}'...");

                    var response = await _apiHelper.RenameFileAsync(driveItem.Id, newName); // Updated to use GoogleId
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMessage = "An error occurred while renaming the item.";
                        try
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                            if (jsonResponse != null && jsonResponse.ContainsKey("message"))
                            {
                                errorMessage = jsonResponse["message"]; // e.g., "An item with this name already exists in the folder."
                            }
                            else
                            {
                                errorMessage = $"HTTP {response.StatusCode}: {errorContent}";
                            }
                        }
                        catch
                        {
                            errorMessage = $"HTTP {response.StatusCode}: Unable to parse error details.";
                        }

                        MessageBox.Show($"Failed to rename item: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusLabel("Error renaming item.");
                        return;
                    }

                    await LoadFolderContents(CurrentFolderId);
                    UpdateStatusLabel($"Item renamed to '{newName}' successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to rename item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatusLabel("Error renaming item.");
                }
            });
        }
        private async Task RenameFile_ClickV1(object sender, EventArgs e)
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
                    var response = await _apiHelper.RenameFileAsync(driveItem.Id, newName);
                    // Check if the response was successful
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMessage = "An error occurred while renaming the client.";
                        try
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                            if (jsonResponse != null && jsonResponse.ContainsKey("message"))
                            {
                                errorMessage = jsonResponse["message"]; // e.g., "A client with this name already exists."
                            }
                            else
                            {
                                errorMessage = $"HTTP {response.StatusCode}: {errorContent}";
                            }
                        }
                        catch
                        {
                            errorMessage = $"HTTP {response.StatusCode}: Unable to parse error details.";
                        }

                        MessageBox.Show($"Failed to rename client: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatusLabel("Error renaming client.");
                        return;
                    }


                    // Refresh the file list
                    await LoadFolderContents(CurrentFolderId);
                    UpdateStatusLabel($"Item renamed to '{newName}' successfully.");
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
                        await Task.Run(async () =>await MonitorAndReplaceFileOnClose(tempFilePath, driveItem.Id, fileName, process));
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"Error processing {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DateTime originalLastWriteTime = File.Exists(filePath) ? File.GetLastWriteTimeUtc(filePath) : DateTime.MinValue;
                string originalHash = File.Exists(filePath) ? ComputeFileHash(filePath) : null;

                using (FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath)))
                {
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                    watcher.Changed += async (s, e) =>
                    {
                        if (File.Exists(filePath))
                        {
                            DateTime currentLastWriteTime = File.GetLastWriteTimeUtc(filePath);
                            string currentHash = ComputeFileHash(filePath);
                            if (currentLastWriteTime > originalLastWriteTime || currentHash != originalHash)
                            {
                                fileChanged = true;
                                UpdateStatusLabel($"Changes detected in {fileName}, uploading...");
                                await RunWithLoader(async () =>
                                {
                                    await WaitForFileRelease(filePath);
                                    var response = await _apiHelper.ReplaceFileAsync(fileId, filePath);
                                    if (response.IsSuccessStatusCode)
                                    {
                                        UpdateStatusLabel($"{fileName} updated successfully.");
                                        originalLastWriteTime = currentLastWriteTime; // Update baseline
                                        originalHash = currentHash; // Update baseline
                                    }
                                    else
                                    {
                                        string errorContent = await response.Content.ReadAsStringAsync();
                                        UpdateStatusLabel($"Failed to update {fileName}: HTTP {response.StatusCode}, {errorContent}");
                                    }
                                });
                            }
                        }
                    };
                    watcher.EnableRaisingEvents = true;

                    UpdateStatusLabel($"Monitoring changes in {fileName}...");
                    await Task.Run(() => process.WaitForExit());

                    // Check for changes one last time after process exit
                    if (!fileChanged && File.Exists(filePath))
                    {
                        DateTime currentLastWriteTime = File.GetLastWriteTimeUtc(filePath);
                        string currentHash = ComputeFileHash(filePath);
                        if (currentLastWriteTime > originalLastWriteTime || currentHash != originalHash)
                        {
                            fileChanged = true;
                            UpdateStatusLabel($"Changes detected in {fileName} on close, uploading...");
                            await RunWithLoader(async () =>
                            {
                                await WaitForFileRelease(filePath);
                                var response = await _apiHelper.ReplaceFileAsync(fileId, filePath);
                                if (response.IsSuccessStatusCode)
                                {
                                    UpdateStatusLabel($"{fileName} updated successfully.");
                                }
                                else
                                {
                                    string errorContent = await response.Content.ReadAsStringAsync();
                                    UpdateStatusLabel($"Failed to update {fileName}: HTTP {response.StatusCode}, {errorContent}");
                                }
                            });
                        }
                    }

                    if (!fileChanged)
                    {
                        UpdateStatusLabel($"No changes detected in {fileName}.");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error updating {fileName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel($"Error updating {fileName}: {ex.Message}");
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        await WaitForFileRelease(filePath);
                        File.Delete(filePath);
                        UpdateStatusLabel($"{fileName} temp file removed.");
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusLabel($"Failed to delete temp file {fileName}: {ex.Message}");
                    }
                }
                openedFiles.Remove(filePath);
            }
        }

        private string ComputeFileHash(string filePath)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private async Task MonitorAndReplaceFileOnCloseV2(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
        {
            try
            {
                bool fileChanged = false;
                DateTime originalLastWriteTime = File.Exists(filePath) ? File.GetLastWriteTimeUtc(filePath) : DateTime.MinValue;

                using (FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath)))
                {
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName;
                    watcher.Changed += (s, e) => fileChanged = true;
                    watcher.EnableRaisingEvents = true;

                    UpdateStatusLabel($"Monitoring changes in {fileName}...");
                    await Task.Run(() => process.WaitForExit());
                }

                // Check for changes via FileSystemWatcher or last write time
                if (!fileChanged && File.Exists(filePath))
                {
                    DateTime currentLastWriteTime = File.GetLastWriteTimeUtc(filePath);
                    fileChanged = currentLastWriteTime > originalLastWriteTime;
                }

                if (fileChanged && File.Exists(filePath))
                {
                    await RunWithLoader(async () =>
                    {
                        UpdateStatusLabel($"Detected changes in {fileName}, updating...");
                        await WaitForFileRelease(filePath);

                        // Retry ReplaceFileAsync up to 3 times for transient errors
                        bool uploadSuccess = false;
                        int maxRetries = 3;
                        int retryDelayMs = 2000;
                        for (int attempt = 1; attempt <= maxRetries; attempt++)
                        {
                            try
                            {
                                var response = await _apiHelper.ReplaceFileAsync(fileId, filePath);
                                if (response.IsSuccessStatusCode)
                                {
                                    uploadSuccess = true;
                                    UpdateStatusLabel($"{fileName} updated successfully.");
                                    break;
                                }
                                else
                                {
                                    string errorContent = await response.Content.ReadAsStringAsync();
                                    UpdateStatusLabel($"Attempt {attempt} failed to update {fileName}: HTTP {response.StatusCode}, {errorContent}");
                                    if (attempt < maxRetries)
                                        await Task.Delay(retryDelayMs);
                                }
                            }
                            catch (Exception ex)
                            {
                                UpdateStatusLabel($"Attempt {attempt} failed to update {fileName}: {ex.Message}");
                                if (attempt < maxRetries)
                                    await Task.Delay(retryDelayMs);
                            }
                        }

                        if (!uploadSuccess)
                        {
                            MessageBox.Show($"Failed to update {fileName} after {maxRetries} attempts. Changes may not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            UpdateStatusLabel($"Failed to update {fileName}.");
                            return; // Keep the temp file for manual recovery
                        }
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
                UpdateStatusLabel($"Error updating {fileName}: {ex.Message}");
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        await WaitForFileRelease(filePath);
                        File.Delete(filePath);
                        UpdateStatusLabel($"{fileName} temp file removed.");
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusLabel($"Failed to delete temp file {fileName}: {ex.Message}");
                        // Keep the file to allow manual recovery
                    }
                }
                openedFiles.Remove(filePath);
            }
        }

        private async Task MonitorAndReplaceFileOnCloseV1(string filePath, string fileId, string fileName, System.Diagnostics.Process process)
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
            int maxRetries = 20; // Increased from 10
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
                    UpdateStatusLabel($"Waiting for file release (attempt {i + 1}/{maxRetries})...");
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
            if (loaderOverlay == null) return;

            if (loaderOverlay.InvokeRequired)
            {
                loaderOverlay.Invoke(new Action(ShowLoader));
            }
            else
            {
                loaderOverlay.Visible = true;
                loaderOverlay.BringToFront(); // Ensure the overlay is on top
                Application.DoEvents();
            }
        }

        private void HideLoader()
        {
            if (loaderOverlay == null) return;

            if (loaderOverlay.InvokeRequired)
            {
                loaderOverlay.Invoke(new Action(HideLoader));
            }
            else
            {
                loaderOverlay.Visible = false;
                Application.DoEvents();
            }
        }

        //private void ShowLoader()
        //{
        //    if (loaderPanel == null) return;

        //    if (loaderPanel.InvokeRequired)
        //    {
        //        loaderPanel.Invoke(new Action(ShowLoader));
        //    }
        //    else
        //    {
        //        loaderPanel.Visible = true;
        //        loaderPanel.BringToFront();
        //        _animationTimer.Start();
        //        Application.DoEvents();
        //    }
        //}

        //private void HideLoader()
        //{
        //    if (loaderPanel == null) return;

        //    if (loaderPanel.InvokeRequired)
        //    {
        //        loaderPanel.Invoke(new Action(HideLoader));
        //    }
        //    else
        //    {
        //        loaderPanel.Visible = false;
        //        Application.DoEvents();
        //    }
        //}

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
            CenterLoaderControls();
            //base.OnResize(e);
            //AdjustControlsLayout();
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

        //private void CenterLoaderControls()
        //{
        //    if (loaderOverlay == null || loaderPictureBox == null || loaderLabel == null) return;

        //    // Center the loaderPictureBox
        //    int pictureBoxX = (this.ClientSize.Width - loaderPictureBox.Width) / 2;
        //    int pictureBoxY = (this.ClientSize.Height - loaderPictureBox.Height) / 2 - 20; // Slightly above center
        //    loaderPictureBox.Location = new Point(pictureBoxX, pictureBoxY);

        //    // Center the loaderLabel below the picture box
        //    int labelX = (this.ClientSize.Width - loaderLabel.Width) / 2;
        //    int labelY = pictureBoxY + loaderPictureBox.Height + 10; // 10px below the picture box
        //    loaderLabel.Location = new Point(labelX, labelY);
        //}

        private void CenterLoaderControls()
        {
            if (loaderOverlay == null || loaderPictureBox == null || loaderLabel == null) return;

            // Center the loaderPictureBox
            int pictureBoxX = (this.ClientSize.Width - loaderPictureBox.Width) / 2;
            int pictureBoxY = (this.ClientSize.Height - loaderPictureBox.Height) / 2 - 20; // Slightly above center
            loaderPictureBox.Location = new Point(pictureBoxX, pictureBoxY);

            // Center the loaderLabel below the picture box
            int labelX = (this.ClientSize.Width - loaderLabel.Width) / 2;
            int labelY = pictureBoxY + loaderPictureBox.Height + 10; // 10px below the picture box
            loaderLabel.Location = new Point(labelX, labelY);
        }

        private void LoadAnimationFrames()
        {
            //_loadingFrames = new List<Image>();
            //// Load frames (replace with actual paths or resources)
            //for (int i = 0; i < 12; i++) // Assuming 12 frames
            //{
            //    _loadingFrames.Add(Image.FromFile($"path_to_frame_{i}.png"));
            //}
        }

        private Image GetLoadingFrame(int frameIndex)
        {
            return _loadingFrames[frameIndex];
        }
    }
}

//public class ListViewItemComparer : IComparer
//{
//    private int column;
//    private SortOrder order;

//    public ListViewItemComparer(int column, SortOrder order)
//    {
//        this.column = column;
//        this.order = order;
//    }

//    public int Compare(object x, object y)
//    {
//        int returnVal = 0;
//        ListViewItem itemX = (ListViewItem)x;
//        ListViewItem itemY = (ListViewItem)y;

//        switch (column)
//        {
//            case 0: // Name column
//                returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
//                break;
//            case 1: // Type column
//                returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
//                break;
//            case 2: // Size column
//                long sizeX = ParseSize(itemX.SubItems[column].Text);
//                long sizeY = ParseSize(itemY.SubItems[column].Text);
//                returnVal = sizeX.CompareTo(sizeY);
//                break;
//        }

//        if (order == SortOrder.Descending)
//            returnVal = -returnVal;

//        return returnVal;
//    }

//    private long ParseSize(string sizeText)
//    {
//        if (string.IsNullOrEmpty(sizeText))
//            return 0;

//        string[] parts = sizeText.Split(' ');
//        if (parts.Length > 0 && long.TryParse(parts[0], out long size))
//        {
//            return size;
//        }
//        return 0;
//    }
//}

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
                double sizeX = ParseSize(itemX.SubItems[column].Text);
                double sizeY = ParseSize(itemY.SubItems[column].Text);
                returnVal = sizeX.CompareTo(sizeY);
                break;
            //case 3: // Date Created column
            //    DateTime dateCreatedX = DateTime.TryParse(itemX.SubItems[column].Text, out var d1) ? d1 : DateTime.MinValue;
            //    DateTime dateCreatedY = DateTime.TryParse(itemY.SubItems[column].Text, out var d2) ? d2 : DateTime.MinValue;
            //    returnVal = DateTime.Compare(dateCreatedX, dateCreatedY);
            //    break;
            //case 4: // Created By column
            //    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
            //    break;
            //case 5: // Date Modified column
            //    DateTime dateModifiedX = DateTime.TryParse(itemX.SubItems[column].Text, out var d3) ? d3 : DateTime.MinValue;
            //    DateTime dateModifiedY = DateTime.TryParse(itemY.SubItems[column].Text, out var d4) ? d4 : DateTime.MinValue;
            //    returnVal = DateTime.Compare(dateModifiedX, dateModifiedY);
            //    break;
            //case 6: // Last Modified By column
            //    returnVal = String.Compare(itemX.SubItems[column].Text, itemY.SubItems[column].Text);
            //    break;
        }

        if (order == SortOrder.Descending)
            returnVal = -returnVal;

        return returnVal;
    }

    private double ParseSize(string sizeText)
    {  
        if (string.IsNullOrEmpty(sizeText) || sizeText == "0 B")
            return 0;

        string[] parts = sizeText.Split(' ');
        if (parts.Length != 2 || !double.TryParse(parts[0], out double size))
            return 0;

        // Convert size to bytes based on the unit
        string unit = parts[1].ToUpper();
        return unit switch
        {
            "B" => size,
            "KB" => size * 1024,
            "MB" => size * 1024 * 1024,
            "GB" => size * 1024 * 1024 * 1024,
            "TB" => size * 1024 * 1024 * 1024 * 1024,
            _ => size
        };
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