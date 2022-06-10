
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/14/2021 15:18:35
-- Generated from EDMX file: D:\source\repos\YHLHRMS\HRM_SourceCode\ERP.Dal\ERP_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TESTAHRM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EmployeeAllowanceMap_AllowanceMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeAllowanceMap] DROP CONSTRAINT [FK_EmployeeAllowanceMap_AllowanceMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAllowanceMap_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeAllowanceMap] DROP CONSTRAINT [FK_EmployeeAllowanceMap_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAttachment_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeAttachment] DROP CONSTRAINT [FK_EmployeeAttachment_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAttendance_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeAttendance] DROP CONSTRAINT [FK_EmployeeAttendance_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDeductionMap_DeductionMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeDeductionMap] DROP CONSTRAINT [FK_EmployeeDeductionMap_DeductionMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDeductionMap_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeDeductionMap] DROP CONSTRAINT [FK_EmployeeDeductionMap_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeLeaveCategory_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveCategory] DROP CONSTRAINT [FK_EmployeeLeaveCategory_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeLeaveCategory_LeaveCategoryMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveCategory] DROP CONSTRAINT [FK_EmployeeLeaveCategory_LeaveCategoryMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeLoan_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLoan] DROP CONSTRAINT [FK_EmployeeLoan_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMaster_DepartmentMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeMaster] DROP CONSTRAINT [FK_EmployeeMaster_DepartmentMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMaster_DesignationMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeMaster] DROP CONSTRAINT [FK_EmployeeMaster_DesignationMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMaster_EmployeeGradeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeMaster] DROP CONSTRAINT [FK_EmployeeMaster_EmployeeGradeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMaster_EmployeeTypeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeMaster] DROP CONSTRAINT [FK_EmployeeMaster_EmployeeTypeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMaster_ShiftMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeMaster] DROP CONSTRAINT [FK_EmployeeMaster_ShiftMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePaidLoan_EmployeeLoan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeePaidLoan] DROP CONSTRAINT [FK_EmployeePaidLoan_EmployeeLoan];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePaidSalary_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeePaidSalary] DROP CONSTRAINT [FK_EmployeePaidSalary_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePaidSalary_FinancialYearMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeePaidSalary] DROP CONSTRAINT [FK_EmployeePaidSalary_FinancialYearMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSalary_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSalary] DROP CONSTRAINT [FK_EmployeeSalary_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeWorkingDay_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeWorkingDay] DROP CONSTRAINT [FK_EmployeeWorkingDay_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_InterviewAttechment_InterviewMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InterviewAttachment] DROP CONSTRAINT [FK_InterviewAttechment_InterviewMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_InterviewMaster_DepartmentMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InterviewMaster] DROP CONSTRAINT [FK_InterviewMaster_DepartmentMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_InterviewMaster_DesignationMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InterviewMaster] DROP CONSTRAINT [FK_InterviewMaster_DesignationMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_InterviewMaster_EducationMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InterviewMaster] DROP CONSTRAINT [FK_InterviewMaster_EducationMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceDetail_InvoiceMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_InvoiceMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceMaster_CurrencyMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceMaster] DROP CONSTRAINT [FK_InvoiceMaster_CurrencyMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_StateMaster_CountryMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateMaster] DROP CONSTRAINT [FK_StateMaster_CountryMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMaster_EmployeeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMaster] DROP CONSTRAINT [FK_UserMaster_EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMaster_RoleMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMaster] DROP CONSTRAINT [FK_UserMaster_RoleMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_UserModuleMap_ModuleMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserModuleMap] DROP CONSTRAINT [FK_UserModuleMap_ModuleMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_UserModuleMap_UserMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserModuleMap] DROP CONSTRAINT [FK_UserModuleMap_UserMaster];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AllowanceMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AllowanceMaster];
GO
IF OBJECT_ID(N'[dbo].[CompanyMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyMaster];
GO
IF OBJECT_ID(N'[dbo].[CountryMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountryMaster];
GO
IF OBJECT_ID(N'[dbo].[CurrencyMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CurrencyMaster];
GO
IF OBJECT_ID(N'[dbo].[DeductionMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeductionMaster];
GO
IF OBJECT_ID(N'[dbo].[DepartmentMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentMaster];
GO
IF OBJECT_ID(N'[dbo].[DesignationMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DesignationMaster];
GO
IF OBJECT_ID(N'[dbo].[DeviceMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceMaster];
GO
IF OBJECT_ID(N'[dbo].[EducationMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EducationMaster];
GO
IF OBJECT_ID(N'[dbo].[EmployeeAllowanceMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeAllowanceMap];
GO
IF OBJECT_ID(N'[dbo].[EmployeeAttachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeAttachment];
GO
IF OBJECT_ID(N'[dbo].[EmployeeAttendance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeAttendance];
GO
IF OBJECT_ID(N'[dbo].[EmployeeAttendanceDevice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeAttendanceDevice];
GO
IF OBJECT_ID(N'[dbo].[EmployeeDeductionMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeDeductionMap];
GO
IF OBJECT_ID(N'[dbo].[EmployeeDeviceMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeDeviceMap];
GO
IF OBJECT_ID(N'[dbo].[EmployeeGradeMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeGradeMaster];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLeaveCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLeaveCategory];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLeaveMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLeaveMap];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLoan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLoan];
GO
IF OBJECT_ID(N'[dbo].[EmployeeMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeMaster];
GO
IF OBJECT_ID(N'[dbo].[EmployeePaidAllowanceMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeePaidAllowanceMap];
GO
IF OBJECT_ID(N'[dbo].[EmployeePaidDeductionMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeePaidDeductionMap];
GO
IF OBJECT_ID(N'[dbo].[EmployeePaidLoan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeePaidLoan];
GO
IF OBJECT_ID(N'[dbo].[EmployeePaidSalary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeePaidSalary];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSalary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSalary];
GO
IF OBJECT_ID(N'[dbo].[EmployeeTypeMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeTypeMaster];
GO
IF OBJECT_ID(N'[dbo].[EmployeeWorkingDay]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeWorkingDay];
GO
IF OBJECT_ID(N'[dbo].[FinancialYearMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinancialYearMaster];
GO
IF OBJECT_ID(N'[dbo].[GlobalSetting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GlobalSetting];
GO
IF OBJECT_ID(N'[dbo].[History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[History];
GO
IF OBJECT_ID(N'[dbo].[HolidayMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HolidayMaster];
GO
IF OBJECT_ID(N'[dbo].[InterviewAttachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InterviewAttachment];
GO
IF OBJECT_ID(N'[dbo].[InterviewMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InterviewMaster];
GO
IF OBJECT_ID(N'[dbo].[InvoiceDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceDetail];
GO
IF OBJECT_ID(N'[dbo].[InvoiceMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceMaster];
GO
IF OBJECT_ID(N'[dbo].[IpInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IpInformation];
GO
IF OBJECT_ID(N'[dbo].[LeaveCategoryMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveCategoryMaster];
GO
IF OBJECT_ID(N'[dbo].[LicenseKeyMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LicenseKeyMaster];
GO
IF OBJECT_ID(N'[dbo].[ModuleMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModuleMaster];
GO
IF OBJECT_ID(N'[dbo].[RoleMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleMaster];
GO
IF OBJECT_ID(N'[dbo].[ShiftMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShiftMaster];
GO
IF OBJECT_ID(N'[dbo].[StateMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateMaster];
GO
IF OBJECT_ID(N'[dbo].[UserMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMaster];
GO
IF OBJECT_ID(N'[dbo].[UserModuleMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserModuleMap];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AllowanceMasters'
CREATE TABLE [dbo].[AllowanceMasters] (
    [AllowanceID] uniqueidentifier  NOT NULL,
    [Allowance] varchar(50)  NOT NULL,
    [IsConsider] bit  NOT NULL,
    [SortNo] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Percentage] float  NULL
);
GO

-- Creating table 'CompanyMasters'
CREATE TABLE [dbo].[CompanyMasters] (
    [CompanyID] uniqueidentifier  NOT NULL,
    [CompanyName] varchar(100)  NOT NULL,
    [CompanyLogo] varchar(50)  NULL,
    [EmailAddress] varchar(200)  NOT NULL,
    [CountryId] uniqueidentifier  NULL,
    [StateId] uniqueidentifier  NULL,
    [City] varchar(50)  NULL,
    [Address] varchar(1000)  NULL,
    [MobileNo] varchar(15)  NULL,
    [PhoneNo] varchar(15)  NULL,
    [HotlineNo] varchar(15)  NULL,
    [FaxNo] varchar(15)  NULL,
    [Website] varchar(500)  NULL,
    [LicenseKey] varchar(50)  NULL,
    [IsKeyActive] bit  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'CountryMasters'
CREATE TABLE [dbo].[CountryMasters] (
    [CountryID] uniqueidentifier  NOT NULL,
    [CountryName] varchar(100)  NOT NULL,
    [Code] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'CurrencyMasters'
CREATE TABLE [dbo].[CurrencyMasters] (
    [CurrencyID] int IDENTITY(1,1) NOT NULL,
    [CurrencyCode] varchar(10)  NOT NULL,
    [CurrencySymbol] nvarchar(50)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'DeductionMasters'
CREATE TABLE [dbo].[DeductionMasters] (
    [DeductionID] uniqueidentifier  NOT NULL,
    [Deduction] varchar(50)  NOT NULL,
    [IsConsider] bit  NOT NULL,
    [SortNo] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'DepartmentMasters'
CREATE TABLE [dbo].[DepartmentMasters] (
    [DepartmentID] uniqueidentifier  NOT NULL,
    [Department] varchar(100)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'DesignationMasters'
CREATE TABLE [dbo].[DesignationMasters] (
    [DesignationID] uniqueidentifier  NOT NULL,
    [Designation] varchar(100)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'DeviceMasters'
CREATE TABLE [dbo].[DeviceMasters] (
    [DeviceID] uniqueidentifier  NOT NULL,
    [DeviceName] nvarchar(100)  NULL,
    [Address] nvarchar(500)  NULL,
    [DeviceCode] nvarchar(20)  NULL,
    [PhoneNo] nvarchar(20)  NULL,
    [Port] int  NULL,
    [IPAddress] nvarchar(50)  NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'EducationMasters'
CREATE TABLE [dbo].[EducationMasters] (
    [EducationID] uniqueidentifier  NOT NULL,
    [EducationName] varchar(100)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeAllowanceMaps'
CREATE TABLE [dbo].[EmployeeAllowanceMaps] (
    [EmployeeAllowanceMapID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [AllowanceId] uniqueidentifier  NOT NULL,
    [Amount] decimal(12,2)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeAttachments'
CREATE TABLE [dbo].[EmployeeAttachments] (
    [EmployeeAttachmentMapID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Description] varchar(1000)  NULL,
    [AttachmentName] varchar(50)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeAttendances'
CREATE TABLE [dbo].[EmployeeAttendances] (
    [EmployeeAttendanceID] uniqueidentifier  NOT NULL,
    [FinancialYearId] uniqueidentifier  NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [AttendanceDate] datetime  NOT NULL,
    [TimeIn] varchar(50)  NULL,
    [TimeOut] varchar(50)  NULL,
    [WorkingHours] decimal(10,2)  NULL,
    [OverTimeHours] decimal(10,2)  NULL,
    [AttendanceType] int  NULL,
    [Attendance] decimal(6,2)  NULL,
    [Description] varchar(1000)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeAttendanceDevices'
CREATE TABLE [dbo].[EmployeeAttendanceDevices] (
    [EmployeeAttendanceID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NULL,
    [DeviceId] uniqueidentifier  NULL,
    [ShiftId] uniqueidentifier  NULL,
    [EnrollNo] nvarchar(15)  NULL,
    [AttendanceDate] datetime  NULL,
    [AttendanceDateTime] datetime  NULL,
    [PunchTime] nvarchar(11)  NULL,
    [VerifyMethod] nvarchar(10)  NULL,
    [PunchType] varchar(10)  NULL,
    [PunchMethod] varchar(10)  NULL,
    [Latitude] nvarchar(1000)  NULL,
    [Longitude] nvarchar(1000)  NULL,
    [LocationName] nvarchar(250)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'EmployeeDeductionMaps'
CREATE TABLE [dbo].[EmployeeDeductionMaps] (
    [EmployeeDeductionMapID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [DeductionId] uniqueidentifier  NOT NULL,
    [Amount] decimal(12,2)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeDeviceMaps'
CREATE TABLE [dbo].[EmployeeDeviceMaps] (
    [EmployeeDeviceID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NULL,
    [DeviceId] uniqueidentifier  NULL,
    [EnrollNo] nvarchar(15)  NULL,
    [CreatedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'EmployeeGradeMasters'
CREATE TABLE [dbo].[EmployeeGradeMasters] (
    [EmployeeGradeID] uniqueidentifier  NOT NULL,
    [EmployeeGrade] varchar(50)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeLeaveCategories'
CREATE TABLE [dbo].[EmployeeLeaveCategories] (
    [EmployeeLeaveCategoryMapID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [LeaveCategoryId] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [TotalDay] decimal(12,2)  NULL,
    [IsFirstHalfDay] bit  NOT NULL,
    [IsLastHalfDay] bit  NOT NULL,
    [Reason] varchar(1000)  NOT NULL,
    [Comments] varchar(1000)  NULL,
    [ApplyDate] datetime  NULL,
    [ApprovedBy] varchar(150)  NULL,
    [ApproveDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsApprove] bit  NULL
);
GO

-- Creating table 'EmployeeLeaveMaps'
CREATE TABLE [dbo].[EmployeeLeaveMaps] (
    [EmployeeLeaveID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NULL,
    [LeaveId] uniqueidentifier  NULL,
    [LeaveCount] int  NULL
);
GO

-- Creating table 'EmployeeLoans'
CREATE TABLE [dbo].[EmployeeLoans] (
    [EmployeeLoanMapID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [Amount] decimal(12,2)  NOT NULL,
    [LoanDate] datetime  NOT NULL,
    [LoanTitle] varchar(100)  NULL,
    [Description] varchar(max)  NULL,
    [ApprovedBy] varchar(150)  NULL,
    [TotalMonths] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsComplete] bit  NOT NULL
);
GO

-- Creating table 'EmployeeMasters'
CREATE TABLE [dbo].[EmployeeMasters] (
    [EmployeeID] uniqueidentifier  NOT NULL,
    [EmployeeTypeId] uniqueidentifier  NOT NULL,
    [EmployeeGradeId] uniqueidentifier  NULL,
    [DepartmentId] uniqueidentifier  NOT NULL,
    [DesignationId] uniqueidentifier  NOT NULL,
    [ShiftId] uniqueidentifier  NOT NULL,
    [FirstName] varchar(50)  NULL,
    [MiddleName] varchar(50)  NULL,
    [LastName] varchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Gender] bit  NULL,
    [MaratialStatus] varchar(50)  NULL,
    [PhotoName] varchar(50)  NULL,
    [CountryId] uniqueidentifier  NULL,
    [StateId] uniqueidentifier  NULL,
    [City] varchar(50)  NULL,
    [Address] varchar(1000)  NULL,
    [PinCode] varchar(10)  NULL,
    [MobileNo] varchar(15)  NULL,
    [PhoneNo] varchar(15)  NULL,
    [JoinDate] datetime  NULL,
    [EmployeeNo] int  NOT NULL,
    [Email] varchar(200)  NULL,
    [BankName] varchar(50)  NULL,
    [BranchName] varchar(50)  NULL,
    [AccountName] varchar(150)  NULL,
    [AccountNo] varchar(50)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsLeave] bit  NOT NULL,
    [LeaveDate] datetime  NULL,
    [LeaveDescription] varchar(max)  NULL,
    [Previlage] nvarchar(20)  NULL,
    [Password] nvarchar(50)  NULL,
    [FaceTemplate] nvarchar(max)  NULL,
    [IsHavingFace] bit  NULL,
    [FaceLength] int  NULL,
    [FingureTemplate] nvarchar(max)  NULL,
    [finger_template_data_bw] varbinary(max)  NULL,
    [finger_template_data_tft] varbinary(max)  NULL,
    [finger_template_data_tft1] varbinary(max)  NULL,
    [finger_template_data_tft2] varbinary(max)  NULL,
    [finger_template_data_tft3] varbinary(max)  NULL,
    [finger_template_data_tft4] varbinary(max)  NULL,
    [finger_template_data_tft5] varbinary(max)  NULL,
    [finger_template_data_tft6] varbinary(max)  NULL,
    [finger_template_data_tft7] varbinary(max)  NULL,
    [finger_template_data_tft8] varbinary(max)  NULL,
    [finger_template_data_tft9] varbinary(max)  NULL,
    [finger_template_data_bw1] varbinary(max)  NULL,
    [finger_template_data_bw2] varbinary(max)  NULL,
    [finger_template_data_bw3] varbinary(max)  NULL,
    [finger_template_data_bw4] varbinary(max)  NULL,
    [finger_template_data_bw5] varbinary(max)  NULL,
    [finger_template_data_bw6] varbinary(max)  NULL,
    [finger_template_data_bw7] varbinary(max)  NULL,
    [finger_template_data_bw8] varbinary(max)  NULL,
    [finger_template_data_bw9] varbinary(max)  NULL,
    [is_having_fingureprint] bit  NULL,
    [IsSend] bit  NULL,
    [FaceTemplateData] varbinary(max)  NULL,
    [PANNo] nchar(10)  NULL,
    [TotalLeaveCount] int  NULL,
    [OverTimeAmount] decimal(18,2)  NULL
);
GO

-- Creating table 'EmployeePaidAllowanceMaps'
CREATE TABLE [dbo].[EmployeePaidAllowanceMaps] (
    [EmployeePaidAllowanceMapID] uniqueidentifier  NOT NULL,
    [EmployeePaidSalaryId] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [AllowanceId] uniqueidentifier  NOT NULL,
    [Amount] decimal(12,2)  NULL,
    [PaidAmount] decimal(12,2)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeePaidDeductionMaps'
CREATE TABLE [dbo].[EmployeePaidDeductionMaps] (
    [EmployeePaidDeductionMapID] uniqueidentifier  NOT NULL,
    [EmployeePaidSalaryId] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [DeductionId] uniqueidentifier  NOT NULL,
    [Amount] decimal(12,2)  NULL,
    [PaidAmount] decimal(12,2)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeePaidLoans'
CREATE TABLE [dbo].[EmployeePaidLoans] (
    [EmployeePaidLoanMapID] uniqueidentifier  NOT NULL,
    [EmployeeLoanMapId] uniqueidentifier  NOT NULL,
    [PaidAmount] decimal(12,2)  NOT NULL,
    [PaidDate] datetime  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Month] varchar(15)  NOT NULL,
    [Year] int  NOT NULL
);
GO

-- Creating table 'EmployeePaidSalaries'
CREATE TABLE [dbo].[EmployeePaidSalaries] (
    [EmployeePaidSalaryID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [Basic] decimal(12,2)  NULL,
    [TotalEarning] decimal(12,2)  NULL,
    [TotalDeduction] decimal(12,2)  NULL,
    [TotalSalary] decimal(12,2)  NULL,
    [PaidBasic] decimal(12,2)  NULL,
    [PaidTotalEarning] decimal(12,2)  NULL,
    [PaidTotalDeduction] decimal(12,2)  NULL,
    [PaidTotalSalary] decimal(12,2)  NULL,
    [Month] varchar(15)  NOT NULL,
    [Year] int  NOT NULL,
    [TotalOverTimeDays] decimal(12,2)  NULL,
    [TotalOverTimeHours] decimal(18,2)  NULL,
    [TotalDays] int  NOT NULL,
    [TotalHours] decimal(18,2)  NULL,
    [AllowLeave] decimal(6,2)  NULL,
    [TotalUseLeave] decimal(6,1)  NULL,
    [TotalHoliday] decimal(6,1)  NULL,
    [TotalPaidLeave] decimal(6,1)  NULL,
    [TotalPaidLeaveAmount] decimal(12,2)  NULL,
    [TotalOverTimeAmount] decimal(12,2)  NULL,
    [PaidLoanAmount] decimal(12,2)  NULL,
    [PaidDate] datetime  NOT NULL,
    [PaidBy] varchar(150)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsPaid] bit  NOT NULL,
    [FinancialYearId] uniqueidentifier  NOT NULL,
    [TotalPresentDays] decimal(6,1)  NULL,
    [ProfessionalTax] decimal(12,2)  NULL,
    [SalaryFromDate] datetime  NULL,
    [SalaryToDate] datetime  NULL
);
GO

-- Creating table 'EmployeeSalaries'
CREATE TABLE [dbo].[EmployeeSalaries] (
    [EmployeeSalaryID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [Basic] decimal(12,2)  NULL,
    [TotalEarning] decimal(12,2)  NULL,
    [TotalDeduction] decimal(12,2)  NULL,
    [TotalSalary] decimal(12,2)  NULL,
    [SalaryType] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeTypeMasters'
CREATE TABLE [dbo].[EmployeeTypeMasters] (
    [EmployeeTypeID] uniqueidentifier  NOT NULL,
    [EmployeeType] varchar(50)  NOT NULL,
    [NoOfLeavePerMonth] decimal(5,1)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'EmployeeWorkingDays'
CREATE TABLE [dbo].[EmployeeWorkingDays] (
    [EmployeeWorkingDayMapID] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NOT NULL,
    [DayName] varchar(15)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'FinancialYearMasters'
CREATE TABLE [dbo].[FinancialYearMasters] (
    [FinancialYearID] uniqueidentifier  NOT NULL,
    [Year] int  NOT NULL,
    [FinancialYear] varchar(15)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [HistoryID] uniqueidentifier  NOT NULL,
    [Description] varchar(max)  NULL,
    [TableId] varchar(50)  NULL,
    [TableTypeId] int  NOT NULL,
    [OperationTypeId] int  NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [CreatedDate] datetime  NOT NULL,
    [XmlContent] varchar(max)  NULL,
    [IPAddress] varchar(20)  NULL
);
GO

-- Creating table 'HolidayMasters'
CREATE TABLE [dbo].[HolidayMasters] (
    [HolidayID] uniqueidentifier  NOT NULL,
    [Title] varchar(100)  NOT NULL,
    [Description] varchar(max)  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'InterviewAttachments'
CREATE TABLE [dbo].[InterviewAttachments] (
    [InterviewAttachmentMapID] uniqueidentifier  NOT NULL,
    [InterviewId] uniqueidentifier  NOT NULL,
    [Name] varchar(100)  NULL,
    [AttachmentType] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'InterviewMasters'
CREATE TABLE [dbo].[InterviewMasters] (
    [InterviewID] uniqueidentifier  NOT NULL,
    [InterviewNo] int  NOT NULL,
    [Name] varchar(200)  NOT NULL,
    [Email] varchar(200)  NOT NULL,
    [MobileNo] varchar(15)  NOT NULL,
    [EducationId] uniqueidentifier  NOT NULL,
    [DepartmentId] uniqueidentifier  NOT NULL,
    [DesignationId] uniqueidentifier  NOT NULL,
    [CurrentSalary] decimal(18,2)  NULL,
    [ExpectedSalary] decimal(18,2)  NULL,
    [ExperienceYear] int  NULL,
    [ExperienceMonth] int  NULL,
    [IsJoinDays] bit  NULL,
    [JoinAfterDaysOrMonth] int  NULL,
    [PersonalDetail] varchar(max)  NULL,
    [InterviewStatusId] int  NULL,
    [InterviewDate] datetime  NULL,
    [InterviewTime] varchar(50)  NULL,
    [JoinDate] datetime  NULL,
    [Reason] varchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'InvoiceDetails'
CREATE TABLE [dbo].[InvoiceDetails] (
    [InvoiceDetailID] int IDENTITY(1,1) NOT NULL,
    [InvoiceId] int  NULL,
    [ItemDescription] nvarchar(100)  NOT NULL,
    [ItemDate] datetime  NOT NULL,
    [Hours] decimal(8,2)  NULL,
    [HourRate] decimal(8,2)  NULL,
    [Amount] decimal(12,2)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'InvoiceMasters'
CREATE TABLE [dbo].[InvoiceMasters] (
    [InvoiceID] int IDENTITY(1,1) NOT NULL,
    [InvoiceNo] int  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [CurrencyId] int  NULL,
    [PartyName] nvarchar(50)  NULL,
    [PartyAddress] nvarchar(500)  NULL,
    [Description] nvarchar(100)  NULL,
    [SubTotal] decimal(12,2)  NULL,
    [ServiceTax] decimal(12,2)  NULL,
    [GrandTotal] decimal(12,2)  NULL,
    [SubTotalINR] decimal(12,2)  NULL,
    [ServiceTaxINR] decimal(12,2)  NULL,
    [GrandTotalINR] decimal(12,2)  NULL,
    [IsFixed] bit  NULL,
    [IsPaid] bit  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'IpInformations'
CREATE TABLE [dbo].[IpInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [IpAddress] nvarchar(100)  NULL,
    [DeviceName] nvarchar(max)  NULL,
    [BrowserName] nvarchar(100)  NULL,
    [DeviceType] varchar(100)  NULL,
    [CreatedDate] datetime  NULL
);
GO

-- Creating table 'LeaveCategoryMasters'
CREATE TABLE [dbo].[LeaveCategoryMasters] (
    [LeaveCategoryID] uniqueidentifier  NOT NULL,
    [LeaveCategory] varchar(100)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'LicenseKeyMasters'
CREATE TABLE [dbo].[LicenseKeyMasters] (
    [LicenseKeyID] uniqueidentifier  NOT NULL,
    [Email] nvarchar(200)  NOT NULL,
    [KeyID] nvarchar(50)  NOT NULL,
    [IsUsed] bit  NOT NULL,
    [CreatedBy] uniqueidentifier  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ModuleMasters'
CREATE TABLE [dbo].[ModuleMasters] (
    [ModuleID] uniqueidentifier  NOT NULL,
    [EnumName] varchar(200)  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [ParentId] uniqueidentifier  NULL,
    [TreeLevel] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [SortOrder] int  NOT NULL
);
GO

-- Creating table 'RoleMasters'
CREATE TABLE [dbo].[RoleMasters] (
    [RoleID] uniqueidentifier  NOT NULL,
    [RoleName] varchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ShiftMasters'
CREATE TABLE [dbo].[ShiftMasters] (
    [ShiftID] uniqueidentifier  NOT NULL,
    [Shift] varchar(50)  NOT NULL,
    [FromTime] varchar(20)  NULL,
    [ToTime] varchar(20)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'StateMasters'
CREATE TABLE [dbo].[StateMasters] (
    [StateID] uniqueidentifier  NOT NULL,
    [CountryId] uniqueidentifier  NULL,
    [StateName] varchar(100)  NULL,
    [CreatedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'UserMasters'
CREATE TABLE [dbo].[UserMasters] (
    [UserID] uniqueidentifier  NOT NULL,
    [RoleId] uniqueidentifier  NOT NULL,
    [EmployeeId] uniqueidentifier  NULL,
    [Username] varchar(200)  NOT NULL,
    [Password] nvarchar(500)  NOT NULL,
    [LastLogin] datetime  NULL,
    [Token] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NULL,
    [ModifiedBy] uniqueidentifier  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'UserModuleMaps'
CREATE TABLE [dbo].[UserModuleMaps] (
    [UserModuleMapID] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [ModuleId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'GlobalSettings'
CREATE TABLE [dbo].[GlobalSettings] (
    [GlobalSettingId] int IDENTITY(1,1) NOT NULL,
    [GlobalSettingName] nvarchar(50)  NOT NULL,
    [GlobalSettingEnum] nvarchar(50)  NOT NULL,
    [GlobalSettingValue] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AllowanceID] in table 'AllowanceMasters'
ALTER TABLE [dbo].[AllowanceMasters]
ADD CONSTRAINT [PK_AllowanceMasters]
    PRIMARY KEY CLUSTERED ([AllowanceID] ASC);
GO

-- Creating primary key on [CompanyID] in table 'CompanyMasters'
ALTER TABLE [dbo].[CompanyMasters]
ADD CONSTRAINT [PK_CompanyMasters]
    PRIMARY KEY CLUSTERED ([CompanyID] ASC);
GO

-- Creating primary key on [CountryID] in table 'CountryMasters'
ALTER TABLE [dbo].[CountryMasters]
ADD CONSTRAINT [PK_CountryMasters]
    PRIMARY KEY CLUSTERED ([CountryID] ASC);
GO

-- Creating primary key on [CurrencyID] in table 'CurrencyMasters'
ALTER TABLE [dbo].[CurrencyMasters]
ADD CONSTRAINT [PK_CurrencyMasters]
    PRIMARY KEY CLUSTERED ([CurrencyID] ASC);
GO

-- Creating primary key on [DeductionID] in table 'DeductionMasters'
ALTER TABLE [dbo].[DeductionMasters]
ADD CONSTRAINT [PK_DeductionMasters]
    PRIMARY KEY CLUSTERED ([DeductionID] ASC);
GO

-- Creating primary key on [DepartmentID] in table 'DepartmentMasters'
ALTER TABLE [dbo].[DepartmentMasters]
ADD CONSTRAINT [PK_DepartmentMasters]
    PRIMARY KEY CLUSTERED ([DepartmentID] ASC);
GO

-- Creating primary key on [DesignationID] in table 'DesignationMasters'
ALTER TABLE [dbo].[DesignationMasters]
ADD CONSTRAINT [PK_DesignationMasters]
    PRIMARY KEY CLUSTERED ([DesignationID] ASC);
GO

-- Creating primary key on [DeviceID] in table 'DeviceMasters'
ALTER TABLE [dbo].[DeviceMasters]
ADD CONSTRAINT [PK_DeviceMasters]
    PRIMARY KEY CLUSTERED ([DeviceID] ASC);
GO

-- Creating primary key on [EducationID] in table 'EducationMasters'
ALTER TABLE [dbo].[EducationMasters]
ADD CONSTRAINT [PK_EducationMasters]
    PRIMARY KEY CLUSTERED ([EducationID] ASC);
GO

-- Creating primary key on [EmployeeAllowanceMapID] in table 'EmployeeAllowanceMaps'
ALTER TABLE [dbo].[EmployeeAllowanceMaps]
ADD CONSTRAINT [PK_EmployeeAllowanceMaps]
    PRIMARY KEY CLUSTERED ([EmployeeAllowanceMapID] ASC);
GO

-- Creating primary key on [EmployeeAttachmentMapID] in table 'EmployeeAttachments'
ALTER TABLE [dbo].[EmployeeAttachments]
ADD CONSTRAINT [PK_EmployeeAttachments]
    PRIMARY KEY CLUSTERED ([EmployeeAttachmentMapID] ASC);
GO

-- Creating primary key on [EmployeeAttendanceID] in table 'EmployeeAttendances'
ALTER TABLE [dbo].[EmployeeAttendances]
ADD CONSTRAINT [PK_EmployeeAttendances]
    PRIMARY KEY CLUSTERED ([EmployeeAttendanceID] ASC);
GO

-- Creating primary key on [EmployeeAttendanceID] in table 'EmployeeAttendanceDevices'
ALTER TABLE [dbo].[EmployeeAttendanceDevices]
ADD CONSTRAINT [PK_EmployeeAttendanceDevices]
    PRIMARY KEY CLUSTERED ([EmployeeAttendanceID] ASC);
GO

-- Creating primary key on [EmployeeDeductionMapID] in table 'EmployeeDeductionMaps'
ALTER TABLE [dbo].[EmployeeDeductionMaps]
ADD CONSTRAINT [PK_EmployeeDeductionMaps]
    PRIMARY KEY CLUSTERED ([EmployeeDeductionMapID] ASC);
GO

-- Creating primary key on [EmployeeDeviceID] in table 'EmployeeDeviceMaps'
ALTER TABLE [dbo].[EmployeeDeviceMaps]
ADD CONSTRAINT [PK_EmployeeDeviceMaps]
    PRIMARY KEY CLUSTERED ([EmployeeDeviceID] ASC);
GO

-- Creating primary key on [EmployeeGradeID] in table 'EmployeeGradeMasters'
ALTER TABLE [dbo].[EmployeeGradeMasters]
ADD CONSTRAINT [PK_EmployeeGradeMasters]
    PRIMARY KEY CLUSTERED ([EmployeeGradeID] ASC);
GO

-- Creating primary key on [EmployeeLeaveCategoryMapID] in table 'EmployeeLeaveCategories'
ALTER TABLE [dbo].[EmployeeLeaveCategories]
ADD CONSTRAINT [PK_EmployeeLeaveCategories]
    PRIMARY KEY CLUSTERED ([EmployeeLeaveCategoryMapID] ASC);
GO

-- Creating primary key on [EmployeeLeaveID] in table 'EmployeeLeaveMaps'
ALTER TABLE [dbo].[EmployeeLeaveMaps]
ADD CONSTRAINT [PK_EmployeeLeaveMaps]
    PRIMARY KEY CLUSTERED ([EmployeeLeaveID] ASC);
GO

-- Creating primary key on [EmployeeLoanMapID] in table 'EmployeeLoans'
ALTER TABLE [dbo].[EmployeeLoans]
ADD CONSTRAINT [PK_EmployeeLoans]
    PRIMARY KEY CLUSTERED ([EmployeeLoanMapID] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'EmployeeMasters'
ALTER TABLE [dbo].[EmployeeMasters]
ADD CONSTRAINT [PK_EmployeeMasters]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [EmployeePaidAllowanceMapID] in table 'EmployeePaidAllowanceMaps'
ALTER TABLE [dbo].[EmployeePaidAllowanceMaps]
ADD CONSTRAINT [PK_EmployeePaidAllowanceMaps]
    PRIMARY KEY CLUSTERED ([EmployeePaidAllowanceMapID] ASC);
GO

-- Creating primary key on [EmployeePaidDeductionMapID] in table 'EmployeePaidDeductionMaps'
ALTER TABLE [dbo].[EmployeePaidDeductionMaps]
ADD CONSTRAINT [PK_EmployeePaidDeductionMaps]
    PRIMARY KEY CLUSTERED ([EmployeePaidDeductionMapID] ASC);
GO

-- Creating primary key on [EmployeePaidLoanMapID] in table 'EmployeePaidLoans'
ALTER TABLE [dbo].[EmployeePaidLoans]
ADD CONSTRAINT [PK_EmployeePaidLoans]
    PRIMARY KEY CLUSTERED ([EmployeePaidLoanMapID] ASC);
GO

-- Creating primary key on [EmployeePaidSalaryID] in table 'EmployeePaidSalaries'
ALTER TABLE [dbo].[EmployeePaidSalaries]
ADD CONSTRAINT [PK_EmployeePaidSalaries]
    PRIMARY KEY CLUSTERED ([EmployeePaidSalaryID] ASC);
GO

-- Creating primary key on [EmployeeSalaryID] in table 'EmployeeSalaries'
ALTER TABLE [dbo].[EmployeeSalaries]
ADD CONSTRAINT [PK_EmployeeSalaries]
    PRIMARY KEY CLUSTERED ([EmployeeSalaryID] ASC);
GO

-- Creating primary key on [EmployeeTypeID] in table 'EmployeeTypeMasters'
ALTER TABLE [dbo].[EmployeeTypeMasters]
ADD CONSTRAINT [PK_EmployeeTypeMasters]
    PRIMARY KEY CLUSTERED ([EmployeeTypeID] ASC);
GO

-- Creating primary key on [EmployeeWorkingDayMapID] in table 'EmployeeWorkingDays'
ALTER TABLE [dbo].[EmployeeWorkingDays]
ADD CONSTRAINT [PK_EmployeeWorkingDays]
    PRIMARY KEY CLUSTERED ([EmployeeWorkingDayMapID] ASC);
GO

-- Creating primary key on [FinancialYearID] in table 'FinancialYearMasters'
ALTER TABLE [dbo].[FinancialYearMasters]
ADD CONSTRAINT [PK_FinancialYearMasters]
    PRIMARY KEY CLUSTERED ([FinancialYearID] ASC);
GO

-- Creating primary key on [HistoryID] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([HistoryID] ASC);
GO

-- Creating primary key on [HolidayID] in table 'HolidayMasters'
ALTER TABLE [dbo].[HolidayMasters]
ADD CONSTRAINT [PK_HolidayMasters]
    PRIMARY KEY CLUSTERED ([HolidayID] ASC);
GO

-- Creating primary key on [InterviewAttachmentMapID] in table 'InterviewAttachments'
ALTER TABLE [dbo].[InterviewAttachments]
ADD CONSTRAINT [PK_InterviewAttachments]
    PRIMARY KEY CLUSTERED ([InterviewAttachmentMapID] ASC);
GO

-- Creating primary key on [InterviewID] in table 'InterviewMasters'
ALTER TABLE [dbo].[InterviewMasters]
ADD CONSTRAINT [PK_InterviewMasters]
    PRIMARY KEY CLUSTERED ([InterviewID] ASC);
GO

-- Creating primary key on [InvoiceDetailID] in table 'InvoiceDetails'
ALTER TABLE [dbo].[InvoiceDetails]
ADD CONSTRAINT [PK_InvoiceDetails]
    PRIMARY KEY CLUSTERED ([InvoiceDetailID] ASC);
GO

-- Creating primary key on [InvoiceID] in table 'InvoiceMasters'
ALTER TABLE [dbo].[InvoiceMasters]
ADD CONSTRAINT [PK_InvoiceMasters]
    PRIMARY KEY CLUSTERED ([InvoiceID] ASC);
GO

-- Creating primary key on [Id] in table 'IpInformations'
ALTER TABLE [dbo].[IpInformations]
ADD CONSTRAINT [PK_IpInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LeaveCategoryID] in table 'LeaveCategoryMasters'
ALTER TABLE [dbo].[LeaveCategoryMasters]
ADD CONSTRAINT [PK_LeaveCategoryMasters]
    PRIMARY KEY CLUSTERED ([LeaveCategoryID] ASC);
GO

-- Creating primary key on [LicenseKeyID] in table 'LicenseKeyMasters'
ALTER TABLE [dbo].[LicenseKeyMasters]
ADD CONSTRAINT [PK_LicenseKeyMasters]
    PRIMARY KEY CLUSTERED ([LicenseKeyID] ASC);
GO

-- Creating primary key on [ModuleID] in table 'ModuleMasters'
ALTER TABLE [dbo].[ModuleMasters]
ADD CONSTRAINT [PK_ModuleMasters]
    PRIMARY KEY CLUSTERED ([ModuleID] ASC);
GO

-- Creating primary key on [RoleID] in table 'RoleMasters'
ALTER TABLE [dbo].[RoleMasters]
ADD CONSTRAINT [PK_RoleMasters]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [ShiftID] in table 'ShiftMasters'
ALTER TABLE [dbo].[ShiftMasters]
ADD CONSTRAINT [PK_ShiftMasters]
    PRIMARY KEY CLUSTERED ([ShiftID] ASC);
GO

-- Creating primary key on [StateID] in table 'StateMasters'
ALTER TABLE [dbo].[StateMasters]
ADD CONSTRAINT [PK_StateMasters]
    PRIMARY KEY CLUSTERED ([StateID] ASC);
GO

-- Creating primary key on [UserID] in table 'UserMasters'
ALTER TABLE [dbo].[UserMasters]
ADD CONSTRAINT [PK_UserMasters]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [UserModuleMapID] in table 'UserModuleMaps'
ALTER TABLE [dbo].[UserModuleMaps]
ADD CONSTRAINT [PK_UserModuleMaps]
    PRIMARY KEY CLUSTERED ([UserModuleMapID] ASC);
GO

-- Creating primary key on [GlobalSettingId] in table 'GlobalSettings'
ALTER TABLE [dbo].[GlobalSettings]
ADD CONSTRAINT [PK_GlobalSettings]
    PRIMARY KEY CLUSTERED ([GlobalSettingId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AllowanceId] in table 'EmployeeAllowanceMaps'
ALTER TABLE [dbo].[EmployeeAllowanceMaps]
ADD CONSTRAINT [FK_EmployeeAllowanceMap_AllowanceMaster]
    FOREIGN KEY ([AllowanceId])
    REFERENCES [dbo].[AllowanceMasters]
        ([AllowanceID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAllowanceMap_AllowanceMaster'
CREATE INDEX [IX_FK_EmployeeAllowanceMap_AllowanceMaster]
ON [dbo].[EmployeeAllowanceMaps]
    ([AllowanceId]);
GO

-- Creating foreign key on [CountryId] in table 'StateMasters'
ALTER TABLE [dbo].[StateMasters]
ADD CONSTRAINT [FK_StateMaster_CountryMaster]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[CountryMasters]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateMaster_CountryMaster'
CREATE INDEX [IX_FK_StateMaster_CountryMaster]
ON [dbo].[StateMasters]
    ([CountryId]);
GO

-- Creating foreign key on [CurrencyId] in table 'InvoiceMasters'
ALTER TABLE [dbo].[InvoiceMasters]
ADD CONSTRAINT [FK_InvoiceMaster_CurrencyMaster]
    FOREIGN KEY ([CurrencyId])
    REFERENCES [dbo].[CurrencyMasters]
        ([CurrencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceMaster_CurrencyMaster'
CREATE INDEX [IX_FK_InvoiceMaster_CurrencyMaster]
ON [dbo].[InvoiceMasters]
    ([CurrencyId]);
GO

-- Creating foreign key on [DeductionId] in table 'EmployeeDeductionMaps'
ALTER TABLE [dbo].[EmployeeDeductionMaps]
ADD CONSTRAINT [FK_EmployeeDeductionMap_DeductionMaster]
    FOREIGN KEY ([DeductionId])
    REFERENCES [dbo].[DeductionMasters]
        ([DeductionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDeductionMap_DeductionMaster'
CREATE INDEX [IX_FK_EmployeeDeductionMap_DeductionMaster]
ON [dbo].[EmployeeDeductionMaps]
    ([DeductionId]);
GO

-- Creating foreign key on [DepartmentId] in table 'EmployeeMasters'
ALTER TABLE [dbo].[EmployeeMasters]
ADD CONSTRAINT [FK_EmployeeMaster_DepartmentMaster]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[DepartmentMasters]
        ([DepartmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMaster_DepartmentMaster'
CREATE INDEX [IX_FK_EmployeeMaster_DepartmentMaster]
ON [dbo].[EmployeeMasters]
    ([DepartmentId]);
GO

-- Creating foreign key on [DepartmentId] in table 'InterviewMasters'
ALTER TABLE [dbo].[InterviewMasters]
ADD CONSTRAINT [FK_InterviewMaster_DepartmentMaster]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[DepartmentMasters]
        ([DepartmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InterviewMaster_DepartmentMaster'
CREATE INDEX [IX_FK_InterviewMaster_DepartmentMaster]
ON [dbo].[InterviewMasters]
    ([DepartmentId]);
GO

-- Creating foreign key on [DesignationId] in table 'EmployeeMasters'
ALTER TABLE [dbo].[EmployeeMasters]
ADD CONSTRAINT [FK_EmployeeMaster_DesignationMaster]
    FOREIGN KEY ([DesignationId])
    REFERENCES [dbo].[DesignationMasters]
        ([DesignationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMaster_DesignationMaster'
CREATE INDEX [IX_FK_EmployeeMaster_DesignationMaster]
ON [dbo].[EmployeeMasters]
    ([DesignationId]);
GO

-- Creating foreign key on [DesignationId] in table 'InterviewMasters'
ALTER TABLE [dbo].[InterviewMasters]
ADD CONSTRAINT [FK_InterviewMaster_DesignationMaster]
    FOREIGN KEY ([DesignationId])
    REFERENCES [dbo].[DesignationMasters]
        ([DesignationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InterviewMaster_DesignationMaster'
CREATE INDEX [IX_FK_InterviewMaster_DesignationMaster]
ON [dbo].[InterviewMasters]
    ([DesignationId]);
GO

-- Creating foreign key on [EducationId] in table 'InterviewMasters'
ALTER TABLE [dbo].[InterviewMasters]
ADD CONSTRAINT [FK_InterviewMaster_EducationMaster]
    FOREIGN KEY ([EducationId])
    REFERENCES [dbo].[EducationMasters]
        ([EducationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InterviewMaster_EducationMaster'
CREATE INDEX [IX_FK_InterviewMaster_EducationMaster]
ON [dbo].[InterviewMasters]
    ([EducationId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeAllowanceMaps'
ALTER TABLE [dbo].[EmployeeAllowanceMaps]
ADD CONSTRAINT [FK_EmployeeAllowanceMap_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAllowanceMap_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeAllowanceMap_EmployeeMaster]
ON [dbo].[EmployeeAllowanceMaps]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeAttachments'
ALTER TABLE [dbo].[EmployeeAttachments]
ADD CONSTRAINT [FK_EmployeeAttachment_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAttachment_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeAttachment_EmployeeMaster]
ON [dbo].[EmployeeAttachments]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeAttendances'
ALTER TABLE [dbo].[EmployeeAttendances]
ADD CONSTRAINT [FK_EmployeeAttendance_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAttendance_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeAttendance_EmployeeMaster]
ON [dbo].[EmployeeAttendances]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeDeductionMaps'
ALTER TABLE [dbo].[EmployeeDeductionMaps]
ADD CONSTRAINT [FK_EmployeeDeductionMap_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDeductionMap_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeDeductionMap_EmployeeMaster]
ON [dbo].[EmployeeDeductionMaps]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeGradeId] in table 'EmployeeMasters'
ALTER TABLE [dbo].[EmployeeMasters]
ADD CONSTRAINT [FK_EmployeeMaster_EmployeeGradeMaster]
    FOREIGN KEY ([EmployeeGradeId])
    REFERENCES [dbo].[EmployeeGradeMasters]
        ([EmployeeGradeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMaster_EmployeeGradeMaster'
CREATE INDEX [IX_FK_EmployeeMaster_EmployeeGradeMaster]
ON [dbo].[EmployeeMasters]
    ([EmployeeGradeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeLeaveCategories'
ALTER TABLE [dbo].[EmployeeLeaveCategories]
ADD CONSTRAINT [FK_EmployeeLeaveCategory_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeLeaveCategory_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeLeaveCategory_EmployeeMaster]
ON [dbo].[EmployeeLeaveCategories]
    ([EmployeeId]);
GO

-- Creating foreign key on [LeaveCategoryId] in table 'EmployeeLeaveCategories'
ALTER TABLE [dbo].[EmployeeLeaveCategories]
ADD CONSTRAINT [FK_EmployeeLeaveCategory_LeaveCategoryMaster]
    FOREIGN KEY ([LeaveCategoryId])
    REFERENCES [dbo].[LeaveCategoryMasters]
        ([LeaveCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeLeaveCategory_LeaveCategoryMaster'
CREATE INDEX [IX_FK_EmployeeLeaveCategory_LeaveCategoryMaster]
ON [dbo].[EmployeeLeaveCategories]
    ([LeaveCategoryId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeLoans'
ALTER TABLE [dbo].[EmployeeLoans]
ADD CONSTRAINT [FK_EmployeeLoan_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeLoan_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeLoan_EmployeeMaster]
ON [dbo].[EmployeeLoans]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeLoanMapId] in table 'EmployeePaidLoans'
ALTER TABLE [dbo].[EmployeePaidLoans]
ADD CONSTRAINT [FK_EmployeePaidLoan_EmployeeLoan]
    FOREIGN KEY ([EmployeeLoanMapId])
    REFERENCES [dbo].[EmployeeLoans]
        ([EmployeeLoanMapID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePaidLoan_EmployeeLoan'
CREATE INDEX [IX_FK_EmployeePaidLoan_EmployeeLoan]
ON [dbo].[EmployeePaidLoans]
    ([EmployeeLoanMapId]);
GO

-- Creating foreign key on [EmployeeTypeId] in table 'EmployeeMasters'
ALTER TABLE [dbo].[EmployeeMasters]
ADD CONSTRAINT [FK_EmployeeMaster_EmployeeTypeMaster]
    FOREIGN KEY ([EmployeeTypeId])
    REFERENCES [dbo].[EmployeeTypeMasters]
        ([EmployeeTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMaster_EmployeeTypeMaster'
CREATE INDEX [IX_FK_EmployeeMaster_EmployeeTypeMaster]
ON [dbo].[EmployeeMasters]
    ([EmployeeTypeId]);
GO

-- Creating foreign key on [ShiftId] in table 'EmployeeMasters'
ALTER TABLE [dbo].[EmployeeMasters]
ADD CONSTRAINT [FK_EmployeeMaster_ShiftMaster]
    FOREIGN KEY ([ShiftId])
    REFERENCES [dbo].[ShiftMasters]
        ([ShiftID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMaster_ShiftMaster'
CREATE INDEX [IX_FK_EmployeeMaster_ShiftMaster]
ON [dbo].[EmployeeMasters]
    ([ShiftId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeePaidSalaries'
ALTER TABLE [dbo].[EmployeePaidSalaries]
ADD CONSTRAINT [FK_EmployeePaidSalary_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePaidSalary_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeePaidSalary_EmployeeMaster]
ON [dbo].[EmployeePaidSalaries]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeSalaries'
ALTER TABLE [dbo].[EmployeeSalaries]
ADD CONSTRAINT [FK_EmployeeSalary_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSalary_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeSalary_EmployeeMaster]
ON [dbo].[EmployeeSalaries]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeeWorkingDays'
ALTER TABLE [dbo].[EmployeeWorkingDays]
ADD CONSTRAINT [FK_EmployeeWorkingDay_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeWorkingDay_EmployeeMaster'
CREATE INDEX [IX_FK_EmployeeWorkingDay_EmployeeMaster]
ON [dbo].[EmployeeWorkingDays]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'UserMasters'
ALTER TABLE [dbo].[UserMasters]
ADD CONSTRAINT [FK_UserMaster_EmployeeMaster]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeMasters]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMaster_EmployeeMaster'
CREATE INDEX [IX_FK_UserMaster_EmployeeMaster]
ON [dbo].[UserMasters]
    ([EmployeeId]);
GO

-- Creating foreign key on [FinancialYearId] in table 'EmployeePaidSalaries'
ALTER TABLE [dbo].[EmployeePaidSalaries]
ADD CONSTRAINT [FK_EmployeePaidSalary_FinancialYearMaster]
    FOREIGN KEY ([FinancialYearId])
    REFERENCES [dbo].[FinancialYearMasters]
        ([FinancialYearID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePaidSalary_FinancialYearMaster'
CREATE INDEX [IX_FK_EmployeePaidSalary_FinancialYearMaster]
ON [dbo].[EmployeePaidSalaries]
    ([FinancialYearId]);
GO

-- Creating foreign key on [InterviewId] in table 'InterviewAttachments'
ALTER TABLE [dbo].[InterviewAttachments]
ADD CONSTRAINT [FK_InterviewAttechment_InterviewMaster]
    FOREIGN KEY ([InterviewId])
    REFERENCES [dbo].[InterviewMasters]
        ([InterviewID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InterviewAttechment_InterviewMaster'
CREATE INDEX [IX_FK_InterviewAttechment_InterviewMaster]
ON [dbo].[InterviewAttachments]
    ([InterviewId]);
GO

-- Creating foreign key on [InvoiceId] in table 'InvoiceDetails'
ALTER TABLE [dbo].[InvoiceDetails]
ADD CONSTRAINT [FK_InvoiceDetail_InvoiceMaster]
    FOREIGN KEY ([InvoiceId])
    REFERENCES [dbo].[InvoiceMasters]
        ([InvoiceID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceDetail_InvoiceMaster'
CREATE INDEX [IX_FK_InvoiceDetail_InvoiceMaster]
ON [dbo].[InvoiceDetails]
    ([InvoiceId]);
GO

-- Creating foreign key on [ModuleId] in table 'UserModuleMaps'
ALTER TABLE [dbo].[UserModuleMaps]
ADD CONSTRAINT [FK_UserModuleMap_ModuleMaster]
    FOREIGN KEY ([ModuleId])
    REFERENCES [dbo].[ModuleMasters]
        ([ModuleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserModuleMap_ModuleMaster'
CREATE INDEX [IX_FK_UserModuleMap_ModuleMaster]
ON [dbo].[UserModuleMaps]
    ([ModuleId]);
GO

-- Creating foreign key on [RoleId] in table 'UserMasters'
ALTER TABLE [dbo].[UserMasters]
ADD CONSTRAINT [FK_UserMaster_RoleMaster]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[RoleMasters]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMaster_RoleMaster'
CREATE INDEX [IX_FK_UserMaster_RoleMaster]
ON [dbo].[UserMasters]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'UserModuleMaps'
ALTER TABLE [dbo].[UserModuleMaps]
ADD CONSTRAINT [FK_UserModuleMap_UserMaster]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserMasters]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserModuleMap_UserMaster'
CREATE INDEX [IX_FK_UserModuleMap_UserMaster]
ON [dbo].[UserModuleMaps]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------