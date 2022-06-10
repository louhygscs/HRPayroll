using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IDocumentService
    {
        #region Document
        Result<List<DocumentModel>> GetDocuments();

        Result<DocumentModel> GetByDocumentId(Guid p_EntityId);

        Result<Boolean> DeleteDocument(Guid p_EntityId, Guid p_userId);

        Result<bool> SaveDocument(DocumentModel p_Entity);

        #endregion

    }
}
