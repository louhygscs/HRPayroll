using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IRoleService
    {
        Result<List<RoleModel>> GetRoleList();

        Result<Boolean> DeleteRoleById(Guid p_RoleID, Guid p_UserId);

        Result<RoleModel> GetRoleById(Guid p_RoleID);

        Result<bool> SaveRole(RoleModel p_RoleName, Guid p_UserId);


    }
}