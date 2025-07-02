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
        PrivateLable = 0,
        PublicComp = 1,
        ForeignCompanies = 2,
        PartnershipFirms = 3,
        NonProfitOrganizations = 4,
        NBFC = 5,
        PICS = 6,
        ProvidentGratuityFunds = 7,
        IndividualsSoleProprietors = 8,
        Others = 9
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
