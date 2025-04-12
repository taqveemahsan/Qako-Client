using QACORDMS.Client.Helpers;
using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace QACORDMS.Client
{
    public partial class Login : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private bool isPasswordVisible = false;

        public Login(QACOAPIHelper apiHelper)
        {
            InitializeComponent();
            _apiHelper = apiHelper;
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
                    string token = user.Token;
                    if (string.IsNullOrEmpty(token))
                    {
                        MessageBox.Show("Token not received from API.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SessionHelper.CurrentUser = user;

                    // If "Remember Me" is checked, save the token
                    if (chkRememberMe.Checked)
                    {
                        SaveToken(token, username);
                    }
                    else
                    {
                        // If "Remember Me" is not checked, delete any existing token
                        DeleteToken();
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var forgotPasswordForm = new ForgotPasswordEmailForm(_apiHelper))
            {
                forgotPasswordForm.ShowDialog();
            }
        }

        private void SaveToken(string token, string username)
        {
            var tokenData = new TokenData
            {
                Token = token,
                Username = username,
                SavedAt = DateTime.Now
            };

            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
            File.WriteAllText(tokenPath, JsonConvert.SerializeObject(tokenData));
        }

        private void DeleteToken()
        {
            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
            if (File.Exists(tokenPath))
            {
                File.Delete(tokenPath);
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
                btnTogglePassword.Text = "👁️";
            }
            else
            {
                txtPassword.PasswordChar = '*'; // Hide password
                btnTogglePassword.Text = "👁️";
            }
        }
    }
}










//using QACORDMS.Client.Helpers;
//using System;
//using System.Windows.Forms;
//using System.IO;
//using Newtonsoft.Json;

//namespace QACORDMS.Client
//{
//    public partial class Login : Form
//    {
//        private readonly QACOAPIHelper _apiHelper;
//        private bool isPasswordVisible = false;

//        public Login(QACOAPIHelper apiHelper)
//        {
//            InitializeComponent();
//            _apiHelper = apiHelper;
//        }

//        private async void btnLogin_Click(object sender, EventArgs e)
//        {
//            var username = txtUsername.Text;
//            var password = txtPassword.Text;

//            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
//            {
//                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                var user = await _apiHelper.LoginAsync(username, password);
//                if (user != null)
//                {
//                    string token = user.Token;
//                    if (string.IsNullOrEmpty(token))
//                    {
//                        MessageBox.Show("Token not received from API.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }

//                    SessionHelper.CurrentUser = user;

//                    // If "Remember Me" is checked, save the token
//                    if (chkRememberMe.Checked)
//                    {
//                        SaveToken(token, username);
//                    }
//                    else
//                    {
//                        // If "Remember Me" is not checked, delete any existing token
//                        DeleteToken();
//                    }

//                    this.DialogResult = DialogResult.OK; // Set DialogResult to OK
//                    this.Close(); // Close the Login form
//                }
//                else
//                {
//                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error: {ex.Message}", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnReset_Click(object sender, EventArgs e)
//        {
//            // Backend logic will be added later
//            // For now, just a placeholder
//        }

//        private void SaveToken(string token, string username)
//        {
//            var tokenData = new TokenData
//            {
//                Token = token,
//                Username = username,
//                SavedAt = DateTime.Now
//            };

//            // Save to a JSON file
//            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
//            File.WriteAllText(tokenPath, JsonConvert.SerializeObject(tokenData));
//        }

//        private void DeleteToken()
//        {
//            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
//            if (File.Exists(tokenPath))
//            {
//                File.Delete(tokenPath);
//            }
//        }

//        private void Login_Load(object sender, EventArgs e)
//        {
//            pictureBoxLogo.Left = (panelContainer.Width - pictureBoxLogo.Width) / 2;
//        }

//        private void btnTogglePassword_Click(object sender, EventArgs e)
//        {
//            isPasswordVisible = !isPasswordVisible;
//            if (isPasswordVisible)
//            {
//                txtPassword.PasswordChar = '\0'; // Show password
//                btnTogglePassword.Text = "👁️";
//            }
//            else
//            {
//                txtPassword.PasswordChar = '*'; // Hide password
//                btnTogglePassword.Text = "👁️";
//            }
//        }
//    }
//}
