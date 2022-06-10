using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Country
    {
        public Guid CountryID { get; set; }

        public string CountryName { get; set; }
        public string Code { get; set; }
    }
}
