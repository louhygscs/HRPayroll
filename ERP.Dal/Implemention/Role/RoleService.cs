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
    public class RoleService : IRoleService
    {
        public Result<List<RoleModel>> GetRoleList()
        {
            Result<List<RoleModel>> _Result = new Result<List<RoleModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.RoleMasters
                                 where e.IsActive == true
                                 select new RoleModel
                                 {
                                     RoleID = e.RoleID,
                                     RoleName = e.RoleName
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

        public Result<Boolean> DeleteRoleById(Guid p_RoleId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.UserMasters.Where(e => e.RoleId == p_RoleId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        RoleMaster _RoleMaster = dbContext.RoleMasters.Where(d => d.RoleID == p_RoleId).FirstOrDefault();

                        if (_RoleMaster != null)
                        {
                            _RoleMaster.IsActive = false;
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

        public Result<RoleModel> GetRoleById(Guid p_RoleId)
        {
            Result<RoleModel> _Result = new Result<RoleModel>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.RoleMasters
                                 where d.RoleID == p_RoleId
                                 select new RoleModel
                                 {
                                     RoleID = d.RoleID,
                                     RoleName = d.RoleName,
                                     IsActive = d.IsActive
                                 };

                    RoleModel _RoleMaster = _Query.FirstOrDefault();
                    if (_RoleMaster != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _RoleMaster;
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

        public Result<bool> SaveRole(RoleModel p_Role, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            using (var dbContext = new ERPEntities())
            {
                RoleMaster _RoleMaster = dbContext.RoleMasters.Where(e => e.RoleName == p_Role.RoleName).FirstOrDefault();

                if (_RoleMaster == null)
                {
                    _RoleMaster = new RoleMaster();

                    if (p_Role.RoleID == Guid.Empty)
                    {
                        _RoleMaster.RoleID   = Guid.NewGuid();
                        _RoleMaster.RoleName = p_Role.RoleName;
                        _RoleMaster.IsActive    = true;
                    }
                    else
                    {
                        _RoleMaster = dbContext.RoleMasters.Where(e => e.RoleID == p_Role.RoleID).FirstOrDefault();

                        _RoleMaster.RoleName = p_Role.RoleName;
                        _RoleMaster.IsActive = p_Role.IsActive;
                    }

                    if (p_Role.RoleID == Guid.Empty)
                    {
                        dbContext.RoleMasters.Add(_RoleMaster);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_RoleMaster.RoleID);
                    _Result.Data = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data = false;
                    _Result.Message = GlobalMsg.AlreadyExistMsg;
                }
            }
            return _Result;
        }
    }
}
