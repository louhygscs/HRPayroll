using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Shift
    {
        public Guid ShiftID { get; set; }

        public string ShiftName { get; set; }

        public string FromTime { get; set; }

        public string ToTime { get; set; }
    }
}
