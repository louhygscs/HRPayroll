using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeLoanService
    {
        Result<List<EmployeeLoans>> GetEmployeeLoanList();

        Result<Boolean> DeleteEmployeeLoanById(Guid p_EmployeeLoanId, Guid p_UserId);

        Result<EmployeeLoans> GetEmployeeLoanById(Guid p_EmployeeLoanId);

        Result<Boolean> SaveEmployeeLoan(EmployeeLoans p_EmployeeLoan, Guid p_UserId);

        Result<List<EmployeeLoans>> LoanReport(List<Guid> p_ListOfEmployeeId, bool? p_Status);
    }
}
