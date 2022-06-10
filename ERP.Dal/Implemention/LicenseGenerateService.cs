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
    public class LicenseGenerateService : ILicenseGenerateService
    {
        public Result<List<LicenseGenerateModel>> GetLicenseKeyList()
        {
            Result<List<LicenseGenerateModel>> _Result = new Result<List<LicenseGenerateModel>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.LicenseKeyMasters.Where(x => x.IsActive == true)
                        .Select(s => new LicenseGenerateModel()
                        {
                            Email = s.Email,
                            Key = s.KeyID,
                            IsUsed = s.IsUsed,
                        });
                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
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

        public Result<bool> SaveLicenseKey(LicenseGenerateModel p_LicenseGenerateModel, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    LicenseKeyMaster _LicenseKeyMasterExist = dbContext.LicenseKeyMasters.Where(x => x.IsActive == true && x.Email == p_LicenseGenerateModel.Email).FirstOrDefault();
                    if (_LicenseKeyMasterExist == null)
                    {
                        LicenseKeyMaster _LicenseKeyMaster = new LicenseKeyMaster();
                        _LicenseKeyMaster.Email = p_LicenseGenerateModel.Email;
                        _LicenseKeyMaster.KeyID = p_LicenseGenerateModel.Key;
                        _LicenseKeyMaster.IsActive = true;
                        _LicenseKeyMaster.IsUsed = false;
                        _LicenseKeyMaster.CreatedDate = DateTime.Now;
                        _LicenseKeyMaster.CreatedBy = p_UserId;
                        _LicenseKeyMaster.LicenseKeyID = Guid.NewGuid();
                        dbContext.LicenseKeyMasters.Add(_LicenseKeyMaster);
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.AlreadyExistKeyMsg;
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

        public Result<bool> LicenseKeyUsedById(Guid p_LicenseKeyId)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    LicenseKeyMaster _LicenseKeyMasterExist = dbContext.LicenseKeyMasters.Where(x => x.IsActive == true && x.LicenseKeyID == p_LicenseKeyId).FirstOrDefault();
                    if (_LicenseKeyMasterExist != null)
                    {
                        _LicenseKeyMasterExist.IsUsed = true;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
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
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

    }
}
