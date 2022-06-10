using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Reports
{
    public partial class EmployeeDetail : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liEmployeeDetail_liHR_liHRReports";

            if (!IsPostBack)
            {
                FillEmployeeType();

                txtDateRange.Value = DateTime.Now.AddMonths(-1).ToString("MM/dd/yyyy") + " - " + DateTime.Now.ToString("MM/dd/yyyy");
            }
        }

        #endregion


        #region Events

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                bool _Result = FillReport();

                if (_Result)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "InitializeControl", "EmployeeDetail.InitializeControl();", true);
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

        private void FillEmployeeType()
        {
            ILookupService _ILookupService = new LookupService();
            Result<List<Item>> _Result = _ILookupService.GetAllEmployeeType();

            if (_Result.IsSuccess)
            {
                ddlEmployeeType.DataTextField = "Text";
                ddlEmployeeType.DataValueField = "Id";
                ddlEmployeeType.DataSource = _Result.Data;
                ddlEmployeeType.DataBind();
            }

            ddlEmployeeType.Items.Insert(0, new ListItem() { Text = "All", Value = "" });
        }

        private bool FillReport()
        {
            try
            {
                DateTime _StartDate = DateTime.Now;
                DateTime _EndDate = _StartDate.AddMonths(1);

                if (!string.IsNullOrEmpty(txtDateRange.Value.Trim()))
                {
                    string[] _SplitDate = txtDateRange.Value.Trim().Split('-');

                    if (_SplitDate.Length > 1)
                    {
                        _StartDate = GlobalHelper.StringToDate(_SplitDate[0]);
                        _EndDate = GlobalHelper.StringToDate(_SplitDate[1]);
                    }
                }

                string _Status = "All";
                Boolean? _IsResign = null;
                if (rbtnPresent.Checked)
                {
                    _Status = rbtnPresent.Text;
                    _IsResign = false;
                }
                else if (rbtnResign.Checked)
                {
                    _Status = rbtnResign.Text;
                    _IsResign = true;
                }

                IEmployeeService _IEmployeeService = new EmployeeService();
                Result<List<Employee>> _Result = _IEmployeeService.EmployeeDetailReport(ddlEmployeeType.SelectedValue, _IsResign, _StartDate, _EndDate);

                if (_Result.IsSuccess)
                {
                    //Fill Report
                   
                    rvReportDetail.ProcessingMode = ProcessingMode.Local;
                    rvReportDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/HRAndPayRoll/EmployeeDetail.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSetReport", _Result.Data);
                    rvReportDetail.LocalReport.DataSources.Clear();
                    rvReportDetail.LocalReport.DataSources.Add(datasource);

                    string _DateRange = txtDateRange.Value;
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
                    ReportParameter[] _ReportParameter = new ReportParameter[8];
                    _ReportParameter[0] = new ReportParameter("DateRange", _DateRange);
                    _ReportParameter[1] = new ReportParameter("ImagePath", _ImagePath);
                    _ReportParameter[2] = new ReportParameter("Footer", _Footer);
                    _ReportParameter[3] = new ReportParameter("EmployeeType", ddlEmployeeType.SelectedItem.Text);
                    _ReportParameter[4] = new ReportParameter("Status", _Status);
                    _ReportParameter[5] = new ReportParameter("CompanyName", _CompanyName);
                    _ReportParameter[6] = new ReportParameter("Address", _Address);
                    _ReportParameter[7] = new ReportParameter("Phone", _Phone);

                    rvReportDetail.LocalReport.SetParameters(_ReportParameter);

                    rvReportDetail.LocalReport.Refresh();

                    return true;
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }

            return false;
        }

        #endregion

    }
}