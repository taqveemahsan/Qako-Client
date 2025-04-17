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
                    MessageBox.Show("Please select at least one role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = new Helpers.User
                {
                    FirstName = "",
                    LastName = "",
                    Username = txtUsername.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    RoleNames = selectedRoles // Changed from RoleName to RoleNames
                };

                if (string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                {
                    MessageBox.Show("Username, Email, and Password are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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