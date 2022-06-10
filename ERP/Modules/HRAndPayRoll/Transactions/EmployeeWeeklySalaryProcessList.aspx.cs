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
    public partial class EmployeeWeeklySalaryProcessList : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Events

        protected void btnCollect_Click(object sender, EventArgs e)
        {
            FillSalaryProcess();
        }


        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liEmployeeWeeklySalaryProcess_liHR_liHRTransactions";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }

                //FillMonth();
            }
        }

        #endregion

        #region Methods

        //private void FillMonth()
        //{
        //    IFinancialYearService _IFinancialYearService = new FinancialYearService();

        //    Result<FinancialYear> _ResultFYear = _IFinancialYearService.GetFinancialYearById(SessionHelper.SessionDetail.FinancialYearId);

        //    if (_ResultFYear.IsSuccess)
        //    {
        //        int _FinancialYear = _ResultFYear.Data.Year;
        //        int _no = 0;

        //        ddlMonth.Items.Insert(_no, new ListItem() { Text = "-- Select --", Value = "" });

        //        bool _Flag = true;
        //        for (int no = 4; no < 13; no++)
        //        {
        //            _no = _no + 1;
        //            ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

        //            if (no == DateTime.Now.Month && _FinancialYear == DateTime.Now.Year)
        //            {
        //                _Flag = false;
        //                break;
        //            }
        //        }

        //        if (_Flag)
        //        {
        //            for (int no = 1; no < 4; no++)
        //            {
        //                _no = _no + 1;
        //                ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + (_FinancialYear + 1), Value = Convert.ToString(no) + "_" + (_FinancialYear + 1) });
        //                if (no == DateTime.Now.Month && _FinancialYear + 1 == DateTime.Now.Year)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        private void FillSalaryProcess()
        {
            divSalaryProcess.Visible = false;
            var _Date = txtDate.Value;

            if (!string.IsNullOrEmpty(_Date))
            {
                DateTime _FromDate = GlobalHelper.StringToDate(_Date.Split('-')[0]);
                DateTime _ToDate = GlobalHelper.StringToDate(_Date.Split('-')[1]);

                IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();

                Result<List<EmployeePaidSalarys>> _ResultCompletedSalaryProcess = _IEmployeePaidSalaryService.GetEmployeeCompletedPaidSalaryByDate(_FromDate, _ToDate, (int)SalaryType.Weekly);

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

                Result<List<EmployeePaidSalarys>> _ResultPendingSalaryProcess = _IEmployeePaidSalaryService.GetEmployeePendingSalaryByDate(_FromDate, _ToDate, (int)SalaryType.Weekly);

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

            ScriptManager.RegisterStartupScript(this, typeof(Page), "EmployeeWeeklySalaryProcessList", "EmployeeWeeklySalaryProcessList.InitailGridDataTable();", true);
        }

        #endregion


    }
}