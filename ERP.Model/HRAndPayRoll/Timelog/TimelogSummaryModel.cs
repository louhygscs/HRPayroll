using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class TimelogSummaryModel
    {
        public System.Guid TimelogId { get; set; }
        public System.Guid EmployeeId { get; set; }
        public Nullable<System.DateTime> logindate { get; set; }
        public Nullable<System.TimeSpan> logintime { get; set; }
        public Nullable<System.DateTime> breakindate { get; set; }
        public Nullable<System.TimeSpan> breakintime { get; set; }
        public Nullable<System.DateTime> breakoutdate { get; set; }
        public Nullable<System.TimeSpan> breakouttime { get; set; }
        public Nullable<System.DateTime> overindate { get; set; }
        public Nullable<System.TimeSpan> overintime { get; set; }
        public Nullable<System.DateTime> overoutdate { get; set; }
        public Nullable<System.TimeSpan> overouttime { get; set; }
        public Nullable<System.DateTime> logoutdate { get; set; }
        public Nullable<System.TimeSpan> logouttime { get; set; }
        public bool isactive { get; set; }
    }
}
