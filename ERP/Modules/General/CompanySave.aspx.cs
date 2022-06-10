using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.General
{
    public partial class CompanySave : System.Web.UI.Page
    {

        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        ICompanyService _ICompanyService = new CompanyService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liGeneralSettings";

            if (!IsPostBack)
            {
                FillCountry();

                FillControls();
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
                _Logger.Info("1");

                Company _Company = new Company();
                _Company.CompanyID = new Guid(hfId.Value);
                _Company.CompanyName = txtCompanyName.Text.Trim();
                _Company.EmailAddress = txtEmail.Text.Trim();
                _Company.CountryId = new Guid(ddlCountry.SelectedValue);
                _Company.StateId = new Guid(ddlState.SelectedValue);
                _Company.City = txtCity.Text.Trim();
                _Company.Address = txtAddress.Text.Trim();
                _Company.MobileNo = txtMobile.Text.Trim();
                _Company.PhoneNo = txtPhone.Text.Trim();
                _Company.HotLineNo = txtHotlineNo.Text.Trim();
                _Company.FaxNo = txtFaxNo.Text.Trim();
                _Company.WebSite = txtWebSite.Text.Trim();
               // _Company.LicenseKey = txtKey.Text.Trim();

                if (fuLogo.HasFile)
                {
                    _Company.CompanyLogo = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(fuLogo.FileName).ToLower();
                }

                Result<Boolean> _Result = _ICompanyService.SaveCompany(_Company, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(_Company.CompanyLogo))
                    {
                        string _PhotoRootPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.CompanyLogo + "/");

                        if (!Directory.Exists(_PhotoRootPath))
                        {
                            Directory.CreateDirectory(_PhotoRootPath);
                        }

                        if (!string.IsNullOrEmpty(hfLogo.Value))
                        {
                            string _PhotoFilePath = _PhotoRootPath + hfLogo.Value;

                            if (File.Exists(_PhotoFilePath))
                            {
                                File.Delete(_PhotoFilePath);
                            }
                        }

                        fuLogo.SaveAs(_PhotoRootPath + _Company.CompanyLogo);
                    }

                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Company");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_Company.CompanyID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<Company>(_Result.Id, TableType.CompanyMaster, OperationType.Insert, _Company, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<Company>(Convert.ToString(_Company.CompanyID), TableType.CompanyMaster, OperationType.Update, _Company, SessionHelper.SessionDetail.UserID);
                    }
                    _Logger.Info("success");
                    Response.Redirect("~/Modules/Main.aspx", false);
                }
                else
                {
                    _Logger.Info("warning");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Company") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Info("exception");
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnGetKey_Click(object sender, EventArgs e)
        {
            try
            {
                string _Body = string.Empty;
                var _TemplatePath = Server.MapPath("~/EmailTemplate/GetLicenseKeyRequest.html");

                using (var _StreamReader = new StreamReader(_TemplatePath))
                {
                    _Body = _StreamReader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(_Body))
                {
                    _Body = _Body.Replace("@Email", Convert.ToString(SessionHelper.SessionDetail.Email));
                }

                List<string> _ToMail = new List<string>();
                _ToMail.Add(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]));
                bool _Flag = EmailHelper.SendMail("Get License Key", _Body, _ToMail);
                if (_Flag)
                {
                    SessionHelper.MessageSession = GlobalMsg.SendSuccessMsg;
                    Response.Redirect("~/Modules/Main.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.SendFailMsg + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnActivateKey_Click(object sender, EventArgs e)
        {
            try
            {
                //string Email = "shivanikalriya21@gmail.com";
                bool _IsActive = true;
                //if (_IsActive != false)
                //{
                //    Result<bool> _Result = _ICompanyService.LicenseKeyActivate(new Guid(hfId.Value));
                //    if (_Result.IsSuccess)
                //    {
                //        string _Body = string.Empty;
                //        var _TemplatePath = Server.MapPath("~/EmailTemplate/ActivateKeyRequest.html");

                //        using (var _StreamReader = new StreamReader(_TemplatePath))
                //        {
                //            _Body = _StreamReader.ReadToEnd();
                //        }

                //        if (!string.IsNullOrEmpty(_Body))
                //        {
                //            _Body = _Body.Replace("@Email", Convert.ToString(txtEmail.Text));
                //            _Body = _Body.Replace("@Key", Convert.ToString(txtKey.Text));
                //        }

                //        List<string> _ToMail = new List<string>();
                //        _ToMail.Add(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendEmailID"]));
                //        bool _Flag = EmailHelper.SendMail("Activate License Key", _Body, _ToMail);
                //        if (_Flag)
                //        {
                //            SessionHelper.MessageSession = GlobalMsg.SendSuccessMsg;
                //            Response.Redirect("~/Modules/Main.aspx", false);
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.SendFailMsg + "');});", true);
                //        }
                //    }
                //}
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }
      
        #endregion

        #region Methods

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

        private void FillControls()
        {
            try
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                Result<Company> _Result = _ICompanyService.GetCompany();

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(_Result.Data.CompanyID);
                    txtCompanyName.Text = _Result.Data.CompanyName;
                    txtEmail.Text = _Result.Data.EmailAddress;
                    ddlCountry.SelectedValue = Convert.ToString(_Result.Data.CountryId);

                    FillState();

                    ddlState.SelectedValue = Convert.ToString(_Result.Data.StateId);
                    txtCity.Text = _Result.Data.City;
                    txtAddress.Text = _Result.Data.Address;
                    txtMobile.Text = _Result.Data.MobileNo;
                    txtPhone.Text = _Result.Data.PhoneNo;
                    txtHotlineNo.Text = _Result.Data.HotLineNo;
                    txtFaxNo.Text = _Result.Data.FaxNo;
                    txtWebSite.Text = _Result.Data.WebSite;
                    //txtKey.Text = _Result.Data.LicenseKey;
                    //if (!string.IsNullOrEmpty(_Result.Data.LicenseKey))
                    //{
                    //    txtKey.ReadOnly = true;
                    //    btnGetKey.Visible = false;
                    //}
                    //if (_Result.Data.IsKeyActive)
                    //{
                    //    btnGetKey.Visible = false;
                    //    btnActivateKey.Visible = false;
                    //}
                    if (!string.IsNullOrEmpty(_Result.Data.CompanyLogo))
                    {
                        string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.CompanyLogo + "/" + _Result.Data.CompanyLogo;

                        if (File.Exists(Server.MapPath(_FilePath)))
                        {
                            imgLogo.Src = _FilePath;
                            divUploadLogo.Style.Add("display", "none");
                            divViewLogo.Style.Add("display", "block");
                            revLogo.Enabled = false;
                            hfLogo.Value = _Result.Data.CompanyLogo;
                        }
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        private bool ActivateKey(string p_Email, string p_Key)
        {
            bool _Flag = false;
            try
            {
               // _Flag = _ERPService.GetKeyActivate(p_Email, p_Key);
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
            return _Flag;
        }
        #endregion

    }
}