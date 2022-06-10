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
using System.Web.UI;

namespace ERP.Modules.BioMetricDevice.Reports
{
    public partial class EmployeeAttendance : System.Web.UI.Page
    {
        #region Variables

        private IEmployeeService _IEmployeeService = new EmployeeService();
        private IDeviceService _IDeviceService = new DeviceService();
        private IEmployeeAttendanceDeviceService _IEmployeeAttendanceDeviceService = new EmployeeAttendanceDeviceService();
        ILookupService _ILookupService = new LookupService();


        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liEmployeeAttendance_liBioMetricDevice_liReport";
            if (!IsPostBack)
            {
                FillDevice();
                FillMonthAndYear();
                FillEmployee();
                txtFromToDate.Visible = false;
                ddlMonth.Visible = false;
                ddlYear.Visible = false;
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }

        #endregion

        #region Events

        protected void rdDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFromToDate.Visible = false;
            ddlMonth.Visible = false;
            ddlYear.Visible = false;
            txtDate.Visible = false;
            if (rbtnWeekly.Checked == true)
            {
                txtFromToDate.Visible = true;
            }
            else if (rbtnMonthly.Checked == true)
            {
                ddlMonth.Visible = true;
                ddlYear.Visible = true;
            }
            else
            {
                txtDate.Visible = true;
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _FromDate = DateTime.Now;
                DateTime _ToDate;
                var _FromToDate = txtFromToDate.Value;
                var _Date = txtDate.Value;
                if (!string.IsNullOrEmpty(_FromToDate))
                {
                    _FromDate = GlobalHelper.StringToDate(_FromToDate.Split('-')[0]);
                    _ToDate = GlobalHelper.StringToDate(_FromToDate.Split('-')[1]);
                }

                if (rbtnMonthly.Checked == true)
                {
                    _FromDate = Convert.ToDateTime(ddlYear.SelectedItem.ToString() + "/" + ddlMonth.SelectedValue + "/01");
                    _ToDate = _FromDate.AddMonths(1).AddDays(-1);
                }
                else
                {
                    _ToDate = GlobalHelper.StringToDate(_Date.Split('-')[0]);
                    _FromDate = _ToDate;
                }
                Result<List<EmployeeModel>> _Result = _IEmployeeService.GetEmployeeAttendanceReportByEmpoyeeIdAndDate(new Guid(Convert.ToString(ddlEmployee.SelectedValue)), _FromDate, _ToDate, new Guid(Convert.ToString(ddlDevice.SelectedValue)));
                if (_Result.IsSuccess)
                {
                    if (_Result.Data.Count > 0)
                    {
                        foreach (var item in _Result.Data)
                        {
                            item.FullName = item.FirstName + " " + item.MiddleName + " " + item.LastName;
                        }
                        rvReportDetail.ProcessingMode = ProcessingMode.Local;
                        rvReportDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/BioMetricDevice/EmployeeAttendanceReport.rdlc");
                        ReportDataSource dsEmployeeAttendance = new ReportDataSource("dsEmployeeAttendance", _Result.Data);
                        rvReportDetail.LocalReport.DataSources.Clear();
                        rvReportDetail.LocalReport.DataSources.Add(dsEmployeeAttendance);

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
                        string _DateRange = _FromDate.ToString("dd") + "/" + _FromDate.ToString("MM") + "/" + _FromDate.Year + " - " + _ToDate.ToString("dd") + "/" + _ToDate.ToString("MM") + "/" + _ToDate.Year;

                        ReportParameter[] _ReportParameter = new ReportParameter[5];
                        _ReportParameter[0] = new ReportParameter("ImagePath", _ImagePath);
                        _ReportParameter[1] = new ReportParameter("CompanyName", _CompanyName);
                        _ReportParameter[2] = new ReportParameter("Address", _Address);
                        _ReportParameter[3] = new ReportParameter("Phone", _Phone);
                        _ReportParameter[4] = new ReportParameter("Footer", _Footer);
                        rvReportDetail.LocalReport.SetParameters(_ReportParameter);

                        rvReportDetail.LocalReport.Refresh();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "InitializeControl", "EmployeeAttendanceReport.InitializeControl();", true);
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
        }

        #endregion

        #region Private Methods

        private void FillDevice()
        {
            Result<List<DeviceModel>> _Result = _IDeviceService.GetDeviceList();

            if (_Result.IsSuccess)
            {
                ddlDevice.DataTextField = "DeviceName";
                ddlDevice.DataValueField = "DeviceID";
                ddlDevice.DataSource = _Result.Data;
                ddlDevice.DataBind();
            }
        }

        private void FillEmployee()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllActiveEmployee();

            if (_Result.IsSuccess)
            {
                ddlEmployee.DataTextField = "Text";
                ddlEmployee.DataValueField = "Id";
                ddlEmployee.DataSource = _Result.Data;
                ddlEmployee.DataBind();
            }
        }

        private void FillMonthAndYear()
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.Add("name");
            _DataTable.Columns.Add("number");

            DataTable _DataTableYear = new DataTable();
            _DataTableYear.Columns.Add("year");

            for (int no = 1; no < 13; no++)
            {
                DataRow _DataRow = _DataTable.NewRow();
                _DataRow["name"] = new DateTime(2012, no, 01).ToString("MMMM");
                _DataRow["number"] = no;
                _DataTable.Rows.Add(_DataRow);
            }

            ddlMonth.DataSource = _DataTable;
            ddlMonth.DataTextField = "name";
            ddlMonth.DataValueField = "number";
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlMonth.DataBind();


            for (int no = 0; no < 5; no++)
            {
                int _Year = DateTime.Now.Year;
                _Year = _Year - no;
                DataRow _DataRowYear = _DataTableYear.NewRow();
                _DataRowYear["year"] = _Year.ToString();
                _DataTableYear.Rows.Add(_DataRowYear);
            }

            ddlYear.DataSource = _DataTableYear;
            ddlYear.DataValueField = "year";
            ddlYear.DataTextField = "year";
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlYear.DataBind();

        }

        #endregion
    }
}