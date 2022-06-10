using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    #region Document
    public class DocumentModel
    {
        public Guid DocumentId { get; set; }
        public Guid RelatedId { get; set; }

        public string DocLabel { get; set; }

        public string DocType { get; set; }

        public string DocFileType { get; set; }
        public string DocFileBase64 { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }

    #endregion


}
