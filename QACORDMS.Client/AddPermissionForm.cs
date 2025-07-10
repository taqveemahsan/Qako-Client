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

                Console.WriteLine($"Loaded {_existingPermissions.Count} existing permissions for project {_projectId}");
                foreach (var perm in _existingPermissions)
                {
                    Console.WriteLine($"Permission: ID={perm.Id}, UserId={perm.UserId}, HasAccess={perm.HasAccess}, ExpiredOn={perm.ExpiredOn}");
                }

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
                        expiryDisplay = expiryDate.ToString("dd/MM/yyyy HH:mm"); // Show date and time
                        _expiryDates[user.Id] = existingPermission.ExpiredOn;
                    }
                    else
                    {
                        expiryDisplay = "Set Expiry";
                    }

                    var index = dgvUsers.Rows.Add(hasPermission, user.Username, user.Email, expiryDisplay);
                    dgvUsers.Rows[index].Tag = user.Id;
                    dgvUsers.Rows[index].Cells["colCheckbox"].Value = hasPermission;

                    // Style the expiry cell based on whether date is set
                    var expiryCell = dgvUsers.Rows[index].Cells["colExpiry"] as DataGridViewButtonCell;
                    if (_expiryDates.ContainsKey(user.Id))
                    {
                        expiryCell.Style.BackColor = System.Drawing.Color.FromArgb(40, 167, 69); // Green for set dates
                        expiryCell.Style.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        expiryCell.Style.BackColor = System.Drawing.Color.FromArgb(108, 117, 125); // Gray for unset
                        expiryCell.Style.ForeColor = System.Drawing.Color.White;
                    }
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

            // Handle Expiry
            if (e.ColumnIndex == dgvUsers.Columns["colExpiry"].Index)
            {
                var userId = dgvUsers.Rows[e.RowIndex].Tag.ToString();

                // Create a more user-friendly dialog
                var dialog = new Form
                {
                    Text = "Set Permission Expiry Date",
                    Size = new System.Drawing.Size(350, 200),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var lblInfo = new Label
                {
                    Text = "Select expiry date for this permission:",
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(300, 20),
                    Font = new System.Drawing.Font("Segoe UI", 9F)
                };

                var datePicker = new DateTimePicker
                {
                    Value = _expiryDates.ContainsKey(userId) ? _expiryDates[userId].ToLocalTime() : DateTime.Now.AddDays(30),
                    MinDate = DateTime.Now,
                    Location = new System.Drawing.Point(20, 50),
                    Size = new System.Drawing.Size(280, 25),
                    Format = DateTimePickerFormat.Long
                };

                var lblCurrent = new Label
                {
                    Text = _expiryDates.ContainsKey(userId)
                        ? $"Current: {_expiryDates[userId].ToLocalTime():dd/MM/yyyy HH:mm}"
                        : "Current: Not set",
                    Location = new System.Drawing.Point(20, 85),
                    Size = new System.Drawing.Size(280, 20),
                    Font = new System.Drawing.Font("Segoe UI", 8F),
                    ForeColor = System.Drawing.Color.Gray
                };

                var btnOk = new Button
                {
                    Text = "Set Date",
                    Location = new System.Drawing.Point(150, 120),
                    Size = new System.Drawing.Size(80, 30),
                    DialogResult = DialogResult.OK,
                    BackColor = System.Drawing.Color.FromArgb(0, 102, 204),
                    ForeColor = System.Drawing.Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new System.Drawing.Point(240, 120),
                    Size = new System.Drawing.Size(80, 30),
                    DialogResult = DialogResult.Cancel,
                    BackColor = System.Drawing.Color.Gray,
                    ForeColor = System.Drawing.Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                dialog.Controls.AddRange(new Control[] { lblInfo, datePicker, lblCurrent, btnOk, btnCancel });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var newExpiryDate = datePicker.Value.ToUniversalTime();
                        var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);

                        if (existingPermission != null)
                        {
                            // Update existing permission via API
                            existingPermission.ExpiredOn = newExpiryDate;
                            var updateResult = await _apiHelper.EditProjectPermissionAsync(existingPermission.Id, existingPermission);

                            if (updateResult)
                            {
                                // Update local storage and UI
                                _expiryDates[userId] = newExpiryDate;
                                dgvUsers.Rows[e.RowIndex].Cells["colExpiry"].Value = newExpiryDate.ToLocalTime().ToString("dd/MM/yyyy HH:mm");

                                // Update button styling to show it's set
                                var expiryCell = dgvUsers.Rows[e.RowIndex].Cells["colExpiry"] as DataGridViewButtonCell;
                                expiryCell.Style.BackColor = System.Drawing.Color.FromArgb(40, 167, 69); // Green
                                expiryCell.Style.ForeColor = System.Drawing.Color.White;

                                MessageBox.Show("Expiry date updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to update expiry date. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // If no existing permission, we need to create one first
                            var newPermission = new UserProjectPermissionDto
                            {
                                UserId = userId,
                                ProjectId = _projectId,
                                HasAccess = true,
                                ExpiredOn = newExpiryDate
                            };

                            var createResult = await _apiHelper.AddProjectPermissionAsync(newPermission);
                            if (createResult)
                            {
                                // Reload permissions to get the new permission ID
                                _existingPermissions = await _apiHelper.GetPermissionsByProjectIdAsync(_projectId);
                                _expiryDates[userId] = newExpiryDate;

                                // Update UI
                                dgvUsers.Rows[e.RowIndex].Cells["colCheckbox"].Value = true;
                                dgvUsers.Rows[e.RowIndex].Cells["colExpiry"].Value = newExpiryDate.ToLocalTime().ToString("dd/MM/yyyy HH:mm");

                                var expiryCell = dgvUsers.Rows[e.RowIndex].Cells["colExpiry"] as DataGridViewButtonCell;
                                expiryCell.Style.BackColor = System.Drawing.Color.FromArgb(40, 167, 69); // Green
                                expiryCell.Style.ForeColor = System.Drawing.Color.White;

                                MessageBox.Show("Permission created with expiry date successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to create permission. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating expiry date: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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