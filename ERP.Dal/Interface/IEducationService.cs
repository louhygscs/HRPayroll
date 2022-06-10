using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IEducationService
    {
        Result<List<Education>> GetEducationList();

        Result<Boolean> DeleteEducationById(Guid p_EducationId, Guid p_UserId);

        Result<Education> GetEducationById(Guid p_EducationId);

        Result<bool> SaveEducation(Education p_Education, Guid p_UserId);


    }
}