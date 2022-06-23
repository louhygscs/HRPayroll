using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common
{
    public enum Role
    {
        [Description("c2737ea6-bfe8-401d-ad15-43147de775ca")]
        Administrator = 1,
        [Description("5417f6e8-9dab-4a8c-9c35-cb197193be13")]
        Employee = 2,
    };

    public enum OperationType
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        Resign = 4,
    };

    public enum TableType
    {
        UserMaster = 1,
        DepartmentMaster = 2,
        EmployeeTypeMaster = 3,
        DesignationMaster = 4,
        LeaveCategoryMaster = 5,
        EmployeeGradeMaster = 6,
        AllowanceMaster = 7,
        DeductionMaster = 8,
        ShiftMaster = 9,
        HolidayMaster = 10,
        EmployeeMaster = 11,
        EmployeeLoan = 12,
        EmployeeSalary = 13,
        EmployeeLeaveCategory = 14,
        CompanyMaster = 15,
        EmployeeAttendance = 16,
        EmployeePaidSalary = 17,
        FinancialYearMaster = 18,
        DeviceMaster = 19,
        LicenseGenerate = 20,
        EducationMaster = 21,
        IterviewMaster = 22,
        CountryMaster = 23,
        StateMaster = 24,
        CategoryMaster = 25,
        RoleMaster = 26
    };

    public enum EmployeeAttachmentType
    {
        Resume = 1,
        OfferLetter = 2,
        JoiningLetter = 3,
        ContractPaper = 4,
        IDProff = 5,
        OtherDocument = 6,
        ResignLetter = 7,
    };

    public enum UploadFileFolderName
    {
        EmployeePhoto = 1,
        EmployeeDocument = 2,
        CompanyLogo = 3,
        InterviewDocument = 4,
    };

    public enum AttendanceType
    {
        [Description("Leave")]
        Leave = 1,
        [Description("Weekly Off")]
        WeeklyOff = 2,
        [Description("Holiday")]
        Holiday = 3,
        [Description("Present")]
        Present = 4,
        [Description("Absent")]
        Absent = 0

    };

    public enum ConnectionStatusValue
    {
        Connected = 1,
        DisConnected = 2,
    }

    public enum PunchMethod
    {
        IN = 1,
        OUT = 2,
    }

    public enum PunchType
    {
        WEB = 1,
        DEVICE = 2,
    }

    public enum InterviewAttachmentType
    {
        Resume = 1,
        Certificate = 2,
    }

    public enum InterviewStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Interview Scheduled")]
        Interview_Scheduled = 2,
        [Description("Will Join")]
        Will_Join = 3,
        [Description("Rejected")]
        Rejected = 4,
        [Description("Lack of Knowledge")]
        Lack_of_Knowledge = 5,
        [Description("In Queue")]
        In_Queue = 6,
        [Description("Salary Unexpected")]
        Salary_Unexpected = 7,
        [Description("Other")]
        Other = 8,
    }

    public enum SalaryType
    {
        Monthly = 0,
        Weekly = 1,
        Daily = 2,
        Hourly = 3
    }

}
