using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class State
    {
        public Guid StateID { get; set; }
        public Guid? CountryID { get; set; }
        public string StateName { get; set; }
    }
}
