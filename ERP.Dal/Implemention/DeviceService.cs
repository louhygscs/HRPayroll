using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERP.Dal.Implemention
{
    public class DeviceService : IDeviceService
    {
        public Result<List<DeviceModel>> GetDeviceList()
        {
            Result<List<DeviceModel>> _Result = new Result<List<DeviceModel>>();
            string Status = Convert.ToString(ConnectionStatusValue.DisConnected);
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DeviceMasters
                                 where d.IsActive == true
                                 select new DeviceModel
                                 {
                                     Address = d.Address,
                                     DeviceCode = d.DeviceCode,
                                     DeviceID = d.DeviceID,
                                     DeviceName = d.DeviceName,
                                     IPAddress = d.IPAddress,
                                     PhoneNo = d.PhoneNo,
                                     Port = d.Port ?? 0,
                                     ConnectionStatus = Status,
                                 };

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

        public Result<DeviceModel> GetDeViceById(Guid p_Id)
        {
            Result<DeviceModel> _Result = new Result<DeviceModel>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DeviceMasters
                                 .Where(x => x.IsActive == true && x.DeviceID == p_Id)
                                 select new DeviceModel()
                                 {
                                     DeviceID = d.DeviceID,
                                     Address = d.Address,
                                     DeviceCode = d.DeviceCode,
                                     DeviceName = d.DeviceName,
                                     IPAddress = d.IPAddress,
                                     PhoneNo = d.PhoneNo,
                                     Port = d.Port ?? 0,
                                 };
                    if (_Query != null)
                    {
                        _Result.Data = _Query.FirstOrDefault();
                        _Result.IsSuccess = true;
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

        public Result<bool> SaveDevice(DeviceModel p_DeviceModel, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            DeviceMaster _DeviceMaster = null;

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    DeviceMaster _DeviceMasteExist = dbContext.DeviceMasters.Where(x => x.DeviceID != p_DeviceModel.DeviceID && x.DeviceName == p_DeviceModel.DeviceName && x.IsActive == true).FirstOrDefault();
                    if (_DeviceMasteExist == null)
                    {
                        if (p_DeviceModel.DeviceID == Guid.Empty)
                        {
                            _DeviceMaster = new DeviceMaster();
                            _DeviceMaster.DeviceID = Guid.NewGuid();
                            _DeviceMaster.CreatedBy = p_UserId;
                            _DeviceMaster.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            _DeviceMaster = dbContext.DeviceMasters.Where(d => d.DeviceID == p_DeviceModel.DeviceID && d.IsActive == true).FirstOrDefault();
                            _DeviceMaster.ModifiedBy = p_UserId;
                            _DeviceMaster.ModifiedDate = DateTime.Now;
                        }
                        _DeviceMaster.Address = p_DeviceModel.Address;
                        _DeviceMaster.DeviceCode = p_DeviceModel.DeviceCode;
                        _DeviceMaster.DeviceName = p_DeviceModel.DeviceName;
                        _DeviceMaster.IPAddress = p_DeviceModel.IPAddress;
                        _DeviceMaster.PhoneNo = p_DeviceModel.PhoneNo;
                        _DeviceMaster.Port = p_DeviceModel.Port;
                        _DeviceMaster.IsActive = true;

                        if (p_DeviceModel.DeviceID == Guid.Empty)
                        {
                            dbContext.DeviceMasters.Add(_DeviceMaster);
                        }
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_DeviceMaster.DeviceID);
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
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> DeleteDeviceById(Guid p_DeviceId, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    DeviceMaster _DeviceMaster = dbContext.DeviceMasters.Where(X => X.DeviceID == p_DeviceId && X.IsActive == true).FirstOrDefault();
                    if (_DeviceMaster != null)
                    {
                        _DeviceMaster.IsActive = false;
                        _DeviceMaster.ModifiedBy = p_UserId;
                        _DeviceMaster.ModifiedDate = DateTime.Now;
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

        public Result<int> GetDeviceCount()
        {
            Result<int> _Result = new Result<int>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext=new ERPEntities())
                {
                    var _Count = dbContext.DeviceMasters.Where(x => x.IsActive == true).Count();
                    _Result.IsSuccess = true;
                    _Result.Data = _Count;
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
