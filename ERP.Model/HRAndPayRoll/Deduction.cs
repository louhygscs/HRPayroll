using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Deduction
    {
        public Guid DeductionID { get; set; }

        public string DeductionName { get; set; }

        public Boolean IsConsider { get; set; }

        public int SortNumber { get; set; }

        public Decimal? Amount { get; set; }

        public Decimal? PaidAmount { get; set; }
    }
}
