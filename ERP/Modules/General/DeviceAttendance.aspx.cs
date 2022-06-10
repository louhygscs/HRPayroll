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

namespace ERP.Modules.General
{
    public partial class DeviceAttendance : Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liDeviceAttendance";

            if (!IsPostBack)
            {
                FillMonth();
            }
        }

        #endregion

        #region Events

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
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
                if (ddlMonth.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                    {
                        Guid _EmployeeId = SessionHelper.SessionDetail.EmployeeId;
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

                        if (_Result.IsSuccess)
                        {
                            EmployeeAttendanceResult _EmployeeAttendanceResult;
                            divcolor.Visible = true;

                            for (DateTime date = _StartDate; date <= _EndDate; date = date.AddDays(1.0))
                            {
                                _EmployeeAttendanceResult = new EmployeeAttendanceResult();

                                _EmployeeAttendanceResult.AttendanceDateValue = Convert.ToString(date.Day + "/"+ date.Month + "/" + date.Year);
                                _EmployeeAttendanceResult.Attendances = String.Join(" | ", _Result.Data.Where(r=>r.AttendanceDate == date).OrderBy(r=>r.AttendanceDateTime).Select(r=>r.PunchTime));
                                _EmployeeAttendanceResult.Type = _EmployeeAttendanceResult.Attendances == string.Empty ? _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(a => a.AttendanceText).FirstOrDefault() : Convert.ToString(AttendanceType.Present);
                                _EmployeeAttendanceResult.AttendanceType = _EmployeeAttendanceResult.Attendances == string.Empty ? _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(a => a.AttendanceType).FirstOrDefault() : Convert.ToInt32(AttendanceType.Present);
                                _EmployeeAttendanceResult.Description = _EmployeeAttendanceResult.AttendanceType == Convert.ToInt32(AttendanceType.Leave) || _EmployeeAttendanceResult.AttendanceType == Convert.ToInt32(AttendanceType.Holiday) ? _ResultManual.Data.Where(a=>a.AttendanceDate == date).Select(a => a.Description).FirstOrDefault() : "";
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

        #endregion
    }
}