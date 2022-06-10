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
    public partial class LeaveApplicationList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liLeaveApplication";

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

        protected void gvLeaveApplication_PreRender(object sender, EventArgs e)
        {
            try
            {
                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<List<EmployeeLeaveCategorys>> _Result = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryListByEmployeeId(SessionHelper.SessionDetail.EmployeeId);

                if (_Result.IsSuccess)
                {
                    gvLeaveApplication.DataSource = _Result.Data;
                    gvLeaveApplication.DataBind();

                    if (gvLeaveApplication.Rows.Count > 0)
                    {
                        gvLeaveApplication.UseAccessibleHeader = true;
                        gvLeaveApplication.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void gvLeaveApplication_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblReason = e.Row.FindControl("lblReason") as Label;
                if (lblReason != null)
                {
                    string strFullText = lblReason.Text;

                    if (lblReason.Text.Length > 50)
                    {
                        lblReason.Text = lblReason.Text.Substring(0, 50) + "...";
                    }
                    lblReason.ToolTip = strFullText;
                }

                Label lblResponse = e.Row.FindControl("lblResponse") as Label;
                if (lblResponse != null)
                {
                    string strFullText = lblResponse.Text;

                    if (lblResponse.Text.Length > 50)
                    {
                        lblResponse.Text = lblResponse.Text.Substring(0, 50) + "...";
                    }
                    lblResponse.ToolTip = strFullText;
                }

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton _btnDelete = (LinkButton)sender;

                Guid _EmployeeLeaveCategoryId = new Guid(_btnDelete.CommandArgument);

                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<Boolean> _Result = _IEmployeeLeaveCategoryService.DeleteEmployeeLeaveCategoryById(_EmployeeLeaveCategoryId, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    IHistoryService _IHistoryService = new HistoryService();
                    _IHistoryService.InsertHistory<Guid>(Convert.ToString(_EmployeeLeaveCategoryId), TableType.EmployeeLeaveCategory, OperationType.Delete, _EmployeeLeaveCategoryId, SessionHelper.SessionDetail.UserID);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionSuccessMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + String.Format(GlobalMsg.DeletionSuccessMsg, "Leave Application") + "');});", true);
                    gvLeaveApplication_PreRender(gvLeaveApplication, new EventArgs());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + String.Format(_Result.Message, "Leave Application") + "');});", true);
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