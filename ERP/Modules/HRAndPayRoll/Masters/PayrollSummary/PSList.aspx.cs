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

namespace ERP.Modules.HRAndPayRoll.Masters.PayrollSummary
{
    public partial class PSList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liPayrollSummary_liHR_liHRMasters";

            if (!IsPostBack)
            {
                //fill up
                FillWorkLocation();
                FillCutOffPeriod();
                FillPaymentTerms();

                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }

        #endregion

        #region Events

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                //payroll master
                Guid _wkLocId    = Guid.Parse(drpWorkLocation.SelectedValue.ToString());
                Guid _ctOffId    = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());
                string _payTerms = drpPaymentTerms.SelectedValue.ToString();

                //
                //
                //payroll summaries

                //Result<List<PayrollSummaryModel>> _Result = _IService.GeneratePayrollSummaryModels(_wkLocId, _ctOffId, _payTerms);

                Result<List<PayrollSummaryModel>> _Result = _IService.GetPayrollSummaryModels(_wkLocId, _ctOffId, _payTerms);
                if (_Result.IsSuccess)
                {
                    gvPayrollSummaries.DataSource = _Result.Data;
                    gvPayrollSummaries.DataBind();

                    if (gvPayrollSummaries.Rows.Count > 0)
                    {
                        gvPayrollSummaries.UseAccessibleHeader = true;
                        gvPayrollSummaries.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        private void FillPaymentTerms()
        {
            drpPaymentTerms.Items.Clear();

            drpPaymentTerms.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            drpPaymentTerms.Items.Insert(1, new ListItem() { Text = "DAILY RATE/DAY", Value = "DAILY RATE/DAY" });
            drpPaymentTerms.Items.Insert(2, new ListItem() { Text = "MONTHLY RATE", Value = "MONTHLY RATE" });

        }
        #endregion

        protected void gvPayrollSummaries_PreRender(object sender, EventArgs e)
        {

        }
    }
}