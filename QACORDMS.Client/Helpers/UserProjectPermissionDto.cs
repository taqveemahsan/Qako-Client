using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers
{
    public class UserProjectPermissionDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProjectId { get; set; }
        public bool HasAccess { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
