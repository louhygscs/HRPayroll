using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Dal.Implemention
{
    public class PayrollService : IPayrollService
    {
        #region Employee Profile

        public Result<List<EmployeeProfileModel>> GetEmployeeProfiles()
        {
            Result<List<EmployeeProfileModel>> _Result = new Result<List<EmployeeProfileModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 join empdept in dbContext.DepartmentMasters on e.DepartmentId equals empdept.DepartmentID
                                 join emppos in dbContext.DesignationMasters on e.DesignationId equals emppos.DesignationID
                                 join empworkloc in dbContext.CompanyMasters on e.WorkLocationId equals empworkloc.CompanyID
                                 //join workcat in dbContext.CategoryMasters on empworkloc.CategoryId equals workcat.CategoryId
                                 where e.IsActive == true
                                 select new EmployeeProfileModel
                                 {
                                     EmployeeId = e.EmployeeId,
                                     PicImg = e.PicImg,
                                     EmployeeNo = e.EmployeeNo,
                                     StaffCode = e.StaffCode,
                                     DateHired = e.DateHired,
                                     //FullName                 = string.Format("{0}, {1} {2}", e.LastName, e.FirstName, e.MiddleName),
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     Gender = e.Gender,
                                     MartialStatus = e.MartialStatus,
                                     DateOfMarriage = e.DateOfMarriage,
                                     DateOfBirth = e.DateOfBirth,
                                     BirthPlace = e.BirthPlace,
                                     NoOfChildren = e.NoOfChildren,
                                     CurrentBasicPay = e.CurrentBasicPay,
                                     PaymentTerms = e.PaymentTerms,
                                     EmploymentStatus = e.EmploymentStatus,
                                     HouseStreetNo = e.HouseStreetNo,
                                     BarangayTownVillage = e.BarangayTownVillage,
                                     CityMunicipalityProvince = e.CityMunicipalityProvince,
                                     CountryId = e.County,
                                     RegionId = e.Region,
                                     PostalCode = e.PostalCode,
                                     HomeTelephoneNo = e.HomeTelephoneNo,
                                     MobileNo = e.MobileNo,
                                     EmailAddress = e.EmailAddress,
                                     HighEducAttainment = e.HighEducAttainment,
                                     School = e.School,
                                     YearsCompleted = e.YearsCompleted,
                                     DateCompleted = e.DateCompleted,
                                     ContractType = e.ContractType,
                                     ContractStartDate = e.ContractStartDate,
                                     ContractEndDate = e.ContractEndDate,
                                     DesignationId = e.DesignationId,
                                     JobTitle = emppos.Designation,
                                     WorkLocationId = e.WorkLocationId,
                                     WorkLocation = empworkloc.CompanyName,
                                     DepartmentId = e.DepartmentId,
                                     Department = empdept.Department,
                                     CostCenter = e.CostCenter,
                                     TypeOfNCR = e.TypeOfNCR,
                                     TINNo = e.TINNo,
                                     TaxExemption = e.TaxExemption,
                                     SSSNo = e.SSSNo,
                                     PagIbigNo = e.PagIbigNo,
                                     PhilhealthNo = e.PhilhealthNo,
                                     DriverLicenseNo = e.DriverLicenseNo,
                                     Remarks = e.Remarks,
                                     CreatedDate = e.CreatedDate,
                                     CreatedBy = e.CreatedBy,
                                     ModifiedDate = e.ModifiedDate,
                                     ModifiedBy = e.ModifiedBy,
                                     IsActive = e.IsActive,
                                     IsResigned = e.IsResigned,
                                     DateResigned = e.DateResigned
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<EmployeeProfileModel>> GetEmployeeProfilesByWorkLocationId(Guid _workLocationId)
        {
            Result<List<EmployeeProfileModel>> _Result = new Result<List<EmployeeProfileModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 join empdept in dbContext.DepartmentMasters on e.DepartmentId equals empdept.DepartmentID
                                 join emppos in dbContext.DesignationMasters on e.DesignationId equals emppos.DesignationID
                                 join empworkloc in dbContext.CompanyMasters on e.WorkLocationId equals empworkloc.CompanyID
                                 //join workcat in dbContext.CategoryMasters on empworkloc.CategoryId equals workcat.CategoryId
                                 where e.IsActive == true && e.WorkLocationId == _workLocationId
                                 select new EmployeeProfileModel
                                 {
                                     EmployeeId = e.EmployeeId,
                                     PicImg = e.PicImg,
                                     EmployeeNo = e.EmployeeNo,
                                     StaffCode = e.StaffCode,
                                     DateHired = e.DateHired,
                                     //FullName                 = string.Format("{0}, {1} {2}", e.LastName, e.FirstName, e.MiddleName),
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     Gender = e.Gender,
                                     MartialStatus = e.MartialStatus,
                                     DateOfMarriage = e.DateOfMarriage,
                                     DateOfBirth = e.DateOfBirth,
                                     BirthPlace = e.BirthPlace,
                                     NoOfChildren = e.NoOfChildren,
                                     CurrentBasicPay = e.CurrentBasicPay,
                                     PaymentTerms = e.PaymentTerms,
                                     EmploymentStatus = e.EmploymentStatus,
                                     HouseStreetNo = e.HouseStreetNo,
                                     BarangayTownVillage = e.BarangayTownVillage,
                                     CityMunicipalityProvince = e.CityMunicipalityProvince,
                                     CountryId = e.County,
                                     RegionId = e.Region,
                                     PostalCode = e.PostalCode,
                                     HomeTelephoneNo = e.HomeTelephoneNo,
                                     MobileNo = e.MobileNo,
                                     EmailAddress = e.EmailAddress,
                                     HighEducAttainment = e.HighEducAttainment,
                                     School = e.School,
                                     YearsCompleted = e.YearsCompleted,
                                     DateCompleted = e.DateCompleted,
                                     ContractType = e.ContractType,
                                     ContractStartDate = e.ContractStartDate,
                                     ContractEndDate = e.ContractEndDate,
                                     DesignationId = e.DesignationId,
                                     JobTitle = emppos.Designation,
                                     WorkLocationId = e.WorkLocationId,
                                     WorkLocation = empworkloc.CompanyName,
                                     DepartmentId = e.DepartmentId,
                                     Department = empdept.Department,
                                     CostCenter = e.CostCenter,
                                     TypeOfNCR = e.TypeOfNCR,
                                     TINNo = e.TINNo,
                                     TaxExemption = e.TaxExemption,
                                     SSSNo = e.SSSNo,
                                     PagIbigNo = e.PagIbigNo,
                                     PhilhealthNo = e.PhilhealthNo,
                                     DriverLicenseNo = e.DriverLicenseNo,
                                     Remarks     = e.Remarks,
                                     CreatedDate    = e.CreatedDate,
                                     CreatedBy      = e.CreatedBy,
                                     ModifiedDate   = e.ModifiedDate,
                                     ModifiedBy     = e.ModifiedBy,
                                     IsActive       = e.IsActive,
                                     IsResigned     = e.IsResigned,
                                     DateResigned   = e.DateResigned
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<EmployeeProfileModel>> GetEmployeeProfilesByWorkLocationPaymentTerm(Guid _workLocationId, string _paymentTerms)
        {
            Result<List<EmployeeProfileModel>> _Result = new Result<List<EmployeeProfileModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 join empdept in dbContext.DepartmentMasters on e.DepartmentId equals empdept.DepartmentID
                                 join emppos in dbContext.DesignationMasters on e.DesignationId equals emppos.DesignationID
                                 join empworkloc in dbContext.CompanyMasters on e.WorkLocationId equals empworkloc.CompanyID
                                 where e.IsActive == true && e.WorkLocationId == _workLocationId && e.PaymentTerms == _paymentTerms
                                 select new EmployeeProfileModel
                                 {
                                     EmployeeId = e.EmployeeId,
                                     PicImg = e.PicImg,
                                     EmployeeNo = e.EmployeeNo,
                                     StaffCode = e.StaffCode,
                                     DateHired = e.DateHired,
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     Gender = e.Gender,
                                     MartialStatus = e.MartialStatus,
                                     DateOfMarriage = e.DateOfMarriage,
                                     DateOfBirth = e.DateOfBirth,
                                     BirthPlace = e.BirthPlace,
                                     NoOfChildren = e.NoOfChildren,
                                     CurrentBasicPay = e.CurrentBasicPay,
                                     PaymentTerms = e.PaymentTerms,
                                     EmploymentStatus = e.EmploymentStatus,
                                     HouseStreetNo = e.HouseStreetNo,
                                     BarangayTownVillage = e.BarangayTownVillage,
                                     CityMunicipalityProvince = e.CityMunicipalityProvince,
                                     CountryId = e.County,
                                     RegionId = e.Region,
                                     PostalCode = e.PostalCode,
                                     HomeTelephoneNo = e.HomeTelephoneNo,
                                     MobileNo = e.MobileNo,
                                     EmailAddress = e.EmailAddress,
                                     HighEducAttainment = e.HighEducAttainment,
                                     School = e.School,
                                     YearsCompleted = e.YearsCompleted,
                                     DateCompleted = e.DateCompleted,
                                     ContractType = e.ContractType,
                                     ContractStartDate = e.ContractStartDate,
                                     ContractEndDate = e.ContractEndDate,
                                     DesignationId = e.DesignationId,
                                     JobTitle = emppos.Designation,
                                     WorkLocationId = e.WorkLocationId,
                                     WorkLocation = empworkloc.CompanyName,
                                     DepartmentId = e.DepartmentId,
                                     Department = empdept.Department,
                                     CostCenter = e.CostCenter,
                                     TypeOfNCR = e.TypeOfNCR,
                                     TINNo = e.TINNo,
                                     TaxExemption = e.TaxExemption,
                                     SSSNo = e.SSSNo,
                                     PagIbigNo = e.PagIbigNo,
                                     PhilhealthNo = e.PhilhealthNo,
                                     DriverLicenseNo = e.DriverLicenseNo,
                                     Remarks = e.Remarks,
                                     CreatedDate = e.CreatedDate,
                                     CreatedBy = e.CreatedBy,
                                     ModifiedDate = e.ModifiedDate,
                                     ModifiedBy = e.ModifiedBy,
                                     IsActive = e.IsActive,
                                     IsResigned = e.IsResigned,
                                     DateResigned = e.DateResigned
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<EmployeeProfileModel> GetByEmployeeProfile(Guid p_EntityId)
        {
            Result<EmployeeProfileModel> _Result = new Result<EmployeeProfileModel>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 join empdept in dbContext.DepartmentMasters on e.DepartmentId equals empdept.DepartmentID
                                 join emppos in dbContext.DesignationMasters on e.DesignationId equals emppos.DesignationID
                                 join empCnt in dbContext.CountryMasters on e.County equals empCnt.CountryID
                                 join empReg in dbContext.StateMasters on e.Region equals empReg.StateID
                                 where e.IsActive == true
                                 where e.EmployeeId == p_EntityId
                                 select new EmployeeProfileModel
                                 {
                                     EmployeeId = e.EmployeeId,
                                     PicImg = e.PicImg,
                                     EmployeeNo = e.EmployeeNo,
                                     StaffCode = e.StaffCode,
                                     DateHired = e.DateHired,
                                     //FullName   = string.Format("{0}, {1} {2}", e.LastName, e.FirstName, e.MiddleName),
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     Gender = e.Gender,
                                     MartialStatus = e.MartialStatus,
                                     DateOfMarriage = e.DateOfMarriage,
                                     DateOfBirth = e.DateOfBirth,
                                     BirthPlace = e.BirthPlace,
                                     NoOfChildren = e.NoOfChildren,
                                     CurrentBasicPay = e.CurrentBasicPay,
                                     PaymentTerms = e.PaymentTerms,
                                     EmploymentStatus = e.EmploymentStatus,
                                     HouseStreetNo = e.HouseStreetNo,
                                     BarangayTownVillage = e.BarangayTownVillage,
                                     CityMunicipalityProvince = e.CityMunicipalityProvince,
                                     CountryId = e.County,
                                     Country = empCnt.CountryName,
                                     RegionId = e.Region,
                                     Region = empReg.StateName,
                                     PostalCode = e.PostalCode,
                                     HomeTelephoneNo = e.HomeTelephoneNo,
                                     MobileNo = e.MobileNo,
                                     EmailAddress = e.EmailAddress,
                                     HighEducAttainment = e.HighEducAttainment,
                                     School = e.School,
                                     YearsCompleted = e.YearsCompleted,
                                     DateCompleted = e.DateCompleted,
                                     ContractType = e.ContractType,
                                     ContractStartDate = e.ContractStartDate,
                                     ContractEndDate = e.ContractEndDate,
                                     DesignationId = e.DesignationId,
                                     WorkLocationId = e.WorkLocationId,
                                     DepartmentId = e.DepartmentId,
                                     CostCenter = e.CostCenter,
                                     TypeOfNCR = e.TypeOfNCR,
                                     TINNo = e.TINNo,
                                     TaxExemption = e.TaxExemption,
                                     SSSNo = e.SSSNo,
                                     PagIbigNo = e.PagIbigNo,
                                     PhilhealthNo = e.PhilhealthNo,
                                     DriverLicenseNo = e.DriverLicenseNo,
                                     Remarks = e.Remarks,
                                     CreatedDate = e.CreatedDate,
                                     CreatedBy = e.CreatedBy,
                                     ModifiedDate = e.ModifiedDate,
                                     ModifiedBy = e.ModifiedBy,
                                     IsActive = e.IsActive,
                                     IsResigned = e.IsResigned,
                                     DateResigned = e.DateResigned
                                 };

                    EmployeeProfileModel _entity = _Query.FirstOrDefault();

                    if (_entity != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _entity;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<EmployeeProfileModel> GetEmployeeProfileByStaffCode(string _staffCode)
        {
            Result<EmployeeProfileModel> _Result = new Result<EmployeeProfileModel>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeProfiles
                                 join empdept in dbContext.DepartmentMasters on e.DepartmentId equals empdept.DepartmentID
                                 join emppos in dbContext.DesignationMasters on e.DesignationId equals emppos.DesignationID
                                 join empCnt in dbContext.CountryMasters on e.County equals empCnt.CountryID
                                 join empReg in dbContext.StateMasters on e.Region equals empReg.StateID
                                 where e.IsActive == true
                                 where e.StaffCode == _staffCode
                                 select new EmployeeProfileModel
                                 {
                                     EmployeeId = e.EmployeeId,
                                     PicImg = e.PicImg,
                                     EmployeeNo = e.EmployeeNo,
                                     StaffCode = e.StaffCode,
                                     DateHired = e.DateHired,
                                     //FullName   = string.Format("{0}, {1} {2}", e.LastName, e.FirstName, e.MiddleName),
                                     FirstName = e.FirstName,
                                     MiddleName = e.MiddleName,
                                     LastName = e.LastName,
                                     Gender = e.Gender,
                                     MartialStatus = e.MartialStatus,
                                     DateOfMarriage = e.DateOfMarriage,
                                     DateOfBirth = e.DateOfBirth,
                                     BirthPlace = e.BirthPlace,
                                     NoOfChildren = e.NoOfChildren,
                                     CurrentBasicPay = e.CurrentBasicPay,
                                     PaymentTerms = e.PaymentTerms,
                                     EmploymentStatus = e.EmploymentStatus,
                                     HouseStreetNo = e.HouseStreetNo,
                                     BarangayTownVillage = e.BarangayTownVillage,
                                     CityMunicipalityProvince = e.CityMunicipalityProvince,
                                     CountryId = e.County,
                                     Country = empCnt.CountryName,
                                     RegionId = e.Region,
                                     Region = empReg.StateName,
                                     PostalCode = e.PostalCode,
                                     HomeTelephoneNo = e.HomeTelephoneNo,
                                     MobileNo = e.MobileNo,
                                     EmailAddress = e.EmailAddress,
                                     HighEducAttainment = e.HighEducAttainment,
                                     School = e.School,
                                     YearsCompleted = e.YearsCompleted,
                                     DateCompleted = e.DateCompleted,
                                     ContractType = e.ContractType,
                                     ContractStartDate = e.ContractStartDate,
                                     ContractEndDate = e.ContractEndDate,
                                     DesignationId = e.DesignationId,
                                     WorkLocationId = e.WorkLocationId,
                                     DepartmentId = e.DepartmentId,
                                     CostCenter = e.CostCenter,
                                     TypeOfNCR = e.TypeOfNCR,
                                     TINNo = e.TINNo,
                                     TaxExemption = e.TaxExemption,
                                     SSSNo = e.SSSNo,
                                     PagIbigNo = e.PagIbigNo,
                                     PhilhealthNo = e.PhilhealthNo,
                                     DriverLicenseNo = e.DriverLicenseNo,
                                     Remarks = e.Remarks,
                                     CreatedDate = e.CreatedDate,
                                     CreatedBy = e.CreatedBy,
                                     ModifiedDate = e.ModifiedDate,
                                     ModifiedBy = e.ModifiedBy,
                                     IsActive = e.IsActive,
                                     IsResigned = e.IsResigned,
                                     DateResigned = e.DateResigned
                                 };

                    EmployeeProfileModel _entity = _Query.FirstOrDefault();

                    if (_entity != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _entity;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> DeleteEmployeeProfile(Guid p_EntityId, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.PayrollCutOffMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        EmployeeProfile _EntityDelete = dbContext.EmployeeProfiles.Where(d => d.EmployeeId == p_EntityId).FirstOrDefault();

                        if (_EntityDelete != null)
                        {
                            _EntityDelete.IsActive = false;

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;

        }

        public Result<bool> SaveEmployeeProfile(EmployeeProfileModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                EmployeeProfile _entry = dbContext.EmployeeProfiles.Where(x => x.EmployeeNo == p_Entity.EmployeeNo).FirstOrDefault();

                if (_entry != null)
                {
                    _entry.EmployeeNo = p_Entity.EmployeeNo;
                    _entry.StaffCode = p_Entity.StaffCode;
                    _entry.DateHired = p_Entity.DateHired;
                    _entry.PicImg = p_Entity.PicImg;

                    _entry.FirstName = p_Entity.FirstName;
                    _entry.MiddleName = p_Entity.MiddleName;
                    _entry.LastName = p_Entity.LastName;

                    _entry.Gender = p_Entity.Gender;
                    _entry.MartialStatus = p_Entity.MartialStatus;
                    _entry.DateOfMarriage = p_Entity.DateOfMarriage;

                    _entry.DateOfBirth = p_Entity.DateOfBirth;
                    _entry.BirthPlace = p_Entity.BirthPlace;
                    _entry.NoOfChildren = p_Entity.NoOfChildren;

                    _entry.CurrentBasicPay = p_Entity.CurrentBasicPay;
                    _entry.PaymentTerms = p_Entity.PaymentTerms;
                    _entry.EmploymentStatus = p_Entity.EmploymentStatus;

                    _entry.HouseStreetNo = p_Entity.HouseStreetNo;
                    _entry.BarangayTownVillage = p_Entity.BarangayTownVillage;
                    _entry.CityMunicipalityProvince = p_Entity.CityMunicipalityProvince;

                    _entry.County = p_Entity.CountryId;
                    _entry.Region = p_Entity.RegionId;
                    _entry.PostalCode = p_Entity.PostalCode;

                    _entry.HomeTelephoneNo = p_Entity.HomeTelephoneNo;
                    _entry.MobileNo = p_Entity.MobileNo;
                    _entry.EmailAddress = p_Entity.EmailAddress;

                    _entry.HighEducAttainment = p_Entity.HighEducAttainment;
                    _entry.School = p_Entity.School;
                    _entry.YearsCompleted = p_Entity.YearsCompleted;

                    _entry.DateCompleted = p_Entity.DateCompleted;
                    _entry.ContractType = p_Entity.ContractType;
                    _entry.ContractStartDate = p_Entity.ContractStartDate;

                    _entry.ContractEndDate = p_Entity.ContractEndDate;
                    _entry.DesignationId = p_Entity.DesignationId;
                    _entry.WorkLocationId = p_Entity.WorkLocationId;

                    _entry.DepartmentId = p_Entity.DepartmentId;
                    _entry.CostCenter = p_Entity.CostCenter;
                    _entry.TypeOfNCR = p_Entity.TypeOfNCR;

                    _entry.TINNo = p_Entity.TINNo;
                    _entry.TaxExemption = p_Entity.TaxExemption;
                    _entry.SSSNo = p_Entity.SSSNo;

                    _entry.PagIbigNo = p_Entity.PagIbigNo;
                    _entry.PhilhealthNo = p_Entity.PhilhealthNo;
                    _entry.DriverLicenseNo = p_Entity.DriverLicenseNo;

                    _entry.Remarks = p_Entity.Remarks;
                    _entry.CreatedDate = p_Entity.CreatedDate;
                    _entry.CreatedBy = p_Entity.CreatedBy;

                    _entry.ModifiedDate = p_Entity.ModifiedDate;
                    _entry.ModifiedBy = p_Entity.ModifiedBy;
                    _entry.IsActive = p_Entity.IsActive;

                    _entry.IsResigned = p_Entity.IsResigned;
                    _entry.DateResigned = p_Entity.DateResigned;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data = false;
                    _Result.Message = GlobalMsg.AlreadyExistMsg;

                    if (p_Entity.EmployeeId == Guid.Empty)
                    {
                        _entry = new EmployeeProfile();

                        _entry.EmployeeId = Guid.NewGuid();

                        _entry.EmployeeNo = p_Entity.EmployeeNo;
                        _entry.StaffCode = p_Entity.StaffCode;
                        _entry.DateHired = p_Entity.DateHired;
                        _entry.PicImg = p_Entity.PicImg;

                        _entry.FirstName = p_Entity.FirstName;
                        _entry.MiddleName = p_Entity.MiddleName;
                        _entry.LastName = p_Entity.LastName;

                        _entry.Gender = p_Entity.Gender;
                        _entry.MartialStatus = p_Entity.MartialStatus;
                        _entry.DateOfMarriage = p_Entity.DateOfMarriage;

                        _entry.DateOfBirth = p_Entity.DateOfBirth;
                        _entry.BirthPlace = p_Entity.BirthPlace;
                        _entry.NoOfChildren = p_Entity.NoOfChildren;

                        _entry.CurrentBasicPay = p_Entity.CurrentBasicPay;
                        _entry.PaymentTerms = p_Entity.PaymentTerms;
                        _entry.EmploymentStatus = p_Entity.EmploymentStatus;

                        _entry.HouseStreetNo = p_Entity.HouseStreetNo;
                        _entry.BarangayTownVillage = p_Entity.BarangayTownVillage;
                        _entry.CityMunicipalityProvince = p_Entity.CityMunicipalityProvince;

                        _entry.County = p_Entity.CountryId;
                        _entry.Region = p_Entity.RegionId;
                        _entry.PostalCode = p_Entity.PostalCode;

                        _entry.HomeTelephoneNo = p_Entity.HomeTelephoneNo;
                        _entry.MobileNo = p_Entity.MobileNo;
                        _entry.EmailAddress = p_Entity.EmailAddress;

                        _entry.HighEducAttainment = p_Entity.HighEducAttainment;
                        _entry.School = p_Entity.School;
                        _entry.YearsCompleted = p_Entity.YearsCompleted;

                        _entry.DateCompleted = p_Entity.DateCompleted;
                        _entry.ContractType = p_Entity.ContractType;
                        _entry.ContractStartDate = p_Entity.ContractStartDate;

                        _entry.ContractEndDate = p_Entity.ContractEndDate;
                        _entry.DesignationId = p_Entity.DesignationId;
                        _entry.WorkLocationId = p_Entity.WorkLocationId;

                        _entry.DepartmentId = p_Entity.DepartmentId;
                        _entry.CostCenter = p_Entity.CostCenter;
                        _entry.TypeOfNCR = p_Entity.TypeOfNCR;

                        _entry.TINNo = p_Entity.TINNo;
                        _entry.TaxExemption = p_Entity.TaxExemption;
                        _entry.SSSNo = p_Entity.SSSNo;

                        _entry.PagIbigNo = p_Entity.PagIbigNo;
                        _entry.PhilhealthNo = p_Entity.PhilhealthNo;
                        _entry.DriverLicenseNo = p_Entity.DriverLicenseNo;

                        _entry.Remarks = p_Entity.Remarks;
                        _entry.CreatedDate = p_Entity.CreatedDate;
                        _entry.CreatedBy = p_Entity.CreatedBy;

                        _entry.ModifiedDate = p_Entity.ModifiedDate;
                        _entry.ModifiedBy = p_Entity.ModifiedBy;
                        _entry.IsActive = p_Entity.IsActive;

                        _entry.IsResigned = p_Entity.IsResigned;
                        _entry.DateResigned = p_Entity.DateResigned;

                        dbContext.EmployeeProfiles.Add(_entry);
                    }

                }

                int affRows = dbContext.SaveChanges();

                if (affRows >= 1)
                {
                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_entry.EmployeeId);
                    _Result.Data = true;
                }
            }

            return _Result;
        }

        #endregion

        #region Employee Schedule
        public Result<List<EmployeeScheduleModelDay>> GetByEmployeeScheduleByWorkLocation(Guid p_WorkLocationId, Guid p_CutOffId)
        {
            Result<List<EmployeeScheduleModelDay>> _Result = new Result<List<EmployeeScheduleModelDay>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from emp in dbContext.EmployeeProfiles
                                 join pos in dbContext.DesignationMasters on emp.DesignationId equals pos.DesignationID
                                 where emp.WorkLocationId == p_WorkLocationId && emp.IsActive == true
                                 select new EmployeeScheduleModelDay
                                 {
                                     EmpShiftId     = Guid.Empty,
                                     WorkLocationId = p_WorkLocationId,
                                     CutOffId       = p_CutOffId,
                                     EmployeeId     = emp.EmployeeId,
                                     EmployeeNo     = emp.EmployeeNo,
                                     LastName       = emp.LastName,
                                     FirstName      = emp.FirstName,
                                     MiddleName     = emp.MiddleName,

                                     DateHired      = emp.DateHired,
                                     DesignationId  = emp.DesignationId.Value,
                                     Designation    = pos.Designation
                                 };

                    foreach (var _empSchedMdlDay in _Query)
                    {

                    }

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<EmployeeScheduleModel>> GetByEmployeeSchedule(Guid p_EmployeeId, Guid p_CutOffId)
        {
            Result<List<EmployeeScheduleModel>> _Result = new Result<List<EmployeeScheduleModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeSchedules
                                 join shf in dbContext.ShiftMasters on e.ShiftId equals shf.ShiftID
                                 join emp in dbContext.EmployeeProfiles on e.EmployeeId equals emp.EmployeeId
                                 join cut in dbContext.PayrollCutOffMasters on e.CutOffId equals cut.PayrollCutOffId
                                 where e.EmployeeId == p_EmployeeId && e.CutOffId == p_CutOffId && e.IsActive == true
                                 orderby e.ActualDate
                                 select new EmployeeScheduleModel
                                 {
                                     EmpShiftId   = e.EmpShiftId,
                                     ShiftId      = e.ShiftId,
                                     Shift        = shf.Shift,
                                     EmployeeId   = e.EmployeeId,
                                     CutOffId     = e.CutOffId,
                                     ActualDate   = e.ActualDate,
                                     Remarks      = e.Remarks,
                                     CreatedBy    = e.CreatedBy,
                                     CreateDate   = e.CreateDate,
                                     ModifiedBy   = e.ModifiedBy,
                                     ModifiedDate = e.ModifiedDate
                                 };

                    _Result.Data = _Query.ToList();

                    if(_Result.Data.Count == 0)
                    {
                        Result<PayrollCutOff> _curCutOff = GetByCutOffPeriodId(p_CutOffId);

                        List<EmployeeScheduleModel> _lstMdl = new List<EmployeeScheduleModel>();

                        if (_curCutOff != null)
                        {
                            DateTime StartDate = _curCutOff.Data.StartDate;
                            DateTime EndDate   = _curCutOff.Data.EndDate;
                            int DayInterval    = 1;

                            List<DateTime> dateList = new List<DateTime>();

                            while (StartDate.AddDays(DayInterval) <= EndDate.AddDays(1))
                            {
                                EmployeeScheduleModel _mdl = new EmployeeScheduleModel();

                                _mdl.EmpShiftId = Guid.NewGuid();
                                _mdl.ShiftId    = Guid.Parse("94D6B894-2BA8-4B83-8B5E-EF3A56F481E2");
                                _mdl.Shift      = "OFF";
                                _mdl.EmployeeId = p_EmployeeId;
                                _mdl.CutOffId   = p_CutOffId;
                                _mdl.ActualDate = StartDate;
                                _mdl.Remarks    = "";
                                //_mdl.CreatedBy  = e.CreatedBy;
                                _mdl.CreateDate = DateTime.Now;

                                _lstMdl.Add(_mdl);

                                StartDate = StartDate.AddDays(DayInterval);
                                dateList.Add(StartDate);
                            }
                        }

                        _Result.Data = _lstMdl;
                    }
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<EmployeeScheduleModel> GetEmployeeSchedulePerDay(Guid p_EmployeeId, Guid p_CutOffId, DateTime _actualDateIme)
        {
            Result<EmployeeScheduleModel> _Result = new Result<EmployeeScheduleModel>();

            try
            {
                _Result.IsSuccess = false;

                using(var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EmployeeSchedules
                                 join shf in dbContext.ShiftMasters on e.ShiftId equals shf.ShiftID
                                 join emp in dbContext.EmployeeProfiles on e.EmployeeId equals emp.EmployeeId
                                 join cut in dbContext.PayrollCutOffMasters on e.CutOffId equals cut.PayrollCutOffId
                                 where e.EmployeeId == p_EmployeeId && e.CutOffId == p_CutOffId && e.IsActive == true && e.ActualDate == _actualDateIme
                                 orderby e.ActualDate
                                 select new EmployeeScheduleModel
                                 {
                                     EmpShiftId   = e.EmpShiftId,
                                     ShiftId      = e.ShiftId,
                                     Shift        = shf.Shift,
                                     EmployeeId   = e.EmployeeId,
                                     CutOffId     = e.CutOffId,
                                     ActualDate   = e.ActualDate,
                                     Remarks      = e.Remarks,
                                     CreatedBy    = e.CreatedBy,
                                     CreateDate   = e.CreateDate,
                                     ModifiedBy   = e.ModifiedBy,
                                     ModifiedDate = e.ModifiedDate
                                 };

                    _Result.Data = _Query.FirstOrDefault();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> DeleteEmployeeSchedule(Guid p_EntityId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    EmployeeSchedule _EntityDelete = dbContext.EmployeeSchedules.Where(d => d.EmpShiftId == p_EntityId).FirstOrDefault();

                    if (_EntityDelete != null)
                    {
                        _EntityDelete.IsActive = false;

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> SaveEmployeeSchedule(EmployeeScheduleModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                EmployeeSchedule _checkEntry = dbContext.EmployeeSchedules.Where(x => x.EmployeeId == p_Entity.EmployeeId && x.CutOffId == p_Entity.CutOffId && x.ActualDate == p_Entity.ActualDate).FirstOrDefault();

                if (_checkEntry == null)
                {
                    EmployeeSchedule _entry = new EmployeeSchedule();

                    if (p_Entity.EmpShiftId == Guid.Empty)
                    {
                        _entry.EmpShiftId = Guid.NewGuid();
                        _entry.ShiftId = p_Entity.ShiftId;
                        _entry.EmployeeId = p_Entity.EmployeeId;
                        _entry.CutOffId = p_Entity.CutOffId;
                        _entry.ActualDate = p_Entity.ActualDate;
                        _entry.Remarks = p_Entity.Remarks;
                        _entry.IsActive = true;
                        _entry.CreatedBy = p_Entity.CreatedBy;
                        _entry.CreateDate = DateTime.Now;

                        dbContext.EmployeeSchedules.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.EmployeeSchedules.Where(e => e.EmpShiftId == p_Entity.EmpShiftId).FirstOrDefault();

                        _entry.ShiftId = p_Entity.ShiftId;
                        _entry.EmployeeId = p_Entity.EmployeeId;
                        _entry.CutOffId = p_Entity.CutOffId;
                        _entry.ActualDate = p_Entity.ActualDate;
                        _entry.Remarks = p_Entity.Remarks;
                        _entry.IsActive = p_Entity.IsActive;

                        _entry.ModifiedBy = p_Entity.ModifiedBy;
                        _entry.ModifiedDate = DateTime.Now;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_entry.EmpShiftId);
                    _Result.Data = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data = false;
                    _Result.Message = GlobalMsg.AlreadyExistMsg;
                }
            }

            return _Result;
        }

        #endregion

        #region CutOff Period

        public Result<List<PayrollCutOff>> GetCutOffPeriods()
        {
            Result<List<PayrollCutOff>> _Result = new Result<List<PayrollCutOff>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.PayrollCutOffMasters
                                 orderby e.ActualDate
                                 select new PayrollCutOff
                                 {
                                     PayrollCutOffId = e.PayrollCutOffId,
                                     CutOffCode = e.CutOffCode,
                                     StartDate = e.StartDate,
                                     EndDate = e.EndDate,
                                     ActualDate = e.ActualDate,
                                     IsActive = e.IsActive,
                                     IsLocked = e.IsLocked,
                                     Remarks = e.Remarks,
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<PayrollCutOff> GetByCutOffPeriodId(Guid p_EntityId)
        {
            Result<PayrollCutOff> _Result = new Result<PayrollCutOff>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.PayrollCutOffMasters
                                 where e.PayrollCutOffId == p_EntityId
                                 select new PayrollCutOff
                                 {
                                     PayrollCutOffId = e.PayrollCutOffId,
                                     CutOffCode = e.CutOffCode,
                                     StartDate = e.StartDate,
                                     EndDate = e.EndDate,
                                     ActualDate = e.ActualDate,
                                     IsActive = e.IsActive,
                                     IsLocked = e.IsLocked,
                                     Remarks = e.Remarks
                                 };

                    PayrollCutOff _Country = _Query.FirstOrDefault();

                    if (_Country != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Country;
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.NoRecordFoundMsg;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> DeleteCutOffPeriod(Guid p_EntityId, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.PayrollCutOffMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        PayrollCutOffMaster _EntityDelete = dbContext.PayrollCutOffMasters.Where(d => d.PayrollCutOffId == p_EntityId).FirstOrDefault();

                        if (_EntityDelete != null)
                        {
                            _EntityDelete.IsActive = false;

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> SaveCutOffPeriod(PayrollCutOff p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                PayrollCutOffMaster _entry = new PayrollCutOffMaster();

                if (p_Entity.PayrollCutOffId == Guid.Empty)
                {
                    PayrollCutOffMaster _checkEntry = dbContext.PayrollCutOffMasters.Where(x => x.IsActive == true && x.StartDate == p_Entity.StartDate && x.EndDate == p_Entity.EndDate).FirstOrDefault();

                    if (_checkEntry == null)
                    {
                        _entry.PayrollCutOffId = Guid.NewGuid();
                        _entry.CutOffCode      = string.Format("PC{0}", p_Entity.ActualDate.ToString("yyyy_MMMdd"));
                        _entry.StartDate       = p_Entity.StartDate;
                        _entry.EndDate         = p_Entity.EndDate;
                        _entry.Remarks         = p_Entity.Remarks;
                        _entry.ActualDate      = p_Entity.ActualDate;
                        _entry.IsActive        = true;
                        _entry.CreatedDate     = DateTime.Now;
                        _entry.CreatedById     = p_Entity.CreatedById;

                        dbContext.PayrollCutOffMasters.Add(_entry);

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id        = Convert.ToString(_entry.PayrollCutOffId);
                        _Result.Data      = true;
                    }
                    else
                    {
                        _Result.IsSuccess = false;
                        _Result.Data      = false;
                        _Result.Message   = GlobalMsg.AlreadyExistMsg;
                    }

                } else
                {
                    PayrollCutOffMaster _checkExist = dbContext.PayrollCutOffMasters.Where(x => x.PayrollCutOffId == p_Entity.PayrollCutOffId).FirstOrDefault();

                    if(_checkExist != null)
                    {
                        _entry = dbContext.PayrollCutOffMasters.Where(e => e.PayrollCutOffId == p_Entity.PayrollCutOffId).FirstOrDefault();

                        _entry.CutOffCode = p_Entity.CutOffCode;
                        _entry.StartDate  = p_Entity.StartDate;
                        _entry.EndDate    = p_Entity.EndDate;
                        _entry.IsActive   = p_Entity.IsActive;
                        _entry.Remarks    = p_Entity.Remarks;

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id        = Convert.ToString(_entry.PayrollCutOffId);
                        _Result.Data      = true;
                    }
                }


            }

            return _Result;
        }
        #endregion

        #region Payroll Masters
        public Result<List<PayrollMasterModel>> GetPayrollModels()
        {
            Result<List<PayrollMasterModel>> _Result = new Result<List<PayrollMasterModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.PayrollMasters
                                 orderby e.CreatedDate descending
                                 select new PayrollMasterModel
                                 {
                                     PayrollId           = e.PayrollId,
                                     PayrollCode         = e.PayrollCode,
                                     CutOffId            = e.CutOffId,
                                     Remarks             = e.Remarks,
                                     IsActive            = e.IsActive,
                                     CreatedDate         = e.CreatedDate,
                                     CreatedBy           = e.CreatedBy,
                                     ModifiedDate        = e.ModifiedDate,
                                     ModifiedBy          = e.ModifiedBy,
                                     IsLockedDTR         = e.IsLockedDTR,
                                     LockedDTRDate       = e.LockedDTRDate,
                                     LockedDTRBy         = e.LockedDTRBy,
                                     IsApproved          = e.IsApproved,
                                     ApprovedDate        = e.ApprovedDate,
                                     ApprovedBy          = e.ApprovedBy,
                                     IsLockedPTSR        = e.IsLockedPTSR,
                                     LockedPTSRBy        = e.LockedPTSRBy,
                                     IsBankFileGenerated = e.IsBankFileGenerated,
                                     GeneratedDate       = e.GeneratedDate,
                                     GeneratedBy         = e.GeneratedBy,
                                     BankType            = e.BankType,
                                     BankFileId          = e.BankFileId,
                                     TotalWorks          = e.TotalWorks,
                                     TotalEmployee       = e.TotalEmployee,
                                     TotalDeduction      = e.TotalDeduction,
                                     TotalSSS            = e.TotalSSS,
                                     TotalPHIC           = e.TotalPHIC,
                                     TotalHDMF           = e.TotalHDMF,
                                     TotalTax            = e.TotalTax
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<PayrollMasterModel> GetByPayrollModelId(Guid p_PayrollId)
        {
            Result<PayrollMasterModel> _Result = new Result<PayrollMasterModel>();

            using (var dbContext = new ERPEntities())
            {
                var _Query = from e in dbContext.PayrollMasters
                             where e.PayrollId == p_PayrollId
                             orderby e.CreatedDate descending
                             select new PayrollMasterModel
                             {
                                 PayrollId           = e.PayrollId,
                                 PayrollCode         = e.PayrollCode,
                                 CutOffId            = e.CutOffId,
                                 Remarks             = e.Remarks,
                                 IsActive            = e.IsActive,
                                 CreatedDate         = e.CreatedDate,
                                 CreatedBy           = e.CreatedBy,
                                 ModifiedDate        = e.ModifiedDate,
                                 ModifiedBy          = e.ModifiedBy,
                                 IsLockedDTR         = e.IsLockedDTR,
                                 LockedDTRDate       = e.LockedDTRDate,
                                 LockedDTRBy         = e.LockedDTRBy,
                                 IsApproved          = e.IsApproved,
                                 ApprovedDate        = e.ApprovedDate,
                                 ApprovedBy          = e.ApprovedBy,
                                 IsLockedPTSR        = e.IsLockedPTSR,
                                 LockedPTSRBy        = e.LockedPTSRBy,
                                 IsBankFileGenerated = e.IsBankFileGenerated,
                                 GeneratedDate       = e.GeneratedDate,
                                 GeneratedBy         = e.GeneratedBy,
                                 BankType            = e.BankType,
                                 BankFileId          = e.BankFileId,
                                 TotalWorks          = e.TotalWorks,
                                 TotalEmployee       = e.TotalEmployee,
                                 TotalDeduction      = e.TotalDeduction,
                                 TotalSSS            = e.TotalSSS,
                                 TotalPHIC           = e.TotalPHIC,
                                 TotalHDMF           = e.TotalHDMF,
                                 TotalTax            = e.TotalTax
                             };

                _Result.Data = _Query.FirstOrDefault();
            }

            return _Result;
        }

        public Result<bool> DeletePayrollMasterModel(Guid p_EntityId, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.PayrollMasters.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        PayrollMaster _EntityDelete = dbContext.PayrollMasters.Where(d => d.PayrollId == p_EntityId).FirstOrDefault();

                        if (_EntityDelete != null)
                        {
                            _EntityDelete.IsActive = false;

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
                    _Result.Message = GlobalMsg.DeletionSuccessMsg;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> SavePayrollMasterModel(PayrollMasterModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                PayrollMaster _checkEntry = dbContext.PayrollMasters.Where(x => x.IsActive == true && x.CutOffId == p_Entity.CutOffId && x.PaymentTerms == p_Entity.PaymentTerms).FirstOrDefault();

                if (_checkEntry == null)
                {
                    PayrollMaster _entry = new PayrollMaster();

                    if (p_Entity.PayrollId == Guid.Empty)
                    {
                        _entry.PayrollId           = Guid.NewGuid();
                        _entry.PayrollCode         = string.Format("PY{0}", DateTime.Now.ToString("yyyy_MMMdd"));
                        
                        _entry.WorkLocationId      = p_Entity.WorkLocationId;
                        _entry.CutOffId            = p_Entity.CutOffId;
                        _entry.PaymentTerms        = p_Entity.PaymentTerms;

                        _entry.Remarks             = p_Entity.Remarks;
                        _entry.IsActive            = true;
                        _entry.CreatedDate         = DateTime.Now;
                        _entry.CreatedBy           = p_Entity.CreatedBy;
                        //_entry.IsLockedDTR         = p_Entity.IsLockedDTR;
                        //_entry.LockedDTRDate       = p_Entity.LockedDTRDate;
                        //_entry.LockedDTRBy         = p_Entity.LockedDTRBy;
                        //_entry.IsApproved          = p_Entity.IsApproved;
                        //_entry.ApprovedDate        = p_Entity.ApprovedDate;
                        //_entry.ApprovedBy          = p_Entity.ApprovedBy;
                        //_entry.IsLockedPTSR        = p_Entity.IsLockedPTSR;
                        //_entry.LockedPTSRDate      = p_Entity.LockedPTSRDate;
                        //_entry.LockedPTSRBy        = p_Entity.LockedPTSRBy;

                        _entry.IsBankFileGenerated = p_Entity.IsBankFileGenerated;
                        _entry.GeneratedDate       = p_Entity.GeneratedDate;
                        _entry.GeneratedBy         = p_Entity.GeneratedBy;
                        _entry.BankType            = p_Entity.BankType;
                        _entry.BankFileId          = p_Entity.BankFileId;
                        _entry.TotalWorks          = p_Entity.TotalWorks;
                        _entry.TotalEmployee       = p_Entity.TotalEmployee;
                        _entry.TotalDeduction      = p_Entity.TotalDeduction;
                        _entry.TotalSSS            = p_Entity.TotalSSS;
                        _entry.TotalPHIC           = p_Entity.TotalPHIC;
                        _entry.TotalHDMF           = p_Entity.TotalHDMF;
                        _entry.TotalTax            = p_Entity.TotalTax;
                        
                        dbContext.PayrollMasters.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.PayrollMasters.Where(e => e.PayrollId == p_Entity.PayrollId).FirstOrDefault();

                        _entry.Remarks             = p_Entity.Remarks;
                        _entry.IsActive            = true;
                        _entry.CreatedDate         = DateTime.Now;
                        _entry.CreatedBy           = p_Entity.CreatedBy;
                        _entry.IsLockedDTR         = p_Entity.IsLockedDTR;
                        _entry.LockedDTRDate       = p_Entity.LockedDTRDate;
                        _entry.LockedDTRBy         = p_Entity.LockedDTRBy;
                        _entry.IsApproved          = p_Entity.IsApproved;
                        _entry.ApprovedDate        = p_Entity.ApprovedDate;
                        _entry.ApprovedBy          = p_Entity.ApprovedBy;
                        _entry.IsLockedPTSR        = p_Entity.IsLockedPTSR;
                        _entry.LockedPTSRDate      = p_Entity.LockedPTSRDate;
                        _entry.LockedPTSRBy        = p_Entity.LockedPTSRBy;

                        _entry.IsBankFileGenerated = p_Entity.IsBankFileGenerated;
                        _entry.GeneratedDate       = p_Entity.GeneratedDate;
                        _entry.GeneratedBy         = p_Entity.GeneratedBy;
                        _entry.BankType            = p_Entity.BankType;
                        _entry.BankFileId          = p_Entity.BankFileId;
                        _entry.TotalWorks          = p_Entity.TotalWorks;
                        _entry.TotalEmployee       = p_Entity.TotalEmployee;
                        _entry.TotalDeduction      = p_Entity.TotalDeduction;
                        _entry.TotalSSS            = p_Entity.TotalSSS;
                        _entry.TotalPHIC           = p_Entity.TotalPHIC;
                        _entry.TotalHDMF           = p_Entity.TotalHDMF;
                        _entry.TotalTax            = p_Entity.TotalTax;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_entry.PayrollId);
                    _Result.Data      = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data      = false;
                    _Result.Message   = GlobalMsg.AlreadyExistMsg;
                }
            }

            return _Result;
        }
        #endregion

        #region Payroll Daily Record

        public Result<List<PayrollDTRModel>> GetEmployeeDTRbyCutOff(Guid _EmployeeId, Guid _CutOffPeriodId)
        {
            Result<List<PayrollDTRModel>> _Result = new Result<List<PayrollDTRModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.PayrollDTRs
                                 where e.EmployeeId == _EmployeeId && e.PayrollCutOffId == _CutOffPeriodId
                                 orderby e.ActualDate
                                 select new PayrollDTRModel
                                 {
                                     DTRId = e.DTRId,
                                     PayrollCutOffId = e.PayrollCutOffId,
                                     ScheduleId = e.ScheduleId,
                                     EmployeeId = e.EmployeeId,
                                     ActualDate = e.ActualDate,
                                     WorkingHrs = e.WorkingHrs,
                                     LateHrs = e.LateHrs,
                                     OvertimeHrs = e.OvertimeHrs,
                                     AdjustHrs = e.AdjustHrs,
                                     CreatedBy = e.CreatedBy,
                                     CreatedDate = e.CreatedDate,
                                     IsActive = e.IsActive,
                                     IsLocked = e.IsLocked,
                                     timein = e.timein,
                                     timeout = e.timeout,
                                     status = e.status,
                                     ModifiedBy = e.ModifiedBy,
                                     ModifiedDate = e.ModifiedDate
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<DTRRawModel>> GetEmployeeDTRRaw(Guid _EmployeeId, Guid _CutOffPeriodId)
        {
            Result<List<DTRRawModel>> _Result = new Result<List<DTRRawModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from raw in dbContext.DTRRaws
                                 where raw.EmployeeId == _EmployeeId && raw.CutOffId == _CutOffPeriodId
                                 orderby raw.ActualDate, raw.TimeTypeOrder, raw.RawOrder
                                 select new DTRRawModel
                                 {
                                     DTRRawId      = raw.DTRRawId,
                                     CutOffId      = raw.CutOffId,
                                     EmployeeId    = raw.EmployeeId,
                                     StaffCode     = raw.StaffCode,
                                     TimeType      = raw.TimeType,
                                     TimeTypeOrder = raw.TimeTypeOrder.Value,
                                     ActualDate    = raw.ActualDate,
                                     ActualTime    = raw.ActualTime,
                                     FromType      = raw.FromType,
                                     RawOrder      = raw.RawOrder.Value
                                 };

                    if(_Query.Count() > 0)
                    {
                        _Result.Data = _Query.ToList();
                    } else
                    {
                        List<DTRRawModel> lstMdl = new List<DTRRawModel>();

                        Result<PayrollCutOff> _resultPayrollCutOff = GetByCutOffPeriodId(_CutOffPeriodId);
                        Result<EmployeeProfileModel> _resultEmployeeProfile = GetByEmployeeProfile(_EmployeeId);
                        
                        DateTime StartDate = _resultPayrollCutOff.Data.StartDate;
                        DateTime EndDate   = _resultPayrollCutOff.Data.EndDate;
                        int DayInterval    = 1;

                        List<DateTime> dateList = new List<DateTime>();
                        int counter = 1;

                        while (StartDate.AddDays(DayInterval) <= EndDate.AddDays(1))
                        {
                            Result<EmployeeScheduleModel> _empSched = GetEmployeeSchedulePerDay(_EmployeeId, _CutOffPeriodId, StartDate);

                            if (_empSched.Data != null)
                            {
                                DTRRawModel _mdlTimeIn = new DTRRawModel()
                                {
                                    DTRRawId = Guid.Empty,
                                    CutOffId = _CutOffPeriodId,
                                    EmployeeId = _EmployeeId,
                                    StaffCode = _resultEmployeeProfile.Data.StaffCode,
                                    TimeType = "Time In",
                                    TimeTypeOrder = 0,
                                    ActualDate = StartDate,
                                    ActualTime = new TimeSpan(),
                                    FromType = "No Timelogs",
                                    RawOrder = counter,
                                    ScheduleId = _empSched.Data.ShiftId.Value
                                };

                                DTRRawModel _mdlTimeOut = new DTRRawModel()
                                {
                                    DTRRawId = Guid.Empty,
                                    CutOffId = _CutOffPeriodId,
                                    EmployeeId = _EmployeeId,
                                    StaffCode = _resultEmployeeProfile.Data.StaffCode,
                                    TimeType = "Time Out",
                                    TimeTypeOrder = 1,
                                    ActualDate = StartDate,
                                    ActualTime = new TimeSpan(),
                                    FromType = "No Timelogs",
                                    RawOrder = counter,
                                    ScheduleId = _empSched.Data.ShiftId.Value
                                };

                                lstMdl.Add(_mdlTimeIn);
                                lstMdl.Add(_mdlTimeOut);
                            }

                            counter = counter + 1;
                            StartDate = StartDate.AddDays(DayInterval);
                            dateList.Add(StartDate);
                        } 

                        _Result.Data = lstMdl;
                    }
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<List<PayrollDTRModel>> GetEmployeeDTRByWorkLocationAndCutOff(Guid _workLocationId, Guid _CutOffPeriodId)
        {
            Result<List<PayrollDTRModel>> _Result = new Result<List<PayrollDTRModel>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from dtr in dbContext.PayrollDTRs
                                 join emp in dbContext.EmployeeProfiles on dtr.EmployeeId equals emp.EmployeeId
                                 where emp.WorkLocationId == _workLocationId && dtr.PayrollCutOffId == _CutOffPeriodId
                                 orderby dtr.ActualDate
                                 select new PayrollDTRModel
                                 {
                                     DTRId           = dtr.DTRId,
                                     PayrollCutOffId = dtr.PayrollCutOffId,
                                     ScheduleId      = dtr.ScheduleId,
                                     DateHired       = emp.DateHired,
                                     EmployeeId      = dtr.EmployeeId,
                                     EmployeeNo      = emp.EmployeeNo,
                                     LastName        = emp.LastName,
                                     FirstName       = emp.FirstName,
                                     MiddleName      = emp.MiddleName,
                                     ActualDate      = dtr.ActualDate,
                                     WorkingHrs      = dtr.WorkingHrs,
                                     LateHrs         = dtr.LateHrs,
                                     OvertimeHrs     = dtr.OvertimeHrs,
                                     AdjustHrs       = dtr.AdjustHrs,
                                     CreatedBy       = dtr.CreatedBy,
                                     CreatedDate     = dtr.CreatedDate,
                                     IsActive        = dtr.IsActive,
                                     IsLocked        = dtr.IsLocked,
                                     timein          = dtr.timein,
                                     timeout         = dtr.timeout,
                                     status          = dtr.status,
                                     ModifiedBy      = dtr.ModifiedBy,
                                     ModifiedDate    = dtr.ModifiedDate
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }
        
        public Result<List<DTRCutOffTimeLogModel>> GetDTRCutOffTimeLogModel(Guid _workLocationId, Guid _CutOffPeriodId)
        {
            Result<List<DTRCutOffTimeLogModel>> _Result = new Result<List<DTRCutOffTimeLogModel>>();

            try
            {
                _Result.IsSuccess = false;

                List<DTRCutOffTimeLogModel> _data = new List<DTRCutOffTimeLogModel>();

                using(var dbContext = new ERPEntities())
                {
                    var _QueryEmployee = from emp in dbContext.EmployeeProfiles
                                         where emp.WorkLocationId == _workLocationId && emp.IsActive == true
                                         orderby emp.LastName 
                                         select new EmployeeProfileModel
                                         {
                                             EmployeeId = emp.EmployeeId,
                                             DateHired  = emp.DateHired,
                                             StaffCode  = emp.StaffCode,
                                             EmployeeNo = emp.EmployeeNo,
                                             LastName   = emp.LastName,
                                             FirstName  = emp.FirstName,
                                             MiddleName = emp.MiddleName
                                         };


                    foreach (EmployeeProfileModel _emp in _QueryEmployee.ToList())
                    {
                        var _QueryDTRRaw = from raw in dbContext.DTRRaws
                                           where raw.EmployeeId == _emp.EmployeeId && raw.CutOffId == _CutOffPeriodId
                                           orderby raw.ActualDate, raw.TimeTypeOrder, raw.RawOrder
                                           select new DTRRawModel
                                           {
                                               DTRRawId      = raw.DTRRawId,
                                               CutOffId      = raw.CutOffId,
                                               EmployeeId    = raw.EmployeeId,
                                               StaffCode     = raw.StaffCode,
                                               TimeType      = raw.TimeType,
                                               TimeTypeOrder = raw.TimeTypeOrder.Value,
                                               ActualDate    = raw.ActualDate,
                                               ActualTime    = raw.ActualTime,
                                               FromType      = raw.FromType,
                                               RawOrder      = raw.RawOrder.Value
                                           };

                        DTRCutOffTimeLogModel _mdl = new DTRCutOffTimeLogModel()
                        {
                            EmployeeId = _emp.EmployeeId,
                            EmployeeNo = _emp.EmployeeNo,
                            FullName   = _emp.FullName
                        };

                        foreach (DTRRawModel _dtr in _QueryDTRRaw.ToList())
                        {
                            _mdl = SetDTRCutOffTimeLogModel(_mdl, _dtr);
                        }

                        _data.Add(_mdl);
                    }

                    _Result.Data = _data;
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> GenerateDailyTimeRecord(Guid WorkLocationId, Guid PayrollCutOffId)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                CompanyMaster _WorkLocation = dbContext.CompanyMasters.Where(p => p.CompanyID == WorkLocationId).FirstOrDefault();
                PayrollCutOffMaster _PayrollCutOff = dbContext.PayrollCutOffMasters.Where(p => p.PayrollCutOffId == PayrollCutOffId).FirstOrDefault();

                List<EmployeeProfile> EmpProfiles = new List<EmployeeProfile>();

                if (_WorkLocation != null)
                {
                    if (_PayrollCutOff != null)
                    {
                        EmpProfiles = dbContext.EmployeeProfiles.Where(p => p.WorkLocationId == _WorkLocation.CompanyID && p.IsActive == true).ToList();

                        foreach (EmployeeProfile emp in EmpProfiles)
                        {
                            DateTime StartDate = _PayrollCutOff.StartDate;
                            DateTime EndDate   = _PayrollCutOff.EndDate;
                            int DayInterval    = 1;

                            List<DateTime> dateList = new List<DateTime>();

                            while (StartDate.AddDays(DayInterval) <= EndDate)
                            {
                                List<DTRRaw> _dtrRawMdlTimes = dbContext.DTRRaws.Where(p => p.EmployeeId == emp.EmployeeId && p.ActualDate == StartDate).OrderByDescending(p => p.ActualTime).ToList();

                                EmployeeSchedule _empSched = dbContext.EmployeeSchedules.Where(p => p.EmployeeId == emp.EmployeeId && p.CutOffId == PayrollCutOffId).FirstOrDefault();

                                if(_dtrRawMdlTimes.Count == 2)
                                {
                                    DTRRaw _dtrTimeOut = _dtrRawMdlTimes[0];
                                    DTRRaw _dtrTimeIn = _dtrRawMdlTimes[1];

                                    if(_dtrTimeIn != null && _dtrTimeOut != null)
                                    {
                                        TimeSpan? dtIN = _dtrTimeIn.ActualTime;
                                        TimeSpan? dtOUT = _dtrTimeOut.ActualTime;

                                        //DateTime _dtTimeIn = DateTime.Parse(_dtrTimeIn.ActualDate.Value.ToString());
                                        DateTime _dtTimeIn = new DateTime(_dtrTimeIn.ActualDate.Value.Year, 
                                                                          _dtrTimeIn.ActualDate.Value.Month, 
                                                                          _dtrTimeIn.ActualDate.Value.Day, 
                                                                          dtIN.Value.Hours, dtIN.Value.Minutes, dtIN.Value.Seconds
                                                                          );

                                        DateTime _dtTimeOut = new DateTime(_dtrTimeOut.ActualDate.Value.Year,
                                                                          _dtrTimeOut.ActualDate.Value.Month,
                                                                          _dtrTimeOut.ActualDate.Value.Day,
                                                                          dtOUT.Value.Hours, dtOUT.Value.Minutes, dtOUT.Value.Seconds
                                                                          );

                                        var wrkHrs = Convert.ToDateTime(_dtTimeOut) - Convert.ToDateTime(_dtTimeIn);

                                        if (_empSched != null)
                                        {
                                            PayrollDTRModel _pyDtrMdl = new PayrollDTRModel()
                                            {
                                                PayrollCutOffId = _PayrollCutOff.PayrollCutOffId,
                                                EmployeeId      = emp.EmployeeId,
                                                ScheduleId      =_empSched.ShiftId,
                                                ActualDate      = StartDate,
                                                WorkingHrs      = decimal.Parse(wrkHrs.TotalHours.ToString()),
                                                LateHrs         = 0,
                                                OvertimeHrs     = 0,
                                                AdjustHrs       = 0,
                                                timein          = _dtrTimeIn.ActualTime,
                                                timeout         = _dtrTimeOut.ActualTime
                                            };

                                            _Result = SaveDailyTimeRecord(_pyDtrMdl);
                                        }
                                    }
                                }

                                //if (_dtrRawMdlTimeIn != null && _dtrRawMdlTimeOut != null)
                                //{
                                //    string _timeOut = _dtrRawMdlTimeOut.ActualTime.ToString();
                                //    string _timeIn = _dtrRawMdlTimeIn.ActualTime.ToString();
                                //    // This should be 5 hours
                                //    TimeSpan dt = Convert.ToDateTime(_timeOut) - Convert.ToDateTime(_timeIn);
                                //    int hours = (int)dt.TotalHours;
                                //    hours = hours < 0 ? 24 + hours : hours;

                                //    //PayrollDTRModel _dtrMdl = new PayrollDTRModel()
                                //    //{
                                //    //    PayrollCutOffId = _PayrollCutOff.PayrollCutOffId,
                                //    //    EmployeeId      = emp.EmployeeId,
                                //    //    ActualDate      = StartDate,
                                //    //    WorkingHrs      = 8,
                                //    //    LateHrs         = 0,
                                //    //    OvertimeHrs     = 0,
                                //    //    AdjustHrs       = 0
                                //    //};

                                //    //_Result = SaveDailyTimeRecord(_dtrMdl);
                                //}

                                StartDate = StartDate.AddDays(DayInterval);
                                dateList.Add(StartDate);
                            }
                        }
                    }
                }
            }

            return _Result;
        }

        public Result<bool> SaveDailyTimeRecord(PayrollDTRModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                PayrollDTR _checkEntry = dbContext.PayrollDTRs.Where(x => x.PayrollCutOffId == p_Entity.PayrollCutOffId && x.EmployeeId == p_Entity.EmployeeId && x.ActualDate == p_Entity.ActualDate).FirstOrDefault();

                if (_checkEntry == null)
                {
                    PayrollDTR _entry = new PayrollDTR();

                    if (p_Entity.DTRId == Guid.Empty)
                    {
                        _entry.DTRId           = Guid.NewGuid();
                        _entry.PayrollCutOffId = p_Entity.PayrollCutOffId;
                        _entry.EmployeeId      = p_Entity.EmployeeId;
                        _entry.ActualDate      = p_Entity.ActualDate;
                        _entry.WorkingHrs      = p_Entity.WorkingHrs;
                        _entry.LateHrs         = p_Entity.LateHrs;
                        _entry.OvertimeHrs     = p_Entity.OvertimeHrs;
                        _entry.AdjustHrs       = p_Entity.AdjustHrs;
                        _entry.timein          = p_Entity.timein;
                        _entry.timeout         = p_Entity.timeout;
                        _entry.CreatedDate     = DateTime.Now;
                        _entry.CreatedBy       = p_Entity.CreatedBy;
                        _entry.IsActive        = true;

                        dbContext.PayrollDTRs.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.PayrollDTRs.Where(e => e.DTRId == p_Entity.DTRId).FirstOrDefault();

                        _entry.ActualDate   = p_Entity.ActualDate;
                        _entry.WorkingHrs   = p_Entity.WorkingHrs;
                        _entry.LateHrs      = p_Entity.LateHrs;
                        _entry.OvertimeHrs  = p_Entity.OvertimeHrs;
                        _entry.AdjustHrs    = p_Entity.AdjustHrs;
                        _entry.timein       = p_Entity.timein;
                        _entry.timeout      = p_Entity.timeout;
                        _entry.status       = p_Entity.status;

                        _entry.ModifiedBy   = p_Entity.ModifiedBy;
                        _entry.ModifiedDate = DateTime.Now;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_entry.PayrollCutOffId);
                    _Result.Data      = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data = false;
                    _Result.Message = GlobalMsg.AlreadyExistMsg;
                }
            }

            return _Result;
        }
        
        public Result<bool> SaveDTRRawModel(DTRRawModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                DTRRaw _checkEntry = dbContext.DTRRaws.Where(x => x.EmployeeId == p_Entity.EmployeeId && x.ActualDate == p_Entity.ActualDate && x.TimeType == p_Entity.TimeType).FirstOrDefault();

                if (_checkEntry == null)
                {
                    DTRRaw _entry = new DTRRaw();

                    if (p_Entity.DTRRawId == Guid.Empty)
                    {
                        _entry.DTRRawId   = Guid.NewGuid();
                        _entry.CutOffId   = p_Entity.CutOffId;
                        _entry.EmployeeId = p_Entity.EmployeeId;
                        _entry.StaffCode  = p_Entity.StaffCode;
                        _entry.TimeType   = p_Entity.TimeType;
                        _entry.ActualDate = p_Entity.ActualDate;
                        _entry.ActualTime = p_Entity.ActualTime;
                        _entry.FromType   = p_Entity.FromType;

                        dbContext.DTRRaws.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.DTRRaws.Where(e => e.DTRRawId == p_Entity.DTRRawId).FirstOrDefault();

                        _entry.EmployeeId = p_Entity.EmployeeId;
                        _entry.CutOffId   = p_Entity.CutOffId;
                        _entry.StaffCode  = p_Entity.StaffCode;
                        _entry.TimeType   = p_Entity.TimeType;
                        _entry.ActualDate = p_Entity.ActualDate;
                        _entry.ActualTime = p_Entity.ActualTime;
                        _entry.FromType   = p_Entity.FromType;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_entry.DTRRawId);
                    _Result.Data      = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data      = false;
                    _Result.Message   = GlobalMsg.AlreadyExistMsg;
                }
            }

            return _Result;
        }
        #endregion

        #region Payroll Time Summary

        #endregion

        #region Payroll Summary
        public Result<List<PayrollSummaryModel>> GetPayrollSummaryModels(Guid _workLocationId, Guid _cutOffId, string payTerms)
        {
            Result<List<PayrollSummaryModel>> _Result = new Result<List<PayrollSummaryModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from ps in dbContext.PayrollSummaries
                                 join py in dbContext.PayrollMasters on ps.PayrollId equals py.PayrollId
                                 join emp in dbContext.EmployeeProfiles on ps.EmployeeId equals emp.EmployeeId                                 
                                 where ps.WorkLocationId == _workLocationId && py.CutOffId == _cutOffId && py.PaymentTerms == payTerms
                                 orderby emp.LastName
                                 select new PayrollSummaryModel
                                 {
                                     PayrollTimeId  = ps.PayrollTimeId,
                                     
                                     EmployeeId     = ps.EmployeeId,
                                     FirstName      = emp.FirstName,
                                     LastName       = emp.LastName,
                                     MiddleName     = emp.MiddleName,

                                     PayrollId      = ps.PayrollId,
                                     WorkLocationId = ps.WorkLocationId,
                                     
                                     //Gross
                                     gMonthlyRate        = ps.gMonthlyRate,
                                     gHalfMonthEarning   = ps.gHalfMonthEarning,
                                     gRTDays             = ps.gRTDays,
                                     gRTHrs              = ps.gRTHrs,
                                     gdAbsDay            = ps.gdAbsDay,
                                     dAbsHrs             = ps.dAbsHrs,
                                     gRTOTHrs            = ps.gRTOTHrs,
                                     gRTOTAmt            = ps.gRTOTAmt,
                                     gRDHrs              = ps.gRDHrs,
                                     gRDAmt              = ps.gRDAmt,
                                     gSHHrs              = ps.gSHHrs,
                                     gSHAmt              = ps.gSHAmt,
                                     gPosAllowance       = ps.gPosAllowance,
                                     gSIL                = ps.gSIL,
                                     gAdjAmt             = ps.gAdjAmt,
                                     gAdjustmentDays     = ps.gAdjustmentDays,
                                     g30PerOT            = ps.g30PerOT,

                                     //deduction
                                     dCigna              = ps.dCigna,
                                     dPhicPrem           = ps.dPhicPrem,
                                     dHDMF               = ps.dHDMF,
                                     dHDMFLoan           = ps.dHDMFLoan,
                                     dMotorLoan          = ps.dMotorLoan,
                                     dCashAdv            = ps.dCashAdv,
                                     dSplFunds           = ps.dSplFunds,
                                     TtlDeduction        = ps.TtlDeduction,
                                     TtlNetSalary        = ps.TtlNetSalary,
                                     DateReceived        = ps.DateReceived,
                                     IsActive            = ps.IsActive,
                                     CreatedDate         = ps.CreatedDate,
                                     CreatedBy           = ps.CreatedBy
                                 };

                    _Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<PayrollSummaryModel> GetPayrollSummaryModelByCutOffPerEmployee(Guid CutOffId, Guid EmployeeId)
        {
            Result<PayrollSummaryModel> _Result = new Result<PayrollSummaryModel>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from ps in dbContext.PayrollSummaries
                                 join py in dbContext.PayrollMasters on ps.PayrollId equals py.PayrollId
                                 join emp in dbContext.EmployeeProfiles on ps.EmployeeId equals emp.EmployeeId
                                 where ps.EmployeeId == EmployeeId && py.CutOffId == CutOffId
                                 orderby emp.LastName
                                 select new PayrollSummaryModel
                                 {
                                     PayrollTimeId = ps.PayrollTimeId,

                                     EmployeeId = ps.EmployeeId,
                                     FirstName  = emp.FirstName,
                                     LastName   = emp.LastName,
                                     MiddleName = emp.MiddleName,

                                     PayrollId      = ps.PayrollId,
                                     WorkLocationId = ps.WorkLocationId,

                                     //Gross
                                     gMonthlyRate      = ps.gMonthlyRate,
                                     gHalfMonthEarning = ps.gHalfMonthEarning,
                                     gRTDays           = ps.gRTDays,
                                     gRTHrs            = ps.gRTHrs,
                                     gdAbsDay          = ps.gdAbsDay,
                                     dAbsHrs           = ps.dAbsHrs,
                                     gRTOTHrs          = ps.gRTOTHrs,
                                     gRTOTAmt          = ps.gRTOTAmt,
                                     gRDHrs            = ps.gRDHrs,
                                     gRDAmt            = ps.gRDAmt,
                                     gSHHrs            = ps.gSHHrs,
                                     gSHAmt            = ps.gSHAmt,
                                     gPosAllowance     = ps.gPosAllowance,
                                     gSIL              = ps.gSIL,
                                     gAdjAmt           = ps.gAdjAmt,
                                     gAdjustmentDays   = ps.gAdjustmentDays,
                                     g30PerOT          = ps.g30PerOT,

                                     //deduction
                                     dCigna         = ps.dCigna,
                                     dPhicPrem      = ps.dPhicPrem,
                                     dHDMF          = ps.dHDMF,
                                     dHDMFLoan      = ps.dHDMFLoan,
                                     dMotorLoan     = ps.dMotorLoan,
                                     dCashAdv       = ps.dCashAdv,
                                     dSplFunds      = ps.dSplFunds,
                                     TtlDeduction   = ps.TtlDeduction,
                                     TtlNetSalary   = ps.TtlNetSalary,
                                     DateReceived   = ps.DateReceived,
                                     IsActive       = ps.IsActive,
                                     CreatedDate    = ps.CreatedDate,
                                     CreatedBy      = ps.CreatedBy
                                 };

                    _Result.Data = _Query.FirstOrDefault();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }
        
        public Result<bool> DeletePayrollSummaryModel(Guid p_EntityId, Guid p_userId)
        {
            Result<bool> _Result = new Result<bool>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.PayrollSummaries.Where(e => e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        PayrollSummary _EntityDelete = dbContext.PayrollSummaries.Where(d => d.PayrollId == p_EntityId).FirstOrDefault();

                        if (_EntityDelete != null)
                        {
                            _EntityDelete.IsActive     = false;
                            _EntityDelete.ModifiedDate = DateTime.Now;
                            _EntityDelete.ModifiedBy   = p_userId;

                            dbContext.SaveChanges();

                            _Result.IsSuccess = true;
                        }
                        else
                        {
                            _Result.Message = GlobalMsg.NoRecordFoundMsg;
                        }
                    }
                    else
                    {
                        _Result.Message = GlobalMsg.ReferenceExistMsg;
                    }
                }

                if (_Result.IsSuccess)
                {
                    _Result.Data = true;
                    _Result.Message = GlobalMsg.DeletionSuccessMsg;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = GlobalMsg.ExceptionErrMsg;
                _Result.Exception = _Exception;
            }

            return _Result;
        }

        public Result<bool> SavePayrollSummaryModel(PayrollSummaryModel p_Entity)
        {
            Result<bool> _Result = new Result<bool>();

            using (var dbContext = new ERPEntities())
            {
                PayrollSummary _checkEntry = dbContext.PayrollSummaries.Where(x => x.EmployeeId == p_Entity.PayrollId).FirstOrDefault();

                if (_checkEntry == null)
                {
                    PayrollSummary _entry = new PayrollSummary();

                    if (p_Entity.PayrollTimeId == Guid.Empty)
                    {
                        _entry.PayrollTimeId = Guid.NewGuid();

                        _entry.EmployeeId        = p_Entity.EmployeeId;
                        _entry.PayrollId         = p_Entity.PayrollId;
                        _entry.WorkLocationId    = p_Entity.WorkLocationId;

                        _entry.gMonthlyRate      = p_Entity.gMonthlyRate;
                        _entry.gHalfMonthEarning = p_Entity.gHalfMonthEarning;
                        _entry.gRTDays           = p_Entity.gRTDays;
                        _entry.gRTHrs            = p_Entity.gRTHrs;
                        _entry.gdAbsDay          = p_Entity.gdAbsDay;
                        _entry.dAbsHrs           = p_Entity.dAbsHrs;
                        _entry.gRTOTHrs          = p_Entity.gRTOTHrs;
                        _entry.gRTOTAmt          = p_Entity.gRTOTAmt;
                        _entry.gRDAmt            = p_Entity.gRDAmt;
                        _entry.gSHHrs            = p_Entity.gSHHrs;
                        _entry.gSHAmt            = p_Entity.gSHAmt;

                        _entry.gPosAllowance     = p_Entity.gPosAllowance;
                        _entry.gSIL              = p_Entity.gSIL;
                        _entry.gAdjAmt           = p_Entity.gAdjAmt;
                        _entry.gAdjustmentDays   = p_Entity.gAdjustmentDays;
                        _entry.g30PerOT          = p_Entity.g30PerOT;
                        _entry.dCigna            = p_Entity.dCigna;
                        _entry.dPhicPrem         = p_Entity.dPhicPrem;
                        _entry.dHDMF             = p_Entity.dHDMF;
                        _entry.dHDMFLoan         = p_Entity.dHDMFLoan;
                        _entry.dMotorLoan        = p_Entity.dMotorLoan;
                        _entry.dCashAdv          = p_Entity.dCashAdv;
                                                 
                        _entry.dSplFunds         = p_Entity.dSplFunds;
                        _entry.TtlDeduction      = p_Entity.TtlDeduction;
                        _entry.TtlNetSalary      = p_Entity.TtlNetSalary;

                        _entry.CreatedDate  = DateTime.Now;
                        _entry.CreatedBy    = p_Entity.CreatedBy;
                        _entry.IsActive     = true;

                        dbContext.PayrollSummaries.Add(_entry);
                    }
                    else
                    {
                        _entry = dbContext.PayrollSummaries.Where(e => e.PayrollTimeId == p_Entity.PayrollTimeId).FirstOrDefault();

                        _entry.EmployeeId        = p_Entity.EmployeeId;
                        _entry.PayrollId         = p_Entity.PayrollId;
                        _entry.WorkLocationId    = p_Entity.WorkLocationId;

                        _entry.gMonthlyRate      = p_Entity.gMonthlyRate;
                        _entry.gHalfMonthEarning = p_Entity.gHalfMonthEarning;
                        _entry.gRTDays           = p_Entity.gRTDays;
                        _entry.gRTHrs            = p_Entity.gRTHrs;
                        _entry.gdAbsDay          = p_Entity.gdAbsDay;
                        _entry.dAbsHrs           = p_Entity.dAbsHrs;
                        _entry.gRTOTHrs          = p_Entity.gRTOTHrs;
                        _entry.gRTOTAmt          = p_Entity.gRTOTAmt;
                        _entry.gRDAmt            = p_Entity.gRDAmt;
                        _entry.gSHHrs            = p_Entity.gSHHrs;
                        _entry.gSHAmt            = p_Entity.gSHAmt;

                        _entry.gPosAllowance     = p_Entity.gPosAllowance;
                        _entry.gSIL              = p_Entity.gSIL;
                        _entry.gAdjAmt           = p_Entity.gAdjAmt;
                        _entry.gAdjustmentDays   = p_Entity.gAdjustmentDays;
                        _entry.g30PerOT          = p_Entity.g30PerOT;
                        _entry.dCigna            = p_Entity.dCigna;
                        _entry.dPhicPrem         = p_Entity.dPhicPrem;
                        _entry.dHDMF             = p_Entity.dHDMF;
                        _entry.dHDMFLoan         = p_Entity.dHDMFLoan;
                        _entry.dMotorLoan        = p_Entity.dMotorLoan;
                        _entry.dCashAdv          = p_Entity.dCashAdv;

                        _entry.dSplFunds         = p_Entity.dSplFunds;
                        _entry.TtlDeduction      = p_Entity.TtlDeduction;
                        _entry.TtlNetSalary      = p_Entity.TtlNetSalary;

                        _entry.IsActive          = p_Entity.IsActive;

                        _entry.ModifiedBy        = p_Entity.ModifiedBy;
                        _entry.ModifiedDate      = DateTime.Now;
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id        = Convert.ToString(_entry.PayrollTimeId);
                    _Result.Data      = true;
                }
                else
                {
                    _Result.IsSuccess = false;
                    _Result.Data      = false;
                    _Result.Message   = GlobalMsg.AlreadyExistMsg;
                }
            }

            return _Result;
        }
        #endregion

        #region Generate Payroll Summaries
        public Result<List<PayrollSummaryModel>> GeneratePayrollSummaryModels(Guid _workLocationId, Guid _cutOffId, string _payTerms)
        {
            Result<List<PayrollSummaryModel>> _Result = new Result<List<PayrollSummaryModel>>();

            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    PayrollMaster _pyExist = dbContext.PayrollMasters.Where(p => p.IsActive == true && p.WorkLocationId == _workLocationId && p.PaymentTerms == _payTerms).FirstOrDefault();

                    if(_pyExist == null)
                    {
                        Result<List<EmployeeProfileModel>> _resEmpProf = GetEmployeeProfilesByWorkLocationPaymentTerm(_workLocationId, _payTerms);

                        if (_resEmpProf.IsSuccess)
                        {
                            //Create Payroll Master
                            PayrollMasterModel _pyMdl = new PayrollMasterModel()
                            {
                                WorkLocationId = _workLocationId,
                                CutOffId       = _cutOffId,
                                PaymentTerms   = _payTerms,
                                IsActive       = true,
                                CreatedDate    = DateTime.Now
                            };

                            Result<bool> _isPyMaster = SavePayrollMasterModel(_pyMdl);

                            if (_isPyMaster.IsSuccess)
                            {
                                Result<bool> _isPsMaster = new Result<bool>();

                                foreach (var _emp in _resEmpProf.Data)
                                {
                                    PayrollSummaryModel _psMdl = new PayrollSummaryModel()
                                    {
                                        EmployeeId        = _emp.EmployeeId,
                                        PayrollId         = Guid.Parse(_isPyMaster.Id),
                                        WorkLocationId    = _workLocationId,
                                        gMonthlyRate      = _emp.CurrentBasicPay,
                                        gHalfMonthEarning = (_emp.CurrentBasicPay / 2),
                                        gRTDays           = 0,
                                        gRTHrs            = 0,
                                        gdAbsDay          = 0,
                                        dAbsHrs           = 0,
                                        gRTOTHrs          = 0,
                                        gRTOTAmt          = 0,
                                        gRDHrs            = 0,
                                        gRDAmt            = 0,
                                        gSHHrs            = 0,
                                        gSHAmt            = 0,
                                        gPosAllowance     = 0,
                                        gSIL              = 0,
                                        gAdjAmt           = 0,
                                        gAdjustmentDays   = 0,
                                        g30PerOT          = 0,
                                        dCigna            = 0,
                                        dPhicPrem         = 0,
                                        dHDMF             = 0,
                                        dHDMFLoan         = 0,
                                        dMotorLoan        = 0,
                                        dCashAdv          = 0,
                                        dSplFunds         = 0,
                                        TtlDeduction      = 0,
                                        TtlNetSalary      = 0,
                                        CreatedDate       = DateTime.Now
                                    };

                                    _isPsMaster = SavePayrollSummaryModel(_psMdl);
                                }

                                if(_isPsMaster.IsSuccess)
                                {

                                }
                            }
                        }
                    } else
                    {
                        //Update Payroll Summaries
                    }
                    
                    //var _Query = from ps in dbContext.PayrollSummaries
                    //             join py in dbContext.PayrollMasters on ps.PayrollId equals py.PayrollId
                    //             join emp in dbContext.EmployeeProfiles on ps.EmployeeId equals emp.EmployeeId
                    //             where ps.WorkLocationId == _workLocationId && py.CutOffId == _cutOffId
                    //             orderby emp.LastName
                    //             select new PayrollSummaryModel
                    //             {
                    //                 PayrollTimeId = ps.PayrollTimeId,

                    //                 EmployeeId = ps.EmployeeId,
                    //                 FirstName = emp.FirstName,
                    //                 LastName = emp.LastName,
                    //                 MiddleName = emp.MiddleName,

                    //                 PayrollId = ps.PayrollId,
                    //                 WorkLocationId = ps.WorkLocationId,

                    //                 //Gross
                    //                 gMonthlyRate = ps.gMonthlyRate,
                    //                 gHalfMonthEarning = ps.gHalfMonthEarning,
                    //                 gRTDays = ps.gRTDays,
                    //                 gRTHrs = ps.gRTHrs,
                    //                 gdAbsDay = ps.gdAbsDay,
                    //                 dAbsHrs = ps.dAbsHrs,
                    //                 gRTOTHrs = ps.gRTOTHrs,
                    //                 gRTOTAmt = ps.gRTOTAmt,
                    //                 gRDHrs = ps.gRDHrs,
                    //                 gRDAmt = ps.gRDAmt,
                    //                 gSHHrs = ps.gSHHrs,
                    //                 gSHAmt = ps.gSHAmt,
                    //                 gPosAllowance = ps.gPosAllowance,
                    //                 gSIL = ps.gSIL,
                    //                 gAdjAmt = ps.gAdjAmt,
                    //                 gAdjustmentDays = ps.gAdjustmentDays,
                    //                 g30PerOT = ps.g30PerOT,

                    //                 //deduction
                    //                 dCigna = ps.dCigna,
                    //                 dPhicPrem = ps.dPhicPrem,
                    //                 dHDMF = ps.dHDMF,
                    //                 dHDMFLoan = ps.dHDMFLoan,
                    //                 dMotorLoan = ps.dMotorLoan,
                    //                 dCashAdv = ps.dCashAdv,
                    //                 dSplFunds = ps.dSplFunds,
                    //                 TtlDeduction = ps.TtlDeduction,
                    //                 TtlNetSalary = ps.TtlNetSalary,
                    //                 DateReceived = ps.DateReceived,
                    //                 IsActive = ps.IsActive,
                    //                 CreatedDate = ps.CreatedDate,
                    //                 CreatedBy = ps.CreatedBy
                    //             };

                    //_Result.Data = _Query.ToList();
                }

                _Result.IsSuccess = true;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message   = _Exception.Message;
                _Result.Exception = _Exception;
            }

            return _Result;
        }
        #endregion

        #region Short Function

        private DTRCutOffTimeLogModel SetDTRCutOffTimeLogModel(DTRCutOffTimeLogModel _mdl, DTRRawModel _dtr)
        {
            switch (_dtr.RawOrder)
            {
                #region Days
                case 1:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day1TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day1TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day1TtlHrs = SetWorkingHours(_mdl.Day1TimeIn, _mdl.Day1TimeOut, _dtr);

                    break;

                case 2:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day2TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day2TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day2TtlHrs = SetWorkingHours(_mdl.Day2TimeIn, _mdl.Day2TimeOut, _dtr);
                    break;

                case 3:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day3TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day3TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day3TtlHrs = SetWorkingHours(_mdl.Day3TimeIn, _mdl.Day3TimeOut, _dtr);
                    break;

                case 4:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day4TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day4TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day4TtlHrs = SetWorkingHours(_mdl.Day4TimeIn, _mdl.Day4TimeOut, _dtr);
                    break;

                case 5:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day5TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day5TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day5TtlHrs = SetWorkingHours(_mdl.Day5TimeIn, _mdl.Day5TimeOut, _dtr);
                    break;
                case 6:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day6TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day6TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day6TtlHrs = SetWorkingHours(_mdl.Day6TimeIn, _mdl.Day6TimeOut, _dtr);
                    break;

                case 7:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day7TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day7TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day7TtlHrs = SetWorkingHours(_mdl.Day7TimeIn, _mdl.Day7TimeOut, _dtr);
                    break;

                case 8:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day8TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day8TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day8TtlHrs = SetWorkingHours(_mdl.Day8TimeIn, _mdl.Day8TimeOut, _dtr);
                    break;

                case 9:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day9TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day9TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day9TtlHrs = SetWorkingHours(_mdl.Day9TimeIn, _mdl.Day9TimeOut, _dtr);
                    break;

                case 10:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day10TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day10TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day10TtlHrs = SetWorkingHours(_mdl.Day10TimeIn, _mdl.Day10TimeOut, _dtr);
                    break;

                case 11:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day11TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day11TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day11TtlHrs = SetWorkingHours(_mdl.Day11TimeIn, _mdl.Day11TimeOut, _dtr);
                    break;

                case 12:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day12TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day12TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day12TtlHrs = SetWorkingHours(_mdl.Day12TimeIn, _mdl.Day12TimeOut, _dtr);
                    break;
                case 13:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day13TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day13TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day13TtlHrs = SetWorkingHours(_mdl.Day13TimeIn, _mdl.Day13TimeOut, _dtr);
                    break;
                case 14:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day14TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day14TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day14TtlHrs = SetWorkingHours(_mdl.Day14TimeIn, _mdl.Day14TimeOut, _dtr);
                    break;
                case 15:
                    switch (_dtr.TimeType)
                    {
                        case "Time In":
                            _mdl.Day15TimeIn = _dtr.ActualTime.Value;
                            break;
                        case "Time Out":
                            _mdl.Day5TimeOut = _dtr.ActualTime.Value;
                            break;
                    }

                    _mdl.Day15TtlHrs = SetWorkingHours(_mdl.Day15TimeIn, _mdl.Day15TimeOut, _dtr);
                    break;
                    #endregion
            }

            return _mdl;
        }

        private TimeSpan SetWorkingHours(TimeSpan _mdlTimeIn, TimeSpan _mdlTimeOut, DTRRawModel _dtr)
        {
            TimeSpan _return;

            DateTime _dtTimeIn = new DateTime(_dtr.ActualDate.Value.Year,
                                              _dtr.ActualDate.Value.Month,
                                              _dtr.ActualDate.Value.Day,
                                              _mdlTimeIn.Hours, _mdlTimeIn.Minutes, _mdlTimeIn.Seconds
                                             );

            DateTime _dtTimeOut = new DateTime(_dtr.ActualDate.Value.Year,
                                               _dtr.ActualDate.Value.Month,
                                               _dtr.ActualDate.Value.Day,
                                               _mdlTimeOut.Hours, _mdlTimeOut.Minutes, _mdlTimeOut.Seconds
                                              );

            var wrkHrs = Convert.ToDateTime(_dtTimeOut) - Convert.ToDateTime(_dtTimeIn);

            _return = wrkHrs;

            return _return;
        }

        #endregion
    }
}