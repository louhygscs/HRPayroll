using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface ICountryService
    {
        Result<List<Country>> GetCountryList();

        Result<Boolean> DeleteCountryById(Guid p_CountryId, Guid p_UserId);

        Result<Country> GetCountryById(Guid p_CountryId);

        Result<bool> SaveCountry(Country p_Country, Guid p_UserId);


    }
}