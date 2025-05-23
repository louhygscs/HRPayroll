//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeMaster
    {
        public EmployeeMaster()
        {
            this.EmployeeAllowanceMaps = new HashSet<EmployeeAllowanceMap>();
            this.EmployeeAttachments = new HashSet<EmployeeAttachment>();
            this.EmployeeAttendances = new HashSet<EmployeeAttendance>();
            this.EmployeeDeductionMaps = new HashSet<EmployeeDeductionMap>();
            this.EmployeeLeaveCategories = new HashSet<EmployeeLeaveCategory>();
            this.EmployeeLoans = new HashSet<EmployeeLoan>();
            this.EmployeePaidSalaries = new HashSet<EmployeePaidSalary>();
            this.EmployeeSalaries = new HashSet<EmployeeSalary>();
            this.EmployeeWorkingDays = new HashSet<EmployeeWorkingDay>();
        }
    
        public System.Guid EmployeeID { get; set; }
        public System.Guid EmployeeTypeId { get; set; }
        public Nullable<System.Guid> EmployeeGradeId { get; set; }
        public System.Guid DepartmentId { get; set; }
        public System.Guid DesignationId { get; set; }
        public System.Guid ShiftId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string MaratialStatus { get; set; }
        public string PhotoName { get; set; }
        public Nullable<System.Guid> CountryId { get; set; }
        public Nullable<System.Guid> StateId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public int EmployeeNo { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsLeave { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public string LeaveDescription { get; set; }
        public string Previlage { get; set; }
        public string Password { get; set; }
        public string FaceTemplate { get; set; }
        public Nullable<bool> IsHavingFace { get; set; }
        public Nullable<int> FaceLength { get; set; }
        public string FingureTemplate { get; set; }
        public byte[] finger_template_data_bw { get; set; }
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
        public byte[] finger_template_data_bw1 { get; set; }
        public byte[] finger_template_data_bw2 { get; set; }
        public byte[] finger_template_data_bw3 { get; set; }
        public byte[] finger_template_data_bw4 { get; set; }
        public byte[] finger_template_data_bw5 { get; set; }
        public byte[] finger_template_data_bw6 { get; set; }
        public byte[] finger_template_data_bw7 { get; set; }
        public byte[] finger_template_data_bw8 { get; set; }
        public byte[] finger_template_data_bw9 { get; set; }
        public Nullable<bool> is_having_fingureprint { get; set; }
        public Nullable<bool> IsSend { get; set; }
        public byte[] FaceTemplateData { get; set; }
        public string PANNo { get; set; }
        public Nullable<int> TotalLeaveCount { get; set; }
        public Nullable<decimal> OverTimeAmount { get; set; }
    
        public virtual DepartmentMaster DepartmentMaster { get; set; }
        public virtual DesignationMaster DesignationMaster { get; set; }
        public virtual ICollection<EmployeeAllowanceMap> EmployeeAllowanceMaps { get; set; }
        public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual ICollection<EmployeeDeductionMap> EmployeeDeductionMaps { get; set; }
        public virtual EmployeeGradeMaster EmployeeGradeMaster { get; set; }
        public virtual ICollection<EmployeeLeaveCategory> EmployeeLeaveCategories { get; set; }
        public virtual ICollection<EmployeeLoan> EmployeeLoans { get; set; }
        public virtual EmployeeTypeMaster EmployeeTypeMaster { get; set; }
        public virtual ShiftMaster ShiftMaster { get; set; }
        public virtual ICollection<EmployeePaidSalary> EmployeePaidSalaries { get; set; }
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual ICollection<EmployeeWorkingDay> EmployeeWorkingDays { get; set; }
    }
}
