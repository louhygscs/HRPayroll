using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Dal.Implemention
{
    public class EmployeeDeviceMapService : IEmployeeDeviceMapService
    {

        public Result<bool> InsertEmployeeDeviceAttendance(EmployeeDeviceMapModel p_EmployeeDeviceMap)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    EmployeeDeviceMap _EmployeeDeviceMap = new EmployeeDeviceMap();
                    _EmployeeDeviceMap.CreatedDate = DateTime.Now;
                    _EmployeeDeviceMap.EmployeeDeviceID = Guid.NewGuid();
                    _EmployeeDeviceMap.EmployeeId = p_EmployeeDeviceMap.EmployeeId;
                    _EmployeeDeviceMap.DeviceId = p_EmployeeDeviceMap.DeviceId;
                    _EmployeeDeviceMap.EnrollNo = p_EmployeeDeviceMap.EnrollmentNo;
                    _EmployeeDeviceMap.IsActive = true;
                    dbContext.EmployeeDeviceMaps.Add(_EmployeeDeviceMap);
                    dbContext.SaveChanges();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<EmployeeDeviceMapModel>> GetAllEmployeeDeviceAttendanceByDeviceId(Guid p_DeviceId)
        {
            Result<List<EmployeeDeviceMapModel>> _Result = new Result<List<EmployeeDeviceMapModel>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.EmployeeDeviceMaps.Where(x => x.IsActive == true && x.DeviceId == p_DeviceId)
                                 .Select(s => new EmployeeDeviceMapModel
                                 {
                                     EmployeeDeviceID = s.EmployeeDeviceID,
                                     EmployeeId = s.EmployeeId,
                                     DeviceId = s.DeviceId,
                                     EnrollmentNo = s.EnrollNo,
                                 });
                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeeDeviceMapModel>> GetAllEmployeeDeviceAttendance()
        {
            Result<List<EmployeeDeviceMapModel>> _Result = new Result<List<EmployeeDeviceMapModel>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.EmployeeDeviceMaps.Where(x => x.IsActive == true)
                         .Select(s => new EmployeeDeviceMapModel
                         {
                             EmployeeDeviceID = s.EmployeeDeviceID,
                             EmployeeId = s.EmployeeId,
                             DeviceId = s.DeviceId,
                             EnrollmentNo = s.EnrollNo,
                         });
                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeeDeviceMapModel>> GetEmployeeEnrolls(Guid p_DeviceId)
        {
            Result<List<EmployeeDeviceMapModel>> _Result = new Result<List<EmployeeDeviceMapModel>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.EmployeeDeviceMaps.Where(x => x.DeviceId == p_DeviceId && x.IsActive == true).Select(s => new EmployeeDeviceMapModel()
                    {
                        EnrollmentNo = s.EnrollNo,
                        EmployeeId = s.EmployeeId,
                    });
                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }                            
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

    }
}
