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
    public class AllowanceService : IAllowanceService
    {
        public Result<List<Allowance>> GetAllowanceList()
        {
            Result<List<Allowance>> _Result = new Result<List<Allowance>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from a in dbContext.AllowanceMasters
                                 where a.IsActive == true
                                 orderby a.SortNo
                                 select new Allowance
                                 {
                                     AllowanceID = a.AllowanceID,
                                     AllowanceName = a.Allowance,
                                     IsConsider=a.IsConsider,
                                     SortNumber=a.SortNo
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

        public Result<Boolean> DeleteAllowanceById(Guid p_AllowanceId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeAllowanceMaps.Where(e => e.AllowanceId == p_AllowanceId && e.IsActive == true && e.EmployeeMaster.IsActive==true).Count();

                    if (_Count <= 0)
                    {
                        AllowanceMaster _AllowanceMaster = dbContext.AllowanceMasters.Where(a => a.AllowanceID == p_AllowanceId).FirstOrDefault();

                        if (_AllowanceMaster != null)
                        {
                            _AllowanceMaster.IsActive = false;
                            _AllowanceMaster.ModifiedDate = DateTime.Now;
                            _AllowanceMaster.ModifiedBy = p_UserId;

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

        public Result<Allowance> GetAllowanceById(Guid p_AllowanceId)
        {
            Result<Allowance> _Result = new Result<Allowance>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from a in dbContext.AllowanceMasters
                                 where a.AllowanceID == p_AllowanceId
                                 select new Allowance
                                 {
                                     AllowanceID = a.AllowanceID,
                                     AllowanceName = a.Allowance,
                                     IsConsider=a.IsConsider,
                                     SortNumber=a.SortNo
                                 };

                    Allowance _Allowance = _Query.FirstOrDefault();
                    if (_Allowance != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Allowance;
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

        public Result<Boolean> SaveAllowance(Allowance p_Allowance, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    AllowanceMaster _AllowanceMasterExist = dbContext.AllowanceMasters.Where(a => a.AllowanceID != p_Allowance.AllowanceID && a.Allowance == p_Allowance.AllowanceName && a.IsActive == true).FirstOrDefault();

                    if (_AllowanceMasterExist == null)
                    {
                        AllowanceMaster _AllowanceMaster = new AllowanceMaster();

                        if (p_Allowance.AllowanceID == Guid.Empty)
                        {
                            _AllowanceMaster.AllowanceID = Guid.NewGuid();
                            _AllowanceMaster.IsActive = true;
                            _AllowanceMaster.CreatedDate = DateTime.Now;
                            _AllowanceMaster.CreatedBy = p_UserId;
                            _AllowanceMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _AllowanceMaster = dbContext.AllowanceMasters.Where(a => a.AllowanceID == p_Allowance.AllowanceID).FirstOrDefault();

                            _AllowanceMaster.ModifiedDate = DateTime.Now;
                            _AllowanceMaster.ModifiedBy = p_UserId;
                        }

                        _AllowanceMaster.Allowance = p_Allowance.AllowanceName;
                        _AllowanceMaster.IsConsider = p_Allowance.IsConsider;
                        _AllowanceMaster.SortNo = p_Allowance.SortNumber;

                        if (p_Allowance.AllowanceID == Guid.Empty)
                        {
                            dbContext.AllowanceMasters.Add(_AllowanceMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_AllowanceMaster.AllowanceID);
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
