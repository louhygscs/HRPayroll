using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    [Serializable()]
    public class Holiday
    {
        public Guid HolidayID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate{ get; set; }

        public DateTime EndDate { get; set; }
    }
}
