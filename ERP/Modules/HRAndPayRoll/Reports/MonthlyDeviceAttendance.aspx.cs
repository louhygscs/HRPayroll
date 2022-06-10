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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Reports
{
    public partial class MonthlyDeviceAttendance : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        IEmployeeAttendanceService _IEmployeeAttendanceService = new EmployeeAttendanceService();

        #endregion


        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liMonthlyDeviceAttendance_liHR_liHRReports";

            if (!IsPostBack)
            {
                FillDepartment();
                FillEmployee();
                FillMonth();
            }
        }

        #endregion


        #region Events

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployee();
            FillMonth();
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                bool _Result = FillReport();

                if (_Result)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "InitializeControl", "ReportMonthlyDeviceAttendance.InitializeControl();", true);
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

        private void FillMonth()
        {
            lbMonth.Items.Clear();

            IFinancialYearService _IFinancialYearService = new FinancialYearService();

            Result<FinancialYear> _ResultFYear = _IFinancialYearService.GetFinancialYearById(SessionHelper.SessionDetail.FinancialYearId);

            if (_ResultFYear.IsSuccess)
            {
                int _FinancialYear = _ResultFYear.Data.Year;

                bool _Flag = true;
                for (int no = 4; no < 13; no++)
                {
                    lbMonth.Items.Add(new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

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
                        lbMonth.Items.Add(new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + (_FinancialYear + 1), Value = Convert.ToString(no) + "_" + (_FinancialYear + 1) });
                        if (no == DateTime.Now.Month && _FinancialYear + 1 == DateTime.Now.Year)
                        {
                            break;
                        }
                    }
                }
            }
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

            ScriptManager.RegisterStartupScript(this, typeof(Page), "InitializeControl", "ReportMonthlyDeviceAttendance.InitializeControl();", true);
        }

        private bool FillReport()
        {
            try
            {
                List<DeviceEmployeeTotalHours> _ListofDeviceEmployeeTotalHours = new List<DeviceEmployeeTotalHours>();
                DeviceEmployeeTotalHours _DeviceEmployeeTotalHours = new DeviceEmployeeTotalHours();

                List<Guid> _ListOfSelectedEmployee = new List<Guid>();

                foreach (ListItem _ListItem in lbEmployees.Items)
                {
                    if (_ListItem.Selected)
                    {
                        _ListOfSelectedEmployee.Add(new Guid(_ListItem.Value));
                    }
                }

                List<int> _ListOfSelectedMonth = new List<int>();
                int _Month;

                foreach (ListItem _ListItem in lbMonth.Items)
                {
                    if (_ListItem.Selected)
                    {
                        string[] _SplitDate = _ListItem.Value.Split('_');

                        if (_SplitDate.Length > 1)
                        {
                            _Month = Convert.ToInt32(_SplitDate[0]);
                            _ListOfSelectedMonth.Add(_Month);
                        }
                    }
                }
                IFinancialYearService _IFinancialYearService = new FinancialYearService();

                Result<FinancialYear> _ResultFYear = _IFinancialYearService.GetFinancialYearById(SessionHelper.SessionDetail.FinancialYearId);
                List<ListItem> _YearList = new List<ListItem>();
                if (_ResultFYear.IsSuccess)
                {
                    int _FinancialYear = _ResultFYear.Data.Year;

                    bool _Flag = true;
                    for (int no = 4; no < 13; no++)
                    {
                        _YearList.Add(new ListItem() { Text = no.ToString(), Value = _FinancialYear.ToString() });

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
                            _YearList.Add(new ListItem() { Text = no.ToString(), Value = (_FinancialYear + 1).ToString() });
                            if (no == DateTime.Now.Month && _FinancialYear + 1 == DateTime.Now.Year)
                            {
                                break;
                            }
                        }
                    }
                }

                IEmployeeAttendanceDeviceService _IEmployeeAttendanceDeviceService = new EmployeeAttendanceDeviceService();
                Result<List<EmployeeAttendanceDevices>> _EmployeeAttendanceResult = _IEmployeeAttendanceDeviceService.DeviceAttendanceReport(_ListOfSelectedEmployee, _ListOfSelectedMonth, SessionHelper.SessionDetail.FinancialYearId);

                if (_EmployeeAttendanceResult.IsSuccess)
                {
                    if (_EmployeeAttendanceResult.Data.Count > 0)
                    {
                        foreach (var item in _ListOfSelectedEmployee)
                        {
                            var _EmployeeAttendance = _EmployeeAttendanceResult.Data.Where(x => x.EmployeeId == item).ToList();
                            if (_EmployeeAttendance.Count > 0)
                            {
                                var _MonthList = _EmployeeAttendance.Select(s => s.Month).Distinct().ToList();
                                foreach (var _MonthNo in _MonthList)
                                {
                                    var _Year = string.Empty;
                                    decimal _WorkingHours = 0;
                                    var _MonthwiseAttendance = _EmployeeAttendance.Where(a => a.Month == _MonthNo).OrderBy(x => x.AttendanceDateTime).ToList();
                                    if (_MonthwiseAttendance.Count > 0)
                                    {
                                        _Year = _YearList.Where(x => x.Text == _MonthNo.ToString()).Select(s => s.Value).FirstOrDefault();
                                        DateTime _StartDate = new DateTime(Convert.ToInt32(_Year), _MonthNo, 01);
                                        DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);
                                        double _WorkingMinutes = 0;
                                        Result<List<EmployeeAttendances>> _ResultManual = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(item, _StartDate, _EndDate);

                                        for (DateTime date = _StartDate; date <= _EndDate; date = date.AddDays(1.0))
                                        {
                                            var _WorkingHoursCount = _ResultManual.Data.Where(a => a.AttendanceDate == date).Select(s => s.WorkingHours ?? 0).FirstOrDefault();
                                            if (_WorkingHoursCount == 0)
                                            {
                                                var _ListofPunchtime = _MonthwiseAttendance.Where(r => r.AttendanceDate == date).OrderBy(r => r.AttendanceDateTime).Select(r => r.PunchTime).ToList();
                                                if (_ListofPunchtime.Count % 2 == 0)
                                                {
                                                    for (int i = 0; i < _ListofPunchtime.Count; i += 2)
                                                    {
                                                        if (_WorkingMinutes == 0)
                                                        {
                                                            _WorkingMinutes = (DateTime.Parse(_ListofPunchtime[i + 1]).Subtract(DateTime.Parse(_ListofPunchtime[i]))).TotalMinutes;
                                                        }
                                                        else
                                                        {
                                                            _WorkingMinutes = _WorkingMinutes + (DateTime.Parse(_ListofPunchtime[i + 1]).Subtract(DateTime.Parse(_ListofPunchtime[i]))).TotalMinutes;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _WorkingHours = _WorkingHours + _WorkingHoursCount;
                                            }
                                        }
                                        TimeSpan _Time = TimeSpan.FromMinutes(_WorkingMinutes);
                                        if (_Time != null)
                                        {
                                            _WorkingHours = _WorkingHours + Convert.ToDecimal(_Time.TotalHours.ToString("0.00"));
                                        }
                                    }
                                    _DeviceEmployeeTotalHours = new DeviceEmployeeTotalHours();
                                    _DeviceEmployeeTotalHours.Department = _MonthwiseAttendance.Where(x => x.Month == _MonthNo).Select(s => s.Department).FirstOrDefault();
                                    _DeviceEmployeeTotalHours.FullName = _MonthwiseAttendance.Where(x => x.Month == _MonthNo).Select(s => s.EmployeeName).FirstOrDefault();
                                    _DeviceEmployeeTotalHours.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_MonthNo) + " " + _Year;
                                    _DeviceEmployeeTotalHours.WorkingHours = _WorkingHours.ToString("0.00");
                                    _ListofDeviceEmployeeTotalHours.Add(_DeviceEmployeeTotalHours);
                                }
                            }
                        }
                        rvReportDetail.ProcessingMode = ProcessingMode.Local;
                        rvReportDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/HRAndPayRoll/DeviceTotalAttendance.rdlc");
                        ReportDataSource dsAttendance = new ReportDataSource("dsTotalAttendance", _ListofDeviceEmployeeTotalHours);
                        rvReportDetail.LocalReport.DataSources.Clear();
                        rvReportDetail.LocalReport.DataSources.Add(dsAttendance);

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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
            return true;
        }

        #endregion
    }
}