using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }

        public Guid EmployeeTypeId { get; set; }

        public String EmployeeType { get; set; }

        public Guid EmployeeGradeId { get; set; }

        public String EmployeeGrade { get; set; }

        public Guid DepartmentId { get; set; }

        public String Department { get; set; }

        public Guid DesignationId { get; set; }

        public String Designation { get; set; }

        public Guid ShiftId { get; set; }

        public String Shift { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public String FatherName { get; set; }

        public Boolean IsGender { get; set; }

        public String FullName
        {
            get { return (FirstName + " " + LastName); }
            set { }
        }

        public String Gender
        {
            get
            {
                return IsGender ? "Male" : "FeMale";
            }
            set { }
        }

        public String MaratialStatus { get; set; }

        public String Cast { get; set; }

        public String PhotoName { get; set; }

        public Guid CountryId { get; set; }

        public String Country { get; set; }

        public Guid StateId { get; set; }

        public String State { get; set; }

        public String City { get; set; }

        public String Address { get; set; }

        public String PinCode { get; set; }

        public String MobileNo { get; set; }

        public String PhoneNo { get; set; }

        public DateTime JoinDate { get; set; }

        public int EmployeeNo { get; set; }

        public String PFNo { get; set; }

        public String Email { get; set; }

        public String BankName { get; set; }

        public String BranchName { get; set; }

        public String AccountName { get; set; }

        public String AccountNo { get; set; }

        public DateTime LeaveDate { get; set; }

        public Boolean IsLeave { get; set; }

        public String Status
        {
            get
            {
                return IsLeave ? "Resign" : "Present";
            }
            set { }
        }

        public List<String> WorkingDays { get; set; }

        public List<EmployeeAttachments> EmployeeAttachments { get; set; }

        public String LeaveDescription { get; set; }

        public String FromTime { get; set; }

        public String ToTime { get; set; }

        public decimal OverTimeAmount { get; set; }
    }

    public class EmployeeBirthDayModel
    {

        public String Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
