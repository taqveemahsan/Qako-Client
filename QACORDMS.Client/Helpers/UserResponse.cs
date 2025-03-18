using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers
{
    public class UserResponse
    {
        public int TotalUsers { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<User> Users { get; set; }
    }
}
