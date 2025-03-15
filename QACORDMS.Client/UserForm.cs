using QACORDMS.Client.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class UserForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;

        public UserForm(QACOAPIHelper apiHelper)
        {
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
            InitializeComponent();
            LoadUsersAsync().ConfigureAwait(false);
        }

        private async void addUserButton_Click(object sender, EventArgs e)
        {
            var addUserForm = new AddUserForm(_apiHelper);
            addUserForm.ShowDialog();
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            var users = await _apiHelper.GetUsersAsync();
            usersGridView.Rows.Clear();

            foreach (var user in users)
            {
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                var index = usersGridView.Rows.Add(fullName, user.Email, user.Username, user.RoleName);
                usersGridView.Rows[index].Tag = user.Id;
            }
        }

        private async void usersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == usersGridView.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var userId = (string)usersGridView.Rows[e.RowIndex].Tag; // Guid ke bajaye string kyunki Identity Id string hai
                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //await _apiHelper.DeleteUserAsync(userId);
                    await LoadUsersAsync();
                }
            }
        }
    }
}