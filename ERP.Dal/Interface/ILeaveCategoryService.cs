using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface ILeaveCategoryService
    {
        Result<List<LeaveCategory>> GetLeaveCategoryList();

        Result<Boolean> DeleteLeaveCategoryById(Guid p_LeaveCategoryId, Guid p_UserId);

        Result<LeaveCategory> GetLeaveCategoryById(Guid p_LeaveCategoryId);

        Result<Boolean> SaveLeaveCategory(LeaveCategory p_LeaveCategory, Guid p_UserId);
    }
}
