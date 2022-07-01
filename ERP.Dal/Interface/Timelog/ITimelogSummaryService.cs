using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface ITimelogSummaryService
    {
        Result<List<TimelogSummaryModel>> GetTimelogSummaries();

        Result<Boolean> DeleteTimelogSummaryById(Guid p_EntityId, Guid p_UserId);

        Result<TimelogSummaryModel> GetTimelogSummaryById(Guid p_EntityId);

        Result<bool> SaveTimelogSummary(TimelogSummaryModel p_Entity, Guid p_UserId);


    }
}