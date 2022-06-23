using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IRoleService
    {
        Result<List<RoleModel>> GetRoleList();

        Result<Boolean> DeleteRoleById(Guid p_RoleId, Guid p_UserId);

        Result<RoleModel> GetRoleById(Guid p_RoleId);

        Result<bool> SaveRole(RoleModel p_Role, Guid p_UserId);
    }
}