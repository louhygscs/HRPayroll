using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class FormModel
    {
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        public string FormType { get; set; }
        public bool? IsActive { get; set; }
        public string JsonData { get; set; }

        public DateTime? CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
    }
}
