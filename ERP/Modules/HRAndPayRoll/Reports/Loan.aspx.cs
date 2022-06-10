using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Reports
{
    public partial class Loan : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liLoan_liHR_liHRReports";

            if (!IsPostBack)
            {
                FillDepartment();
                FillEmployee();
            }
        }

        #endregion

        #region Events

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployee();
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                bool _Result = FillReport();

                if (_Result)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "InitializeControl", "ReportLoan.InitializeControl();", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

        #region Methods

        private void FillDepartment()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllDepartment();

            if (_Result.IsSuccess)
            {
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "Text";
                ddlDepartment.DataSource = _Result.Data;
                ddlDepartment.DataBind();
            }

            ddlDepartment.Items.Insert(0, new ListItem() { Text = "All", Value = Guid.Empty.ToString() });
        }

        private void FillEmployee()
        {
            lbEmployees.Items.Clear();

            string _DepartmentId = ddlDepartment.SelectedValue;

            Result<List<Item>> _Result = _ILookupService.GetAllActiveEmployeeByDepartmentId(new Guid(_DepartmentId));

            if (_Result.IsSuccess)
            {
                lbEmployees.DataTextField = "Text";
                lbEmployees.DataValueField = "Id";
                lbEmployees.DataSource = _Result.Data;
                lbEmployees.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), "InitializeControl", "ReportLoan.InitializeControl();", true);
        }

        private bool FillReport()
        {
            try
            {
                List<Guid> _ListOfSelectedEmployee = new List<Guid>();

                foreach (ListItem _ListItem in lbEmployees.Items)
                {
                    if (_ListItem.Selected)
                    {
                        _ListOfSelectedEmployee.Add(new Guid(_ListItem.Value));
                    }
                }

                string _Status = "All";
                Boolean? _IsCompleted = null;
                if (rbtnCompleted.Checked)
                {
                    _Status = rbtnCompleted.Text;
                    _IsCompleted = true;
                }
                else if (rbtnPending.Checked)
                {
                    _Status = rbtnPending.Text;
                    _IsCompleted = false;
                }

                IEmployeeLoanService _IEmployeeLoanService = new EmployeeLoanService();

                Result<List<EmployeeLoans>> _EmployeeLoanResult = _IEmployeeLoanService.LoanReport(_ListOfSelectedEmployee, _IsCompleted);

                if (_EmployeeLoanResult.IsSuccess)
                {
                    if (_EmployeeLoanResult.Data.Count > 0)
                    {
                        rvReportDetail.ProcessingMode = ProcessingMode.Local;
                        rvReportDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/HRAndPayRoll/Loan.rdlc");
                        ReportDataSource dsLoan = new ReportDataSource("dsLoan", _EmployeeLoanResult.Data);
                        rvReportDetail.LocalReport.DataSources.Clear();
                        rvReportDetail.LocalReport.DataSources.Add(dsLoan);

                        string _ImagePath = new Uri(Server.MapPath("~/Images/Logo.png")).AbsoluteUri;
                        string _CompanyName = "Arity Infoway";
                        string _Address = "";
                        string _Phone = "";
                        string _Footer = "Copyright © " + DateTime.Now.Year + " Arity Infoway";

                        ICompanyService _ICompanyService = new CompanyService();
                        Result<Company> _ResultCompany = _ICompanyService.GetCompany();

                        if (_ResultCompany.IsSuccess)
                        {
                            string _FilePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.CompanyLogo + "/" + _ResultCompany.Data.CompanyLogo;

                            if (File.Exists(Server.MapPath(_FilePath)))
                            {
                                _ImagePath = new Uri(Server.MapPath(_FilePath)).AbsoluteUri;
                            }

                            _CompanyName = _ResultCompany.Data.CompanyName;
                            _Address = _ResultCompany.Data.Address + "  " + _ResultCompany.Data.City;
                            _Phone = _ResultCompany.Data.PhoneNo;
                            _Footer = "Copyright © " + DateTime.Now.Year + " " + _ResultCompany.Data.CompanyName;
                        }

                        rvReportDetail.LocalReport.EnableExternalImages = true;
                        ReportParameter[] _ReportParameter = new ReportParameter[6];
                        _ReportParameter[0] = new ReportParameter("ImagePath", _ImagePath);
                        _ReportParameter[1] = new ReportParameter("CompanyName", _CompanyName);
                        _ReportParameter[2] = new ReportParameter("Address", _Address);
                        _ReportParameter[3] = new ReportParameter("Phone", _Phone);
                        _ReportParameter[4] = new ReportParameter("Footer", _Footer);
                        _ReportParameter[5] = new ReportParameter("Status", _Status);

                        rvReportDetail.LocalReport.SetParameters(_ReportParameter);

                        rvReportDetail.LocalReport.Refresh();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, 'No Records Found');});", true);
                        rvReportDetail.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
                    rvReportDetail.Visible = false;
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
            return true;
        }

        #endregion
    }
}