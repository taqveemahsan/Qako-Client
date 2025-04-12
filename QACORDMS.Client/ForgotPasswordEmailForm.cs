using Newtonsoft.Json.Linq;
using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class ForgotPasswordEmailForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;

        public ForgotPasswordEmailForm(QACOAPIHelper apiHelper)
        {
            InitializeComponent();
            _apiHelper = apiHelper;

            // Attach event handlers
            btnSubmit.Click += btnSubmit_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check if email exists
                var checkEmailResult = await _apiHelper.CheckEmailAsync(email);
                if (!checkEmailResult.IsSuccessStatusCode)
                {
                    var checkEmailJson = JObject.Parse(await checkEmailResult.Content.ReadAsStringAsync());
                    MessageBox.Show(checkEmailJson["Message"]?.ToString() ?? "Error checking email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Send OTP
                var sendOtpResult = await _apiHelper.SendOtpAsync(email);
                if (!sendOtpResult.IsSuccessStatusCode)
                {
                    var sendOtpJson = JObject.Parse(await sendOtpResult.Content.ReadAsStringAsync());
                    MessageBox.Show(sendOtpJson["Message"]?.ToString() ?? "Error sending OTP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("OTP sent to your email. Please check your inbox (and spam/junk folder).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the OTP and password reset form
                using (var resetForm = new ForgotPasswordResetForm(_apiHelper, email))
                {
                    resetForm.ShowDialog();
                }

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
    }
}
