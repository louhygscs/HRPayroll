using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IDimensionService
    {
        #region Category
        Result<List<CategoryModel>> GetCategories();

        Result<CategoryModel> GetByCategoryId(Guid p_EntityId);

        Result<bool> DeleteCategory(Guid p_EntityId, Guid p_userId);

        Result<bool> SaveCategory(CategoryModel p_Entity);  
        #endregion

    }
}
