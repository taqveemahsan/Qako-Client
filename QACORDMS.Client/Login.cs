using QACORDMS.Client.Helpers;
using System;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class Login : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private bool isPasswordVisible = false;

        public Login()
        {
            InitializeComponent();
            _apiHelper = new QACOAPIHelper(new HttpClient());
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = await _apiHelper.LoginAsync(username, password);
                if (user != null)
                {
                    SessionHelper.CurrentUser = user;
                    MainForm mainForm = new MainForm(_apiHelper, user.Role);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            pictureBoxLogo.Left = (panelContainer.Width - pictureBoxLogo.Width) / 2;
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            if (isPasswordVisible)
            {
                txtPassword.PasswordChar = '\0'; // Show password
                btnTogglePassword.Text = "👁️"; // You can change this to another icon if needed
            }
            else
            {
                txtPassword.PasswordChar = '*'; // Hide password
                btnTogglePassword.Text = "👁️"; // You can change this to another icon if needed
            }
        }
    }
}