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

    public class ClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CompanyType { get; set; }
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
        public string Id { get; set; } // This will be Guid in the database, string in Google Drive
        public string Name { get; set; }
        public string MimeType { get; set; }
        public long? Size { get; set; }
        public long SizeInKb => (long)(Size == null || Size == 0 ? 0 : Size / 1024);
        public string ParentId { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string FileExtension { get; set; }

        // Database-specific fields (nullable to handle Google Drive data)
        public Guid GoogleId { get; set; } // Maps to the Google Drive item's Id
        public bool IsFolder { get; set; } // Already present in your database entity
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }


    }
    public class GoogleDriveItemV1
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public long? Size { get; set; }
        public long SizeInKb => (long)(Size == null || Size == 0 ? 0 : Size / 1024);
        public string ParentId { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string FileExtension { get; set; }
        

    }

    public class APIUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public static implicit operator APIUserModel(User v)
        {
            throw new NotImplementedException();
        }
    }
}
