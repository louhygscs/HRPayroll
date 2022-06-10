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

namespace ERP.Modules.HRAndPayRoll.Masters
{
    public partial class EmployeeResignSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IEmployeeService _IEmployeeService = new EmployeeService();

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liEmployee_liHR_liHRMasters";

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
                }
            }
        }


        #endregion


        #region Event

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Employee _Employee = new Employee();
                _Employee.EmployeeID = new Guid(hfId.Value);
                _Employee.LeaveDate = GlobalHelper.StringToDate(txtResignDate.Text.Trim());
                _Employee.LeaveDescription = txtDescription.Text.Trim();
                _Employee.EmployeeAttachments = new List<EmployeeAttachments>();

                FillEmployeeDocuments(ref _Employee, fuResignLetter, divViewResignLetter, hfResignLetter, EmployeeAttachmentType.ResignLetter.ToString());

                Result<Boolean> _Result = _IEmployeeService.ResignEmployee(_Employee, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    if (_Employee.EmployeeAttachments != null)
                    {
                        UploadAndDeleteEmployeeDocuments(fuResignLetter, hfResignLetter, divViewResignLetter, _Employee.EmployeeAttachments, EmployeeAttachmentType.ResignLetter.ToString());
                    }

                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Resign");

                    IHistoryService _IHistoryService = new HistoryService();

                    _IHistoryService.InsertHistory<Employee>(Convert.ToString(_Employee.EmployeeID), TableType.EmployeeMaster, OperationType.Resign, _Employee, SessionHelper.SessionDetail.UserID);

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/EmployeeList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Resign") + "');});", true);
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

        private void FillEmployeeDocuments(ref Employee p_Employee, FileUpload p_FileUpload, HtmlControl p_divViewDocument, HiddenField p_hfDocumentName, string p_DocumentType)
        {
            if (p_FileUpload.HasFile)
            {
                EmployeeAttachments _EmployeeAttachment = new EmployeeAttachments();

                _EmployeeAttachment.IsDelete = false;
                _EmployeeAttachment.Name = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(p_FileUpload.FileName).ToLower();
                _EmployeeAttachment.AttachmentName = p_DocumentType;

                p_Employee.EmployeeAttachments.Add(_EmployeeAttachment);
            }
            else
            {
                if (p_divViewDocument.Style["display"] == "none" && !string.IsNullOrEmpty(p_hfDocumentName.Value))
                {
                    EmployeeAttachments _EmployeeAttachment = new EmployeeAttachments();

                    _EmployeeAttachment.IsDelete = true;
                    _EmployeeAttachment.Name = p_hfDocumentName.Value;
                    _EmployeeAttachment.AttachmentName = p_DocumentType;

                    p_Employee.EmployeeAttachments.Add(_EmployeeAttachment);
                }
            }
        }

        private void UploadAndDeleteEmployeeDocuments(FileUpload p_FileUpload, HiddenField p_HiddenField, HtmlControl p_HtmlControl, List<EmployeeAttachments> p_ListOfEmployeeAttachment, string p_DocumentType)
        {
            string _RootPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeeDocument + "/");

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
                string _DocumentName = p_ListOfEmployeeAttachment.Where(ed => ed.AttachmentName == p_DocumentType).Select(ed => ed.Name).FirstOrDefault();

                if (!string.IsNullOrEmpty(_DocumentName))
                {
                    p_FileUpload.SaveAs(_RootPath + _DocumentName);
                }
            }
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                Result<Employee> _Result = _IEmployeeService.GetEmployeeById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    lblFullName.InnerText = _Result.Data.FullName;
                    txtDescription.Text = _Result.Data.LeaveDescription;

                    if (_Result.Data.IsLeave)
                    {
                        txtResignDate.Text = _Result.Data.LeaveDate.ToString("MM/dd/yyyy");
                    }

                    SetViewEmployeeDocuments(divUploadResignLetter, divViewResignLetter, btnViewResignLetter, hfResignLetter, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.ResignLetter.ToString());
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

        private void SetViewEmployeeDocuments(HtmlControl p_divUploadDocument, HtmlControl p_divViewDocument, HtmlControl p_btnViewDocument, HiddenField p_hfDocumentName, List<EmployeeAttachments> p_ListOfEmployeeAttachment, string p_DocumentType)
        {
            string _DocumentName = p_ListOfEmployeeAttachment.Where(ed => ed.AttachmentName == p_DocumentType).Select(ed => ed.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(_DocumentName))
            {
                string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeeDocument + "/" + _DocumentName;

                if (File.Exists(Server.MapPath(_FilePath)))
                {
                    p_btnViewDocument.Attributes.Add("href", _FilePath);
                    p_divUploadDocument.Style.Add("display", "none");
                    p_divViewDocument.Style.Add("display", "block");
                    p_hfDocumentName.Value = _DocumentName;
                }
            }
        }

        #endregion

    }
}