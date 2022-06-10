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

namespace ERP.Modules.HRAndPayRoll.Masters.Dimension
{
    public partial class CategoryList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liCategory_liHR_liHRMasters";

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

        protected void gvCategory_PreRender(object sender, EventArgs e)
        {
            try
            {
                IDimensionService _IService = new DimensionService();

                Result<List<CategoryModel>> _Result = _IService.GetCategories();

                if (_Result.IsSuccess)
                {
                    gvCategory.DataSource = _Result.Data;
                    gvCategory.DataBind();

                    if (gvCategory.Rows.Count > 0)
                    {
                        gvCategory.UseAccessibleHeader = true;
                        gvCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
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

                Guid _EntityId = new Guid(_btnDelete.CommandArgument);

                IDimensionService _IService = new DimensionService();

                Result<bool> _Result = _IService.DeleteCategory(_EntityId, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    IHistoryService _IHistoryService = new HistoryService();
                    _IHistoryService.InsertHistory<Guid>(Convert.ToString(_EntityId), TableType.CategoryMaster, OperationType.Delete, _EntityId, SessionHelper.SessionDetail.UserID);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionSuccessMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + String.Format(GlobalMsg.DeletionSuccessMsg, "Category") + "');});", true);
                    gvCategory_PreRender(gvCategory, new EventArgs());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + String.Format(_Result.Message, "Category") + "');});", true);
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