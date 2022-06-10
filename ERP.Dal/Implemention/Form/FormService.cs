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
    public class FormService : IFormService
    {
        #region Form Master
        public Result<List<FormModel>> GetForms()
        {
            Result<List<FormModel>> _Result = new Result<List<FormModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.FormMasters
                                 where e.IsActive == true
                                 select new FormModel
                                 {
                                     FormId      = e.FormId,
                                     FormName    = e.FormName,
                                     FormType    = e.FormType,
                                     IsActive    = e.IsActive,
                                     JsonData    = e.JsonData,
                                     CreatedDate = e.CreatedDate,
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
        public Result<FormModel> GetFormById(Guid p_Id)
        {
            Result<FormModel> _Result = new Result<FormModel>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.FormMasters
                                 where e.FormId == p_Id
                                 select new FormModel
                                 {
                                     FormId      = e.FormId,
                                     FormName    = e.FormName,
                                     FormType    = e.FormType,
                                     IsActive    = e.IsActive,
                                     JsonData    = e.JsonData,
                                     CreatedDate = e.CreatedDate,
                                 };

                    _Result.Data = _Query.FirstOrDefault();
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
        public Result<bool> DeleteForm(Guid p_Id, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.FormMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        FormMaster _EntityDelete = dbContext.FormMasters.Where(d => d.FormId == p_Id).FirstOrDefault();

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
        public Result<bool> SaveForm(FormModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                FormMaster _entry = new FormMaster();

                if (p_Entity.FormId == Guid.Empty)
                {
                    _entry.FormId      = Guid.NewGuid();
                    _entry.FormName    = p_Entity.FormName;
                    _entry.FormType    = p_Entity.FormType;
                    _entry.IsActive    = true;
                    _entry.JsonData    = p_Entity.JsonData;
                    _entry.CreatedDate = DateTime.Now;
                    _entry.CreatedBy   = p_Entity.CreatedBy;

                    dbContext.FormMasters.Add(_entry);
                }
                else
                {
                    _entry = dbContext.FormMasters.Where(e => e.FormId == p_Entity.FormId).FirstOrDefault();

                    _entry.FormName = p_Entity.FormName;
                    _entry.FormType = p_Entity.FormType;
                    _entry.IsActive = p_Entity.IsActive;
                    _entry.JsonData = p_Entity.JsonData;
                }

                dbContext.SaveChanges();

                _Result.IsSuccess = true;
                _Result.Message   = GlobalMsg.SaveSuccessMsg;
                _Result.Id        = Convert.ToString(_entry.FormId);
                _Result.Data      = true;
            }

            return _Result;
        }

        #endregion

        #region Field Master
        public Result<List<FieldModel>> GetFields()
        {
            Result<List<FieldModel>> _Result = new Result<List<FieldModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.FieldMasters
                                 select new FieldModel
                                 {
                                     FieldId         = e.FieldId,
                                     FormId          = e.FormId,
                                     FieldLabel      = e.FieldLabel,
                                     FieldType       = e.FieldType,
                                     FieldIsRequired = e.FieldIsRequired,
                                     ItemsJson       = e.ItemsJson,
                                     CreatedDate     = e.CreatedDate,
                                     CreatedBy       = e.CreatedBy
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
        public Result<FieldModel> GetFieldById(Guid p_Id)
        {
            Result<FieldModel> _Result = new Result<FieldModel>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.FieldMasters
                                 where e.FormId == p_Id
                                 select new FieldModel
                                 {
                                     FieldId         = e.FieldId,
                                     FormId          = e.FormId,
                                     FieldLabel      = e.FieldLabel,
                                     FieldType       = e.FieldType,
                                     FieldIsRequired = e.FieldIsRequired,
                                     ItemsJson       = e.ItemsJson,
                                     CreatedDate     = e.CreatedDate,
                                     CreatedBy       = e.CreatedBy
                                 };

                    _Result.Data = _Query.FirstOrDefault();
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
        public Result<bool> DeleteField(Guid p_Id, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.FormMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        FormMaster _EntityDelete = dbContext.FormMasters.Where(d => d.FormId == p_Id).FirstOrDefault();

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
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }
        public Result<bool> SaveField(FieldModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                FieldMaster _entry = new FieldMaster();

                if (p_Entity.FieldId == Guid.Empty)
                {
                    _entry.FieldId         = Guid.NewGuid();
                    _entry.FormId          = p_Entity.FormId;
                    _entry.FieldLabel      = p_Entity.FieldLabel;
                    _entry.FieldType       = p_Entity.FieldType;
                    _entry.FieldIsRequired = p_Entity.FieldIsRequired;
                    _entry.ItemsJson       = p_Entity.ItemsJson;
                    _entry.CreatedDate     = DateTime.Now;
                    _entry.CreatedBy       = p_Entity.CreatedBy;

                    dbContext.FieldMasters.Add(_entry);
                }
                else
                {
                    _entry = dbContext.FieldMasters.Where(e => e.FormId == p_Entity.FormId).FirstOrDefault();

                    _entry.FieldLabel       = p_Entity.FieldLabel;
                    _entry.FieldType        = p_Entity.FieldType;
                    _entry.FieldIsRequired  = p_Entity.FieldIsRequired;
                    _entry.ItemsJson        = p_Entity.ItemsJson;
                }

                dbContext.SaveChanges();

                _Result.IsSuccess = true;
                _Result.Message   = GlobalMsg.SaveSuccessMsg;
                _Result.Id        = Convert.ToString(_entry.FormId);
                _Result.Data      = true;
            }

            return _Result;
        }

        #endregion

        #region Value Master
        public Result<List<ValueModel>> GetValues()
        {
            Result<List<ValueModel>> _Result = new Result<List<ValueModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.ValueMasters
                                 where e.IsActive == true
                                 select new ValueModel
                                 {
                                     FieldId     = e.FieldId,
                                     FieldValue  = e.FieldValue,
                                     FieldType   = e.FieldType,
                                     ItemsJson   = e.ItemsJson,
                                     IsActive    = e.IsActive,
                                     CreatedDate = e.CreatedDate,
                                     CreatedBy   = e.CreatedBy
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }
        public Result<ValueModel> GetValueById(Guid p_Id)
        {
            Result<ValueModel> _Result = new Result<ValueModel>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.ValueMasters
                                 where e.ValueId == p_Id
                                 select new ValueModel
                                 {
                                     FieldId     = e.FieldId,
                                     FieldValue  = e.FieldValue,
                                     FieldType   = e.FieldType,
                                     ItemsJson   = e.ItemsJson,
                                     IsActive    = e.IsActive,
                                     CreatedDate = e.CreatedDate,
                                     CreatedBy   = e.CreatedBy
                                 };

                    _Result.Data = _Query.FirstOrDefault();
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
        public Result<bool> DeleteValue(Guid p_Id, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.ValueMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        ValueMaster _EntityDelete = dbContext.ValueMasters.Where(d => d.ValueId == p_Id).FirstOrDefault();

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
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }
        public Result<bool> SaveValue(ValueModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                ValueMaster _entry = new ValueMaster();

                if (p_Entity.ValueId == Guid.Empty)
                {
                    _entry.ValueId     = Guid.NewGuid();
                    _entry.FieldId     = p_Entity.FieldId;
                    _entry.FieldValue  = p_Entity.FieldValue;
                    _entry.FieldType   = p_Entity.FieldType;
                    _entry.ItemsJson   = p_Entity.ItemsJson;
                    _entry.IsActive    = p_Entity.IsActive;
                    _entry.CreatedDate = DateTime.Now;
                    _entry.CreatedBy   = p_Entity.CreatedBy;

                    dbContext.ValueMasters.Add(_entry);
                }
                else
                {
                    _entry = dbContext.ValueMasters.Where(e => e.ValueId == p_Entity.ValueId).FirstOrDefault();

                    _entry.FieldValue = p_Entity.FieldValue;
                    _entry.FieldType  = p_Entity.FieldType;
                    _entry.ItemsJson  = p_Entity.ItemsJson;
                    _entry.IsActive   = p_Entity.IsActive;
                }

                dbContext.SaveChanges();

                _Result.IsSuccess = true;
                _Result.Message   = GlobalMsg.SaveSuccessMsg;
                _Result.Id        = Convert.ToString(_entry.ValueId);
                _Result.Data      = true;
            }

            return _Result;
        }

        #endregion
    }
}
