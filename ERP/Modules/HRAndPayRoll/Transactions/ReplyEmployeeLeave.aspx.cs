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

namespace ERP.Modules.HRAndPayRoll.Transactions
{
    public partial class ReplyEmployeeLeave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployeeLeave_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _Result = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_Result)
                    {
                        FillControls(_id);
                    }
                    else
                    {
                        Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeLeaveList.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeLeaveList.aspx", false);
                }
            }
        }

        #endregion

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeLeaveCategorys _EmployeeLeaveCategorys = new EmployeeLeaveCategorys();

                _EmployeeLeaveCategorys.EmployeeLeaveCategoryMapID = new Guid(hfId.Value);
                _EmployeeLeaveCategorys.IsApprove = chkIsApprove.Checked;
                _EmployeeLeaveCategorys.Comments = txtReply.Text.Trim();
                _EmployeeLeaveCategorys.ApprovedBy = SessionHelper.SessionDetail.FullName;

                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<Boolean> _Result = _IEmployeeLeaveCategoryService.SaveReplyEmployeeLeaveCategory(_EmployeeLeaveCategorys, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Reply Employee Leave");

                    IHistoryService _IHistoryService = new HistoryService();

                    _IHistoryService.InsertHistory<EmployeeLeaveCategorys>(Convert.ToString(_EmployeeLeaveCategorys.EmployeeLeaveCategoryMapID), TableType.EmployeeLeaveCategory, OperationType.Update, _EmployeeLeaveCategorys, SessionHelper.SessionDetail.UserID);

                    string _Body = string.Empty;
                    var _TemplatePath = Server.MapPath("~/EmailTemplate/ReplyLeave.html");

                    using (var _StreamReader = new StreamReader(_TemplatePath))
                    {
                        _Body = _StreamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(_Body))
                    {
                        _Body = _Body.Replace("@EmployeeNo", lblEmployeeNo.InnerText);
                        _Body = _Body.Replace("@EmployeeName", lblEmployeeName.InnerText);
                        _Body = _Body.Replace("@LeaveCategory", lblLeaveCategory.InnerText);
                        _Body = _Body.Replace("@DateRange", lblDateRange.InnerText);
                        _Body = _Body.Replace("@Reason", lblReason.InnerText);
                        _Body = _Body.Replace("@Status", chkIsApprove.Checked ? "Approve" : "Dis Approve");
                        _Body = _Body.Replace("@Reply", txtReply.Text);
                        _Body = _Body.Replace("@TotalLeave", lblTotalLeave.InnerText);
                        _Body = _Body.Replace("@AprovedBy", SessionHelper.SessionDetail.FullName);
                        _Body = _Body.Replace("@SendDate", DateTime.Now.ToString());
                    }

                    List<string> _ToMail = new List<string>();
                    _ToMail.Add(_Result.Id);
                    _ToMail.Add(SessionHelper.SessionDetail.Email);

                    EmailHelper.SendMail("Reply Leave", _Body, _ToMail);

                    Response.Redirect("~/Modules/HRAndPayRoll/Transactions/EmployeeLeaveList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Leave") + "');});", true);
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

        private void FillControls(Guid p_Id)
        {
            try
            {
                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<EmployeeLeaveCategorys> _Result = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    lblEmployeeName.InnerText = _Result.Data.EmployeeName;
                    lblEmployeeNo.InnerText = Convert.ToString(_Result.Data.EmployeeNo);
                    lblLeaveCategory.InnerText = _Result.Data.LeaveCategory;
                    lblDateRange.InnerText = _Result.Data.StartDate.ToString("MM/dd/yyyy") + " - " + _Result.Data.EndDate.ToString("MM/dd/yyyy");
                    chkStartDateHalfLeave.Checked = _Result.Data.IsFirstHalfDay;
                    chkEndDateHalfLeave.Checked = _Result.Data.IsLastHalfDay;
                    lblTotalLeave.InnerText = string.Format("{0:0.#}", _Result.Data.TotalDay);
                    lblApplyDate.InnerText = _Result.Data.ApplyDate.ToString("MM/dd/yyyy");
                    lblReason.InnerText = _Result.Data.Reason;
                    txtReply.Text = _Result.Data.Comments;
                    lblStatus.InnerText = _Result.Data.Status;
                    chkIsApprove.Checked = _Result.Data.IsApprove;
                    FillLeftLeaves(_Result.Data.EmployeeId);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        private void FillLeftLeaves(Guid p_EmployeeId)
        {
            if (SessionHelper.SessionDetail.EmployeeId != null)
            {
                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<Dashboard> _Result = _IEmployeeLeaveCategoryService.GetTotalEmployeeLeavesByEmployeeId(p_EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

                if (_Result.IsSuccess)
                {
                    lblLeaves.InnerText = string.Format("{0:0.#}", _Result.Data.EmployeeLeave) + " (Total Leaves) - " + string.Format("{0:0.#}", _Result.Data.UsedEmployeeLeave) + " (Used Leaves) = " + string.Format("{0:0.#}", (_Result.Data.EmployeeLeave - _Result.Data.UsedEmployeeLeave)) + " (Left Leaves)";
                }
            }
        }

        #endregion

    }
}