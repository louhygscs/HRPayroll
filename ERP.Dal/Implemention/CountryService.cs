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
    public class CountryService : ITimelogSummaryService
    {
        public Result<List<Country>> GetCountryList()
        {
            Result<List<Country>> _Result = new Result<List<Country>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.CountryMasters
                                 where e.IsActive == true
                                 select new Country
                                 {
                                     CountryID = e.CountryID,
                                     CountryName = e.CountryName
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

        public Result<Boolean> DeleteCountryById(Guid p_CountryId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.CountryId == p_CountryId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        CountryMaster _CountryMaster = dbContext.CountryMasters.Where(d => d.CountryID == p_CountryId).FirstOrDefault();

                        if (_CountryMaster != null)
                        {
                            _CountryMaster.IsActive = false;
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

        public Result<Country> GetCountryById(Guid p_CountryId)
        {
            Result<Country> _Result = new Result<Country>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.CountryMasters
                                 where d.CountryID == p_CountryId
                                 select new Country
                                 {
                                     CountryID = d.CountryID,
                                     CountryName = d.CountryName,
                                     Code = d.Code
                                 };

                    Country _Country = _Query.FirstOrDefault();
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
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> SaveCountry(Country p_Country, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            using (var dbContext = new ERPEntities())
            {
                CountryMaster _EducationMasterExist = dbContext.CountryMasters.Where(x => x.CountryID != p_Country.CountryID && x.IsActive == true && x.CountryName == p_Country.CountryName).FirstOrDefault();
                if (_EducationMasterExist == null)
                {
                    CountryMaster _CountryMaster = new CountryMaster();

                    if (p_Country.CountryID == Guid.Empty)
                    {
                        _CountryMaster.CountryID   = Guid.NewGuid();
                        _CountryMaster.CountryName = p_Country.CountryName;
                        _CountryMaster.Code        = p_Country.Code;
                        _CountryMaster.CreatedDate = DateTime.Now;
                        _CountryMaster.IsActive    = true;
                    }
                    else
                    {
                        _CountryMaster = dbContext.CountryMasters.Where(e => e.CountryID == p_Country.CountryID).FirstOrDefault();

                        //_CountryMaster.ModifiedDate = DateTime.Now;
                        //_CountryMaster.ModifiedBy = p_UserId;
                    }

                    _CountryMaster.CountryName = p_Country.CountryName;
                    _CountryMaster.Code = p_Country.Code;

                    if (p_Country.CountryID == Guid.Empty)
                    {
                        dbContext.CountryMasters.Add(_CountryMaster);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_CountryMaster.CountryID);
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
