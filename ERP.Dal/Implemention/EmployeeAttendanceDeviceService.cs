using ERP.Dal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Model;
using ERP.Common;

namespace ERP.Dal.Implemention
{
    public class EmployeeAttendanceDeviceService : IEmployeeAttendanceDeviceService
    {
        public Result<List<EmployeeAttendanceDevices>> GetEmployeeAttendanceDeviceByEmployeeId(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate)
        {
            Result<List<EmployeeAttendanceDevices>> _Result = new Result<List<EmployeeAttendanceDevices>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    //var _Query = from e in dbContext.EmployeeAttendanceDevices
                    //             where e.EmployeeId == p_EmployeeId && e.AttendanceDate >= p_FromDate && e.AttendanceDate <= p_ToDate && e.IsActive == true
                    //             orderby e.AttendanceDate
                    //             select new EmployeeAttendanceDevices
                    //             {
                    //                 EmployeeAttendanceID = e.EmployeeAttendanceID,
                    //                 EmployeeId = e.EmployeeId,
                    //                 EnrollNo = e.EnrollNo,
                    //                 AttendanceDate = e.AttendanceDate,
                    //                 AttendanceDateTime = e.AttendanceDateTime,
                    //                 PunchTime = e.PunchTime,
                    //                 VerifyMethod = e.VerifyMethod,
                    //                 PunchMethod = e.PunchMethod,
                    //             };

                    var _Query = from e in dbContext.EmployeeAttendanceDevices
                                 join ee in dbContext.EmployeeMasters on e.EmployeeId equals ee.EmployeeID
                                 where e.AttendanceDate >= p_FromDate && e.AttendanceDate <= p_ToDate && ee.IsActive == true && ee.IsLeave == false
                                 orderby e.AttendanceDate
                                 select new EmployeeAttendanceDevices
                                 {
                                     EmployeeAttendanceID = e.EmployeeAttendanceID,
                                     EmployeeId = e.EmployeeId,
                                     EnrollNo = e.EnrollNo,
                                     AttendanceDate = e.AttendanceDate,
                                     AttendanceDateTime = e.AttendanceDateTime,
                                     PunchTime = e.PunchTime,
                                     VerifyMethod = e.VerifyMethod,
                                     PunchMethod = e.PunchMethod,
                                     ShiftId = e.ShiftId,
                                     PunchType = e.PunchType,

                                 };

                    if (p_EmployeeId != null)
                    {
                        _Query = _Query.Where(x => x.EmployeeId == p_EmployeeId);
                    }

                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<string>> GetEmployeeAttendanceTimeByEmpoyeeIdAndDate(Guid p_EmployeeId, DateTime p_CurrentDate)
        {
            Result<List<string>> _Result = new Result<List<string>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    List<EmployeeAttendanceDevice> _EmployeeAttendanceDevice = dbContext.EmployeeAttendanceDevices.Where(x => x.IsActive == true && x.AttendanceDate == p_CurrentDate && x.EmployeeId == p_EmployeeId && x.PunchType == "DEVICE").ToList();
                    if (_EmployeeAttendanceDevice != null)
                    {
                        _Result.Data = _EmployeeAttendanceDevice.Select(x => x.PunchTime).ToList();
                        _Result.IsSuccess = true;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> SaveEmployeeAttendance(EmployeeAttendanceDeviceModel p_EmployeeAttendanceDeviceModel)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    EmployeeAttendanceDevice _EmployeeAttendanceDevice = new EmployeeAttendanceDevice();
                    _EmployeeAttendanceDevice.EmployeeAttendanceID = Guid.NewGuid();
                    _EmployeeAttendanceDevice.EmployeeId = p_EmployeeAttendanceDeviceModel.EmployeeId;
                    _EmployeeAttendanceDevice.DeviceId = p_EmployeeAttendanceDeviceModel.DeviceId;
                    _EmployeeAttendanceDevice.EnrollNo = p_EmployeeAttendanceDeviceModel.EnrollNo;
                    _EmployeeAttendanceDevice.AttendanceDateTime = p_EmployeeAttendanceDeviceModel.AttendanceDateTime;
                    _EmployeeAttendanceDevice.AttendanceDate = p_EmployeeAttendanceDeviceModel.AttendanceDate;
                    _EmployeeAttendanceDevice.PunchTime = p_EmployeeAttendanceDeviceModel.PunchTime;
                    _EmployeeAttendanceDevice.VerifyMethod = p_EmployeeAttendanceDeviceModel.VerifyMethod;
                    _EmployeeAttendanceDevice.PunchMethod = p_EmployeeAttendanceDeviceModel.PunchMethod;
                    _EmployeeAttendanceDevice.PunchType = p_EmployeeAttendanceDeviceModel.PunchType;
                    _EmployeeAttendanceDevice.CreatedDate = DateTime.Now;
                    _EmployeeAttendanceDevice.IsActive = true;
                    _EmployeeAttendanceDevice.ShiftId = p_EmployeeAttendanceDeviceModel.ShiftId;
                    dbContext.EmployeeAttendanceDevices.Add(_EmployeeAttendanceDevice);
                    dbContext.SaveChanges();
                    _Result.IsSuccess = true;
                    _Result.TotalCount = dbContext.EmployeeAttendanceDevices.Where(x => x.EmployeeId == p_EmployeeAttendanceDeviceModel.EmployeeId && x.PunchType == "WEB" && x.AttendanceDate == p_EmployeeAttendanceDeviceModel.AttendanceDate).Count();
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<string> GetEmployeeAttendancePunchTypeandCount(Guid p_EmployeeId)
        {
            Result<string> _Result = new Result<string>();
            try
            {
                _Result.IsSuccess = false;
                var _Date = DateTime.Now.Date;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.EmployeeAttendanceDevices.Where(x => x.EmployeeId == p_EmployeeId && x.AttendanceDate == _Date && x.PunchType == "WEB" && x.IsActive == true).OrderByDescending(s => s.AttendanceDateTime).Select(a => a.PunchMethod).FirstOrDefault();
                    if (_Query != null)
                    {
                        _Result.Data = _Query;
                        _Result.IsSuccess = true;
                        _Result.TotalCount = dbContext.EmployeeAttendanceDevices.Where(x => x.EmployeeId == p_EmployeeId && x.AttendanceDate == _Date && x.PunchType == "WEB" && x.IsActive == true).OrderByDescending(s => s.AttendanceDateTime).Count();
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Guid> GetShiftByEmployeeId(Guid p_EmployeeId)
        {
            Result<Guid> _Result = new Result<Guid>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMaster = dbContext.EmployeeMasters.Where(x => x.EmployeeID == p_EmployeeId).FirstOrDefault();
                    if (_EmployeeMaster != null)
                    {
                        _Result.Data = _EmployeeMaster.ShiftId;
                        _Result.IsSuccess = true;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> SaveAttendance(AttendanceRequest p_AttendanceRequest)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    EmployeeAttendanceDevice _EmployeeAttendanceDevice = new EmployeeAttendanceDevice();
                    _EmployeeAttendanceDevice.EmployeeAttendanceID = Guid.NewGuid();
                    _EmployeeAttendanceDevice.EmployeeId = p_AttendanceRequest.EmployeeId;
                    _EmployeeAttendanceDevice.EnrollNo = p_AttendanceRequest.EnrollNo;
                    _EmployeeAttendanceDevice.AttendanceDateTime = Convert.ToDateTime(p_AttendanceRequest.AttendanceDate);
                    _EmployeeAttendanceDevice.AttendanceDate = Convert.ToDateTime(p_AttendanceRequest.AttendanceDate).Date;
                    _EmployeeAttendanceDevice.PunchTime = Convert.ToDateTime(p_AttendanceRequest.AttendanceDate).ToLongTimeString();
                    _EmployeeAttendanceDevice.PunchMethod = p_AttendanceRequest.PunchMethod;
                    _EmployeeAttendanceDevice.PunchType = "WEB";
                    _EmployeeAttendanceDevice.CreatedDate = DateTime.Now;
                    _EmployeeAttendanceDevice.IsActive = true;
                    _EmployeeAttendanceDevice.LocationName = p_AttendanceRequest.LocationName;
                    dbContext.EmployeeAttendanceDevices.Add(_EmployeeAttendanceDevice);
                    dbContext.SaveChanges();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeeAttendanceDevices>> DeviceAttendanceReport(List<Guid> p_ListOfEmployeeId, List<int> p_ListOfMonth, Guid p_FinancialYearId)
        {
            Result<List<EmployeeAttendanceDevices>> _Result = new Result<List<EmployeeAttendanceDevices>>();
            int _FromYear = 0;
            int _ToYear = 0;
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _FinancialYear = dbContext.FinancialYearMasters.Where(x => x.FinancialYearID == p_FinancialYearId && x.IsActive == true).FirstOrDefault();
                    if (_FinancialYear != null)
                    {
                        _FromYear = Convert.ToInt32(_FinancialYear.FinancialYear.Split('-')[0]);
                        _ToYear = Convert.ToInt32(_FinancialYear.FinancialYear.Split('-')[1]);
                    }
                    DateTime p_FromDate = new DateTime(_FromYear, 4, 01);
                    DateTime p_ToDate = new DateTime(_ToYear, 3, 31);

                    var _Query = from e in dbContext.EmployeeAttendanceDevices.Where(x => x.AttendanceDate >= p_FromDate && x.AttendanceDate <= p_ToDate)
                                 join ee in dbContext.EmployeeMasters on e.EmployeeId equals ee.EmployeeID
                                 where p_ListOfEmployeeId.Contains(ee.EmployeeID) && p_ListOfMonth.Contains(e.AttendanceDate.Value.Month) && ee.IsActive == true && ee.IsLeave == false
                                 orderby e.AttendanceDate
                                 select new EmployeeAttendanceDevices
                                 {
                                     EmployeeAttendanceID = e.EmployeeAttendanceID,
                                     EmployeeId = e.EmployeeId,
                                     EmployeeName = ee.FirstName + " " + ee.LastName,
                                     EnrollNo = e.EnrollNo,
                                     AttendanceDate = e.AttendanceDate,
                                     AttendanceDateTime = e.AttendanceDateTime,
                                     PunchTime = e.PunchTime,
                                     VerifyMethod = e.VerifyMethod,
                                     PunchMethod = e.PunchMethod,
                                     PunchType = e.PunchType,
                                     ShiftId = e.ShiftId,
                                     Month = e.AttendanceDate.Value.Month,
                                     Department = ee.DepartmentMaster.Department,
                                 };
                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> InsertAttendance()
        {
            Result<bool> _Result = new Result<bool>();
            List<Guid> _ListofShiftmaster = new List<Guid>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    var _Shift = dbContext.ShiftMasters.Where(x => x.IsActive == true).ToList();
                    if (_Shift.Count > 0)
                    {
                        foreach (var item in _Shift)
                        {
                            var _TimeStapAMPM = item.FromTime.Split()[1];
                            if (_TimeStapAMPM == "PM")
                            {
                                _ListofShiftmaster.Add(item.ShiftID);
                            }
                        }
                    }

                    var _EmployeeId = dbContext.EmployeeMasters.Where(x => x.IsActive == true && _ListofShiftmaster.Contains(x.ShiftId)).ToList();
                    if (_EmployeeId != null)
                    {
                        foreach (var item in _EmployeeId)
                        {
                            EmployeeAttendanceDevice _EmployeeAttendanceDevice = new EmployeeAttendanceDevice();
                            _EmployeeAttendanceDevice.EmployeeAttendanceID = Guid.NewGuid();
                            _EmployeeAttendanceDevice.EmployeeId = item.EmployeeID;
                            _EmployeeAttendanceDevice.EnrollNo = item.EmployeeNo.ToString();
                            _EmployeeAttendanceDevice.PunchType = "DEVICE";
                            _EmployeeAttendanceDevice.PunchMethod = "OUT";
                            _EmployeeAttendanceDevice.PunchTime = "23:59:00";
                            _EmployeeAttendanceDevice.DeviceId = Guid.Empty;
                            _EmployeeAttendanceDevice.AttendanceDate = DateTime.Now.Date;
                            _EmployeeAttendanceDevice.AttendanceDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 00);
                            _EmployeeAttendanceDevice.CreatedDate = DateTime.Now;
                            _EmployeeAttendanceDevice.ModifiedDate = DateTime.Now;
                            _EmployeeAttendanceDevice.IsActive = true;
                            dbContext.EmployeeAttendanceDevices.Add(_EmployeeAttendanceDevice);
                        }
                        foreach (var item in _EmployeeId)
                        {
                            EmployeeAttendanceDevice _EmployeeAttendanceDevice = new EmployeeAttendanceDevice();
                            _EmployeeAttendanceDevice.EmployeeAttendanceID = Guid.NewGuid();
                            _EmployeeAttendanceDevice.EmployeeId = item.EmployeeID;
                            _EmployeeAttendanceDevice.EnrollNo = item.EmployeeNo.ToString();
                            _EmployeeAttendanceDevice.PunchType = "DEVICE";
                            _EmployeeAttendanceDevice.PunchMethod = "IN";
                            _EmployeeAttendanceDevice.PunchTime = "01:00:00";
                            _EmployeeAttendanceDevice.DeviceId = Guid.Empty;
                            _EmployeeAttendanceDevice.AttendanceDate = DateTime.Now.Date.AddDays(1);
                            _EmployeeAttendanceDevice.AttendanceDateTime = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 01, 00, 00);
                            _EmployeeAttendanceDevice.CreatedDate = DateTime.Now;
                            _EmployeeAttendanceDevice.ModifiedDate = DateTime.Now;
                            _EmployeeAttendanceDevice.IsActive = true;
                            dbContext.EmployeeAttendanceDevices.Add(_EmployeeAttendanceDevice);
                        }
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }


    }
}
