using QACORDMS.Client.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class UserForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalUsers = 0;
        private string _searchQuery = "";

        public UserForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            InitializeComponent();
            LoadUsersAsync().ConfigureAwait(false);

            this.Resize += UserForm_Resize; // Subscribe to the Resize event

            txtSearch.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSearch_Click(sender, e); // Trigger the search button click event
                    e.SuppressKeyPress = true; // Prevent the Enter key from making a beep sound
                }
            };
        }

        private void UserForm_Resize(object sender, EventArgs e)
        {
            // Adjust the search bar and button
            int margin = 40;
            int searchButtonWidth = btnSearch.Width;
            int searchButtonHeight = btnSearch.Height;

            // Update search bar width
            txtSearch.Width = this.ClientSize.Width - margin * 2 - searchButtonWidth - 10; // 10 for spacing between search bar and button

            // Reposition search button
            btnSearch.Location = new Point(txtSearch.Right + 10, txtSearch.Top);

            // Ensure grid view height leaves space for the buttons at the bottom
            int bottomControlsHeight = addUserButton.Height + 20; // Space for buttons and margin
            usersGridView.Height = this.ClientSize.Height - usersGridView.Top - bottomControlsHeight - margin;

            // Reposition bottom buttons
            addUserButton.Location = new Point(this.ClientSize.Width - addUserButton.Width - margin, this.ClientSize.Height - bottomControlsHeight);
            btnPrevious.Location = new Point(margin, this.ClientSize.Height - bottomControlsHeight);
            btnNext.Location = new Point(btnPrevious.Right + 10, this.ClientSize.Height - bottomControlsHeight);
            lblPageInfo.Location = new Point(btnNext.Right + 10, this.ClientSize.Height - bottomControlsHeight);
        }

        private async void addUserButton_Click(object sender, EventArgs e)
        {
            var addUserForm = new AddUserForm(_apiHelper);
            addUserForm.ShowDialog();
            await LoadUsersAsync();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _searchQuery = txtSearch.Text.Trim();
            _currentPage = 1; // Reset to first page on new search
            await LoadUsersAsync();
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadUsersAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;
            if (_currentPage < totalPages)
            {
                _currentPage++;
                await LoadUsersAsync();
            }
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var response = await _apiHelper.GetUsersAsync(_searchQuery, _currentPage, _pageSize);
                usersGridView.Rows.Clear();

                _totalUsers = response.TotalUsers;
                int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;

                foreach (var user in response.Users)
                {
                    var fullName = $"{user.FirstName} {user.LastName}".Trim();
                    // Join the RoleNames list into a comma-separated string
                    var roles = user.RoleNames != null && user.RoleNames.Any()
                        ? string.Join(", ", user.RoleNames)
                        : "None";
                    var index = usersGridView.Rows.Add(user.Email, user.Username, roles);
                    usersGridView.Rows[index].Tag = user.Id;
                }

                lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";
                btnPrevious.Enabled = _currentPage > 1;
                btnNext.Enabled = _currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void usersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Handle Change Password
            if (e.ColumnIndex == usersGridView.Columns["ChangePassword"].Index)
            {
                var userId = usersGridView.Rows[e.RowIndex].Tag.ToString();
                var username = usersGridView.Rows[e.RowIndex].Cells[0].Value.ToString(); // Assuming username is in first column

                ShowChangePasswordDialog(userId, username);
            }
            // Handle Delete
            else if (e.ColumnIndex == usersGridView.Columns["Delete"].Index)
            {
                var userId = usersGridView.Rows[e.RowIndex].Tag.ToString();
                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Call the API helper to delete the user
                        await _apiHelper.DeleteUserAsync(Guid.Parse(userId));
                        MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    await LoadUsersAsync();
                }
            }
        }

        private async void ShowChangePasswordDialog(string userId, string username)
        {
            // Create a more user-friendly dialog
            var dialog = new Form
            {
                Text = "Change Password",
                Size = new System.Drawing.Size(400, 330),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblInfo = new Label
            {
                Text = $"Change password for: {username}",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(350, 20),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(0, 102, 204)
            };

            var lblCurrentPassword = new Label
            {
                Text = "Current Password:",
                Location = new System.Drawing.Point(20, 55),
                Size = new System.Drawing.Size(120, 20),
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            var txtCurrentPassword = new TextBox
            {
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(340, 25),
                UseSystemPasswordChar = true,
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            var lblNewPassword = new Label
            {
                Text = "New Password:",
                Location = new System.Drawing.Point(20, 115),
                Size = new System.Drawing.Size(120, 20),
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            var txtNewPassword = new TextBox
            {
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(340, 25),
                UseSystemPasswordChar = true,
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            var lblConfirmPassword = new Label
            {
                Text = "Confirm New Password:",
                Location = new System.Drawing.Point(20, 175),
                Size = new System.Drawing.Size(150, 20),
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            var txtConfirmPassword = new TextBox
            {
                Location = new System.Drawing.Point(20, 200),
                Size = new System.Drawing.Size(340, 25),
                UseSystemPasswordChar = true,
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            var btnChange = new Button
            {
                Text = "Change Password",
                Location = new System.Drawing.Point(180, 235),
                Size = new System.Drawing.Size(120, 30),
                DialogResult = DialogResult.OK,
                BackColor = System.Drawing.Color.FromArgb(0, 102, 204),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(310, 235),
                Size = new System.Drawing.Size(70, 30),
                DialogResult = DialogResult.Cancel,
                BackColor = System.Drawing.Color.Gray,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };

            dialog.Controls.AddRange(new Control[] {
                lblInfo, lblCurrentPassword, txtCurrentPassword,
                lblNewPassword, txtNewPassword, lblConfirmPassword,
                txtConfirmPassword, btnChange, btnCancel
            });

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
                {
                    MessageBox.Show("Please enter the current password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    MessageBox.Show("Please enter a new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("New password and confirmation do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text.Length < 6)
                {
                    MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var changePasswordRequest = new ChangePasswordRequest
                    {
                        UserId = userId,
                        CurrentPassword = txtCurrentPassword.Text,
                        NewPassword = txtNewPassword.Text
                    };

                    var result = await _apiHelper.ChangePasswordAsync(changePasswordRequest);
                    if (result)
                    {
                        MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to change password. Please check the current password and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error changing password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
