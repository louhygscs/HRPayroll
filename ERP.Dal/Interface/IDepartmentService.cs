using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IDepartmentService
    {
        Result<List<Department>> GetDepartmentList();

        Result<Boolean> DeleteDepartmentById(Guid p_DepartmentId,Guid p_UserId);

        Result<Department> GetDepartmentById(Guid p_DepartmentId);

        Result<Boolean> SaveDepartment(Department p_Department,Guid p_UserId);
    }
}
