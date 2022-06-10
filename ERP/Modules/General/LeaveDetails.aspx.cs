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
    public partial class LeaveDetails : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liLeaveDetails";

            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        #endregion

        #region Methods

        private void FillGrid()
        {
            try
            {
                IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();

                Result<List<EmployeePaidSalarys>> _Result = _IEmployeePaidSalaryService.GetLeaveDetailsByEmployeeId(SessionHelper.SessionDetail.EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

                if (_Result.IsSuccess)
                {
                    gvLeaveDetails.DataSource = _Result.Data;
                    gvLeaveDetails.DataBind();

                    if (gvLeaveDetails.Rows.Count > 0)
                    {
                        gvLeaveDetails.UseAccessibleHeader = true;
                        gvLeaveDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
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
    }
}