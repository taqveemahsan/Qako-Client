using QACORDMS.Client.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class AddClientForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;

        public AddClientForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
            InitializeComponent();
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Name is required. Please enter a client name to proceed.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var newClient = new Helpers.Client
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text,
                Address = addressTextBox.Text,
                CompanyType = (CompanyType)companyTypeComboBox.SelectedIndex
            };

            try
            {
                var response = await _apiHelper.CreateClientAsync(newClient);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"🎉 Client '{newClient.Name}' added successfully! You're all set! 🚀",
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
                            // Parse the JSON error response
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

                    // Customize the message for "client already exists" case
                    if (errorMessage.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                    {
                        errorMessage = $"⚠️ A client with the name '{newClient.Name}' already exists. Please choose a different name and try again.";
                    }
                    else
                    {
                        errorMessage = $"Failed to add client: {errorMessage}";
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


        private void nameTextBox_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, nameTextBox.ClientRectangle,
                Color.FromArgb(200, 200, 200), ButtonBorderStyle.Solid);
        }

        private void AddRoundedCorners(Control control, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }
    }
}
