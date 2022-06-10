using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class AttendanceRequest
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string LocationName { get; set; }

        public Guid EmployeeId { get; set; }

        public string AttendanceDate { get; set; }

        public string PunchMethod { get; set; }

        public string EnrollNo { get; set; }
    }
}
