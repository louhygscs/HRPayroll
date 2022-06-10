using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class FinancialYear
    {
        public Guid FinancialYearId { get; set; }

        public string FinancialYearText { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; }

        public bool? IsLocked { get; set; }
    }
}
