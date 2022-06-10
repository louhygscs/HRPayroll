using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeLeaveCategoryService
    {
        Result<List<EmployeeLeaveCategorys>> GetEmployeeLeaveCategoryListByEmployeeId(Guid p_EmployeeId);

        Result<List<EmployeeLeaveCategorys>> GetEmployeeLeaveCategoryListByDate(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate);

        Result<Boolean> DeleteEmployeeLeaveCategoryById(Guid p_EmployeeLeaveCategoryId, Guid p_UserId);

        Result<EmployeeLeaveCategorys> GetEmployeeLeaveCategoryById(Guid p_EmployeeLeaveCategoryId);

        Result<Boolean> SaveApplyEmployeeLeaveCategory(EmployeeLeaveCategorys p_EmployeeLeaveCategory, Guid p_UserId);

        Result<List<EmployeeLeaveCategorys>> GetEmployeeLeaveCategoryList();

        Result<Boolean> SaveReplyEmployeeLeaveCategory(EmployeeLeaveCategorys p_EmployeeLeaveCategory, Guid p_UserId);

        Result<Dashboard> GetTotalEmployeeLeavesByEmployeeId(Guid p_EmployeeId, Guid p_FinancialYearId);

        Result<int> GetLeaveEmployeeCount();

    }
}
