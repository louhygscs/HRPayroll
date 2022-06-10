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
    public class EmployeeTypeService : IEmployeeTypeService
    {
        public Result<List<EmployeeType>> GetEmployeeTypeList()
        {
            Result<List<EmployeeType>> _Result = new Result<List<EmployeeType>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeTypeMasters
                                      where e.IsActive == true
                                      select new EmployeeType
                                 {
                                     EmployeeTypeID = e.EmployeeTypeID,
                                     EmployeeTypeName = e.EmployeeType,
                                     NoOfLeavePerMonth=e.NoOfLeavePerMonth
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

        public Result<Boolean> DeleteEmployeeTypeById(Guid p_EmployeeTypeId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.EmployeeTypeId == p_EmployeeTypeId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        EmployeeTypeMaster _EmployeeTypeMaster = dbContext.EmployeeTypeMasters.Where(e => e.EmployeeTypeID == p_EmployeeTypeId).FirstOrDefault();

                        if (_EmployeeTypeMaster != null)
                        {
                            _EmployeeTypeMaster.IsActive = false;
                            _EmployeeTypeMaster.ModifiedDate = DateTime.Now;
                            _EmployeeTypeMaster.ModifiedBy = p_UserId;

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

        public Result<EmployeeType> GetEmployeeTypeById(Guid p_EmployeeTypeId)
        {
            Result<EmployeeType> _Result = new Result<EmployeeType>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeTypeMasters
                                 where e.EmployeeTypeID == p_EmployeeTypeId
                                 select new EmployeeType
                                 {
                                     EmployeeTypeID = e.EmployeeTypeID,
                                     EmployeeTypeName = e.EmployeeType,
                                     NoOfLeavePerMonth = e.NoOfLeavePerMonth
                                 };

                    EmployeeType _EmployeeType = _Query.FirstOrDefault();
                    if (_EmployeeType != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _EmployeeType;
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

        public Result<Boolean> SaveEmployeeType(EmployeeType p_EmployeeType, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeTypeMaster _EmployeeTypeMasterExist = dbContext.EmployeeTypeMasters.Where(e => e.EmployeeTypeID != p_EmployeeType.EmployeeTypeID && e.EmployeeType == p_EmployeeType.EmployeeTypeName && e.IsActive == true).FirstOrDefault();

                    if (_EmployeeTypeMasterExist == null)
                    {
                        EmployeeTypeMaster _EmployeeTypeMaster = new EmployeeTypeMaster();

                        if (p_EmployeeType.EmployeeTypeID == Guid.Empty)
                        {
                            _EmployeeTypeMaster.EmployeeTypeID = Guid.NewGuid();
                            _EmployeeTypeMaster.IsActive = true;
                            _EmployeeTypeMaster.CreatedDate = DateTime.Now;
                            _EmployeeTypeMaster.CreatedBy = p_UserId;
                            _EmployeeTypeMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _EmployeeTypeMaster = dbContext.EmployeeTypeMasters.Where(e => e.EmployeeTypeID == p_EmployeeType.EmployeeTypeID).FirstOrDefault();

                            _EmployeeTypeMaster.ModifiedDate = DateTime.Now;
                            _EmployeeTypeMaster.ModifiedBy = p_UserId;
                        }

                        _EmployeeTypeMaster.EmployeeType = p_EmployeeType.EmployeeTypeName;
                        _EmployeeTypeMaster.NoOfLeavePerMonth = p_EmployeeType.NoOfLeavePerMonth;

                        if (p_EmployeeType.EmployeeTypeID == Guid.Empty)
                        {
                            dbContext.EmployeeTypeMasters.Add(_EmployeeTypeMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_EmployeeTypeMaster.EmployeeTypeID);
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
    }
}
