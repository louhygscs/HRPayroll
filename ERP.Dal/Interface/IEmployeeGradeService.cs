using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IEmployeeGradeService
    {
        Result<List<EmployeeGrade>> GetEmployeeGradeList();

        Result<Boolean> DeleteEmployeeGradeById(Guid p_EmployeeGradeId, Guid p_UserId);

        Result<EmployeeGrade> GetEmployeeGradeById(Guid p_EmployeeGradeId);

        Result<Boolean> SaveEmployeeGrade(EmployeeGrade p_EmployeeGrade, Guid p_UserId);
    }
}
