using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Dal.Implemention
{
    public class EmployeeService : IEmployeeService
    {
        public Result<List<Employee>> GetAllEmployeeList()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {

                    var _Query = (from e in dbContext.EmployeeMasters
                                  select new Employee
                                  {

                                      EmployeeID = e.EmployeeID,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      EmployeeNo = e.EmployeeNo,
                                      IsLeave = e.IsLeave,

                                  });

                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _Result.IsSuccess = false;
                _Result.Message = ex.Message;
                _Result.Exception = ex;
            }
            return _Result;
        }

        public Result<List<Employee>> GetAllIsActiveEmployeeList()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {

                    var _Query = (from e in dbContext.EmployeeMasters
                                  where e.IsActive == true && e.IsLeave == false
                                  select new Employee
                                  {

                                      EmployeeID = e.EmployeeID,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      EmployeeNo = e.EmployeeNo,
                                      IsLeave = e.IsLeave,

                                  });

                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _Result.IsSuccess = false;
                _Result.Message = ex.Message;
                _Result.Exception = ex;
            }
            return _Result;
        }

        public Result<List<Employee>> GetAllIsActiveFromEmployeeProfileList()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {

                    var _Query = (from e in dbContext.EmployeeProfiles
                                  where e.IsActive == true 
                                  select new Employee
                                  {
                                      EmployeeID = e.EmployeeId,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                  });

                    _Result.Data = _Query.ToList();
                }
                _Result.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _Result.IsSuccess = false;
                _Result.Message = ex.Message;
                _Result.Exception = ex;
            }
            return _Result;
        }

        public Result<List<Employee>> GetEmployeeList()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from e in dbContext.EmployeeMasters
                                  join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                  join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                  join dg in dbContext.DesignationMasters on e.DesignationId equals dg.DesignationID
                                  join g in dbContext.EmployeeGradeMasters on e.EmployeeGradeId equals g.EmployeeGradeID
                                  join s in dbContext.ShiftMasters on e.ShiftId equals s.ShiftID
                                  where e.IsActive == true && et.IsActive == true && d.IsActive == true && dg.IsActive == true
                                  select new Employee
                                  {
                                      EmployeeID = e.EmployeeID,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      EmployeeNo = e.EmployeeNo,
                                      EmployeeType = et.EmployeeType,
                                      Department = d.Department,
                                      Designation = dg.Designation,
                                      IsLeave = e.IsLeave,
                                      EmployeeGrade = g.EmployeeGrade,
                                      Shift = s.Shift,
                                      FromTime = s.FromTime,
                                      ToTime = s.ToTime,
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

        public Result<Boolean> DeleteEmployeeById(Guid p_EmployeeId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    //int _Count = dbContext.EmployeeMasters.Where(e => e.DepartmentId == p_DepartmentId && e.IsActive == true).Count();

                    //if (_Count <= 0)
                    //{
                    EmployeeMaster _EmployeeMaster = dbContext.EmployeeMasters.Where(e => e.EmployeeID == p_EmployeeId).FirstOrDefault();

                    if (_EmployeeMaster != null)
                    {
                        _EmployeeMaster.IsActive = false;
                        _EmployeeMaster.ModifiedDate = DateTime.Now;
                        _EmployeeMaster.ModifiedBy = p_UserId;

                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                    //}
                    //else
                    //{
                    //    _Result.Message = GlobalMsg.ReferenceExistMsg;
                    //}
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

        public Result<Boolean> DeletePermenentEmployeeById(Guid p_EmployeeId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    List<EmployeeAllowanceMap> _EmployeeAllowanceMap = dbContext.EmployeeAllowanceMaps.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeAttachment> _EmployeeAttachment = dbContext.EmployeeAttachments.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeAttendance> _EmployeeAttendance = dbContext.EmployeeAttendances.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeDeductionMap> _EmployeeDeductionMap = dbContext.EmployeeDeductionMaps.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeLeaveCategory> _EmployeeLeaveCategory = dbContext.EmployeeLeaveCategories.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeLoan> _EmployeeLoan = dbContext.EmployeeLoans.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeePaidSalary> _EmployeePaidSalary = dbContext.EmployeePaidSalaries.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeSalary> _EmployeeSalary = dbContext.EmployeeSalaries.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<EmployeeWorkingDay> _EmployeeWorkingDay = dbContext.EmployeeWorkingDays.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    List<UserMaster> _UserMaster = dbContext.UserMasters.Where(e => e.EmployeeId == p_EmployeeId).ToList();
                    EmployeeMaster _EmployeeMaster = dbContext.EmployeeMasters.Where(e => e.EmployeeID == p_EmployeeId).FirstOrDefault();


                    if (_EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null
                        && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null
                        && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null && _EmployeeAllowanceMap != null)
                    {

                        if (_EmployeeAllowanceMap != null && _EmployeeAllowanceMap.Count() > 0)
                        {
                            foreach (var item in _EmployeeAllowanceMap)
                            {
                                dbContext.EmployeeAllowanceMaps.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;

                            }
                        }

                        if (_EmployeeAttachment != null && _EmployeeAttachment.Count() > 0)
                        {
                            foreach (var item in _EmployeeAttachment)
                            {
                                dbContext.EmployeeAttachments.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeAttendance != null && _EmployeeAttendance.Count() > 0)
                        {
                            foreach (var item in _EmployeeAttendance)
                            {
                                dbContext.EmployeeAttendances.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeDeductionMap != null && _EmployeeDeductionMap.Count() > 0)
                        {
                            foreach (var item in _EmployeeDeductionMap)
                            {
                                dbContext.EmployeeDeductionMaps.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeLeaveCategory != null && _EmployeeLeaveCategory.Count() > 0)
                        {
                            foreach (var item in _EmployeeLeaveCategory)
                            {
                                dbContext.EmployeeLeaveCategories.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeLoan != null && _EmployeeLoan.Count() > 0)
                        {
                            foreach (var item in _EmployeeLoan)
                            {
                                dbContext.EmployeeLoans.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeePaidSalary != null && _EmployeePaidSalary.Count() > 0)
                        {
                            foreach (var item in _EmployeePaidSalary)
                            {
                                dbContext.EmployeePaidSalaries.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeSalary != null && _EmployeeSalary.Count() > 0)
                        {
                            foreach (var item in _EmployeeSalary)
                            {
                                dbContext.EmployeeSalaries.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeWorkingDay != null && _EmployeeWorkingDay.Count() > 0)
                        {
                            foreach (var item in _EmployeeWorkingDay)
                            {
                                dbContext.EmployeeWorkingDays.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_UserMaster != null && _UserMaster != null)
                        {
                            foreach (var item in _UserMaster)
                            {
                                dbContext.UserMasters.Remove(item);
                                dbContext.SaveChanges();
                                _Result.IsSuccess = true;
                            }
                        }

                        if (_EmployeeMaster != null)
                        {

                            dbContext.EmployeeMasters.Remove(_EmployeeMaster);
                            dbContext.SaveChanges();
                            _Result.IsSuccess = true;
                        }
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

        public Result<int> GetMaxEmployeeNo()
        {
            Result<int> _Result = new Result<int>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    int? _No = dbContext.EmployeeMasters.Max(e => (int?)e.EmployeeNo);
                    _Result.Data = (_No ?? 0) + 1;
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

        public Result<Employee> GetEmployeeById(Guid p_EmployeeId)
        {
            Result<Employee> _Result = new Result<Employee>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 join s in dbContext.StateMasters on e.StateId equals s.StateID
                                 join c in dbContext.CountryMasters on e.CountryId equals c.CountryID
                                 where e.EmployeeID == p_EmployeeId
                                 select new Employee
                                 {
                                     EmployeeID = e.EmployeeID,
                                     EmployeeTypeId = e.EmployeeTypeId,
                                     EmployeeType = e.EmployeeTypeMaster.EmployeeType,
                                     EmployeeGradeId = e.EmployeeGradeId ?? Guid.Empty,
                                     EmployeeGrade = e.EmployeeGradeMaster != null ? e.EmployeeGradeMaster.EmployeeGrade : string.Empty,
                                     DepartmentId = e.DepartmentId,
                                     Department = e.DepartmentMaster.Department,
                                     DesignationId = e.DesignationId,
                                     Designation = e.DesignationMaster.Designation,
                                     ShiftId = e.ShiftId,
                                     Shift = e.ShiftMaster.Shift + " " + e.ShiftMaster.FromTime + " - " + e.ShiftMaster.ToTime,
                                     FromTime = e.ShiftMaster.FromTime,
                                     ToTime = e.ShiftMaster.ToTime,
                                     CountryId = e.CountryId ?? Guid.Empty,
                                     StateId = e.StateId ?? Guid.Empty,
                                     State = s.StateName,
                                     Country = c.CountryName,
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     BirthDate = e.BirthDate ?? DateTime.Now,
                                     IsGender = e.Gender ?? true,
                                     MaratialStatus = e.MaratialStatus,
                                     City = e.City,
                                     Address = e.Address,
                                     PinCode = e.PinCode,
                                     MobileNo = e.MobileNo,
                                     PhoneNo = e.PhoneNo,
                                     JoinDate = e.JoinDate ?? DateTime.Now,
                                     EmployeeNo = e.EmployeeNo,
                                     Email = e.Email,
                                     BankName = e.BankName,
                                     BranchName = e.BranchName,
                                     AccountName = e.AccountName,
                                     AccountNo = e.AccountNo,
                                     PhotoName = e.PhotoName,
                                     LeaveDescription = e.LeaveDescription,
                                     OverTimeAmount = e.OverTimeAmount ?? 0,
                                     IsLeave = e.IsLeave,
                                     LeaveDate = e.LeaveDate ?? DateTime.Now,
                                     WorkingDays = e.EmployeeWorkingDays.Select(ew => ew.DayName).ToList(),
                                     EmployeeAttachments = (from ea in e.EmployeeAttachments
                                                            select new EmployeeAttachments { AttachmentName = ea.AttachmentName, Name = ea.Name }).ToList()
                                 };

                    Employee _Employee = _Query.FirstOrDefault();

                    if (_Employee != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Employee;
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

        public Result<Boolean> SaveEmployee(Employee p_Employee, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMasterExist = dbContext.EmployeeMasters.Where(e => e.EmployeeID != p_Employee.EmployeeID && e.Email == p_Employee.Email).FirstOrDefault();

                    if (_EmployeeMasterExist == null)
                    {
                        EmployeeMaster _EmployeeMaster = new EmployeeMaster();

                        if (p_Employee.EmployeeID == Guid.Empty)
                        {
                            _EmployeeMaster.EmployeeID = Guid.NewGuid();
                            _EmployeeMaster.IsActive = true;
                            _EmployeeMaster.CreatedDate = DateTime.Now;
                            _EmployeeMaster.CreatedBy = p_UserId;
                            _EmployeeMaster.ModifiedDate = DateTime.Now;
                            _EmployeeMaster.IsLeave = false;
                            _EmployeeMaster.LeaveDate = DateTime.Now;
                            _EmployeeMaster.EmployeeNo = p_Employee.EmployeeNo;
                        }
                        else
                        {
                            _EmployeeMaster = dbContext.EmployeeMasters.Where(e => e.EmployeeID == p_Employee.EmployeeID).FirstOrDefault();

                            _EmployeeMaster.ModifiedDate = DateTime.Now;
                            _EmployeeMaster.ModifiedBy = p_UserId;
                        }

                        _EmployeeMaster.EmployeeTypeId = p_Employee.EmployeeTypeId;
                        _EmployeeMaster.EmployeeGradeId = p_Employee.EmployeeGradeId;
                        _EmployeeMaster.DepartmentId = p_Employee.DepartmentId;
                        _EmployeeMaster.DesignationId = p_Employee.DesignationId;
                        _EmployeeMaster.ShiftId = p_Employee.ShiftId;
                        _EmployeeMaster.CountryId = p_Employee.CountryId;
                        _EmployeeMaster.StateId = p_Employee.StateId;
                        _EmployeeMaster.FirstName = p_Employee.FirstName;
                        _EmployeeMaster.MiddleName = p_Employee.MiddleName;
                        _EmployeeMaster.LastName = p_Employee.LastName;
                        _EmployeeMaster.BirthDate = p_Employee.BirthDate;
                        _EmployeeMaster.Gender = p_Employee.IsGender;
                        _EmployeeMaster.MaratialStatus = p_Employee.MaratialStatus;
                        _EmployeeMaster.City = p_Employee.City;
                        _EmployeeMaster.Address = p_Employee.Address;
                        _EmployeeMaster.PinCode = p_Employee.PinCode;
                        _EmployeeMaster.MobileNo = p_Employee.MobileNo;
                        _EmployeeMaster.PhoneNo = p_Employee.PhoneNo;
                        _EmployeeMaster.JoinDate = p_Employee.JoinDate;
                        _EmployeeMaster.Email = p_Employee.Email;
                        _EmployeeMaster.BankName = p_Employee.BankName;
                        _EmployeeMaster.BranchName = p_Employee.BranchName;
                        _EmployeeMaster.AccountName = p_Employee.AccountName;
                        _EmployeeMaster.AccountNo = p_Employee.AccountNo;
                        _EmployeeMaster.OverTimeAmount = p_Employee.OverTimeAmount;

                        if (!string.IsNullOrEmpty(p_Employee.PhotoName))
                        {
                            _EmployeeMaster.PhotoName = p_Employee.PhotoName;
                        }

                        if (p_Employee.EmployeeID == Guid.Empty)
                        {
                            dbContext.EmployeeMasters.Add(_EmployeeMaster);
                        }

                        dbContext.SaveChanges();

                        if (p_Employee.EmployeeID != Guid.Empty)
                        {
                            dbContext.EmployeeWorkingDays.RemoveRange(dbContext.EmployeeWorkingDays.Where(ew => ew.EmployeeId == _EmployeeMaster.EmployeeID));
                        }

                        foreach (String _WorkingDay in p_Employee.WorkingDays)
                        {
                            EmployeeWorkingDay _EmployeeWorkingDay = new EmployeeWorkingDay();

                            _EmployeeWorkingDay.EmployeeWorkingDayMapID = Guid.NewGuid();
                            _EmployeeWorkingDay.EmployeeId = _EmployeeMaster.EmployeeID;
                            _EmployeeWorkingDay.DayName = _WorkingDay;
                            _EmployeeWorkingDay.IsActive = true;
                            _EmployeeWorkingDay.CreatedDate = DateTime.Now;
                            _EmployeeWorkingDay.CreatedBy = p_UserId;
                            _EmployeeWorkingDay.ModifiedDate = DateTime.Now;

                            dbContext.EmployeeWorkingDays.Add(_EmployeeWorkingDay);
                        }

                        foreach (EmployeeAttachments _EmployeeAttachments in p_Employee.EmployeeAttachments)
                        {
                            if (_EmployeeAttachments.IsDelete)
                            {
                                dbContext.EmployeeAttachments.Remove(dbContext.EmployeeAttachments.Where(ea => ea.Name == _EmployeeAttachments.Name).FirstOrDefault());
                            }
                            else
                            {
                                EmployeeAttachment _EmployeeAttachment = new EmployeeAttachment();

                                _EmployeeAttachment = dbContext.EmployeeAttachments.Where(e => e.EmployeeId == _EmployeeMaster.EmployeeID && e.AttachmentName == _EmployeeAttachments.AttachmentName).FirstOrDefault();

                                if (_EmployeeAttachment != null)
                                {
                                    _EmployeeAttachment.ModifiedDate = DateTime.Now;
                                    _EmployeeAttachment.ModifiedBy = p_UserId;
                                    _EmployeeAttachment.Name = _EmployeeAttachments.Name;
                                }
                                else
                                {
                                    _EmployeeAttachment = new EmployeeAttachment();

                                    _EmployeeAttachment.EmployeeAttachmentMapID = Guid.NewGuid();
                                    _EmployeeAttachment.IsActive = true;
                                    _EmployeeAttachment.CreatedDate = DateTime.Now;
                                    _EmployeeAttachment.CreatedBy = p_UserId;
                                    _EmployeeAttachment.ModifiedDate = DateTime.Now;
                                    _EmployeeAttachment.EmployeeId = _EmployeeMaster.EmployeeID;
                                    _EmployeeAttachment.Name = _EmployeeAttachments.Name;
                                    _EmployeeAttachment.AttachmentName = _EmployeeAttachments.AttachmentName;

                                    dbContext.EmployeeAttachments.Add(_EmployeeAttachment);
                                }
                            }
                        }

                        if (p_Employee.EmployeeID == Guid.Empty)
                        {
                            UserMaster _UserMaster = new UserMaster();

                            _UserMaster.UserID = Guid.NewGuid();
                            _UserMaster.EmployeeId = _EmployeeMaster.EmployeeID;
                            _UserMaster.Username = _EmployeeMaster.Email;
                            _UserMaster.Password = SecurityHelper.EncryptString("default@123");
                            _UserMaster.RoleId = new Guid(GlobalHelper.GetEnumDescription(Role.Employee));
                            _UserMaster.IsActive = true;
                            _UserMaster.CreatedDate = DateTime.Now;
                            _UserMaster.CreatedBy = p_UserId;
                            _UserMaster.ModifiedDate = DateTime.Now;
                            _UserMaster.LastLogin = DateTime.Now;

                            dbContext.UserMasters.Add(_UserMaster);
                        }
                        else
                        {
                            UserMaster _UserMaster = dbContext.UserMasters.Where(x => x.EmployeeId == p_Employee.EmployeeID).FirstOrDefault();
                            if (_UserMaster != null)
                            {
                                _UserMaster.Username = p_Employee.Email;
                                _UserMaster.ModifiedDate = DateTime.Now;
                            }
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_EmployeeMaster.EmployeeID);
                        _Result.Data = true;
                    }
                    else
                    {
                        _Result.IsSuccess = false;
                        _Result.Data = false;
                        _Result.Message = GlobalMsg.AlreadyExistMsg;
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

        public Result<Boolean> UpdateEmployeeProfile(Employee p_Employee, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMaster = new EmployeeMaster();

                    _EmployeeMaster = dbContext.EmployeeMasters.Where(e => e.EmployeeID == p_Employee.EmployeeID).FirstOrDefault();

                    _EmployeeMaster.ModifiedDate = DateTime.Now;
                    _EmployeeMaster.ModifiedBy = p_UserId;

                    _EmployeeMaster.CountryId = p_Employee.CountryId;
                    _EmployeeMaster.StateId = p_Employee.StateId;
                    _EmployeeMaster.FirstName = p_Employee.FirstName;
                    _EmployeeMaster.MiddleName = p_Employee.MiddleName;
                    _EmployeeMaster.LastName = p_Employee.LastName;
                    _EmployeeMaster.BirthDate = p_Employee.BirthDate;
                    _EmployeeMaster.Gender = p_Employee.IsGender;
                    _EmployeeMaster.MaratialStatus = p_Employee.MaratialStatus;
                    _EmployeeMaster.City = p_Employee.City;
                    _EmployeeMaster.Address = p_Employee.Address;
                    _EmployeeMaster.PinCode = p_Employee.PinCode;
                    _EmployeeMaster.MobileNo = p_Employee.MobileNo;
                    _EmployeeMaster.PhoneNo = p_Employee.PhoneNo;

                    if (!string.IsNullOrEmpty(p_Employee.PhotoName))
                    {
                        _EmployeeMaster.PhotoName = p_Employee.PhotoName;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EmployeeMaster.EmployeeID);
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

        public Result<Boolean> ResignEmployee(Employee p_Employee, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMaster = dbContext.EmployeeMasters.Where(e => e.EmployeeID == p_Employee.EmployeeID).FirstOrDefault();
                    if (_EmployeeMaster != null)
                    {
                        _EmployeeMaster.ModifiedBy = p_UserId;
                        _EmployeeMaster.ModifiedDate = DateTime.Now;
                        _EmployeeMaster.LeaveDate = p_Employee.LeaveDate;
                        _EmployeeMaster.LeaveDescription = p_Employee.LeaveDescription;
                        _EmployeeMaster.IsLeave = true;

                        dbContext.SaveChanges();

                        EmployeeAttachments _EmployeeAttachments = p_Employee.EmployeeAttachments.FirstOrDefault();

                        if (_EmployeeAttachments != null)
                        {
                            if (_EmployeeAttachments.IsDelete)
                            {
                                dbContext.EmployeeAttachments.Remove(dbContext.EmployeeAttachments.Where(ea => ea.Name == _EmployeeAttachments.Name).FirstOrDefault());
                            }
                            else
                            {
                                EmployeeAttachment _EmployeeAttachment = new EmployeeAttachment();
                                _EmployeeAttachment = dbContext.EmployeeAttachments.Where(e => e.EmployeeId == _EmployeeMaster.EmployeeID && e.AttachmentName == _EmployeeAttachments.AttachmentName).FirstOrDefault();
                                if (_EmployeeAttachment != null)
                                {
                                    _EmployeeAttachment.ModifiedDate = DateTime.Now;
                                    _EmployeeAttachment.ModifiedBy = p_UserId;
                                    _EmployeeAttachment.Name = _EmployeeAttachments.Name;
                                }
                                else
                                {
                                    _EmployeeAttachment = new EmployeeAttachment();

                                    _EmployeeAttachment.EmployeeAttachmentMapID = Guid.NewGuid();
                                    _EmployeeAttachment.IsActive = true;
                                    _EmployeeAttachment.CreatedDate = DateTime.Now;
                                    _EmployeeAttachment.CreatedBy = p_UserId;
                                    _EmployeeAttachment.ModifiedDate = DateTime.Now;
                                    _EmployeeAttachment.EmployeeId = _EmployeeMaster.EmployeeID;
                                    _EmployeeAttachment.Name = _EmployeeAttachments.Name;
                                    _EmployeeAttachment.AttachmentName = _EmployeeAttachments.AttachmentName;

                                    dbContext.EmployeeAttachments.Add(_EmployeeAttachment);
                                }
                            }
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_EmployeeMaster.EmployeeID);
                        _Result.Data = true;
                    }
                    else
                    {
                        _Result.IsSuccess = false;
                        _Result.Data = false;
                        _Result.Message = GlobalMsg.AlreadyExistMsg;
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

        public Result<List<Employee>> EmployeeDetailReport(string p_EmployeeTypeId, Boolean? P_IsResign, DateTime p_FromDate, DateTime p_ToDate)
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from e in dbContext.EmployeeMasters
                                  join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                  join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                  join dg in dbContext.DesignationMasters on e.DesignationId equals dg.DesignationID
                                  join g in dbContext.EmployeeGradeMasters on e.EmployeeGradeId equals g.EmployeeGradeID
                                  where e.IsActive == true && et.IsActive == true && d.IsActive == true && dg.IsActive == true && e.JoinDate >= p_FromDate && e.JoinDate < p_ToDate
                                  select new Employee
                                  {
                                      EmployeeID = e.EmployeeID,
                                      EmployeeTypeId = et.EmployeeTypeID,
                                      FirstName = e.FirstName,
                                      MiddleName = e.MiddleName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      EmployeeNo = e.EmployeeNo,
                                      EmployeeType = et.EmployeeType,
                                      EmployeeGrade = g.EmployeeGrade,
                                      Department = d.Department,
                                      Designation = dg.Designation,
                                      IsLeave = e.IsLeave,
                                  });

                    if (!string.IsNullOrEmpty(p_EmployeeTypeId))
                    {
                        Guid _EmployeeTypeId = new Guid(p_EmployeeTypeId);
                        _Query = _Query.Where(x => x.EmployeeTypeId == _EmployeeTypeId);
                    }

                    if (P_IsResign.HasValue)
                    {
                        _Query = _Query.Where(x => x.IsLeave == P_IsResign);
                    }

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

        public Result<Boolean> UpdateEmployeeShift(Guid p_EmployeeId, Guid p_ShiftId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMaster = new EmployeeMaster();

                    if (p_EmployeeId != Guid.Empty)
                    {
                        _EmployeeMaster = dbContext.EmployeeMasters.Where(e => e.EmployeeID == p_EmployeeId).FirstOrDefault();
                        _EmployeeMaster.ShiftId = p_ShiftId;

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_EmployeeMaster.EmployeeID);
                        _Result.Data = true;
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

        public Result<List<EmployeeModel>> GetAllDeviceEmployeeList()
        {
            Result<List<EmployeeModel>> _Result = new Result<List<EmployeeModel>>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 where e.IsActive == true && e.IsLeave == false
                                 select new EmployeeModel
                                 {
                                     Email = e.Email,
                                     FullName = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                                     Mobile = e.MobileNo,
                                     IsFace = e.IsHavingFace ?? false,
                                     IsFinger = e.is_having_fingureprint ?? false,
                                     JointDate = e.JoinDate,
                                     Count = dbContext.EmployeeDeviceMaps.Where(x => x.IsActive == true && x.EmployeeId == e.EmployeeID).Count(),
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

        public Result<List<EmployeeModel>> GetAllSendPendingEmployeeByDevice(Guid p_DeviceId)
        {
            Result<List<EmployeeModel>> _Result = new Result<List<EmployeeModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    List<Guid?> _EmployeeId = dbContext.EmployeeDeviceMaps.Where(x => x.IsActive == true && x.DeviceId == p_DeviceId).Select(x => x.EmployeeId).ToList();

                    var _Query = from e in dbContext.EmployeeMasters
                                 where e.IsActive == true && e.IsLeave == false
                                 && !_EmployeeId.Contains(e.EmployeeID)
                                 select new EmployeeModel
                                 {
                                     EmployeeId = e.EmployeeID,
                                     FullName = e.FirstName + " " + e.MiddleName + " " + e.LastName,
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

        public Result<bool> SaveEmployeeFingerPrint(EmployeeModel p_Employee)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    EmployeeMaster _EmployeeMaster = dbContext.EmployeeMasters.Where(x => x.EmployeeID == p_Employee.EmployeeId).FirstOrDefault();
                    if (_EmployeeMaster != null)
                    {
                        _EmployeeMaster.FaceTemplateData = p_Employee.FaceTemplateData;
                        _EmployeeMaster.IsHavingFace = p_Employee.IsFace;
                        _EmployeeMaster.FaceLength = p_Employee.FaceLength;
                        _EmployeeMaster.Password = p_Employee.Password;
                        _EmployeeMaster.is_having_fingureprint = p_Employee.IsFinger;
                        _EmployeeMaster.finger_template_data_tft = p_Employee.finger_template_data_tft;
                        _EmployeeMaster.finger_template_data_tft1 = p_Employee.finger_template_data_tft1;
                        _EmployeeMaster.finger_template_data_tft2 = p_Employee.finger_template_data_tft2;
                        _EmployeeMaster.finger_template_data_tft3 = p_Employee.finger_template_data_tft3;
                        _EmployeeMaster.finger_template_data_tft4 = p_Employee.finger_template_data_tft4;
                        _EmployeeMaster.finger_template_data_tft5 = p_Employee.finger_template_data_tft5;
                        _EmployeeMaster.finger_template_data_tft6 = p_Employee.finger_template_data_tft6;
                        _EmployeeMaster.finger_template_data_tft7 = p_Employee.finger_template_data_tft7;
                        _EmployeeMaster.finger_template_data_tft8 = p_Employee.finger_template_data_tft8;
                        _EmployeeMaster.finger_template_data_tft9 = p_Employee.finger_template_data_tft9;

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Data = true;
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

        public Result<List<EmployeeModel>> GetEmployeeAttendanceReportByEmpoyeeIdAndDate(Guid p_EmployeeId, DateTime p_FromDate, DateTime p_ToDate, Guid p_DeviceId)
        {
            Result<List<EmployeeModel>> _Result = new Result<List<EmployeeModel>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeMasters
                                 join ed in dbContext.EmployeeAttendanceDevices
                                 on e.EmployeeID equals ed.EmployeeId
                                 join d in dbContext.DeviceMasters
                                 on ed.DeviceId equals d.DeviceID
                                 where e.IsActive == true && ed.AttendanceDate >= p_FromDate && ed.AttendanceDate <= p_ToDate
                                 select new EmployeeModel
                                 {
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     AttendanceDate = ed.AttendanceDate,
                                     PunchTime = ed.PunchTime,
                                     AttendanceDateTime = ed.AttendanceDateTime,
                                     PunchMethod = ed.PunchMethod,
                                     DeviceName = d.DeviceName,
                                     EmployeeId = e.EmployeeID,
                                     DeviceId = d.DeviceID,
                                 };

                    if (!string.IsNullOrEmpty(Convert.ToString(p_EmployeeId)))
                    {
                        _Query = _Query.Where(x => x.EmployeeId == p_EmployeeId);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(p_DeviceId)))
                    {
                        _Query = _Query.Where(x => x.DeviceId == p_DeviceId);
                    }
                    _Result.Data = _Query.OrderBy(x => x.AttendanceDateTime).ToList();
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

        public Result<List<EmployeeBirthDayModel>> GetUpComingBirthDate()
        {
            Result<List<EmployeeBirthDayModel>> _Result = new Result<List<EmployeeBirthDayModel>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.EmployeeMasters.Where(x => x.IsActive == true && x.IsLeave == false)
                       .Select(s => new EmployeeBirthDayModel()
                       {
                           Name = s.FirstName + " " + s.LastName,
                           BirthDate = s.BirthDate ?? DateTime.Now,
                       });
                    if (_Query != null)
                    {
                        _Result.Data = _Query.ToList();
                        _Result.IsSuccess = true;
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

        public Result<int> GetPresentEmployee()
        {
            Result<int> _Result = new Result<int>();
            DateTime _TodayDate = DateTime.Now.Date;
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from e in dbContext.EmployeeMasters
                                  join ea in dbContext.EmployeeAttendanceDevices
                                  on e.EmployeeID equals ea.EmployeeId
                                  where e.IsActive == true && e.IsLeave == false
                                  && ea.AttendanceDate == _TodayDate
                                  select ea.EmployeeId).Distinct().Count();

                    if (_Query != null)
                    {
                        _Result.Data = _Query;
                        _Result.IsSuccess = true;
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

        public Result<List<EmployeeDepartment>> GetDepartmentChartInfo()
        {
            Result<List<EmployeeDepartment>> _Result = new Result<List<EmployeeDepartment>>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.DepartmentMasters.Where(x => x.IsActive == true)
                                 .Select(s => new EmployeeDepartment()
                                 {
                                     Department = s.Department,
                                     TotalEmployee = dbContext.EmployeeMasters.Where(a => a.IsActive == true && a.DepartmentId == s.DepartmentID).Count(),
                                 });
                    if (_Query != null)
                    {
                        _Result.Data = _Query.ToList();
                        _Result.IsSuccess = true;
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

        public Result<List<Employee>> GetPresentEmployeeList()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();
            DateTime _TodayDate = DateTime.Now.Date;

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from e in dbContext.EmployeeMasters
                                  join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                  join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                  join dg in dbContext.DesignationMasters on e.DesignationId equals dg.DesignationID
                                  join g in dbContext.EmployeeGradeMasters on e.EmployeeGradeId equals g.EmployeeGradeID
                                  join s in dbContext.ShiftMasters on e.ShiftId equals s.ShiftID
                                  join ea in dbContext.EmployeeAttendanceDevices on e.EmployeeID equals ea.EmployeeId
                                  where e.IsActive == true && et.IsActive == true && d.IsActive == true && dg.IsActive == true && ea.AttendanceDate == _TodayDate
                                  select new Employee
                                  {
                                      EmployeeID = e.EmployeeID,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      EmployeeNo = e.EmployeeNo,
                                      EmployeeType = et.EmployeeType,
                                      Department = d.Department,
                                      Designation = dg.Designation,
                                      IsLeave = e.IsLeave,
                                      EmployeeGrade = g.EmployeeGrade,
                                      Shift = s.Shift,
                                      FromTime = s.FromTime,
                                      ToTime = s.ToTime,
                                  });

                    _Result.Data = _Query.Distinct().ToList();
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

        public Result<List<Employee>> GetAbsentEmployeeList()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();
            DateTime _TodayDate = DateTime.Now.Date;

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = (from e in dbContext.EmployeeMasters
                                  join et in dbContext.EmployeeTypeMasters on e.EmployeeTypeId equals et.EmployeeTypeID
                                  join d in dbContext.DepartmentMasters on e.DepartmentId equals d.DepartmentID
                                  join dg in dbContext.DesignationMasters on e.DesignationId equals dg.DesignationID
                                  join g in dbContext.EmployeeGradeMasters on e.EmployeeGradeId equals g.EmployeeGradeID
                                  join s in dbContext.ShiftMasters on e.ShiftId equals s.ShiftID
                                  join ea in dbContext.EmployeeAttendanceDevices on e.EmployeeID equals ea.EmployeeId
                                  where e.IsActive == true && et.IsActive == true && d.IsActive == true && dg.IsActive == true && ea.AttendanceDate != _TodayDate
                                  select new Employee
                                  {
                                      EmployeeID = e.EmployeeID,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      EmployeeNo = e.EmployeeNo,
                                      EmployeeType = et.EmployeeType,
                                      Department = d.Department,
                                      Designation = dg.Designation,
                                      IsLeave = e.IsLeave,
                                      EmployeeGrade = g.EmployeeGrade,
                                      Shift = s.Shift,
                                      FromTime = s.FromTime,
                                      ToTime = s.ToTime,
                                  });

                    _Result.Data = _Query.Distinct().ToList();
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
