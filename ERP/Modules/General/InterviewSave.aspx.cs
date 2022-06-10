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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ERP.Modules.General
{
    public partial class InterviewSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        IInterviewService _IInterviewService = new InterviewService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liInterview";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);


                FillDepartment();
                FillDesignation();
                FillEducation();
                divReason.Visible = true;
                divJoinDate.Visible = false;
                divScheduleDate.Visible = false;
                divScheduleTime.Visible = false;

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
                Interview _Interview = new Interview();

                _Interview.InterviewID = new Guid(hfId.Value);
                _Interview.CurrentSalary = Convert.ToDecimal(txtCurrentSalary.Text.Trim());
                _Interview.DepartmentId = new Guid(ddlDepartment.SelectedValue);
                _Interview.DesignationId = new Guid(ddlDesignation.SelectedValue);
                _Interview.EducationId = new Guid(ddlEducation.SelectedValue);
                _Interview.ExpectedSalary = Convert.ToDecimal(txtExpectedSalary.Text.Trim());
                _Interview.ExperienceMonth = Convert.ToInt32(txtExperienceMonth.Text.Trim());
                _Interview.ExperienceYear = Convert.ToInt32(txtExperienceYear.Text.Trim());
                _Interview.IsJoinDays = chkIsJoinDays.Checked;
                _Interview.JoinAfterDaysOrMonth = Convert.ToInt32(txtJoinAfterDaysOrMonth.Text.Trim());
                _Interview.Mobile = txtMobile.Text.Trim();
                _Interview.Email = txtEmail.Text.Trim();
                _Interview.Name = txtName.Text.Trim();
                _Interview.PersonalDetail = txtPersonalDetail.Text.Trim();
                _Interview.InterviewStatusId = Convert.ToInt32(ddlStatus.SelectedValue.Trim());
                _Interview.InterviewDate = GlobalHelper.StringToDate(txtInterviewDate.Value.Trim());
                _Interview.InterviewTime = txtInterviewTime.Value;
                _Interview.JoinDate = GlobalHelper.StringToDate(txtJoinDate.Value.Trim());
                _Interview.Reason = txtReason.Text.Trim();

                _Interview.ListOfInterviewAttachment = new List<InterviewAttachmentModel>();

                FillInterviewDocuments(ref _Interview, fuResume, divViewResume, hfResume, (int)InterviewAttachmentType.Resume);
                FillInterviewDocuments(ref _Interview, fuCertificate, divUploadCertificate, hfCertificate, (int)InterviewAttachmentType.Certificate);

                Result<Boolean> _Result = _IInterviewService.SaveInterview(_Interview, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    if (_Interview.ListOfInterviewAttachment != null)
                    {
                        UploadAndDeleteInterviewDocuments(fuResume, hfResume, divViewResume, _Interview.ListOfInterviewAttachment, (int)InterviewAttachmentType.Resume);
                        UploadAndDeleteInterviewDocuments(fuCertificate, hfCertificate, divUploadCertificate, _Interview.ListOfInterviewAttachment, (int)InterviewAttachmentType.Certificate);
                    }

                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Interview");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_Interview.InterviewID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<Interview>(_Result.Id, TableType.IterviewMaster, OperationType.Insert, _Interview, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<Interview>(Convert.ToString(_Interview.InterviewID), TableType.IterviewMaster, OperationType.Update, _Interview, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/General/InterviewList.aspx", false);
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Education") + "');});", true);
                //}
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void chkIsJoinDays_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsJoinDays.Checked == true)
            {
                lblMonthOrDays.Text = " Join After/Notice Period(Days)";
            }
            else
            {
                lblMonthOrDays.Text = " Join After/Notice Period(Months)";
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _Status = Convert.ToInt32(ddlStatus.SelectedValue);
            StatusDivHideShow(_Status);
        }

        #endregion

        #region Methods

        private void FillDepartment()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllDepartment();

            if (_Result.IsSuccess)
            {
                ddlDepartment.DataTextField = "Text";
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataSource = _Result.Data;
                ddlDepartment.DataBind();
            }

            ddlDepartment.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillDesignation()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllDesignation();

            if (_Result.IsSuccess)
            {
                ddlDesignation.DataTextField = "Text";
                ddlDesignation.DataValueField = "Id";
                ddlDesignation.DataSource = _Result.Data;
                ddlDesignation.DataBind();
            }

            ddlDesignation.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillEducation()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllEducation();

            if (_Result.IsSuccess)
            {
                ddlEducation.DataTextField = "Text";
                ddlEducation.DataValueField = "Id";
                ddlEducation.DataSource = _Result.Data;
                ddlEducation.DataBind();
            }

            ddlEducation.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillControls(Guid p_Id)
        {
            try
            {

                Result<Interview> _Result = _IInterviewService.GetInterviewByInterviewId(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtCurrentSalary.Text = Convert.ToString(_Result.Data.CurrentSalary);
                    txtEmail.Text = _Result.Data.Email;
                    txtExpectedSalary.Text = Convert.ToString(_Result.Data.ExpectedSalary);
                    txtExperienceMonth.Text = Convert.ToString(_Result.Data.ExperienceMonth);
                    txtExperienceYear.Text = Convert.ToString(_Result.Data.ExperienceYear);
                    txtJoinAfterDaysOrMonth.Text = Convert.ToString(_Result.Data.JoinAfterDaysOrMonth);
                    txtMobile.Text = _Result.Data.Mobile;
                    txtName.Text = _Result.Data.Name;
                    txtPersonalDetail.Text = _Result.Data.PersonalDetail;
                    chkIsJoinDays.Checked = _Result.Data.IsJoinDays;
                    if (chkIsJoinDays.Checked == true)
                    {
                        lblMonthOrDays.Text = " Join After/Notice Period(Days)";
                    }
                    ddlDepartment.SelectedValue = Convert.ToString(_Result.Data.DepartmentId);
                    ddlDesignation.SelectedValue = Convert.ToString(_Result.Data.DesignationId);
                    ddlEducation.SelectedValue = Convert.ToString(_Result.Data.EducationId);

                    ddlStatus.SelectedValue = Convert.ToString(_Result.Data.InterviewStatusId);
                    if (_Result.Data.InterviewDate != null)
                    {
                        txtInterviewDate.Value = _Result.Data.InterviewDate.Value.ToString("MM/dd/yyyy");
                    }
                    txtInterviewTime.Value = _Result.Data.InterviewTime;
                    if (_Result.Data.JoinDate != null)
                    {
                        txtJoinDate.Value = _Result.Data.JoinDate.Value.ToString("MM/dd/yyyy");
                    }
                    txtReason.Text = _Result.Data.Reason.Trim();
                    StatusDivHideShow(_Result.Data.InterviewStatusId);

                    SetViewInterviewDocuments(divUploadResume, divViewResume, btnViewResume, hfResume, _Result.Data.ListOfInterviewAttachment, (int)InterviewAttachmentType.Resume);
                    SetViewInterviewDocuments(divUploadCertificate, divViewCertificate, btnViewCertificate, hfCertificate, _Result.Data.ListOfInterviewAttachment, (int)InterviewAttachmentType.Certificate);

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

        private void FillInterviewDocuments(ref Interview p_Interview, FileUpload p_FileUpload, HtmlControl p_divViewDocument, HiddenField p_hfDocumentName, int p_DocumentType)
        {
            if (p_FileUpload.HasFile)
            {
                InterviewAttachmentModel _InterviewAttechment = new InterviewAttachmentModel();

                _InterviewAttechment.IsDelete = false;
                _InterviewAttechment.AttachmentName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(p_FileUpload.FileName).ToLower();
                _InterviewAttechment.AttachmentType = p_DocumentType;

                p_Interview.ListOfInterviewAttachment.Add(_InterviewAttechment);
            }
            else
            {
                if (p_divViewDocument.Style["display"] == "none" && !string.IsNullOrEmpty(p_hfDocumentName.Value))
                {
                    InterviewAttachmentModel _InterviewAttechment = new InterviewAttachmentModel();

                    _InterviewAttechment.IsDelete = true;
                    _InterviewAttechment.AttachmentName = p_hfDocumentName.Value;
                    _InterviewAttechment.AttachmentType = p_DocumentType;

                    p_Interview.ListOfInterviewAttachment.Add(_InterviewAttechment);
                }
            }
        }

        private void UploadAndDeleteInterviewDocuments(FileUpload p_FileUpload, HiddenField p_HiddenField, HtmlControl p_HtmlControl, List<InterviewAttachmentModel> p_ListOfInterviewAttechmentModel, int p_DocumentType)
        {
            string _RootPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.InterviewDocument + "/");

            if (!Directory.Exists(_RootPath))
            {
                Directory.CreateDirectory(_RootPath);
            }

            if (p_HtmlControl.Style["display"] == "none" && !string.IsNullOrEmpty(p_HiddenField.Value))
            {
                string _PhotoFilePath = _RootPath + p_HiddenField.Value;

                if (File.Exists(_PhotoFilePath))
                {
                    File.Delete(_PhotoFilePath);
                }
            }

            if (p_FileUpload.HasFile)
            {
                string _DocumentName = p_ListOfInterviewAttechmentModel.Where(ed => ed.AttachmentType == p_DocumentType).Select(ed => ed.AttachmentName).FirstOrDefault();

                if (!string.IsNullOrEmpty(_DocumentName))
                {
                    p_FileUpload.SaveAs(_RootPath + _DocumentName);
                }
            }
        }

        private void SetViewInterviewDocuments(HtmlControl p_divUploadDocument, HtmlControl p_divViewDocument, HtmlControl p_btnViewDocument, HiddenField p_hfDocumentName, List<InterviewAttachmentModel> p_ListOfInterviewAttechmentModel, int p_DocumentType)
        {
            string _DocumentName = p_ListOfInterviewAttechmentModel.Where(ed => ed.AttachmentType == p_DocumentType).Select(ed => ed.AttachmentName).FirstOrDefault();

            if (!string.IsNullOrEmpty(_DocumentName))
            {
                string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.InterviewDocument + "/" + _DocumentName;

                if (File.Exists(Server.MapPath(_FilePath)))
                {

                    p_btnViewDocument.Attributes.Add("href", _FilePath);
                    p_divUploadDocument.Style.Add("display", "none");
                    p_divViewDocument.Style.Add("display", "block");
                    p_hfDocumentName.Value = _DocumentName;
                }
            }
        }

        private void StatusDivHideShow(int _Status)
        {
            divReason.Visible = false;
            divJoinDate.Visible = false;
            divScheduleDate.Visible = false;
            divScheduleTime.Visible = false;

            if (_Status == (int)InterviewStatus.Pending)
            {
                divReason.Visible = true;
                lblReason.InnerText = "Text";
            }
            else if (_Status == (int)InterviewStatus.Interview_Scheduled)
            {
                divScheduleDate.Visible = true;
                divScheduleTime.Visible = true;
            }
            else if (_Status == (int)InterviewStatus.In_Queue)
            {
                divReason.Visible = true;
                lblReason.InnerText = "Text";
            }
            else if (_Status == (int)InterviewStatus.Lack_of_Knowledge)
            {
                divReason.Visible = true;
                lblReason.InnerText = "Reason";
            }
            else if (_Status == (int)InterviewStatus.Other)
            {
                divReason.Visible = true;
                lblReason.InnerText = "Text";
            }
            else if (_Status == (int)InterviewStatus.Rejected)
            {
                divReason.Visible = true;
                lblReason.InnerText = "Reason";
            }
            else if (_Status == (int)InterviewStatus.Salary_Unexpected)
            {
                divReason.Visible = true;
                lblReason.InnerText = "Text";
            }
            else if (_Status == (int)InterviewStatus.Will_Join)
            {
                divJoinDate.Visible = true;
            }
        }

        #endregion


    }
}