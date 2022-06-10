using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IStateService
    {
        Result<List<State>> GetStateList();
        Result<Boolean> DeleteStateById(Guid p_StateId, Guid p_UserId);
        Result<State> GetStateById(Guid p_StateId);
        Result<State> GetStateByCountryId(Guid p_CountryId);
        Result<bool> SaveState(State p_State, Guid p_UserId);
    }
}