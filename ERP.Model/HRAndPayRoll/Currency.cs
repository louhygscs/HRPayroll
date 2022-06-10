using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Currency
    {
        public int CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public bool? IsActive { get; set; }
    }
}
