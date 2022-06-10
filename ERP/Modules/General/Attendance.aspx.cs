using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.General
{
    public partial class Attendance : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liAttendance";

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
                        if (no == DateTime.Now.Month && _FinancialYear+1 == DateTime.Now.Year)
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
                divAttandanceInformation.Visible = false;
                divcolor.Visible = false;

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

                        IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();

                        Result<List<EmployeeAttendances>> _Result = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_EmployeeId, _StartDate, _EndDate);

                        if (_Result.IsSuccess)
                        {
                            gvEmployeeAttendance.DataSource = _Result.Data;
                            gvEmployeeAttendance.DataBind();

                            if (_Result.Data.Count() > 0)
                            {
                                divAttandanceInformation.Visible = true;
                                divcolor.Visible = true;
                                lblTotalDays.Text = Convert.ToString(_Result.Data.Count());
                                lblTotalLeave.Text = string.Format("{0:0.#}", (_Result.Data.Where(p => p.AttendanceType == Convert.ToInt32(AttendanceType.Leave)).Sum(p => (decimal?)p.Attendance) ?? 0));
                                lblTotalHoliday.Text = _Result.Data.Where(p => p.AttendanceType == Convert.ToInt32(AttendanceType.Holiday)).Count().ToString();
                                lblTotalWeeklyOff.Text = _Result.Data.Where(p => p.AttendanceType == Convert.ToInt32(AttendanceType.WeeklyOff)).Count().ToString();
                                lblTotalPresent.Text = string.Format("{0:0.#}", (Convert.ToDecimal(lblTotalDays.Text) - (Convert.ToDecimal(lblTotalLeave.Text) + Convert.ToDecimal(lblTotalHoliday.Text) + Convert.ToDecimal(lblTotalWeeklyOff.Text))));
                                lblTotalWorkingHours.Text = Convert.ToString(_Result.Data.Sum(p => (decimal?)p.WorkingHours));
                                lblTotalOvertimeHours.Text = Convert.ToString(_Result.Data.Sum(p => (decimal?)p.OverTimeHours));
                            }
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