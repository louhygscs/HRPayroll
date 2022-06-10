using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class EmployeeAttendanceDevices
    {
        public Guid EmployeeAttendanceID { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid DeviceId { get; set; }

        public string EnrollNo { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public DateTime? AttendanceDateTime { get; set; }

        public string PunchTime { get; set; }

        public string VerifyMethod { get; set; }

        public string PunchMethod { get; set; }

        public Guid? ShiftId { get; set; }

        public string PunchType { get; set; }

        public string EmployeeName { get; set; }

        public int Month { get; set; }

        public string Department { get; set; }
    }

    public class EmployeeAttendanceResult
    {
        public string AttendanceDateValue { get; set; }

        public string Attendances { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int AttendanceType { get; set; }

        public string WorkingHours { get; set; }

        public string PunchMethod { get; set; }

        public decimal OvertimeHours { get; set; }

        public bool IsApproved { get; set; }

    }

    public class EmployeeAttendanceOverTimeResult
    {
        public string AttendanceDateValue { get; set; }

        public string Attendances { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int AttendanceType { get; set; }

        public string FinalHours { get; set; }

        public decimal? FinalHoursInDecimal { get; set; }

        public string OverTimeHours { get; set; }

        public decimal? OverTimeHoursInDecimal { get; set; }

    }

    public class DeviceEmployeeTotalHours
    {
        public string FullName { get; set; }

        public string Department { get; set; }

        public string Month { get; set; }

        public string WorkingHours { get; set; }

    }
}
