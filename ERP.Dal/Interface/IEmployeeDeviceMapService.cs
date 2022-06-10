using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IEmployeeDeviceMapService
    {
        Result<bool> InsertEmployeeDeviceAttendance(EmployeeDeviceMapModel p_EmployeeDeviceMap);

        Result<List<EmployeeDeviceMapModel>> GetAllEmployeeDeviceAttendanceByDeviceId(Guid p_DeviceId);

        Result<List<EmployeeDeviceMapModel>> GetAllEmployeeDeviceAttendance();

        Result<List<EmployeeDeviceMapModel>> GetEmployeeEnrolls(Guid p_DeviceId);
    }
}
