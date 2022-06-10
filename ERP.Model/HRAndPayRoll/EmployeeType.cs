using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class EmployeeType
    {
        public Guid EmployeeTypeID { get; set; }

        public string EmployeeTypeName { get; set; }

        public decimal NoOfLeavePerMonth { get; set; }
    }
}
