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
                            if (_UserMaster.EmployeeMaster != null)
                            {
                                if (_UserMaster.EmployeeMaster.IsActive && _UserMaster.EmployeeMaster.IsLeave == false)
                                {
                                    SessionDetail _SessionDetail = new SessionDetail();
                                    _SessionDetail.UserID = _UserMaster.UserID;
                                    _SessionDetail.Email = _UserMaster.Username;
                                    _SessionDetail.RoleId = _UserMaster.RoleId;
                                    _SessionDetail.FullName = _UserMaster.EmployeeMaster.FirstName + " " + _UserMaster.EmployeeMaster.LastName;
                                    _SessionDetail.PhotoName = _UserMaster.EmployeeMaster.PhotoName;
                                    _SessionDetail.EmployeeId = _UserMaster.EmployeeMaster.EmployeeID;
                                    _SessionDetail.EmployeeNo = _UserMaster.EmployeeMaster.EmployeeNo;
                                    _SessionDetail.EmployeeDesignation = dbContext.DesignationMasters.Where(x => x.DesignationID == _UserMaster.EmployeeMaster.DesignationId).Select(s => "@" + s.Designation).FirstOrDefault();
                                    _SessionDetail.EmployeeShift = dbContext.ShiftMasters.Where(x => x.ShiftID == _UserMaster.EmployeeMaster.ShiftId).Select(s => s.FromTime + " to " + s.ToTime).FirstOrDefault();

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
                        if (_UserMaster.EmployeeMaster != null)
                        {
                            if (_UserMaster.EmployeeMaster.IsActive)
                            {
                                _Result.Id = Convert.ToString(_UserMaster.UserID);
                                _Result.Data = _UserMaster.EmployeeMaster.FirstName + " " + _UserMaster.EmployeeMaster.LastName;
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
                            if (_UserMaster.EmployeeMaster != null)
                            {
                                if (_UserMaster.EmployeeMaster.IsActive && _UserMaster.EmployeeMaster.IsLeave == false)
                                {
                                    LoginResponse _LoginResponse = new LoginResponse();
                                    _LoginResponse.UserID = _UserMaster.UserID;
                                    _LoginResponse.Email = _UserMaster.Username;
                                    _LoginResponse.RoleId = _UserMaster.RoleId;
                                    _LoginResponse.FullName = _UserMaster.EmployeeMaster.FirstName + " " + _UserMaster.EmployeeMaster.LastName;
                                    _LoginResponse.PhotoName = _UserMaster.EmployeeMaster.PhotoName;
                                    _LoginResponse.EmployeeId = _UserMaster.EmployeeMaster.EmployeeID;
                                    _LoginResponse.EmployeeNo = _UserMaster.EmployeeMaster.EmployeeNo;

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
