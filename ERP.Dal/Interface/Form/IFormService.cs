using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IFormService
    {
        #region Form Master
        Result<List<FormModel>> GetForms();
        Result<FormModel> GetFormById(Guid p_Id);
        Result<bool> DeleteForm(Guid p_Id, Guid p_userId);
        Result<bool> SaveForm(FormModel p_Entity);

        #endregion

        #region Field Master
        Result<List<FieldModel>> GetFields();
        Result<FieldModel> GetFieldById(Guid p_Id);
        Result<bool> DeleteField(Guid p_Id, Guid p_userId);
        Result<bool> SaveField(FieldModel p_Entity);

        #endregion

        #region Value Master
        Result<List<ValueModel>> GetValues();
        Result<ValueModel> GetValueById(Guid p_Id);
        Result<bool> DeleteValue(Guid p_Id, Guid p_userId);
        Result<bool> SaveValue(ValueModel p_Entity);

        #endregion
    }
}
