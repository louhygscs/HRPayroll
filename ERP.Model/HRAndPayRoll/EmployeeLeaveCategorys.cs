using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Serializable()]
    public class EmployeeLeaveCategorys
    {
        public Guid EmployeeLeaveCategoryMapID { get; set; }

        public Guid EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int EmployeeNo { get; set; }

        public Guid LeaveCategoryId { get; set; }

        public string LeaveCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalDay { get; set; }

        public Boolean IsFirstHalfDay { get; set; }

        public Boolean IsLastHalfDay { get; set; }

        public string Reason { get; set; }

        public string Comments { get; set; }

        public DateTime ApplyDate { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime ApproveDate { get; set; }

        public Boolean IsApprove { get; set; }

        public string Status { get; set; }

        public Guid UserID { get; set; }

        public string Email { get; set; }
    }
}
