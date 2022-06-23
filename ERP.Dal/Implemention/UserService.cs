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
    public class UserService : IUserService
    {
        public Result<SessionDetail> CheckLogin(string p_UserName, string p_Password)
        {
            Result<SessionDetail> _Result = new Result<SessionDetail>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(u => u.Username == p_UserName && u.Password == p_Password && u.IsActive == true).FirstOrDefault();

                    if (_UserMaster != null)
                    {
                        _UserMaster.LastLogin = DateTime.Now;
                        dbContext.SaveChanges();

                        Guid _RoleId = new Guid(GlobalHelper.GetEnumDescription(Role.Administrator));

                        if (_UserMaster.RoleId == _RoleId)
                        {
                            SessionDetail _SessionDetail = new SessionDetail();
                            _SessionDetail.UserID = _UserMaster.UserID;
                            _SessionDetail.Email = _UserMaster.Username;
                            _SessionDetail.RoleId = _UserMaster.RoleId;
                            _SessionDetail.FullName = _UserMaster.Username;

                            _Result.Data = _SessionDetail;
                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            bool _Flag = true;
                            if (_UserMaster.EmployeeProfile != null)
                            {
                                if (_UserMaster.EmployeeProfile.IsActive.Value)
                                {
                                    SessionDetail _SessionDetail = new SessionDetail();
                                    _SessionDetail.UserID = _UserMaster.UserID;
                                    _SessionDetail.Email = _UserMaster.Username;
                                    _SessionDetail.RoleId = _UserMaster.RoleId;
                                    _SessionDetail.FullName = _UserMaster.EmployeeProfile.FirstName + " " + _UserMaster.EmployeeProfile.LastName;
                                    _SessionDetail.PhotoName = _UserMaster.EmployeeProfile.PicImg;
                                    _SessionDetail.EmployeeId = _UserMaster.EmployeeProfile.EmployeeId;
                                    _SessionDetail.EmployeeNo = _UserMaster.EmployeeProfile.EmployeeNo;
                                    //_SessionDetail.EmployeeDesignation = dbContext.DesignationMasters.Where(x => x.DesignationID == _UserMaster.EmployeeMaster.DesignationId).Select(s => "@" + s.Designation).FirstOrDefault();
                                    //_SessionDetail.EmployeeShift = dbContext.ShiftMasters.Where(x => x.ShiftID == _UserMaster.EmployeeMaster.ShiftId).Select(s => s.FromTime + " to " + s.ToTime).FirstOrDefault();

                                    _Result.Data = _SessionDetail;
                                    _Result.IsSuccess = true;

                                    _Flag = false;
                                }
                            }

                            if (_Flag)
                            {
                                _Result.Message = GlobalMsg.AuthenticationFailMsg;
                            }
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.AuthenticationFailMsg;
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

        public Result<List<UserModel>> GetAllUserList()
        {
            Result<List<UserModel>> _Result = new Result<List<UserModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.UserMasters
                                 where e.IsActive == true
                                 select new UserModel
                                 {
                                     UserID       = e.UserID,
                                     RoleId       = e.RoleId,
                                     RoleName     = e.RoleMaster.RoleName,
                                     //EmployeeId   = e.EmployeeId.Value,
                                     EmployeeNo   = e.EmployeeProfile.EmployeeNo,
                                     FirstName    = e.EmployeeProfile.FirstName,
                                     MiddleName   = e.EmployeeProfile.MiddleName,
                                     LastName     = e.EmployeeProfile.LastName,
                                     Username     = e.Username,
                                     Password     = e.Password,
                                     //LastLogin    = e.LastLogin.Value,
                                     //Token        = e.Token,
                                     //CreatedDate  = e.CreatedDate,
                                     //ModifiedDate = e.ModifiedDate,
                                     IsActive     = e.IsActive
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

        public Result<UserModel> GetUserById(Guid p_UserId)
        {
            Result<UserModel> _Result = new Result<UserModel>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.UserMasters
                                 where d.UserID == p_UserId
                                 select new UserModel
                                 {
                                     UserID         = d.UserID,
                                     WorkLocationId = d.EmployeeProfile.WorkLocationId,
                                     EmployeeId     = (d.EmployeeId.HasValue) ? d.EmployeeId.Value : Guid.Empty,
                                     RoleId         = d.RoleId,
                                     RoleName       = d.RoleMaster.RoleName,
                                     EmployeeNo     = d.EmployeeProfile.EmployeeNo,
                                     FirstName      = d.EmployeeProfile.FirstName,
                                     MiddleName     = d.EmployeeProfile.MiddleName,
                                     LastName       = d.EmployeeProfile.LastName,
                                     Username       = d.Username,
                                     Password       = d.Password,
                                     IsActive       = d.IsActive
                                 };

                    UserModel _UserModel = _Query.FirstOrDefault();
                    if (_UserModel != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _UserModel;
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

        public Result<String> CheckUserByUserName(string p_UserName)
        {
            Result<String> _Result = new Result<String>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(u => u.Username == p_UserName && u.IsActive == true).FirstOrDefault();

                    bool _Flag = true;

                    if (_UserMaster != null)
                    {
                        if (_UserMaster.EmployeeProfile != null)
                        {
                            if (_UserMaster.EmployeeProfile.IsActive.Value)
                            {
                                _Result.Id = Convert.ToString(_UserMaster.UserID);
                                _Result.Data = _UserMaster.EmployeeProfile.FirstName + " " + _UserMaster.EmployeeProfile.LastName;
                                _Result.IsSuccess = true;

                                _Flag = false;
                            }
                        }
                        else
                        {
                            _Result.Id = Convert.ToString(_UserMaster.UserID);
                            _Result.Data = _UserMaster.Username;
                            _Result.IsSuccess = true;

                            _Flag = false;
                        }
                    }

                    if (_Flag)
                    {
                        _Result.Message = GlobalMsg.UsernameNotExistsMsg;
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

        public Result<Boolean> ResetPassword(Guid p_UserId, string p_Password)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(u => u.UserID == p_UserId && u.IsActive == true).FirstOrDefault();

                    if (_UserMaster != null)
                    {
                        _UserMaster.ModifiedDate = DateTime.Now;
                        _UserMaster.ModifiedBy = _UserMaster.UserID;
                        _UserMaster.Password = p_Password;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.UsernameNotExistsMsg;
                    }

                    if (_Result.IsSuccess)
                    {
                        _Result.Data = true;
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

        public Result<Boolean> DeActivateUser(Guid p_UserId, bool isActive, Guid p_ModifiedId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(u => u.UserID == p_UserId && u.IsActive == true).FirstOrDefault();

                    if (_UserMaster != null)
                    {
                        _UserMaster.ModifiedDate = DateTime.Now;
                        _UserMaster.ModifiedBy = p_ModifiedId;
                        _UserMaster.IsActive = isActive;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.UsernameNotExistsMsg;
                    }

                    if (_Result.IsSuccess)
                    {
                        _Result.Data = true;
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

        public Result<Boolean> CreateUser(UserModel p_User, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;
                
                UserMaster _UserMaster = null;

                using (var dbContext = new ERPEntities())
                {
                    if(p_User.UserID == Guid.Empty)
                    {
                        _UserMaster = dbContext.UserMasters.Where(u => u.Username == p_User.Username && u.IsActive == true).FirstOrDefault();

                        if (_UserMaster == null)
                        {
                            _UserMaster = new UserMaster();

                            _UserMaster.UserID = Guid.NewGuid();
                            _UserMaster.RoleId = p_User.RoleId;
                            _UserMaster.EmployeeId = p_User.EmployeeId;
                            _UserMaster.Username = p_User.Username;
                            _UserMaster.Password = p_User.Password;
                            _UserMaster.CreatedDate = DateTime.Now;
                            _UserMaster.CreatedBy = p_UserId;
                            _UserMaster.ModifiedBy = null;
                            _UserMaster.ModifiedDate = DateTime.Now;
                            _UserMaster.IsActive = true;

                            dbContext.UserMasters.Add(_UserMaster);

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.AlreadyExistMsg;
                        }
                    } else
                    {
                        _UserMaster = dbContext.UserMasters.Where(u => u.UserID == p_User.UserID && u.IsActive == true).FirstOrDefault();

                        if(_UserMaster != null)
                        {
                            _UserMaster.RoleId = p_User.RoleId;
                            _UserMaster.EmployeeId = p_User.EmployeeId;
                            _UserMaster.Username = p_User.Username;
                            _UserMaster.Password = p_User.Password;
                            _UserMaster.ModifiedBy = p_UserId;
                            _UserMaster.ModifiedDate = DateTime.Now;
                            _UserMaster.IsActive = true;

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        } else
                        {
                            _Result.Message = GlobalMsg.UsernameNotExistsMsg;
                        }
                    }

                    if (_Result.IsSuccess)
                    {
                        _Result.Data = true;
                        _Result.Message = GlobalMsg.SaveSuccessMsg;
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

        public Result<Boolean> ChangePassword(Guid p_UserId, string p_OldPassword, string p_NewPassword)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(u => u.UserID == p_UserId && u.Password == p_OldPassword && u.IsActive == true).FirstOrDefault();

                    if (_UserMaster != null)
                    {
                        _UserMaster.ModifiedDate = DateTime.Now;
                        _UserMaster.ModifiedBy = _UserMaster.UserID;
                        _UserMaster.Password = p_NewPassword;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.OldPasswordNotMatchMsg;
                    }

                    if (_Result.IsSuccess)
                    {
                        _Result.Data = true;
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

        public Result<List<String>> GetAllAdminEmail()
        {
            Result<List<String>> _Result = new Result<List<String>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    Guid _RoleId = new Guid(GlobalHelper.GetEnumDescription(Role.Administrator));

                    _Result.Data = dbContext.UserMasters.Where(u => u.RoleId == _RoleId && u.IsActive == true).Select(u => u.Username).ToList();

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

        public Result<LoginResponse> UserLogin(LoginRequest p_LoginRequest)
        {
            Result<LoginResponse> _Result = new Result<LoginResponse>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(u => u.Username == p_LoginRequest.Username && u.Password == p_LoginRequest.Password && u.IsActive == true).FirstOrDefault();

                    if (_UserMaster != null)
                    {
                        _UserMaster.LastLogin = DateTime.Now;
                        dbContext.SaveChanges();

                        Guid _RoleId = new Guid(GlobalHelper.GetEnumDescription(Role.Administrator));

                        if (_UserMaster.RoleId == _RoleId)
                        {
                            LoginResponse _LoginResponse = new LoginResponse();
                            _LoginResponse.UserID = _UserMaster.UserID;
                            _LoginResponse.Email = _UserMaster.Username;
                            _LoginResponse.RoleId = _UserMaster.RoleId;
                            _LoginResponse.FullName = _UserMaster.Username;

                            _Result.Data = _LoginResponse;
                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            bool _Flag = true;
                            if (_UserMaster.EmployeeProfile != null)
                            {
                                if (_UserMaster.EmployeeProfile.IsActive.Value)
                                {
                                    LoginResponse _LoginResponse = new LoginResponse();
                                    _LoginResponse.UserID = _UserMaster.UserID;
                                    _LoginResponse.Email = _UserMaster.Username;
                                    _LoginResponse.RoleId = _UserMaster.RoleId;
                                    _LoginResponse.FullName = _UserMaster.EmployeeProfile.FirstName + " " + _UserMaster.EmployeeProfile.LastName;
                                    _LoginResponse.PhotoName = _UserMaster.EmployeeProfile.PicImg;
                                    _LoginResponse.EmployeeId = _UserMaster.EmployeeProfile.EmployeeId;
                                    _LoginResponse.EmployeeNo = _UserMaster.EmployeeProfile.EmployeeNo;

                                    _Result.Data = _LoginResponse;
                                    _Result.IsSuccess = true;

                                    _Flag = false;
                                }
                            }

                            if (_Flag)
                            {
                                _Result.Message = GlobalMsg.AuthenticationFailMsg;
                            }
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.AuthenticationFailMsg;
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

        public Result<bool> SaveDeviceRegistrationForNotification(DeviceRegistration p_DeviceRegistration)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    UserMaster _UserMaster = dbContext.UserMasters.Where(x => x.UserID == p_DeviceRegistration.UserId && x.IsActive == true).FirstOrDefault();
                    if (_UserMaster != null)
                    {
                        _UserMaster.Token = p_DeviceRegistration.Token;
                        dbContext.SaveChanges();
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
