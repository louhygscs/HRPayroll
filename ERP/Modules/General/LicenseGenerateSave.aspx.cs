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

namespace ERP.Modules.General
{
    public partial class LicenseGenerateSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILicenseGenerateService _ILicenseGenerateService = new LicenseGenerateService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

           // this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liLicence";
            if (!IsPostBack)
            {
                FillDetail();
            }
        }

        #endregion

        #region Private Methods

        private void FillDetail()
        {
            string _Key = Guid.NewGuid().ToString("N");
            txtKey.Text = _Key;
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LicenseGenerateModel _LicenseGenerateModel = new LicenseGenerateModel();

                _LicenseGenerateModel.Email = txtEmail.Text ;
                _LicenseGenerateModel.Key = txtKey.Text;

                Result<Boolean> _Result = _ILicenseGenerateService.SaveLicenseKey(_LicenseGenerateModel, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "License Key");
                    string _Body = string.Empty;
                    var _TemplatePath = Server.MapPath("~/EmailTemplate/SendLicenseKey.html");

                    using (var _StreamReader = new StreamReader(_TemplatePath))
                    {
                        _Body = _StreamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(_Body))
                    {
                        _Body = _Body.Replace("@Email", _LicenseGenerateModel.Email);
                        _Body = _Body.Replace("@Key", _LicenseGenerateModel.Key);
                    }
                    List<string> _ToMail = new List<string>();
                    _ToMail.Add(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendEmailID"]));
                    _ToMail.Add(_LicenseGenerateModel.Email);
                    bool _Flag = EmailHelper.SendMail("Send License Key", _Body, _ToMail);
                    if (_Flag)
                    {
                        SessionHelper.MessageSession = GlobalMsg.SendSuccessMsg;
                        Response.Redirect("~/Modules/Main.aspx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.SendFailMsg + "');});", true);
                    }
                    IHistoryService _IHistoryService = new HistoryService();

                    _IHistoryService.InsertHistory<LicenseGenerateModel>(_Result.Id, TableType.LicenseGenerate, OperationType.Insert, _LicenseGenerateModel, SessionHelper.SessionDetail.UserID);

                    Response.Redirect("~/Modules/General/LicenseGenerateList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "License Generate Key") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

    }
}