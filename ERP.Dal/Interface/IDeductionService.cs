using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IDeductionService
    {
        Result<List<Deduction>> GetDeductionList();

        Result<Boolean> DeleteDeductionById(Guid p_DeductionId, Guid p_UserId);

        Result<Deduction> GetDeductionById(Guid p_DeductionId);

        Result<Boolean> SaveDeduction(Deduction p_Deduction, Guid p_UserId);
    }
}
