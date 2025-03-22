using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers
{
    public enum CompanyType
    {
        PrivateLable,
        PublicComp,
    }
    public enum ProjectType
    {
        Tax = 1,
        Audit = 2,
        Corporate = 3,
        Advisory=4,
        ERP=5,
        Bookkeeping=6,
        Other=7
    }
    public enum UserRole
    {
        Partner = 1,
        User = 2
    }
}
