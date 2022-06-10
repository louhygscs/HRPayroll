using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using MKB.TimePicker;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Transactions
{
    public partial class EmployeeAttendanceEntry : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployeeAttendance_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                FillDepartment();
                FillMonth();
            }
        }

        #endregion


        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<EmployeeAttendances> _ListOfEmployeeAttendances = new List<EmployeeAttendances>();

                foreach (GridViewRow row in gvEmployeeAttendance.Rows)
                {
                    EmployeeAttendances _EmployeeAttendances = new EmployeeAttendances();

                    HiddenField hfEmployeeAttendanceId = (HiddenField)row.FindControl("hfEmployeeAttendanceId");

                    if (hfEmployeeAttendanceId != null)
                    {
                        _EmployeeAttendances.EmployeeAttendanceID = new Guid(hfEmployeeAttendanceId.Value);
                    }

                    _EmployeeAttendances.EmployeeId = new Guid(ddlEmployee.SelectedValue);
                    _EmployeeAttendances.FinancialYearId = SessionHelper.SessionDetail.FinancialYearId;

                    Label lblAttendanceDate = (Label)row.FindControl("lblAttendanceDate");

                    if (lblAttendanceDate != null)
                    {
                        _EmployeeAttendances.AttendanceDate = GlobalHelper.StringToDate(lblAttendanceDate.Text.Trim());
                    }

                    TimeSelector tsTimeIn = (TimeSelector)row.FindControl("tsTimeIn");

                    if (tsTimeIn != null)
                    {
                        _EmployeeAttendances.TimeIn = tsTimeIn.Hour.ToString("00") + ":" + tsTimeIn.Minute.ToString("00") + " " + tsTimeIn.AmPm;
                    }

                    TimeSelector tsTimeOut = (TimeSelector)row.FindControl("tsTimeOut");

                    if (tsTimeOut != null)
                    {
                        _EmployeeAttendances.TimeOut = tsTimeOut.Hour.ToString("00") + ":" + tsTimeOut.Minute.ToString("00") + " " + tsTimeOut.AmPm;
                    }

                    TextBox txtWorkingHours = (TextBox)row.FindControl("txtWorkingHours");

                    if (txtWorkingHours != null)
                    {
                        if (!string.IsNullOrEmpty(txtWorkingHours.Text))
                        {
                            _EmployeeAttendances.WorkingHours = Convert.ToDecimal(txtWorkingHours.Text);
                        }
                    }

                    TextBox txtOverTimeHours = (TextBox)row.FindControl("txtOverTimeHours");

                    if (txtOverTimeHours != null)
                    {
                        if (!string.IsNullOrEmpty(txtOverTimeHours.Text))
                        {
                            _EmployeeAttendances.OverTimeHours = Convert.ToDecimal(txtOverTimeHours.Text);
                        }
                    }

                    TextBox txtDescription = (TextBox)row.FindControl("txtDescription");

                    if (txtDescription != null)
                    {
                        _EmployeeAttendances.Description = txtDescription.Text;
                    }

                    DropDownList ddlAttendanceType = (DropDownList)row.FindControl("ddlAttendanceType");

                    _EmployeeAttendances.AttendanceType = Convert.ToInt32(AttendanceType.Present);

                    if (ddlAttendanceType != null)
                    {
                        _EmployeeAttendances.AttendanceType = Convert.ToInt32(ddlAttendanceType.SelectedValue);
                    }

                    DropDownList ddlAttendance = (DropDownList)row.FindControl("ddlAttendance");

                    if (ddlAttendance != null)
                    {
                        _EmployeeAttendances.Attendance = Convert.ToDecimal(ddlAttendance.SelectedValue);
                    }

                    _ListOfEmployeeAttendances.Add(_EmployeeAttendances);
                }

                Result<Boolean> _Result = _IEmployeeAttendanceService.SaveEmployeeAttendance(_ListOfEmployeeAttendances, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    IHistoryService _IHistoryService = new HistoryService();

                    _IHistoryService.InsertHistory<EmployeeAttendances>(_Result.Id, TableType.EmployeeAttendance, OperationType.Update, _ListOfEmployeeAttendances.FirstOrDefault(), SessionHelper.SessionDetail.UserID);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + String.Format(GlobalMsg.SaveSuccessMsg, "Employee Attendance") + "');});", true);

                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }

            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMonth.SelectedIndex = 0;
            FillEmployee();

            FillGrid();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillGrid();
        }

        protected void ddlAttendanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAttendanceType = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddlAttendanceType.NamingContainer;
                DropDownList ddlAttendance = (DropDownList)row.FindControl("ddlAttendance");
                TextBox txtWorkingHours = (TextBox)row.FindControl("txtWorkingHours");
                Label lblAttendance = (Label)row.FindControl("lblAttendance");

                ddlAttendance.Visible = false;
                txtWorkingHours.Text = "0";
                txtWorkingHours.Enabled = false;
                lblAttendance.Visible = false;

                if (ddlAttendanceType.SelectedValue == (Convert.ToInt32(AttendanceType.Leave)).ToString())
                {
                    ddlAttendance.Visible = true;
                    txtWorkingHours.Enabled = true;
                }
                else if (ddlAttendanceType.SelectedValue == (Convert.ToInt32(AttendanceType.Present)).ToString())
                {
                    ddlAttendance.SelectedValue = "1.00";
                    txtWorkingHours.Enabled = true;
                    lblAttendance.Text = "Present";
                    lblAttendance.Visible = true;
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnAttendanceAdd_Click(object sender, EventArgs e)
        {
            EmployeeAttendances _EmployeeAttendances = new EmployeeAttendances();

            _EmployeeAttendances.EmployeeId = new Guid(ddlEmployee.SelectedValue);
            _EmployeeAttendances.FinancialYearId = SessionHelper.SessionDetail.FinancialYearId;
            _EmployeeAttendances.AttendanceDate = Convert.ToDateTime(hfDate.Value);
            _EmployeeAttendances.TimeIn = atsTimeIn.Hour.ToString("00") + ":" + atsTimeIn.Minute.ToString("00") + " " + atsTimeIn.AmPm;
            _EmployeeAttendances.TimeOut = atsTimeOut.Hour.ToString("00") + ":" + atsTimeOut.Minute.ToString("00") + " " + atsTimeOut.AmPm;
            _EmployeeAttendances.Description = atxtDescription.Text;
            _EmployeeAttendances.WorkingHours = Convert.ToDecimal((DateTime.Parse(_EmployeeAttendances.TimeOut).Subtract(DateTime.Parse(_EmployeeAttendances.TimeIn))).TotalHours);
            _EmployeeAttendances.AttendanceType = Convert.ToInt32(AttendanceType.Present);
            _EmployeeAttendances.EmployeeAttendanceID = new Guid(hfEmployeeAttendanceID.Value);
            _EmployeeAttendances.EnrollNo = SessionHelper.SessionDetail.EmployeeNo.ToString();
            Result<bool> _Result = _IEmployeeAttendanceService.SaveManualEmployeeAttendance(_EmployeeAttendances, SessionHelper.SessionDetail.UserID);
            if (_Result.IsSuccess)
            {
                IHistoryService _IHistoryService = new HistoryService();

                _IHistoryService.InsertHistory<EmployeeAttendances>(_Result.Id, TableType.EmployeeAttendance, OperationType.Update, _EmployeeAttendances, SessionHelper.SessionDetail.UserID);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + String.Format(GlobalMsg.SaveSuccessMsg, "Employee Attendance") + "');});", true);

                FillGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
            }
        }

        #endregion


        #region Methods

        private void FillDepartment()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllDepartment();

            if (_Result.IsSuccess)
            {
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "Text";
                ddlDepartment.DataSource = _Result.Data;
                ddlDepartment.DataBind();
            }

            ddlDepartment.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

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

        private void FillEmployee()
        {
            ddlEmployee.Items.Clear();

            string _DepartmentId = ddlDepartment.SelectedValue;

            if (!string.IsNullOrEmpty(_DepartmentId))
            {
                Result<List<Item>> _Result = _ILookupService.GetAllActiveEmployeeByDepartmentId(new Guid(_DepartmentId));

                if (_Result.IsSuccess)
                {
                    ddlEmployee.DataValueField = "Id";
                    ddlEmployee.DataTextField = "Text";
                    ddlEmployee.DataSource = _Result.Data;
                    ddlEmployee.DataBind();
                }

                ddlEmployee.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        private void FillGrid()
        {
            try
            {
                btnSave.Visible = true;
                string _MonthId = ddlMonth.SelectedValue;
                if (!string.IsNullOrEmpty(_MonthId) && !string.IsNullOrEmpty(ddlEmployee.SelectedValue))
                {
                    int _Month = DateTime.Now.Month;
                    int _Year = DateTime.Now.Year;

                    string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

                    if (_SplitDate.Length > 1)
                    {
                        _Month = Convert.ToInt32(_SplitDate[0]);
                        _Year = Convert.ToInt32(_SplitDate[1]);
                    }

                    IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();

                    Result<Boolean> _Result = _IEmployeePaidSalaryService.CheckSalaryPaidByEmployee(new Guid(ddlEmployee.SelectedValue), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Month), _Year);

                    if (_Result.IsSuccess)
                    {
                        if (_Result.Data)
                        {
                            btnSave.Visible = false;
                        }
                    }
                }


                if (ddlMonth.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
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

                    IHolidayService _IHolidayService = new HolidayService();
                    IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();
                    IEmployeeService _IEmployeeService = new EmployeeService();

                    List<EmployeeAttendances> _FinalResult = new List<EmployeeAttendances>();

                    Result<List<EmployeeAttendances>> _Result = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_EmployeeId, _StartDate, _EndDate);

                    Result<List<Holiday>> _HolidayResult = _IHolidayService.GetHolidayListByDate(_StartDate, _EndDate);

                    Result<List<EmployeeLeaveCategorys>> _LeaveResult = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryListByDate(_EmployeeId, _StartDate, _EndDate);

                    Result<Employee> _EmployeeResult = _IEmployeeService.GetEmployeeById(_EmployeeId);

                    if (_Result.IsSuccess)
                    {
                        if (_Result.Data != null)
                        {
                            _FinalResult = _Result.Data;

                            if (_FinalResult.Count() > 0)
                            {
                                _StartDate = _FinalResult.Max(a => a.AttendanceDate).AddDays(1);
                            }
                            else if (_StartDate < _EmployeeResult.Data.JoinDate)
                            {
                                _StartDate = _EmployeeResult.Data.JoinDate;
                            }
                        }
                    }

                    for (DateTime _Date = _StartDate; _Date <= _EndDate; _Date = _Date.AddDays(1))
                    {
                        EmployeeAttendances _EmployeeAttendance = new EmployeeAttendances();

                        _EmployeeAttendance.EmployeeAttendanceID = Guid.Empty;
                        _EmployeeAttendance.AttendanceDate = _Date;

                        _EmployeeAttendance.TimeIn = "10:00 AM";
                        _EmployeeAttendance.TimeOut = "08:00 PM";
                        _EmployeeAttendance.AttendanceType = Convert.ToInt32(AttendanceType.Present);
                        _EmployeeAttendance.Attendance = Convert.ToDecimal("1.00");

                        bool _Flag = true;

                        if (_EmployeeResult.IsSuccess)
                        {
                            _EmployeeAttendance.TimeIn = _EmployeeResult.Data.FromTime;
                            _EmployeeAttendance.TimeOut = _EmployeeResult.Data.ToTime;

                            if (_EmployeeResult.Data.WorkingDays != null)
                            {
                                bool _IsHoliday = _EmployeeResult.Data.WorkingDays.Contains(_Date.ToString("dddd"));

                                if (!_IsHoliday)
                                {
                                    _EmployeeAttendance.AttendanceType = Convert.ToInt32(AttendanceType.WeeklyOff);
                                    _Flag = false;
                                }
                            }
                        }

                        if (_Flag)
                        {
                            if (_LeaveResult.IsSuccess)
                            {
                                EmployeeLeaveCategorys _EmployeeLeaveCategorys = _LeaveResult.Data.Where(h => h.StartDate <= _Date && h.EndDate >= _Date).FirstOrDefault();

                                if (_EmployeeLeaveCategorys != null)
                                {
                                    _Flag = false;
                                    _EmployeeAttendance.AttendanceType = Convert.ToInt32(AttendanceType.Leave);

                                    if (_EmployeeLeaveCategorys.IsFirstHalfDay && _EmployeeLeaveCategorys.StartDate.Date == _Date.Date)
                                    {
                                        _EmployeeAttendance.Attendance = Convert.ToDecimal("0.50");
                                    }
                                    else if (_EmployeeLeaveCategorys.IsLastHalfDay && _EmployeeLeaveCategorys.EndDate.Date == _Date.Date)
                                    {
                                        _EmployeeAttendance.Attendance = Convert.ToDecimal("0.50");
                                    }
                                    else
                                    {
                                        _EmployeeAttendance.Attendance = Convert.ToDecimal("1.00");
                                    }

                                    _EmployeeAttendance.Description = _EmployeeLeaveCategorys.LeaveCategory.ToString();
                                }
                            }
                        }

                        if (_Flag)
                        {
                            if (_HolidayResult.IsSuccess)
                            {
                                Holiday _Holiday = _HolidayResult.Data.Where(h => h.StartDate <= _Date && h.EndDate >= _Date).FirstOrDefault();

                                if (_Holiday != null)
                                {
                                    _EmployeeAttendance.AttendanceType = Convert.ToInt32(AttendanceType.Holiday);
                                    _EmployeeAttendance.Description = _Holiday.Title;
                                }
                            }
                        }

                        _FinalResult.Add(_EmployeeAttendance);
                    }

                    gvEmployeeAttendance.DataSource = _FinalResult;
                    gvEmployeeAttendance.DataBind();
                }
                else
                {
                    gvEmployeeAttendance.DataSource = null;
                    gvEmployeeAttendance.DataBind();
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion


    }
}