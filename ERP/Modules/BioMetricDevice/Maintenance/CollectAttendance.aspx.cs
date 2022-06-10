using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using zkemkeeper;

namespace ERP.Modules.BioMetricDevice.Maintenance
{
    public partial class CollectAttendance : System.Web.UI.Page
    {
        #region Variables

        string _ErrorMessage = "";
        private IDeviceService _IDeviceService = new DeviceService();
        private IEmployeeService _IEmployeeService = new EmployeeService();
        private IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();
        private IEmployeeDeviceMapService _IEmployeeDeviceMapService = new EmployeeDeviceMapService();
        private IEmployeeAttendanceDeviceService _IEmployeeAttendanceDeviceService = new EmployeeAttendanceDeviceService();
        private IShiftService _IShiftService = new ShiftService();
        private IHolidayService _IHolidayService = new HolidayService();
        private IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

        private CZKEM CtrlBioComm = new CZKEM();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liCollectAttendance_liBioMetricDevice_liMaintenance";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }

        #endregion


        #region Events

        protected void btnCollect_Click(object sender, EventArgs e)
        {
            string ErrorMessage = "";
            var _Date = txtDate.Value;
            if (!string.IsNullOrEmpty(_Date))
            {
                DateTime _FromDate = GlobalHelper.StringToDate(_Date.Split('-')[0]);
                DateTime _ToDate = GlobalHelper.StringToDate(_Date.Split('-')[1]);
                ErrorMessage = SaveCollectAttendance(_FromDate, _ToDate);
                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + ErrorMessage + "');});", true);
                }
                else
                {
                    ErrorMessage = "Collect Attendance Successfully.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + ErrorMessage + "');});", true);
                }
            }
        }

        #endregion

        #region Private Methods

        private string SaveCollectAttendance(DateTime _FromDate, DateTime _ToDate)
        {
            DataTable _DataTable = null;
            _DataTable = new DataTable();
            _DataTable.Columns.Add("_terminalID");
            _DataTable.Columns.Add("_enrollNumber");
            _DataTable.Columns.Add("_verifyMode");
            _DataTable.Columns.Add("_inOutMode");
            _DataTable.Columns.Add("_date", typeof(DateTime));
            _DataTable.Columns.Add("_hour");
            _DataTable.Columns.Add("_minute");
            _DataTable.Columns.Add("_second");
            _DataTable.Columns.Add("_workcode");
            _DataTable.Columns.Add("_fulldate", typeof(DateTime));

            #region CONNECT DEVICE

            if (SessionHelper.DeviceSessionDetail != null)
            {
                bool _Connected = false;

                _Connected = CtrlBioComm.Connect_Net(SessionHelper.DeviceSessionDetail.IPAddress, SessionHelper.DeviceSessionDetail.Port);

                if (_Connected)
                {
                    Result<DeviceModel> _Result = _IDeviceService.GetDeViceById(SessionHelper.DeviceSessionDetail.DeviceId);
                    if (CtrlBioComm.IsTFTMachine(1))
                    {
                        _ErrorMessage = GetAllAndUpdateData(_Result.Data, true, _ErrorMessage, ref _DataTable);
                    }
                    else
                    {
                        _ErrorMessage = GetAllAndUpdateData(_Result.Data, false, _ErrorMessage, ref _DataTable);
                    }

                    CtrlBioComm.Disconnect();
                    SessionHelper.RemoveDeviceSessionDetail();

                    _ErrorMessage = _ErrorMessage + InsertAttendance(_DataTable, _FromDate, _ToDate, _Result.Data.DeviceID);
                }
                else
                {
                    if (_ErrorMessage == "")
                    {
                        _ErrorMessage = "Unable to connect " + SessionHelper.DeviceSessionDetail.DeviceName + " device";
                    }
                    else
                    {
                        _ErrorMessage += "\nUnable to connect " + SessionHelper.DeviceSessionDetail.DeviceName + " device.";
                    }
                }
            }
            else
            {
                _ErrorMessage = "Unable to connect device , please Check Network / Firewall ";
            }

            #region Old Code

            //Result<List<DeviceModel>> _Result = _IDeviceService.GetDeviceList();

            //if (_Result.IsSuccess)
            //{
            //    foreach (DeviceModel _DeviceItem in _Result.Data)
            //    {
            //        GlobalHelper.Connect(_DeviceItem.IPAddress);
            //    }
            //    string _NetworkDevices = IPInfo.GetARPResult();

            //    if (_Result.Data.Where(q => _NetworkDevices.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").Count() != 0)
            //    {
            //        List<DeviceModel> _ListOfSelectDevice = _Result.Data.Where(q => _NetworkDevices.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").ToList();

            //        foreach (DeviceModel _DeviceItem in _ListOfSelectDevice)
            //        {
            //            bool _Connected = false;

            //            _Connected = CtrlBioComm.Connect_Net(_DeviceItem.IPAddress, Convert.ToInt32(_DeviceItem.Port));

            //            if (_Connected)
            //            {
            //                if (CtrlBioComm.IsTFTMachine(1))
            //                {
            //                    _ErrorMessage = GetAllAndUpdateData(_DeviceItem, true, _ErrorMessage, ref _DataTable);
            //                }
            //                else
            //                {
            //                    _ErrorMessage = GetAllAndUpdateData(_DeviceItem, false, _ErrorMessage, ref _DataTable);
            //                }

            //                CtrlBioComm.Disconnect();

            //                _ErrorMessage = _ErrorMessage + InsertAttendance(_DataTable, _FromDate, _ToDate, _DeviceItem.DeviceID);
            //            }
            //            else
            //            {
            //                if (_ErrorMessage == "")
            //                {
            //                    _ErrorMessage = "Unable to connect " + _DeviceItem.DeviceName + " device";
            //                }
            //                else
            //                {
            //                    _ErrorMessage += "\nUnable to connect " + _DeviceItem.DeviceName + " device.";
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        _ErrorMessage = "Unable to connect device";
            //    }
            //} 

            #endregion

            #endregion

            return _ErrorMessage;
        }

        private string GetAllAndUpdateData(DeviceModel p_Device, bool p_IsTFT, string p_ErrorMessage, ref DataTable p_DataTable)
        {
            #region FOR MEMBER ATTENDANCE

            int _errorCode = 0, _verifyMode = 0, _inOutMode = 0, _year = 0, _month = 0, _day = 0, _hour = 0, _minute = 0, _second = 0, _workcode = 0, _reserved = 0, _dwenrollNumber = 0;
            string _enrollNumber = "";
            if (CtrlBioComm.ReadGeneralLogData(1))
            {
                CtrlBioComm.GetLastError(ref _errorCode);
                while (_errorCode != 0)
                {
                    if (p_IsTFT)
                    {
                        #region TFT MACHINES
                        if (CtrlBioComm.SSR_GetGeneralLogData(1, out _enrollNumber, out _verifyMode, out _inOutMode, out _year, out _month, out _day, out _hour, out _minute, out _second, ref _workcode))
                        {
                            DataRow newrow = p_DataTable.NewRow();
                            newrow["_terminalID"] = p_Device.DeviceID;
                            newrow["_enrollNumber"] = _enrollNumber;
                            newrow["_verifyMode"] = _verifyMode;
                            newrow["_inOutMode"] = _inOutMode;
                            newrow["_date"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " 00:00:00 AM");
                            newrow["_hour"] = _hour;
                            newrow["_minute"] = _minute;
                            newrow["_second"] = _second;
                            newrow["_workcode"] = _workcode;
                            newrow["_fulldate"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " " + _hour + ":" + _minute + ":" + _second);
                            p_DataTable.Rows.Add(newrow);
                        }
                        #endregion
                    }
                    else
                    {
                        #region OTHER MACHINES
                        if (CtrlBioComm.GetGeneralExtLogData(1, ref _dwenrollNumber, ref _verifyMode, ref _inOutMode, ref _year, ref _month, ref _day, ref _hour, ref _minute, ref _second, ref _workcode, ref _reserved))
                        {
                            DataRow newrow = p_DataTable.NewRow();
                            newrow["_terminalID"] = p_Device.DeviceID;
                            newrow["_enrollNumber"] = _dwenrollNumber;
                            newrow["_verifyMode"] = _verifyMode;
                            newrow["_inOutMode"] = _inOutMode;
                            newrow["_date"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " 00:00:00 AM");
                            newrow["_hour"] = _hour;
                            newrow["_minute"] = _minute;
                            newrow["_second"] = _second;
                            newrow["_workcode"] = _workcode;
                            newrow["_fulldate"] = Convert.ToDateTime(_year + "-" + _month + "-" + _day + " " + _hour + ":" + _minute + ":" + _second);
                            p_DataTable.Rows.Add(newrow);
                        }
                        #endregion
                    }
                    CtrlBioComm.GetLastError(ref _errorCode);
                }
            }
            else
            {
                #region ERROR WHILE COOLECTING DATA
                CtrlBioComm.GetLastError(ref _errorCode);
                if (_errorCode != 0)
                {
                    if (p_ErrorMessage == " ")
                    {
                        p_ErrorMessage = "Unable to collect data from " + p_Device.DeviceName + " device,  Error Code: " + Convert.ToString(_errorCode);
                    }
                    else
                    {
                        p_ErrorMessage += "\nUnable to collect data from " + p_Device.DeviceName + " device,  Error Code: " + Convert.ToString(_errorCode);
                    }
                }
                #endregion
            }
            #endregion

            return p_ErrorMessage;
        }

        private string InsertAttendance(DataTable p_DataTable, DateTime p_FromDate, DateTime p_ToDate, Guid p_DeviceId)
        {
            string p_ErrorMessage = "";
            List<Guid> _ListOfEmployeeId = new List<Guid>();
            if (p_DataTable != null && p_DataTable.Rows.Count > 0)
            {
                DataRow[] _Rows = null;

                _Rows = p_DataTable.AsEnumerable().Where(q => (Convert.ToDateTime(q["_date"]).Date >= p_FromDate) && (Convert.ToDateTime(q["_date"]).Date <= p_ToDate)).OrderBy(q => q.Field<DateTime>("_fulldate")).ToArray();

                if (_Rows != null && _Rows.Count() > 0)
                {
                    Result<List<EmployeeDeviceMapModel>> _ResultEmployeeEnroll = _IEmployeeDeviceMapService.GetEmployeeEnrolls(p_DeviceId);

                    if (_ResultEmployeeEnroll.IsSuccess)
                    {
                        if (_ResultEmployeeEnroll.Data != null && _ResultEmployeeEnroll.Data.Count > 0)
                        {
                            foreach (DataRow dr in _Rows)
                            {
                                EmployeeAttendanceDeviceModel _EmployeeAttendance = new EmployeeAttendanceDeviceModel();

                                _EmployeeAttendance.EnrollNo = Convert.ToString(dr["_enrollNumber"]);
                                _EmployeeAttendance.DeviceId = p_DeviceId;
                                _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.IN);
                                _EmployeeAttendance.AttendanceDate = Convert.ToDateTime(dr["_date"]);
                                _EmployeeAttendance.PunchTime = Convert.ToString(dr["_hour"]) + ":" + Convert.ToString(dr["_minute"]) + ":" + Convert.ToString(dr["_second"]);
                                _EmployeeAttendance.VerifyMethod = Convert.ToString(dr["_verifyMode"]);
                                _EmployeeAttendance.AttendanceDateTime = Convert.ToDateTime(dr["_fulldate"]);
                                _EmployeeAttendance.PunchType = Convert.ToString(PunchType.DEVICE);

                                var _DataRow = _ResultEmployeeEnroll.Data.Where(q => q.EnrollmentNo == _EmployeeAttendance.EnrollNo).ToArray();

                                bool _IsAllow = true;
                                if (_DataRow != null && _DataRow.Count() > 0)
                                {
                                    _EmployeeAttendance.EmployeeId = Guid.Parse(Convert.ToString(_DataRow[0].EmployeeId));
                                    _ListOfEmployeeId.Add(_EmployeeAttendance.EmployeeId);
                                }
                                else
                                {
                                    _IsAllow = false;
                                }

                                _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.IN);

                                if (_IsAllow)
                                {
                                    Result<List<string>> _ResultAttendanceEmployee = _IEmployeeAttendanceDeviceService.GetEmployeeAttendanceTimeByEmpoyeeIdAndDate(_EmployeeAttendance.EmployeeId, Convert.ToDateTime(dr["_date"]));

                                    if (_ResultAttendanceEmployee.IsSuccess)
                                    {
                                        if (_ResultAttendanceEmployee.Data != null)
                                        {
                                            if (_ResultAttendanceEmployee.Data.Where(x => x.Contains(Convert.ToString(_EmployeeAttendance.PunchTime))).Count() > 0)
                                            {
                                                _IsAllow = false;
                                            }
                                            else
                                            {
                                                if ((_ResultAttendanceEmployee.Data.Count % 2) == 0)
                                                {
                                                    _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.IN);
                                                }
                                                else
                                                {
                                                    _EmployeeAttendance.PunchMethod = Convert.ToString(PunchMethod.OUT);
                                                }
                                            }
                                        }
                                    }
                                }

                                if (_IsAllow)
                                {
                                    Result<Guid> _ResultShift = _IEmployeeAttendanceDeviceService.GetShiftByEmployeeId(_EmployeeAttendance.EmployeeId);
                                    if (_ResultShift.IsSuccess)
                                    {
                                        _EmployeeAttendance.ShiftId = _ResultShift.Data;
                                    }
                                    //Insert Attendance

                                    Result<bool> _ResultSave = _IEmployeeAttendanceDeviceService.SaveEmployeeAttendance(_EmployeeAttendance);

                                    if (!_ResultSave.IsSuccess)
                                    {
                                        p_ErrorMessage = _ResultSave.Message;
                                    }

                                }
                            }
                            if (_ListOfEmployeeId.Count > 0)
                            {
                                List<EmployeeAttendances> _ListOfEmployeeAttendanceResult = new List<EmployeeAttendances>();
                                foreach (var item in _ListOfEmployeeId)
                                {

                                    decimal _TotalWorkingHours = 0;
                                    Result<List<EmployeeAttendanceDevices>> _Result = _IEmployeeAttendanceDeviceService.GetEmployeeAttendanceDeviceByEmployeeId(item, p_FromDate, p_ToDate);
                                    Result<List<EmployeeAttendances>> _ResultManual = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(item, p_FromDate, p_ToDate);

                                    Result<Shift> _ShiftResult = _IShiftService.GetShiftByEmployeeId(item);

                                    DateTime start = DateTime.Parse(_ShiftResult.Data.FromTime);
                                    DateTime end = DateTime.Parse(_ShiftResult.Data.ToTime);
                                    if (start > end)
                                    {
                                        end = end.AddDays(1);
                                        TimeSpan duration = end.Subtract(start);
                                        _TotalWorkingHours = Convert.ToDecimal(duration.Hours.ToString() + "." + duration.Minutes.ToString());
                                    }
                                    else
                                    {
                                        var _diffTime = TimeSpan.FromMinutes((DateTime.Parse(_ShiftResult.Data.ToTime).Subtract(DateTime.Parse(_ShiftResult.Data.FromTime))).TotalMinutes);
                                        _TotalWorkingHours = Convert.ToDecimal(_diffTime.Hours.ToString() + "." + _diffTime.Minutes.ToString());
                                    }

                                    for (DateTime date = p_FromDate; date <= p_ToDate; date = date.AddDays(1.0))
                                    {
                                        EmployeeAttendances _EmployeeAttendanceResult = new EmployeeAttendances();

                                        _EmployeeAttendanceResult.AttendanceDate = new DateTime(date.Year, date.Month, date.Day);
                                        decimal _WorkingHours = 0;
                                        decimal _OvertimeHours = 0;
                                        _WorkingHours = _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(s => s.WorkingHours ?? 0).FirstOrDefault();
                                        _OvertimeHours = _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(s => s.OverTimeHours ?? 0).FirstOrDefault();
                                        if (_WorkingHours == 0)
                                        {
                                            double _WorkingMinutes = 0;
                                            var _ListofPunchtime = _Result.Data.Where(r => r.AttendanceDate == date && r.PunchType != "MANUAL").OrderBy(r => r.AttendanceDateTime).Select(r => r.PunchTime).ToList();
                                            if (_ListofPunchtime.Count % 2 == 0)
                                            {
                                                for (int i = 0; i < _ListofPunchtime.Count; i += 2)
                                                {
                                                    if (_WorkingMinutes == 0)
                                                    {
                                                        _WorkingMinutes = (DateTime.Parse(_ListofPunchtime[i + 1]).Subtract(DateTime.Parse(_ListofPunchtime[i]))).TotalMinutes;
                                                    }
                                                    else
                                                    {
                                                        _WorkingMinutes = _WorkingMinutes + (DateTime.Parse(_ListofPunchtime[i + 1]).Subtract(DateTime.Parse(_ListofPunchtime[i]))).TotalMinutes;
                                                    }
                                                }
                                                var _Time = TimeSpan.FromMinutes(_WorkingMinutes);
                                                if (_Time != null)
                                                {
                                                    _WorkingHours = Convert.ToDecimal(_Time.Hours.ToString() + "." + _Time.Minutes.ToString());
                                                }
                                            }
                                        }
                                        if (_WorkingHours > _TotalWorkingHours)
                                        {
                                            _EmployeeAttendanceResult.WorkingHours = Convert.ToDecimal(_TotalWorkingHours.ToString("0.00"));
                                            if (_OvertimeHours > 0)
                                            {
                                                _EmployeeAttendanceResult.OverTimeHours = _OvertimeHours;
                                            }
                                            else
                                            {
                                                _EmployeeAttendanceResult.OverTimeHours = _WorkingHours - _TotalWorkingHours;
                                            }
                                        }
                                        else
                                        {
                                            bool _Flag = true;
                                            Result<List<Holiday>> _HolidayResult = _IHolidayService.GetHolidayListByDate(p_FromDate, p_ToDate);

                                            Result<List<EmployeeLeaveCategorys>> _LeaveResult = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryListByDate(item, p_FromDate, p_ToDate);
                                            if (_LeaveResult.IsSuccess)
                                            {
                                                EmployeeLeaveCategorys _EmployeeLeaveCategorys = _LeaveResult.Data.Where(h => h.StartDate <= date && h.EndDate >= date).FirstOrDefault();

                                                if (_EmployeeLeaveCategorys != null)
                                                {
                                                    _Flag = false;
                                                    _EmployeeAttendanceResult.AttendanceType = Convert.ToInt32(AttendanceType.Leave);

                                                    if (_EmployeeLeaveCategorys.IsFirstHalfDay && _EmployeeLeaveCategorys.StartDate.Date == date.Date)
                                                    {
                                                        _EmployeeAttendanceResult.Attendance = Convert.ToDecimal("0.50");
                                                    }
                                                    else if (_EmployeeLeaveCategorys.IsLastHalfDay && _EmployeeLeaveCategorys.EndDate.Date == date.Date)
                                                    {
                                                        _EmployeeAttendanceResult.Attendance = Convert.ToDecimal("0.50");
                                                    }
                                                    else
                                                    {
                                                        _EmployeeAttendanceResult.Attendance = Convert.ToDecimal("1.00");
                                                    }

                                                    _EmployeeAttendanceResult.Description = _EmployeeLeaveCategorys.LeaveCategory.ToString();
                                                }
                                            }

                                            if (_Flag)
                                            {
                                                if (_HolidayResult.IsSuccess)
                                                {
                                                    Holiday _Holiday = _HolidayResult.Data.Where(h => h.StartDate <= date && h.EndDate >= date).FirstOrDefault();

                                                    if (_Holiday != null)
                                                    {
                                                        _Flag = false;
                                                        _EmployeeAttendanceResult.AttendanceType = Convert.ToInt32(AttendanceType.Holiday);
                                                        _EmployeeAttendanceResult.Description = _Holiday.Title;
                                                    }
                                                }
                                            }
                                            if (_Flag)
                                            {
                                                _EmployeeAttendanceResult.WorkingHours = Convert.ToDecimal(_WorkingHours.ToString("0.00"));
                                            }
                                        }
                                        _EmployeeAttendanceResult.FinancialYearId = SessionHelper.SessionDetail.FinancialYearId;
                                        _EmployeeAttendanceResult.EmployeeId = item;
                                        _EmployeeAttendanceResult.TimeIn = _ShiftResult.Data.FromTime;
                                        _EmployeeAttendanceResult.TimeOut = _ShiftResult.Data.ToTime;
                                        _EmployeeAttendanceResult.AttendanceType = (int)AttendanceType.Present;
                                        decimal _halfDay = _WorkingHours / 2;
                                        if (_TotalWorkingHours > _halfDay)
                                        {
                                            _EmployeeAttendanceResult.Attendance = 1;
                                        }
                                        else
                                        {
                                            _EmployeeAttendanceResult.Attendance = Convert.ToDecimal(0.5);
                                        }
                                        _EmployeeAttendanceResult.Description = _EmployeeAttendanceResult.AttendanceType == Convert.ToInt32(AttendanceType.Leave) || _EmployeeAttendanceResult.AttendanceType == Convert.ToInt32(AttendanceType.Holiday) ? _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(a => a.Description).FirstOrDefault() : "";

                                        _ListOfEmployeeAttendanceResult.Add(_EmployeeAttendanceResult);
                                    }
                                }
                                Result<Boolean> _Results = _IEmployeeAttendanceService.SaveEmployeeAttendance(_ListOfEmployeeAttendanceResult, SessionHelper.SessionDetail.UserID);
                            }
                        }
                    }
                }
                //else
                //{
                //    p_ErrorMessage = Messages.NoRecordMsg;
                //}
            }
            //else
            //{
            //    p_ErrorMessage = Messages.NoRecordMsg;
            //}

            return p_ErrorMessage;
        }

        #endregion
    }
}