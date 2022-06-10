using System;

namespace ERP.Model
{
    public class EmployeeModel
    {
        public Guid? EmployeeId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime? JointDate { get; set; }

        public bool IsFinger { get; set; }

        public bool IsFace { get; set; }

        public int Count { get; set; }

        public byte[] finger_template_data_tft { get; set; }

        public byte[] finger_template_data_tft1 { get; set; }

        public byte[] finger_template_data_tft2 { get; set; }

        public byte[] finger_template_data_tft3 { get; set; }

        public byte[] finger_template_data_tft4 { get; set; }

        public byte[] finger_template_data_tft5 { get; set; }

        public byte[] finger_template_data_tft6 { get; set; }

        public byte[] finger_template_data_tft7 { get; set; }

        public byte[] finger_template_data_tft8 { get; set; }

        public byte[] finger_template_data_tft9 { get; set; }

        public string EnrollNo { get; set; }

        public string FaceTemplate { get; set; }

        public int FaceLength { get; set; }

        public string Password { get; set; }

        public byte[] FaceTemplateData { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public string PunchTime { get; set; }

        public DateTime? AttendanceDateTime { get; set; }

        public string PunchMethod { get; set; }

        public string DeviceName { get; set; }

        public Guid DeviceId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public decimal OverTimeAmount { get; set; }

    }
}
