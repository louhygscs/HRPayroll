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
    public partial class ChangePassword : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liDashboard";
        }

        #endregion


        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionHelper.SessionDetail != null)
                {
                    IUserService _IUserService = new UserService();

                    Result<Boolean> _Result = _IUserService.ChangePassword(SessionHelper.SessionDetail.UserID, SecurityHelper.EncryptString(txtOldPassword.Text.Trim()), SecurityHelper.EncryptString(txtConfirmPassword.Text.Trim()));

                    if (_Result.IsSuccess)
                    {
                        SessionHelper.MessageSession = GlobalMsg.ChangePasswordSuccess;

                        IHistoryService _IHistoryService = new HistoryService();
                        _IHistoryService.InsertHistory<Guid>(Convert.ToString(SessionHelper.SessionDetail.UserID), TableType.UserMaster, OperationType.Update, SessionHelper.SessionDetail.UserID, SessionHelper.SessionDetail.UserID);

                        Response.Redirect("~/Modules/Main.aspx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "AuthenticationFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                    }
                }
                else
                {
                    Response.Redirect("~/Modules/Login.aspx", true);
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