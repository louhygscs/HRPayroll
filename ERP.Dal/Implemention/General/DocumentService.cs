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
    public class DocumentService : IDocumentService
    {
        public Result<List<DocumentModel>> GetDocuments()
        {
            Result<List<DocumentModel>> _Result = new Result<List<DocumentModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.DocumentMasters
                                 where e.IsActive == true
                                 select new DocumentModel
                                 {
                                     DocumentId    = e.DocumentId,
                                     RelatedId     = e.RelatedId,
                                     DocLabel      = e.DocLabel,
                                     DocType       = e.DocType,
                                     DocFileType   = e.DocFileType,
                                     DocFileBase64 = e.DocFileBase64,
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

        public Result<DocumentModel> GetByDocumentId(Guid p_EntityId)
        {
            Result<DocumentModel> _Result = new Result<DocumentModel>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.DocumentMasters
                                 where e.DocumentId == p_EntityId
                                 select new DocumentModel
                                 {
                                     DocumentId    = e.DocumentId,
                                     RelatedId     = e.RelatedId,
                                     DocLabel      = e.DocLabel,
                                     DocType       = e.DocType,
                                     DocFileType   = e.DocFileType,
                                     DocFileBase64 = e.DocFileBase64,
                                     IsActive      = e.IsActive,
                                     CreateDate    = e.CreatedDate,
                                     CreatedBy     = e.CreatedBy,
                                     ModifiedDate  = e.ModifiedDate,
                                     ModifiedBy    = e.ModifiedBy
                                 };

                    DocumentModel _Country = _Query.FirstOrDefault();

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
        
        public Result<bool> DeleteDocument(Guid p_EntityId, Guid p_userId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.PayrollCutOffMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        PayrollCutOffMaster _EntityDelete = dbContext.PayrollCutOffMasters.Where(d => d.PayrollCutOffId == p_EntityId).FirstOrDefault();

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

        public Result<bool> SaveDocument(DocumentModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                DocumentMaster _checkEntry = dbContext.DocumentMasters.Where(x => x.DocType == p_Entity.DocType && x.RelatedId == p_Entity.RelatedId).FirstOrDefault();

                if (_checkEntry == null)
                {
                    DocumentMaster _entry = new DocumentMaster();

                    if (p_Entity.DocumentId == Guid.Empty)
                    {
                        _entry.DocumentId    = Guid.NewGuid();
                        _entry.RelatedId     = p_Entity.RelatedId;
                        _entry.DocLabel      = p_Entity.DocLabel;
                        _entry.DocType       = p_Entity.DocType;
                        _entry.DocFileType   = p_Entity.DocFileType;
                        _entry.DocFileBase64 = p_Entity.DocFileBase64;
                        _entry.IsActive      = true;
                        _entry.CreatedDate   = DateTime.Now;
                        _entry.CreatedBy     = p_Entity.CreatedBy;

                        dbContext.DocumentMasters.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.DocumentMasters.Where(e => e.DocumentId == p_Entity.DocumentId).FirstOrDefault();

                        _entry.DocLabel      = p_Entity.DocLabel;
                        _entry.DocType       = p_Entity.DocType;
                        _entry.DocFileType   = p_Entity.DocFileType;
                        _entry.DocFileBase64 = p_Entity.DocFileBase64;
                        _entry.IsActive      = true;
                        _entry.ModifiedDate  = DateTime.Now;
                        _entry.ModifiedBy    = p_Entity.CreatedBy;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_entry.DocumentId);
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
