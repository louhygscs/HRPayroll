using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Allowance
    {
        public Guid AllowanceID { get; set; }

        public string AllowanceName { get; set; }

        public Boolean IsConsider { get; set; }

        public int SortNumber { get; set; }

        public Decimal? Amount { get; set; }

        public Decimal? PaidAmount { get; set; }

        public double Percentage { get; set; }
    }
}
