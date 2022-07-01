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
    public class TimelogSumarryService : ITimelogSummaryService
    {
        public Result<List<TimelogSummaryModel>> GetTimelogSummaries()
        {
            Result<List<TimelogSummaryModel>> _Result = new Result<List<TimelogSummaryModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.TimelogSummaries
                                 where e.isactive == true
                                 select new TimelogSummaryModel
                                 {
                                    TimelogId = e.TimelogId,
                                    EmployeeId= e.EmployeeId,
                                    logindate = e.logindate,
                                    logintime = e.logintime,
                                    breakindate = e.breakindate,
                                    breakintime = e.breakintime,
                                    breakoutdate = e.breakoutdate,
                                    breakouttime = e.breakouttime,
                                    overindate = e.overindate,
                                    overintime = e.overintime,
                                    overoutdate = e.overoutdate,
                                    overouttime = e.overouttime,
                                    logoutdate = e.logoutdate,
                                    logouttime = e.logouttime,
                                    isactive = e.isactive
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

        public Result<Boolean> DeleteTimelogSummaryById(Guid p_EntityId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    TimelogSummary _EntityTable = dbContext.TimelogSummaries.Where(d => d.TimelogId == p_EntityId).FirstOrDefault();

                    if (_EntityTable != null)
                    {
                        _EntityTable.isactive = false;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
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

        public Result<TimelogSummaryModel> GetTimelogSummaryById(Guid p_EntityId)
        {
            Result<TimelogSummaryModel> _Result = new Result<TimelogSummaryModel>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.TimelogSummaries
                                 where d.TimelogId == p_EntityId
                                 select new TimelogSummaryModel
                                 {
                                     TimelogId = d.TimelogId,
                                     EmployeeId = d.EmployeeId,
                                     logindate = d.logindate,
                                     logintime = d.logintime,
                                     breakindate = d.breakindate,
                                     breakintime = d.breakintime,
                                     breakoutdate = d.breakoutdate,
                                     breakouttime = d.breakouttime,
                                     overindate = d.overindate,
                                     overintime = d.overintime,
                                     overoutdate = d.overoutdate,
                                     overouttime = d.overouttime,
                                     logoutdate = d.logoutdate,
                                     logouttime = d.logouttime,
                                     isactive = d.isactive
                                 };

                    TimelogSummaryModel _Role = _Query.FirstOrDefault();
                    if (_Role != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Role;
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

        public Result<bool> SaveTimelogSummary(TimelogSummaryModel p_Entity, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            using (var dbContext = new ERPEntities())
            {
                TimelogSummary _EntityTable = dbContext.TimelogSummaries.Where(e => e.EmployeeId == p_Entity.EmployeeId).FirstOrDefault();

                if (_EntityTable == null)
                {
                    _EntityTable = new TimelogSummary();

                    if (p_Entity.TimelogId == Guid.Empty)
                    {
                        _EntityTable.TimelogId = Guid.NewGuid();
                        _EntityTable.EmployeeId = p_Entity.EmployeeId;
                        _EntityTable.logindate = p_Entity.logindate;
                        _EntityTable.logintime = p_Entity.logintime;
                        _EntityTable.breakindate = p_Entity.breakindate;
                        _EntityTable.breakintime = p_Entity.breakintime;
                        _EntityTable.breakoutdate = p_Entity.breakoutdate;
                        _EntityTable.breakouttime = p_Entity.breakouttime;
                        _EntityTable.overindate = p_Entity.overindate;
                        _EntityTable.overintime = p_Entity.overintime;
                        _EntityTable.overoutdate = p_Entity.overoutdate;
                        _EntityTable.overouttime = p_Entity.overouttime;
                        _EntityTable.logoutdate = p_Entity.logoutdate;
                        _EntityTable.logouttime = p_Entity.logouttime;
                        _EntityTable.isactive = true;
                    }
                    else
                    {
                        _EntityTable = dbContext.TimelogSummaries.Where(e => e.TimelogId == p_Entity.TimelogId).FirstOrDefault();

                        if(_EntityTable != null)
                        {
                            _EntityTable.EmployeeId = p_Entity.EmployeeId;
                            _EntityTable.logindate = p_Entity.logindate;
                            _EntityTable.logintime = p_Entity.logintime;
                            _EntityTable.breakindate = p_Entity.breakindate;
                            _EntityTable.breakintime = p_Entity.breakintime;
                            _EntityTable.breakoutdate = p_Entity.breakoutdate;
                            _EntityTable.breakouttime = p_Entity.breakouttime;
                            _EntityTable.overindate = p_Entity.overindate;
                            _EntityTable.overintime = p_Entity.overintime;
                            _EntityTable.overoutdate = p_Entity.overoutdate;
                            _EntityTable.overouttime = p_Entity.overouttime;
                            _EntityTable.logoutdate = p_Entity.logoutdate;
                            _EntityTable.logouttime = p_Entity.logouttime;
                            _EntityTable.isactive = p_Entity.isactive;
                        }
                    }

                    if (p_Entity.TimelogId == Guid.Empty)
                    {
                        dbContext.TimelogSummaries.Add(_EntityTable);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EntityTable.TimelogId);
                    _Result.Data = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data = false;
                    _Result.Message = GlobalMsg.AlreadyExistMsg;
                }
            }
            return _Result;
        }
    }
}
