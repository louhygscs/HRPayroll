using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class SessionDetail
    {
        public Guid UserID { get; set; }

        public Guid FinancialYearId { get; set; }

        public Guid RoleId { get; set; }

        public String Email { get; set; }

        public String FullName { get; set; }

        public String PhotoName { get; set; }

        public Guid EmployeeId { get; set; }

        public int EmployeeNo { get; set; }

        public string EmployeeDesignation { get; set; }

        public string EmployeeShift { get; set; }
    }
}
