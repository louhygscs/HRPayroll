using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnSendLink";
        }

        #endregion


        #region Events

        protected void btnSendLink_Click(object sender, EventArgs e)
        {
            try
            {
                IUserService _IUserService = new UserService();

                Result<String> _Result = _IUserService.CheckUserByUserName(txtUserName.Text.Trim());

                if (_Result.IsSuccess)
                {
                    bool _SendMail = false;
                    string _Body = string.Empty;
                    var _TemplatePath = Server.MapPath("~/EmailTemplate/ForgotPassword.html");

                    using (var _StreamReader = new StreamReader(_TemplatePath))
                    {
                        _Body = _StreamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(_Body))
                    {
                        _Body = _Body.Replace("@UserFullName", _Result.Data);
                        _Body = _Body.Replace("@UserName", txtUserName.Text.Trim());

                        var _CallbackUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/Modules/ResetPassword.aspx?id=" + SecurityHelper.EncryptString(_Result.Id.ToString());

                        _Body = _Body.Replace("@ResetPasswordLink", _CallbackUrl);
                        _Body = _Body.Replace("@CompanyTeamName", GlobalMsg.CompanyTeamName);
                    }

                    List<string> _ToMails = new List<string>();
                    _ToMails.Add(txtUserName.Text.Trim());

                    _SendMail = EmailHelper.SendMail("Forgot Password", _Body, _ToMails);

                    if (_SendMail)
                    {
                        SessionHelper.MessageSession = GlobalMsg.SendLinkSuccessMsg;
                        Response.Redirect("~/Modules/Login.aspx", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "SendFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.SendFailMsg + "');});", true);
                    }
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