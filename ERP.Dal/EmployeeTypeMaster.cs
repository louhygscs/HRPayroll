//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeTypeMaster
    {
        public EmployeeTypeMaster()
        {
            this.EmployeeMasters = new HashSet<EmployeeMaster>();
        }
    
        public System.Guid EmployeeTypeID { get; set; }
        public string EmployeeType { get; set; }
        public decimal NoOfLeavePerMonth { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<EmployeeMaster> EmployeeMasters { get; set; }
    }
}
