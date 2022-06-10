using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Dal.Implemention
{
    public class StateService : IStateService
    {
        public Result<List<State>> GetStateList()
        {
            Result<List<State>> _Result = new Result<List<State>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.StateMasters
                                 where e.IsActive == true
                                 select new State
                                 {
                                     StateID = e.StateID,
                                     CountryID = e.CountryId,
                                     StateName = e.StateName
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

        public Result<Boolean> DeleteStateById(Guid p_StateId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.StateId == p_StateId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        StateMaster _StateMaster = dbContext.StateMasters.Where(d => d.StateID == p_StateId).FirstOrDefault();

                        if (_StateMaster != null)
                        {
                            _StateMaster.IsActive = false;

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

        public Result<State> GetStateById(Guid p_StateId)
        {
            Result<State> _Result = new Result<State>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.StateMasters
                                 where d.StateID == p_StateId
                                 select new State
                                 {
                                     StateID   = d.StateID,
                                     CountryID = d.CountryId,
                                     StateName = d.StateName
                                 };

                    State _State = _Query.FirstOrDefault();

                    if (_State != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data      = _State;
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

        public Result<State> GetStateByCountryId(Guid p_CountryId)
        {
            Result<State> _Result = new Result<State>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.StateMasters
                                 where d.CountryId == p_CountryId
                                 select new State
                                 {
                                     StateID = d.StateID,
                                     CountryID = d.CountryId,
                                     StateName = d.StateName
                                 };

                    State _State = _Query.FirstOrDefault();

                    if (_State != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _State;
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

        public Result<bool> SaveState(State p_State, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            using (var dbContext = new ERPEntities())
            {
                string UniqueId = string.Empty;

                    if (p_State.StateID == Guid.Empty)
                    {
                        StateMaster _StateMaster = new StateMaster();

                        _StateMaster.StateID     = Guid.NewGuid();
                        _StateMaster.CountryId   = p_State.CountryID;
                        _StateMaster.StateName   = p_State.StateName;
                        _StateMaster.CreatedDate = DateTime.Now;
                        _StateMaster.IsActive    = true;

                        dbContext.StateMasters.Add(_StateMaster);

                        UniqueId = Convert.ToString(_StateMaster.StateID);
                    }
                    else
                    {
                        StateMaster _StateExistMaster = dbContext.StateMasters.Where(e => e.StateName == p_State.StateName).FirstOrDefault();

                        if (_StateExistMaster != null)
                        {
                            _Result.IsSuccess = false;
                            _Result.Data      = false;
                            _Result.Message   = GlobalMsg.AlreadyExistMsg;
                        }
                        else
                        {
                            StateMaster _StateMaster = dbContext.StateMasters.Where(e => e.StateID == p_State.StateID).FirstOrDefault();

                            _StateMaster.CountryId = p_State.CountryID;
                            _StateMaster.StateName = p_State.StateName;

                            UniqueId = Convert.ToString(_StateMaster.StateID);
                        }
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = UniqueId;
                    _Result.Data      = true;
            }

            return _Result;
        }
    }
}