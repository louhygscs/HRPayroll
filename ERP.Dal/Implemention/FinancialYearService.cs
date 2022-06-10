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
    public class FinancialYearService : IFinancialYearService
    {
        public Result<FinancialYear> GetFinancialYearById(Guid p_FinancialYearId)
        {
            Result<FinancialYear> _Result = new Result<FinancialYear>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from f in dbContext.FinancialYearMasters
                                 where f.FinancialYearID == p_FinancialYearId
                                 select new FinancialYear
                                 {
                                    FinancialYearId   = f.FinancialYearID,
                                    FinancialYearText = f.FinancialYear,
                                    Year              = f.Year,
                                    IsActive          = f.IsActive,
                                    IsLocked          = f.IsLocked
                                 };

                    _Result.Data = _Query.First();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<FinancialYear>> GetFinancialYearList()
        {
            Result<List<FinancialYear>> _Result = new Result<List<FinancialYear>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from f in dbContext.FinancialYearMasters
                                 where f.IsActive == true
                                 select new FinancialYear
                                 {
                                     FinancialYearId    = f.FinancialYearID,
                                     FinancialYearText  = f.FinancialYear,
                                     Year               = f.Year,
                                     IsActive           = f.IsActive,
                                     IsLocked           = f.IsLocked
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Boolean> DeleteFinancialYearById(Guid p_FinancialYearId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeAttendances.Where(ea => ea.FinancialYearId == p_FinancialYearId && ea.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        FinancialYearMaster _FinancialYearMaster = dbContext.FinancialYearMasters.Where(f => f.FinancialYearID == p_FinancialYearId).FirstOrDefault();

                        if (_FinancialYearMaster != null)
                        {
                            _FinancialYearMaster.IsActive     = false;
                            _FinancialYearMaster.ModifiedDate = DateTime.Now;
                            _FinancialYearMaster.ModifiedBy   = p_UserId;
                            _FinancialYearMaster.IsLocked     = false;
                            _FinancialYearMaster.LockedDate   = DateTime.Now;
                            _FinancialYearMaster.LockedBy     = p_UserId;

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
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Boolean> SaveFinancialYear(FinancialYear p_FinancialYear, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    FinancialYearMaster _FinancialYearMasterExist = dbContext.FinancialYearMasters.Where(f => f.FinancialYearID != p_FinancialYear.FinancialYearId && f.FinancialYear == p_FinancialYear.FinancialYearText && f.Year == p_FinancialYear.Year && f.IsActive == true).FirstOrDefault();

                    if (_FinancialYearMasterExist == null)
                    {
                        FinancialYearMaster _FinancialYearMaster = new FinancialYearMaster();

                        _FinancialYearMaster.FinancialYearID = Guid.NewGuid();
                        _FinancialYearMaster.FinancialYear   = p_FinancialYear.FinancialYearText;
                        _FinancialYearMaster.Year            = p_FinancialYear.Year;
                        _FinancialYearMaster.IsActive        = true;
                        _FinancialYearMaster.CreatedDate     = DateTime.Now;
                        _FinancialYearMaster.CreatedBy       = p_UserId;
                        _FinancialYearMaster.ModifiedDate    = DateTime.Now;

                        dbContext.FinancialYearMasters.Add(_FinancialYearMaster);

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id        = Convert.ToString(_FinancialYearMaster.FinancialYearID);
                        _Result.Data      = true;
                    }
                    else
                    {
                        _Result.IsSuccess = false;
                        _Result.Data      = false;
                        _Result.Message   = GlobalMsg.AlreadyExistMsg;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }
    }
}
