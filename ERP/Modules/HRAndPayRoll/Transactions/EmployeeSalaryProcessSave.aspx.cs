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

namespace ERP.Modules.HRAndPayRoll.Transactions
{
    public partial class EmployeeSalaryProcessSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();
        IEmployeeSalaryService _IEmployeeSalaryService = new EmployeeSalaryService();

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployeeSalaryProcess_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                hfId.Value = Guid.Empty.ToString();

                if (Request.QueryString["employeeid"] != null && Request.QueryString["monthyear"] != null)
                {
                    Guid _EmployeeId;
                    bool _Result;

                    _Result = Guid.TryParse(Convert.ToString(Request.QueryString["employeeid"]), out _EmployeeId);

                    if (_Result)
                    {
                        hfEmployeeId.Value = Convert.ToString(_EmployeeId);

                        string _MonthId = Request.QueryString["monthyear"].ToString();

                        if (!string.IsNullOrEmpty(_MonthId))
                        {
                            int _Month = DateTime.Now.Month;
                            int _Year = DateTime.Now.Year;

                            string[] _SplitDate = _MonthId.Split('_');

                            if (_SplitDate.Length > 1)
                            {
                                _Month = Convert.ToInt32(_SplitDate[0]);
                                _Year = Convert.ToInt32(_SplitDate[1]);
                            }

                            hfMonth.Value = Convert.ToString(_Month);
                            lblMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Month);
                            lblYear.Text = _Year.ToString();

                            CheckForEmployeePaidSalaryId();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessList.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessList.aspx", false);
                }
            }
        }

        #endregion


        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeePaidSalarys _EmployeePaidSalarys = new EmployeePaidSalarys();

                _EmployeePaidSalarys.EmployeePaidSalaryID = new Guid(hfId.Value);
                _EmployeePaidSalarys.EmployeeId = new Guid(hfEmployeeId.Value);
                _EmployeePaidSalarys.Month = lblMonth.Text;
                _EmployeePaidSalarys.Year = Convert.ToInt32(lblYear.Text);
                _EmployeePaidSalarys.FinancialYearId = SessionHelper.SessionDetail.FinancialYearId;
                _EmployeePaidSalarys.Basic = Convert.ToDecimal(lblBasic.Text);
                _EmployeePaidSalarys.PaidBasic = Convert.ToDecimal(lblPaidBasic.Text);
                _EmployeePaidSalarys.TotalEarning = Convert.ToDecimal(lblTotalAllowance.Text);
                _EmployeePaidSalarys.PaidTotalEarning = Convert.ToDecimal(lblPaidTotalAllowance.Text);
                _EmployeePaidSalarys.TotalDeduction = Convert.ToDecimal(lblTotalDeduction.Text);
                _EmployeePaidSalarys.PaidTotalDeduction = Convert.ToDecimal(lblPaidTotalDeduction.Text);
                _EmployeePaidSalarys.TotalSalary = Convert.ToDecimal(lblNetSalary.Text);
                _EmployeePaidSalarys.PaidTotalSalary = Convert.ToDecimal(lblOnHandSalary.Text);
                _EmployeePaidSalarys.ProfessionalTax = Convert.ToDecimal(lblProfessionalTax.Text);
                _EmployeePaidSalarys.TotalOverTimeDays = Convert.ToDecimal(lblTotalOverTimeDays.Text);
                _EmployeePaidSalarys.TotalDays = Convert.ToInt32(lblTotalDays.Text.Trim());
                _EmployeePaidSalarys.AllowLeave = Convert.ToDecimal(hfAllowLeave.Value);
                _EmployeePaidSalarys.TotalUseLeave = Convert.ToDecimal(hfTotalUsedLeave.Value);
                _EmployeePaidSalarys.TotalHolidays = Convert.ToDecimal(lblTotalHolidays.Text);
                _EmployeePaidSalarys.TotalPresentDays = Convert.ToDecimal(lblTotalPresentDays.Text);

                if (Convert.ToDecimal(hfCalculateLeave.Value) < 0 || Convert.ToInt32(hfMonth.Value) == 3)
                {
                    _EmployeePaidSalarys.TotalPaidLeave = Convert.ToDecimal(hfCalculateLeave.Value);
                }

                _EmployeePaidSalarys.TotalPaidLeaveAmount = Convert.ToDecimal(lblTotalPaidLeaveSalary.Text);
                _EmployeePaidSalarys.TotalOverTimeAmount = Convert.ToDecimal(lblTotalOverTimeSalary.Text);
                _EmployeePaidSalarys.PaidLoanAmount = Convert.ToDecimal(lblTotalPaidLoanAmount.Text);

                _EmployeePaidSalarys.PaidDate = GlobalHelper.StringToDate(txtPaidDate.Value.Trim());
                _EmployeePaidSalarys.PaidBy = SessionHelper.SessionDetail.FullName;
                _EmployeePaidSalarys.IsPaid = chkbxIsPaid.Checked;

                _EmployeePaidSalarys.ListPaidAllowance = new List<Allowance>();
                _EmployeePaidSalarys.ListPaidDeduction = new List<Deduction>();
                _EmployeePaidSalarys.ListEmployeePaidLoan = new List<EmployeeLoans>();

                foreach (RepeaterItem item in rptAllowance.Items)
                {
                    Label lblPaidAllowance = (Label)item.FindControl("lblPaidAllowance");
                    Label lblAllowance = (Label)item.FindControl("lblAllowance");

                    if (lblPaidAllowance != null && lblAllowance != null)
                    {
                        if (!String.IsNullOrEmpty(lblPaidAllowance.Text))
                        {
                            HiddenField hfAllowanceId = (HiddenField)item.FindControl("hfAllowanceId");

                            if (hfAllowanceId != null)
                            {
                                Allowance _Allowance = new Allowance();
                                _Allowance.AllowanceID = new Guid(hfAllowanceId.Value);
                                _Allowance.Amount = Convert.ToDecimal(lblAllowance.Text);
                                _Allowance.PaidAmount = Convert.ToDecimal(lblPaidAllowance.Text);
                                _EmployeePaidSalarys.ListPaidAllowance.Add(_Allowance);
                            }
                        }
                    }
                }

                foreach (RepeaterItem item in rptDeduction.Items)
                {
                    Label lblPaidDeduction = (Label)item.FindControl("lblPaidDeduction");
                    Label lblDeduction = (Label)item.FindControl("lblDeduction");

                    if (lblPaidDeduction != null && lblDeduction != null)
                    {
                        if (!String.IsNullOrEmpty(lblPaidDeduction.Text))
                        {
                            HiddenField hfDeductionId = (HiddenField)item.FindControl("hfDeductionId");

                            if (hfDeductionId != null)
                            {
                                Deduction _Deduction = new Deduction();
                                _Deduction.DeductionID = new Guid(hfDeductionId.Value);
                                _Deduction.Amount = Convert.ToDecimal(lblDeduction.Text);
                                _Deduction.PaidAmount = Convert.ToDecimal(lblPaidDeduction.Text);
                                _EmployeePaidSalarys.ListPaidDeduction.Add(_Deduction);
                            }
                        }
                    }
                }

                foreach (RepeaterItem item in rptLoan.Items)
                {
                    TextBox txtPaidLoanAmount = (TextBox)item.FindControl("txtPaidLoanAmount");
                    Label lblPendingLoan = (Label)item.FindControl("lblPendingLoan");

                    if (txtPaidLoanAmount != null && lblPendingLoan != null)
                    {
                        decimal _PaidLoan = String.IsNullOrEmpty(txtPaidLoanAmount.Text.Trim()) ? 0 : Convert.ToDecimal(txtPaidLoanAmount.Text.Trim());

                        HiddenField hfLoanId = (HiddenField)item.FindControl("hfLoanId");

                        if (hfLoanId != null)
                        {
                            EmployeeLoans _EmployeePaidLoans = new EmployeeLoans();
                            _EmployeePaidLoans.EmployeeLoanID = new Guid(hfLoanId.Value);
                            _EmployeePaidLoans.PaidLoanAmount = _PaidLoan;

                            if ((Convert.ToDecimal(lblPendingLoan.Text) - _PaidLoan) <= 0)
                            {
                                _EmployeePaidLoans.IsComplete = true;
                            }
                            _EmployeePaidSalarys.ListEmployeePaidLoan.Add(_EmployeePaidLoans);
                        }
                    }
                }

                Result<Boolean> _Result = _IEmployeePaidSalaryService.SaveEmployeePaidSalary(_EmployeePaidSalarys, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Paid Salary");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_EmployeePaidSalarys.EmployeePaidSalaryID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<EmployeePaidSalarys>(_Result.Id, TableType.EmployeePaidSalary, OperationType.Insert, _EmployeePaidSalarys, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<EmployeePaidSalarys>(Convert.ToString(_EmployeePaidSalarys.EmployeePaidSalaryID), TableType.EmployeePaidSalary, OperationType.Update, _EmployeePaidSalarys, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Salary Process") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion



        #region Method

        private void CheckForEmployeePaidSalaryId()
        {
            try
            {
                //Result<EmployeePaidSalarys> _Result = _IEmployeePaidSalaryService.GetEmployeePaidSalaryByEmployeeId(new Guid(hfEmployeeId.Value), lblMonth.Text, Convert.ToInt32(lblYear.Text));
                Result<EmployeePaidSalarys> _Result = _IEmployeePaidSalaryService.GetEmployeePaidSalaryByEmployeeIds(new Guid(hfEmployeeId.Value), lblMonth.Text, Convert.ToInt32(lblYear.Text), null, null, SessionHelper.SessionDetail.FinancialYearId);

                if (_Result.IsSuccess)
                {
                    if (_Result.Data != null)
                    {
                        hfId.Value = _Result.Data.EmployeePaidSalaryID.ToString();
                        lblDepartment.Text = _Result.Data.Department;
                        lblEmployeeName.Text = _Result.Data.FullName;
                        lblEmployeeNo.Text = _Result.Data.EmployeeNo.ToString();
                        lblTotalDays.Text = Convert.ToString(_Result.Data.TotalDays);
                        lblTotalHolidays.Text = Convert.ToString(_Result.Data.TotalHolidays);
                        hfAllowLeave.Value = Convert.ToString(_Result.Data.AllowLeave);
                        hfTotalUsedLeave.Value = Convert.ToString(_Result.Data.TotalUseLeave);
                        hfCalculateLeave.Value = Convert.ToString(_Result.Data.TotalPaidLeave);

                        lblLeave.Text = hfTotalUsedLeave.Value + " (Used Leave)";

                        lblTotalPresentDays.Text = Convert.ToString(_Result.Data.TotalPresentDays);
                        lblTotalOverTimeDays.Text = Convert.ToString(_Result.Data.TotalOverTimeDays);
                        lblTotalOverTimeSalary.Text = Convert.ToString(_Result.Data.TotalOverTimeAmount);
                        lblTotalPaidLeaveSalary.Text = Convert.ToString(_Result.Data.TotalPaidLeaveAmount);

                        if (_Result.Data.TotalPaidLeaveAmount > 0)
                        {
                            lblTotalPaidLeaveSalary.CssClass = "greenFont";
                        }
                        else if (_Result.Data.TotalPaidLeaveAmount < 0)
                        {
                            lblTotalPaidLeaveSalary.CssClass = "redFont";
                        }

                        if (_Result.Data.TotalOverTimeAmount > 0)
                        {
                            lblTotalOverTimeSalary.CssClass = "greenFont";
                        }

                        lblBasic.Text = Convert.ToString(_Result.Data.Basic);
                        lblPaidBasic.Text = Convert.ToString(_Result.Data.PaidBasic);
                        lblTotalAllowance.Text = Convert.ToString(_Result.Data.TotalEarning);
                        lblPaidTotalAllowance.Text = Convert.ToString(_Result.Data.PaidTotalEarning);
                        lblTotalDeduction.Text = Convert.ToString(_Result.Data.TotalDeduction);
                        lblPaidTotalDeduction.Text = Convert.ToString(_Result.Data.PaidTotalDeduction);

                        FillAllowanceByEmployeeId(_Result.Data.EmployeePaidSalaryID, _Result.Data.EmployeeId);
                        FillDeductionByEmployeeId(_Result.Data.EmployeePaidSalaryID, _Result.Data.EmployeeId);
                        FillEmployeePaidLoanByEmployeeId(_Result.Data.EmployeeId, _Result.Data.Month, _Result.Data.Year);

                        lblTotalPaidLoanAmount.Text = Convert.ToString(_Result.Data.PaidLoanAmount);

                        lblNetSalary.Text = Convert.ToString(_Result.Data.TotalSalary);

                        decimal _CalculateSalary = Convert.ToDecimal(lblPaidBasic.Text) + Convert.ToDecimal(lblPaidTotalAllowance.Text);
                        decimal _ProfessinalTax = _Result.Data.ProfessionalTax;

                        _CalculateSalary = _CalculateSalary - _ProfessinalTax;
                        lblProfessionalTax.Text = String.Format("{0:0.00}", _ProfessinalTax);
                        hfCalculateSalary.Value = String.Format("{0:0.00}", _CalculateSalary);

                        lblOnHandSalary.Text = Convert.ToString(_Result.Data.PaidTotalSalary);
                        txtOnHandSalary.Text = lblOnHandSalary.Text;

                        txtPaidDate.Value = _Result.Data.PaidDate.ToString("MM/dd/yyyy");
                        chkbxIsPaid.Checked = _Result.Data.IsPaid;

                        if (_Result.Data.IsPaid)
                        {
                            btnSave.Visible = false;
                        }
                    }
                    else
                    {
                        FillDefaultValues();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        private void FillDefaultValues()
        {
            try
            {
                Guid _EmployeeId = new Guid(hfEmployeeId.Value);

                Result<EmployeeSalarys> _EmployeeSalaryResult = _IEmployeeSalaryService.GetEmployeeSalaryByEmployeeId(_EmployeeId);
                DateTime _JoinDate = DateTime.Now;

                if (_EmployeeSalaryResult.IsSuccess)
                {
                    lblDepartment.Text = _EmployeeSalaryResult.Data.Department;
                    lblEmployeeName.Text = _EmployeeSalaryResult.Data.FullName;
                    lblEmployeeNo.Text = _EmployeeSalaryResult.Data.EmployeeNo.ToString();
                    lblBasic.Text = lblPaidBasic.Text = _EmployeeSalaryResult.Data.Basic.ToString();
                    lblTotalAllowance.Text = _EmployeeSalaryResult.Data.TotalEarning.ToString();
                    lblTotalDeduction.Text = _EmployeeSalaryResult.Data.TotalDeduction.ToString();
                    lblNetSalary.Text = _EmployeeSalaryResult.Data.TotalSalary.ToString();
                    _JoinDate = _EmployeeSalaryResult.Data.JoinDate;
                }

                Result<List<EmployeePaidSalarys>> _ListOfPaidSalary = _IEmployeePaidSalaryService.GetLeaveDetailsByEmployeeId(_EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

                decimal _UsedLeave = 0;
                decimal _AlreadyPendingLeave = 0;

                if (_ListOfPaidSalary.IsSuccess)
                {
                    _UsedLeave = _ListOfPaidSalary.Data.Sum(p => p.TotalUseLeave);
                }

                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<Dashboard> _EmployeeLeaveResult = _IEmployeeLeaveCategoryService.GetTotalEmployeeLeavesByEmployeeId(_EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

                if (_EmployeeLeaveResult.IsSuccess)
                {
                    if (_EmployeeLeaveResult.Data != null)
                    {
                        hfAllowLeave.Value = String.Format("{0:0.#}", _EmployeeLeaveResult.Data.NoOfLeavesPerMonth);
                        _AlreadyPendingLeave = _EmployeeLeaveResult.Data.EmployeeLeave - _UsedLeave;

                        if (_AlreadyPendingLeave < 0)
                        {
                            _AlreadyPendingLeave = 0;
                        }
                    }
                }

                int _TotalDaysOfMonth = DateTime.DaysInMonth(Convert.ToInt32(lblYear.Text), Convert.ToInt32(hfMonth.Value));
                DateTime _StartDate = new DateTime(Convert.ToInt32(lblYear.Text), Convert.ToInt32(hfMonth.Value), 1);
                DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);

                int _JoinDay = 0;
                if (_StartDate < _JoinDate)
                {
                    _JoinDay = (_StartDate - _JoinDate).Days;
                }

                IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();

                Result<List<EmployeeAttendances>> _EmployeeAttendanceResult = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_EmployeeId, _StartDate, _EndDate);

                bool _Flag = true;
                if (_EmployeeAttendanceResult.IsSuccess)
                {
                    if (_EmployeeAttendanceResult.Data != null)
                    {
                        if (_EmployeeAttendanceResult.Data.Count() > 0)
                        {
                            _Flag = false;
                        }

                        int _WeeklyOff = _EmployeeAttendanceResult.Data.Where(h => h.AttendanceType == Convert.ToInt32(AttendanceType.WeeklyOff)).Count();
                        lblTotalHolidays.Text = _EmployeeAttendanceResult.Data.Where(h => h.AttendanceType == Convert.ToInt32(AttendanceType.Holiday)).Count().ToString();

                        hfTotalUsedLeave.Value = String.Format("{0:0.#}", _EmployeeAttendanceResult.Data.Where(el => el.AttendanceType == Convert.ToInt32(AttendanceType.Leave)).Sum(l => l.Attendance));

                        lblTotalOverTimeDays.Text = String.Format("{0:0.#}", _EmployeeAttendanceResult.Data.Sum(ot => ot.OverTimeHours) / 8);
                        lblTotalPresentDays.Text = String.Format("{0:0.#}", _EmployeeAttendanceResult.Data.Count() - (Convert.ToDecimal(hfTotalUsedLeave.Value) + Convert.ToDecimal(lblTotalHolidays.Text) + Convert.ToDecimal(_WeeklyOff)));
                    }
                }

                if (_Flag)
                {
                    btnSave.Visible = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "WarningMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, 'No any find out attendnace entry. Please enter attendance entry.');});", true);
                }

                decimal _CalculateLeave = _AlreadyPendingLeave - Convert.ToDecimal(hfTotalUsedLeave.Value);
                hfCalculateLeave.Value = Convert.ToString(_CalculateLeave);

                lblTotalDays.Text = String.Format("{0:0.#}", _TotalDaysOfMonth + _JoinDay);
                lblLeave.Text = String.Format("{0:0.#}", _AlreadyPendingLeave) + " (Already Pending Leave) - " + hfTotalUsedLeave.Value + " (Used Leave) = " + String.Format("{0:0.#}", _CalculateLeave) + " (Calculate Leave)";

                decimal _BasicPerDay = Convert.ToDecimal(lblBasic.Text) / 30;
                decimal _OverTimeSalary = 0, _LeaveSalary = 0;

                lblPaidBasic.Text = lblBasic.Text;

                if (_JoinDay < 0)
                {
                    lblPaidBasic.Text = String.Format("{0:0.00}", (Convert.ToDecimal(lblPaidBasic.Text) + _BasicPerDay * _JoinDay));
                }

                if (_CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3)
                {
                    _LeaveSalary = _BasicPerDay * _CalculateLeave;
                    lblPaidBasic.Text = String.Format("{0:0.00}", (Convert.ToDecimal(lblPaidBasic.Text) + _LeaveSalary));
                }

                _OverTimeSalary = _BasicPerDay * Convert.ToDecimal(lblTotalOverTimeDays.Text);
                lblPaidBasic.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblPaidBasic.Text) + _OverTimeSalary);

                Result<List<Allowance>> _ResultAllowance = _IEmployeePaidSalaryService.GetEmployeeAllowanceByEmployeePaidSalaryId(Guid.Empty, _EmployeeId);

                if (_ResultAllowance.IsSuccess)
                {
                    if (_JoinDay < 0 || _CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3 || Convert.ToDecimal(lblTotalOverTimeDays.Text) > 0)
                    {
                        decimal _PaidAllowancePerDay;
                        foreach (Allowance _PaidAllowance in _ResultAllowance.Data)
                        {
                            _PaidAllowance.PaidAmount = _PaidAllowance.Amount;

                            if (_PaidAllowance.IsConsider)
                            {
                                _PaidAllowancePerDay = 0;
                                _PaidAllowancePerDay = Convert.ToDecimal(_PaidAllowance.Amount) / 30;

                                if (_JoinDay < 0)
                                {
                                    _PaidAllowance.PaidAmount = _PaidAllowance.PaidAmount + _PaidAllowancePerDay * _JoinDay;
                                }

                                if (_CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3)
                                {
                                    _LeaveSalary = _LeaveSalary + _PaidAllowancePerDay * _CalculateLeave;
                                    _PaidAllowance.PaidAmount = _PaidAllowance.PaidAmount + _PaidAllowancePerDay * _CalculateLeave;
                                }

                                _OverTimeSalary = _OverTimeSalary + _PaidAllowancePerDay * Convert.ToDecimal(lblTotalOverTimeDays.Text);
                                _PaidAllowance.PaidAmount = _PaidAllowance.PaidAmount + _PaidAllowancePerDay * Convert.ToDecimal(lblTotalOverTimeDays.Text);
                            }
                        }
                    }
                    rptAllowance.DataSource = _ResultAllowance.Data;
                    rptAllowance.DataBind();

                    lblPaidTotalAllowance.Text = String.Format("{0:0.00}", _ResultAllowance.Data.Sum(pa => pa.PaidAmount));
                }

                Result<List<Deduction>> _ResultDeduction = _IEmployeePaidSalaryService.GetEmployeeDeductionByEmployeePaidSalaryId(Guid.Empty, _EmployeeId);

                if (_ResultDeduction.IsSuccess)
                {
                    if (_JoinDay < 0 || _CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3 || Convert.ToDecimal(lblTotalOverTimeDays.Text) > 0)
                    {
                        decimal _PaidDeductionPerDay;

                        foreach (Deduction _PaidDeduction in _ResultDeduction.Data)
                        {
                            _PaidDeduction.PaidAmount = _PaidDeduction.Amount;
                            if (_PaidDeduction.IsConsider)
                            {
                                _PaidDeductionPerDay = 0;
                                _PaidDeductionPerDay = Convert.ToDecimal(_PaidDeduction.Amount / 30);

                                if (_JoinDay < 0)
                                {
                                    _PaidDeduction.PaidAmount = _PaidDeduction.PaidAmount + _PaidDeductionPerDay * _JoinDay;
                                }

                                if (_CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3)
                                {
                                    _LeaveSalary = _LeaveSalary + _PaidDeductionPerDay * _CalculateLeave;
                                    _PaidDeduction.PaidAmount = _PaidDeduction.PaidAmount + _PaidDeductionPerDay * _CalculateLeave;
                                }

                                _OverTimeSalary = _OverTimeSalary + _PaidDeductionPerDay * Convert.ToDecimal(lblTotalOverTimeDays.Text);
                                _PaidDeduction.PaidAmount = _PaidDeduction.PaidAmount + _PaidDeductionPerDay * Convert.ToDecimal(lblTotalOverTimeDays.Text);
                            }
                        }
                    }
                    rptDeduction.DataSource = _ResultDeduction.Data;
                    rptDeduction.DataBind();

                    lblPaidTotalDeduction.Text = String.Format("{0:0.00}", _ResultDeduction.Data.Sum(pa => pa.PaidAmount));
                }

                if (_CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3)
                {
                    lblTotalPaidLeaveSalary.Text = String.Format("{0:0.00}", _LeaveSalary);
                }

                if (_LeaveSalary > 0)
                {
                    lblTotalPaidLeaveSalary.CssClass = "greenFont";
                }
                else if (_LeaveSalary < 0)
                {
                    lblTotalPaidLeaveSalary.CssClass = "redFont";
                }

                lblTotalOverTimeSalary.Text = String.Format("{0:0.00}", _OverTimeSalary);

                if (_OverTimeSalary > 0)
                {
                    lblTotalOverTimeSalary.CssClass = "greenFont";
                }

                FillEmployeePaidLoanByEmployeeId(_EmployeeId, lblMonth.Text, Convert.ToInt32(lblYear.Text));

                decimal _CalculateSalary = Convert.ToDecimal(lblPaidBasic.Text) + Convert.ToDecimal(lblPaidTotalAllowance.Text);
                decimal _ProfessinalTax = 0;

                if (_CalculateSalary >= 6000 && _CalculateSalary < 9000)
                {
                    _ProfessinalTax = 80;
                }
                else if (_CalculateSalary >= 9000 && _CalculateSalary < 12000)
                {
                    _ProfessinalTax = 150;
                }
                else if (_CalculateSalary >= 12000)
                {
                    _ProfessinalTax = 200;
                }

                _CalculateSalary = _CalculateSalary - _ProfessinalTax;
                lblProfessionalTax.Text = String.Format("{0:0.00}", _ProfessinalTax);
                hfCalculateSalary.Value = String.Format("{0:0.00}", _CalculateSalary);
                lblOnHandSalary.Text = String.Format("{0:0.00}", _CalculateSalary - Convert.ToDecimal(lblTotalPaidLoanAmount.Text));
                txtOnHandSalary.Text = lblOnHandSalary.Text;
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        private void FillAllowanceByEmployeeId(Guid p_EmployeePaidSalaryId, Guid p_EmployeeId)
        {
            Result<List<Allowance>> _Result = _IEmployeePaidSalaryService.GetEmployeeAllowanceByEmployeePaidSalaryId(p_EmployeePaidSalaryId, p_EmployeeId);

            if (_Result.IsSuccess)
            {
                rptAllowance.DataSource = _Result.Data;
                rptAllowance.DataBind();
            }
        }

        private void FillDeductionByEmployeeId(Guid p_EmployeePaidSalaryId, Guid p_EmployeeId)
        {
            Result<List<Deduction>> _Result = _IEmployeePaidSalaryService.GetEmployeeDeductionByEmployeePaidSalaryId(p_EmployeePaidSalaryId, p_EmployeeId);

            if (_Result.IsSuccess)
            {
                rptDeduction.DataSource = _Result.Data;
                rptDeduction.DataBind();
            }
        }

        private void FillEmployeePaidLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year)
        {
            Result<List<EmployeeLoans>> _EmployeePaidLoanResult = _IEmployeePaidSalaryService.GetEmployeeLoanByEmployeeId(p_EmployeeId, p_Month, p_Year);

            if (_EmployeePaidLoanResult.IsSuccess)
            {
                if (_EmployeePaidLoanResult.Data != null)
                {
                    rptLoan.DataSource = _EmployeePaidLoanResult.Data;
                    rptLoan.DataBind();

                    lblTotalPaidLoanAmount.Text = String.Format("{0:0.00}", _EmployeePaidLoanResult.Data.Sum(p => p.PaidAmount));
                }
            }
        }

        #endregion

    }
}