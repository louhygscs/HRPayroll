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

namespace ERP.Modules.HRAndPayRoll.Masters.EmployeeSchedule
{
    public partial class EmployeeScheduleList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liEmployeeSchedule_liHR_liHRMasters";

            if (!IsPostBack)
            {
                //fill up
                FillWorkLocation();
                FillCutOffPeriod();

                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }
        #endregion

        #region Events

        #endregion

        #region Fill Controls
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

        private void FillCutOffPeriod()
        {
            drpCutOffPeriod.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetCutOffPeriod();

            if (_Result.IsSuccess)
            {
                drpCutOffPeriod.DataTextField = "Text";
                drpCutOffPeriod.DataValueField = "Id";
                drpCutOffPeriod.DataSource = _Result.Data;
                drpCutOffPeriod.DataBind();
            }

            drpCutOffPeriod.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillSchedule(DropDownList drpSchedule)
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

        private void FillRegion()
        {
            //ddlDay1Schedule.

            //DayNameFormat .Items.Clear();

            //string _CountryId = string.Empty;  //drpCountry.SelectedValue;

            //if (!string.IsNullOrEmpty(drpCountry.SelectedValue))
            //{
            //    _CountryId = drpCountry.SelectedValue;
            //}
            //else
            //{
            //    _CountryId = "815A5321-D34E-47F9-ADD8-0DE89B9F0556";
            //}

            //if (!string.IsNullOrEmpty(_CountryId))
            //{
            //    Result<List<Item>> _Result = _ILookupService.GetAllStateByCountryId(new Guid(_CountryId));

            //    if (_Result.IsSuccess)
            //    {
            //        drpRegion.DataTextField = "Text";
            //        drpRegion.DataValueField = "Id";
            //        drpRegion.DataSource = _Result.Data;
            //        drpRegion.DataBind();
            //    }

            //    drpRegion.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            //}
        }

        #endregion

        protected void btnEmpSchedGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _IService = new PayrollService();
                IShiftService _IShiftService = new ShiftService();

                Guid _wrkId = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());
                Guid _shiftId = Guid.Empty;

                Guid _shiftOff = Guid.Parse("94D6B894-2BA8-4B83-8B5E-EF3A56F481E2");
                Guid _shiftReg = Guid.Parse("F9DAB584-5287-44AD-A05E-366EB5B4337D");
                
                Result<PayrollCutOff> _Result = _IService.GetByCutOffPeriodId(_ctOffId);
                Result<List<EmployeeProfileModel>> _ResultEmpProfiles = _IService.GetEmployeeProfilesByWorkLocationId(_wrkId);

                if (_Result.IsSuccess && _ResultEmpProfiles.IsSuccess)
                {
                    PayrollCutOff _co = _Result.Data;
                    List<EmployeeProfileModel> _empProfiles = _ResultEmpProfiles.Data;

                    if (_co != null)
                    {
                        foreach (EmployeeProfileModel _emp in _empProfiles)
                        {
                            int intervalDays = 1;
                            int columnIndex  = 4;

                            DateTime startdate = _co.StartDate;

                            while (startdate <= _co.EndDate.AddDays(-1))
                            {
                                columnIndex = columnIndex + 1;

                                string dayName = startdate.ToString("dddd");
                                    
                                if(dayName == "Sunday")
                                {
                                    _shiftId = _shiftOff;
                                } else
                                {
                                    _shiftId = _shiftReg;
                                }

                                //save employee shift
                                EmployeeScheduleModel _empshftModel = new EmployeeScheduleModel()
                                {
                                     ShiftId    = _shiftId,
                                     EmployeeId = _emp.EmployeeId,
                                     CutOffId   = _co.PayrollCutOffId,
                                     ActualDate = startdate,
                                     Remarks    = string.Empty,
                                     IsActive   = true
                                };

                                _IService.SaveEmployeeSchedule(_empshftModel);

                                startdate = startdate.AddDays(intervalDays);
                            }
                        }
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

        protected void gvEmpSchedule_PreRender(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                string cutOffPeriodId = drpCutOffPeriod.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(cutOffPeriodId))
                {
                    Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

                    Result<PayrollCutOff> _Result = _IService.GetByCutOffPeriodId(_ctOffId);

                    if (_Result.IsSuccess)
                    {
                        PayrollCutOff _co = _Result.Data;

                        if (_co != null)
                        {
                            int intervalDays = 1;
                            int columnIndex = 7;

                            DateTime startdate = _co.StartDate;

                            while (startdate <= _co.EndDate.AddDays(-1))
                            {
                                columnIndex = columnIndex + 1;
                                gvEmpSchedule.Columns[columnIndex].HeaderText = startdate.ToString("dd-MMM [dddd]");
                                startdate = startdate.AddDays(intervalDays);
                            }
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

        protected void gvEmpSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {                                                                                                                               
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row.
                DropDownList ddlDay1Schedule = (e.Row.FindControl("ddlDay1Schedule") as DropDownList);
                DropDownList ddlDay2Schedule = (e.Row.FindControl("ddlDay2Schedule") as DropDownList);
                DropDownList ddlDay3Schedule = (e.Row.FindControl("ddlDay3Schedule") as DropDownList);
                DropDownList ddlDay4Schedule = (e.Row.FindControl("ddlDay4Schedule") as DropDownList);
                DropDownList ddlDay5Schedule = (e.Row.FindControl("ddlDay5Schedule") as DropDownList);
                DropDownList ddlDay6Schedule = (e.Row.FindControl("ddlDay6Schedule") as DropDownList);
                DropDownList ddlDay7Schedule = (e.Row.FindControl("ddlDay7Schedule") as DropDownList);
                DropDownList ddlDay8Schedule = (e.Row.FindControl("ddlDay8Schedule") as DropDownList);
                DropDownList ddlDay9Schedule = (e.Row.FindControl("ddlDay9Schedule") as DropDownList);
                DropDownList ddlDay10Schedule = (e.Row.FindControl("ddlDay10Schedule") as DropDownList);
                DropDownList ddlDay11Schedule = (e.Row.FindControl("ddlDay11Schedule") as DropDownList);
                DropDownList ddlDay12Schedule = (e.Row.FindControl("ddlDay12Schedule") as DropDownList);
                DropDownList ddlDay13Schedule = (e.Row.FindControl("ddlDay13Schedule") as DropDownList);
                DropDownList ddlDay14Schedule = (e.Row.FindControl("ddlDay14Schedule") as DropDownList);
                DropDownList ddlDay15Schedule = (e.Row.FindControl("ddlDay15Schedule") as DropDownList);

                //Add Default Item in the DropDownList.
                FillSchedule(ddlDay1Schedule);
                FillSchedule(ddlDay2Schedule);
                FillSchedule(ddlDay3Schedule);
                FillSchedule(ddlDay4Schedule);
                FillSchedule(ddlDay5Schedule);
                FillSchedule(ddlDay6Schedule);
                FillSchedule(ddlDay7Schedule);
                FillSchedule(ddlDay8Schedule);
                FillSchedule(ddlDay9Schedule);
                FillSchedule(ddlDay10Schedule);
                FillSchedule(ddlDay11Schedule);
                FillSchedule(ddlDay12Schedule);
                FillSchedule(ddlDay13Schedule);
                FillSchedule(ddlDay14Schedule);
                FillSchedule(ddlDay15Schedule);
            }
        }

        #region DropDownList
        protected void ddlDay1Schedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int rowIndex = gvRow.RowIndex;

                GridViewRow row = (GridViewRow)gvEmpSchedule.Rows[rowIndex];

                string _empShiftId     = row.Cells[0].ToString();
                string _workLocationId = row.Cells[0].ToString();
                string _cutOffId       = row.Cells[0].ToString();
                string _employeeId     = row.Cells[0].ToString();
                
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void ddlDay2Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay3Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay4Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay5Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay6Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay7Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay8Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay9Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay10Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay11Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay12Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay13Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay14Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDay15Schedule_SelectedIndexChanged(object sender, EventArgs e)
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

        #endregion

        protected void btnDisplaySchedule_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                Guid _wrkId = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

                Result<List<EmployeeScheduleModelDay>> _Result = _IService.GetByEmployeeScheduleByWorkLocation(_wrkId, _ctOffId);

                if (_Result.IsSuccess)
                {
                    gvEmpSchedule.DataSource = _Result.Data;
                    gvEmpSchedule.DataBind();

                    if (gvEmpSchedule.Rows.Count > 0)
                    {
                        gvEmpSchedule.UseAccessibleHeader = true;
                        gvEmpSchedule.HeaderRow.TableSection = TableRowSection.TableHeader;
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
    }
}