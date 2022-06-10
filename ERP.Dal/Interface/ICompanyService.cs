using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface ICompanyService
    {
        Result<List<Company>> GetCompanies();

        Result<Company> GetCompany();
        Result<Company> GetCompany(Guid p_Id);

        Result<Boolean> SaveCompany(Company p_Company, Guid p_UserId);

        Result<bool> LicenseKeyActivate(Guid P_CompanyId);

        Result<string> GetGlobalSetting();

        Result<bool> SaveGlobalSetting(string p_Value, int p_Id);

    }
}