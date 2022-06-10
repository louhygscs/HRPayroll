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
    public class LeaveCategoryService : ILeaveCategoryService
    {
        public Result<List<LeaveCategory>> GetLeaveCategoryList()
        {
            Result<List<LeaveCategory>> _Result = new Result<List<LeaveCategory>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from l in dbContext.LeaveCategoryMasters
                                      where l.IsActive == true
                                 select new LeaveCategory
                                 {
                                     LeaveCategoryID = l.LeaveCategoryID,
                                     LeaveCategoryName = l.LeaveCategory
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

        public Result<Boolean> DeleteLeaveCategoryById(Guid p_LeaveCategoryId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeLeaveCategories.Where(e => e.LeaveCategoryId == p_LeaveCategoryId && e.IsActive == true && e.EmployeeMaster.IsActive==true).Count();

                    if (_Count <= 0)
                    {
                        LeaveCategoryMaster _LeaveCategoryMaster = dbContext.LeaveCategoryMasters.Where(l => l.LeaveCategoryID == p_LeaveCategoryId).FirstOrDefault();

                        if (_LeaveCategoryMaster != null)
                        {
                            _LeaveCategoryMaster.IsActive = false;
                            _LeaveCategoryMaster.ModifiedDate = DateTime.Now;
                            _LeaveCategoryMaster.ModifiedBy = p_UserId;

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

        public Result<LeaveCategory> GetLeaveCategoryById(Guid p_LeaveCategoryId)
        {
            Result<LeaveCategory> _Result = new Result<LeaveCategory>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from l in dbContext.LeaveCategoryMasters
                                 where l.LeaveCategoryID == p_LeaveCategoryId
                                 select new LeaveCategory
                                 {
                                     LeaveCategoryID = l.LeaveCategoryID,
                                     LeaveCategoryName = l.LeaveCategory,
                                 };

                    LeaveCategory _LeaveCategory = _Query.FirstOrDefault();
                    if (_LeaveCategory != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _LeaveCategory;
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

        public Result<Boolean> SaveLeaveCategory(LeaveCategory p_LeaveCategory, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    LeaveCategoryMaster _LeaveCategoryMasterExist = dbContext.LeaveCategoryMasters.Where(l => l.LeaveCategoryID != p_LeaveCategory.LeaveCategoryID && l.LeaveCategory == p_LeaveCategory.LeaveCategoryName && l.IsActive == true).FirstOrDefault();

                    if (_LeaveCategoryMasterExist == null)
                    {
                        LeaveCategoryMaster _LeaveCategoryMaster = new LeaveCategoryMaster();

                        if (p_LeaveCategory.LeaveCategoryID == Guid.Empty)
                        {
                            _LeaveCategoryMaster.LeaveCategoryID = Guid.NewGuid();
                            _LeaveCategoryMaster.IsActive = true;
                            _LeaveCategoryMaster.CreatedDate = DateTime.Now;
                            _LeaveCategoryMaster.CreatedBy = p_UserId;
                            _LeaveCategoryMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _LeaveCategoryMaster = dbContext.LeaveCategoryMasters.Where(l => l.LeaveCategoryID == p_LeaveCategory.LeaveCategoryID).FirstOrDefault();

                            _LeaveCategoryMaster.ModifiedDate = DateTime.Now;
                            _LeaveCategoryMaster.ModifiedBy = p_UserId;
                        }

                        _LeaveCategoryMaster.LeaveCategory = p_LeaveCategory.LeaveCategoryName;

                        if (p_LeaveCategory.LeaveCategoryID == Guid.Empty)
                        {
                            dbContext.LeaveCategoryMasters.Add(_LeaveCategoryMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_LeaveCategoryMaster.LeaveCategoryID);
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
