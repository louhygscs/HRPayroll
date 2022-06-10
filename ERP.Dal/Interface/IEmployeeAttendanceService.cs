using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeAttendanceService
    {
        Result<List<EmployeeAttendances>> GetEmployeeAttendanceByEmployeeId(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate);

        Result<Boolean> SaveEmployeeAttendance(List<EmployeeAttendances> p_EmployeeAttendance, Guid p_UserId);

        Result<bool> SaveManualEmployeeAttendance(EmployeeAttendances p_EmployeeAttendances, Guid p_UserId);

        Result<bool> SaveOverTime(EmployeeAttendances p_EmployeeAttendance);


    }
}
