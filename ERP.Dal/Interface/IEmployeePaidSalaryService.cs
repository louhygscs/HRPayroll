using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeePaidSalaryService
    {
        Result<Boolean> CheckSalaryPaidByEmployee(Guid p_EmployeeId, string p_Month, int p_Year);

        //Result<EmployeePaidSalarys> GetEmployeePaidSalaryByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year);

        Result<EmployeePaidSalarys> GetEmployeePaidSalaryByEmployeeIds(Guid p_EmployeeId, string p_Month, int p_Year, DateTime? p_FormDate, DateTime? p_ToDate, Guid _FinancialYearId);
       
        Result<List<Allowance>> GetEmployeeAllowanceByEmployeePaidSalaryId(Guid p_EmployeePaidSalaryId, Guid p_EmployeeId);

        Result<List<EmployeeLoans>> GetEmployeeLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year);

        Result<List<Deduction>> GetEmployeeDeductionByEmployeePaidSalaryId(Guid p_EmployeePaidSalaryId, Guid p_EmployeeId);

        Result<List<EmployeePaidSalarys>> GetEmployeeCompletedPaidSalaryByMonth(string p_Month, int p_Year, int p_IsMonthSalary);

        Result<List<EmployeePaidSalarys>> GetEmployeePendingSalaryByMonth(string p_Month, int p_IntMonth, int p_Year, int p_IsMonthSalary);

        Result<List<EmployeePaidSalarys>> GetEmployeeCompletedPaidSalaryByDate(DateTime p_FromDate, DateTime p_ToDate, int p_IsMonthSalary);

        Result<List<EmployeePaidSalarys>> GetEmployeePendingSalaryByDate(DateTime p_FromDate, DateTime p_ToDate, int p_IsMonthSalary);

        Result<List<EmployeePaidSalarys>> GetLeaveDetailsByEmployeeId(Guid p_EmployeeId, Guid p_FinancialYearId);

        Result<List<EmployeePaidSalarys>> GetLeaveOpeningDetailsByFinancialYearId(Guid p_FinancialYearId, Guid? p_EmployeeId);

        Result<Boolean> SaveEmployeePaidSalary(EmployeePaidSalarys p_EmployeePaidSalary, Guid p_UserId);

        Result<List<EmployeePaidSalarys>> SalaryReport(List<Guid> p_ListOfEmployeeId, List<string> p_ListOfMonthYear, Guid p_FinancialYearId);

        Result<EmployeePaidSalarys> SalarySlipReport(Guid p_EmployeeId, string p_Month, int p_Year, DateTime p_FromDate, DateTime p_ToDate);

        Result<List<EmployeeLoans>> GetEmployeePaidLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year);

        Result<List<EmployeePaidSalarys>> LeaveReport(List<Guid> p_ListOfEmployeeId, Guid p_FinancialYearId);

        Result<List<EmployeePaidSalarys>> AttendanceReport(List<Guid> p_ListOfEmployeeId, List<int> p_ListOfMonth, Guid p_FinancialYearId);

        Result<List<EmployeePaidSalarys>> GetAttendanceChartByEmployeeID(Guid p_EmployeeId, Guid p_FinancialYearId);
        Result<List<EmployeePaidSalarys>> GetAttendanceChartInfo(Guid p_FinancialYearId);
    }
}
