using System;

namespace ERP.Model
{
    public class EmployeeAttendanceDeviceModel
    {
        public Guid EmployeeId { get; set; }

        public Guid DeviceId { get; set; }

        public string PunchTime { get; set; }

        public string VerifyMethod { get; set; }

        public string EnrollNo { get; set; }

        public string PunchMethod { get; set; }

        public string PunchType { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public DateTime? AttendanceDateTime { get; set; }

        public Guid? ShiftId { get; set; } 

    }
}
