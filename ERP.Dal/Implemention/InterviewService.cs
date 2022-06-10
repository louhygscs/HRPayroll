using ERP.Common;
using ERP.Dal.Interface;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Implemention
{
    public class InterviewService : IInterviewService
    {

        public Result<List<Interview>> GetInterviewList()
        {
            Result<List<Interview>> _Result = new Result<List<Interview>>();

            _Result.IsSuccess = false;
            using (var dbContext = new ERPEntities())
            {
                try
                {
                    var _Query = dbContext.InterviewMasters.Where(x => x.IsActive == true)
                        .Select(s => new Interview
                        {
                            InterviewID = s.InterviewID,
                            CurrentSalary = s.CurrentSalary,
                            Department = s.DepartmentMaster.Department,
                            Designation = s.DesignationMaster.Designation,
                            Education = s.EducationMaster.EducationName,
                            ExpectedSalary = s.ExpectedSalary,
                            InterviewNo = s.InterviewNo,
                            ExperienceYear = s.ExperienceYear ?? 0,
                            ExperienceMonth = s.ExperienceMonth ?? 0,
                            InterviewStatusId = s.InterviewStatusId ?? 1,
                            IsJoinDays = s.IsJoinDays ?? false,
                            JoinAfterDaysOrMonth = s.JoinAfterDaysOrMonth ?? 0,
                            Mobile = s.MobileNo,
                            Name = s.Name,
                        });
                    if (_Query != null)
                    {
                        _Result.Data = _Query.ToList();
                        _Result.IsSuccess = true;
                    }
                }
                catch (Exception _Exception)
                {
                    _Result.IsSuccess = false;
                    _Result.Message = _Exception.Message;
                    _Result.Exception = _Exception;
                }
            }
            return _Result;
        }

        public Result<Interview> GetInterviewByInterviewId(Guid p_InterviewId)
        {
            Result<Interview> _Result = new Result<Interview>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.InterviewMasters.Where(x => x.IsActive == true && x.InterviewID == p_InterviewId)
                                           .Select(s => new Interview
                                           {
                                               InterviewID = s.InterviewID,
                                               CurrentSalary = s.CurrentSalary,
                                               Department = s.DepartmentMaster.Department,
                                               Designation = s.DesignationMaster.Designation,
                                               Education = s.EducationMaster.EducationName,
                                               ExpectedSalary = s.ExpectedSalary,
                                               InterviewNo = s.InterviewNo,
                                               ExperienceYear = s.ExperienceYear ?? 0,
                                               ExperienceMonth = s.ExperienceMonth ?? 0,
                                               InterviewStatusId = s.InterviewStatusId ?? 1,
                                               IsJoinDays = s.IsJoinDays ?? false,
                                               JoinAfterDaysOrMonth = s.JoinAfterDaysOrMonth ?? 0,
                                               Mobile = s.MobileNo,
                                               DepartmentId = s.DepartmentId,
                                               DesignationId = s.DesignationId,
                                               EducationId = s.EducationId,
                                               InterviewDate = s.InterviewDate,
                                               InterviewTime = s.InterviewTime,
                                               JoinDate = s.JoinDate,
                                               Reason = s.Reason,
                                               Name = s.Name,
                                               Email = s.Email,
                                               PersonalDetail = s.PersonalDetail,
                                               ListOfInterviewAttachment = (dbContext.InterviewAttachments.Where(x => x.InterviewId == s.InterviewID && x.IsActive == true)
                                               .Select(a => new InterviewAttachmentModel
                                               {
                                                   AttachmentName = a.Name,
                                                   AttachmentType = a.AttachmentType,
                                                   IsDelete = false,
                                                   InterviewAttechmentMapID = a.InterviewAttachmentMapID,
                                               })).ToList(),
                                           });
                    if (_Query != null)
                    {
                        _Result.Data = _Query.FirstOrDefault();
                        _Result.IsSuccess = true;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> DeleteInterviewById(Guid p_InterviewId, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    InterviewMaster _InterviewMaster = dbContext.InterviewMasters.Where(x => x.InterviewID == p_InterviewId && x.IsActive == true).FirstOrDefault();
                    if (_InterviewMaster != null)
                    {
                        _InterviewMaster.IsActive = false;
                        _InterviewMaster.ModifiedBy = p_UserId;
                        _InterviewMaster.ModifiedDate = DateTime.Now;
                        dbContext.SaveChanges();
                        _Result.IsSuccess = true;
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
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<int> GetTodayInterviewCount()
        {
            Result<int> _Result = new Result<int>();
            DateTime _TodayDate = DateTime.Now.Date;
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = dbContext.InterviewMasters.Where(x => x.IsActive == true && x.InterviewDate == _TodayDate).Count();
                    if (_Query != null)
                    {
                        _Result.Data = _Query;
                        _Result.IsSuccess = true;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> SaveInterview(Interview p_Interview, Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    InterviewMaster _InterviewMaster = new InterviewMaster();
                    if (p_Interview.InterviewID == Guid.Empty)
                    {
                        _InterviewMaster.InterviewID = Guid.NewGuid();
                        _InterviewMaster.IsActive = true;
                        _InterviewMaster.CreatedDate = DateTime.Now;
                        _InterviewMaster.CreatedBy = p_UserId;
                        _InterviewMaster.ModifiedDate = DateTime.Now;
                        _InterviewMaster.InterviewNo = dbContext.InterviewMasters.DefaultIfEmpty().Max(x => x == null ? 0 : x.InterviewNo) + 1;
                        _InterviewMaster.InterviewStatusId = (int)InterviewStatus.Pending;
                    }
                    else
                    {
                        _InterviewMaster = dbContext.InterviewMasters.Where(x => x.InterviewID == p_Interview.InterviewID && x.IsActive == true).FirstOrDefault();
                        _InterviewMaster.ModifiedBy = p_UserId;
                        _InterviewMaster.ModifiedDate = DateTime.Now;
                    }
                    _InterviewMaster.CurrentSalary = p_Interview.CurrentSalary;
                    _InterviewMaster.DepartmentId = p_Interview.DepartmentId;
                    _InterviewMaster.DesignationId = p_Interview.DesignationId;
                    _InterviewMaster.EducationId = p_Interview.EducationId;
                    _InterviewMaster.Email = p_Interview.Email;
                    _InterviewMaster.ExpectedSalary = p_Interview.ExpectedSalary;
                    _InterviewMaster.ExperienceMonth = p_Interview.ExperienceMonth;
                    _InterviewMaster.ExperienceYear = p_Interview.ExperienceYear;
                    _InterviewMaster.IsJoinDays = p_Interview.IsJoinDays;
                    _InterviewMaster.JoinAfterDaysOrMonth = p_Interview.JoinAfterDaysOrMonth;
                    _InterviewMaster.MobileNo = p_Interview.Mobile;
                    _InterviewMaster.Name = p_Interview.Name;
                    _InterviewMaster.PersonalDetail = p_Interview.PersonalDetail;
                    _InterviewMaster.InterviewStatusId = p_Interview.InterviewStatusId;
                    _InterviewMaster.Reason = p_Interview.Reason;
                    if (p_Interview.InterviewStatusId == (int)InterviewStatus.Will_Join)
                    {
                        _InterviewMaster.JoinDate = p_Interview.JoinDate;
                    }
                    if (p_Interview.InterviewStatusId == (int)InterviewStatus.Interview_Scheduled)
                    {
                        _InterviewMaster.InterviewDate = p_Interview.InterviewDate;
                        _InterviewMaster.InterviewTime = p_Interview.InterviewTime;
                    }

                    if (p_Interview.InterviewID == Guid.Empty)
                    {
                        dbContext.InterviewMasters.Add(_InterviewMaster);
                    }
                    dbContext.SaveChanges();

                    if (p_Interview.ListOfInterviewAttachment != null)
                    {
                        foreach (InterviewAttachmentModel _InterviewAttachments in p_Interview.ListOfInterviewAttachment)
                        {
                            if (_InterviewAttachments.IsDelete)
                            {
                                dbContext.InterviewAttachments.Remove(dbContext.InterviewAttachments.Where(ea => ea.AttachmentType == _InterviewAttachments.AttachmentType).FirstOrDefault());
                            }
                            else
                            {
                                InterviewAttachment _InterviewAttachment = new InterviewAttachment();

                                _InterviewAttachment = dbContext.InterviewAttachments.Where(e => e.InterviewId == _InterviewMaster.InterviewID && e.Name == _InterviewAttachments.AttachmentName).FirstOrDefault();

                                if (_InterviewAttachment != null)
                                {
                                    _InterviewAttachment.ModifiedDate = DateTime.Now;
                                    _InterviewAttachment.ModifiedBy = p_UserId;
                                    _InterviewAttachment.Name = _InterviewAttachments.AttachmentName;
                                    _InterviewAttachment.AttachmentType = _InterviewAttachments.AttachmentType;
                                }
                                else
                                {
                                    _InterviewAttachment = new InterviewAttachment();

                                    _InterviewAttachment.InterviewAttachmentMapID = Guid.NewGuid();
                                    _InterviewAttachment.IsActive = true;
                                    _InterviewAttachment.CreatedDate = DateTime.Now;
                                    _InterviewAttachment.CreatedBy = p_UserId;
                                    _InterviewAttachment.ModifiedDate = DateTime.Now;
                                    _InterviewAttachment.InterviewId = _InterviewMaster.InterviewID;
                                    _InterviewAttachment.Name = _InterviewAttachments.AttachmentName;
                                    _InterviewAttachment.AttachmentType = _InterviewAttachments.AttachmentType;

                                    dbContext.InterviewAttachments.Add(_InterviewAttachment);
                                }
                            }
                            dbContext.SaveChanges();
                        }
                    }
                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_InterviewMaster.InterviewID);
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = _Exception.Message;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

    }
}
