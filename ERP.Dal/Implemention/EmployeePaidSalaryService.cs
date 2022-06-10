using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Dal.Implemention
{
    public class EmployeePaidSalaryService : IEmployeePaidSalaryService
    {
        public Result<Boolean> CheckSalaryPaidByEmployee(Guid p_EmployeeId, string p_Month, int p_Year)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeePaidSalaries.Where(eps => eps.Month == p_Month && eps.Year == p_Year && eps.EmployeeId == p_EmployeeId && eps.IsPaid == true && eps.IsActive == true).Count();

                    if (_Count > 0)
                    {
                        _Result.Data = true;
                    }
                    else
                    {
                        _Result.Data = false;
                    }

                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        //public Result<EmployeePaidSalarys> GetEmployeePaidSalaryByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year)
        public Result<EmployeePaidSalarys> GetEmployeePaidSalaryByEmployeeIds(Guid p_EmployeeId, string p_Month, int p_Year, DateTime? p_FormDate, DateTime? p_ToDate, Guid _FinancialYearId)
        {
            Result<EmployeePaidSalarys> _Result = new Result<EmployeePaidSalarys>();

            _Result.IsSuccess = false;
            int Present = Convert.ToInt32(AttendanceType.Present);
            int Holiday = Convert.ToInt32(AttendanceType.Holiday);
            int WeeklyOff = Convert.ToInt32(AttendanceType.WeeklyOff);
            int Leave = Convert.ToInt32(AttendanceType.Leave);


            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {

                    //DateTime aprilF = Convert.ToDateTime("01-04-" + p_Year);     // 01/04/2019 12:00:00 AM
                    //DateTime aprilS = aprilF.AddMonths(1).AddDays(-1);                // 30/04/2019 12:00:00 AM
                    //int AprilM = Convert.ToDateTime("01-04-" + p_Year).Month;


                    //int iMonthNo = Convert.ToDateTime("01-" + p_Month + "-" + p_Year).Month;      // 10
                    //DateTime iDateF = Convert.ToDateTime("01-" + p_Month + "-" + p_Year).Date;      // 01/10/2019 12:00:00 AM
                    //DateTime iDateS = iDateF.AddMonths(1).AddDays(-1);                    //   31/10/2019 12:00:00 AM

                    //var _EmpQuery = from e in dbContext.EmployeeAttendances
                    //                join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                    //                join d in dbContext.DepartmentMasters on em.DepartmentId equals d.DepartmentID
                    //                group e by new { Month = e.AttendanceDate.Month, Id = e.EmployeeId } into ea
                    //                where ea.FirstOrDefault().EmployeeId == p_EmployeeId && ea.FirstOrDefault().IsActive == true && ea.FirstOrDefault().FinancialYearId == _FinancialYearId
                    //                orderby ea.Key.Month
                    //                select new EmployeePaidSalarys
                    //                {
                    //                    MonthNo = ea.Key.Month,
                    //                    TotalDays = ea.Count(),
                    //                    TotalPresentDays = ea.Where(p => p.AttendanceType == Present).Count(),
                    //                    TotalHolidays = ea.Where(p => p.AttendanceType == Holiday).Count(),
                    //                    WeeklyOff = ea.Where(p => p.AttendanceType == WeeklyOff).Count(),
                    //                    TotalUseLeave = ea.Where(p => p.AttendanceType == Leave).Sum(a => a.Attendance) ?? 0,
                    //                };

                    //List<EmployeePaidSalarys> _ListCout = _EmpQuery.ToList();
                    //int caryForward = 0;
                    //decimal LeaveSum = 0;
                    //decimal? _TotalPaidLeaveAmountS = 0;
                    //bool flags = false;

                    //if (_ListCout.Count() > 0)
                    //{

                    //    EmployeeSalarys empSal = new EmployeeSalarys();

                    //    var TotSal = from s in dbContext.EmployeeSalaries
                    //                 where s.EmployeeId == p_EmployeeId
                    //                 select new EmployeeSalarys
                    //                 {
                    //                     TotalSalary = s.TotalSalary ?? 0
                    //                 };

                    //    empSal = TotSal.FirstOrDefault();


                    //    int count = 0;

                    //    for (int i = 0; i < _ListCout.Count; i++)
                    //    {

                    //        if (Convert.ToDouble(_ListCout[i].TotalUseLeave) == 0.00)
                    //        {
                    //            caryForward = caryForward + 1;
                    //        }
                    //        else
                    //        {
                    //            LeaveSum = LeaveSum + (_ListCout[i].TotalUseLeave - 1);
                    //        }

                    //        if (LeaveSum > caryForward)
                    //        {
                    //            LeaveSum = LeaveSum - caryForward;
                    //            caryForward = caryForward - 1;
                    //        }

                    //        count++;

                    //        if (count == _ListCout.Count)
                    //        {
                    //            decimal OneDaySalary = empSal.TotalSalary / _ListCout[i].TotalDays;

                    //            _TotalPaidLeaveAmountS = OneDaySalary * LeaveSum;
                    //        }
                    //    }
                    //}

                    var _Query = from eps in dbContext.EmployeePaidSalaries
                                 join e in dbContext.EmployeeMasters on eps.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where eps.EmployeeId == p_EmployeeId && eps.Month == p_Month && eps.Year == p_Year
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = eps.EmployeePaidSalaryID,
                                     EmployeeId = e.EmployeeID,
                                     Department = d.Department,
                                     FullName = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     Basic = eps.Basic ?? 0,
                                     TotalEarning = eps.TotalEarning ?? 0,
                                     TotalDeduction = eps.TotalDeduction ?? 0,
                                     TotalSalary = eps.TotalSalary ?? 0,
                                     PaidBasic = eps.PaidBasic ?? 0,
                                     PaidTotalEarning = eps.PaidTotalEarning ?? 0,
                                     PaidTotalSalary = eps.PaidTotalSalary ?? 0,
                                     PaidTotalDeduction = eps.PaidTotalDeduction ?? 0,
                                     Month = eps.Month,
                                     Year = eps.Year,
                                     TotalOverTimeDays = eps.TotalOverTimeDays ?? 0,
                                     TotalDays = eps.TotalDays,
                                     AllowLeave = eps.AllowLeave ?? 0,
                                     TotalUseLeave = eps.TotalUseLeave ?? 0,
                                     TotalHolidays = eps.TotalHoliday ?? 0,
                                     TotalPresentDays = eps.TotalPresentDays ?? 0,
                                     TotalPaidLeave = eps.TotalPaidLeave ?? 0,
                                     TotalPaidLeaveAmount = eps.TotalPaidLeaveAmount ?? 0,
                                     //TotalPaidLeaveAmount = _TotalPaidLeaveAmountS ?? 0,
                                     TotalOverTimeAmount = eps.TotalOverTimeAmount ?? 0,
                                     PaidLoanAmount = eps.PaidLoanAmount ?? 0,
                                     PaidDate = eps.PaidDate,
                                     IsPaid = eps.IsPaid,
                                     ProfessionalTax = eps.ProfessionalTax ?? 0,
                                     TotalHours = eps.TotalHours ?? 0,
                                     TotalOverTimeHours = eps.TotalOverTimeHours ?? 0,
                                     SalaryFromDate = eps.SalaryFromDate,
                                     SalaryToDate = eps.SalaryToDate,
                                 };

                    if (p_FormDate != null && p_ToDate != null)
                    {
                        _Query = _Query.Where(x => x.SalaryFromDate >= p_FormDate && x.SalaryToDate <= p_ToDate);
                    }

                    EmployeePaidSalarys _EmployeePaidSalarys = _Query.FirstOrDefault();

                    if (_EmployeePaidSalarys != null)
                    {
                        _Result.Data = _EmployeePaidSalarys;
                    }
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<Allowance>> GetEmployeeAllowanceByEmployeePaidSalaryId(Guid p_EmployeePaidSalaryId, Guid p_EmployeeId)
        {
            Result<List<Allowance>> _Result = new Result<List<Allowance>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from a in dbContext.AllowanceMasters
                                 join ea in dbContext.EmployeeAllowanceMaps on a.AllowanceID equals ea.AllowanceId
                                 join epa in dbContext.EmployeePaidAllowanceMaps.Where(ea => ea.EmployeePaidSalaryId == p_EmployeePaidSalaryId) on a.AllowanceID equals epa.AllowanceId into eam
                                 from x in eam.DefaultIfEmpty()
                                 where a.IsActive == true && ea.IsActive == true && ea.EmployeeId == p_EmployeeId
                                 orderby a.SortNo
                                 select new Allowance
                                 {
                                     AllowanceID = a.AllowanceID,
                                     AllowanceName = a.Allowance,
                                     IsConsider = a.IsConsider,
                                     Amount = (x == null ? ea.Amount : x.Amount),
                                     PaidAmount = (x == null ? ea.Amount : x.PaidAmount),
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<Deduction>> GetEmployeeDeductionByEmployeePaidSalaryId(Guid p_EmployeePaidSalaryId, Guid p_EmployeeId)
        {
            Result<List<Deduction>> _Result = new Result<List<Deduction>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DeductionMasters
                                 join ed in dbContext.EmployeeDeductionMaps on d.DeductionID equals ed.DeductionId
                                 join epd in dbContext.EmployeePaidDeductionMaps.Where(ae => ae.EmployeePaidSalaryId == p_EmployeePaidSalaryId) on d.DeductionID equals epd.DeductionId into edm
                                 from x in edm.DefaultIfEmpty()
                                 where d.IsActive == true && ed.IsActive == true && ed.EmployeeId == p_EmployeeId
                                 orderby d.SortNo
                                 select new Deduction
                                 {
                                     DeductionID = d.DeductionID,
                                     DeductionName = d.Deduction,
                                     IsConsider = d.IsConsider,
                                     Amount = (x == null ? ed.Amount : x.Amount),
                                     PaidAmount = (x == null ? ed.Amount : x.PaidAmount),
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeeLoans>> GetEmployeeLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year)
        {
            Result<List<EmployeeLoans>> _Result = new Result<List<EmployeeLoans>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from el in dbContext.EmployeeLoans
                                 where el.IsActive == true && el.IsComplete == false && el.EmployeeId == p_EmployeeId
                                 select new EmployeeLoans
                                 {
                                     EmployeeLoanID = el.EmployeeLoanMapID,
                                     LoanTitle = el.LoanTitle,
                                     Amount = el.Amount,
                                     TotalMonths = el.TotalMonths ?? 1,
                                     PaidLoan = el.EmployeePaidLoans.Count() > 0 ? el.EmployeePaidLoans.Where(e => e.IsActive == true).Sum(e => e.PaidAmount) : 0,
                                     PaidLoanAmount = el.EmployeePaidLoans.Count() > 0 ? el.EmployeePaidLoans.Where(e => e.IsActive == true && e.Month == p_Month && e.Year == p_Year).Select(e => (decimal?)e.PaidAmount).FirstOrDefault() : null,
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetEmployeeCompletedPaidSalaryByMonth(string p_Month, int p_Year, int p_IsMonthSalary)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _EmployeeList = dbContext.EmployeeSalaries.Where(x => x.SalaryType == p_IsMonthSalary && x.EmployeeMaster.IsActive == true && x.EmployeeMaster.IsLeave == true).Select(s => s.EmployeeId).ToList();
                    var _Query = from eps in dbContext.EmployeePaidSalaries
                                 join e in dbContext.EmployeeMasters on eps.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where eps.Month == p_Month && eps.Year == p_Year && eps.IsActive == true && eps.IsPaid == true && e.IsActive == true && e.IsLeave == false
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = eps.EmployeePaidSalaryID,
                                     EmployeeId = e.EmployeeID,
                                     Department = d.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     PaidBasic = eps.PaidBasic ?? 0,
                                     PaidTotalEarning = eps.PaidTotalEarning ?? 0,
                                     PaidTotalSalary = eps.PaidTotalSalary ?? 0,
                                     PaidTotalDeduction = eps.PaidTotalDeduction ?? 0,
                                 };

                    _Result.Data = _Query.Where(a => _EmployeeList.Contains(a.EmployeeId)).ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetEmployeeCompletedPaidSalaryByDate(DateTime p_FromDate, DateTime p_ToDate, int p_IsMonthSalary)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _EmployeeList = dbContext.EmployeeSalaries.Where(x => x.SalaryType == p_IsMonthSalary && x.EmployeeMaster.IsActive == true && x.EmployeeMaster.IsLeave == true).Select(s => s.EmployeeId).ToList();
                    var _Query = from eps in dbContext.EmployeePaidSalaries
                                 join e in dbContext.EmployeeMasters on eps.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where eps.PaidDate >= p_FromDate && eps.PaidDate == p_ToDate && eps.IsActive == true && eps.IsPaid == true && e.IsActive == true && e.IsLeave == false
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = eps.EmployeePaidSalaryID,
                                     EmployeeId = e.EmployeeID,
                                     Department = d.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     PaidBasic = eps.PaidBasic ?? 0,
                                     PaidTotalEarning = eps.PaidTotalEarning ?? 0,
                                     PaidTotalSalary = eps.PaidTotalSalary ?? 0,
                                     PaidTotalDeduction = eps.PaidTotalDeduction ?? 0,
                                 };

                    _Result.Data = _Query.Where(a => _EmployeeList.Contains(a.EmployeeId)).ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetEmployeePendingSalaryByMonth(string p_Month, int p_IntMonth, int p_Year, int p_IsMonthSalary)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    DateTime _MonthYearDate = new DateTime(p_Year, p_IntMonth, 01).AddMonths(1);
                    var _Query = from e in dbContext.EmployeeMasters
                                 join es in dbContext.EmployeeSalaries on e.EmployeeID equals es.EmployeeId
                                 where e.IsActive == true && e.JoinDate < _MonthYearDate && es.SalaryType == p_IsMonthSalary && e.IsLeave == false && es.IsActive == true && !e.EmployeePaidSalaries.Where(eps => eps.IsActive == true && eps.IsPaid == true && eps.Month == p_Month && eps.Year == p_Year).Select(eps => eps.EmployeeId).Contains(e.EmployeeID)
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = Guid.Empty,
                                     EmployeeId = e.EmployeeID,
                                     Department = e.DepartmentMaster.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     PaidBasic = es.Basic ?? 0,
                                     PaidTotalEarning = es.TotalEarning ?? 0,
                                     PaidTotalSalary = es.TotalSalary ?? 0,
                                     PaidTotalDeduction = es.TotalDeduction ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetEmployeePendingSalaryByDate(DateTime p_FromDate, DateTime p_ToDate, int p_IsMonthSalary)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 join es in dbContext.EmployeeSalaries on e.EmployeeID equals es.EmployeeId
                                 where e.IsActive == true && e.JoinDate < p_FromDate && es.SalaryType == p_IsMonthSalary && e.IsLeave == false && es.IsActive == true && !e.EmployeePaidSalaries.Where(eps => eps.IsActive == true && eps.IsPaid == true && eps.SalaryFromDate >= p_FromDate && eps.SalaryToDate <= p_ToDate).Select(eps => eps.EmployeeId).Contains(e.EmployeeID)
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = Guid.Empty,
                                     EmployeeId = e.EmployeeID,
                                     Department = e.DepartmentMaster.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     PaidBasic = es.Basic ?? 0,
                                     PaidTotalEarning = es.TotalEarning ?? 0,
                                     PaidTotalSalary = es.TotalSalary ?? 0,
                                     PaidTotalDeduction = es.TotalDeduction ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<Boolean> SaveEmployeePaidSalary(EmployeePaidSalarys p_EmployeePaidSalary, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeePaidSalary _EmployeePaidSalary = new EmployeePaidSalary();

                    if (p_EmployeePaidSalary.EmployeePaidSalaryID == Guid.Empty)
                    {
                        _EmployeePaidSalary.EmployeePaidSalaryID = Guid.NewGuid();
                        _EmployeePaidSalary.IsActive = true;
                        _EmployeePaidSalary.CreatedDate = DateTime.Now;
                        _EmployeePaidSalary.CreatedBy = p_UserId;
                        _EmployeePaidSalary.ModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        _EmployeePaidSalary = dbContext.EmployeePaidSalaries.Where(eps => eps.EmployeePaidSalaryID == p_EmployeePaidSalary.EmployeePaidSalaryID).FirstOrDefault();

                        _EmployeePaidSalary.ModifiedDate = DateTime.Now;
                        _EmployeePaidSalary.ModifiedBy = p_UserId;
                    }

                    _EmployeePaidSalary.EmployeeId = p_EmployeePaidSalary.EmployeeId;
                    _EmployeePaidSalary.Basic = p_EmployeePaidSalary.Basic;
                    _EmployeePaidSalary.TotalEarning = p_EmployeePaidSalary.TotalEarning;
                    _EmployeePaidSalary.TotalDeduction = p_EmployeePaidSalary.TotalDeduction;
                    _EmployeePaidSalary.TotalSalary = p_EmployeePaidSalary.TotalSalary;
                    _EmployeePaidSalary.PaidBasic = p_EmployeePaidSalary.PaidBasic;
                    _EmployeePaidSalary.PaidTotalEarning = p_EmployeePaidSalary.PaidTotalEarning;
                    _EmployeePaidSalary.PaidTotalDeduction = p_EmployeePaidSalary.PaidTotalDeduction;
                    _EmployeePaidSalary.PaidTotalSalary = p_EmployeePaidSalary.PaidTotalSalary;
                    _EmployeePaidSalary.Month = p_EmployeePaidSalary.Month;
                    _EmployeePaidSalary.Year = p_EmployeePaidSalary.Year;
                    _EmployeePaidSalary.TotalOverTimeDays = p_EmployeePaidSalary.TotalOverTimeDays;
                    _EmployeePaidSalary.TotalDays = p_EmployeePaidSalary.TotalDays;
                    _EmployeePaidSalary.AllowLeave = p_EmployeePaidSalary.AllowLeave;
                    _EmployeePaidSalary.TotalUseLeave = p_EmployeePaidSalary.TotalUseLeave;
                    _EmployeePaidSalary.TotalHoliday = p_EmployeePaidSalary.TotalHolidays;
                    _EmployeePaidSalary.TotalPaidLeave = p_EmployeePaidSalary.TotalPaidLeave;
                    _EmployeePaidSalary.TotalPaidLeaveAmount = p_EmployeePaidSalary.TotalPaidLeaveAmount;
                    _EmployeePaidSalary.TotalOverTimeAmount = p_EmployeePaidSalary.TotalOverTimeAmount;
                    _EmployeePaidSalary.PaidLoanAmount = p_EmployeePaidSalary.PaidLoanAmount;
                    _EmployeePaidSalary.PaidDate = p_EmployeePaidSalary.PaidDate;
                    _EmployeePaidSalary.IsPaid = p_EmployeePaidSalary.IsPaid;
                    _EmployeePaidSalary.PaidBy = p_EmployeePaidSalary.PaidBy;
                    _EmployeePaidSalary.FinancialYearId = p_EmployeePaidSalary.FinancialYearId;
                    _EmployeePaidSalary.TotalPresentDays = p_EmployeePaidSalary.TotalPresentDays;
                    _EmployeePaidSalary.ProfessionalTax = p_EmployeePaidSalary.ProfessionalTax;
                    _EmployeePaidSalary.SalaryFromDate = p_EmployeePaidSalary.SalaryFromDate;
                    _EmployeePaidSalary.SalaryToDate = p_EmployeePaidSalary.SalaryToDate;

                    if (p_EmployeePaidSalary.EmployeePaidSalaryID == Guid.Empty)
                    {
                        dbContext.EmployeePaidSalaries.Add(_EmployeePaidSalary);
                    }

                    dbContext.SaveChanges();

                    if (p_EmployeePaidSalary.EmployeePaidSalaryID != Guid.Empty)
                    {
                        dbContext.EmployeePaidAllowanceMaps.RemoveRange(dbContext.EmployeePaidAllowanceMaps.Where(epa => epa.EmployeePaidSalaryId == _EmployeePaidSalary.EmployeePaidSalaryID));
                        dbContext.EmployeePaidDeductionMaps.RemoveRange(dbContext.EmployeePaidDeductionMaps.Where(epd => epd.EmployeePaidSalaryId == _EmployeePaidSalary.EmployeePaidSalaryID));
                        dbContext.EmployeePaidLoans.RemoveRange(dbContext.EmployeePaidLoans.Where(epd => epd.Month == _EmployeePaidSalary.Month && epd.Year == _EmployeePaidSalary.Year && epd.EmployeeLoan.EmployeeId == _EmployeePaidSalary.EmployeeId));
                    }

                    foreach (Allowance _PaidAllowance in p_EmployeePaidSalary.ListPaidAllowance)
                    {
                        EmployeePaidAllowanceMap _EmployeePaidAllowanceMap = new EmployeePaidAllowanceMap();

                        _EmployeePaidAllowanceMap.EmployeePaidAllowanceMapID = Guid.NewGuid();
                        _EmployeePaidAllowanceMap.EmployeePaidSalaryId = _EmployeePaidSalary.EmployeePaidSalaryID;
                        _EmployeePaidAllowanceMap.EmployeeId = _EmployeePaidSalary.EmployeeId;
                        _EmployeePaidAllowanceMap.AllowanceId = _PaidAllowance.AllowanceID;
                        _EmployeePaidAllowanceMap.Amount = _PaidAllowance.Amount;
                        _EmployeePaidAllowanceMap.PaidAmount = _PaidAllowance.PaidAmount;
                        _EmployeePaidAllowanceMap.IsActive = true;
                        _EmployeePaidAllowanceMap.CreatedDate = DateTime.Now;
                        _EmployeePaidAllowanceMap.CreatedBy = p_UserId;
                        _EmployeePaidAllowanceMap.ModifiedDate = DateTime.Now;

                        dbContext.EmployeePaidAllowanceMaps.Add(_EmployeePaidAllowanceMap);
                    }

                    foreach (Deduction _PaidDeduction in p_EmployeePaidSalary.ListPaidDeduction)
                    {
                        EmployeePaidDeductionMap _EmployeePaidDeductionMap = new EmployeePaidDeductionMap();

                        _EmployeePaidDeductionMap.EmployeePaidDeductionMapID = Guid.NewGuid();
                        _EmployeePaidDeductionMap.EmployeePaidSalaryId = _EmployeePaidSalary.EmployeePaidSalaryID;
                        _EmployeePaidDeductionMap.EmployeeId = _EmployeePaidSalary.EmployeeId;
                        _EmployeePaidDeductionMap.DeductionId = _PaidDeduction.DeductionID;
                        _EmployeePaidDeductionMap.Amount = _PaidDeduction.Amount;
                        _EmployeePaidDeductionMap.PaidAmount = _PaidDeduction.PaidAmount;
                        _EmployeePaidDeductionMap.IsActive = true;
                        _EmployeePaidDeductionMap.CreatedDate = DateTime.Now;
                        _EmployeePaidDeductionMap.CreatedBy = p_UserId;
                        _EmployeePaidDeductionMap.ModifiedDate = DateTime.Now;

                        dbContext.EmployeePaidDeductionMaps.Add(_EmployeePaidDeductionMap);
                    }

                    dbContext.SaveChanges();

                    foreach (EmployeeLoans _EmployeePaidLoans in p_EmployeePaidSalary.ListEmployeePaidLoan)
                    {
                        EmployeePaidLoan _EmployeePaidLoan = new EmployeePaidLoan();

                        _EmployeePaidLoan.EmployeePaidLoanMapID = Guid.NewGuid();
                        _EmployeePaidLoan.EmployeeLoanMapId = _EmployeePaidLoans.EmployeeLoanID;
                        _EmployeePaidLoan.IsActive = true;
                        _EmployeePaidLoan.CreatedDate = DateTime.Now;
                        _EmployeePaidLoan.CreatedBy = p_UserId;
                        _EmployeePaidLoan.ModifiedDate = DateTime.Now;
                        _EmployeePaidLoan.Month = _EmployeePaidSalary.Month;
                        _EmployeePaidLoan.Year = _EmployeePaidSalary.Year;
                        _EmployeePaidLoan.PaidAmount = _EmployeePaidLoans.PaidLoanAmount ?? 0;
                        _EmployeePaidLoan.PaidDate = _EmployeePaidSalary.PaidDate;

                        dbContext.EmployeePaidLoans.Add(_EmployeePaidLoan);

                        if (_EmployeePaidLoans.IsComplete && _EmployeePaidSalary.IsPaid)
                        {
                            EmployeeLoan _EmployeeLoan = dbContext.EmployeeLoans.Where(el => el.EmployeeLoanMapID == _EmployeePaidLoans.EmployeeLoanID).FirstOrDefault();
                            _EmployeeLoan.IsComplete = true;
                            _EmployeeLoan.ModifiedBy = p_UserId;
                            _EmployeeLoan.ModifiedDate = System.DateTime.Now;

                            dbContext.SaveChanges();
                        }
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EmployeePaidSalary.EmployeePaidSalaryID);
                    _Result.Data = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetLeaveDetailsByEmployeeId(Guid p_EmployeeId, Guid p_FinancialYearId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeePaidSalaries
                                 where e.IsActive == true && e.EmployeeId == p_EmployeeId && e.FinancialYearId == p_FinancialYearId
                                 select new EmployeePaidSalarys
                                 {
                                     Month = e.Month,
                                     Year = e.Year,
                                     AllowLeave = e.AllowLeave ?? 0,
                                     TotalUseLeave = e.TotalUseLeave ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetLeaveOpeningDetailsByFinancialYearId(Guid p_FinancialYearId, Guid? p_EmployeeId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _FinancialYear = dbContext.FinancialYearMasters.Where(f => f.FinancialYearID == p_FinancialYearId).Select(f => f.Year).FirstOrDefault();

                    int _AttendanceType = Convert.ToInt32(AttendanceType.Leave);

                    var _Query = from e in dbContext.EmployeeMasters
                                 join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                 where e.IsActive == true && e.IsLeave == false && (p_EmployeeId == null ? true : e.EmployeeID == p_EmployeeId)
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeeId = e.EmployeeID,
                                     Department = e.DepartmentMaster.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     NoOfLeavePerMonth = et.NoOfLeavePerMonth,
                                     JoinDate = e.JoinDate ?? DateTime.Now,
                                     PaidMonthCount = e.EmployeePaidSalaries.Where(eps => eps.FinancialYearId == p_FinancialYearId && eps.IsPaid == true && eps.IsActive == true).Count(),
                                     PaidMonthAllowLeave = e.EmployeePaidSalaries.Where(eps => eps.FinancialYearId == p_FinancialYearId && eps.IsPaid == true && eps.IsActive == true).Sum(eps => eps.AllowLeave) ?? 0,
                                     FinancialYear = _FinancialYear,
                                     TotalUseLeave = e.EmployeeAttendances.Where(ea => ea.IsActive == true && ea.AttendanceType == _AttendanceType && ea.FinancialYearId == p_FinancialYearId).Sum(ea => ea.Attendance) ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> SalaryReport(List<Guid> p_ListOfEmployeeId, List<string> p_ListOfMonthYear, Guid p_FinancialYearId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from eps in dbContext.EmployeePaidSalaries
                                 join e in dbContext.EmployeeMasters on eps.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where p_ListOfEmployeeId.Contains(eps.EmployeeId) && p_ListOfMonthYear.Contains(eps.Month) && eps.FinancialYearId == p_FinancialYearId && eps.IsActive == true && eps.IsPaid == true
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = eps.EmployeePaidSalaryID,
                                     EmployeeId = eps.EmployeeId,
                                     Department = d.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     PaidBasic = eps.PaidBasic ?? 0,
                                     PaidTotalEarning = eps.PaidTotalEarning ?? 0,
                                     PaidTotalSalary = eps.PaidTotalSalary ?? 0,
                                     PaidTotalDeduction = eps.PaidTotalDeduction ?? 0,
                                     Month = eps.Month,
                                     Year = eps.Year,
                                     TotalPaidLeaveAmount = eps.TotalPaidLeaveAmount ?? 0,
                                     TotalOverTimeAmount = eps.TotalOverTimeAmount ?? 0,
                                     TotalOverTimeDays = eps.TotalOverTimeDays ?? 0,
                                     PaidLoanAmount = eps.PaidLoanAmount ?? 0,
                                     PaidDate = eps.PaidDate,
                                     ProfessionalTax = eps.ProfessionalTax ?? 0,
                                 };
                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<EmployeePaidSalarys> SalarySlipReport(Guid p_EmployeeId, string p_Month, int p_Year, DateTime p_FromDate, DateTime p_ToDate)
        {
            Result<EmployeePaidSalarys> _Result = new Result<EmployeePaidSalarys>();

            try
            {
                int WeeklyOff = Convert.ToInt32(AttendanceType.WeeklyOff);
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from eps in dbContext.EmployeePaidSalaries
                                 join e in dbContext.EmployeeMasters on eps.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where eps.EmployeeId == p_EmployeeId && eps.Month == p_Month && eps.Year == p_Year
                                 select new EmployeePaidSalarys
                                 {
                                     EmployeePaidSalaryID = eps.EmployeePaidSalaryID,
                                     Department = d.Department,
                                     FullName = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     PaidBasic = eps.PaidBasic ?? 0,
                                     PaidTotalSalary = eps.PaidTotalSalary ?? 0,
                                     Month = eps.Month,
                                     Year = eps.Year,
                                     TotalOverTimeDays = eps.TotalOverTimeDays ?? 0,
                                     TotalDays = eps.TotalDays,
                                     TotalUseLeave = eps.TotalUseLeave ?? 0,
                                     TotalHolidays = eps.TotalHoliday ?? 0,
                                     TotalPresentDays = eps.TotalPresentDays ?? 0,
                                     TotalPaidLeave = eps.TotalPaidLeave ?? 0,
                                     TotalPaidLeaveAmount = eps.TotalPaidLeaveAmount ?? 0,
                                     TotalOverTimeAmount = eps.TotalOverTimeAmount ?? 0,
                                     PaidLoanAmount = eps.PaidLoanAmount ?? 0,
                                     WeeklyOff = e.EmployeeAttendances.Count() > 0 ? e.EmployeeAttendances.Where(ea => ea.AttendanceDate >= p_FromDate && ea.AttendanceDate <= p_ToDate && ea.AttendanceType == WeeklyOff).Count() : 0,
                                     PaidDate = eps.PaidDate,
                                     IsPaid = eps.IsPaid,
                                     ProfessionalTax = eps.ProfessionalTax ?? 0,
                                 };

                    _Result.Data = _Query.FirstOrDefault();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeeLoans>> GetEmployeePaidLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year)
        {
            Result<List<EmployeeLoans>> _Result = new Result<List<EmployeeLoans>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from epl in dbContext.EmployeePaidLoans
                                 join el in dbContext.EmployeeLoans on epl.EmployeeLoanMapId equals el.EmployeeLoanMapID
                                 where el.IsActive == true && el.EmployeeId == p_EmployeeId && epl.IsActive == true && epl.Month == p_Month && epl.Year == p_Year
                                 select new EmployeeLoans
                                 {
                                     LoanTitle = el.LoanTitle,
                                     PaidLoanAmount = epl.PaidAmount,
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> LeaveReport(List<Guid> p_ListOfEmployeeId, Guid p_FinancialYearId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _FinancialYear = dbContext.FinancialYearMasters.Where(f => f.FinancialYearID == p_FinancialYearId).Select(f => f.Year).FirstOrDefault();

                    int _AttendanceType = Convert.ToInt32(AttendanceType.Leave);

                    var _Query = from e in dbContext.EmployeeMasters
                                 join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                 where e.IsActive == true && e.IsLeave == false && p_ListOfEmployeeId.Contains(e.EmployeeID)
                                 select new EmployeePaidSalarys
                                 {
                                     Department = e.DepartmentMaster.Department,
                                     FullName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     NoOfLeavePerMonth = et.NoOfLeavePerMonth,
                                     JoinDate = e.JoinDate ?? DateTime.Now,
                                     PaidMonthCount = e.EmployeePaidSalaries.Where(eps => eps.FinancialYearId == p_FinancialYearId && eps.IsPaid == true && eps.IsActive == true).Count(),
                                     PaidMonthAllowLeave = e.EmployeePaidSalaries.Where(eps => eps.FinancialYearId == p_FinancialYearId && eps.IsPaid == true && eps.IsActive == true).Sum(eps => eps.AllowLeave) ?? 0,
                                     FinancialYear = _FinancialYear,
                                     TotalUseLeave = e.EmployeeAttendances.Where(ea => ea.IsActive == true && ea.AttendanceType == _AttendanceType && ea.FinancialYearId == p_FinancialYearId).Sum(ea => ea.Attendance) ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> AttendanceReport(List<Guid> p_ListOfEmployeeId, List<int> p_ListOfMonth, Guid p_FinancialYearId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                int Present = Convert.ToInt32(AttendanceType.Present);
                int Holiday = Convert.ToInt32(AttendanceType.Holiday);
                int WeeklyOff = Convert.ToInt32(AttendanceType.WeeklyOff);
                int Leave = Convert.ToInt32(AttendanceType.Leave);

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeAttendances
                                 join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                 join d in dbContext.DepartmentMasters on em.DepartmentId equals d.DepartmentID
                                 group e by new { Month = e.AttendanceDate.Month, Id = e.EmployeeId } into ea
                                 where p_ListOfEmployeeId.Contains(ea.FirstOrDefault().EmployeeId) && p_ListOfMonth.Contains(ea.Key.Month) && ea.FirstOrDefault().IsActive == true && ea.FirstOrDefault().FinancialYearId == p_FinancialYearId
                                 orderby ea.Key.Month
                                 select new EmployeePaidSalarys
                                 {
                                     MonthNo = ea.Key.Month,
                                     FullName = ea.FirstOrDefault().EmployeeMaster.FirstName + " " + ea.FirstOrDefault().EmployeeMaster.LastName,
                                     Department = ea.FirstOrDefault().EmployeeMaster.DepartmentMaster.Department,
                                     Year = ea.FirstOrDefault().AttendanceDate.Year,
                                     WorkingHours = ea.Sum(w => w.WorkingHours ?? 0),
                                     OverTimeHours = ea.Sum(ot => ot.OverTimeHours ?? 0),
                                     TotalDays = ea.Count(),
                                     TotalPresentDays = ea.Where(p => p.AttendanceType == Present).Count(),
                                     TotalHolidays = ea.Where(p => p.AttendanceType == Holiday).Count(),
                                     WeeklyOff = ea.Where(p => p.AttendanceType == WeeklyOff).Count(),
                                     TotalUseLeave = ea.Where(p => p.AttendanceType == Leave).Sum(a => a.Attendance) ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetAttendanceChartByEmployeeID(Guid p_EmployeeId, Guid p_FinancialYearId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                int Present = Convert.ToInt32(AttendanceType.Present);
                int Holiday = Convert.ToInt32(AttendanceType.Holiday);
                int WeeklyOff = Convert.ToInt32(AttendanceType.WeeklyOff);
                int Leave = Convert.ToInt32(AttendanceType.Leave);

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeAttendances
                                 join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                 join d in dbContext.DepartmentMasters on em.DepartmentId equals d.DepartmentID
                                 group e by new { Month = e.AttendanceDate.Month, Id = e.EmployeeId } into ea
                                 where ea.FirstOrDefault().EmployeeId == p_EmployeeId && ea.FirstOrDefault().IsActive == true && ea.FirstOrDefault().FinancialYearId == p_FinancialYearId
                                 orderby ea.Key.Month
                                 select new EmployeePaidSalarys
                                 {
                                     MonthNo = ea.Key.Month,
                                     TotalDays = ea.Count(),
                                     TotalPresentDays = ea.Where(p => p.AttendanceType == Present).Count(),
                                     TotalHolidays = ea.Where(p => p.AttendanceType == Holiday).Count(),
                                     WeeklyOff = ea.Where(p => p.AttendanceType == WeeklyOff).Count(),
                                     TotalUseLeave = ea.Where(p => p.AttendanceType == Leave).Sum(a => a.Attendance) ?? 0,
                                 };

                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<EmployeePaidSalarys>> GetAttendanceChartInfo(Guid p_FinancialYearId)
        {
            Result<List<EmployeePaidSalarys>> _Result = new Result<List<EmployeePaidSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                int Present = Convert.ToInt32(AttendanceType.Present);
                int Holiday = Convert.ToInt32(AttendanceType.Holiday);
                int WeeklyOff = Convert.ToInt32(AttendanceType.WeeklyOff);
                int Leave = Convert.ToInt32(AttendanceType.Leave);

                using (var dbContext = new ERPEntities())
                {

                    //var _Query = from e in dbContext.EmployeeAttendances
                    //             join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID  where em.IsLeave == false
                    //             join d in dbContext.DepartmentMasters on em.DepartmentId equals d.DepartmentID
                    //             group e by new { Month = e.AttendanceDate.Month } into ea
                    //             where ea.FirstOrDefault().IsActive == true && ea.FirstOrDefault().FinancialYearId == p_FinancialYearId
                    //             orderby ea.Key.Month
                    //             select new EmployeePaidSalarys
                    //             {
                    //                 MonthNo = ea.Key.Month,
                    //                 TotalDays = ea.Count(),

                    //                 TotalPresentDays = ea.Where(p => p.AttendanceType == Present).Count(),
                    //                 TotalHolidays = ea.Where(p => p.AttendanceType == Holiday).Count(),
                    //                 WeeklyOff = ea.Where(p => p.AttendanceType == WeeklyOff).Count(),
                    //                 TotalUseLeave = ea.Where(p => p.AttendanceType == Leave).Count(),
                    //                 //TotalUseLeave = ea.Where(p => p.AttendanceType == Leave).Sum(a => a.Attendance) ?? 0,


                    //             };


                    var _Query = from e in dbContext.EmployeeAttendances
                                 join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                 where em.IsLeave == false
                                 group e by new { Month = e.AttendanceDate.Month, Year = e.AttendanceDate.Year } into ea
                                 where ea.FirstOrDefault().IsActive == true && ea.FirstOrDefault().FinancialYearId == p_FinancialYearId

                                 orderby ea.Key.Month
                                 select new EmployeePaidSalarys
                                 {
                                     MonthNo = ea.Key.Month,
                                     Year = ea.Key.Year,
                                     TotalPresentDays = ea.Where(p => p.AttendanceType == Present).Count()
                                 };



                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

    }
}
