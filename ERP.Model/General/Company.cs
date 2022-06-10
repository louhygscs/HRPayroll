using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public  class Company
    {
        public Guid CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public string EmailAddress { get; set; }
        
        public Guid? CountryId { get; set; }

        public Guid? StateId { get; set; }

        public Guid? CategoryId { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string MobileNo { get; set; }

        public string PhoneNo { get; set; }

        public string HotLineNo { get; set; }

        public string FaxNo { get; set; }

        public string WebSite { get; set; }

        public string LicenseKey { get; set; }
        public string TINNo { get; set; }
        public string BusinessPermitNo { get; set; }

        public bool? IsKeyActive { get; set; }

        public string Remarks { get; set; }
    }
}
