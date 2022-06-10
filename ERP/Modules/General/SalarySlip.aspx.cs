using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.General
{
    public partial class SalarySlip : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liSalarySlip";

            if (!IsPostBack)
            {
                FillMonth();
            }
        }

        #endregion


        #region Events

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReport();
        }

        #endregion


        #region Methods

        private void FillMonth()
        {
            IFinancialYearService _IFinancialYearService = new FinancialYearService();

            Result<FinancialYear> _ResultFYear = _IFinancialYearService.GetFinancialYearById(SessionHelper.SessionDetail.FinancialYearId);

            if (_ResultFYear.IsSuccess)
            {
                int _FinancialYear = _ResultFYear.Data.Year;
                int _no = 0;

                ddlMonth.Items.Insert(_no, new ListItem() { Text = "-- Select --", Value = "" });

                bool _Flag = true;
                for (int no = 4; no < 13; no++)
                {
                    _no = _no + 1;
                    ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

                    if (no == DateTime.Now.Month && _FinancialYear == DateTime.Now.Year)
                    {
                        _Flag = false;
                        break;
                    }
                }

                if (_Flag)
                {
                    for (int no = 1; no < 4; no++)
                    {
                        _no = _no + 1;
                        ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + (_FinancialYear + 1), Value = Convert.ToString(no) + "_" + (_FinancialYear + 1) });
                        if (no == DateTime.Now.Month && _FinancialYear + 1 == DateTime.Now.Year)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void FillReport()
        {
            try
            {
                if (ddlMonth.SelectedIndex != 0 && ddlMonth.SelectedValue != null)
                {
                    int _Month = DateTime.Now.Month;
                    int _Year = DateTime.Now.Year;

                    string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

                    if (_SplitDate.Length > 1)
                    {
                        _Month = Convert.ToInt32(_SplitDate[0]);
                        _Year = Convert.ToInt32(_SplitDate[1]);
                    }

                    DateTime _StartDate = new DateTime(_Year, _Month, 1);
                    DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);

                    string p_Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Month);
                    Guid _EmployeeId = SessionHelper.SessionDetail.EmployeeId;

                    IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();

                    Result<EmployeePaidSalarys> _EmployeePaidSalaryResult = _IEmployeePaidSalaryService.SalarySlipReport(_EmployeeId, p_Month, _Year, _StartDate, _EndDate);
                    
                    if (_EmployeePaidSalaryResult.IsSuccess)
                    {
                        if (_EmployeePaidSalaryResult.Data!=null)
                        {
                            Result<List<Allowance>> _EmployeeAllowanceResult = _IEmployeePaidSalaryService.GetEmployeeAllowanceByEmployeePaidSalaryId(_EmployeePaidSalaryResult.Data.EmployeePaidSalaryID, _EmployeeId);

                            Result<List<Deduction>> _EmployeeDeductionResult = _IEmployeePaidSalaryService.GetEmployeeDeductionByEmployeePaidSalaryId(_EmployeePaidSalaryResult.Data.EmployeePaidSalaryID, _EmployeeId);

                            Result<List<EmployeeLoans>> _EmployeePaidLoanResult = _IEmployeePaidSalaryService.GetEmployeePaidLoanByEmployeeId(_EmployeeId, p_Month, _Year);

                            if (_EmployeeAllowanceResult.IsSuccess && _EmployeeDeductionResult.IsSuccess && _EmployeePaidLoanResult.IsSuccess)
                            {
                                if (_EmployeeDeductionResult.Data==null)
                                {
                                    _EmployeeDeductionResult.Data = new List<Deduction>();
                                }

                                Deduction _Deduction = new Deduction();
                                _Deduction.DeductionName = "Professional Tax";
                                _Deduction.PaidAmount = _EmployeePaidSalaryResult.Data.ProfessionalTax;
                                _EmployeeDeductionResult.Data.Add(_Deduction);

                                List<EmployeePaidSalarys> _ListOfEmployeePaidSalarys = new List<EmployeePaidSalarys>();
                                _ListOfEmployeePaidSalarys.Add(_EmployeePaidSalaryResult.Data);

                                rvReportDetail.ProcessingMode = ProcessingMode.Local;
                                rvReportDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/General/SalarySlip.rdlc");
                              
                                ReportDataSource dsSalarySlip = new ReportDataSource("dsSalarySlip", _ListOfEmployeePaidSalarys);
                                ReportDataSource dsPaidAllowance = new ReportDataSource("dsPaidAllowance", _EmployeeAllowanceResult.Data);
                                ReportDataSource dsPaidDeduction = new ReportDataSource("dsPaidDeduction", _EmployeeDeductionResult.Data);
                                ReportDataSource dsPaidLoan = new ReportDataSource("dsPaidLoan", _EmployeePaidLoanResult.Data);
                                rvReportDetail.LocalReport.DataSources.Clear();
                                rvReportDetail.LocalReport.DataSources.Add(dsSalarySlip);
                                rvReportDetail.LocalReport.DataSources.Add(dsPaidAllowance);
                                rvReportDetail.LocalReport.DataSources.Add(dsPaidDeduction);
                                rvReportDetail.LocalReport.DataSources.Add(dsPaidLoan);

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
                                ReportParameter[] _ReportParameter = new ReportParameter[5];
                                _ReportParameter[0] = new ReportParameter("ImagePath", _ImagePath);
                                _ReportParameter[1] = new ReportParameter("CompanyName", _CompanyName);
                                _ReportParameter[2] = new ReportParameter("Address", _Address);
                                _ReportParameter[3] = new ReportParameter("Phone", _Phone);
                                _ReportParameter[4] = new ReportParameter("Footer", _Footer);

                                rvReportDetail.LocalReport.SetParameters(_ReportParameter);
                                rvReportDetail.Visible = true;
                                rvReportDetail.LocalReport.Refresh();
                            }
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
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

    }
}