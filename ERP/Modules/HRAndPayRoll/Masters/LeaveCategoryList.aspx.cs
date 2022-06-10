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
    public partial class LeaveCategoryList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liLeaveCategory_liHR_liHRMasters";

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

        protected void gvLeaveCategory_PreRender(object sender, EventArgs e)
        {
            try
            {
                ILeaveCategoryService _ILeaveCategoryService = new LeaveCategoryService();

                Result<List<LeaveCategory>> _Result = _ILeaveCategoryService.GetLeaveCategoryList();

                if (_Result.IsSuccess)
                {
                    gvLeaveCategory.DataSource = _Result.Data;
                    gvLeaveCategory.DataBind();

                    if (gvLeaveCategory.Rows.Count > 0)
                    {
                        gvLeaveCategory.UseAccessibleHeader = true;
                        gvLeaveCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton _btnDelete = (LinkButton)sender;

                Guid _LeaveCategoryId = new Guid(_btnDelete.CommandArgument);

                ILeaveCategoryService _ILeaveCategoryService = new LeaveCategoryService();

                Result<Boolean> _Result = _ILeaveCategoryService.DeleteLeaveCategoryById(_LeaveCategoryId, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    IHistoryService _IHistoryService = new HistoryService();
                    _IHistoryService.InsertHistory<Guid>(Convert.ToString(_LeaveCategoryId), TableType.LeaveCategoryMaster, OperationType.Delete, _LeaveCategoryId, SessionHelper.SessionDetail.UserID);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionSuccessMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + String.Format(GlobalMsg.DeletionSuccessMsg, "Leave Category") + "');});", true);
                    gvLeaveCategory_PreRender(gvLeaveCategory, new EventArgs());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + String.Format(_Result.Message, "Leave Category") + "');});", true);
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