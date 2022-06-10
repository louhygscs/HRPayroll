using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IFinancialYearService
    {
        Result<FinancialYear> GetFinancialYearById(Guid p_FinancialYearId);

        Result<List<FinancialYear>> GetFinancialYearList();

        Result<Boolean> DeleteFinancialYearById(Guid p_FinancialYearId, Guid p_UserId);

        Result<Boolean> SaveFinancialYear(FinancialYear p_FinancialYear, Guid p_UserId);
    }
}
