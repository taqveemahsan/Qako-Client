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
    }
}
