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
    public class LookupService : ILookupService
    {
        public Result<List<Item>> GetAllFinancialYear()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from f in dbContext.FinancialYearMasters
                                 where f.IsActive == true
                                 orderby f.Year
                                 select new Item
                                 {
                                     Id = f.FinancialYearID,
                                     Text = f.FinancialYear
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

        public Result<List<Item>> GetAllEmployeeByDepartmentId(Guid p_DepartmentId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 where e.DepartmentId == p_DepartmentId && e.IsActive == true
                                 select new Item
                                 {
                                     Id = e.EmployeeID,
                                     Text = e.FirstName + " " + e.LastName
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

        public Result<List<Item>> GetAllActiveEmployeeByDepartmentId(Guid p_DepartmentId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 where (p_DepartmentId == Guid.Empty ? true : e.DepartmentId == p_DepartmentId) && e.IsActive == true && e.IsLeave == false
                                 select new Item
                                 {
                                     Id = e.EmployeeID,
                                     Text = e.FirstName + " " + e.LastName
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

        public Result<List<Item>> GetAllCountry()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from c in dbContext.CountryMasters
                                 where c.IsActive == true
                                 orderby c.CountryName
                                 select new Item
                                 {
                                     Id = c.CountryID,
                                     Text = c.CountryName
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

        public Result<List<Item>> GetTableCategory(string p_Table)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from s in dbContext.CategoryMasters
                                 where s.CategoryTable == p_Table && s.IsActive == true
                                 orderby s.CategoryName
                                 select new Item
                                 {
                                     Id = s.CategoryId,
                                     Text = s.CategoryName
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
        public Result<List<Item>> GetAllStateByCountryId(Guid p_CountryId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from s in dbContext.StateMasters
                                 where s.CountryId == p_CountryId && s.IsActive == true
                                 orderby s.StateName
                                 select new Item
                                 {
                                     Id = s.StateID,
                                     Text = s.StateName
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

        public Result<List<Item>> GetAllEmployeeType()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeTypeMasters
                                 where e.IsActive == true
                                 orderby e.EmployeeType
                                 select new Item
                                 {
                                     Id = e.EmployeeTypeID,
                                     Text = e.EmployeeType
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

        public Result<List<Item>> GetAllDepartment()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DepartmentMasters
                                 where d.IsActive == true
                                 orderby d.Department
                                 select new Item
                                 {
                                     Id = d.DepartmentID,
                                     Text = d.Department
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

        public Result<List<Item>> GetAllDesignation()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DesignationMasters
                                 where d.IsActive == true
                                 orderby d.Designation
                                 select new Item
                                 {
                                     Id = d.DesignationID,
                                     Text = d.Designation
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

        public Result<List<Item>> GetAllEmployeeGrade()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeGradeMasters
                                 where e.IsActive == true
                                 orderby e.EmployeeGrade
                                 select new Item
                                 {
                                     Id = e.EmployeeGradeID,
                                     Text = e.EmployeeGrade
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

        public Result<List<Item>> GetAllShift()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from s in dbContext.ShiftMasters
                                 where s.IsActive == true
                                 orderby s.Shift
                                 select new Item
                                 {
                                     Id = s.ShiftID,
                                     Text = s.Shift //+ " " + s.FromTime + " - " + s.ToTime,
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

        public Result<List<Item>> GetAllUnAssignSalaryEmployeeByDepartmentId(Guid p_DepartmentId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from e in dbContext.EmployeeMasters
                                  where e.IsActive == true && e.DepartmentId == p_DepartmentId && e.EmployeeSalaries.Count() == 0
                                  select new Item
                                  {
                                      Id = e.EmployeeID,
                                      Text = e.FirstName + " " + e.LastName,
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

        public Result<List<Item>> GetAllLeaveCategory()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from l in dbContext.LeaveCategoryMasters
                                 where l.IsActive == true
                                 orderby l.LeaveCategory
                                 select new Item
                                 {
                                     Id = l.LeaveCategoryID,
                                     Text = l.LeaveCategory,
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

        public Result<List<Dashboard>> GetSalaryChartDetailsByEmployeeId(Guid p_EmployeeId, Guid p_FinancialYearId)
        {
            Result<List<Dashboard>> _Result = new Result<List<Dashboard>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeePaidSalaries
                                 where e.EmployeeId == p_EmployeeId && e.FinancialYearId== p_FinancialYearId && e.IsActive == true orderby e.PaidDate
                                 select new Dashboard
                                 {
                                     Month = e.Month,
                                     PaidTotalSalary= e.PaidTotalSalary ?? 0,
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
        public Result<List<Dashboard>> GetSalaryChartInfoForAllEmployee(Guid p_FinancialYearId)
        {
            Result<List<Dashboard>> _Result = new Result<List<Dashboard>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {

                    //var _Query = (from e in dbContext.EmployeePaidSalaries
                    //              join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                    //              where em.IsLeave == false
                    //              where e.FinancialYearId == p_FinancialYearId && e.IsActive == true
                    //              orderby e.PaidDate
                    //              select new { e.Month, e.PaidTotalSalary }
                    //          ).ToList().GroupBy(x => x.Month).Select(s => new Dashboard() { Month = s.Key, PaidTotalSalary = Convert.ToDecimal(s.Sum(o => o.PaidTotalSalary)) });

                    var _Query = (from e in dbContext.EmployeePaidSalaries
                                  join em in dbContext.EmployeeMasters on e.EmployeeId equals em.EmployeeID
                                  where em.IsLeave == false &&
                                   e.FinancialYearId == p_FinancialYearId && e.IsActive == true
                                  orderby e.PaidDate
                                  select new { e.Month, e.PaidTotalSalary }
                              ).ToList().GroupBy(x => x.Month).Select(s => new Dashboard() { Month = s.Key, PaidTotalSalary = Convert.ToDecimal(s.Sum(o => o.PaidTotalSalary)) });



                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Exception.Message;
                _Result.Exception = Exception;
            }
            return _Result;
        }

        public Result<List<Item>> GetAllActiveEmployee()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 where e.IsActive == true && e.IsLeave == false
                                 select new Item
                                 {
                                     Id = e.EmployeeID,
                                     Text = e.FirstName + " " + e.LastName
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

        public Result<List<Item>> GetAllActiveEmployeeProfile(Guid p_WorkLocationId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 where e.IsActive == true 
                                 select new Item
                                 {
                                     Id = e.EmployeeId,
                                     Text = e.LastName + ", " + e.FirstName + " " + e.MiddleName
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

        public Result<List<Item>> GetEmployeePerWorkLocationId(Guid p_WorkLocationId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 where e.IsActive == true && e.WorkLocationId == p_WorkLocationId
                                 orderby e.LastName ascending
                                 select new Item
                                 {
                                     Id   = e.EmployeeId,
                                     Text = e.LastName + ", " + e.FirstName + " " + e.MiddleName
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

        public Result<List<Item>> GetAllEducation()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.EducationMasters
                                 where d.IsActive == true
                                 orderby d.EducationName
                                 select new Item
                                 {
                                     Id = d.EducationID,
                                     Text = d.EducationName
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

        public Result<List<Item>> GetWorkLocation(Guid p_RelatedId)
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.CompanyMasters
                                 where d.IsActive == true && d.CategoryId == p_RelatedId
                                 orderby d.CompanyName orderby d.CompanyName
                                 select new Item
                                 {
                                     Id   = d.CompanyID,
                                     Text = d.CompanyName
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<Item>> GetCutOffPeriod()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.PayrollCutOffMasters
                                 where d.IsActive == true
                                 orderby d.ActualDate
                                 select new Item
                                 {
                                     Id = d.PayrollCutOffId,
                                     Text = d.CutOffCode
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

        public Result<List<Item>> GetSchedule()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.ShiftMasters
                                 where d.IsActive == true 
                                 select new Item
                                 {
                                     Id   = d.ShiftID,
                                     Text = d.Shift
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

        public Result<List<Item>> GetRoleList()
        {
            Result<List<Item>> _Result = new Result<List<Item>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.RoleMasters
                                 where d.IsActive == true
                                 select new Item
                                 {
                                     Id = d.RoleID,
                                     Text = d.RoleName
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
