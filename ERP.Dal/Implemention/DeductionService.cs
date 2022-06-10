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
    public class DeductionService : IDeductionService
    {
        public Result<List<Deduction>> GetDeductionList()
        {
            Result<List<Deduction>> _Result = new Result<List<Deduction>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DeductionMasters
                                 where d.IsActive == true
                                 orderby d.SortNo
                                 select new Deduction
                                 {
                                     DeductionID = d.DeductionID,
                                     DeductionName = d.Deduction,
                                     IsConsider= d.IsConsider,
                                     SortNumber= d.SortNo
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

        public Result<Boolean> DeleteDeductionById(Guid p_DeductionId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeDeductionMaps.Where(e => e.DeductionId == p_DeductionId && e.IsActive == true && e.EmployeeMaster.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        DeductionMaster _DeductionMaster = dbContext.DeductionMasters.Where(d => d.DeductionID == p_DeductionId).FirstOrDefault();

                        if (_DeductionMaster != null)
                        {
                            _DeductionMaster.IsActive = false;
                            _DeductionMaster.ModifiedDate = DateTime.Now;
                            _DeductionMaster.ModifiedBy = p_UserId;

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

        public Result<Deduction> GetDeductionById(Guid p_DeductionId)
        {
            Result<Deduction> _Result = new Result<Deduction>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DeductionMasters
                                 where d.DeductionID == p_DeductionId
                                 select new Deduction
                                 {
                                     DeductionID = d.DeductionID,
                                     DeductionName = d.Deduction,
                                     IsConsider = d.IsConsider,
                                     SortNumber = d.SortNo
                                 };

                    Deduction _Deduction = _Query.FirstOrDefault();
                    if (_Deduction != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Deduction;
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

        public Result<Boolean> SaveDeduction(Deduction p_Deduction, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    DeductionMaster _DeductionMasterExist = dbContext.DeductionMasters.Where(d => d.DeductionID != p_Deduction.DeductionID && d.Deduction == p_Deduction.DeductionName && d.IsActive == true).FirstOrDefault();
                    if (_DeductionMasterExist == null)
                    {
                        DeductionMaster _DeductionMaster = new DeductionMaster();

                        if (p_Deduction.DeductionID == Guid.Empty)
                        {
                            _DeductionMaster.DeductionID = Guid.NewGuid();
                            _DeductionMaster.IsActive = true;
                            _DeductionMaster.CreatedDate = DateTime.Now;
                            _DeductionMaster.CreatedBy = p_UserId;
                            _DeductionMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _DeductionMaster = dbContext.DeductionMasters.Where(d => d.DeductionID == p_Deduction.DeductionID).FirstOrDefault();

                            _DeductionMaster.ModifiedDate = DateTime.Now;
                            _DeductionMaster.ModifiedBy = p_UserId;
                        }

                        _DeductionMaster.Deduction = p_Deduction.DeductionName;
                        _DeductionMaster.IsConsider = p_Deduction.IsConsider;
                        _DeductionMaster.SortNo = p_Deduction.SortNumber;

                        if (p_Deduction.DeductionID == Guid.Empty)
                        {
                            dbContext.DeductionMasters.Add(_DeductionMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_DeductionMaster.DeductionID);
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
