using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class ValueModel
    {
        public Guid ValueId { get; set; }
        public Guid? FieldId { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
        public string ItemsJson { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
