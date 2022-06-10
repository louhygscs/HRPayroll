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

namespace ERP.Modules
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnResetPassword";

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    hfId.Value = SecurityHelper.DecryptString(Convert.ToString(Request.QueryString["id"]));
                }
                else
                {
                    Response.Redirect("~/Modules/Login.aspx", true);
                }
            }
        }

        #endregion


        #region Events

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                IUserService _IUserService = new UserService();

                Result<Boolean> _Result = _IUserService.ResetPassword(new Guid(hfId.Value),SecurityHelper.EncryptString(txtConfirmPassword.Text.Trim()));

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = GlobalMsg.ResetPasswordSuccess;

                    IHistoryService _IHistoryService = new HistoryService();
                    _IHistoryService.InsertHistory<string>(hfId.Value, TableType.UserMaster, OperationType.Update, hfId.Value, new Guid(hfId.Value));

                    Response.Redirect("~/Modules/Login.aspx", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AuthenticationFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
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