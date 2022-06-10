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

namespace ERP.Modules.HRAndPayRoll.Masters.DailyTimeRecord
{
    public partial class DTRList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liCutOff_liHR_liEmployeeProfile";

            if (!IsPostBack)
            {
                //fill up
                FillWorkLocation();
                FillCutOffPeriod();

            }

        }
        #endregion

        #region Events
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Result<List<DTRCutOffTimeLogModel>> _Result = null;

                IPayrollService _iService = new PayrollService();

                Guid _wkLocId = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

                _Result = _iService.GetDTRCutOffTimeLogModel(_wkLocId, _ctOffId);

                if (_Result.IsSuccess)
                {
                    gvDTRDailyTimelogs.DataSource = _Result.Data;
                    gvDTRDailyTimelogs.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Daily Time Record") + "');});", true);
                }
            }
            catch (Exception _ex)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _ex);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnGeneratePayrollDTR_Click(object sender, EventArgs e)
        {
            try
            {
                Result<bool> _Result = null;

                IPayrollService _iService = new PayrollService();

                Guid _wkLocId = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());

                _Result = _iService.GenerateDailyTimeRecord(_wkLocId, _ctOffId);

                if (_Result.IsSuccess)
                {
                    //gvDTRDaily.DataSource = _Result.Data;
                    //gvDTRDaily.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Daily Time Record") + "');});", true);
                }
            }
            catch (Exception _ex)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _ex);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

        #region Methods

        private void FillWorkLocation()
        {
            drpWorkLocation.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetWorkLocation(Guid.Parse("F699B807-07E7-45FD-BAC6-0F3BFF1E34BA"));

            if (_Result.IsSuccess)
            {
                drpWorkLocation.DataTextField  = "Text";
                drpWorkLocation.DataValueField = "Id";
                drpWorkLocation.DataSource     = _Result.Data;
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
                drpCutOffPeriod.DataTextField  = "Text";
                drpCutOffPeriod.DataValueField = "Id";
                drpCutOffPeriod.DataSource     = _Result.Data;
                drpCutOffPeriod.DataBind();
            }

            drpCutOffPeriod.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        #endregion

    }
}