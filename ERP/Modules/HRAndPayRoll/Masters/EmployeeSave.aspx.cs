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
    public partial class EmployeeSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
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

                txtBirthDate.Value = DateTime.Now.AddYears(-20).ToString("MM/dd/yyyy");

                FillEmployeeNo();
                FillCountry();
                FillEmployeeType();
                FillDepartment();
                FillDesignation();
                FillEmployeeGrade();
                FillShift();

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

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillState();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Employee _Employee = new Employee();

                _Employee.EmployeeID = new Guid(hfId.Value);
                _Employee.EmployeeTypeId = new Guid(ddlEmployeeType.SelectedValue);
                _Employee.EmployeeGradeId = new Guid(ddlEmployeeGrade.SelectedValue);
                _Employee.DepartmentId = new Guid(ddlDepartment.SelectedValue);
                _Employee.DesignationId = new Guid(ddlDesignation.SelectedValue);
                _Employee.ShiftId = new Guid(ddlShift.SelectedValue);
                _Employee.CountryId = new Guid(ddlCountry.SelectedValue);
                _Employee.StateId = new Guid(ddlState.SelectedValue);
                _Employee.FirstName = txtFirstName.Text.Trim();
                _Employee.MiddleName = txtMiddleName.Text.Trim();
                _Employee.LastName = txtLastName.Text.Trim();
                _Employee.BirthDate = GlobalHelper.StringToDate(txtBirthDate.Value.Trim());
                _Employee.IsGender = rbtnMale.Checked;
                _Employee.MaratialStatus = ddlMaratialStatus.SelectedValue;
                _Employee.City = txtCity.Text.Trim();
                _Employee.Address = txtAddress.Text.Trim();
                _Employee.PinCode = txtPinCode.Text.Trim();
                _Employee.MobileNo = txtMobile.Text.Trim();
                _Employee.PhoneNo = txtPhone.Text.Trim();
                _Employee.JoinDate = GlobalHelper.StringToDate(txtJoinDate.Value.Trim());
                _Employee.EmployeeNo = Convert.ToInt32(lblEmployeeNo.InnerText);
                _Employee.Email = txtEmail.Text.Trim();
                _Employee.BankName = txtBankName.Text.Trim();
                _Employee.BranchName = txtBranchName.Text.Trim();
                _Employee.AccountName = txtAccountName.Text.Trim();
                _Employee.AccountNo = txtAccountNumber.Text.Trim();
                if (!string.IsNullOrEmpty(txtOvertimeAmount.Text.Trim()))
                {
                    _Employee.OverTimeAmount = Convert.ToDecimal(txtOvertimeAmount.Text.Trim());
                }
                _Employee.WorkingDays = new List<string>();

                foreach (ListItem _ListItem in chkListWorkingDays.Items)
                {
                    if (_ListItem.Selected)
                    {
                        _Employee.WorkingDays.Add(_ListItem.Value);
                    }
                }

                if (fuPhoto.HasFile)
                {
                    _Employee.PhotoName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(fuPhoto.FileName).ToLower();
                }

                _Employee.EmployeeAttachments = new List<EmployeeAttachments>();

                FillEmployeeDocuments(ref _Employee, fuResume, divViewResume, hfResume, EmployeeAttachmentType.Resume.ToString());
                FillEmployeeDocuments(ref _Employee, fuOfferLetter, divViewOfferLetter, hfOfferLetter, EmployeeAttachmentType.OfferLetter.ToString());
                FillEmployeeDocuments(ref _Employee, fuJoiningLetter, divViewJoiningLetter, hfJoiningLetter, EmployeeAttachmentType.JoiningLetter.ToString());
                FillEmployeeDocuments(ref _Employee, fuContractPaper, divViewContractPaper, hfContractPaper, EmployeeAttachmentType.ContractPaper.ToString());
                FillEmployeeDocuments(ref _Employee, fuIDProff, divViewIDProff, hfIDProff, EmployeeAttachmentType.IDProff.ToString());
                FillEmployeeDocuments(ref _Employee, fuOtherDocument, divViewOtherDocument, hfOtherDocument, EmployeeAttachmentType.OtherDocument.ToString());

                Result<Boolean> _Result = _IEmployeeService.SaveEmployee(_Employee, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(_Employee.PhotoName))
                    {
                        string _PhotoRootPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeePhoto + "/");

                        if (!Directory.Exists(_PhotoRootPath))
                        {
                            Directory.CreateDirectory(_PhotoRootPath);
                        }

                        if (!string.IsNullOrEmpty(hfPhoto.Value))
                        {
                            string _PhotoFilePath = _PhotoRootPath + hfPhoto.Value;

                            if (File.Exists(_PhotoFilePath))
                            {
                                File.Delete(_PhotoFilePath);
                            }
                        }

                        fuPhoto.SaveAs(_PhotoRootPath + _Employee.PhotoName);
                    }

                    if (_Employee.EmployeeAttachments != null)
                    {
                        UploadAndDeleteEmployeeDocuments(fuResume, hfResume, divViewResume, _Employee.EmployeeAttachments, EmployeeAttachmentType.Resume.ToString());
                        UploadAndDeleteEmployeeDocuments(fuOfferLetter, hfOfferLetter, divViewOfferLetter, _Employee.EmployeeAttachments, EmployeeAttachmentType.OfferLetter.ToString());
                        UploadAndDeleteEmployeeDocuments(fuJoiningLetter, hfJoiningLetter, divViewJoiningLetter, _Employee.EmployeeAttachments, EmployeeAttachmentType.JoiningLetter.ToString());
                        UploadAndDeleteEmployeeDocuments(fuContractPaper, hfContractPaper, divViewContractPaper, _Employee.EmployeeAttachments, EmployeeAttachmentType.ContractPaper.ToString());
                        UploadAndDeleteEmployeeDocuments(fuIDProff, hfIDProff, divViewIDProff, _Employee.EmployeeAttachments, EmployeeAttachmentType.IDProff.ToString());
                        UploadAndDeleteEmployeeDocuments(fuOtherDocument, hfOtherDocument, divViewOtherDocument, _Employee.EmployeeAttachments, EmployeeAttachmentType.OtherDocument.ToString());
                    }

                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_Employee.EmployeeID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<Employee>(_Result.Id, TableType.EmployeeMaster, OperationType.Insert, _Employee, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<Employee>(Convert.ToString(_Employee.EmployeeID), TableType.EmployeeMaster, OperationType.Update, _Employee, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/EmployeeList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Email") + "');});", true);
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

        private void FillEmployeeNo()
        {
            if (new Guid(hfId.Value) == Guid.Empty)
            {
                Result<int> _Result = _IEmployeeService.GetMaxEmployeeNo();

                if (_Result.IsSuccess)
                {
                    lblEmployeeNo.InnerText = _Result.Data.ToString();
                }
            }
        }

        private void FillCountry()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllCountry();

            if (_Result.IsSuccess)
            {
                ddlCountry.DataTextField = "Text";
                ddlCountry.DataValueField = "Id";
                ddlCountry.DataSource = _Result.Data;
                ddlCountry.DataBind();
            }

            ddlCountry.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillState()
        {
            ddlState.Items.Clear();

            string _CountryId = ddlCountry.SelectedValue;

            if (!string.IsNullOrEmpty(_CountryId))
            {
                Result<List<Item>> _Result = _ILookupService.GetAllStateByCountryId(new Guid(_CountryId));

                if (_Result.IsSuccess)
                {
                    ddlState.DataTextField = "Text";
                    ddlState.DataValueField = "Id";
                    ddlState.DataSource = _Result.Data;
                    ddlState.DataBind();
                }

                ddlState.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        private void FillEmployeeType()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllEmployeeType();

            if (_Result.IsSuccess)
            {
                ddlEmployeeType.DataTextField = "Text";
                ddlEmployeeType.DataValueField = "Id";
                ddlEmployeeType.DataSource = _Result.Data;
                ddlEmployeeType.DataBind();
            }

            ddlEmployeeType.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

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

        private void FillEmployeeGrade()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllEmployeeGrade();

            if (_Result.IsSuccess)
            {
                ddlEmployeeGrade.DataTextField = "Text";
                ddlEmployeeGrade.DataValueField = "Id";
                ddlEmployeeGrade.DataSource = _Result.Data;
                ddlEmployeeGrade.DataBind();
            }

            ddlEmployeeGrade.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillShift()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllShift();

            if (_Result.IsSuccess)
            {
                ddlShift.DataTextField = "Text";
                ddlShift.DataValueField = "Id";
                ddlShift.DataSource = _Result.Data;
                ddlShift.DataBind();
            }

            ddlShift.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                Result<Employee> _Result = _IEmployeeService.GetEmployeeById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    lblEmployeeNo.InnerText = Convert.ToString(_Result.Data.EmployeeNo);
                    ddlEmployeeType.SelectedValue = Convert.ToString(_Result.Data.EmployeeTypeId);
                    ddlEmployeeGrade.SelectedValue = Convert.ToString(_Result.Data.EmployeeGradeId);
                    ddlDepartment.SelectedValue = Convert.ToString(_Result.Data.DepartmentId);
                    ddlDesignation.SelectedValue = Convert.ToString(_Result.Data.DesignationId);
                    ddlShift.SelectedValue = Convert.ToString(_Result.Data.ShiftId);
                    ddlCountry.SelectedValue = Convert.ToString(_Result.Data.CountryId);

                    FillState();

                    ddlState.SelectedValue = Convert.ToString(_Result.Data.StateId);
                    txtFirstName.Text = _Result.Data.FirstName;
                    txtMiddleName.Text = _Result.Data.MiddleName;
                    txtLastName.Text = _Result.Data.LastName;
                    txtBirthDate.Value = _Result.Data.BirthDate.ToString("MM/dd/yyyy");
                    rbtnMale.Checked = _Result.Data.IsGender;
                    rbtnFeMale.Checked = !_Result.Data.IsGender;
                    ddlMaratialStatus.SelectedValue = _Result.Data.MaratialStatus;
                    txtCity.Text = _Result.Data.City;
                    txtAddress.Text = _Result.Data.Address;
                    txtPinCode.Text = _Result.Data.PinCode;
                    txtMobile.Text = _Result.Data.MobileNo;
                    txtPhone.Text = _Result.Data.PhoneNo;
                    txtJoinDate.Value = _Result.Data.JoinDate.ToString("MM/dd/yyyy");
                    txtEmail.Text = _Result.Data.Email;
                    txtBankName.Text = _Result.Data.BankName;
                    txtBranchName.Text = _Result.Data.BranchName;
                    txtAccountName.Text = _Result.Data.AccountName;
                    txtAccountNumber.Text = _Result.Data.AccountNo;
                    txtOvertimeAmount.Text = _Result.Data.OverTimeAmount.ToString();

                    chkListWorkingDays.ClearSelection();

                    if (_Result.Data.WorkingDays != null)
                    {
                        foreach (string _WorkingDay in _Result.Data.WorkingDays)
                        {
                            chkListWorkingDays.Items.FindByValue(_WorkingDay).Selected = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(_Result.Data.PhotoName))
                    {
                        string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeePhoto + "/" + _Result.Data.PhotoName;

                        if (File.Exists(Server.MapPath(_FilePath)))
                        {
                            imgPhoto.Src = _FilePath;
                            divUploadPhoto.Style.Add("display", "none");
                            divViewPhoto.Style.Add("display", "block");
                            revPhoto.Enabled = false;
                            hfPhoto.Value = _Result.Data.PhotoName;
                        }
                    }

                    SetViewEmployeeDocuments(divUploadResume, divViewResume, btnViewResume, hfResume, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.Resume.ToString());
                    SetViewEmployeeDocuments(divUploadOfferLetter, divViewOfferLetter, btnViewOfferLetter, hfOfferLetter, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.OfferLetter.ToString());
                    SetViewEmployeeDocuments(divUploadJoiningLetter, divViewJoiningLetter, btnViewJoiningLetter, hfJoiningLetter, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.JoiningLetter.ToString());
                    SetViewEmployeeDocuments(divUploadContractPaper, divViewContractPaper, btnViewContractPaper, hfContractPaper, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.ContractPaper.ToString());
                    SetViewEmployeeDocuments(divUploadIDProff, divViewIDProff, btnViewIDProff, hfIDProff, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.IDProff.ToString());
                    SetViewEmployeeDocuments(divUploadOtherDocument, divViewOtherDocument, btnViewOtherDocument, hfOtherDocument, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.OtherDocument.ToString());
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

        #endregion
    }
}