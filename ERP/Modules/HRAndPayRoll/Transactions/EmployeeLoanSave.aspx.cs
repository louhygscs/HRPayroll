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

namespace ERP.Modules.HRAndPayRoll.Transactions
{
    public partial class EmployeeLoanSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployeeLoan_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                FillDepartment();
                hfId.Value = Convert.ToString(Guid.Empty);

                if (SessionHelper.SessionDetail != null)
                {
                    txtApprovedBy.Text = SessionHelper.SessionDetail.FullName;
                }

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _Result = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_Result)
                    {
                        FillControls(_id);
                    }
                }
            }
        }

        #endregion


        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeLoans _EmployeeLoans = new EmployeeLoans();

                _EmployeeLoans.EmployeeLoanID = new Guid(hfId.Value);
                _EmployeeLoans.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                _EmployeeLoans.ApprovedBy = txtApprovedBy.Text.Trim();
                _EmployeeLoans.Description = txtDescription.Text.Trim();
                _EmployeeLoans.LoanDate = GlobalHelper.StringToDate(txtLoanDate.Value.Trim());
                _EmployeeLoans.EmployeeId = new Guid(ddlEmployee.SelectedValue);
                _EmployeeLoans.LoanTitle = txtTitle.Text.Trim();
                _EmployeeLoans.TotalMonths = Convert.ToInt32(txtTotalMonths.Text.Trim());

                IEmployeeLoanService _IEmployeeLoanService = new EmployeeLoanService();
                Result<Boolean> _Result = _IEmployeeLoanService.SaveEmployeeLoan(_EmployeeLoans, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Loan");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_EmployeeLoans.EmployeeLoanID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<EmployeeLoans>(_Result.Id, TableType.EmployeeLoan, OperationType.Insert, _EmployeeLoans, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<EmployeeLoans>(Convert.ToString(_EmployeeLoans.EmployeeLoanID), TableType.EmployeeLoan, OperationType.Update, _EmployeeLoans, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeLoanList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Loan") + "');});", true);
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
            FillEmployee();
        }


        #endregion


        #region Methods

        private void FillControls(Guid p_Id)
        {
            try
            {
                IEmployeeLoanService _IEmployeeLoanService = new EmployeeLoanService();
                Result<EmployeeLoans> _Result = _IEmployeeLoanService.GetEmployeeLoanById(p_Id);
                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtAmount.Text = _Result.Data.Amount.ToString();
                    txtApprovedBy.Text = _Result.Data.ApprovedBy;
                    txtDescription.Text = _Result.Data.Description;
                    txtLoanDate.Value = _Result.Data.LoanDate.ToString("MM/dd/yyyy");
                    txtTitle.Text = _Result.Data.LoanTitle;
                    txtTotalMonths.Text = _Result.Data.TotalMonths.ToString();
                    ddlDepartment.SelectedValue = _Result.Data.DepartmentId.ToString();
                    FillEmployee();
                    ddlEmployee.SelectedValue = _Result.Data.EmployeeId.ToString();
                    lblInstallment.InnerText = Math.Round(_Result.Data.Amount /_Result.Data.TotalMonths,2).ToString();
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

        private void FillEmployee()
        {
            ddlEmployee.Items.Clear();
            string _DepartmentId = ddlDepartment.SelectedValue;
            Result<List<Item>> _Result = _ILookupService.GetAllEmployeeByDepartmentId(new Guid(_DepartmentId));

            if (_Result.IsSuccess)
            {
                ddlEmployee.DataValueField = "Id";
                ddlEmployee.DataTextField = "Text";
                ddlEmployee.DataSource = _Result.Data;
                ddlEmployee.DataBind();
            }

            ddlEmployee.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

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


        #endregion
       
    }
}