using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class AddPermissionForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private readonly Guid _projectId;
        private List<UserProjectPermissionDto> _existingPermissions;
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalUsers = 0;
        private string _searchQuery = "";
        private Dictionary<string, DateTime> _expiryDates = new Dictionary<string, DateTime>(); // Store in UTC

        public AddPermissionForm(QACOAPIHelper apiHelper, Guid projectId)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            _projectId = projectId;
            InitializeComponent();
            LoadUsersAndPermissionsAsync();
        }

        private async void LoadUsersAndPermissionsAsync()
        {
            try
            {
                var response = await _apiHelper.GetRoleUsersAsync(_searchQuery, _currentPage, 50);
                var allUsers = response.Users;
                // Updated: Check if "User" role exists in RoleNames list
                var userRoleUsers = allUsers.Where(u => u.RoleNames != null && u.RoleNames.Contains("User", StringComparer.OrdinalIgnoreCase)).ToList();
                _existingPermissions = await _apiHelper.GetPermissionsByProjectIdAsync(_projectId);

                _totalUsers = userRoleUsers.Count; // Use filtered count
                int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;

                dgvUsers.Rows.Clear();
                foreach (var user in userRoleUsers)
                {
                    var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == user.Id);
                    var hasPermission = existingPermission != null && existingPermission.HasAccess;
                    string expiryDisplay;
                    if (existingPermission != null && existingPermission.ExpiredOn != default(DateTime))
                    {
                        var expiryDate = existingPermission.ExpiredOn.ToLocalTime();
                        expiryDisplay = expiryDate.ToString("g");
                        _expiryDates[user.Id] = existingPermission.ExpiredOn;
                    }
                    else
                    {
                        expiryDisplay = "Set Expiry";
                    }

                    var index = dgvUsers.Rows.Add(hasPermission, user.Username, user.Email, expiryDisplay);
                    dgvUsers.Rows[index].Tag = user.Id;
                    dgvUsers.Rows[index].Cells["colCheckbox"].Value = hasPermission;
                }

                lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";
                btnPrevious.Enabled = _currentPage > 1;
                btnNext.Enabled = _currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users/permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _searchQuery = txtSearch.Text.Trim();
            _currentPage = 1; // Reset to first page on new search
             LoadUsersAndPermissionsAsync();
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                 LoadUsersAndPermissionsAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;
            if (_currentPage < totalPages)
            {
                _currentPage++;
                 LoadUsersAndPermissionsAsync();
            }
        }

        private async void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Handle Delete
            if (e.ColumnIndex == dgvUsers.Columns["colDelete"].Index)
            {
                var userId = dgvUsers.Rows[e.RowIndex].Tag.ToString();
                var permission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
                if (permission != null)
                {
                    var result = MessageBox.Show($"Are you sure you want to remove permission for {dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value}?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        await _apiHelper.DeleteProjectPermissionAsync(permission.Id);
                        dgvUsers.Rows.RemoveAt(e.RowIndex);
                        _existingPermissions.Remove(permission);
                        _expiryDates.Remove(userId);
                        MessageBox.Show("Permission deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            // Handle Expiry
            else if (e.ColumnIndex == dgvUsers.Columns["colExpiry"].Index)
            {
                var userId = dgvUsers.Rows[e.RowIndex].Tag.ToString();
                var datePicker = new DateTimePicker
                {
                    Value = _expiryDates.ContainsKey(userId) ? _expiryDates[userId].ToLocalTime() : DateTime.Now.AddDays(30), // Show local time
                    MinDate = DateTime.Now
                };
                var dialog = new Form
                {
                    Text = "Set Expiry Date",
                    Size = new System.Drawing.Size(300, 150),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                datePicker.Location = new System.Drawing.Point(20, 20);
                var btnOk = new Button
                {
                    Text = "OK",
                    Location = new System.Drawing.Point(100, 60),
                    DialogResult = DialogResult.OK
                };
                dialog.Controls.Add(datePicker);
                dialog.Controls.Add(btnOk);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _expiryDates[userId] = datePicker.Value.ToUniversalTime(); // Convert to UTC for storage
                    // Update grid to reflect local time
                    dgvUsers.Rows[e.RowIndex].Cells["colExpiry"].Value = _expiryDates[userId].ToLocalTime().ToString("g");
                    MessageBox.Show("Expiry date set!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var updatedPermissions = new List<(string UserId, bool HasAccess, DateTime ExpiredOn)>();
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    var checkboxValue = row.Cells["colCheckbox"].Value;
                    var isChecked = checkboxValue != null ? (bool)checkboxValue : false;
                    var userId = row.Tag?.ToString();
                    if (userId != null)
                    {
                        // Use default expiry if not set
                        var expiryDate = _expiryDates.ContainsKey(userId) ? _expiryDates[userId] : DateTime.UtcNow.AddDays(30); // UTC
                        updatedPermissions.Add((userId, isChecked, expiryDate));
                    }
                }

                foreach (var (userId, hasAccess, expiredOn) in updatedPermissions)
                {
                    var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
                    if (existingPermission == null && hasAccess)
                    {
                        var permission = new UserProjectPermissionDto
                        {
                            UserId = userId,
                            ProjectId = _projectId,
                            HasAccess = true,
                            ExpiredOn = expiredOn // Already in UTC
                        };
                        await _apiHelper.AddProjectPermissionAsync(permission);
                    }
                    else if (existingPermission != null && (existingPermission.HasAccess != hasAccess || existingPermission.ExpiredOn != expiredOn))
                    {
                        existingPermission.HasAccess = hasAccess;
                        existingPermission.ExpiredOn = expiredOn; // Already in UTC
                        await _apiHelper.EditProjectPermissionAsync(existingPermission.Id, existingPermission);
                    }
                }

                MessageBox.Show("Permissions updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}















//using QACORDMS.Client.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Forms;

//namespace QACORDMS.Client
//{
//    public partial class AddPermissionForm : Form
//    {
//        private readonly QACOAPIHelper _apiHelper;
//        private readonly Guid _projectId;
//        private List<UserProjectPermissionDto> _existingPermissions;
//        private int _currentPage = 1;
//        private int _pageSize = 5;
//        private int _totalUsers = 0;
//        private string _searchQuery = "";
//        private Dictionary<string, DateTime> _expiryDates = new Dictionary<string, DateTime>(); // Store in UTC

//        public AddPermissionForm(QACOAPIHelper apiHelper, Guid projectId)
//        {
//            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
//            _projectId = projectId;
//            InitializeComponent();
//            LoadUsersAndPermissionsAsync();
//        }

//        private async void LoadUsersAndPermissionsAsync()
//        {
//            try
//            {
//                var response = await _apiHelper.GetUsersAsync(_searchQuery, _currentPage, _pageSize);
//                var allUsers = response.Users;
//                var userRoleUsers = allUsers.Where(u => u.RoleName == "User").ToList();
//                _existingPermissions = await _apiHelper.GetPermissionsByProjectIdAsync(_projectId);

//                _totalUsers = response.TotalUsers;
//                int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;

//                dgvUsers.Rows.Clear();
//                foreach (var user in userRoleUsers)
//                {
//                    var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == user.Id);
//                    var hasPermission = existingPermission != null && existingPermission.HasAccess;
//                    var expiryDate = existingPermission != null ? existingPermission.ExpiredOn.ToLocalTime() : DateTime.MinValue; // Convert to local time for display
//                    if (existingPermission != null)
//                    {
//                        _expiryDates[user.Id] = existingPermission.ExpiredOn; // Store UTC in dictionary
//                    }
//                    var index = dgvUsers.Rows.Add(hasPermission, user.Username, user.Email, expiryDate != DateTime.MinValue ? expiryDate.ToString("g") : "Set Expiry");
//                    dgvUsers.Rows[index].Tag = user.Id;
//                    dgvUsers.Rows[index].Cells["colCheckbox"].Value = hasPermission;
//                }

//                lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";
//                btnPrevious.Enabled = _currentPage > 1;
//                btnNext.Enabled = _currentPage < totalPages;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading users/permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private async void btnSearch_Click(object sender, EventArgs e)
//        {
//            _searchQuery = txtSearch.Text.Trim();
//            _currentPage = 1; // Reset to first page on new search
//            LoadUsersAndPermissionsAsync();
//        }

//        private async void btnPrevious_Click(object sender, EventArgs e)
//        {
//            if (_currentPage > 1)
//            {
//                _currentPage--;
//                LoadUsersAndPermissionsAsync();
//            }
//        }

//        private async void btnNext_Click(object sender, EventArgs e)
//        {
//            int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;
//            if (_currentPage < totalPages)
//            {
//                _currentPage++;
//                LoadUsersAndPermissionsAsync();
//            }
//        }

//        private async void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0) return;

//            // Handle Delete
//            if (e.ColumnIndex == dgvUsers.Columns["colDelete"].Index)
//            {
//                var userId = dgvUsers.Rows[e.RowIndex].Tag.ToString();
//                var permission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
//                if (permission != null)
//                {
//                    var result = MessageBox.Show($"Are you sure you want to remove permission for {dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value}?",
//                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                    if (result == DialogResult.Yes)
//                    {
//                        await _apiHelper.DeleteProjectPermissionAsync(permission.Id);
//                        dgvUsers.Rows.RemoveAt(e.RowIndex);
//                        _existingPermissions.Remove(permission);
//                        _expiryDates.Remove(userId);
//                        MessageBox.Show("Permission deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                }
//            }
//            // Handle Expiry
//            else if (e.ColumnIndex == dgvUsers.Columns["colExpiry"].Index)
//            {
//                var userId = dgvUsers.Rows[e.RowIndex].Tag.ToString();
//                var datePicker = new DateTimePicker
//                {
//                    Value = _expiryDates.ContainsKey(userId) ? _expiryDates[userId].ToLocalTime() : DateTime.Now.AddDays(30), // Show local time
//                    MinDate = DateTime.Now
//                };
//                var dialog = new Form
//                {
//                    Text = "Set Expiry Date",
//                    Size = new System.Drawing.Size(300, 150),
//                    StartPosition = FormStartPosition.CenterParent,
//                    FormBorderStyle = FormBorderStyle.FixedDialog,
//                    MaximizeBox = false,
//                    MinimizeBox = false
//                };
//                datePicker.Location = new System.Drawing.Point(20, 20);
//                var btnOk = new Button
//                {
//                    Text = "OK",
//                    Location = new System.Drawing.Point(100, 60),
//                    DialogResult = DialogResult.OK
//                };
//                dialog.Controls.Add(datePicker);
//                dialog.Controls.Add(btnOk);
//                if (dialog.ShowDialog() == DialogResult.OK)
//                {
//                    _expiryDates[userId] = datePicker.Value.ToUniversalTime(); // Convert to UTC for storage
//                    // Update grid to reflect local time
//                    dgvUsers.Rows[e.RowIndex].Cells["colExpiry"].Value = _expiryDates[userId].ToLocalTime().ToString("g");
//                    MessageBox.Show("Expiry date set!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private async void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var updatedPermissions = new List<(string UserId, bool HasAccess, DateTime ExpiredOn)>();
//                foreach (DataGridViewRow row in dgvUsers.Rows)
//                {
//                    var checkboxValue = row.Cells["colCheckbox"].Value;
//                    var isChecked = checkboxValue != null ? (bool)checkboxValue : false;
//                    var userId = row.Tag?.ToString();
//                    if (userId != null)
//                    {
//                        var expiryDate = _expiryDates.ContainsKey(userId) ? _expiryDates[userId] : DateTime.UtcNow.AddDays(30); // UTC
//                        updatedPermissions.Add((userId, isChecked, expiryDate));
//                    }
//                }

//                foreach (var (userId, hasAccess, expiredOn) in updatedPermissions)
//                {
//                    var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
//                    if (existingPermission == null && hasAccess)
//                    {
//                        var permission = new UserProjectPermissionDto
//                        {
//                            UserId = userId,
//                            ProjectId = _projectId,
//                            HasAccess = true,
//                            ExpiredOn = expiredOn // Already in UTC
//                        };
//                        await _apiHelper.AddProjectPermissionAsync(permission);
//                    }
//                    else if (existingPermission != null && (existingPermission.HasAccess != hasAccess || existingPermission.ExpiredOn != expiredOn))
//                    {
//                        existingPermission.HasAccess = hasAccess;
//                        existingPermission.ExpiredOn = expiredOn; // Already in UTC
//                        await _apiHelper.EditProjectPermissionAsync(existingPermission.Id, existingPermission);
//                    }
//                }

//                MessageBox.Show("Permissions updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error saving permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}















////using QACORDMS.Client.Helpers;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Windows.Forms;

////namespace QACORDMS.Client
////{
////    public partial class AddPermissionForm : Form
////    {
////        private readonly QACOAPIHelper _apiHelper;
////        private readonly Guid _projectId;
////        private List<UserProjectPermissionDto> _existingPermissions;

////        public AddPermissionForm(QACOAPIHelper apiHelper, Guid projectId)
////        {
////            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
////            _projectId = projectId;
////            InitializeComponent();
////            LoadUsersAndPermissionsAsync();
////        }

////        private async void LoadUsersAndPermissionsAsync()
////        {
////            try
////            {
////                var allUsers = await _apiHelper.GetUsersAsync();
////                var userRoleUsers = allUsers.Where(u => u.RoleName == "User").ToList();
////                _existingPermissions = await _apiHelper.GetPermissionsByProjectIdAsync(_projectId);

////                dgvUsers.Rows.Clear();
////                foreach (var user in userRoleUsers)
////                {
////                    var hasPermission = _existingPermissions.Any(p => p.UserId == user.Id && p.HasAccess);
////                    var index = dgvUsers.Rows.Add(hasPermission, user.Username, user.Email);
////                    dgvUsers.Rows[index].Tag = user.Id;
////                    dgvUsers.Rows[index].Cells["colCheckbox"].Value = hasPermission; // Ensure value set
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Error loading users/permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        private async void btnSave_Click(object sender, EventArgs e)
////        {
////            try
////            {
////                var updatedPermissions = new List<(string UserId, bool HasAccess)>();
////                foreach (DataGridViewRow row in dgvUsers.Rows)
////                {
////                    var checkboxValue = row.Cells["colCheckbox"].Value;
////                    var isChecked = checkboxValue != null ? (bool)checkboxValue : false;
////                    var userId = row.Tag?.ToString();
////                    if (userId != null)
////                    {
////                        updatedPermissions.Add((userId, isChecked));
////                    }
////                }

////                foreach (var (userId, hasAccess) in updatedPermissions)
////                {
////                    var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
////                    if (existingPermission == null && hasAccess)
////                    {
////                        var permission = new UserProjectPermissionDto
////                        {
////                            UserId = userId,
////                            ProjectId = _projectId,
////                            HasAccess = true
////                        };
////                        await _apiHelper.AddProjectPermissionAsync(permission);
////                    }
////                    else if (existingPermission != null && existingPermission.HasAccess != hasAccess)
////                    {
////                        existingPermission.HasAccess = hasAccess;
////                        await _apiHelper.EditProjectPermissionAsync(existingPermission.Id, existingPermission);
////                    }
////                }

////                MessageBox.Show("Permissions updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                this.Close();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Error saving permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        private async void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
////        {
////            if (e.ColumnIndex == dgvUsers.Columns["colDelete"].Index && e.RowIndex >= 0)
////            {
////                var userId = dgvUsers.Rows[e.RowIndex].Tag.ToString();
////                var permission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
////                if (permission != null)
////                {
////                    var result = MessageBox.Show($"Are you sure you want to remove permission for {dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value}?",
////                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
////                    if (result == DialogResult.Yes)
////                    {
////                        await _apiHelper.DeleteProjectPermissionAsync(permission.Id);
////                        dgvUsers.Rows.RemoveAt(e.RowIndex);
////                        _existingPermissions.Remove(permission);
////                        MessageBox.Show("Permission deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                    }
////                }   
////            }
////        }
////    }
////}