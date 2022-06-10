using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules
{
    public partial class Login : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnLogin";

            if (!IsPostBack)
            {
                SessionHelper.RemoveSessionDetail();
                SessionHelper.RemoveDeviceSessionDetail();

                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }

                if (Request.Cookies["ERPUserName"] != null && Request.Cookies["ERPPassword"] != null)
                {
                    txtUserName.Text = Request.Cookies["ERPUserName"].Value;
                    txtPassword.Attributes["value"] = Request.Cookies["ERPPassword"].Value;
                    chkRememberMe.Checked = true;
                }

                FillFinancialYear();
            }
        }

        #endregion


        #region Events

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkRememberMe.Checked)
                {
                    Response.Cookies["ERPUserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["ERPPassword"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["ERPUserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["ERPPassword"].Expires = DateTime.Now.AddDays(-1);
                }

                Response.Cookies["ERPUserName"].Value = txtUserName.Text.Trim();
                Response.Cookies["ERPPassword"].Value = txtPassword.Text.Trim();

                IUserService _IUserService = new UserService();
                Result<SessionDetail> _Result = _IUserService.CheckLogin(txtUserName.Text.Trim(), SecurityHelper.EncryptString(txtPassword.Text.Trim()));
                if (_Result.IsSuccess)
                {
                    IHistoryService _IHistoryService = new HistoryService();
                    IpInformationModel _IpInformationModel = new IpInformationModel();
                    string _Userip = Request.UserHostAddress;
                    string _UserBrowser = Request.Browser.Browser;
                    string _UserHostName = Request.UserAgent;
                    string _DeviceType = string.Empty;
                    if (Request.Browser.IsMobileDevice == true)
                    {
                        _DeviceType = "SmartPhone";
                    }
                    else
                    {
                        _DeviceType = "Desktop";
                    }
                    _IpInformationModel.IpAddress = _Userip;
                    _IpInformationModel.DeviceName = _UserHostName;
                    _IpInformationModel.BrowserName = _UserBrowser;
                    _IpInformationModel.DeviceType = _DeviceType;
                    _IHistoryService.SaveIpInformation(_IpInformationModel);

                    SessionHelper.SessionDetail = _Result.Data;
                    SessionHelper.SessionDetail.FinancialYearId = new Guid(ddlFinancialYear.SelectedValue);
                    Response.Redirect("~/Modules/Main.aspx", false);
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


        #region Methods

        private void FillFinancialYear()
        {
            ILookupService _ILookupService = new LookupService();
            Result<List<Item>> _Result = _ILookupService.GetAllFinancialYear();

            if (_Result.IsSuccess)
            {
                ddlFinancialYear.DataTextField = "Text";
                ddlFinancialYear.DataValueField = "Id";
                ddlFinancialYear.DataSource = _Result.Data;
                ddlFinancialYear.DataBind();
            }

            ddlFinancialYear.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        #endregion
    }
}