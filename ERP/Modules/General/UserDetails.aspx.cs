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
    public partial class UserDetails : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liUserDetail";

            if (!IsPostBack)
            {
                Guid _id;

                bool _Result = Guid.TryParse(SessionHelper.SessionDetail.EmployeeId.ToString(), out _id);

                if (_Result)
                {
                    FillControls(_id);
                }
            }
        }

        #endregion


        #region Methods

        private void FillControls(Guid p_Id)
        {
            try
            {
                IEmployeeService _IEmployeeService = new EmployeeService();

                Result<Employee> _Result = _IEmployeeService.GetEmployeeById(p_Id);

                if (_Result.IsSuccess)
                {
                    lblFirstName.Text = _Result.Data.FirstName;
                    lblMiddleName.Text = _Result.Data.MiddleName;
                    lblLastName.Text = _Result.Data.LastName;
                    lblFatherName.Text = _Result.Data.FatherName;
                    lblDateOfBirth.Text = _Result.Data.BirthDate.ToString("MM/dd/yyyy");
                    lblGender.Text = _Result.Data.Gender;
                    lblMaritalStatus.Text = _Result.Data.MaratialStatus;
                    lblCast.Text = _Result.Data.Cast;

                    if (!string.IsNullOrEmpty(_Result.Data.PhotoName))
                    {
                        string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeePhoto + "/" + _Result.Data.PhotoName;

                        if (File.Exists(Server.MapPath(_FilePath)))
                        {
                            imgPhoto.Src = _FilePath;                           
                        }
                    }

                    lblEmployeeType.Text = _Result.Data.EmployeeType;
                    lblDepartment.Text = _Result.Data.Department;
                    lblDesignation.Text = _Result.Data.Designation;
                    lblEmployeeGrade.Text = _Result.Data.EmployeeGrade;
                    lblJoinDate.Text = _Result.Data.JoinDate.ToString("MM/dd/yyyy");
                    lblPFNumber.Text = _Result.Data.PFNo;
                    lblShift.Text = _Result.Data.Shift;

                    string _WorkingDay = string.Empty;

                    foreach (string wd in _Result.Data.WorkingDays)
                    {
                        if (string.IsNullOrEmpty(_WorkingDay))
                        {
                            _WorkingDay=wd;
                        }
                        else
                        {
                            _WorkingDay = _WorkingDay+", "+wd;
                        }
                    }

                    lblWorkingDays.Text =_WorkingDay;

                    lblCountry.Text = _Result.Data.Country;
                    lblState.Text = _Result.Data.State;
                    lblCity.Text = _Result.Data.City;
                    lblAddress.Text = _Result.Data.Address;
                    lblPinCode.Text = _Result.Data.PinCode;
                    lblMobile.Text = _Result.Data.MobileNo;
                    lblPhone.Text = _Result.Data.PhoneNo;
                    lblEmail.Text = _Result.Data.Email;

                    lblBankName.Text = _Result.Data.BankName;
                    lblBranchName.Text = _Result.Data.BranchName;
                    lblAccountName.Text = _Result.Data.AccountName;
                    lblAccountNumber.Text = _Result.Data.AccountNo;

                    SetViewEmployeeDocuments(divViewResume, btnViewResume,  _Result.Data.EmployeeAttachments, EmployeeAttachmentType.Resume.ToString());
                    SetViewEmployeeDocuments(divViewOfferLetter, btnViewOfferLetter, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.OfferLetter.ToString());
                    SetViewEmployeeDocuments(divViewJoiningLetter, btnViewJoiningLetter,  _Result.Data.EmployeeAttachments, EmployeeAttachmentType.JoiningLetter.ToString());
                    SetViewEmployeeDocuments(divViewContractPaper, btnViewContractPaper,  _Result.Data.EmployeeAttachments, EmployeeAttachmentType.ContractPaper.ToString());
                    SetViewEmployeeDocuments(divViewIDProff, btnViewIDProff, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.IDProff.ToString());
                    SetViewEmployeeDocuments(divViewOtherDocument, btnViewOtherDocument,  _Result.Data.EmployeeAttachments, EmployeeAttachmentType.OtherDocument.ToString());

                    if (_Result.Data.IsLeave)
                    {
                        SetViewEmployeeDocuments(divResignLetter, btnViewResignLetter, _Result.Data.EmployeeAttachments, EmployeeAttachmentType.ResignLetter.ToString());
                        lblLeaveDate.Text = _Result.Data.LeaveDate.ToString("MM/dd/yyyy");
                        lblLeaveDescription.Text = _Result.Data.LeaveDescription;
                    }
                    else
                    {
                        divResignationInformation.Style.Add("display", "none");
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        private void SetViewEmployeeDocuments(HtmlControl p_divViewDocument, HtmlControl p_btnViewDocument, List<EmployeeAttachments> p_ListOfEmployeeAttachment, string p_DocumentType)
        {
            string _DocumentName = p_ListOfEmployeeAttachment.Where(ed => ed.AttachmentName == p_DocumentType).Select(ed => ed.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(_DocumentName))
            {
                string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeeDocument + "/" + _DocumentName;

                if (File.Exists(Server.MapPath(_FilePath)))
                {
                    p_btnViewDocument.Attributes.Add("href", _FilePath);
                    p_divViewDocument.Style.Add("display", "block");                   
                }
            }
        }

        #endregion

    }
}