using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IHolidayService
    {
        Result<List<Holiday>> GetHolidayList();

        Result<List<Holiday>> GetHolidayListByDate(DateTime p_FromDate, DateTime p_ToDate);

        Result<Boolean> DeleteHolidayById(Guid p_HolidayId, Guid p_UserId);

        Result<Holiday> GetHolidayById(Guid p_HolidayId);

        Result<Boolean> SaveHoliday(Holiday p_Holiday, Guid p_UserId);
    }
}
