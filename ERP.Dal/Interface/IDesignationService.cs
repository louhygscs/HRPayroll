using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IDesignationService
    {
        Result<List<Designation>> GetDesignationList();

        Result<Boolean> DeleteDesignationById(Guid p_DesignationId, Guid p_UserId);

        Result<Designation> GetDesignationById(Guid p_DesignationId);

        Result<Boolean> SaveDesignation(Designation p_Designation, Guid p_UserId);
    }
}
