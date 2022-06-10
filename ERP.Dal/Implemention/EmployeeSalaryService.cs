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
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        public Result<List<EmployeeSalarys>> GetEmployeeSalaryList()
        {
            Result<List<EmployeeSalarys>> _Result = new Result<List<EmployeeSalarys>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from es in dbContext.EmployeeSalaries
                                  join e in dbContext.EmployeeMasters on es.EmployeeId equals e.EmployeeID
                                  where e.IsActive == true && es.IsActive == true
                                  select new EmployeeSalarys
                                  {
                                      EmployeeSalaryID = es.EmployeeSalaryID,
                                      FullName = e.FirstName + " " + e.LastName,
                                      EmployeeNo = e.EmployeeNo,
                                      JoinDate = e.JoinDate ?? DateTime.Now,
                                      Basic = es.Basic ?? 0,
                                      TotalEarning = es.TotalEarning ?? 0,
                                      TotalDeduction = es.TotalDeduction ?? 0,
                                      TotalSalary = es.TotalSalary ?? 0,
                                      IsLeave = e.IsLeave,
                                  });

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

        public Result<List<Allowance>> GetEmployeeAllowanceByEmployeeId(Guid p_EmployeeId)
        {
            Result<List<Allowance>> _Result = new Result<List<Allowance>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from a in dbContext.AllowanceMasters
                                 join ea in dbContext.EmployeeAllowanceMaps.Where(ae => ae.EmployeeId == p_EmployeeId) on a.AllowanceID equals ea.AllowanceId into eam
                                 from x in eam.DefaultIfEmpty()
                                 where a.IsActive == true
                                 orderby a.SortNo
                                 select new Allowance
                                 {
                                     AllowanceID = a.AllowanceID,
                                     AllowanceName = a.Allowance,
                                     IsConsider = a.IsConsider,
                                     Amount = (x == null ? null : x.Amount),
                                     Percentage = a.Percentage ?? 0,
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

        public Result<List<Deduction>> GetEmployeeDeductionByEmployeeId(Guid p_EmployeeId)
        {
            Result<List<Deduction>> _Result = new Result<List<Deduction>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DeductionMasters
                                 join ed in dbContext.EmployeeDeductionMaps.Where(ae => ae.EmployeeId == p_EmployeeId) on d.DeductionID equals ed.DeductionId into edm
                                 from x in edm.DefaultIfEmpty()
                                 where d.IsActive == true
                                 orderby d.SortNo
                                 select new Deduction
                                 {
                                     DeductionID = d.DeductionID,
                                     DeductionName = d.Deduction,
                                     IsConsider = d.IsConsider,
                                     Amount = (x == null ? null : x.Amount)
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

        public Result<EmployeeSalarys> GetEmployeeSalaryById(Guid p_EmployeeSalaryId)
        {
            Result<EmployeeSalarys> _Result = new Result<EmployeeSalarys>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from es in dbContext.EmployeeSalaries
                                 join e in dbContext.EmployeeMasters on es.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where es.EmployeeSalaryID == p_EmployeeSalaryId
                                 select new EmployeeSalarys
                                 {
                                     EmployeeSalaryID = es.EmployeeSalaryID,
                                     EmployeeId = e.EmployeeID,
                                     FullName = e.FirstName + " " + e.LastName,
                                     Department = d.Department,
                                     Basic = es.Basic ?? 0,
                                     TotalEarning = es.TotalEarning ?? 0,
                                     TotalDeduction = es.TotalDeduction ?? 0,
                                     TotalSalary = es.TotalSalary ?? 0,
                                     IsMonthlySalary = es.SalaryType ?? 0,
                                 };

                    EmployeeSalarys _EmployeeSalarys = _Query.FirstOrDefault();
                    if (_EmployeeSalarys != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _EmployeeSalarys;
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

        public Result<EmployeeSalarys> GetEmployeeSalaryByEmployeeId(Guid p_EmployeeId)
        {
            Result<EmployeeSalarys> _Result = new Result<EmployeeSalarys>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from es in dbContext.EmployeeSalaries
                                 join e in dbContext.EmployeeMasters on es.EmployeeId equals e.EmployeeID
                                 join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                 where e.EmployeeID == p_EmployeeId
                                 select new EmployeeSalarys
                                 {
                                     EmployeeSalaryID = es.EmployeeSalaryID,
                                     EmployeeId = e.EmployeeID,
                                     EmployeeNo = e.EmployeeNo,
                                     JoinDate = e.JoinDate ?? DateTime.Now,
                                     FullName = e.FirstName + " " + e.LastName,
                                     Department = d.Department,
                                     Basic = es.Basic ?? 0,
                                     TotalEarning = es.TotalEarning ?? 0,
                                     TotalDeduction = es.TotalDeduction ?? 0,
                                     TotalSalary = es.TotalSalary ?? 0,
                                 };

                    EmployeeSalarys _EmployeeSalarys = _Query.FirstOrDefault();
                    if (_EmployeeSalarys != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _EmployeeSalarys;
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

        public Result<Boolean> SaveEmployeeSalary(EmployeeSalarys p_EmployeeSalary, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeSalary _EmployeeSalary = new EmployeeSalary();

                    if (p_EmployeeSalary.EmployeeSalaryID == Guid.Empty)
                    {
                        _EmployeeSalary.EmployeeSalaryID = Guid.NewGuid();
                        _EmployeeSalary.IsActive = true;
                        _EmployeeSalary.CreatedDate = DateTime.Now;
                        _EmployeeSalary.CreatedBy = p_UserId;
                        _EmployeeSalary.ModifiedDate = DateTime.Now;
                        _EmployeeSalary.EmployeeId = p_EmployeeSalary.EmployeeId;
                    }
                    else
                    {
                        _EmployeeSalary = dbContext.EmployeeSalaries.Where(es => es.EmployeeSalaryID == p_EmployeeSalary.EmployeeSalaryID).FirstOrDefault();

                        _EmployeeSalary.ModifiedDate = DateTime.Now;
                        _EmployeeSalary.ModifiedBy = p_UserId;
                    }

                    _EmployeeSalary.Basic = p_EmployeeSalary.Basic;
                    _EmployeeSalary.TotalEarning = p_EmployeeSalary.TotalEarning;
                    _EmployeeSalary.TotalDeduction = p_EmployeeSalary.TotalDeduction;
                    _EmployeeSalary.TotalSalary = p_EmployeeSalary.TotalSalary;
                    _EmployeeSalary.SalaryType = p_EmployeeSalary.IsMonthlySalary;

                    if (p_EmployeeSalary.EmployeeSalaryID == Guid.Empty)
                    {
                        dbContext.EmployeeSalaries.Add(_EmployeeSalary);
                    }

                    dbContext.SaveChanges();

                    if (p_EmployeeSalary.EmployeeSalaryID != Guid.Empty)
                    {
                        dbContext.EmployeeAllowanceMaps.RemoveRange(dbContext.EmployeeAllowanceMaps.Where(ea => ea.EmployeeId == _EmployeeSalary.EmployeeId));
                        dbContext.EmployeeDeductionMaps.RemoveRange(dbContext.EmployeeDeductionMaps.Where(ea => ea.EmployeeId == _EmployeeSalary.EmployeeId));
                    }

                    foreach (Allowance _Allowance in p_EmployeeSalary.ListAllowance)
                    {
                        EmployeeAllowanceMap _EmployeeAllowanceMap = new EmployeeAllowanceMap();

                        _EmployeeAllowanceMap.EmployeeAllowanceMapID = Guid.NewGuid();
                        _EmployeeAllowanceMap.EmployeeId = _EmployeeSalary.EmployeeId;
                        _EmployeeAllowanceMap.AllowanceId = _Allowance.AllowanceID;
                        _EmployeeAllowanceMap.Amount = _Allowance.Amount;
                        _EmployeeAllowanceMap.IsActive = true;
                        _EmployeeAllowanceMap.CreatedDate = DateTime.Now;
                        _EmployeeAllowanceMap.CreatedBy = p_UserId;
                        _EmployeeAllowanceMap.ModifiedDate = DateTime.Now;

                        dbContext.EmployeeAllowanceMaps.Add(_EmployeeAllowanceMap);
                    }

                    foreach (Deduction _Deduction in p_EmployeeSalary.ListDeduction)
                    {
                        EmployeeDeductionMap _EmployeeDeductionMap = new EmployeeDeductionMap();

                        _EmployeeDeductionMap.EmployeeDeductionMapID = Guid.NewGuid();
                        _EmployeeDeductionMap.EmployeeId = _EmployeeSalary.EmployeeId;
                        _EmployeeDeductionMap.DeductionId = _Deduction.DeductionID;
                        _EmployeeDeductionMap.Amount = _Deduction.Amount;
                        _EmployeeDeductionMap.IsActive = true;
                        _EmployeeDeductionMap.CreatedDate = DateTime.Now;
                        _EmployeeDeductionMap.CreatedBy = p_UserId;
                        _EmployeeDeductionMap.ModifiedDate = DateTime.Now;

                        dbContext.EmployeeDeductionMaps.Add(_EmployeeDeductionMap);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EmployeeSalary.EmployeeSalaryID);
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
    }
}
