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
                MessageBox.Show("Name is required.");
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

            await _apiHelper.CreateClientAsync(newClient);
            MessageBox.Show("Client added successfully!");
            this.Close();
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
