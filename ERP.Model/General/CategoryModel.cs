using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    #region Category
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        
        public string CategoryTable { get; set; }

        public string CategoryName { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }

    #endregion
}
