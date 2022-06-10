using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IShiftService
    {
        Result<List<Shift>> GetShiftList();

        Result<Boolean> DeleteShiftById(Guid p_ShiftId, Guid p_UserId);

        Result<Shift> GetShiftById(Guid p_ShiftId);

        Result<Boolean> SaveShift(Shift p_Shift, Guid p_UserId);

        Result<Shift> GetShiftByEmployeeId(Guid p_EmployeeId);
    }
}
