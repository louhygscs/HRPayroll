using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class EmployeeSalarys
    {
        public Guid EmployeeSalaryID { get; set; }

        public Guid EmployeeId { get; set; }

        public String Department { get; set; }

        public String FullName { get; set; }

        public DateTime JoinDate { get; set; }

        public int EmployeeNo { get; set; }

        public decimal Basic { get; set; }

        public decimal TotalEarning { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal TotalSalary { get; set; }

        public List<Allowance> ListAllowance { get; set; }

        public List<Deduction> ListDeduction { get; set; }

        public Boolean IsLeave { get; set; }

        public int IsMonthlySalary { get; set; }
    }
}
