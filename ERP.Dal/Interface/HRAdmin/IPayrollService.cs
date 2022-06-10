using ERP.Model;
using System;
using System.Collections.Generic;

namespace ERP.Dal.Interface
{
    public interface IPayrollService
    {
        #region Employee Profile Masters
        Result<List<EmployeeProfileModel>> GetEmployeeProfiles();

        Result<List<EmployeeProfileModel>> GetEmployeeProfilesByWorkLocationId(Guid _workLocationId);

        Result<List<EmployeeProfileModel>> GetEmployeeProfilesByWorkLocationPaymentTerm(Guid _workLocationId, string _paymentTerms);

        Result<EmployeeProfileModel> GetByEmployeeProfile(Guid p_EntityId);

        Result<EmployeeProfileModel> GetEmployeeProfileByStaffCode(string _staffCode);
        Result<bool> DeleteEmployeeProfile(Guid p_EntityId, Guid p_userId);

        Result<bool> SaveEmployeeProfile(EmployeeProfileModel p_Entity);

        #endregion

        #region Employee Schedule
        Result<List<EmployeeScheduleModelDay>> GetByEmployeeScheduleByWorkLocation(Guid p_WorkLocationId, Guid p_CutOffId);
        Result<List<EmployeeScheduleModel>> GetByEmployeeSchedule(Guid p_EmployeeId, Guid p_CutOffId);

        Result<bool> DeleteEmployeeSchedule(Guid p_EntityId);

        Result<bool> SaveEmployeeSchedule(EmployeeScheduleModel p_Entity);

        #endregion

        #region CutOff Period
        Result<List<PayrollCutOff>> GetCutOffPeriods();

        Result<PayrollCutOff> GetByCutOffPeriodId(Guid p_EntityId);

        Result<bool> DeleteCutOffPeriod(Guid p_EntityId, Guid p_userId);

        Result<bool> SaveCutOffPeriod(PayrollCutOff p_Entity);

        #endregion

        #region Payroll Masters

        Result<List<PayrollMasterModel>> GetPayrollModels();

        Result<PayrollMasterModel> GetByPayrollModelId(Guid p_EntityId);

        Result<bool> DeletePayrollMasterModel(Guid p_EntityId, Guid p_userId);

        Result<bool> SavePayrollMasterModel(PayrollMasterModel p_Entity);

        #endregion

        #region Payroll Daily Time Record
        Result<List<PayrollDTRModel>> GetEmployeeDTRbyCutOff(Guid EmployeeId, Guid CutOffPeriodId);

        Result<List<DTRRawModel>> GetEmployeeDTRRaw(Guid _EmployeeId, Guid _CutOffPeriodId);
        
        Result<List<PayrollDTRModel>> GetEmployeeDTRByWorkLocationAndCutOff(Guid _workLocationId, Guid CutOffPeriodId);
        
        Result<List<DTRCutOffTimeLogModel>> GetDTRCutOffTimeLogModel(Guid _workLocationId, Guid _CutOffPeriodId);

        Result<bool> GenerateDailyTimeRecord(Guid WorkLocationId, Guid PayrollCutOffId);
        
        Result<bool> SaveDailyTimeRecord(PayrollDTRModel p_Entity);

        Result<bool> SaveDTRRawModel(DTRRawModel p_Entity);

        #endregion

        #region Payroll Time Summary
        #endregion

        #region Payroll Summaries

        Result<List<PayrollSummaryModel>> GetPayrollSummaryModels(Guid workLocationId, Guid CutOffId, string payTerms);

        Result<PayrollSummaryModel> GetPayrollSummaryModelByCutOffPerEmployee(Guid CutOffId, Guid EmployeeId);

        Result<bool> DeletePayrollSummaryModel(Guid p_EntityId, Guid p_userId);

        Result<bool> SavePayrollSummaryModel(PayrollSummaryModel p_Entity);

        #endregion

        #region Generate Payroll Summaries
        Result<List<PayrollSummaryModel>> GeneratePayrollSummaryModels(Guid workLocationId, Guid CutOffId, string payTerms);

        #endregion
    }
}
