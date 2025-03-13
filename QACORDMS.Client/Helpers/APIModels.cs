using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public CompanyType CompanyType { get; set; }
    }

    // ClientProject Model
    public class ClientProject
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string ProjectName { get; set; }
        public string GoogleDriveFolderId { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    // GoogleDriveItem Model
    public class GoogleDriveItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string ParentId { get; set; }
        public string ThumbnailLink { get; set; }
        
    }

    public class APIUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
