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
    public partial class EmployeeShiftSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IEmployeeService _IEmployeeService = new EmployeeService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployeeShift_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                hfEmployeeId.Value = Convert.ToString(Guid.Empty);

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _Result = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_Result)
                    {
                        FillControls(_id);
                    }
                }
                else
                {
                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeShiftList.aspx", false);
                }
            }
        }

        #endregion

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(hfEmployeeId.Value != null)
                {
                    Guid _EmployeeId = new Guid(hfEmployeeId.Value);

                    if(_EmployeeId != Guid.Empty)
                    {
                        Employee _Employee = new Employee();

                        _Employee.EmployeeID = _EmployeeId;
                        _Employee.ShiftId = new Guid(ddlShift.SelectedValue);

                        Result<Boolean> _Result = _IEmployeeService.UpdateEmployeeShift(_Employee.EmployeeID, _Employee.ShiftId, SessionHelper.SessionDetail.UserID);

                        if(_Result.IsSuccess)
                        {
                            SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Shift");

                            IHistoryService _IHistoryService = new HistoryService();

                            _IHistoryService.InsertHistory<Employee>(_Result.Id, TableType.EmployeeMaster, OperationType.Update, _Employee, SessionHelper.SessionDetail.UserID);

                            Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeShiftList.aspx", false);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeShiftList.aspx", false);
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

        private void FillShift()
        {
            ILookupService _ILookupService = new LookupService();

            Result<List<Item>> _Result = _ILookupService.GetAllShift();

            if (_Result.IsSuccess)
            {
                ddlShift.DataTextField = "Text";
                ddlShift.DataValueField = "Id";
                ddlShift.DataSource = _Result.Data;
                ddlShift.DataBind();
            }

            ddlShift.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillControls(Guid p_EmployeeId)
        {
            Result<Employee> _Result = _IEmployeeService.GetEmployeeById(p_EmployeeId);

            if(_Result.IsSuccess)
            {
                if(_Result.Data != null)
                {
                    hfEmployeeId.Value = _Result.Data.EmployeeID.ToString();
                    lblDepartment.Text = _Result.Data.Department;
                    lblEmployeeName.Text = _Result.Data.FullName;
                    FillShift();
                    ddlShift.SelectedValue = _Result.Data.ShiftId.ToString();
                }
            }
        }

        #endregion
    }
}