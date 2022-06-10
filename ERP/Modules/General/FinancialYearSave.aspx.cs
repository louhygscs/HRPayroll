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

namespace ERP.Modules.General
{
    public partial class FinancialYearSave : System.Web.UI.Page
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

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liFinancialYear";

            if (!IsPostBack)
            {
                FillYear();
            }
        }

        #endregion


        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FinancialYear _FinancialYear = new FinancialYear();

                _FinancialYear.Year = Convert.ToInt32(ddlYear.SelectedValue);
                _FinancialYear.FinancialYearText = lblFinancialYear.Text;

                IFinancialYearService _IFinancialYearService = new FinancialYearService();

                Result<Boolean> _Result = _IFinancialYearService.SaveFinancialYear(_FinancialYear, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Financial Year");

                    IHistoryService _IHistoryService = new HistoryService();

                    _IHistoryService.InsertHistory<FinancialYear>(_Result.Id, TableType.FinancialYearMaster, OperationType.Insert, _FinancialYear, SessionHelper.SessionDetail.UserID);

                    Response.Redirect("~/Modules/General/FinancialYearList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Financial Year") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                int _NextYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
                lblFinancialYear.Text = ddlYear.SelectedValue + " - " + _NextYear.ToString();
            }
        }

        #endregion


        #region Methods

        private void FillYear()
        {
            ddlYear.Items.Clear();

            int _Year = DateTime.Now.Year;

            ddlYear.Items.Add(new ListItem("-- Select Year --", ""));
            ddlYear.Items.Add(new ListItem((_Year - 1).ToString(), (_Year - 1).ToString()));
            ddlYear.Items.Add(new ListItem(_Year.ToString(), _Year.ToString()));
        }

        #endregion

    }
}