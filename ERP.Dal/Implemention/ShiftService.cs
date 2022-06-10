using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Implemention
{
    public class ShiftService : IShiftService
    {
        public Result<List<Shift>> GetShiftList()
        {
            Result<List<Shift>> _Result = new Result<List<Shift>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from s in dbContext.ShiftMasters
                                      where s.IsActive == true
                                 select new Shift
                                 {
                                     ShiftID = s.ShiftID,
                                     ShiftName = s.Shift,
                                     FromTime=s.FromTime,
                                     ToTime=s.ToTime,
                                 };

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

        public Result<Boolean> DeleteShiftById(Guid p_ShiftId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.ShiftId == p_ShiftId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        ShiftMaster _ShiftMaster = dbContext.ShiftMasters.Where(s => s.ShiftID == p_ShiftId).FirstOrDefault();

                        if (_ShiftMaster != null)
                        {
                            _ShiftMaster.IsActive = false;
                            _ShiftMaster.ModifiedDate = DateTime.Now;
                            _ShiftMaster.ModifiedBy = p_UserId;

                            dbContext.SaveChanges();
                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
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

        public Result<Shift> GetShiftById(Guid p_ShiftId)
        {
            Result<Shift> _Result = new Result<Shift>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from s in dbContext.ShiftMasters
                                 where s.ShiftID == p_ShiftId
                                 select new Shift
                                 {
                                     ShiftID = s.ShiftID,
                                     ShiftName = s.Shift,
                                     FromTime=s.FromTime,
                                     ToTime=s.ToTime,
                                 };

                    Shift _Shift = _Query.FirstOrDefault();
                    if (_Shift != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Shift;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
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

        public Result<Boolean> SaveShift(Shift p_Shift, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    ShiftMaster _ShiftMasterExist = dbContext.ShiftMasters.Where(s => s.ShiftID != p_Shift.ShiftID && s.Shift == p_Shift.ShiftName && s.IsActive == true).FirstOrDefault();

                    if (_ShiftMasterExist == null)
                    {
                        ShiftMaster _ShiftMaster = new ShiftMaster();

                        if (p_Shift.ShiftID == Guid.Empty)
                        {
                            _ShiftMaster.ShiftID = Guid.NewGuid();
                            _ShiftMaster.IsActive = true;
                            _ShiftMaster.CreatedDate = DateTime.Now;
                            _ShiftMaster.CreatedBy = p_UserId;
                            _ShiftMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _ShiftMaster = dbContext.ShiftMasters.Where(s => s.ShiftID == p_Shift.ShiftID).FirstOrDefault();

                            _ShiftMaster.ModifiedDate = DateTime.Now;
                            _ShiftMaster.ModifiedBy = p_UserId;
                        }

                        _ShiftMaster.Shift = p_Shift.ShiftName;
                        _ShiftMaster.FromTime = p_Shift.FromTime;
                        _ShiftMaster.ToTime = p_Shift.ToTime;

                        if (p_Shift.ShiftID == Guid.Empty)
                        {
                            dbContext.ShiftMasters.Add(_ShiftMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_ShiftMaster.ShiftID);
                        _Result.Data = true;
                    }
                    else
                    {
                        _Result.IsSuccess = false;
                        _Result.Data = false;
                        _Result.Message = GlobalMsg.AlreadyExistMsg;
                    }
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

        public Result<Shift> GetShiftByEmployeeId(Guid p_EmployeeId)
        {
            Result<Shift> _Result = new Result<Shift>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMaster = dbContext.EmployeeMasters.Where(x => x.EmployeeID == p_EmployeeId).FirstOrDefault();
                    if (_EmployeeMaster != null)
                    {
                        Shift _shift = new Shift();
                        ShiftMaster _ShiftMaster = dbContext.ShiftMasters.Where(x => x.ShiftID == _EmployeeMaster.ShiftId && x.IsActive == true).FirstOrDefault();
                        if (_ShiftMaster != null)
                        {
                            _shift.ShiftID = _ShiftMaster.ShiftID;
                            _shift.FromTime = _ShiftMaster.FromTime;
                            _shift.ToTime = _ShiftMaster.ToTime;
                            _Result.Data = _shift;
                            _Result.IsSuccess = true;
                        }
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
