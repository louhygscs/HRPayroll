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
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;
using ERP.Common;
using System.IO;

namespace ERP.Modules
{
    public partial class Main1 : System.Web.UI.Page
    {
        #region Variable

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IHolidayService _IHolidayService = new HolidayService();
        IEmployeePaidSalaryService _IEmployeePaidSalaryService = new EmployeePaidSalaryService();
        IEmployeeLeaveCategoryService _IEmployeeLeaveCategoryService = new EmployeeLeaveCategoryService();
        IEmployeeService _IEmployeeService = new EmployeeService();
        IEmployeeAttendanceDeviceService _IEmployeeAttendanceDeviceService = new EmployeeAttendanceDeviceService();
        IDepartmentService _IDepartmentService = new DepartmentService();
        IInterviewService _IInterviewService = new InterviewService();
        IDeviceService _IDeviceService = new DeviceService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }
            SessionHelper.SelectMenuSession = "liDashboard";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }

                if (SessionHelper.SessionDetail.RoleId != null)
                {
                    if (SessionHelper.SessionDetail.RoleId == new Guid(GlobalHelper.GetEnumDescription(Role.Employee)))
                    {
                        divEmployee.Visible = true;
                        FillDashBoardDetails();
                    }
                    else
                    {
                        divEmployee.Visible = false;
                        divAdmin.Visible = true;
                        FillDashBoardInfo();
                    }
                    // lblEmployeeName.Text = SessionHelper.SessionDetail.FullName;
                }
                else
                {
                    Response.Redirect("~/Modules/Login.aspx", true);
                }


            }
        }

        #endregion

        #region Events

        protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            CalendarDay day = (CalendarDay)e.Day;

            if (ViewState["WorkingDaysResult"] == null)
            {
                List<string> _WorkingDaysResult = _IEmployeeService.GetEmployeeById(SessionHelper.SessionDetail.EmployeeId).Data.WorkingDays.ToList();

                if (_WorkingDaysResult != null)
                {
                    ViewState["WorkingDaysResult"] = _WorkingDaysResult;
                }
            }
            List<string> _ListOfWeeklyOff = (List<string>)ViewState["WorkingDaysResult"];
            bool _WeeklyOff = _ListOfWeeklyOff.Contains(e.Day.Date.DayOfWeek.ToString());
            if (!_WeeklyOff)
            {
                e.Cell.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                if (ViewState["HolidayResult"] == null)
                {
                    Result<List<Holiday>> _HolidayResult = _IHolidayService.GetHolidayList();

                    if (_HolidayResult.IsSuccess)
                    {
                        ViewState["HolidayResult"] = _HolidayResult.Data;
                    }
                }

                List<Holiday> _ListOfHoliDay = (List<Holiday>)ViewState["HolidayResult"];
                int _Holiday = _ListOfHoliDay.Where(d => d.StartDate <= day.Date && d.EndDate >= day.Date).Count();

                if (_Holiday > 0)
                {
                    e.Cell.ForeColor = System.Drawing.Color.Cyan;
                }
                else
                {
                    if (ViewState["LeaveResult"] == null)
                    {
                        Result<List<EmployeeLeaveCategorys>> _LeaveResult = _IEmployeeLeaveCategoryService.GetEmployeeLeaveCategoryListByEmployeeId(SessionHelper.SessionDetail.EmployeeId);

                        if (_LeaveResult.IsSuccess && _LeaveResult.Data != null)
                        {
                            ViewState["LeaveResult"] = _LeaveResult.Data;
                        }
                    }
                    List<EmployeeLeaveCategorys> _ListOfLeave = (List<EmployeeLeaveCategorys>)ViewState["LeaveResult"];
                    int _Leave = _ListOfLeave.Where(l => l.StartDate <= day.Date && l.EndDate >= day.Date).Count();

                    if (_Leave > 0)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void btnClockIn_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeAttendanceDeviceModel _EmployeeAttendanceDeviceModel = new EmployeeAttendanceDeviceModel();
                _EmployeeAttendanceDeviceModel.AttendanceDate = DateTime.Now.Date;
                _EmployeeAttendanceDeviceModel.AttendanceDateTime = DateTime.Now;
                _EmployeeAttendanceDeviceModel.EmployeeId = SessionHelper.SessionDetail.EmployeeId;
                _EmployeeAttendanceDeviceModel.EnrollNo = Convert.ToString(SessionHelper.SessionDetail.EmployeeNo);
                _EmployeeAttendanceDeviceModel.PunchMethod = Convert.ToString(PunchMethod.IN);
                _EmployeeAttendanceDeviceModel.PunchTime = DateTime.Now.ToString("HH:mm:ss");
                _EmployeeAttendanceDeviceModel.PunchType = Convert.ToString(PunchType.WEB);
                Result<bool> _Result = _IEmployeeAttendanceDeviceService.SaveEmployeeAttendance(_EmployeeAttendanceDeviceModel);
                if (_Result.IsSuccess)
                {
                    if (_Result.TotalCount >= 8)
                    {
                        btnClockIn.Enabled = false;
                        btnClockOut.Enabled = false;
                    }
                    else
                    {
                        btnClockIn.Enabled = false;
                        btnClockOut.Enabled = true;
                    }
                }


            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnClockOut_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeAttendanceDeviceModel _EmployeeAttendanceDeviceModel = new EmployeeAttendanceDeviceModel();
                _EmployeeAttendanceDeviceModel.AttendanceDate = DateTime.Now.Date;
                _EmployeeAttendanceDeviceModel.AttendanceDateTime = DateTime.Now;
                _EmployeeAttendanceDeviceModel.EmployeeId = SessionHelper.SessionDetail.EmployeeId;
                _EmployeeAttendanceDeviceModel.EnrollNo = Convert.ToString(SessionHelper.SessionDetail.EmployeeNo);
                _EmployeeAttendanceDeviceModel.PunchMethod = Convert.ToString(PunchMethod.OUT);
                _EmployeeAttendanceDeviceModel.PunchTime = DateTime.Now.ToString("HH:mm:ss");
                _EmployeeAttendanceDeviceModel.PunchType = Convert.ToString(PunchType.WEB);
                Result<bool> _Result = _IEmployeeAttendanceDeviceService.SaveEmployeeAttendance(_EmployeeAttendanceDeviceModel);
                if (_Result.IsSuccess)
                {
                    if (_Result.TotalCount >= 8)
                    {
                        btnClockIn.Enabled = false;
                        btnClockOut.Enabled = false;
                    }
                    else
                    {
                        btnClockIn.Enabled = true;
                        btnClockOut.Enabled = false;
                    }
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

        private void FillDashBoardDetails()
        {
            Result<List<EmployeePaidSalarys>> _Result = _IEmployeePaidSalaryService.GetLeaveOpeningDetailsByFinancialYearId(SessionHelper.SessionDetail.FinancialYearId, SessionHelper.SessionDetail.EmployeeId);

            if (_Result.IsSuccess && _Result.Data != null)
            {
                lblTotalLeave.Text = _Result.Data.FirstOrDefault().TotalAllowLeave.ToString();
                lblUsedLeave.Text = _Result.Data.FirstOrDefault().TotalUseLeave.ToString();
                lblRemainingLeave.Text = _Result.Data.FirstOrDefault().RemainingLeave.ToString();
            }
            lblusername.Text = SessionHelper.SessionDetail.FullName;
            spnDesignation.InnerText = SessionHelper.SessionDetail.EmployeeDesignation;
            employeeId.InnerHtml = SessionHelper.SessionDetail.EmployeeNo.ToString();
            employeeshift.InnerText = SessionHelper.SessionDetail.EmployeeShift;
            string _Path = System.Configuration.ConfigurationManager.AppSettings["ImagePath"] + UploadFileFolderName.EmployeePhoto + "/";
            string _FilePath = !string.IsNullOrEmpty(SessionHelper.SessionDetail.PhotoName) && File.Exists(Server.MapPath(Path.Combine(_Path, SessionHelper.SessionDetail.PhotoName))) ? Path.Combine(_Path, SessionHelper.SessionDetail.PhotoName) : "~/Images/DefaultUser.png";
            imgUserProfile.Src = _FilePath;

            Result<List<Holiday>> _HolidayResult = _IHolidayService.GetHolidayList();

            if (_HolidayResult.IsSuccess)
            {
                gvHoliday.DataSource = _HolidayResult.Data.Where(a => a.StartDate >= DateTime.Now).OrderBy(a => a.StartDate).Take(10);
                gvHoliday.DataBind();
            }

            // BirthDay

            Result<List<EmployeeBirthDayModel>> _BirthDayResult = _IEmployeeService.GetUpComingBirthDate();
            if (_BirthDayResult.IsSuccess)
            {
                var day = DateTime.Now.Day;
                var month = DateTime.Now.Month;

                List<EmployeeBirthDayModel> _ListofBirthday = _BirthDayResult.Data.Where(a => a.BirthDate.Day >= day && a.BirthDate.Month >= month).OrderBy(a => a.BirthDate.Month).ToList();
                List<EmployeeBirthDayModel> _ListofBirthday1 = _BirthDayResult.Data.Where(a => a.BirthDate.Day < day && a.BirthDate.Month > month).OrderBy(a => a.BirthDate.Month).ToList();

                gvBirthday.DataSource = (_ListofBirthday.Union(_ListofBirthday1)).Take(5);
                gvBirthday.DataBind();
            }

            Result<string> _AttendanceResult = _IEmployeeAttendanceDeviceService.GetEmployeeAttendancePunchTypeandCount(SessionHelper.SessionDetail.EmployeeId);
            if (_AttendanceResult.IsSuccess)
            {
                if (_AttendanceResult.TotalCount >= 8)
                {
                    btnClockIn.Enabled = false;
                    btnClockOut.Enabled = false;
                }
                else
                {
                    if (_AttendanceResult.Data == Convert.ToString(PunchMethod.IN))
                    {
                        btnClockOut.Enabled = true;
                        btnClockIn.Enabled = false;
                    }
                    else
                    {
                        btnClockIn.Enabled = true;
                        btnClockOut.Enabled = false;
                    }
                }
            }

            ILookupService _ILookupService = new LookupService();

            Result<List<Dashboard>> _PaidSalaryResult = _ILookupService.GetSalaryChartDetailsByEmployeeId(SessionHelper.SessionDetail.EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

            if (_PaidSalaryResult.IsSuccess)
            {
                if (_PaidSalaryResult.Data != null)
                {
                    ChartSalary.DataSource = _PaidSalaryResult.Data;
                    ChartSalary.Series["SeriesTotal"].XValueMember = "Month";
                    ChartSalary.Series["SeriesTotal"].YValueMembers = "PaidTotalSalary";
                    ChartSalary.DataBind();
                }
            }

            // Attendance Chart

            Result<List<EmployeePaidSalarys>> _ResultAttendance = _IEmployeePaidSalaryService.GetAttendanceChartByEmployeeID(SessionHelper.SessionDetail.EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

            if (_ResultAttendance.IsSuccess)
            {
                if (_ResultAttendance.Data != null)
                {
                    _ResultAttendance.Data.ForEach(e => e.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(e.MonthNo));
                    ChartAttendance.DataSource = _ResultAttendance.Data;

                    ChartAttendance.Series["SeriesTotalDays"].XValueMember = "Month";

                    ChartAttendance.Series["SeriesTotalDays"].YValueMembers = "TotalDays";
                    ChartAttendance.Series["SeriesPresentDays"].YValueMembers = "TotalPresentDays";
                    ChartAttendance.Series["SeriesWeekluOff"].YValueMembers = "WeeklyOff";
                    ChartAttendance.Series["SeriesHolidays"].YValueMembers = "TotalHolidays";
                    ChartAttendance.Series["SeriesTotalUseLeave"].YValueMembers = "TotalUseLeave";

                    ChartAttendance.DataBind();
                }
            }
        }

        private void FillDashBoardInfo()
        {

            //Result<List<Employee>> _ResultAll = _IEmployeeService.GetAllEmployeeList();
            //Result<List<Employee>> _Result = _IEmployeeService.GetAllIsActiveEmployeeList();

            //lblTotalEmployee.Text = Convert.ToString(_ResultAll.Data.Count());
            //lblActiveEmployee.Text = Convert.ToString(_Result.Data.Where(x => x.IsLeave == false).ToList().Count());

            //int _Month = DateTime.Now.Month;
            //int _Year = DateTime.Now.Year;

            Result<List<Holiday>> _HolidayResult = _IHolidayService.GetHolidayList();

            if (_HolidayResult.IsSuccess)
            {
                gvUpcomingHoliday.DataSource = _HolidayResult.Data.Where(a => a.StartDate >= DateTime.Now).OrderBy(a => a.StartDate).Take(10);
                gvUpcomingHoliday.DataBind();
            }

            // BirthDay

            Result<List<EmployeeBirthDayModel>> _BirthDayResult = _IEmployeeService.GetUpComingBirthDate();
            if (_BirthDayResult.IsSuccess)
            {
                var day = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                List<EmployeeBirthDayModel> _ListofBirthday = _BirthDayResult.Data.Where(a => a.BirthDate.Day >= day && a.BirthDate.Month >= month).OrderBy(a => a.BirthDate.Month).ToList();
                List<EmployeeBirthDayModel> _ListofBirthday1 = _BirthDayResult.Data.Where(a => a.BirthDate.Day < day && a.BirthDate.Month > month).OrderBy(a => a.BirthDate.Month).ToList();

                lblBirthdayCount.Text = _BirthDayResult.Data.Where(a => a.BirthDate.Day == day && a.BirthDate.Month == month).OrderBy(a => a.BirthDate.Month).ToList().Count().ToString();
                gvUpcomingBirthday.DataSource = (_ListofBirthday.Union(_ListofBirthday1)).Take(5);
                gvUpcomingBirthday.DataBind();
            }

            // Department Chart

            Result<List<Department>> _DepartmentResult = _IDepartmentService.GetDepartmentList();
            if (_DepartmentResult.IsSuccess)
            {
                string _DepartmentHtml = string.Empty;
                _DepartmentHtml += "<ul>";
                foreach (var item in _DepartmentResult.Data)
                {
                    _DepartmentHtml += "<li>" + item.DepartmentName + "</li>";
                }
                _DepartmentHtml += "</ul>";
                divDepartment.InnerHtml = _DepartmentHtml;
            }

            // Active, Present, Absent,LeaveEmployees , Interview Counts

            //Result<List<Employee>> _Result = _IEmployeeService.GetAllIsActiveEmployeeList();
            Result<List<Employee>> _Result = _IEmployeeService.GetAllIsActiveFromEmployeeProfileList();
            Result<int> _ResultPresentEmployee = _IEmployeeService.GetPresentEmployee();
            Result<int> _ResultLeaveEmployee = _IEmployeeLeaveCategoryService.GetLeaveEmployeeCount();

            lblActiveEmployee.Text = Convert.ToString(_Result.Data.Count());

            if (_ResultPresentEmployee.IsSuccess)
            {
                lblPresentEmployee.Text = Convert.ToString(_ResultPresentEmployee.Data);
            }

            if (_ResultLeaveEmployee.IsSuccess)
            {
                lblLeaveEmployee.Text = Convert.ToString(_ResultLeaveEmployee.Data);
            }

            lblAbsentEmployee.Text = Convert.ToString(_Result.Data.Where(x => x.IsLeave == false).Count() - _ResultPresentEmployee.Data);

            Result<int> _ResultInterview = _IInterviewService.GetTodayInterviewCount();
            if (_ResultInterview.IsSuccess)
            {
                lblInterview.Text = Convert.ToString(_ResultInterview.Data);
            }

            Result<int> _ResultDevice = _IDeviceService.GetDeviceCount();
            if (_ResultDevice.IsSuccess)
            {
                lblBioMetricDevice.Text = Convert.ToString(_ResultDevice.Data);
            }

            #region totalHours

            int _Month = 9;
            int _Year = DateTime.Now.Year;

            DateTime _StartDate = new DateTime(_Year, _Month, 1);
            DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);

            if (_EndDate.Date > DateTime.Now.Date)
            {
                _EndDate = DateTime.Now.Date;
            }


            //bool falgs = false;


            //if (_Result.Data.Count > 0)
            //{
            //    EmployeeAttendanceOverTimeResult _EmployeeAttendanceResult;
            //    decimal TotalEmpHours = 0;
            //    decimal TotalCurrentMonthHors = 0;

            //    for (int x = 0; x < _Result.Data.Count; x++)
            //    {

            //        _EmployeeAttendanceResult = new EmployeeAttendanceOverTimeResult();


            //        if (_Result.Data[x].IsLeave == false)
            //        {
            //            Result<List<EmployeeAttendanceDevices>> _GetResult = _IEmployeeAttendanceDeviceService.GetEmployeeAttendanceDeviceByEmployeeId(_Result.Data[x].EmployeeID, _StartDate, _EndDate);

            //            if (_GetResult.IsSuccess)
            //            {
            //                for (DateTime _date = _StartDate; _date <= _EndDate; _date = _date.AddDays(1.0))
            //                {
            //                    _EmployeeAttendanceResult = new EmployeeAttendanceOverTimeResult();
            //                    _EmployeeAttendanceResult.Attendances = String.Join(" | ", _GetResult.Data.Where(r => r.AttendanceDate == _date).OrderBy(r => r.AttendanceDateTime).Select(r => r.PunchTime));

            //                    string FinalHours = "";

            //                    if (_EmployeeAttendanceResult.Attendances != "" && _EmployeeAttendanceResult.Attendances != null)
            //                    {
            //                        string[] arrPunchTime = _EmployeeAttendanceResult.Attendances.Split('|');


            //                        List<string> PunchOneTime = new List<string>();
            //                        List<string> PunchBefore = new List<string>();
            //                        List<string> PunchAfter = new List<string>();

            //                        if (arrPunchTime.Count() == 2 || arrPunchTime.Count() == 3)
            //                        {
            //                            for (int i = 0; i < arrPunchTime.Length; i++)
            //                            {
            //                                if (i == 0)
            //                                {
            //                                    PunchOneTime.Add(arrPunchTime[i].Trim());
            //                                }
            //                                else if (i == 1)
            //                                {
            //                                    PunchOneTime.Add(arrPunchTime[i].Trim());
            //                                }
            //                            }

            //                            DateTime PunchOneTimeFrom;
            //                            DateTime PunchOneTimeTo;
            //                            string sBeforeDateFrom = PunchOneTime[0];
            //                            string sBeforeDateTo = PunchOneTime[1];

            //                            if (DateTime.TryParse(sBeforeDateFrom, out PunchOneTimeFrom) && DateTime.TryParse(sBeforeDateTo, out PunchOneTimeTo))
            //                            {
            //                                TimeSpan bTS = PunchOneTimeTo - PunchOneTimeFrom;
            //                                int hourb = bTS.Hours;
            //                                int minsb = bTS.Minutes;
            //                                int secsb = bTS.Seconds;
            //                                FinalHours = hourb.ToString("00") + ":" + minsb.ToString("00") + ":" + secsb.ToString("00");
            //                            }
            //                            PunchOneTime.Clear();
            //                        }
            //                        else if (arrPunchTime.Count() == 4 || arrPunchTime.Count() == 5)
            //                        {
            //                            for (int i = 0; i < arrPunchTime.Length; i++)
            //                            {
            //                                if (i == 0)
            //                                {
            //                                    PunchBefore.Add(arrPunchTime[i].Trim());
            //                                }
            //                                else if (i == 1)
            //                                {
            //                                    PunchBefore.Add(arrPunchTime[i].Trim());
            //                                }
            //                                else if (i == 2)
            //                                {
            //                                    PunchAfter.Add(arrPunchTime[i].Trim());
            //                                }
            //                                else if (i == 3)
            //                                {
            //                                    PunchAfter.Add(arrPunchTime[i].Trim());
            //                                }
            //                            }

            //                            DateTime PunchBeforeFrom;
            //                            DateTime PunchBeforeTo;
            //                            string sBeforeDateFrom = PunchBefore[0];
            //                            string sBeforeDateTo = PunchBefore[1];

            //                            string timeBeforeDiff = "";
            //                            if (DateTime.TryParse(sBeforeDateFrom, out PunchBeforeFrom) && DateTime.TryParse(sBeforeDateTo, out PunchBeforeTo))
            //                            {
            //                                TimeSpan bTS = PunchBeforeTo - PunchBeforeFrom;
            //                                int hourb = bTS.Hours;
            //                                int minsb = bTS.Minutes;
            //                                int secsb = bTS.Seconds;
            //                                timeBeforeDiff = hourb.ToString("00") + ":" + minsb.ToString("00") + ":" + secsb.ToString("00");

            //                            }

            //                            string timeAfterDiff = "";
            //                            if (PunchAfter.Count() > 0)
            //                            {
            //                                DateTime PunchAfterFrom;
            //                                DateTime PunchAfterTo;
            //                                string sAfterDateFrom = PunchAfter[0];
            //                                string sAfterDateTo = PunchAfter[1];


            //                                if (DateTime.TryParse(sAfterDateFrom, out PunchAfterFrom) && DateTime.TryParse(sAfterDateTo, out PunchAfterTo))
            //                                {
            //                                    TimeSpan TS = PunchAfterTo - PunchAfterFrom;
            //                                    int houra = TS.Hours;
            //                                    int minsa = TS.Minutes;
            //                                    int secsa = TS.Seconds;
            //                                    timeAfterDiff = houra.ToString("00") + ":" + minsa.ToString("00") + ":" + secsa.ToString("00");

            //                                }
            //                            }

            //                            TimeSpan BeforeDiff = TimeSpan.Parse(timeBeforeDiff);
            //                            TimeSpan AfterDiff = TimeSpan.Parse(timeAfterDiff);
            //                            TimeSpan FinalDiff = BeforeDiff.Add(AfterDiff);

            //                            int hour = FinalDiff.Hours;
            //                            int mins = FinalDiff.Minutes;
            //                            int secs = FinalDiff.Seconds;
            //                            FinalHours = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

            //                            timeBeforeDiff = "";
            //                            timeAfterDiff = "";
            //                            PunchBefore.Clear();
            //                            PunchAfter.Clear();
            //                        }

            //                    }
            //                    else
            //                    {
            //                        FinalHours = "";
            //                    }

            //                    if (FinalHours != "")
            //                    {
            //                        decimal one = Convert.ToDecimal(TimeSpan.Parse(FinalHours).TotalHours);

            //                        TotalCurrentMonthHors = TotalCurrentMonthHors + one;
            //                        falgs = true;
            //                    }
            //                    FinalHours = "";

            //                }
            //            }
            //        }

            //        if (falgs == true)
            //        {
            //            TotalEmpHours = TotalEmpHours + TotalCurrentMonthHors;
            //            falgs = false;
            //        }

            //    }

            //} 

            #endregion

            // Salary Chart 

            ILookupService _ILookupService = new LookupService();
            Result<List<Dashboard>> _AllEmpMonthWiseSalaryResult = _ILookupService.GetSalaryChartInfoForAllEmployee(SessionHelper.SessionDetail.FinancialYearId);

            if (_AllEmpMonthWiseSalaryResult.IsSuccess)
            {
                if (_AllEmpMonthWiseSalaryResult.Data != null)
                {
                    ChartMonthSalaryAdmin.DataSource = _AllEmpMonthWiseSalaryResult.Data;
                    ChartMonthSalaryAdmin.Series["SeriesTotalAdmin"].XValueMember = "Month";
                    ChartMonthSalaryAdmin.Series["SeriesTotalAdmin"].YValueMembers = "PaidTotalSalary";
                    ChartMonthSalaryAdmin.DataBind();
                    //ChartMonthSalaryAdmin.Width = 1000;
                }
            }

            //Result<List<EmployeePaidSalarys>> _ResultAttendanceAdmin = _IEmployeePaidSalaryService.GetAttendanceChartInfo(SessionHelper.SessionDetail.FinancialYearId);
            //if (_ResultAttendanceAdmin.IsSuccess)
            //{
            //    if (_ResultAttendanceAdmin.Data != null)
            //    {
            //        _ResultAttendanceAdmin.Data.ForEach(e => e.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(e.MonthNo));
            //        ChartDepartmentAdmin.DataSource = _ResultAttendanceAdmin.Data;

            //        ChartAttendanceAdmin.Series["SeriesTotalDaysAdmin"].XValueMember = "Month";

            //        ChartAttendanceAdmin.Series["SeriesTotalDaysAdmin"].YValueMembers = "TotalDays";
            //        ChartAttendanceAdmin.Series["SeriesPresentDaysAdmin"].YValueMembers = "TotalPresentDays";
            //        //ChartAttendanceAdmin.Series["SeriesWeekluOffAdmin"].YValueMembers = "WeeklyOff";
            //        //ChartAttendanceAdmin.Series["SeriesHolidaysAdmin"].YValueMembers = "TotalHolidays";
            //        //ChartAttendanceAdmin.Series["SeriesTotalUseLeaveAdmin"].YValueMembers = "TotalUseLeave";
            //        ChartAttendanceAdmin.DataBind();
            //        //ChartAttendanceAdmin.Width = 1000;
            //    }
            //}

            // Department Chart

            Result<List<EmployeeDepartment>> _ResultDepartment = _IEmployeeService.GetDepartmentChartInfo();
            if (_ResultDepartment.IsSuccess)
            {
                if (_ResultDepartment.Data != null)
                {
                    ChartDepartmentAdmin.DataSource = _ResultDepartment.Data.Where(x => x.TotalEmployee != 0);
                    ChartDepartmentAdmin.Series["SeriesTotalEmployeeAdmin"].XValueMember = "Department";
                    ChartDepartmentAdmin.Series["SeriesTotalEmployeeAdmin"].YValueMembers = "TotalEmployee";
                    ChartDepartmentAdmin.DataBind();
                }
            }
        }

        #endregion

    }
}