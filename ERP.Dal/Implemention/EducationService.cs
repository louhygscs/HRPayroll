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
    public class EducationService : IEducationService
    {
        public Result<List<Education>> GetEducationList()
        {
            Result<List<Education>> _Result = new Result<List<Education>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from e in dbContext.EducationMasters
                                 where e.IsActive == true
                                 select new Education
                                 {
                                     EducationID = e.EducationID,
                                     EducationName = e.EducationName
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

        public Result<Boolean> DeleteEducationById(Guid p_EducationId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.InterviewMasters.Where(e => e.EducationId == p_EducationId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        EducationMaster _EducationMaster = dbContext.EducationMasters.Where(d => d.EducationID == p_EducationId).FirstOrDefault();

                        if (_EducationMaster != null)
                        {
                            _EducationMaster.IsActive = false;
                            _EducationMaster.ModifiedDate = DateTime.Now;
                            _EducationMaster.ModifiedBy = p_UserId;

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

        public Result<Education> GetEducationById(Guid p_EducationId)
        {
            Result<Education> _Result = new Result<Education>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.EducationMasters
                                 where d.EducationID == p_EducationId
                                 select new Education
                                 {
                                     EducationID = d.EducationID,
                                     EducationName = d.EducationName,
                                 };

                    Education _Education = _Query.FirstOrDefault();
                    if (_Education != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Education;
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

        public Result<bool> SaveEducation(Education p_Education,Guid p_UserId)
        {
            Result<bool> _Result = new Result<bool>();
            using (var dbContext = new ERPEntities())
            {
                EducationMaster _EducationMasterExist = dbContext.EducationMasters.Where(x => x.EducationID != p_Education.EducationID && x.IsActive == true && x.EducationName == p_Education.EducationName).FirstOrDefault();
                if (_EducationMasterExist == null)
                {
                    EducationMaster _EducationMaster = new EducationMaster();

                    if (p_Education.EducationID == Guid.Empty)
                    {
                        _EducationMaster.EducationID = Guid.NewGuid();
                        _EducationMaster.IsActive = true;
                        _EducationMaster.CreatedDate = DateTime.Now;
                        _EducationMaster.CreatedBy = p_UserId;
                        _EducationMaster.ModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        _EducationMaster = dbContext.EducationMasters.Where(e => e.EducationID == p_Education.EducationID).FirstOrDefault();

                        _EducationMaster.ModifiedDate = DateTime.Now;
                        _EducationMaster.ModifiedBy = p_UserId;
                    }

                    _EducationMaster.EducationName = p_Education.EducationName;

                    if (p_Education.EducationID == Guid.Empty)
                    {
                        dbContext.EducationMasters.Add(_EducationMaster);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_EducationMaster.EducationID);
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
    }
}
