using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Dashboard
    {

        public Decimal EmployeeLeave { get; set; }

        public Decimal UsedEmployeeLeave { get; set; }

        public Decimal NoOfLeavesPerMonth { get; set; }

        public string  Month { get; set; }

        public decimal PaidTotalSalary { get; set; }

        public string FullName { get; set; }

    }

    public  class EmployeeDepartment
    {
        public string Department { get; set; }

        public int TotalEmployee { get; set; }
    }
}
