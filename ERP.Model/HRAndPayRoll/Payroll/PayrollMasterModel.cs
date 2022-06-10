using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    #region Employee Profile
    public class EmployeeProfileModel
    {
        public Guid EmployeeId { get; set; }
        public string StaffCode { get; set; }
        public string PicImg { get; set; }
        public string EmployeeNo { get; set; }
        public DateTime? DateHired { get; set; }

        public string FullName { 
            get { return string.Format("{0}, {1} {2}", this.LastName, this.FirstName, this.MiddleName); } 
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MartialStatus { get; set; }
        public DateTime? DateOfMarriage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        public decimal? NoOfChildren { get; set; }
        public decimal? CurrentBasicPay { get; set; }
        public decimal? HalfCurrentBasicPay { get; set; }
        public string PaymentTerms { get; set; }
        public string EmploymentStatus { get; set; }
        public string HouseStreetNo { get; set; }
        public string BarangayTownVillage { get; set; }
        public string CityMunicipalityProvince { get; set; }
        public Guid? CountryId { get; set; }
        public string Country { get; set; }
        public Guid? RegionId { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string HomeTelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string HighEducAttainment { get; set; }
        public string School { get; set; }
        public string YearsCompleted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string ContractType { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public Guid? DesignationId { get; set; }

        public string JobTitle { get; set; }
        public Guid? WorkLocationId { get; set; }
        public string WorkLocation { get; set; }
        public Guid? DepartmentId { get; set; }
        public string Department { get; set; }
        public string CostCenter { get; set; }
        public string TypeOfNCR { get; set; }
        public string TINNo { get; set; }
        public string TaxExemption { get; set; }
        public string SSSNo { get; set; }
        public string PagIbigNo { get; set; }
        public string PhilhealthNo { get; set; }
        public string DriverLicenseNo { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsResigned { get; set; }
        public DateTime? DateResigned { get; set; }
    }

    #endregion

    #region Employee Schedule
    public class EmployeeScheduleModel
    {
        public System.Guid EmpShiftId { get; set; }
        public Nullable<System.Guid> ShiftId { get; set; }
        public string Shift { get; set; }
        public Nullable<System.Guid> EmployeeId { get; set; }
        public Nullable<System.Guid> CutOffId { get; set; }

        public string ActualDateDayName { get {

                string _dayname = string.Empty;

                if(this.ActualDate.HasValue)
                {
                    _dayname = this.ActualDate.Value.ToString("dddd");
                }

                return _dayname;
            }
        }
        public Nullable<System.DateTime> ActualDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public class EmployeeScheduleModelDay
    {
        public Guid EmpShiftId { get; set; }
        public Guid WorkLocationId { get; set; }
        public Guid CutOffId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateHired { get; set; }
        public Guid DesignationId { get; set; }
        public string Designation { get; set; }
        
        public Guid ShifId_Day1 { get; set; }
        public string ShiftDesc_Day1 { get; set; }

        public Guid ShifId_Day2 { get; set; }
        public string ShiftDesc_Day2 { get; set; }

        public Guid ShifId_Day3 { get; set; }
        public string ShiftDesc_Day3 { get; set; }

        public Guid ShifId_Day4 { get; set; }
        public string ShiftDesc_Day4 { get; set; }

        public Guid ShifId_Day5 { get; set; }
        public string ShiftDesc_Day5 { get; set; }

        public Guid ShifId_Day6 { get; set; }
        public string ShiftDesc_Day6 { get; set; }

        public Guid ShifId_Day7 { get; set; }
        public string ShiftDesc_Day7 { get; set; }

        public Guid ShifId_Day8 { get; set; }
        public string ShiftDesc_Day8 { get; set; }

        public Guid ShifId_Day9 { get; set; }
        public string ShiftDesc_Day9 { get; set; }

        public Guid ShifId_Day10 { get; set; }
        public string ShiftDesc_Day10 { get; set; }

        public Guid ShifId_Day11 { get; set; }
        public string ShiftDesc_Day11 { get; set; }

        public Guid ShifId_Day12 { get; set; }
        public string ShiftDesc_Day12 { get; set; }

        public Guid ShifId_Day13 { get; set; }
        public string ShiftDesc_Day13 { get; set; }

        public Guid ShifId_Day14 { get; set; }
        public string ShiftDesc_Day14 { get; set; }

        public Guid ShifId_Day15 { get; set; }
        public string ShiftDesc_Day15 { get; set; }
    }

    #endregion

    #region CutOff Period
    public class PayrollCutOffModel
    {
        public Guid PayrollCutOffId { get; set; }
        public string CutOffCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LockedDate { get; set; }
        public Guid? LockedById { get; set; }
    }
    #endregion

    #region Payroll Master

    public class PayrollMasterModel
    {
        public System.Guid PayrollId { get; set; }
        public string PayrollCode { get; set; }
        public Nullable<System.Guid> WorkLocationId { get; set; }
        public Nullable<System.Guid> CutOffId { get; set; }
        public string PaymentTerms { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<bool> IsLockedDTR { get; set; }
        public Nullable<System.DateTime> LockedDTRDate { get; set; }
        public Nullable<System.Guid> LockedDTRBy { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<System.Guid> ApprovedBy { get; set; }
        public Nullable<bool> IsLockedPTSR { get; set; }
        public Nullable<System.DateTime> LockedPTSRDate { get; set; }
        public Nullable<System.Guid> LockedPTSRBy { get; set; }
        public Nullable<bool> IsBankFileGenerated { get; set; }
        public Nullable<System.DateTime> GeneratedDate { get; set; }
        public Nullable<System.Guid> GeneratedBy { get; set; }
        public string BankType { get; set; }
        public Nullable<System.Guid> BankFileId { get; set; }
        public Nullable<decimal> TotalWorks { get; set; }
        public Nullable<decimal> TotalEmployee { get; set; }
        public Nullable<decimal> TotalDeduction { get; set; }
        public Nullable<decimal> TotalSSS { get; set; }
        public Nullable<decimal> TotalPHIC { get; set; }
        public Nullable<decimal> TotalHDMF { get; set; }
        public Nullable<decimal> TotalTax { get; set; }
    }
    #endregion

    #region Payroll Daily Record
    public class PayrollDTRModel
    {
        public System.Guid DTRId { get; set; }
        public System.Guid PayrollCutOffId { get; set; }
        public System.Guid? ScheduleId { get; set; }
        public System.Guid ScheduleName { get; set; }
        public Nullable<System.DateTime> DateHired { get; set; }
        public System.Guid EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0}, {1} {2}", this.LastName, this.FirstName, this.MiddleName); }
        }

        public string ActualDateDayName
        {
            get
            {
                string _dayname = string.Empty;

                if (this.ActualDate.HasValue)
                {
                    _dayname = this.ActualDate.Value.ToString("dddd");
                }

                return _dayname;
            }
        }
        public Nullable<System.DateTime> ActualDate { get; set; }
        public Nullable<decimal> WorkingHrs { get; set; }
        public Nullable<decimal> LateHrs { get; set; }
        public Nullable<decimal> OvertimeHrs { get; set; }
        public Nullable<decimal> AdjustHrs { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public Nullable<System.TimeSpan> timein { get; set; }
        public Nullable<System.TimeSpan> timeout { get; set; }
        public string status { get; set; }
    }

    public class PayrollDTRRawTimeLogModel
    {
        public string LineNo { get; set; }
        public string StaffCode { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
        public string UserId { get; set; }
        public string Week { get; set; }
        public DateTime StrDate { get; set; }
        public TimeSpan StrTime { get; set; }
        public string MachineId { get; set; }
        public string Remark { get; set; }
    }

    public  class DTRRawModel
    {
        public System.Guid DTRRawId { get; set; }
        public Nullable<System.Guid> CutOffId { get; set; }
        public Nullable<System.Guid> EmployeeId { get; set; }
        public string StaffCode { get; set; }
        public string TimeType { get; set; }
        public int TimeTypeOrder { get; set; }

        public Guid ScheduleId { get; set; }
        public string ShiftDesc { get; set; }

        public Nullable<System.DateTime> ActualDate { get; set; }
        public string ActualDateDayName
        {
            get
            {
                string _dayname = string.Empty;

                if (this.ActualDate.HasValue)
                {
                    _dayname = this.ActualDate.Value.ToString("dddd");
                }

                return _dayname;
            }
        }
        public Nullable<System.TimeSpan> ActualTime { get; set; }
        public string FromType { get; set; }
        public int RawOrder { get; set; }
    }

    public class DTRCutOffTimeLogModel
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }

        #region Days
        public TimeSpan Day1TimeIn { get; set; }
        public TimeSpan Day1TimeOut { get; set; }
        public TimeSpan Day1TtlHrs { get; set; }

        public TimeSpan Day2TimeIn { get; set; }
        public TimeSpan Day2TimeOut { get; set; }
        public TimeSpan Day2TtlHrs { get; set; }

        public TimeSpan Day3TimeIn { get; set; }
        public TimeSpan Day3TimeOut { get; set; }
        public TimeSpan Day3TtlHrs { get; set; }

        public TimeSpan Day4TimeIn { get; set; }
        public TimeSpan Day4TimeOut { get; set; }
        public TimeSpan Day4TtlHrs { get; set; }

        public TimeSpan Day5TimeIn { get; set; }
        public TimeSpan Day5TimeOut { get; set; }
        public TimeSpan Day5TtlHrs { get; set; }

        public TimeSpan Day6TimeIn { get; set; }
        public TimeSpan Day6TimeOut { get; set; }
        public TimeSpan Day6TtlHrs { get; set; }

        public TimeSpan Day7TimeIn { get; set; }
        public TimeSpan Day7TimeOut { get; set; }
        public TimeSpan Day7TtlHrs { get; set; }

        public TimeSpan Day8TimeIn { get; set; }
        public TimeSpan Day8TimeOut { get; set; }
        public TimeSpan Day8TtlHrs { get; set; }

        public TimeSpan Day9TimeIn { get; set; }
        public TimeSpan Day9TimeOut { get; set; }
        public TimeSpan Day9TtlHrs { get; set; }

        public TimeSpan Day10TimeIn { get; set; }
        public TimeSpan Day10TimeOut { get; set; }
        public TimeSpan Day10TtlHrs { get; set; }

        public TimeSpan Day11TimeIn { get; set; }
        public TimeSpan Day11TimeOut { get; set; }
        public TimeSpan Day11TtlHrs { get; set; }

        public TimeSpan Day12TimeIn { get; set; }
        public TimeSpan Day12TimeOut { get; set; }
        public TimeSpan Day12TtlHrs { get; set; }

        public TimeSpan Day13TimeIn { get; set; }
        public TimeSpan Day13TimeOut { get; set; }
        public TimeSpan Day13TtlHrs { get; set; }

        public TimeSpan Day14TimeIn { get; set; }
        public TimeSpan Day14TimeOut { get; set; }
        public TimeSpan Day14TtlHrs { get; set; }

        public TimeSpan Day15TimeIn { get; set; }
        public TimeSpan Day15TimeOut { get; set; }
        public TimeSpan Day15TtlHrs { get; set; }
        #endregion

    }
    #endregion

    #region Payroll Time Summary
    #endregion

    #region Payroll Summaries
    public class PayrollSummaryModel
    {
        public System.Guid PayrollTimeId { get; set; }  //Unique Id
        public Nullable<System.DateTime> DateHired { get; set; }
        public System.Guid EmployeeId { get; set; }     //Employee Id in employeeProfile
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0}, {1} {2}", this.LastName, this.FirstName, this.MiddleName); }
        }

        public System.Guid PayrollId { get; set; }      //Payrol Id in Payroll Master
        public System.Guid? WorkLocationId { get; set; } //WorkLocation Id
        //public System.Guid ScheduleId { get; set; }     //Scheduke Id

        public Nullable<decimal> NoofDays { get; set; }
        public Nullable<decimal> TotalBasic { get; set; }

        public Nullable<decimal> gMonthlyRate { get; set; }
        public Nullable<decimal> gHalfMonthEarning { get; set; }
        public Nullable<decimal> gRTDays { get; set; }
        public Nullable<decimal> gRTHrs { get; set; }
        
        public Nullable<decimal> gRTOTHrs { get; set; }
        public Nullable<decimal> gRTOTAmt { get; set; }

        public Nullable<decimal> gNP1Hrs { get; set; }
        public Nullable<decimal> gNP1Amt { get; set; }

        public Nullable<decimal> gNP2Hrs { get; set; }
        public Nullable<decimal> gNP2Amt { get; set; }

        public Nullable<decimal> gNP3Hrs { get; set; }
        public Nullable<decimal> gNP3Amt { get; set; }

        public Nullable<decimal> gNP4Hrs { get; set; }
        public Nullable<decimal> gNP4Amt { get; set; }

        public Nullable<decimal> gRDHrs { get; set; }
        public Nullable<decimal> gRDAmt { get; set; }
        public Nullable<decimal> gSHHrs { get; set; }
        public Nullable<decimal> gSHAmt { get; set; }
        public Nullable<decimal> gPosAllowance { get; set; }
        public Nullable<decimal> gSIL { get; set; }
        public Nullable<decimal> gAdjAmt { get; set; }
        public Nullable<decimal> gAdjustmentDays { get; set; }
        public Nullable<decimal> g30PerOT { get; set; }
        public Nullable<decimal> ttlGross { get; set; }
        public Nullable<decimal> dCigna { get; set; }
        public Nullable<decimal> dPhicPrem { get; set; }
        public Nullable<decimal> dHDMF { get; set; }
        public Nullable<decimal> dHDMFLoan { get; set; }
        public Nullable<decimal> dMotorLoan { get; set; }
        public Nullable<decimal> dCashAdv { get; set; }
        public Nullable<decimal> dSplFunds { get; set; }
        public Nullable<decimal> gdAbsDay { get; set; }
        public Nullable<decimal> dAbsHrs { get; set; }
        public Nullable<decimal> TtlDeduction { get; set; }
        public Nullable<decimal> TtlNetSalary { get; set; }
        public Nullable<System.DateTime> DateReceived { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
    }
    #endregion

}
