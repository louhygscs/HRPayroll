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

namespace ERP.Modules
{
    public partial class Profile : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liDashboard";

            if (!IsPostBack)
            {
                Guid _id;

                bool _Result = Guid.TryParse(SessionHelper.SessionDetail.EmployeeId.ToString(), out _id);

                FillCountry();

                if (_Result)
                {
                    FillControls(_id);
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

                _Employee.FirstName = txtFirstName.Text.Trim();
                _Employee.MiddleName = txtMiddleName.Text.Trim();
                _Employee.LastName = txtLastName.Text.Trim();
                _Employee.FatherName = txtFatherName.Text.Trim();
                _Employee.BirthDate = GlobalHelper.StringToDate(txtBirthDate.Value.Trim());
                _Employee.IsGender = rbtnMale.Checked;
                _Employee.MaratialStatus = ddlMaratialStatus.SelectedValue;
                _Employee.Cast = txtCast.Text.Trim();

                _Employee.CountryId = new Guid(ddlCountry.SelectedValue);
                _Employee.StateId = new Guid(ddlState.SelectedValue);
                _Employee.City = txtCity.Text.Trim();
                _Employee.Address = txtAddress.Text.Trim();
                _Employee.PinCode = txtPinCode.Text.Trim();
                _Employee.MobileNo = txtMobile.Text.Trim();
                _Employee.PhoneNo = txtPhone.Text.Trim();

                if (fuPhoto.HasFile)
                {
                    _Employee.PhotoName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(fuPhoto.FileName).ToLower();
                }

                IEmployeeService _IEmployeeService = new EmployeeService();
                Result<Boolean> _Result = _IEmployeeService.UpdateEmployeeProfile(_Employee, SessionHelper.SessionDetail.UserID);

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

                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Profile");

                    IHistoryService _IHistoryService = new HistoryService();


                    _IHistoryService.InsertHistory<Employee>(Convert.ToString(_Employee.EmployeeID), TableType.EmployeeMaster, OperationType.Update, _Employee, SessionHelper.SessionDetail.UserID);


                    Response.Redirect("~/Modules/Main.aspx", false);
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

        private void FillControls(Guid p_Id)
        {
            try
            {
                IEmployeeService _IEmployeeService = new EmployeeService();
                Result<Employee> _Result = _IEmployeeService.GetEmployeeById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);

                    ddlCountry.SelectedValue = Convert.ToString(_Result.Data.CountryId);

                    FillState();

                    ddlState.SelectedValue = Convert.ToString(_Result.Data.StateId);
                    txtFirstName.Text = _Result.Data.FirstName;
                    txtMiddleName.Text = _Result.Data.MiddleName;
                    txtLastName.Text = _Result.Data.LastName;
                    txtBirthDate.Value = _Result.Data.BirthDate.ToString("MM/dd/yyyy");
                    txtFatherName.Text = _Result.Data.FatherName;
                    rbtnMale.Checked = _Result.Data.IsGender;
                    rbtnFeMale.Checked = !_Result.Data.IsGender;
                    ddlMaratialStatus.SelectedValue = _Result.Data.MaratialStatus;
                    txtCast.Text = _Result.Data.Cast;
                    txtCity.Text = _Result.Data.City;
                    txtAddress.Text = _Result.Data.Address;
                    txtPinCode.Text = _Result.Data.PinCode;
                    txtMobile.Text = _Result.Data.MobileNo;
                    txtPhone.Text = _Result.Data.PhoneNo;

                    if (!string.IsNullOrEmpty(_Result.Data.PhotoName))
                    {
                        string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeePhoto + "/" + _Result.Data.PhotoName;

                        if (File.Exists(Server.MapPath(_FilePath)))
                        {
                            imgPhoto.Src = _FilePath;
                            divUploadPhoto.Style.Add("display", "none");
                            divViewPhoto.Style.Add("display", "block");
                            rfvPhoto.Enabled = false;
                            revPhoto.Enabled = false;
                            hfPhoto.Value = _Result.Data.PhotoName;
                        }
                    }
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

        #endregion
    }
}