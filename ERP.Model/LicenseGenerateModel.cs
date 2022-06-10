using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class LicenseGenerateModel
    {
        public string Email { get; set; }

        public string Key { get; set; }

        public bool IsUsed { get; set; }

        public Guid LicenseKeyID { get; set; }

    }
}
