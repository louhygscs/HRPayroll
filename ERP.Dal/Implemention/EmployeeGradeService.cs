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
    public class EmployeeGradeService : IEmployeeGradeService
    {
        public Result<List<EmployeeGrade>> GetEmployeeGradeList()
        {
            Result<List<EmployeeGrade>> _Result = new Result<List<EmployeeGrade>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeGradeMasters
                                 where e.IsActive == true
                                 select new EmployeeGrade
                                 {
                                     EmployeeGradeID = e.EmployeeGradeID,
                                     EmployeeGradeName = e.EmployeeGrade,
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

        public Result<Boolean> DeleteEmployeeGradeById(Guid p_EmployeeGradeId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.EmployeeGradeId == p_EmployeeGradeId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        EmployeeGradeMaster _EmployeeGradeMaster = dbContext.EmployeeGradeMasters.Where(d => d.EmployeeGradeID == p_EmployeeGradeId).FirstOrDefault();

                        if (_EmployeeGradeMaster != null)
                        {
                            _EmployeeGradeMaster.IsActive = false;
                            _EmployeeGradeMaster.ModifiedDate = DateTime.Now;
                            _EmployeeGradeMaster.ModifiedBy = p_UserId;

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

        public Result<EmployeeGrade> GetEmployeeGradeById(Guid p_EmployeeGradeId)
        {
            Result<EmployeeGrade> _Result = new Result<EmployeeGrade>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeGradeMasters
                                 where e.EmployeeGradeID == p_EmployeeGradeId
                                 select new EmployeeGrade
                                 {
                                     EmployeeGradeID = e.EmployeeGradeID,
                                     EmployeeGradeName = e.EmployeeGrade,
                                 };

                    EmployeeGrade _EmployeeGrade = _Query.FirstOrDefault();
                    if (_EmployeeGrade != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _EmployeeGrade;
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

        public Result<Boolean> SaveEmployeeGrade(EmployeeGrade p_EmployeeGrade, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeGradeMaster _EmployeeGradeMasterExist = dbContext.EmployeeGradeMasters.Where(e => e.EmployeeGradeID != p_EmployeeGrade.EmployeeGradeID && e.EmployeeGrade == p_EmployeeGrade.EmployeeGradeName && e.IsActive == true).FirstOrDefault();

                    if (_EmployeeGradeMasterExist == null)
                    {   
                        EmployeeGradeMaster _EmployeeGradeMaster = new EmployeeGradeMaster();

                        if (p_EmployeeGrade.EmployeeGradeID == Guid.Empty)
                        {
                            _EmployeeGradeMaster.EmployeeGradeID = Guid.NewGuid();
                            _EmployeeGradeMaster.IsActive = true;
                            _EmployeeGradeMaster.CreatedDate = DateTime.Now;
                            _EmployeeGradeMaster.CreatedBy = p_UserId;
                            _EmployeeGradeMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _EmployeeGradeMaster = dbContext.EmployeeGradeMasters.Where(d => d.EmployeeGradeID == p_EmployeeGrade.EmployeeGradeID).FirstOrDefault();

                            _EmployeeGradeMaster.ModifiedDate = DateTime.Now;
                            _EmployeeGradeMaster.ModifiedBy = p_UserId;
                        }

                        _EmployeeGradeMaster.EmployeeGrade = p_EmployeeGrade.EmployeeGradeName;

                        if (p_EmployeeGrade.EmployeeGradeID == Guid.Empty)
                        {
                            dbContext.EmployeeGradeMasters.Add(_EmployeeGradeMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_EmployeeGradeMaster.EmployeeGradeID);
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
