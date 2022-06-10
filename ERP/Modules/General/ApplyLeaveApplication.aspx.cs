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
    public partial class ApplyLeaveApplication : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liLeaveApplication";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);
                lblEmployeeName.InnerText = SessionHelper.SessionDetail.FullName;
                FillLeaveCategory();
                FillLeftLeaves();

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _Result = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_Result)
                    {
                        FillControls(_id);
                    }
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
                _EmployeeLeaveCategorys.LeaveCategoryId = new Guid(ddlLeaveCategory.SelectedValue);
                _EmployeeLeaveCategorys.EmployeeId = SessionHelper.SessionDetail.EmployeeId;

                if (!string.IsNullOrEmpty(txtDateRange.Value.Trim()))
                {
                    string[] _SplitDate = txtDateRange.Value.Trim().Split('-');

                    if (_SplitDate.Length > 1)
                    {
                        _EmployeeLeaveCategorys.StartDate = GlobalHelper.StringToDate(_SplitDate[0]);
                        _EmployeeLeaveCategorys.EndDate = GlobalHelper.StringToDate(_SplitDate[1]);
                    }
                }

                _EmployeeLeaveCategorys.IsFirstHalfDay = chkStartDateHalfLeave.Checked;
                _EmployeeLeaveCategorys.IsLastHalfDay = chkEndDateHalfLeave.Checked;
                _EmployeeLeaveCategorys.TotalDay = Convert.ToDecimal(txtTotalLeave.Text.Trim());
                _EmployeeLeaveCategorys.Reason = txtReason.Text.Trim();

                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<Boolean> _Result = _IEmployeeLeaveCategoryService.SaveApplyEmployeeLeaveCategory(_EmployeeLeaveCategorys, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Apply Leave Application");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_EmployeeLeaveCategorys.EmployeeLeaveCategoryMapID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<EmployeeLeaveCategorys>(_Result.Id, TableType.EmployeeLeaveCategory, OperationType.Insert, _EmployeeLeaveCategorys, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<EmployeeLeaveCategorys>(Convert.ToString(_EmployeeLeaveCategorys.EmployeeLeaveCategoryMapID), TableType.EmployeeLeaveCategory, OperationType.Update, _EmployeeLeaveCategorys, SessionHelper.SessionDetail.UserID);
                    }

                    string _Body = string.Empty;
                    var _TemplatePath = Server.MapPath("~/EmailTemplate/ApplyLeave.html");

                    using (var _StreamReader = new StreamReader(_TemplatePath))
                    {
                        _Body = _StreamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(_Body))
                    {
                        _Body = _Body.Replace("@EmployeeNo", Convert.ToString(SessionHelper.SessionDetail.EmployeeNo));
                        _Body = _Body.Replace("@EmployeeName", lblEmployeeName.InnerText);
                        _Body = _Body.Replace("@LeaveCategory", ddlLeaveCategory.SelectedItem.Text);
                        _Body = _Body.Replace("@DateRange", txtDateRange.Value);
                        _Body = _Body.Replace("@Reason", txtReason.Text);
                        _Body = _Body.Replace("@Status", string.IsNullOrEmpty(hfStatus.Value) ? "Pending" : hfStatus.Value);
                        _Body = _Body.Replace("@Reply", hfReply.Value);
                        _Body = _Body.Replace("@TotalLeave", txtTotalLeave.Text);
                        _Body = _Body.Replace("@SendDate", DateTime.Now.ToString());
                    }

                    List<string> _ToMail = new List<string>();
                    _ToMail.Add(SessionHelper.SessionDetail.Email);

                    IUserService _IUserService = new UserService();

                    Result<List<String>> _ResultEmail = _IUserService.GetAllAdminEmail();

                    if(_ResultEmail.IsSuccess)
                    {
                        foreach (string _Email in _ResultEmail.Data)
                        {
                            _ToMail.Add(_Email);
                        }
                    }

                    string _Subject = "Apply Leave";

                    if (!string.IsNullOrEmpty(hfStatus.Value))
                    {
                        _Subject = "Re-Apply Leave";
                    }

                    EmailHelper.SendMail(_Subject, _Body, _ToMail);

                    Response.Redirect("~/Modules/General/LeaveApplicationList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Apply Leave Application") + "');});", true);
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

        private void FillLeaveCategory()
        {
            ILookupService _ILookupService = new LookupService();
            Result<List<Item>> _Result = _ILookupService.GetAllLeaveCategory();

            if (_Result.IsSuccess)
            {
                ddlLeaveCategory.DataTextField = "Text";
                ddlLeaveCategory.DataValueField = "Id";
                ddlLeaveCategory.DataSource = _Result.Data;
                ddlLeaveCategory.DataBind();
            }

            ddlLeaveCategory.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillControls(Guid p_Id)
        {
            IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

            Result<EmployeeLeaveCategorys> _Result = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryById(p_Id);

            if (_Result.IsSuccess)
            {
                hfId.Value = Convert.ToString(p_Id);
                ddlLeaveCategory.SelectedValue = _Result.Data.LeaveCategoryId.ToString();
                txtDateRange.Value = _Result.Data.StartDate.ToString("MM/dd/yyyy") + " - " + _Result.Data.EndDate.ToString("MM/dd/yyyy");
                chkStartDateHalfLeave.Checked = _Result.Data.IsFirstHalfDay;
                chkEndDateHalfLeave.Checked = _Result.Data.IsLastHalfDay;
                txtTotalLeave.Text = string.Format("{0:0.#}", _Result.Data.TotalDay);
                txtReason.Text = _Result.Data.Reason;
                hfStatus.Value = _Result.Data.Status;
                hfReply.Value = _Result.Data.Comments;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
            }
        }

        private void FillLeftLeaves()
        {
            if (SessionHelper.SessionDetail.EmployeeId != null)
            {
                IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();

                Result<Dashboard> _Result = _IEmployeeLeaveCategoryService.GetTotalEmployeeLeavesByEmployeeId(SessionHelper.SessionDetail.EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

                if (_Result.IsSuccess)
                {
                    lblLeaves.InnerText = string.Format("{0:0.#}", _Result.Data.EmployeeLeave) + " (Total Leaves) - " + string.Format("{0:0.#}", _Result.Data.UsedEmployeeLeave) + " (Used Leaves) = " + string.Format("{0:0.#}", (_Result.Data.EmployeeLeave - _Result.Data.UsedEmployeeLeave)) + " (Left Leaves)";
                }
            }
        }

        #endregion

    }
}