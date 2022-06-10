using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeSalaryService
    {
        Result<List<EmployeeSalarys>> GetEmployeeSalaryList();

        Result<List<Allowance>> GetEmployeeAllowanceByEmployeeId(Guid p_EmployeeId);

        Result<List<Deduction>> GetEmployeeDeductionByEmployeeId(Guid p_EmployeeId);

        Result<EmployeeSalarys> GetEmployeeSalaryById(Guid p_EmployeeSalaryId);

        Result<EmployeeSalarys> GetEmployeeSalaryByEmployeeId(Guid p_EmployeeEmployeeId);

        Result<Boolean> SaveEmployeeSalary(EmployeeSalarys p_EmployeeSalary, Guid p_UserId);

    }
}
