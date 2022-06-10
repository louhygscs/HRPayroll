using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Implemention
{
    public class DimensionService : IDimensionService
    {
        public Result<List<CategoryModel>> GetCategories()
        {
            Result<List<CategoryModel>> _Result = new Result<List<CategoryModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.CategoryMasters
                                 where e.IsActive == true
                                 select new CategoryModel
                                 {
                                     CategoryId    = e.CategoryId,
                                     CategoryTable = e.CategoryTable,
                                     CategoryName  = e.CategoryName,
                                     IsActive      = e.IsActive,
                                     CreateDate    = e.CreatedDate,
                                     CreatedBy     = e.CreatedBy,
                                     ModifiedDate  = e.ModifiedDate,
                                     ModifiedBy    = e.ModifiedBy
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<CategoryModel> GetByCategoryId(Guid p_EntityId)
        {
            Result<CategoryModel> _Result = new Result<CategoryModel>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.CategoryMasters
                                 where e.CategoryId == p_EntityId
                                 select new CategoryModel
                                 {
                                     CategoryId    = e.CategoryId,
                                     CategoryTable = e.CategoryTable,
                                     CategoryName  = e.CategoryName,
                                     IsActive      = e.IsActive,
                                     CreateDate    = e.CreatedDate,
                                     CreatedBy     = e.CreatedBy,
                                     ModifiedDate  = e.ModifiedDate,
                                     ModifiedBy    = e.ModifiedBy
                                 };

                    CategoryModel _Country = _Query.FirstOrDefault();

                    if (_Country != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data      = _Country;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }
        
        public Result<bool> DeleteCategory(Guid p_EntityId, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.CategoryMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        CategoryMaster _EntityDelete = dbContext.CategoryMasters.Where(d => d.CategoryId == p_EntityId).FirstOrDefault();

                        if (_EntityDelete != null)
                        {
                            _EntityDelete.IsActive = false;

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> SaveCategory(CategoryModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                CategoryMaster _checkEntry = dbContext.CategoryMasters.Where(x => x.CategoryTable == p_Entity.CategoryTable && x.CategoryName == p_Entity.CategoryName).FirstOrDefault();

                if (_checkEntry == null)
                {
                    CategoryMaster _entry = new CategoryMaster();

                    if (p_Entity.CategoryId == Guid.Empty)
                    {
                        _entry.CategoryId    = Guid.NewGuid();
                        _entry.CategoryTable = p_Entity.CategoryTable;
                        _entry.CategoryName  = p_Entity.CategoryName;
                        _entry.IsActive      = true;
                        _entry.CreatedDate   = DateTime.Now;
                        _entry.CreatedBy     = p_Entity.CreatedBy;

                        dbContext.CategoryMasters.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.CategoryMasters.Where(e => e.CategoryId == p_Entity.CategoryId).FirstOrDefault();

                        _entry.CategoryTable = p_Entity.CategoryTable;
                        _entry.CategoryName  = p_Entity.CategoryName;
                        _entry.IsActive      = p_Entity.IsActive;
                        _entry.ModifiedDate  = DateTime.Now;
                        _entry.ModifiedBy    = p_Entity.CreatedBy;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_entry.CategoryId);
                    _Result.Data      = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data      = false;
                    _Result.Message   = GlobalMsg.AlreadyExistMsg;
                }
            }

            return _Result;
        }
        
    }
}
