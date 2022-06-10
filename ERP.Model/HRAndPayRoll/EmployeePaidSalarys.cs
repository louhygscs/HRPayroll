using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class EmployeePaidSalarys
    {
        public Guid EmployeePaidSalaryID { get; set; }

        public Guid EmployeeSalaryID { get; set; }

        public Guid EmployeeId { get; set; }

        public String Department { get; set; }

        public String FullName { get; set; }

        public int EmployeeNo { get; set; }

        public string PFNo { get; set; }

        public decimal Basic { get; set; }

        public decimal TotalEarning { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal TotalSalary { get; set; }

        public decimal PaidBasic { get; set; }

        public decimal PaidTotalEarning { get; set; }

        public decimal PaidTotalDeduction { get; set; }

        public decimal PaidTotalSalary { get; set; }

        public decimal ProfessionalTax { get; set; }

        public string Month
        {
            get;
            set;
        }

        public int Year { get; set; }

        public decimal TotalOverTimeDays { get; set; }
        
        public decimal TotalOverTimeHours { get; set; }

        public decimal TotalHours { get; set; }

        public int TotalDays { get; set; }

        public decimal TotalPresentDays { get; set; }

        public decimal AllowLeave { get; set; }

        public decimal TotalUseLeave { get; set; }

        public decimal TotalHolidays { get; set; }

        public decimal TotalPaidLeave { get; set; }

        public decimal TotalPaidLeaveAmount { get; set; }

        public decimal TotalOverTimeAmount { get; set; }

        public decimal PaidLoanAmount { get; set; }

        public DateTime PaidDate { get; set; }

        public Boolean IsPaid { get; set; }

        public string PaidBy { get; set; }

        public Guid FinancialYearId { get; set; }

        public decimal RemainingLeave
        {
            get
            { return TotalAllowLeave - TotalUseLeave; }
            set { }
        }

        public int PaidMonthCount { get; set; }

        public decimal PaidMonthAllowLeave { get; set; }

        public decimal TotalAllowLeave
        {
            get
            {
                if (FinancialYear > 0)
                {
                    DateTime _FinancialStartDate = new DateTime(FinancialYear, 04, 01);
                    double _TotalDays = (JoinDate - _FinancialStartDate).TotalDays;
                    int _LeftMonth = 0;

                    if (_TotalDays > 0)
                    {
                        _LeftMonth = Convert.ToInt32(Math.Round(_TotalDays / 30, 0));

                        double _DecimalPoint = (_TotalDays / 30) - _LeftMonth;

                        if (_DecimalPoint > 0.5)
                        {
                            _LeftMonth = _LeftMonth - 1;
                        }
                    }

                    if (PaidMonthCount > 0)
                    {
                        return PaidMonthAllowLeave + (NoOfLeavePerMonth * (12 - (PaidMonthCount + _LeftMonth)));
                    }
                    else
                    {
                        return NoOfLeavePerMonth * (12 - _LeftMonth);
                    }
                }
                return 0;
            }
            set { }
        }

        public int FinancialYear { get; set; }

        public List<Allowance> ListPaidAllowance { get; set; }

        public List<Deduction> ListPaidDeduction { get; set; }

        public List<EmployeeLoans> ListEmployeePaidLoan { get; set; }

        public DateTime JoinDate { get; set; }

        public decimal NoOfLeavePerMonth { get; set; }

        public int WeeklyOff { get; set; }

        public Decimal? WorkingHours { get; set; }

        public Decimal? OverTimeHours { get; set; }

        public int MonthNo { get; set; }

        public DateTime? SalaryFromDate { get; set; }

        public DateTime? SalaryToDate { get; set; }

    }
}
