using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Implemention
{
    public class EmployeeLeaveCategoryService : IEmployeeLeaveCategoryService
    {
        public Result<List<EmployeeLeaveCategorys>> GetEmployeeLeaveCategoryListByEmployeeId(Guid p_EmployeeId)
        {
            Result<List<EmployeeLeaveCategorys>> _Result = new Result<List<EmployeeLeaveCategorys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from el in dbContext.EmployeeLeaveCategories
                                 join l in dbContext.LeaveCategoryMasters on el.LeaveCategoryId equals l.LeaveCategoryID
                                 where el.IsActive == true && l.IsActive == true && el.EmployeeId == p_EmployeeId
                                 orderby el.ApplyDate descending
                                 select new EmployeeLeaveCategorys
                                 {
                                     EmployeeLeaveCategoryMapID = el.EmployeeLeaveCategoryMapID,
                                     EmployeeId = el.EmployeeId,
                                     LeaveCategoryId = el.LeaveCategoryId,
                                     LeaveCategory = l.LeaveCategory,
                                     StartDate = el.StartDate ?? DateTime.Now,
                                     EndDate = el.EndDate ?? DateTime.Now,
                                     TotalDay = el.TotalDay ?? 0,
                                     Reason = el.Reason,
                                     Comments = el.Comments,
                                     ApplyDate = el.ApplyDate ?? DateTime.Now,
                                     ApproveDate = el.ApproveDate ?? DateTime.Now,
                                     ApprovedBy = el.ApprovedBy,
                                     IsApprove = el.IsApprove ?? false,
                                     Status = ((el.IsApprove ?? false) ? "Approve" : ((el.Comments == "" || el.Comments == null) ? "Pending" : "Dis Approve")),
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

        public Result<List<EmployeeLeaveCategorys>> GetEmployeeLeaveCategoryListByDate(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate)
        {
            Result<List<EmployeeLeaveCategorys>> _Result = new Result<List<EmployeeLeaveCategorys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from el in dbContext.EmployeeLeaveCategories
                                 join l in dbContext.LeaveCategoryMasters on el.LeaveCategoryId equals l.LeaveCategoryID
                                 where el.IsActive == true && el.IsApprove == true && el.EmployeeId == p_EmployeeId && ((el.StartDate >= p_FromDate && el.StartDate <= p_ToDate) || (el.EndDate >= p_FromDate && el.EndDate <= p_ToDate))
                                 select new EmployeeLeaveCategorys
                                 {
                                     StartDate = el.StartDate ?? DateTime.Now,
                                     EndDate = el.EndDate ?? DateTime.Now,
                                     IsFirstHalfDay = el.IsFirstHalfDay,
                                     IsLastHalfDay = el.IsLastHalfDay,
                                     LeaveCategory = l.LeaveCategory,
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

        public Result<Boolean> DeleteEmployeeLeaveCategoryById(Guid p_EmployeeLeaveCategoryId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeLeaveCategory _EmployeeLeaveCategory = dbContext.EmployeeLeaveCategories.Where(l => l.EmployeeLeaveCategoryMapID == p_EmployeeLeaveCategoryId).FirstOrDefault();

                    if (_EmployeeLeaveCategory != null)
                    {
                        _EmployeeLeaveCategory.IsActive = false;
                        _EmployeeLeaveCategory.ModifiedDate = DateTime.Now;
                        _EmployeeLeaveCategory.ModifiedBy = p_UserId;

                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
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

        public Result<EmployeeLeaveCategorys> GetEmployeeLeaveCategoryById(Guid p_EmployeeLeaveCategoryId)
        {
            Result<EmployeeLeaveCategorys> _Result = new Result<EmployeeLeaveCategorys>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from el in dbContext.EmployeeLeaveCategories
                                 join l in dbContext.LeaveCategoryMasters on el.LeaveCategoryId equals l.LeaveCategoryID
                                 join e in dbContext.EmployeeMasters on el.EmployeeId equals e.EmployeeID
                                 where el.EmployeeLeaveCategoryMapID == p_EmployeeLeaveCategoryId
                                 select new EmployeeLeaveCategorys
                                 {
                                     EmployeeLeaveCategoryMapID = el.EmployeeLeaveCategoryMapID,
                                     EmployeeId = el.EmployeeId,
                                     LeaveCategoryId = el.LeaveCategoryId,
                                     LeaveCategory = l.LeaveCategory,
                                     EmployeeName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     StartDate = el.StartDate ?? DateTime.Now,
                                     EndDate = el.EndDate ?? DateTime.Now,
                                     TotalDay = el.TotalDay ?? 0,
                                     IsFirstHalfDay = el.IsFirstHalfDay,
                                     IsLastHalfDay = el.IsLastHalfDay,
                                     ApprovedBy = el.ApprovedBy,
                                     ApplyDate = el.ApplyDate ?? DateTime.Now,
                                     ApproveDate = el.ApproveDate ?? DateTime.Now,
                                     Reason = el.Reason,
                                     Comments = el.Comments,
                                     IsApprove = el.IsApprove ?? false,
                                     Status = ((el.IsApprove ?? false) ? "Approve" : ((el.Comments == "" || el.Comments == null) ? "Pending" : "Dis Approve")),
                                 };

                    EmployeeLeaveCategorys _EmployeeLeaveCategorys = _Query.FirstOrDefault();
                    if (_EmployeeLeaveCategorys != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _EmployeeLeaveCategorys;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
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

        public Result<Boolean> SaveApplyEmployeeLeaveCategory(EmployeeLeaveCategorys p_EmployeeLeaveCategory, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeLeaveCategory _EmployeeLeaveCategory = new EmployeeLeaveCategory();

                    if (p_EmployeeLeaveCategory.EmployeeLeaveCategoryMapID == Guid.Empty)
                    {
                        _EmployeeLeaveCategory.EmployeeLeaveCategoryMapID = Guid.NewGuid();
                        _EmployeeLeaveCategory.IsActive = true;
                        _EmployeeLeaveCategory.CreatedDate = DateTime.Now;
                        _EmployeeLeaveCategory.CreatedBy = p_UserId;
                        _EmployeeLeaveCategory.ModifiedDate = DateTime.Now;
                        _EmployeeLeaveCategory.ApplyDate = DateTime.Now;
                        _EmployeeLeaveCategory.ApproveDate = DateTime.Now;
                        _EmployeeLeaveCategory.IsApprove = false;
                    }
                    else
                    {
                        _EmployeeLeaveCategory = dbContext.EmployeeLeaveCategories.Where(el => el.EmployeeLeaveCategoryMapID == p_EmployeeLeaveCategory.EmployeeLeaveCategoryMapID).FirstOrDefault();

                        _EmployeeLeaveCategory.ModifiedDate = DateTime.Now;
                        _EmployeeLeaveCategory.ModifiedBy = p_UserId;
                    }

                    _EmployeeLeaveCategory.LeaveCategoryId = p_EmployeeLeaveCategory.LeaveCategoryId;
                    _EmployeeLeaveCategory.EmployeeId = p_EmployeeLeaveCategory.EmployeeId;
                    _EmployeeLeaveCategory.StartDate = p_EmployeeLeaveCategory.StartDate;
                    _EmployeeLeaveCategory.EndDate = p_EmployeeLeaveCategory.EndDate;
                    _EmployeeLeaveCategory.IsFirstHalfDay = p_EmployeeLeaveCategory.IsFirstHalfDay;
                    _EmployeeLeaveCategory.IsLastHalfDay = p_EmployeeLeaveCategory.IsLastHalfDay;
                    _EmployeeLeaveCategory.Reason = p_EmployeeLeaveCategory.Reason;
                    _EmployeeLeaveCategory.TotalDay = p_EmployeeLeaveCategory.TotalDay;

                    if (p_EmployeeLeaveCategory.EmployeeLeaveCategoryMapID == Guid.Empty)
                    {
                        dbContext.EmployeeLeaveCategories.Add(_EmployeeLeaveCategory);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EmployeeLeaveCategory.EmployeeLeaveCategoryMapID);
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

        public Result<List<EmployeeLeaveCategorys>> GetEmployeeLeaveCategoryList()
        {
            Result<List<EmployeeLeaveCategorys>> _Result = new Result<List<EmployeeLeaveCategorys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from el in dbContext.EmployeeLeaveCategories
                                 join l in dbContext.LeaveCategoryMasters on el.LeaveCategoryId equals l.LeaveCategoryID
                                 join e in dbContext.EmployeeMasters on el.EmployeeId equals e.EmployeeID
                                 where el.IsActive == true && l.IsActive == true && e.IsActive == true
                                 orderby el.ApplyDate descending
                                 select new EmployeeLeaveCategorys
                                 {
                                     EmployeeLeaveCategoryMapID = el.EmployeeLeaveCategoryMapID,
                                     EmployeeId = el.EmployeeId,
                                     EmployeeName = e.FirstName + " " + e.LastName,
                                     EmployeeNo = e.EmployeeNo,
                                     LeaveCategoryId = el.LeaveCategoryId,
                                     LeaveCategory = l.LeaveCategory,
                                     StartDate = el.StartDate ?? DateTime.Now,
                                     EndDate = el.EndDate ?? DateTime.Now,
                                     TotalDay = el.TotalDay ?? 0,
                                     Reason = el.Reason,
                                     Comments = el.Comments,
                                     ApplyDate = el.ApplyDate ?? DateTime.Now,
                                     ApproveDate = el.ApproveDate ?? DateTime.Now,
                                     ApprovedBy = el.ApprovedBy,
                                     IsApprove = el.IsApprove ?? false,
                                     Status = ((el.IsApprove ?? false) ? "Approve" : ((el.Comments == "" || el.Comments == null) ? "Pending" : "Dis Approve")),
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

        public Result<Boolean> SaveReplyEmployeeLeaveCategory(EmployeeLeaveCategorys p_EmployeeLeaveCategory, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeLeaveCategory _EmployeeLeaveCategory = new EmployeeLeaveCategory();

                    _EmployeeLeaveCategory = dbContext.EmployeeLeaveCategories.Where(el => el.EmployeeLeaveCategoryMapID == p_EmployeeLeaveCategory.EmployeeLeaveCategoryMapID).FirstOrDefault();

                    _EmployeeLeaveCategory.ModifiedDate = DateTime.Now;
                    _EmployeeLeaveCategory.ModifiedBy = p_UserId;
                    _EmployeeLeaveCategory.IsApprove = p_EmployeeLeaveCategory.IsApprove;
                    _EmployeeLeaveCategory.Comments = p_EmployeeLeaveCategory.Comments;
                    _EmployeeLeaveCategory.ApproveDate = DateTime.Now;
                    _EmployeeLeaveCategory.ApprovedBy = p_EmployeeLeaveCategory.ApprovedBy;

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EmployeeLeaveCategory.EmployeeMaster.Email);
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

        public Result<Dashboard> GetTotalEmployeeLeavesByEmployeeId(Guid p_EmployeeId, Guid p_FinancialYearId)
        {
            Result<Dashboard> _Result = new Result<Dashboard>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    Dashboard _Dashboard = new Dashboard();

                    int _FinancialYear = dbContext.FinancialYearMasters.Where(f => f.FinancialYearID == p_FinancialYearId).Select(f => f.Year).FirstOrDefault();
                    DateTime _FinancialStartDate = new DateTime(_FinancialYear, 04, 01);

                    var _TotalLeavesOfPaidMonth = from eps in dbContext.EmployeePaidSalaries
                                                  where eps.EmployeeId == p_EmployeeId && eps.FinancialYearId == p_FinancialYearId && eps.IsPaid == true && eps.IsActive == true
                                                  group eps by eps.EmployeeId into c
                                                  select new
                                                  {
                                                      count = c.Count(),
                                                      sum = c.Sum(eps => eps.AllowLeave) ?? 0
                                                  };

                    var _Employee = from e in dbContext.EmployeeMasters
                                    join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                    where e.EmployeeID == p_EmployeeId
                                    select new
                                    {
                                        NoOfLeavePerMonth = et.NoOfLeavePerMonth,
                                        JoinDate = e.JoinDate
                                    };

                    double _TotalDays = ((_Employee.FirstOrDefault().JoinDate ?? DateTime.Now) - _FinancialStartDate).TotalDays;
                    int _LeftMonth = 0;

                    if (_TotalDays > 0)
                    {
                        _LeftMonth = Convert.ToInt32(Math.Round(_TotalDays / 30, 0));

                        double _DecimalPoint = (_TotalDays / 30) - _LeftMonth;

                        if (_DecimalPoint > 0.5)
                        {
                            _LeftMonth = _LeftMonth - 1;
                        }
                    }

                    if (_TotalLeavesOfPaidMonth.Count() > 0)
                    {
                        decimal _SumOfLeavesOfPaidMonth = Convert.ToDecimal(_TotalLeavesOfPaidMonth.FirstOrDefault().sum);
                        decimal _PaidMonthCount = _TotalLeavesOfPaidMonth.FirstOrDefault().count;
                        _Dashboard.EmployeeLeave = _SumOfLeavesOfPaidMonth + (_Employee.FirstOrDefault().NoOfLeavePerMonth * (12 - (_PaidMonthCount + _LeftMonth)));
                    }
                    else
                    {
                        _Dashboard.EmployeeLeave = _Employee.FirstOrDefault().NoOfLeavePerMonth * (12 - _LeftMonth);
                    }

                    _Dashboard.NoOfLeavesPerMonth = _Employee.FirstOrDefault().NoOfLeavePerMonth;

                    int _AttendanceType = Convert.ToInt32(AttendanceType.Leave);
                    _Dashboard.UsedEmployeeLeave = dbContext.EmployeeAttendances.Where(ea => ea.EmployeeId == p_EmployeeId && ea.FinancialYearId == p_FinancialYearId && ea.IsActive == true && ea.AttendanceType == _AttendanceType).Sum(ea => ea.Attendance) ?? 0;

                    _Result.IsSuccess = true;
                    _Result.Data = _Dashboard;
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

        public Result<int> GetLeaveEmployeeCount()
        {
            Result<int> _Result = new Result<int>();
            DateTime _TodayDate = new DateTime().Date;
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _LeaveCount = dbContext.EmployeeLeaveCategories.Where(x => x.IsActive == true && x.IsApprove == true && x.StartDate.Value.Date >= _TodayDate && x.EndDate.Value.Date <= _TodayDate).ToList().Count() ;
                    _Result.Data = _LeaveCount;
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

    }
}
