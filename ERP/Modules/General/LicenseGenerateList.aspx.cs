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

namespace ERP.Modules.General
{
    public partial class LicenseGenerateList : System.Web.UI.Page
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

            //this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liLicence";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }

        #endregion

        #region Events

        protected void gvLicense_PreRender(object sender, EventArgs e)
        {
            try
            {
                ILicenseGenerateService _ILicenseGenerateService = new LicenseGenerateService();

                Result<List<LicenseGenerateModel>> _Result = _ILicenseGenerateService.GetLicenseKeyList();

                if (_Result.IsSuccess)
                {
                    gvLicense.DataSource = _Result.Data;
                    gvLicense.DataBind();

                    if (gvLicense.Rows.Count > 0)
                    {
                        gvLicense.UseAccessibleHeader = true;
                        gvLicense.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnUsedKey_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton _btnDelete = (LinkButton)sender;

                Guid _LicenseKeyId = new Guid(_btnDelete.CommandArgument);

                ILicenseGenerateService _ILicenseGenerateService = new LicenseGenerateService();

                Result<Boolean> _Result = _ILicenseGenerateService.LicenseKeyUsedById(_LicenseKeyId);

                if (_Result.IsSuccess)
                {
                    IHistoryService _IHistoryService = new HistoryService();
                    _IHistoryService.InsertHistory<Guid>(Convert.ToString(_LicenseKeyId), TableType.LicenseGenerate, OperationType.Update, _LicenseKeyId, SessionHelper.SessionDetail.UserID);

                    gvLicense_PreRender(gvLicense, new EventArgs());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + String.Format(_Result.Message, "Employee") + "');});", true);
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