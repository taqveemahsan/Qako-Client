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
                var allUsers = await _apiHelper.GetUsersAsync();
                var userRoleUsers = allUsers.Where(u => u.RoleName == "User").ToList();
                _existingPermissions = await _apiHelper.GetPermissionsByProjectIdAsync(_projectId);

                dgvUsers.Rows.Clear();
                foreach (var user in userRoleUsers)
                {
                    var hasPermission = _existingPermissions.Any(p => p.UserId == user.Id && p.HasAccess);
                    var index = dgvUsers.Rows.Add(hasPermission, user.Username, user.Email);
                    dgvUsers.Rows[index].Tag = user.Id;
                    dgvUsers.Rows[index].Cells["colCheckbox"].Value = hasPermission; // Ensure value set
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users/permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var updatedPermissions = new List<(string UserId, bool HasAccess)>();
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    var checkboxValue = row.Cells["colCheckbox"].Value;
                    var isChecked = checkboxValue != null ? (bool)checkboxValue : false;
                    var userId = row.Tag?.ToString();
                    if (userId != null)
                    {
                        updatedPermissions.Add((userId, isChecked));
                    }
                }

                foreach (var (userId, hasAccess) in updatedPermissions)
                {
                    var existingPermission = _existingPermissions.FirstOrDefault(p => p.UserId == userId);
                    if (existingPermission == null && hasAccess)
                    {
                        var permission = new UserProjectPermissionDto
                        {
                            UserId = userId,
                            ProjectId = _projectId,
                            HasAccess = true
                        };
                        await _apiHelper.AddProjectPermissionAsync(permission);
                    }
                    else if (existingPermission != null && existingPermission.HasAccess != hasAccess)
                    {
                        existingPermission.HasAccess = hasAccess;
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

        private async void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvUsers.Columns["colDelete"].Index && e.RowIndex >= 0)
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
                        MessageBox.Show("Permission deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}