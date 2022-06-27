using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class TimelogEntryModel
    {
        public Guid TimelogId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime? EntryLog { get; set; }
        public string EntryType { get; set; }
    }
}
