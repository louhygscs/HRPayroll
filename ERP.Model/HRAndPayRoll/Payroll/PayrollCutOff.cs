using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class PayrollCutOff
    {
        public Guid PayrollCutOffId { get; set; }
        public string CutOffCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LockedDate { get; set; }
        public Guid? LockedById { get; set; }
    }
}
