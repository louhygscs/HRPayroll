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

namespace ERP.Modules.HRAndPayRoll.Masters
{
    public partial class EmployeeProfile : System.Web.UI.Page
    {
        #region Variables
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        IEmployeeService _IEmployeeService = new EmployeeService();
        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liCutOff_liHR_liEmployeeProfile";

            if(!IsPostBack)
            {
                //fill up
                FillMarriedStatus();
                FillNoOfChildren();
                FillPaymentTerms();
                FillEmploymentStatus();
                FillPositionTitle();
                FillDepartment();

                //FillYearsCompleted();
                FillContractType();

                FillCountry();

                drpCountry.SelectedValue = "815A5321-D34E-47F9-ADD8-0DE89B9F0556";

                FillRegion();
                FillWorkLocation();

                FillCutOffPeriod(drpSchedCutOff);  /* Schedule */
                FillCutOffPeriod(drpCutOffPeriod); /* CutOff Period */
                FillCutOffPeriod(drpSalary);       /* Payslip Summary */

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
        

        protected void drpCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FillRegion();
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #region Basic Information
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _iService = new PayrollService();

                EmployeeProfileModel _model = new EmployeeProfileModel();

                //_model.PicImg = e.PicImg,
                _model.EmployeeNo = txtEmployeeNo.Text;
                _model.StaffCode = txtStaffCode.Text;
                _model.DateHired = DateTime.Parse(txtDateHired.Text.Trim());

                _model.FirstName = txtFirstName.Text;
                _model.MiddleName = txtMiddleName.Text;
                _model.LastName = txtLastName.Text;

                if (rbtnFeMale.Checked)
                {
                    _model.Gender = "FeMale";
                }

                if (rbtnMale.Checked)
                {
                    _model.Gender = "Male";
                }

                _model.MartialStatus = drpMartialStatus.SelectedValue.Trim();

                if (drpMartialStatus.SelectedValue.Trim() == "Married")
                {
                    _model.DateOfMarriage = DateTime.Parse(txtMartialDate.Text.Trim());
                }

                _model.DateOfBirth = DateTime.Parse(txtDateOfBirth.Text.Trim());
                _model.BirthPlace = txtBirthPlace.Text;
                _model.NoOfChildren = decimal.Parse(drpNoOfChildren.SelectedValue.Trim());

                _model.CurrentBasicPay = decimal.Parse(txtBasicPay.Text);
                _model.PaymentTerms = drpPaymentTerms.SelectedValue.Trim();
                _model.EmploymentStatus = drpEmployeeStatus.SelectedValue.Trim();

                _model.HouseStreetNo = txtHouseStreetNo.Text;
                _model.BarangayTownVillage = txtBarangayTown.Text;
                _model.CityMunicipalityProvince = txtCityMunicipality.Text;

                _model.CountryId = Guid.Parse(drpCountry.SelectedValue.Trim());
                _model.RegionId = Guid.Parse(drpRegion.SelectedValue.Trim());
                _model.PostalCode = txtPostalCode.Text;

                _model.HomeTelephoneNo = txtHomeTelephoneNo.Text;
                _model.MobileNo = txtMobileNo.Text;
                _model.EmailAddress = txtEmailAddress.Text;

                _model.HighEducAttainment = txtHighEducAtt.Text;

                _model.School = txtSchool.Text;
                _model.YearsCompleted = txtYearCompleted.Text;
                _model.DateCompleted = DateTime.Parse(txtDateHired.Text.Trim());

                _model.ContractType = drpContractType.Text;
                _model.ContractStartDate = DateTime.Parse(txtContractStartDate.Text.Trim());
                _model.ContractEndDate = DateTime.Parse(txtContractEndDate.Text.Trim());

                _model.DesignationId = Guid.Parse(drpPositionTitle.SelectedValue.Trim());
                _model.WorkLocationId = Guid.Parse(drpWorkLocation.SelectedValue.Trim());
                _model.DepartmentId = Guid.Parse(drpDepartment.SelectedValue.Trim());

                _model.CostCenter = txtCostCenter.Text;
                _model.TypeOfNCR = txtTypeOfNCR.Text;

                _model.TINNo = txtTINNo.Text;
                _model.TaxExemption = txtTaxExemption.Text;
                _model.SSSNo = txtSSSNo.Text;

                _model.PagIbigNo = txtPagIbigNo.Text;
                _model.PhilhealthNo = txtPhilHealthNo.Text;
                _model.DriverLicenseNo = txtDriverLicenseNo.Text;

                _model.Remarks = txtAbout.Text;

                _model.CreatedDate = DateTime.Now;
                _model.CreatedBy = SessionHelper.SessionDetail.UserID;
                _model.IsActive = true;

                if (fuPhoto.HasFile)
                {
                    _model.PicImg = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(fuPhoto.FileName).ToLower();
                }

                Result<Boolean> _Result = _iService.SaveEmployeeProfile(_model);

                if (_Result.IsSuccess)
                {
                    //Image 
                    if (!string.IsNullOrEmpty(_model.PicImg))
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

                        fuPhoto.SaveAs(_PhotoRootPath + _model.PicImg);
                    }

                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Profile");

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/EmployeeProfileList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Employee Profile") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }

        }
        #endregion

        #region Schedule

        /* Load Schedule */
        protected void btnGenerateSched_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                Guid _empId = Guid.Parse(hfId.Value);
                Guid _ctOffId = Guid.Parse(drpSchedCutOff.SelectedValue.ToString());

                Result<List<EmployeeScheduleModel>> _Result = _IService.GetByEmployeeSchedule(_empId, _ctOffId);

                if (_Result.IsSuccess)
                {
                    grdEmpSched.DataSource = _Result.Data;
                    grdEmpSched.DataBind();

                    if (grdEmpSched.Rows.Count > 0)
                    {
                        grdEmpSched.UseAccessibleHeader = true;
                        grdEmpSched.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        /* Save Schedule */
        protected void btnSaveSched_Click(object sender, EventArgs e)
        {
            try
            {
                Result<bool> _Result = new Result<bool>();

                IPayrollService _IService = new PayrollService();

                Guid _empId = Guid.Parse(hfId.Value);
                Guid _ctOffId = Guid.Parse(drpSchedCutOff.SelectedValue.ToString());

                foreach (GridViewRow _itm in grdEmpSched.Rows)
                {
                    string _actDate = _itm.Cells[1].Text;
                    string _dayName = _itm.Cells[2].Text;

                    DropDownList _drpShift = (DropDownList)_itm.FindControl("ddlDayShift");

                    Guid _shiftId = Guid.Parse(_drpShift.SelectedValue.ToString());

                    EmployeeScheduleModel _mdl = new EmployeeScheduleModel()
                    {
                        EmpShiftId = Guid.Empty,
                        ShiftId    = _shiftId,
                        EmployeeId = _empId,
                        CutOffId   = _ctOffId,
                        ActualDate = DateTime.Parse(_actDate),
                        Remarks    = string.Empty,
                        IsActive   = true,
                        CreatedBy  = SessionHelper.SessionDetail.UserID,
                        CreateDate = DateTime.Now,
                    };

                    _Result = _IService.SaveEmployeeSchedule(_mdl);
                }

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Schedule");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        /* Grid Row Bound */
        protected void grdEmpSched_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row.
                DropDownList ddlDayShift = (e.Row.FindControl("ddlDayShift") as DropDownList);

                //Add Default Item in the DropDownList.
                FillShift(ddlDayShift);

                string shiftId = (e.Row.FindControl("hdDayShiftId") as HiddenField).Value;

                ddlDayShift.Items.FindByValue(shiftId).Selected = true;
            }
        }

        #endregion

        #region Timelogs

        /* Load Timelogs */
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                Guid _empId = Guid.Parse(hfId.Value);
                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

                Result<List<DTRRawModel>> _Result = _IService.GetEmployeeDTRRaw(_empId, _ctOffId);

                if (_Result.IsSuccess)
                {
                    gvList.DataSource = _Result.Data;
                    gvList.DataBind();

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.UseAccessibleHeader = true;
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    //if (gvList.Rows.Count > 0)
                    //{
                    //    gvList.UseAccessibleHeader = true;
                    //    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

                    //    int ttlDays       = _Result.Data.Count;
                    //    decimal ttlWrks   = _Result.Data.Sum(p => p.WorkingHrs).Value;
                    //    decimal ttlLate   = _Result.Data.Sum(p => p.LateHrs).Value;
                    //    decimal ttlOver   = _Result.Data.Sum(p => p.OvertimeHrs).Value;
                    //    decimal ttlAdjust = _Result.Data.Sum(p => p.AdjustHrs).Value;

                    //    lblTotalDays.Text     = string.Format("Total Days: {0}",ttlDays);
                    //    lblTotalWrks.Text     = string.Format("Total Working Hrs: {0}",ttlWrks);
                    //    lblTotalLate.Text     = string.Format("Total Late Hrs: {0}",ttlLate);
                    //    lblTotalOvertime.Text = string.Format("Total Overtime Hrs: {0}",ttlOver);
                    //    lblTotalAdjust.Text   = string.Format("Total Adjustment Hrs: {0}", ttlAdjust);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        /* Grid Row Bound */
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row.
                DropDownList ddlDayShift = (e.Row.FindControl("ddlTimeLogDay1Schedule") as DropDownList);

                //Add Default Item in the DropDownList.
                FillShift(ddlDayShift);

                string shiftId = (e.Row.FindControl("hdDayTimeLogShiftId") as HiddenField).Value;

                ddlDayShift.Items.FindByValue(shiftId).Selected = true;
            }
        }

        /* Save Timelogs */
        protected void BtnSaveTimelogs_Click(object sender, EventArgs e)
        {
            try
            {
                Result<bool> _Result = new Result<bool>();

                IPayrollService _IService = new PayrollService();

                Guid _empId   = Guid.Parse(hfId.Value);
                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

                foreach (GridViewRow _itm in gvList.Rows)
                {
                    string _actDate = _itm.Cells[0].Text;
                    string _dayName = _itm.Cells[1].Text;
                    string _timeType = _itm.Cells[3].Text;

                    TextBox _txtActualTime = (TextBox)_itm.FindControl("txtActualTime");
                    string _actTime = _txtActualTime.Text;

                    DropDownList _ddlTimeLogStatus = (DropDownList)_itm.FindControl("ddlTimeLogStatus");
                    string _drpStatus = _ddlTimeLogStatus.Text;

                    DropDownList _ddlTimeLogDay1Schedule = (DropDownList)_itm.FindControl("ddlTimeLogDay1Schedule");

                    //Guid _shiftId = Guid.Parse(_drpShift.SelectedValue.ToString());

                    //EmployeeScheduleModel _mdl = new EmployeeScheduleModel()
                    //{
                    //    EmpShiftId = Guid.Empty,
                    //    ShiftId = _shiftId,
                    //    EmployeeId = _empId,
                    //    CutOffId = _ctOffId,
                    //    ActualDate = DateTime.Parse(_actDate),
                    //    Remarks = string.Empty,
                    //    IsActive = true,
                    //    CreatedBy = SessionHelper.SessionDetail.UserID,
                    //    CreateDate = DateTime.Now,
                    //};

                    //_Result = _IService.SaveEmployeeSchedule(_mdl);
                }

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Timelogs");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }
        #endregion

        #region Payslip Summary
        /* load payslip */
        protected void btnGenSalary_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region History
        #endregion

        #region Documents
        #endregion

        #endregion

        #region Methods

        private void FillSchedule(DropDownList drpSchedule)
        {
            if (drpSchedule != null)
            {
                //drpSchedule.Items.Clear();

                Result<List<Item>> _Result = _ILookupService.GetAllShift();

                if (_Result.IsSuccess)
                {
                    drpSchedule.DataTextField = "Text";
                    drpSchedule.DataValueField = "Id";
                    drpSchedule.DataSource = _Result.Data;
                    drpSchedule.DataBind();
                }

                drpSchedule.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        private void FillMarriedStatus()
        {
            drpMartialStatus.Items.Clear();

            drpMartialStatus.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            drpMartialStatus.Items.Insert(1, new ListItem() { Text = "Single", Value = "Single" });
            drpMartialStatus.Items.Insert(2, new ListItem() { Text = "Married", Value = "Married" });
            drpMartialStatus.Items.Insert(3, new ListItem() { Text = "Widow", Value = "Widow" });
            drpMartialStatus.Items.Insert(4, new ListItem() { Text = "Separated", Value = "Separated" });
        }

        private void FillNoOfChildren()
        {
            drpNoOfChildren.Items.Clear();

            drpNoOfChildren.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            drpNoOfChildren.Items.Insert(1, new ListItem() { Text = "0", Value = "0" });
            drpNoOfChildren.Items.Insert(2, new ListItem() { Text = "1", Value = "1" });
            drpNoOfChildren.Items.Insert(3, new ListItem() { Text = "2", Value = "2" });
            drpNoOfChildren.Items.Insert(4, new ListItem() { Text = "3", Value = "3" });
            drpNoOfChildren.Items.Insert(5, new ListItem() { Text = "4", Value = "4" });
            drpNoOfChildren.Items.Insert(6, new ListItem() { Text = "5", Value = "5" });
            drpNoOfChildren.Items.Insert(7, new ListItem() { Text = "6", Value = "6" });
            drpNoOfChildren.Items.Insert(8, new ListItem() { Text = "7", Value = "7" });
            drpNoOfChildren.Items.Insert(9, new ListItem() { Text = "8", Value = "8" });
            drpNoOfChildren.Items.Insert(10, new ListItem() { Text = "9", Value = "9" });
            drpNoOfChildren.Items.Insert(11, new ListItem() { Text = "10", Value = "10" });
            drpNoOfChildren.Items.Insert(12, new ListItem() { Text = "11", Value = "11" });
            drpNoOfChildren.Items.Insert(13, new ListItem() { Text = "12", Value = "12" });
        }

        private void FillPaymentTerms()
        {
            drpPaymentTerms.Items.Clear();

            drpPaymentTerms.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            drpPaymentTerms.Items.Insert(1, new ListItem() { Text = "DAILY RATE/DAY", Value = "DAILY RATE/DAY" });
            drpPaymentTerms.Items.Insert(2, new ListItem() { Text = "MONTHLY RATE"  , Value = "MONTHLY RATE" });
            
        }

        private void FillEmploymentStatus()
        {
            drpEmployeeStatus.Items.Clear();

            drpEmployeeStatus.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            drpEmployeeStatus.Items.Insert(1, new ListItem() { Text = "Active", Value = "Active" });
            drpEmployeeStatus.Items.Insert(2, new ListItem() { Text = "Inactive", Value = "Inactive" });

        }

        private void FillContractType()
        {
            drpContractType.Items.Clear();

            drpContractType.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            drpContractType.Items.Insert(1, new ListItem() { Text = "External", Value = "External" });
            drpContractType.Items.Insert(2, new ListItem() { Text = "Non External", Value = "Non External" });
        }

        private void FillPositionTitle()
        {
            drpPositionTitle.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetAllDesignation();

            if (_Result.IsSuccess)
            {
                drpPositionTitle.DataTextField = "Text";
                drpPositionTitle.DataValueField = "Id";
                drpPositionTitle.DataSource = _Result.Data;
                drpPositionTitle.DataBind();
            }

            drpPositionTitle.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });

        }

        private void FillDepartment()
        {
            drpDepartment.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetAllDepartment();

            if (_Result.IsSuccess)
            {
                drpDepartment.DataTextField = "Text";
                drpDepartment.DataValueField = "Id";
                drpDepartment.DataSource = _Result.Data;
                drpDepartment.DataBind();
            }

            drpDepartment.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillCountry()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllCountry();

            if (_Result.IsSuccess)
            {
                drpCountry.DataTextField  = "Text";
                drpCountry.DataValueField = "Id";
                drpCountry.DataSource     = _Result.Data;
                drpCountry.DataBind();
            }

            drpCountry.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillRegion()
        {
            drpRegion.Items.Clear();

            string _CountryId = string.Empty;  //drpCountry.SelectedValue;

            if(!string.IsNullOrEmpty(drpCountry.SelectedValue))
            {
                _CountryId = drpCountry.SelectedValue;
            } else
            {
                _CountryId = "815A5321-D34E-47F9-ADD8-0DE89B9F0556";
            }

            if (!string.IsNullOrEmpty(_CountryId))
            {
                Result<List<Item>> _Result = _ILookupService.GetAllStateByCountryId(new Guid(_CountryId));

                if (_Result.IsSuccess)
                {
                    drpRegion.DataTextField = "Text";
                    drpRegion.DataValueField = "Id";
                    drpRegion.DataSource = _Result.Data;
                    drpRegion.DataBind();
                }

                drpRegion.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        private void FillWorkLocation()
        {
            drpWorkLocation.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetWorkLocation(Guid.Parse("F699B807-07E7-45FD-BAC6-0F3BFF1E34BA"));

            if (_Result.IsSuccess)
            {
                drpWorkLocation.DataTextField = "Text";
                drpWorkLocation.DataValueField = "Id";
                drpWorkLocation.DataSource = _Result.Data;
                drpWorkLocation.DataBind();
            }

            drpWorkLocation.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillCutOffPeriod(DropDownList _drpCutOff)
        {
            _drpCutOff.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetCutOffPeriod();

            if (_Result.IsSuccess)
            {
                _drpCutOff.DataTextField = "Text";
                _drpCutOff.DataValueField = "Id";
                _drpCutOff.DataSource = _Result.Data;
                _drpCutOff.DataBind();
            }

            _drpCutOff.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void InitialControls(bool isEnabled)
        {
            txtAbout.ReadOnly = isEnabled;

            txtEmployeeNo.ReadOnly = isEnabled;
            txtDateHired.ReadOnly = isEnabled;

            txtFirstName.ReadOnly = isEnabled;
            txtMiddleName.ReadOnly = isEnabled;
            txtLastName.ReadOnly = isEnabled;

            rbtnMale.Enabled = !isEnabled;
            rbtnFeMale.Enabled = !isEnabled;

            drpMartialStatus.Enabled = !isEnabled;

            txtMartialDate.ReadOnly = isEnabled;

            txtDateOfBirth.ReadOnly = isEnabled;
            txtBirthPlace.ReadOnly = isEnabled;
            drpNoOfChildren.Enabled = !isEnabled;

            txtBasicPay.ReadOnly = isEnabled;
            drpPaymentTerms.Enabled = !isEnabled;
            drpEmployeeStatus.Enabled = !isEnabled;


            txtHouseStreetNo.ReadOnly = isEnabled;
            txtBarangayTown.ReadOnly = isEnabled;
            txtCityMunicipality.ReadOnly = isEnabled;

            drpCountry.Enabled = !isEnabled;
            drpRegion.Enabled = !isEnabled;
            txtPostalCode.ReadOnly = isEnabled;

            txtHomeTelephoneNo.ReadOnly = isEnabled;
            txtMobileNo.ReadOnly = isEnabled;
            txtEmailAddress.ReadOnly = isEnabled;

            txtHighEducAtt.ReadOnly = isEnabled;

            txtSchool.ReadOnly = isEnabled;
            txtYearCompleted.Enabled = !isEnabled;
            txtDateCompleted.ReadOnly = isEnabled;

            drpContractType.Enabled = !isEnabled;
            txtContractStartDate.ReadOnly = isEnabled;
            txtContractEndDate.ReadOnly = isEnabled;

            drpPositionTitle.Enabled = !isEnabled;
            drpWorkLocation.Enabled = !isEnabled;
            drpDepartment.Enabled = !isEnabled;

            txtCostCenter.ReadOnly = isEnabled;
            txtTypeOfNCR.ReadOnly = isEnabled;

            txtTINNo.ReadOnly = isEnabled;
            txtTaxExemption.ReadOnly = isEnabled;
            txtSSSNo.ReadOnly = isEnabled;

            txtPagIbigNo.ReadOnly = isEnabled;
            txtPhilHealthNo.ReadOnly = isEnabled;
            txtDriverLicenseNo.ReadOnly = isEnabled;
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                Result<EmployeeProfileModel> _Result = _IService.GetByEmployeeProfile(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = _Result.Data.EmployeeId.ToString();

                    //Pic Image
                    if (!string.IsNullOrEmpty(_Result.Data.PicImg))
                    {
                        string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeePhoto + "/" + _Result.Data.PicImg;

                        if (File.Exists(Server.MapPath(_FilePath)))
                        {
                            imgPhoto.Src = _FilePath;
                            divUploadPhoto.Style.Add("display", "none");
                            divViewPhoto.Style.Add("display", "block");
                            revPhoto.Enabled = false;
                            hfPhoto.Value = _Result.Data.PicImg;
                        }
                    }

                    lblFullName.Text = _Result.Data.FullName;
                    lnkEmail.Text = _Result.Data.EmailAddress;
                    lnkEmail.NavigateUrl = string.Format("mailto:{0}", _Result.Data.EmailAddress);

                    txtEmployeeNo.Text = _Result.Data.EmployeeNo;
                    txtStaffCode.Text = _Result.Data.StaffCode;
                    txtDateHired.Text  = _Result.Data.DateHired.Value.ToString("yyyy-MM-dd");

                    txtFirstName.Text  = _Result.Data.FirstName;
                    txtMiddleName.Text = _Result.Data.MiddleName;
                    txtLastName.Text   = _Result.Data.LastName;

                    //gender
                    if (_Result.Data.Gender == "Male")
                    {
                        rbtnMale.Checked = true;
                    } else {
                        rbtnMale.Checked = false;
                    }

                    if(_Result.Data.Gender == "FeMale")
                    {
                        rbtnFeMale.Checked = true;
                    } else
                    {
                        rbtnFeMale.Checked = false;
                    }

                    drpMartialStatus.SelectedValue = _Result.Data.MartialStatus;
                    
                    if(_Result.Data.DateOfMarriage.HasValue)
                    {
                        txtMartialDate.Text = _Result.Data.DateOfMarriage.Value.ToString("yyyy-MM-dd");
                    }
                    
                    txtDateOfBirth.Text = _Result.Data.DateOfBirth.Value.ToString("yyyy-MM-dd");
                    txtBirthPlace.Text = _Result.Data.BirthPlace;
                    
                    string _noChildren = "0";

                    if(!string.IsNullOrEmpty(_Result.Data.NoOfChildren.ToString()))
                    {
                        if (_Result.Data.NoOfChildren.HasValue)
                        {
                            _noChildren = decimal.ToInt32(_Result.Data.NoOfChildren.Value).ToString();
                        }
                    }

                    drpNoOfChildren.SelectedValue = _noChildren;

                    txtBasicPay.Text = _Result.Data.CurrentBasicPay.ToString();
                    drpPaymentTerms.SelectedValue = _Result.Data.PaymentTerms;
                    drpEmployeeStatus.SelectedValue = _Result.Data.EmploymentStatus;

                    txtHouseStreetNo.Text = _Result.Data.HouseStreetNo;
                    txtBarangayTown.Text = _Result.Data.BarangayTownVillage;
                    txtCityMunicipality.Text = _Result.Data.CityMunicipalityProvince;

                    drpCountry.SelectedValue = _Result.Data.CountryId.ToString();
                    drpRegion.SelectedValue = _Result.Data.RegionId.ToString();
                    txtPostalCode.Text = _Result.Data.PostalCode;

                    txtHomeTelephoneNo.Text = _Result.Data.HomeTelephoneNo;
                    txtMobileNo.Text = _Result.Data.MobileNo;
                    txtEmailAddress.Text = _Result.Data.EmailAddress;

                    txtHighEducAtt.Text = _Result.Data.HighEducAttainment;

                    txtSchool.Text = _Result.Data.School;
                    txtYearCompleted.Text = _Result.Data.YearsCompleted;
                    txtDateCompleted.Text = _Result.Data.DateCompleted.Value.ToString("yyyy-MM-dd");

                    drpContractType.SelectedValue = _Result.Data.ContractType;
                    txtContractStartDate.Text = _Result.Data.ContractStartDate.Value.ToString("yyyy-MM-dd");
                    txtContractEndDate.Text = _Result.Data.ContractEndDate.Value.ToString("yyyy-MM-dd");

                    drpPositionTitle.SelectedValue = _Result.Data.DesignationId.ToString();
                    drpWorkLocation.SelectedValue = _Result.Data.WorkLocationId.ToString();
                    drpDepartment.SelectedValue = _Result.Data.DepartmentId.ToString();

                    txtCostCenter.Text = _Result.Data.CostCenter;
                    txtTypeOfNCR.Text = _Result.Data.TypeOfNCR;

                    txtTINNo.Text = _Result.Data.TINNo;
                    txtTaxExemption.Text = _Result.Data.TaxExemption;
                    txtSSSNo.Text = _Result.Data.SSSNo;

                    txtPagIbigNo.Text = _Result.Data.PagIbigNo;
                    txtPhilHealthNo.Text = _Result.Data.PhilhealthNo;
                    txtDriverLicenseNo.Text = _Result.Data.DriverLicenseNo;

                    txtAbout.Text = _Result.Data.Remarks;
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

        private void FillShift(DropDownList drpSchedule)
        {
            drpSchedule.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetAllShift();

            if (_Result.IsSuccess)
            {
                drpSchedule.DataTextField = "Text";
                drpSchedule.DataValueField = "Id";
                drpSchedule.DataSource = _Result.Data;
                drpSchedule.DataBind();
            }

            drpSchedule.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        #endregion

        protected void lnkEmail_PreRender(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void BtnGenerate2_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _iService = new PayrollService();

                Result<Boolean> _Result = new Result<bool>();

                for (int i = 1; i < 1116; i++)
                {
                    EmployeeProfileModel _model = new EmployeeProfileModel()
                    {
                        EmployeeNo = string.Format("EMP0{0}", i),
                        DateHired = DateTime.Now,

                        FirstName = "FirstName",
                        MiddleName = "MiddleName",
                        LastName = "LastName",

                        Gender = "Male",
                        MartialStatus = "Single",

                        DateOfMarriage = null,
                        DateOfBirth = DateTime.Now,
                        BirthPlace = "Davao City",

                        NoOfChildren = 1,
                        CurrentBasicPay = 390,
                        PaymentTerms = "DAILY RATE/DAY",
                        EmploymentStatus = "ACTIVE",

                        HouseStreetNo = "Davao City",
                        BarangayTownVillage = "Davao City",
                        CityMunicipalityProvince = "Davao City",

                        CountryId = Guid.Parse("815A5321-D34E-47F9-ADD8-0DE89B9F0556"),
                        RegionId = Guid.Parse("84CFC1AA-5BBE-414F-AFEA-6EF2EB67CC59"),
                        PostalCode = "8000",

                        HomeTelephoneNo = "09175236214",
                        MobileNo = "09175236214",
                        EmailAddress = "info@fullsupportmanpower.com",

                        HighEducAttainment = "College Graduate",
                        School = "Holy Cross of Davao",
                        YearsCompleted = "1 Year",
                        DateCompleted = DateTime.Now,

                        ContractType = "External",
                        ContractStartDate = DateTime.Now,
                        ContractEndDate = DateTime.Now.AddMonths(6),

                        DesignationId = Guid.Parse("9121B77A-FC77-4AD2-8F28-511DC42C2EF8"),
                        WorkLocationId = Guid.Parse("CCF390DF-3D27-47DF-9AAD-907F024D3941"),
                        DepartmentId = Guid.Parse("CA5AB8C2-5EDA-4F4A-B281-3C90D8FA87B1"),

                        CostCenter = "",
                        TypeOfNCR = "",

                        TINNo = "000-000-000",
                        SSSNo = "000-000-000",
                        PagIbigNo = "000-000-000",

                        PhilhealthNo = "000-000-000",
                        DriverLicenseNo = "000-000-000",
                        Remarks = "remarks"
                    };

                    _Result = _iService.SaveEmployeeProfile(_model);
                }

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Employee Profile");
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        
    }
}