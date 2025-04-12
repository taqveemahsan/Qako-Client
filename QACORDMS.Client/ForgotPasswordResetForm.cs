using QACORDMS.Client.Helpers;
using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace QACORDMS.Client
{
    public partial class ForgotPasswordResetForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private readonly string _email;

        public ForgotPasswordResetForm(QACOAPIHelper apiHelper, string email)
        {
            InitializeComponent();
            _apiHelper = apiHelper;
            _email = email;

            // Attach event handlers
            btnReset.Click += btnReset_Click;
            btnCancel.Click += btnCancel_Click;
            lnkResendOtp.LinkClicked += lnkResendOtp_LinkClicked;
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            var otp = txtOtp.Text.Trim();
            var newPassword = txtNewPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(otp) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var resetResult = await _apiHelper.ResetPasswordAsync(_email, otp, newPassword, confirmPassword);
                if (!resetResult.IsSuccessStatusCode)
                {
                    var resetJson = JObject.Parse(await resetResult.Content.ReadAsStringAsync());
                    MessageBox.Show(resetJson["Message"]?.ToString() ?? "Error resetting password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Password reset successfully. Please login with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void lnkResendOtp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var sendOtpResult = await _apiHelper.SendOtpAsync(_email);
            if (sendOtpResult.IsSuccessStatusCode)
            {
                MessageBox.Show("OTP resent to your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var sendOtpJson = JObject.Parse(await sendOtpResult.Content.ReadAsStringAsync());
                MessageBox.Show(sendOtpJson["Message"]?.ToString() ?? "Error resending OTP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}









//using Newtonsoft.Json.Linq;
//using QACORDMS.Client.Helpers;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace QACORDMS.Client
//{
//    public partial class ForgotPasswordResetForm: Form
//    {
//        private readonly QACOAPIHelper _apiHelper;
//        private readonly string _email;

//        public ForgotPasswordResetForm(QACOAPIHelper apiHelper, string email)
//        {
//            InitializeComponent();
//            _apiHelper = apiHelper;
//            _email = email;

//            // Attach event handlers
//            btnReset.Click += btnReset_Click;
//            btnCancel.Click += btnCancel_Click;
//        }

//        private async void btnReset_Click(object sender, EventArgs e)
//        {
//            var otp = txtOtp.Text.Trim();
//            var newPassword = txtNewPassword.Text;
//            var confirmPassword = txtConfirmPassword.Text;

//            if (string.IsNullOrWhiteSpace(otp) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
//            {
//                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (newPassword != confirmPassword)
//            {
//                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                var resetResult = await _apiHelper.ResetPasswordAsync(_email, otp, newPassword, confirmPassword);
//                if (!resetResult.IsSuccessStatusCode)
//                {
//                    var resetJson = JObject.Parse(await resetResult.Content.ReadAsStringAsync());
//                    MessageBox.Show(resetJson["Message"]?.ToString() ?? "Error resetting password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }

//                MessageBox.Show("Password reset successfully. Please login with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                this.DialogResult = DialogResult.OK;
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }
//    }
//}
