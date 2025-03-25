using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void SetupRolesCheckedListBox()
        {
            var roles = new[] { "Partner", "User", "TaxManager", "AuditManager" };
            clbRoles.DataSource = roles;
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
                    MessageBox.Show("Please select at least one role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = new Helpers.User
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Username = txtUsername.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    RoleNames = selectedRoles // Changed from RoleName to RoleNames
                };

                if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                {
                    MessageBox.Show("First Name, Username, Email, and Password are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = await _apiHelper.RegisterUserAsync(user);
                if (success)
                {
                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to register user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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