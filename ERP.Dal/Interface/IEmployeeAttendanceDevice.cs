using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IEmployeeAttendanceDeviceService
    {
        Result<List<EmployeeAttendanceDevices>> GetEmployeeAttendanceDeviceByEmployeeId(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate);

        Result<List<string>> GetEmployeeAttendanceTimeByEmpoyeeIdAndDate(Guid p_EmployeeId, DateTime p_CurrentDate);

        Result<bool> SaveEmployeeAttendance(EmployeeAttendanceDeviceModel p_EmployeeAttendanceDeviceModel);

        Result<string> GetEmployeeAttendancePunchTypeandCount(Guid p_EmployeeId);

        Result<Guid> GetShiftByEmployeeId(Guid p_EmployeeId);

        Result<bool> SaveAttendance(AttendanceRequest p_AttendanceRequest);

        Result<List<EmployeeAttendanceDevices>> DeviceAttendanceReport(List<Guid> p_ListOfEmployeeId, List<int> p_ListOfMonth, Guid p_FinancialYearId);

        Result<bool> InsertAttendance();

    }
}
