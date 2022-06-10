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
    public partial class SetEmployeeSchedule : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liSetEmployeeSchedule_liHR_liHRMasters";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(drpWorkLocation.SelectedValue.ToString()))
                {
                    Guid _wrkId = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                    FillEmployeeCheckBox(_wrkId);
                }

                //fill up
                FillWorkLocation();
                FillCutOffPeriod();

                //Weekdays and Weekends
                FillSchedule(drpSunday);
                FillSchedule(drpSaturday);

                FillSchedule(drpMonday);
                FillSchedule(drpTuesday);
                FillSchedule(drpWednesday);
                FillSchedule(drpThursday);
                FillSchedule(drpFriday);

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

        #region Methods

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

            Result<List<Item>> _Result = _ILookupService.GetSchedule();

            if (_Result.IsSuccess)
            {
                drpSchedule.DataTextField = "Text";
                drpSchedule.DataValueField = "Id";
                drpSchedule.DataSource = _Result.Data;
                drpSchedule.DataBind();
            }

            drpSchedule.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillEmployeeCheckBox(Guid _workLocationId)
        {
            chkEmpList.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetEmployeePerWorkLocationId(_workLocationId);

            if (_Result.IsSuccess)
            {
                chkEmpList.DataTextField = "Text";
                chkEmpList.DataValueField = "Id";
                chkEmpList.DataSource = _Result.Data;
                chkEmpList.DataBind();
            }

            //drpSchedule.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });

        }

        #endregion

        protected void drpWorkLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(drpWorkLocation.SelectedValue.ToString()))
            {
                Guid _wrkId = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                FillEmployeeCheckBox(_wrkId);
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (ListItem _itm in chkEmpList.Items)
            {
                _itm.Selected = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (ListItem _itm in chkEmpList.Items)
            {
                _itm.Selected = false;
            }
        }

        protected void btnSetScheduke_Click(object sender, EventArgs e)
        {
            IPayrollService _iService = new PayrollService();

            Guid _cutId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

            Result<PayrollCutOff> _resultCurCutOff = _iService.GetByCutOffPeriodId(_cutId);

            if (_resultCurCutOff != null)
            {
                foreach (ListItem _itm in chkEmpList.Items)
                {
                    if (_itm.Selected)
                    {
                        Guid empId = Guid.Parse(_itm.Value.ToString());

                        
                        Guid sunId = Guid.Parse(drpSunday.SelectedValue.ToString());
                        Guid satId = Guid.Parse(drpSaturday.SelectedValue.ToString());

                        Guid monId = Guid.Parse(drpMonday.SelectedValue.ToString());
                        Guid tueId = Guid.Parse(drpTuesday.SelectedValue.ToString());
                        Guid wedId = Guid.Parse(drpWednesday.SelectedValue.ToString());
                        Guid thuId = Guid.Parse(drpThursday.SelectedValue.ToString());
                        Guid friId = Guid.Parse(drpFriday.SelectedValue.ToString());


                    }
                }
            }
        }
    }
}