using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IAllowanceService
    {
        Result<List<Allowance>> GetAllowanceList();

        Result<Boolean> DeleteAllowanceById(Guid p_AllowanceId, Guid p_UserId);

        Result<Allowance> GetAllowanceById(Guid p_AllowanceId);

        Result<Boolean> SaveAllowance(Allowance p_Allowance, Guid p_UserId);
    }
}
