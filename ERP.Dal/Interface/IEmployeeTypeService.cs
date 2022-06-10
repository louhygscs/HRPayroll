using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeTypeService
    {
        Result<List<EmployeeType>> GetEmployeeTypeList();

        Result<Boolean> DeleteEmployeeTypeById(Guid p_EmployeeTypeId, Guid p_UserId);

        Result<EmployeeType> GetEmployeeTypeById(Guid p_EmployeeTypeId);

        Result<Boolean> SaveEmployeeType(EmployeeType p_EmployeeType, Guid p_UserId);
    }
}
