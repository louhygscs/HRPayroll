using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Reports
{
    public partial class DeviceAttendance : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();
        ILookupService _ILookupService = new LookupService();
        IShiftService _IShiftService = new ShiftService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liAttendanceDevice_liHR_liHRReports";

            if (!IsPostBack)
            {
                FillMonth();
                FillEmployee();
            }
        }


        #endregion

        #region Events

        protected void btnReport_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        #endregion

        #region Methods

        private void FillMonth()
        {
            IFinancialYearService _IFinancialYearService = new FinancialYearService();

            Result<FinancialYear> _ResultFYear = _IFinancialYearService.GetFinancialYearById(SessionHelper.SessionDetail.FinancialYearId);

            if (_ResultFYear.IsSuccess)
            {
                int _FinancialYear = _ResultFYear.Data.Year;
                int _no = 0;

                ddlMonth.Items.Insert(_no, new ListItem() { Text = "-- Select --", Value = "" });

                bool _Flag = true;
                for (int no = 4; no < 13; no++)
                {
                    _no = _no + 1;
                    ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

                    if (no == DateTime.Now.Month && _FinancialYear == DateTime.Now.Year)
                    {
                        _Flag = false;
                        break;
                    }
                }

                if (_Flag)
                {
                    for (int no = 1; no < 4; no++)
                    {
                        _no = _no + 1;
                        ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + (_FinancialYear + 1), Value = Convert.ToString(no) + "_" + (_FinancialYear + 1) });
                        if (no == DateTime.Now.Month && _FinancialYear + 1 == DateTime.Now.Year)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void FillGrid()
        {
            try
            {
                decimal _TotalWorkingHours = 0;
                if (ddlMonth.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                    {
                        Guid _EmployeeId = new Guid(ddlEmployee.SelectedValue);
                        int _Month = DateTime.Now.Month;
                        int _Year = DateTime.Now.Year;

                        string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

                        if (_SplitDate.Length > 1)
                        {
                            _Month = Convert.ToInt32(_SplitDate[0]);
                            _Year = Convert.ToInt32(_SplitDate[1]);
                        }

                        DateTime _StartDate = new DateTime(_Year, _Month, 1);
                        DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);

                        if (_EndDate.Date > DateTime.Now.Date)
                        {
                            _EndDate = DateTime.Now.Date;
                        }

                        IEmployeeAttendanceDeviceService _IEmployeeAttendanceDeviceService = new EmployeeAttendanceDeviceService();
                        List<EmployeeAttendanceResult> _ListOfEmployeeAttendanceResult = new List<EmployeeAttendanceResult>();

                        Result<List<EmployeeAttendanceDevices>> _Result = _IEmployeeAttendanceDeviceService.GetEmployeeAttendanceDeviceByEmployeeId(_EmployeeId, _StartDate, _EndDate);
                        Result<List<EmployeeAttendances>> _ResultManual = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_EmployeeId, _StartDate, _EndDate);

                        Result<Shift> _ShiftResult = _IShiftService.GetShiftByEmployeeId(_EmployeeId);

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
                        if (_Result.IsSuccess)
                        {
                            EmployeeAttendanceResult _EmployeeAttendanceResult;
                            divcolor.Visible = true;

                            for (DateTime date = _StartDate; date <= _EndDate; date = date.AddDays(1.0))
                            {
                                _EmployeeAttendanceResult = new EmployeeAttendanceResult();

                                _EmployeeAttendanceResult.AttendanceDateValue = Convert.ToString(date.Day + "/" + date.Month + "/" + date.Year);
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
                                    _EmployeeAttendanceResult.WorkingHours = _TotalWorkingHours.ToString("0.00");
                                    if (_OvertimeHours > 0)
                                    {
                                        _EmployeeAttendanceResult.OvertimeHours = _OvertimeHours;
                                        _EmployeeAttendanceResult.IsApproved = true;
                                    }
                                    else
                                    {
                                        _EmployeeAttendanceResult.OvertimeHours = _WorkingHours - _TotalWorkingHours;
                                    }
                                }
                                else
                                {
                                    _EmployeeAttendanceResult.WorkingHours = _WorkingHours.ToString("0.00");
                                }
                                _EmployeeAttendanceResult.Attendances = String.Join(" | ", _Result.Data.Where(r => r.AttendanceDate == date).OrderBy(r => r.AttendanceDateTime).Select(r => r.PunchTime));
                                _EmployeeAttendanceResult.PunchMethod = String.Join(" | ", _Result.Data.Where(r => r.AttendanceDate == date).OrderBy(r => r.AttendanceDateTime).Select(r => r.PunchMethod));
                                _EmployeeAttendanceResult.Type = _EmployeeAttendanceResult.Attendances == string.Empty ? GlobalHelper.GetEnumDescription((AttendanceType)_ResultManual.Data.Where(a => a.AttendanceDate == date).Select(a => a.AttendanceType).FirstOrDefault()) : (_WorkingHours > 0 ? Convert.ToString(AttendanceType.Present) : Convert.ToString(AttendanceType.Absent));
                                _EmployeeAttendanceResult.AttendanceType = _EmployeeAttendanceResult.Attendances == string.Empty ? _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(a => a.AttendanceType).FirstOrDefault() : (_WorkingHours > 0 ? Convert.ToInt32(AttendanceType.Present) : Convert.ToInt32(AttendanceType.Absent));
                                _EmployeeAttendanceResult.Description = _EmployeeAttendanceResult.AttendanceType == Convert.ToInt32(AttendanceType.Leave) || _EmployeeAttendanceResult.AttendanceType == Convert.ToInt32(AttendanceType.Holiday) ? _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(a => a.Description).FirstOrDefault() : "";

                                _ListOfEmployeeAttendanceResult.Add(_EmployeeAttendanceResult);
                            }

                            gvDeviceAttendance.DataSource = _ListOfEmployeeAttendanceResult;
                            gvDeviceAttendance.DataBind();
                        }
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected string setClass(int AttendanceType)
        {
            string classToApply = "blueFont";

            if (AttendanceType == Convert.ToInt32(Common.AttendanceType.Leave))
            {
                classToApply = "redFont";
            }
            else if (AttendanceType == Convert.ToInt32(Common.AttendanceType.Present))
            {
                classToApply = "greenFont";
            }
            else if (AttendanceType == Convert.ToInt32(Common.AttendanceType.Holiday))
            {
                classToApply = "cyanFont";
            }

            return classToApply;
        }

        private void FillEmployee()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllActiveEmployee();

            if (_Result.IsSuccess)
            {
                ddlEmployee.DataTextField = "Text";
                ddlEmployee.DataValueField = "Id";
                ddlEmployee.DataSource = _Result.Data;
                ddlEmployee.DataBind();

                ddlEmployee.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        #endregion

    }
}