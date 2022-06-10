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
    public class CurrencyService : ICurrencyService
    {
        public Result<List<Currency>> GetList()
        {
            Result<List<Currency>> _Result = new Result<List<Currency>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.CurrencyMasters
                                 where e.IsActive == true
                                 select new Currency
                                 {
                                     CurrencyID     = e.CurrencyID,
                                     CurrencyCode   = e.CurrencyCode,
                                     CurrencySymbol = e.CurrencySymbol,
                                     IsActive       = e.IsActive
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

        public Result<Currency> GetById(int p_EntityId)
        {
            Result<Currency> _Result = new Result<Currency>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.CurrencyMasters
                                 where d.CurrencyID == p_EntityId
                                 select new Currency
                                 {
                                     CurrencyID     = d.CurrencyID,
                                     CurrencyCode   = d.CurrencyCode,
                                     CurrencySymbol = d.CurrencySymbol,
                                     IsActive       = d.IsActive
                                 };

                    Currency _Country = _Query.FirstOrDefault();
                    if (_Country != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Country;
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
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }
        public Result<Currency> GetByCode(string p_Code)
        {
            Result<Currency> _Result = new Result<Currency>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.CurrencyMasters
                                 where d.CurrencyCode == p_Code
                                 select new Currency
                                 {
                                     CurrencyID     = d.CurrencyID,
                                     CurrencyCode   = d.CurrencyCode,
                                     CurrencySymbol = d.CurrencySymbol,
                                     IsActive       = d.IsActive
                                 };

                    Currency _Country = _Query.FirstOrDefault();
                    if (_Country != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Country;
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
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Boolean> DeleteEntity(int p_EntityId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.CurrencyMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        CurrencyMaster _EntityDelete = dbContext.CurrencyMasters.Where(d => d.CurrencyID == p_EntityId).FirstOrDefault();

                        if (_EntityDelete != null)
                        {
                            _EntityDelete.IsActive = false;

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
        public Result<bool> SaveEntity(Currency p_Entity)
        {
            Result<bool> _Result = new Result<bool>();
            using (var dbContext = new ERPEntities())
            {
                CurrencyMaster _EntityExist = dbContext.CurrencyMasters.Where(x => x.CurrencyID != p_Entity.CurrencyID && x.IsActive == true && x.CurrencyCode == p_Entity.CurrencyCode).FirstOrDefault();
                
                int _entityId = -1;

                if (_EntityExist == null)
                {
                    CurrencyMaster _EntitySave = new CurrencyMaster();

                    if (p_Entity.CurrencyID == -1)
                    {
                        List<CurrencyMaster> _list = dbContext.CurrencyMasters.Where(x => x.IsActive == true).ToList();

                        int _maxId = (_list.Count > 0 ? dbContext.CurrencyMasters.Max(p => p.CurrencyID) : 0);

                        if (!string.IsNullOrEmpty(_maxId.ToString()))
                        {
                            bool isParse = int.TryParse(_maxId.ToString(), out _entityId);

                            if (isParse)
                            {
                                _EntitySave.CurrencyID = _entityId + 1;
                            }
                            else
                            {
                                _entityId = 1;
                            }
                        }

                        _EntitySave.CurrencyID     = _entityId;
                        _EntitySave.CurrencyCode   = p_Entity.CurrencyCode;
                        _EntitySave.CurrencySymbol = p_Entity.CurrencySymbol;
                        _EntitySave.IsActive       = true;
                    }
                    else
                    {
                        _EntitySave = dbContext.CurrencyMasters.Where(e => e.CurrencyID == p_Entity.CurrencyID).FirstOrDefault();

                        _EntitySave.CurrencyCode   = p_Entity.CurrencyCode;
                        _EntitySave.CurrencySymbol = p_Entity.CurrencySymbol;
                        _EntitySave.IsActive       = p_Entity.IsActive;
                    }

                    if (p_Entity.CurrencyID == -1)
                    {
                        dbContext.CurrencyMasters.Add(_EntitySave);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_EntitySave.CurrencyID);
                    _Result.Data      = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data      = false;
                    _Result.Message   = GlobalMsg.AlreadyExistMsg;
                }
            }
            return _Result;
        }
        
    }
}
