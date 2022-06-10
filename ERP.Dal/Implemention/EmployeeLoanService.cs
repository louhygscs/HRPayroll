using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Common;
using ERP.Dal.Interface;

namespace ERP.Dal.Implemention
{
    public class EmployeeLoanService : IEmployeeLoanService
    {
        public Result<List<EmployeeLoans>> GetEmployeeLoanList()
        {
            Result<List<EmployeeLoans>> _Result = new Result<List<EmployeeLoans>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeLoans
                                 join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                 where e.IsActive == true && em.IsActive == true
                                 select new EmployeeLoans
                                 {
                                     EmployeeLoanID = e.EmployeeLoanMapID,
                                     Amount = e.Amount,
                                     EmployeeName = em.FirstName + " " + em.LastName,
                                     LoanTitle = e.LoanTitle,
                                     LoanDate = e.LoanDate,
                                     Description = e.Description,
                                     ApprovedBy = e.ApprovedBy,
                                     TotalMonths = e.TotalMonths ?? 0,
                                     IsComplete = e.IsComplete,
                                     PaidLoan = e.EmployeePaidLoans.Where(p => p.IsActive == true).Sum(p =>(decimal?)p.PaidAmount)??0,
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

        public Result<Boolean> DeleteEmployeeLoanById(Guid p_EmployeeLoanId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeePaidLoans.Where(e => e.EmployeeLoanMapId == p_EmployeeLoanId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        EmployeeLoan _EmployeeLoan = dbContext.EmployeeLoans.Where(e => e.EmployeeLoanMapID == p_EmployeeLoanId).FirstOrDefault();

                        if (_EmployeeLoan != null)
                        {
                            _EmployeeLoan.IsActive = false;
                            _EmployeeLoan.ModifiedDate = DateTime.Now;
                            _EmployeeLoan.ModifiedBy = p_UserId;

                            dbContext.SaveChanges();
                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
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

        public Result<EmployeeLoans> GetEmployeeLoanById(Guid p_EmployeeLoanId)
        {
            Result<EmployeeLoans> _Result = new Result<EmployeeLoans>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeLoans
                                 join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                 where e.EmployeeLoanMapID == p_EmployeeLoanId
                                 select new EmployeeLoans
                                 {
                                     EmployeeLoanID = e.EmployeeLoanMapID,
                                     Amount = e.Amount,
                                     ApprovedBy = e.ApprovedBy,
                                     Description = e.Description,
                                     EmployeeId = em.EmployeeID,
                                     LoanDate = e.LoanDate,
                                     LoanTitle = e.LoanTitle,
                                     TotalMonths = e.TotalMonths ?? 0,
                                     DepartmentId = em.DepartmentId,
                                 };

                    EmployeeLoans _EmployeeLoans = _Query.FirstOrDefault();
                    if (_EmployeeLoans != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _EmployeeLoans;
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

        public Result<Boolean> SaveEmployeeLoan(EmployeeLoans p_EmployeeLoans, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {

                    EmployeeLoan _EmployeeLoan = new EmployeeLoan();

                    if (p_EmployeeLoans.EmployeeLoanID == Guid.Empty)
                    {
                        _EmployeeLoan.EmployeeLoanMapID = Guid.NewGuid();
                        _EmployeeLoan.IsActive = true;
                        _EmployeeLoan.CreatedDate = DateTime.Now;
                        _EmployeeLoan.CreatedBy = p_UserId;
                        _EmployeeLoan.ModifiedDate = DateTime.Now;
                        _EmployeeLoan.IsComplete = false;
                    }
                    else
                    {
                        _EmployeeLoan = dbContext.EmployeeLoans.Where(e => e.EmployeeLoanMapID == p_EmployeeLoans.EmployeeLoanID).FirstOrDefault();

                        _EmployeeLoan.ModifiedDate = DateTime.Now;
                        _EmployeeLoan.ModifiedBy = p_UserId;
                    }

                    _EmployeeLoan.EmployeeId = p_EmployeeLoans.EmployeeId;
                    _EmployeeLoan.Amount = p_EmployeeLoans.Amount;
                    _EmployeeLoan.LoanDate = p_EmployeeLoans.LoanDate;
                    _EmployeeLoan.LoanTitle = p_EmployeeLoans.LoanTitle;
                    _EmployeeLoan.Description = p_EmployeeLoans.Description;
                    _EmployeeLoan.ApprovedBy = p_EmployeeLoans.ApprovedBy;
                    _EmployeeLoan.TotalMonths = p_EmployeeLoans.TotalMonths;

                    if (p_EmployeeLoans.EmployeeLoanID == Guid.Empty)
                    {
                        dbContext.EmployeeLoans.Add(_EmployeeLoan);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EmployeeLoan.EmployeeLoanMapID);
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

        public Result<List<EmployeeLoans>> LoanReport(List<Guid> p_ListOfEmployeeId, bool? p_Status)
        {
            Result<List<EmployeeLoans>> _Result = new Result<List<EmployeeLoans>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeLoans
                                 join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                 join d in dbContext.DepartmentMasters on em.DepartmentId equals d.DepartmentID
                                 where e.IsActive == true && (p_Status == null? true : e.IsComplete == p_Status) && p_ListOfEmployeeId.Contains(e.EmployeeId)
                                 select new EmployeeLoans
                                 {
                                     Amount = e.Amount,
                                     EmployeeName = em.FirstName + " " + em.LastName,
                                     Department = d.Department,
                                     LoanTitle = e.LoanTitle,
                                     LoanDate = e.LoanDate,
                                     TotalMonths = e.TotalMonths ?? 0,
                                     PaidLoan = e.EmployeePaidLoans.Where(p => p.IsActive == true).Sum(p => (decimal?)p.PaidAmount) ?? 0,
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
