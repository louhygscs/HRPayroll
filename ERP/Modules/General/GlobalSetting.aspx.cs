using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.General
{
    public partial class GlobalSetting : System.Web.UI.Page
    {

        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        ICompanyService _ICompanyService = new CompanyService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liGlobalSettings";

            if (!IsPostBack)
            {
                FillControls();
            }
        }

        #endregion

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Result<bool> _Result = _ICompanyService.SaveGlobalSetting(txtLateTime.Text,Convert.ToInt32(hfId.Value));
                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Global Setting");
                }
                Response.Redirect("~/Modules/Main.aspx", false);
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

        #region Methods

        private void FillControls()
        {
            try
            {
                hfId.Value = "0";

                Result<string> _Result = _ICompanyService.GetGlobalSetting();

                if (_Result.IsSuccess)
                {
                    txtLateTime.Text = _Result.Data.ToString();
                    hfId.Value = _Result.Id;
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