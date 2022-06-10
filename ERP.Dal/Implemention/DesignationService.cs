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
    public class DesignationService : IDesignationService
    {
        public Result<List<Designation>> GetDesignationList()
        {
            Result<List<Designation>> _Result = new Result<List<Designation>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DesignationMasters
                                      where d.IsActive == true
                                      select new Designation
                                      {
                                          DesignationID = d.DesignationID,
                                          DesignationName = d.Designation
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

        public Result<Boolean> DeleteDesignationById(Guid p_DesignationId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    int _Count = dbContext.EmployeeMasters.Where(e => e.DesignationId == p_DesignationId && e.IsActive == true).Count();

                    if (_Count <= 0)
                    {
                        DesignationMaster _DesignationMaster = dbContext.DesignationMasters.Where(d => d.DesignationID == p_DesignationId).FirstOrDefault();

                        if (_DesignationMaster != null)
                        {
                            _DesignationMaster.IsActive = false;
                            _DesignationMaster.ModifiedDate = DateTime.Now;
                            _DesignationMaster.ModifiedBy = p_UserId;

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

        public Result<Designation> GetDesignationById(Guid p_DesignationId)
        {
            Result<Designation> _Result = new Result<Designation>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from d in dbContext.DesignationMasters
                                 where d.DesignationID == p_DesignationId
                                 select new Designation
                                 {
                                     DesignationID = d.DesignationID,
                                     DesignationName = d.Designation,
                                 };

                    Designation _Designation = _Query.FirstOrDefault();
                    if (_Designation != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Designation;
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

        public Result<Boolean> SaveDesignation(Designation p_Designation, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    DesignationMaster _DesignationMasterExist = dbContext.DesignationMasters.Where(d => d.DesignationID != p_Designation.DesignationID && d.Designation == p_Designation.DesignationName && d.IsActive == true).FirstOrDefault();
                    if (_DesignationMasterExist==null)
                    {
                        DesignationMaster _DesignationMaster = new DesignationMaster();
                        if (p_Designation.DesignationID== Guid.Empty)
                        {
                            _DesignationMaster.DesignationID = Guid.NewGuid();
                            _DesignationMaster.IsActive = true;
                            _DesignationMaster.CreatedDate = DateTime.Now;
                            _DesignationMaster.CreatedBy = p_UserId;
                            _DesignationMaster.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            _DesignationMaster = dbContext.DesignationMasters.Where(d => d.DesignationID == p_Designation.DesignationID).FirstOrDefault();

                            _DesignationMaster.ModifiedDate = DateTime.Now;
                            _DesignationMaster.ModifiedBy = p_UserId;
                        }

                        _DesignationMaster.Designation = p_Designation.DesignationName;
                        if (p_Designation.DesignationID == Guid.Empty)
                        {
                            dbContext.DesignationMasters.Add(_DesignationMaster);
                        }

                        dbContext.SaveChanges();

                        _Result.IsSuccess = true;
                        _Result.Id = Convert.ToString(_DesignationMaster.DesignationID);
                        _Result.Data = true;
                    }
                    else
                    {
                        _Result.IsSuccess = false;
                        _Result.Data = false;
                        _Result.Message = GlobalMsg.AlreadyExistMsg;
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
       
    }
}
