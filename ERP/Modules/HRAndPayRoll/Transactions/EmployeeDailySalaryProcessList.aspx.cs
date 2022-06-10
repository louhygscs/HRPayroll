using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Transactions
{
    public partial class EmployeeDailySalaryProcessList : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liEmployeeDailySalaryProcess_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }

        #endregion


        #region Events

        protected void btnCollect_Click(object sender, EventArgs e)
        {
            FillSalaryProcess();
        }

        #endregion


        #region Methods

        private void FillSalaryProcess()
        {
            divSalaryProcess.Visible = false;
            var _Date = txtDate.Value;

            if (!string.IsNullOrEmpty(_Date))
            {
                DateTime _FromDate = GlobalHelper.StringToDate(_Date.Split('-')[0]);
                DateTime _ToDate = GlobalHelper.StringToDate(_Date.Split('-')[1]);

                IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();

                Result<List<EmployeePaidSalarys>> _ResultCompletedSalaryProcess = _IEmployeePaidSalaryService.GetEmployeeCompletedPaidSalaryByDate(_FromDate, _ToDate, (int)SalaryType.Daily);

                if (_ResultCompletedSalaryProcess.IsSuccess)
                {
                    gvEmployeeCompletedSalaryProcess.DataSource = _ResultCompletedSalaryProcess.Data;
                    gvEmployeeCompletedSalaryProcess.DataBind();

                    if (gvEmployeeCompletedSalaryProcess.Rows.Count > 0)
                    {
                        gvEmployeeCompletedSalaryProcess.UseAccessibleHeader = true;
                        gvEmployeeCompletedSalaryProcess.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }

                Result<List<EmployeePaidSalarys>> _ResultPendingSalaryProcess = _IEmployeePaidSalaryService.GetEmployeePendingSalaryByDate(_FromDate, _ToDate, (int)SalaryType.Daily);

                if (_ResultPendingSalaryProcess.IsSuccess)
                {
                    gvEmployeePendingSalaryProcess.DataSource = _ResultPendingSalaryProcess.Data;
                    gvEmployeePendingSalaryProcess.DataBind();

                    if (gvEmployeePendingSalaryProcess.Rows.Count > 0)
                    {
                        gvEmployeePendingSalaryProcess.UseAccessibleHeader = true;
                        gvEmployeePendingSalaryProcess.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }

                divSalaryProcess.Visible = true;
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), "EmployeeDailySalaryProcessList", "EmployeeDailySalaryProcessList.InitailGridDataTable();", true);
        }

        #endregion
    }
}