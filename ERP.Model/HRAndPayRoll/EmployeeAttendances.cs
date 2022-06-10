using ERP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class EmployeeAttendances
    {
        public Guid EmployeeAttendanceID { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid FinancialYearId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public String TimeIn { get; set; }

        public int TimeInHours
        {
            get { return Convert.ToInt32(TimeIn.Substring(0, 2)); }
            set { }
        }

        public int TimeInMinutes
        {
            get { return Convert.ToInt32(TimeIn.Substring(3, 2)); }
            set { }
        }

        public String TimeInAMPM
        {
            get { return TimeIn.Substring(TimeIn.Length - 2, 2); }
            set { }
        }

        public String TimeOut { get; set; }

        public int TimeOutHours { get { return Convert.ToInt32(TimeOut.Substring(0, 2)); } set { } }

        public int TimeOutMinutes { get { return Convert.ToInt32(TimeOut.Substring(3, 2)); } set { } }

        public String TimeOutAMPM { get { return TimeOut.Substring(TimeOut.Length - 2, 2); } set { } }

        public Decimal? WorkingHours { get; set; }

        public Decimal? OverTimeHours { get; set; }

        public int AttendanceType { get; set; }

        public Decimal? Attendance { get; set; }

        public String Description { get; set; }

        public string EnrollNo { get; set; }

        public string AttendanceText
        {
            get
            {
                return AttendanceType == Convert.ToInt32(Common.AttendanceType.Leave) ? Convert.ToString(Common.AttendanceType.Leave) + " - " + string.Format("{0:0.#}", Attendance) : GlobalHelper.GetEnumDescription((AttendanceType)AttendanceType);
            }
            set { }

        }
    }
}
