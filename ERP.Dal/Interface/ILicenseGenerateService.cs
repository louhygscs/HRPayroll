using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface ILicenseGenerateService
    {
        Result<List<LicenseGenerateModel>> GetLicenseKeyList();

        Result<bool> SaveLicenseKey(LicenseGenerateModel p_LicenseGenerateModel, Guid p_UserId);

        Result<bool> LicenseKeyUsedById(Guid p_LicenseKeyId);

    }
}
