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
    public partial class EmployeeTypeSave : System.Web.UI.Page
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

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployeeType_liHR_liHRMasters";

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
            }
        }

        #endregion


        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeType _EmployeeType = new EmployeeType();

                _EmployeeType.EmployeeTypeID = new Guid(hfId.Value);
                _EmployeeType.EmployeeTypeName = txtEmployeeType.Text.Trim();
                _EmployeeType.NoOfLeavePerMonth =Convert.ToDecimal( ddlLeave.SelectedValue);

                IEmployeeTypeService _IEmployeeTypeService = new EmployeeTypeService();

                Result<Boolean> _Result = _IEmployeeTypeService.SaveEmployeeType(_EmployeeType, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Type");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_EmployeeType.EmployeeTypeID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<EmployeeType>(_Result.Id, TableType.EmployeeTypeMaster, OperationType.Insert, _EmployeeType, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<EmployeeType>(Convert.ToString(_EmployeeType.EmployeeTypeID), TableType.EmployeeTypeMaster, OperationType.Update, _EmployeeType, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/EmployeeTypeList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Type") + "');});", true);
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

        private void FillControls(Guid p_Id)
        {
            try
            {
                IEmployeeTypeService _IEmployeeTypeService = new EmployeeTypeService();

                Result<EmployeeType> _Result = _IEmployeeTypeService.GetEmployeeTypeById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtEmployeeType.Text = _Result.Data.EmployeeTypeName;
                    ddlLeave.SelectedValue =Convert.ToString( _Result.Data.NoOfLeavePerMonth);
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
    }
}