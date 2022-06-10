using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using zkemkeeper;

namespace ERP.Api.Controllers
{
    public class ERPController : BaseApiController
    {

        #region Variable

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFinancialYearService _IFinancialYearService = new FinancialYearService();
        private readonly IUserService _IUserService = new UserService();
        private readonly IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();
        private readonly IEmployeeAttendanceDeviceService _IEmployeeAttendanceDeviceService = new EmployeeAttendanceDeviceService();
        private readonly IEmployeeDeviceMapService _IEmployeeDeviceMapService = new EmployeeDeviceMapService();
        private readonly IDeviceService _IDeviceService = new DeviceService();
        private readonly ILookupService _ILookupService = new LookupService();


        #endregion

        #region MY API

        [HttpPost]
        public Result<List<FinancialYear>> GetFinacialYearList()
        {
            Result<List<FinancialYear>> _Result = new Result<List<FinancialYear>>();
            try
            {
                _Result = _IFinancialYearService.GetFinancialYearList();
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<LoginResponse> UserLogin(LoginRequest p_LoginRequest)
        {
            Result<LoginResponse> _Result = new Result<LoginResponse>();
            try
            {
                _Result = _IUserService.UserLogin(p_LoginRequest);
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<bool> ForgotPassword(ForgotPasswordRequest p_ForgotPasswordRequest)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                Result<string> _EmailResult = _IUserService.CheckUserByUserName(p_ForgotPasswordRequest.Username);
                if (_EmailResult.IsSuccess)
                {
                    bool _SendMail = false;
                    string _Body = string.Empty;
                    var _TemplatePath = HttpContext.Current.Server.MapPath("~/EmailTemplate/ForgotPassword.html");

                    using (var _StreamReader = new StreamReader(_TemplatePath))
                    {
                        _Body = _StreamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(_Body))
                    {
                        _Body = _Body.Replace("@UserFullName", _EmailResult.Data);
                        _Body = _Body.Replace("@UserName", p_ForgotPasswordRequest.Username);

                        var _CallbackUrl = ConfigurationManager.AppSettings["HRMUrl"] + "/Modules/ResetPassword.aspx?id=" + SecurityHelper.EncryptString(_Result.Id.ToString());

                        _Body = _Body.Replace("@ResetPasswordLink", _CallbackUrl);
                        _Body = _Body.Replace("@CompanyTeamName", GlobalMsg.CompanyTeamName);
                    }

                    List<string> _ToMails = new List<string>();
                    _ToMails.Add(p_ForgotPasswordRequest.Username);

                    _SendMail = EmailHelper.SendMail("Forgot Password", _Body, _ToMails);

                    if (_SendMail)
                    {
                        _Result.Message = GlobalMsg.SendLinkSuccessMsg;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }

            return _Result;
        }

        [HttpPost]
        public Result<List<EmployeeLeaveCategorys>> GetLeaveDetailList(Guid p_EmployeeId)
        {
            Result<List<EmployeeLeaveCategorys>> _Result = new Result<List<EmployeeLeaveCategorys>>();
            try
            {
                _Result = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryListByEmployeeId(p_EmployeeId);
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<bool> SaveLeaveApplicationDetail(EmployeeLeaveCategorys p_EmployeeLeaveCategorys)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result = _IEmployeeLeaveCategoryService.SaveApplyEmployeeLeaveCategory(p_EmployeeLeaveCategorys, p_EmployeeLeaveCategorys.UserID);

                if (_Result.IsSuccess)
                {
                    _Result.Message = String.Format(GlobalMsg.SaveSuccessMsg, "Apply Leave Application");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (p_EmployeeLeaveCategorys.EmployeeLeaveCategoryMapID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<EmployeeLeaveCategorys>(_Result.Id, TableType.EmployeeLeaveCategory, OperationType.Insert, p_EmployeeLeaveCategorys, p_EmployeeLeaveCategorys.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<EmployeeLeaveCategorys>(Convert.ToString(p_EmployeeLeaveCategorys.EmployeeLeaveCategoryMapID), TableType.EmployeeLeaveCategory, OperationType.Update, p_EmployeeLeaveCategorys, p_EmployeeLeaveCategorys.UserID);
                    }

                    string _Body = string.Empty;
                    var _TemplatePath = HttpContext.Current.Server.MapPath("~/EmailTemplate/ApplyLeave.html");

                    using (var _StreamReader = new StreamReader(_TemplatePath))
                    {
                        _Body = _StreamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(_Body))
                    {
                        _Body = _Body.Replace("@EmployeeNo", Convert.ToString(p_EmployeeLeaveCategorys.EmployeeNo));
                        _Body = _Body.Replace("@EmployeeName", p_EmployeeLeaveCategorys.EmployeeName);
                        _Body = _Body.Replace("@LeaveCategory", p_EmployeeLeaveCategorys.LeaveCategory);
                        _Body = _Body.Replace("@DateRange", p_EmployeeLeaveCategorys.StartDate.ToString("dd/MM/yyyy") + " " + p_EmployeeLeaveCategorys.EndDate.ToString("dd/MM/yyyy"));
                        _Body = _Body.Replace("@Reason", p_EmployeeLeaveCategorys.Reason);
                        _Body = _Body.Replace("@Status", string.IsNullOrEmpty(p_EmployeeLeaveCategorys.Status) ? "Pending" : p_EmployeeLeaveCategorys.Status);
                        _Body = _Body.Replace("@Reply", "");
                        _Body = _Body.Replace("@TotalLeave", p_EmployeeLeaveCategorys.TotalDay.ToString());
                        _Body = _Body.Replace("@SendDate", DateTime.Now.ToString());
                    }

                    List<string> _ToMail = new List<string>();
                    _ToMail.Add(p_EmployeeLeaveCategorys.Email);

                    IUserService _IUserService = new UserService();

                    Result<List<String>> _ResultEmail = _IUserService.GetAllAdminEmail();

                    if (_ResultEmail.IsSuccess)
                    {
                        foreach (string _Email in _ResultEmail.Data)
                        {
                            _ToMail.Add(_Email);
                        }
                    }

                    string _Subject = "Apply Leave";

                    if (!string.IsNullOrEmpty(p_EmployeeLeaveCategorys.Status))
                    {
                        _Subject = "Re-Apply Leave";
                    }

                    EmailHelper.SendMail(_Subject, _Body, _ToMail);
                }
                else
                {
                    _Result.Message = String.Format(_Result.Message, "Apply Leave Application");
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<EmployeeLeaveCategorys> GetLeaveApplicationDetail(Guid p_EmployeeLeaveCategoryId)
        {
            Result<EmployeeLeaveCategorys> _Result = new Result<EmployeeLeaveCategorys>();
            try
            {
                _Result = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryById(p_EmployeeLeaveCategoryId);
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<bool> SaveAttendance(AttendanceRequest p_AttendanceRequest)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result = _IEmployeeAttendanceDeviceService.SaveAttendance(p_AttendanceRequest);
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<bool> Registration_FCM(DeviceRegistration p_DeviceRegistrationRequest)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result = _IUserService.SaveDeviceRegistrationForNotification(p_DeviceRegistrationRequest);
            }
            catch (Exception _Exception)
            {
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<List<Item>> GetCountryList()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();
            try
            {
                _Result = _ILookupService.GetAllCountry();
            }
            catch (Exception _Exception)
            {
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        [HttpPost]
        public Result<List<Item>> GetStateList(Guid CountryId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();
            try
            {
                _Result = _ILookupService.GetAllStateByCountryId(CountryId);
            }
            catch (Exception _Exception)
            {
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return _Result;
        }

        #endregion

        //[HttpPost]
        //public Result<bool> InsertAttendance()
        //{
        //    Result<bool> _Result = new Result<bool>();
        //    try
        //    {
        //        _Result = _IEmployeeAttendanceDeviceService.InsertAttendance();
        //    }
        //    catch (Exception _Exception)
        //    {
        //        _Result.Message = GlobalMsg.ExceptionErrMsg;
        //        _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
        //    }
        //    return _Result;
        //}

        //[HttpPost]
        //public Result<bool> SaveCollectAttendance()
        //{
        //    CZKEM CtrlBioComm = new CZKEM();
        //    Result<bool> _Results = new Result<bool>();
        //    string _ErrorMessage = string.Empty;

        //    DataTable _DataTable = null;
        //    _DataTable = new DataTable();
        //    _DataTable.Columns.Add("_terminalID");
        //    _DataTable.Columns.Add("_enrollNumber");
        //    _DataTable.Columns.Add("_verifyMode");
        //    _DataTable.Columns.Add("_inOutMode");
        //    _DataTable.Columns.Add("_date", typeof(DateTime));
        //    _DataTable.Columns.Add("_hour");
        //    _DataTable.Columns.Add("_minute");
        //    _DataTable.Columns.Add("_second");
        //    _DataTable.Columns.Add("_workcode");
        //    _DataTable.Columns.Add("_fulldate", typeof(DateTime));

        //    #region CONNECT DEVICE

        //    Result<List<DeviceModel>> _Result = _IDeviceService.GetDeviceList();

        //    if (_Result.IsSuccess)
        //    {
        //        foreach (DeviceModel _DeviceItem in _Result.Data)
        //        {
        //            GlobalHelper.Connect(_DeviceItem.IPAddress);
        //        }
        //        string _NetworkDevices = IPInfo.GetARPResult();

        //        if (_Result.Data.Where(q => _NetworkDevices.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").Count() != 0)
        //        {
        //            List<DeviceModel> _ListOfSelectDevice = _Result.Data.Where(q => _NetworkDevices.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").ToList();

        //            foreach (DeviceModel _DeviceItem in _ListOfSelectDevice)
        //            {
        //                bool _Connected = false;

        //                _Connected = CtrlBioComm.Connect_Net(_DeviceItem.IPAddress, Convert.ToInt32(_DeviceItem.Port));

        //                if (_Connected)
        //                {
        //                    if (CtrlBioComm.IsTFTMachine(1))
        //                    {
        //                        _ErrorMessage = GetAllAndUpdateData(_DeviceItem, true, _ErrorMessage, ref _DataTable);
        //                    }
        //                    else
        //                    {
        //                        _ErrorMessage = GetAllAndUpdateData(_DeviceItem, false, _ErrorMessage, ref _DataTable);
        //                    }

        //                    CtrlBioComm.Disconnect();

        //                    _ErrorMessage = _ErrorMessage + InsertDeviceAttendance(_DataTable, DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(1), _DeviceItem.DeviceID);
        //                }
        //                else
        //                {
        //                    if (_ErrorMessage == "")
        //                    {
        //                        _ErrorMessage = "Unable to connect " + _DeviceItem.DeviceName + " device";
        //                    }
        //                    else
        //                    {
        //                        _ErrorMessage += "\nUnable to connect " + _DeviceItem.DeviceName + " device.";
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            _ErrorMessage = "Unable to connect device";
        //        }
        //    }

        //    #endregion
        //    if (!string.IsNullOrEmpty(_ErrorMessage))
        //    {
        //        _Results.IsSuccess = false;
        //        _Results.Message = _ErrorMessage;
        //    }
        //    else
        //    {
        //        _Results.IsSuccess = true;
        //    }
        //    return _Results;
        //}

        //public string GetAllAndUpdateData(DeviceModel p_Device, bool p_IsTFT, string p_ErrorMessage, ref DataTable p_DataTable)
        //{
        //    CZKEM CtrlBioComm = new CZKEM();

        //    #region FOR MEMBER ATTENDANCE

        //    int _errorCode = 0, _verifyMode = 0, _inOutMode = 0, _year = 0, _month = 0, _day = 0, _hour = 0, _minute = 0, _second = 0, _workcode = 0, _reserved = 0, _dwenrollNumber = 0;
        //    string _enrollNumber = "";
        //    if (CtrlBioComm.ReadGeneralLogData(1))
        //    {
        //        CtrlBioComm.GetLastError(ref _errorCode);
        //        while (_errorCode != 0)
        //        {
        //            if (p_IsTFT)
        //            {
        //                #region TFT MACHINES

        //                if (CtrlBioComm.SSR_GetGeneralLogData(1, out _enrollNumber, out _verifyMode, out _inOutMode, out _year, out _month, out _day, out _hour, out _minute, out _second, ref _workcode))
        //                {
        //                    DataRow newrow = p_DataTable.NewRow();
        //                    newrow["_terminalID"] = p_Device.DeviceID;
        //                    newrow["_enrollNumber"] = _enrollNumber;
        //                    newrow["_verifyMode"] = _verifyMode;
        //                    newrow["_inOutMode"] = _inOutMode;
        //                    newrow["_date"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " 00:00:00 AM");
        //                    newrow["_hour"] = _hour;
        //                    newrow["_minute"] = _minute;
        //                    newrow["_second"] = _second;
        //                    newrow["_workcode"] = _workcode;
        //                    newrow["_fulldate"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " " + _hour + ":" + _minute + ":" + _second);
        //                    p_DataTable.Rows.Add(newrow);
        //                }

        //                #endregion
        //            }
        //            else
        //            {
        //                #region OTHER MACHINES

        //                if (CtrlBioComm.GetGeneralExtLogData(1, ref _dwenrollNumber, ref _verifyMode, ref _inOutMode, ref _year, ref _month, ref _day, ref _hour, ref _minute, ref _second, ref _workcode, ref _reserved))
        //                {
        //                    DataRow newrow = p_DataTable.NewRow();
        //                    newrow["_terminalID"] = p_Device.DeviceID;
        //                    newrow["_enrollNumber"] = _dwenrollNumber;
        //                    newrow["_verifyMode"] = _verifyMode;
        //                    newrow["_inOutMode"] = _inOutMode;
        //                    newrow["_date"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " 00:00:00 AM");
        //                    newrow["_hour"] = _hour;
        //                    newrow["_minute"] = _minute;
        //                    newrow["_second"] = _second;
        //                    newrow["_workcode"] = _workcode;
        //                    newrow["_fulldate"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " " + _hour + ":" + _minute + ":" + _second);
        //                    p_DataTable.Rows.Add(newrow);
        //                }

        //                #endregion
        //            }
        //            CtrlBioComm.GetLastError(ref _errorCode);
        //        }
        //    }
        //    else
        //    {
        //        #region ERROR WHILE COOLECTING DATA

        //        CtrlBioComm.GetLastError(ref _errorCode);
        //        if (_errorCode != 0)
        //        {
        //            if (p_ErrorMessage == " ")
        //            {
        //                p_ErrorMessage = "Unable to collect data from " + p_Device.DeviceName + " device,  Error Code: " + Convert.ToString(_errorCode);
        //            }
        //            else
        //            {
        //                p_ErrorMessage += "\nUnable to collect data from " + p_Device.DeviceName + " device,  Error Code: " + Convert.ToString(_errorCode);
        //            }
        //        }

        //        #endregion
        //    }

        //    #endregion

        //    return p_ErrorMessage;
        //}

        //public string InsertDeviceAttendance(DataTable p_DataTable, DateTime p_FromDate, DateTime p_ToDate, Guid p_DeviceId)
        //{
        //    string p_ErrorMessage = "";
        //    if (p_DataTable != null && p_DataTable.Rows.Count > 0)
        //    {
        //        DataRow[] _Rows = null;

        //        _Rows = p_DataTable.AsEnumerable().Where(q => (Convert.ToDateTime(q["_date"]).Date >= p_FromDate) && (Convert.ToDateTime(q["_date"]).Date <= p_ToDate)).OrderBy(q => q.Field<DateTime>("_fulldate")).ToArray();

        //        if (_Rows != null && _Rows.Count() > 0)
        //        {
        //            Result<List<EmployeeDeviceMapModel>> _ResultEmployeeEnroll = _IEmployeeDeviceMapService.GetEmployeeEnrolls(p_DeviceId);

        //            if (_ResultEmployeeEnroll.IsSuccess)
        //            {
        //                if (_ResultEmployeeEnroll.Data != null && _ResultEmployeeEnroll.Data.Count > 0)
        //                {
        //                    foreach (DataRow dr in _Rows)
        //                    {
        //                        EmployeeAttendanceDeviceModel _EmployeeAttendance = new EmployeeAttendanceDeviceModel();

        //                        _EmployeeAttendance.EnrollNo = Convert.ToString(dr["_enrollNumber"]);
        //                        _EmployeeAttendance.DeviceId = p_DeviceId;
        //                        _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.IN);
        //                        _EmployeeAttendance.AttendanceDate = Convert.ToDateTime(dr["_date"]);
        //                        _EmployeeAttendance.PunchTime = Convert.ToString(dr["_hour"]) + ":" + Convert.ToString(dr["_minute"]) + ":" + Convert.ToString(dr["_second"]);
        //                        _EmployeeAttendance.VerifyMethod = Convert.ToString(dr["_verifyMode"]);
        //                        _EmployeeAttendance.AttendanceDateTime = Convert.ToDateTime(dr["_fulldate"]);
        //                        _EmployeeAttendance.PunchType = Convert.ToString(PunchType.DEVICE);

        //                        var _DataRow = _ResultEmployeeEnroll.Data.Where(q => q.EnrollmentNo == _EmployeeAttendance.EnrollNo).ToArray();

        //                        bool _IsAllow = true;
        //                        if (_DataRow != null && _DataRow.Count() > 0)
        //                        {
        //                            _EmployeeAttendance.EmployeeId = Guid.Parse(Convert.ToString(_DataRow[0].EmployeeId));
        //                        }
        //                        else
        //                        {
        //                            _IsAllow = false;
        //                        }

        //                        _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.IN);

        //                        if (_IsAllow)
        //                        {
        //                            Result<List<string>> _ResultAttendanceEmployee = _IEmployeeAttendanceDeviceService.GetEmployeeAttendanceTimeByEmpoyeeIdAndDate(_EmployeeAttendance.EmployeeId, Convert.ToDateTime(dr["_date"]));

        //                            if (_ResultAttendanceEmployee.IsSuccess)
        //                            {
        //                                if (_ResultAttendanceEmployee.Data != null)
        //                                {
        //                                    if (_ResultAttendanceEmployee.Data.Where(x => x.Contains(Convert.ToString(_EmployeeAttendance.PunchTime))).Count() > 0)
        //                                    {
        //                                        _IsAllow = false;
        //                                    }
        //                                    else
        //                                    {
        //                                        if ((_ResultAttendanceEmployee.Data.Count % 2) == 0)
        //                                        {
        //                                            _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.IN);
        //                                        }
        //                                        else
        //                                        {
        //                                            _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.OUT);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        if (_IsAllow)
        //                        {
        //                            Result<Guid> _ResultShift = _IEmployeeAttendanceDeviceService.GetShiftByEmployeeId(_EmployeeAttendance.EmployeeId);
        //                            if (_ResultShift.IsSuccess)
        //                            {
        //                                _EmployeeAttendance.ShiftId = _ResultShift.Data;
        //                            }
        //                            //Insert Attendance

        //                            Result<bool> _ResultSave = _IEmployeeAttendanceDeviceService.SaveEmployeeAttendance(_EmployeeAttendance);

        //                            if (!_ResultSave.IsSuccess)
        //                            {
        //                                p_ErrorMessage = _ResultSave.Message;
        //                            }
        //                        }
        //                    }
        //                }
        //            }


        //        }
        //        //else
        //        //{
        //        //    p_ErrorMessage = Messages.NoRecordMsg;
        //        //}
        //    }
        //    //else
        //    //{
        //    //    p_ErrorMessage = Messages.NoRecordMsg;
        //    //}

        //    return p_ErrorMessage;
        //}

    }
}