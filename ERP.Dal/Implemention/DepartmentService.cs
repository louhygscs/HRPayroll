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
    public class DepartmentService : IDepartmentService
    {
        public Result<List<Department>> GetDepartmentList()
        {
            Result<List<Department>> _Result = new Result<List<Department>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DepartmentMasters
                                      where d.IsActive == true
                                      select new Department
                                 {
                                     DepartmentID = d.DepartmentID,
                                     DepartmentName = d.Department
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

        public Result<Boolean> DeleteDepartmentById(Guid p_DepartmentId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.DepartmentId == p_DepartmentId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        DepartmentMaster _DepartmentMaster = dbContext.DepartmentMasters.Where(d => d.DepartmentID == p_DepartmentId).FirstOrDefault();

                        if (_DepartmentMaster != null)
                        {
                            _DepartmentMaster.IsActive = false;
                            _DepartmentMaster.ModifiedDate = DateTime.Now;
                            _DepartmentMaster.ModifiedBy = p_UserId;

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

        public Result<Department> GetDepartmentById(Guid p_DepartmentId)
        {
            Result<Department> _Result = new Result<Department>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DepartmentMasters
                                 where d.DepartmentID == p_DepartmentId
                                 select new Department
                                 {
                                     DepartmentID = d.DepartmentID,
                                     DepartmentName = d.Department,
                                 };

                    Department _Department = _Query.FirstOrDefault();
                    if (_Department != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Department;
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

        public Result<Boolean> SaveDepartment(Department p_Department, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    DepartmentMaster _DepartmentMasterExist = dbContext.DepartmentMasters.Where(d => d.DepartmentID != p_Department.DepartmentID && d.Department == p_Department.DepartmentName && d.IsActive==true).FirstOrDefault();

                    if (_DepartmentMasterExist == null)
                    {
                        DepartmentMaster _DepartmentMaster = new DepartmentMaster();

                        if (p_Department.DepartmentID == Guid.Empty)
                        {
                            _DepartmentMaster.DepartmentID = Guid.NewGuid();
                            _DepartmentMaster.IsActive = true;
                            _DepartmentMaster.CreatedDate = DateTime.Now;
                            _DepartmentMaster.CreatedBy = p_UserId;
                            _DepartmentMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _DepartmentMaster = dbContext.DepartmentMasters.Where(d => d.DepartmentID == p_Department.DepartmentID).FirstOrDefault();

                            _DepartmentMaster.ModifiedDate = DateTime.Now;
                            _DepartmentMaster.ModifiedBy = p_UserId;
                        }

                        _DepartmentMaster.Department = p_Department.DepartmentName;

                        if (p_Department.DepartmentID == Guid.Empty)
                        {
                            dbContext.DepartmentMasters.Add(_DepartmentMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_DepartmentMaster.DepartmentID);
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
    }
}
