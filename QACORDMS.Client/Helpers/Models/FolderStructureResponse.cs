using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers.Models
{
    public class FolderStructureResponse
    {
        public List<FolderStructureDto> FolderStructures { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class FolderStructureDto
    {
        public string FolderName { get; set; }
        public string ParentFolderId { get; set; }
        public string GoogleDriveFolderId { get; set; }
        public string ClientName { get; set; } // Client ka naam
        public string ClientType { get; set; } // Client type (PrivateLabel ya PublicLabel)
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
