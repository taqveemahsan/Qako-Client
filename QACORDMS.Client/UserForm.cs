using QACORDMS.Client.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QACORDMS.Client
{
    public partial class UserForm : Form
    {
        private readonly QACOAPIHelper _apiHelper;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalUsers = 0;
        private string _searchQuery = "";

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

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _searchQuery = txtSearch.Text.Trim();
            _currentPage = 1; // Reset to first page on new search
            await LoadUsersAsync();
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadUsersAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;
            if (_currentPage < totalPages)
            {
                _currentPage++;
                await LoadUsersAsync();
            }
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var response = await _apiHelper.GetUsersAsync(_searchQuery, _currentPage, _pageSize);
                usersGridView.Rows.Clear();

                _totalUsers = response.TotalUsers;
                int totalPages = (_totalUsers + _pageSize - 1) / _pageSize;

                foreach (var user in response.Users)
                {
                    var fullName = $"{user.FirstName} {user.LastName}".Trim();
                    var index = usersGridView.Rows.Add(fullName, user.Email, user.Username, user.RoleName);
                    usersGridView.Rows[index].Tag = user.Id;
                }

                lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";
                btnPrevious.Enabled = _currentPage > 1;
                btnNext.Enabled = _currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void usersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == usersGridView.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var userId = (string)usersGridView.Rows[e.RowIndex].Tag;
                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // await _apiHelper.DeleteUserAsync(userId); // Uncomment if delete API exists
                    await LoadUsersAsync();
                }
            }
        }
    }
}














//using QACORDMS.Client.Helpers;
//using System;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace QACORDMS.Client
//{
//    public partial class UserForm : Form
//    {
//        private readonly QACOAPIHelper _apiHelper;

//        public UserForm(QACOAPIHelper apiHelper)
//        {
//            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
//            InitializeComponent();
//            LoadUsersAsync().ConfigureAwait(false);
//        }

//        private async void addUserButton_Click(object sender, EventArgs e)
//        {
//            var addUserForm = new AddUserForm(_apiHelper);
//            addUserForm.ShowDialog();
//            await LoadUsersAsync();
//        }

//        private async Task LoadUsersAsync()
//        {
//            var users = await _apiHelper.GetUsersAsync();
//            usersGridView.Rows.Clear();

//            foreach (var user in users)
//            {
//                var fullName = $"{user.FirstName} {user.LastName}".Trim();
//                var index = usersGridView.Rows.Add(fullName, user.Email, user.Username, user.RoleName);
//                usersGridView.Rows[index].Tag = user.Id;
//            }
//        }

//        private async void usersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.ColumnIndex == usersGridView.Columns["Delete"].Index && e.RowIndex >= 0)
//            {
//                var userId = usersGridView.Rows[e.RowIndex].Tag; // Guid ke bajaye string kyunki Identity Id string hai
//                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                if (result == DialogResult.Yes)
//                {
//                    //await _apiHelper.DeleteUserAsync(userId);
//                    await LoadUsersAsync();
//                }
//            }
//        }
//    }
//}