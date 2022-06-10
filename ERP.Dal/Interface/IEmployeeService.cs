using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeService
    {
        Result<List<Employee>> GetEmployeeList();

        Result<Boolean> DeleteEmployeeById(Guid p_EmployeeId, Guid p_UserId);

        Result<Boolean> DeletePermenentEmployeeById(Guid p_EmployeeId, Guid p_UserId);

        Result<int> GetMaxEmployeeNo();

        Result<Employee> GetEmployeeById(Guid p_EmployeeId);

        Result<Boolean> SaveEmployee(Employee p_Employee, Guid p_UserId);

        Result<Boolean> UpdateEmployeeProfile(Employee p_Employee, Guid p_UserId);

        Result<Boolean> ResignEmployee(Employee p_Employee, Guid p_UserId);

        Result<List<Employee>> EmployeeDetailReport(string p_EmployeeTypeId, Boolean? P_IsResign, DateTime p_FromDate, DateTime p_ToDate);

        Result<Boolean> UpdateEmployeeShift(Guid p_EmployeeId, Guid p_ShiftId, Guid p_UserId);

        Result<List<Employee>> GetAllEmployeeList();
        Result<List<Employee>> GetAllIsActiveEmployeeList();
        Result<List<Employee>> GetAllIsActiveFromEmployeeProfileList();

        Result<List<EmployeeModel>> GetAllDeviceEmployeeList();

        Result<List<EmployeeModel>> GetAllSendPendingEmployeeByDevice(Guid p_DeviceId);

        Result<bool> SaveEmployeeFingerPrint(EmployeeModel p_Employee);

        Result<List<EmployeeModel>> GetEmployeeAttendanceReportByEmpoyeeIdAndDate(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate, Guid p_DeviceId);

        Result<List<EmployeeBirthDayModel>> GetUpComingBirthDate();

        Result<int> GetPresentEmployee();

        Result<List<EmployeeDepartment>> GetDepartmentChartInfo();

        Result<List<Employee>> GetPresentEmployeeList();

        Result<List<Employee>> GetAbsentEmployeeList();

    }
}
