using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface ITimelogEntryService
    {
        Result<List<TimelogEntryModel>> GetTimeLogEntries();

        Result<Boolean> DeleteTimelogEntryById(Guid p_EntityId, Guid p_UserId);

        Result<TimelogEntryModel> GetTimelogEntryById(Guid p_EntityId);

        Result<bool> SaveTimelogEntry(TimelogEntryModel p_Entity, Guid p_UserId);


    }
}
