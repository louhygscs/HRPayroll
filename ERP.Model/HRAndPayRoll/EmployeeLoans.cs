using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class EmployeeLoans
    {
        public Guid EmployeeLoanID { get; set; }

        public Guid EmployeePaidLoanID { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal Amount { get; set; }

        public decimal PaidLoan { get; set; }

        public decimal PendingLoan
        {
            get
            {
                if (PaidLoanAmount.HasValue)
                {
                    return (Amount - (PaidLoan + PaidLoanAmount.Value));
                }
                else
                {
                    return (Amount - PaidLoan);
                }
            }
            set { }
        }

        public DateTime LoanDate { get; set; }

        public string LoanTitle { get; set; }

        public string Description { get; set; }

        public string ApprovedBy { get; set; }

        public int TotalMonths { get; set; }

        public string EmployeeName { get; set; }

        public Guid DepartmentId { get; set; }

        public bool IsComplete { get; set; }

        public decimal? PaidLoanAmount { get; set; }

        public decimal PaidAmount
        {
            get
            {
                if (PaidLoanAmount.HasValue)
                {
                    return PaidLoanAmount.Value;
                }
                else
                {
                    if (PendingLoan < (Amount / TotalMonths))
                    {
                        return PendingLoan;
                    }
                    else
                    {
                        return (Amount / TotalMonths);
                    }
                }
            }
            set { }
        }

        public string Department { get; set; }

    }
}
