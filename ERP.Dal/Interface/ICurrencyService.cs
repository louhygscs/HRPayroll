using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface ICurrencyService
    {
        Result<List<Currency>> GetList();

        Result<Currency> GetById(int p_EntityId);

        Result<Currency> GetByCode(string p_Code);

        Result<Boolean> DeleteEntity(int p_EntityId);

        Result<bool> SaveEntity(Currency p_Entity);
    }
}