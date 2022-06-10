using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Masters
{
    public partial class EmployeeSalarySave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
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

            SessionHelper.SelectMenuSession = "liEmployeeSalary_liHR_liHRMasters";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _Result = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_Result)
                    {
                        FillControls(_id);
                    }
                }

                if (hfId.Value == Convert.ToString(Guid.Empty))
                {
                    FillDepartment();
                    FillAllowanceByEmployeeId(Guid.Empty);
                    FillDeductionByEmployeeId(Guid.Empty);
                }
            }
        }

        #endregion


        #region Events

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployee();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeSalarys _EmployeeSalarys = new EmployeeSalarys();

                _EmployeeSalarys.EmployeeSalaryID = new Guid(hfId.Value);

                if (_EmployeeSalarys.EmployeeSalaryID == Guid.Empty)
                {
                    _EmployeeSalarys.EmployeeId = new Guid(ddlEmployee.SelectedValue);
                }

                _EmployeeSalarys.Basic = Convert.ToDecimal(txtBasic.Text.Trim());
                _EmployeeSalarys.TotalEarning = Convert.ToDecimal(hfTotalAllowance.Value.Trim());
                _EmployeeSalarys.TotalDeduction = Convert.ToDecimal(hfTotalDeduction.Value.Trim());
                _EmployeeSalarys.TotalSalary = Convert.ToDecimal(hfTotalSalary.Value.Trim());
                _EmployeeSalarys.ListAllowance = new List<Allowance>();
                _EmployeeSalarys.ListDeduction = new List<Deduction>();

                if (rbtnHourSalary.Checked == true)
                {
                    _EmployeeSalarys.IsMonthlySalary = 3;
                }
                if (rbtnMonthSalary.Checked == true)
                {
                    _EmployeeSalarys.IsMonthlySalary = 0;
                }
                if (rbtnDailySalary.Checked == true)
                {
                    _EmployeeSalarys.IsMonthlySalary = 2;
                }
                if (rbtnWeeklySalary.Checked == true)
                {
                    _EmployeeSalarys.IsMonthlySalary = 1;
                }
                foreach (RepeaterItem item in rptAllowance.Items)
                {
                    TextBox txtAllowance = (TextBox)item.FindControl("txtAllowance");

                    if (txtAllowance != null)
                    {
                        if (!String.IsNullOrEmpty(txtAllowance.Text.Trim()))
                        {
                            HiddenField hfAllowanceId = (HiddenField)item.FindControl("hfAllowanceId");

                            if (hfAllowanceId != null)
                            {
                                Allowance _Allowance = new Allowance();
                                _Allowance.AllowanceID = new Guid(hfAllowanceId.Value);
                                _Allowance.Amount = Convert.ToDecimal(txtAllowance.Text.Trim());
                                _EmployeeSalarys.ListAllowance.Add(_Allowance);
                            }
                        }
                    }
                }

                foreach (RepeaterItem item in rptDeduction.Items)
                {
                    TextBox txtDeduction = (TextBox)item.FindControl("txtDeduction");

                    if (txtDeduction != null)
                    {
                        if (!String.IsNullOrEmpty(txtDeduction.Text.Trim()))
                        {
                            HiddenField hfDeductionId = (HiddenField)item.FindControl("hfDeductionId");

                            if (hfDeductionId != null)
                            {
                                Deduction _Deduction = new Deduction();
                                _Deduction.DeductionID = new Guid(hfDeductionId.Value);
                                _Deduction.Amount = Convert.ToDecimal(txtDeduction.Text.Trim());
                                _EmployeeSalarys.ListDeduction.Add(_Deduction);
                            }
                        }
                    }
                }

                Result<Boolean> _Result = _IEmployeeSalaryService.SaveEmployeeSalary(_EmployeeSalarys, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Salary");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_EmployeeSalarys.EmployeeSalaryID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<EmployeeSalarys>(_Result.Id, TableType.EmployeeSalary, OperationType.Insert, _EmployeeSalarys, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<EmployeeSalarys>(Convert.ToString(_EmployeeSalarys.EmployeeSalaryID), TableType.EmployeeSalary, OperationType.Update, _EmployeeSalarys, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/EmployeeSalaryList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Salary") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion


        #region Methods

        private void FillDepartment()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllDepartment();

            if (_Result.IsSuccess)
            {
                ddlDepartment.DataTextField = "Text";
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataSource = _Result.Data;
                ddlDepartment.DataBind();
            }

            ddlDepartment.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillEmployee()
        {
            ddlEmployee.Items.Clear();

            string _DepartmentId = ddlDepartment.SelectedValue;

            if (!string.IsNullOrEmpty(_DepartmentId))
            {
                Result<List<Item>> _Result = _ILookupService.GetAllUnAssignSalaryEmployeeByDepartmentId(new Guid(_DepartmentId));

                if (_Result.IsSuccess)
                {
                    ddlEmployee.DataTextField = "Text";
                    ddlEmployee.DataValueField = "Id";
                    ddlEmployee.DataSource = _Result.Data;
                    ddlEmployee.DataBind();
                }

                ddlEmployee.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        private void FillAllowanceByEmployeeId(Guid p_EmployeeId)
        {
            Result<List<Allowance>> _Result = _IEmployeeSalaryService.GetEmployeeAllowanceByEmployeeId(p_EmployeeId);

            if (_Result.IsSuccess)
            {
                rptAllowance.DataSource = _Result.Data;
                rptAllowance.DataBind();
            }
        }

        private void FillDeductionByEmployeeId(Guid p_EmployeeId)
        {
            Result<List<Deduction>> _Result = _IEmployeeSalaryService.GetEmployeeDeductionByEmployeeId(p_EmployeeId);

            if (_Result.IsSuccess)
            {
                rptDeduction.DataSource = _Result.Data;
                rptDeduction.DataBind();
            }
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                Result<EmployeeSalarys> _Result = _IEmployeeSalaryService.GetEmployeeSalaryById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtBasic.Text = Convert.ToString(_Result.Data.Basic);
                    lblEmployee.Text = _Result.Data.FullName;
                    lblDepartment.Text = _Result.Data.Department;
                    ddlDepartment.Visible = false;
                    ddlEmployee.Visible = false;
                    lblEmployee.Visible = true;
                    lblDepartment.Visible = true;
                    FillAllowanceByEmployeeId(_Result.Data.EmployeeId);
                    FillDeductionByEmployeeId(_Result.Data.EmployeeId);
                    hfTotalAllowance.Value = lblTotalAllowance.Text = Convert.ToString(_Result.Data.TotalEarning);
                    hfTotalDeduction.Value = lblTotalDeduction.Text = Convert.ToString(_Result.Data.TotalDeduction);
                    hfTotalSalary.Value = lblTotalSalary.Text = Convert.ToString(_Result.Data.TotalSalary);
                    rbtnHourSalary.Checked = _Result.Data.IsMonthlySalary == Convert.ToInt32(SalaryType.Hourly) ? true : false;
                    rbtnMonthSalary.Checked = _Result.Data.IsMonthlySalary == Convert.ToInt32(SalaryType.Monthly) ? true : false;
                    rbtnDailySalary.Checked = _Result.Data.IsMonthlySalary == Convert.ToInt32(SalaryType.Daily) ? true : false;
                    rbtnWeeklySalary.Checked = _Result.Data.IsMonthlySalary == Convert.ToInt32(SalaryType.Weekly) ? true : false;
                    if (rbtnHourSalary.Checked == true)
                    {
                        divAllowance.Visible = false;
                        lblBasic.InnerText = "Hourly Basic Salary";
                    }
                    else
                    {
                        divAllowance.Visible = true;
                        lblBasic.InnerText = "Basic";
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

        #endregion

        protected void rbtnMonthSalary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHourSalary.Checked == true)
            {
                divAllowance.Visible = false;
                lblBasic.InnerText = "Hourly Basic Salary";
            }
            else if (rbtnWeeklySalary.Checked == true)
            {
                divAllowance.Visible = false;
                lblBasic.InnerText = "Weekly Basic Salary";
            }
            else if (rbtnDailySalary.Checked == true)
            {
                divAllowance.Visible = false;
                lblBasic.InnerText = "Daily Basic Salary";
            }
            else
            {
                divAllowance.Visible = true;
                lblBasic.InnerText = "Basic";
            }
        }

    }
}