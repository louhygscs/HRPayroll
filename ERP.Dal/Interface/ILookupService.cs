using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface ILookupService
    {
        Result<List<Item>> GetAllFinancialYear();

        Result<List<Item>> GetAllEmployeeByDepartmentId(Guid p_DepartmentId);

        Result<List<Item>> GetAllActiveEmployeeByDepartmentId(Guid p_DepartmentId);

        Result<List<Item>> GetAllCountry();

        Result<List<Item>> GetTableCategory(string Table);

        Result<List<Item>> GetAllStateByCountryId(Guid p_CountryId);

        Result<List<Item>> GetAllEmployeeType();

        Result<List<Item>> GetAllDepartment();

        Result<List<Item>> GetAllDesignation();

        Result<List<Item>> GetAllEmployeeGrade();

        Result<List<Item>> GetAllShift();

        Result<List<Item>> GetAllUnAssignSalaryEmployeeByDepartmentId(Guid p_DepartmentId);

        Result<List<Item>> GetAllLeaveCategory();

        Result<List<Dashboard>> GetSalaryChartDetailsByEmployeeId(Guid p_EmployeeId, Guid p_FinancialYearId);

        Result<List<Item>> GetAllActiveEmployee();
        Result<List<Item>> GetAllActiveEmployeeProfile(Guid p_WorkLocationId);

        Result<List<Item>> GetEmployeePerWorkLocationId(Guid p_WorkLocationId);
        Result<List<Dashboard>> GetSalaryChartInfoForAllEmployee(Guid p_p_FinancialYearId);

        Result<List<Item>> GetAllEducation();

        Result<List<Item>> GetWorkLocation(Guid p_RelatedId);

        Result<List<Item>> GetCutOffPeriod();

        Result<List<Item>> GetSchedule();

        Result<List<Item>> GetRoleList();
    }
}
