using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class AddUserForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;

        public AddUserForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            InitializeComponent();
            SetupRolesCheckedListBox();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            // Attach the Paint event handler for panelContainer
            panelContainer.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panelContainer.ClientRectangle,
                    Color.FromArgb(0, 102, 204), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 102, 204), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 102, 204), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(0, 102, 204), 1, ButtonBorderStyle.Solid);
            };
        }
        private void SetupRolesCheckedListBox()
        {
            var roles = new[] { "Partner", "User", "Tax Manager", "Audit Manager", "Corporate Manager", "Advisory Manager", "ERP Manager", "Bookkeeping Manager", "Other Manager" };
            clbRoles.DataSource = roles;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form when Cancel is clicked
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected roles from CheckedListBox
                var selectedRoles = clbRoles.CheckedItems.Cast<string>().ToList();

                // Validate that at least one role is selected
                if (!selectedRoles.Any())
                {
                    MessageBox.Show("Please select at least one role to proceed.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string rawUsername = txtUsername.Text;
                string cleanUsername = Regex.Replace(rawUsername, @"[^a-zA-Z0-9_]", "");

                var user = new Helpers.User
                {
                    FirstName = "",
                    LastName = "",
                    Username = cleanUsername,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    RoleNames = selectedRoles
                };

                // Validate required fields
                if (string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.Username) ||
                    string.IsNullOrWhiteSpace(user.Password))
                {
                    MessageBox.Show("Username, Email, and Password are required. Please fill in all fields.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Password validation using regex
                string passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*(),.?\:{}|<>]).{1,}$";
                if (!Regex.IsMatch(user.Password, passwordPattern))
                {
                    MessageBox.Show("Password must contain:\n- At least 1 uppercase letter\n- At least 1 lowercase letter\n- At least 1 number\n- At least 1 special character (e.g., !@#$%^&*())",
                        "Invalid Password",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var response = await _apiHelper.RegisterUserAsync(user);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"🎉 User '{user.Username}' registered successfully! Welcome aboard! 🚀",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    // Try to read error details from the response
                    string errorMessage = response.ReasonPhrase;
                    try
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(errorContent))
                        {
                            using var doc = JsonDocument.Parse(errorContent);
                            var root = doc.RootElement;
                            if (root.TryGetProperty("message", out var messageElement))
                            {
                                errorMessage = messageElement.GetString();
                            }
                            else
                            {
                                errorMessage = $"API Error: {errorContent}";
                            }
                        }
                    }
                    catch (JsonException)
                    {
                        errorMessage = $"API Error (invalid response format): {errorMessage}";
                    }
                    catch
                    {
                        // Fallback to ReasonPhrase if content reading fails
                    }

                    if (errorMessage.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                    {
                        errorMessage = $"⚠️ A user with the username '{user.Username}' or email '{user.Email}' already exists. Please use a different username or email.";
                    }
                    else
                    {
                        errorMessage = $"Failed to register user: {errorMessage}";
                    }

                    MessageBox.Show(errorMessage,
                        "Oops! Something Went Wrong",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}. Please contact support if this persists.",
                    "Critical Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}












//using QACORDMS.Client.Helpers;
//using System;
//using System.Windows.Forms;

//namespace QACORDMS.Client
//{
//    public partial class AddUserForm : Form
//    {
//        private readonly QACOAPIHelper _apiHelper;

//        public AddUserForm(QACOAPIHelper apiHelper)
//        {
//            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
//            InitializeComponent();
//            SetupRoleComboBox();
//        }

//        private void SetupRoleComboBox()
//        {
//            var roles = new[] { "Partner", "User", "TaxManager", "AuditManager" };
//            cmbRole.DataSource = roles;
//            cmbRole.SelectedIndex = 0;
//        }


//        private async void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var user = new Helpers.User
//                {
//                    FirstName = txtFirstName.Text,
//                    LastName = txtLastName.Text,
//                    Username = txtUsername.Text,
//                    Email = txtEmail.Text,
//                    Password = txtPassword.Text,
//                    RoleName = cmbRole.SelectedItem.ToString()
//                };

//                if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.Email) ||
//                    string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
//                {
//                    MessageBox.Show("First Name, Username, Email, and Password are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                bool success = await _apiHelper.RegisterUserAsync(user);
//                if (success)
//                {
//                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    this.Close();
//                }
//                else
//                {
//                    MessageBox.Show("Failed to register user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}