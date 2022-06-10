using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Implemention
{
    public class CompanyService : ICompanyService
    {
        public Result<List<Company>> GetCompanies()
        {
            Result<List<Company>> _Result = new Result<List<Company>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.CompanyMasters
                                 where e.IsActive == true
                                 select new Company
                                 {
                                     CompanyID     = e.CompanyID,
                                     CompanyName   = e.CompanyName,
                                     CompanyLogo   = e.CompanyLogo,
                                     EmailAddress  = e.EmailAddress,
                                     CountryId     = e.CountryId,
                                     StateId       = e.StateId,
                                     CategoryId    = e.CategoryId,
                                     City          = e.City,
                                     Address       = e.Address,
                                     MobileNo      = e.MobileNo,
                                     PhoneNo       = e.PhoneNo,
                                     HotLineNo     = e.HotlineNo,
                                     FaxNo         = e.FaxNo,
                                     WebSite       = e.Website,
                                     LicenseKey       = e.LicenseKey,
                                     TINNo            = e.TINNo,
                                     BusinessPermitNo = e.BusinessPermitNo,
                                     IsKeyActive      = e.IsKeyActive,
                                     Remarks = e.Remarks
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

        public Result<Company> GetCompany()
        {
            Result<Company> _Result = new Result<Company>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from c in dbContext.CompanyMasters
                                 where c.IsActive == true
                                 select new Company
                                 {
                                     CompanyID = c.CompanyID,
                                     CompanyName = c.CompanyName,
                                     CompanyLogo = c.CompanyLogo,
                                     EmailAddress = c.EmailAddress,
                                     CountryId = c.CountryId ?? Guid.Empty,
                                     StateId = c.StateId ?? Guid.Empty,
                                     CategoryId = c.CategoryId ?? Guid.Empty,
                                     City = c.City,
                                     Address = c.Address,
                                     MobileNo = c.MobileNo,
                                     PhoneNo = c.PhoneNo,
                                     HotLineNo = c.HotlineNo,
                                     FaxNo = c.FaxNo,
                                     WebSite = c.Website,
                                     LicenseKey = c.LicenseKey,
                                     IsKeyActive = c.IsKeyActive ?? false,
                                     Remarks = c.Remarks,
                                 };

                    Company _Company = _Query.FirstOrDefault();

                    if (_Company != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Company;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        _Result.IsSuccess = false;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Company> GetCompany(Guid p_Id)
        {
            Result<Company> _Result = new Result<Company>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from c in dbContext.CompanyMasters
                                 where c.CompanyID == p_Id
                                 select new Company
                                 {
                                     CompanyID = c.CompanyID,
                                     CompanyName = c.CompanyName,
                                     CompanyLogo = c.CompanyLogo,
                                     EmailAddress = c.EmailAddress,
                                     CountryId = c.CountryId ?? Guid.Empty,
                                     StateId = c.StateId ?? Guid.Empty,
                                     CategoryId = c.CategoryId ?? Guid.Empty,
                                     City = c.City,
                                     Address = c.Address,
                                     MobileNo = c.MobileNo,
                                     PhoneNo = c.PhoneNo,
                                     HotLineNo = c.HotlineNo,
                                     FaxNo = c.FaxNo,
                                     WebSite = c.Website,
                                     LicenseKey = c.LicenseKey,
                                     IsKeyActive = c.IsKeyActive ?? false,
                                     Remarks = c.Remarks
                                 };

                    Company _Company = _Query.FirstOrDefault();

                    if (_Company != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Company;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        _Result.IsSuccess = false;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Boolean> SaveCompany(Company p_Company, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    CompanyMaster _CompanyMaster = new CompanyMaster();

                    if (p_Company.CompanyID == Guid.Empty)
                    {
                        _CompanyMaster.CompanyID = Guid.NewGuid();
                        _CompanyMaster.ModifiedBy = p_UserId;
                        _CompanyMaster.IsActive = true;
                        _CompanyMaster.ModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        _CompanyMaster = dbContext.CompanyMasters.Where(c => c.CompanyID == p_Company.CompanyID).FirstOrDefault();
                        _CompanyMaster.ModifiedDate = DateTime.Now;
                        _CompanyMaster.ModifiedBy = p_UserId;
                    }

                    _CompanyMaster.CompanyName = p_Company.CompanyName;
                    _CompanyMaster.EmailAddress = p_Company.EmailAddress;
                    _CompanyMaster.CountryId = p_Company.CountryId;
                    _CompanyMaster.StateId = p_Company.StateId;
                    _CompanyMaster.CategoryId = p_Company.CategoryId;
                    _CompanyMaster.City = p_Company.City;
                    _CompanyMaster.Address = p_Company.Address;
                    _CompanyMaster.MobileNo = p_Company.MobileNo;
                    _CompanyMaster.PhoneNo = p_Company.PhoneNo;
                    _CompanyMaster.HotlineNo = p_Company.HotLineNo;
                    _CompanyMaster.FaxNo = p_Company.FaxNo;
                    _CompanyMaster.Website = p_Company.WebSite;
                    _CompanyMaster.TINNo = p_Company.TINNo;
                    _CompanyMaster.BusinessPermitNo = p_Company.BusinessPermitNo;
                    _CompanyMaster.IsKeyActive = false;
                    _CompanyMaster.Remarks = p_Company.Remarks;

                    if (!string.IsNullOrEmpty(p_Company.LicenseKey))
                    {
                        _CompanyMaster.LicenseKey = p_Company.LicenseKey;
                    }
                    if (!string.IsNullOrEmpty(p_Company.CompanyLogo))
                    {
                        _CompanyMaster.CompanyLogo = p_Company.CompanyLogo;
                    }

                    if (p_Company.CompanyID == Guid.Empty)
                    {
                        dbContext.CompanyMasters.Add(_CompanyMaster);
                    }

                    dbContext.SaveChanges();
                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_CompanyMaster.CompanyID);
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

        public Result<bool> LicenseKeyActivate(Guid P_CompanyId)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    CompanyMaster _CompanyMaster = dbContext.CompanyMasters.Where(c => c.CompanyID == P_CompanyId).FirstOrDefault();
                    if (_CompanyMaster != null)
                    {
                        _CompanyMaster.IsKeyActive = true;
                        dbContext.SaveChanges();
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

        public Result<string> GetGlobalSetting()
        {
            Result<string> _Result = new Result<string>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    var _EnumValue = dbContext.GlobalSettings.Where(a => a.GlobalSettingEnum == "LateAllowTime").FirstOrDefault();

                    if (_EnumValue != null)
                    {
                        _Result.Data = _EnumValue.GlobalSettingValue;
                        _Result.Id = _EnumValue.GlobalSettingId.ToString();
                        _Result.IsSuccess = true;
                    } else
                    {
                        GlobalSetting newSetting = new GlobalSetting();
                        newSetting.GlobalSettingName = "Late Allow Time";
                        newSetting.GlobalSettingEnum = "LateAllowTime";
                        newSetting.GlobalSettingValue = "5";
                        dbContext.GlobalSettings.Add(newSetting);
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
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

        public Result<bool> SaveGlobalSetting(string p_Value, int p_Id)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    GlobalSetting _GlobalSetting = dbContext.GlobalSettings.Where(x => x.GlobalSettingId == p_Id).FirstOrDefault();
                    if (_GlobalSetting != null)
                    {
                        _GlobalSetting.GlobalSettingValue = p_Value;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
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
