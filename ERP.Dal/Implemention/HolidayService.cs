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
    public class HolidayService : IHolidayService
    {
        public Result<List<Holiday>> GetHolidayList()
        {
            Result<List<Holiday>> _Result = new Result<List<Holiday>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from h in dbContext.HolidayMasters
                                 where h.IsActive == true
                                 select new Holiday
                                 {
                                     HolidayID = h.HolidayID,
                                     Title = h.Title,
                                     Description = h.Description,
                                     StartDate = h.StartDate ?? DateTime.Now,
                                     EndDate = h.EndDate ?? DateTime.Now
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

        public Result<List<Holiday>> GetHolidayListByDate(DateTime p_FromDate, DateTime p_ToDate)
        {
            Result<List<Holiday>> _Result = new Result<List<Holiday>>();

            try
            {
                _Result.IsSuccess = false;
                using (var dbContext = new ERPEntities())
                {
                    var _Query = from h in dbContext.HolidayMasters
                                 where h.IsActive == true && h.StartDate >= p_FromDate && h.StartDate <= p_ToDate
                                 select new Holiday
                                 {
                                     Title = h.Title,
                                     StartDate = h.StartDate ?? DateTime.Now,
                                     EndDate = h.EndDate ?? DateTime.Now
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

        public Result<Boolean> DeleteHolidayById(Guid p_HolidayId, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    HolidayMaster _HolidayMaster = dbContext.HolidayMasters.Where(h => h.HolidayID == p_HolidayId).FirstOrDefault();

                    if (_HolidayMaster != null)
                    {
                        _HolidayMaster.IsActive = false;
                        _HolidayMaster.ModifiedDate = DateTime.Now;
                        _HolidayMaster.ModifiedBy = p_UserId;

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

        public Result<Holiday> GetHolidayById(Guid p_HolidayId)
        {
            Result<Holiday> _Result = new Result<Holiday>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    var _Query = from h in dbContext.HolidayMasters
                                 where h.HolidayID == p_HolidayId
                                 select new Holiday
                                 {
                                     HolidayID = h.HolidayID,
                                     Title = h.Title,
                                     Description = h.Description,
                                     StartDate = h.StartDate ?? DateTime.Now,
                                     EndDate = h.EndDate ?? DateTime.Now
                                 };

                    Holiday _Holiday = _Query.FirstOrDefault();
                    if (_Holiday != null)
                    {
                        _Result.IsSuccess = true;
                        _Result.Data = _Holiday;
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

        public Result<Boolean> SaveHoliday(Holiday p_Holiday, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();
            try
            {
                _Result.IsSuccess = false;

                using (var dbContext = new ERPEntities())
                {
                    HolidayMaster _HolidayMaster = new HolidayMaster();

                    if (p_Holiday.HolidayID == Guid.Empty)
                    {
                        _HolidayMaster.HolidayID = Guid.NewGuid();
                        _HolidayMaster.IsActive = true;
                        _HolidayMaster.CreatedDate = DateTime.Now;
                        _HolidayMaster.CreatedBy = p_UserId;
                        _HolidayMaster.ModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        _HolidayMaster = dbContext.HolidayMasters.Where(h => h.HolidayID == p_Holiday.HolidayID).FirstOrDefault();

                        _HolidayMaster.ModifiedDate = DateTime.Now;
                        _HolidayMaster.ModifiedBy = p_UserId;
                    }

                    _HolidayMaster.Title = p_Holiday.Title;
                    _HolidayMaster.Description = p_Holiday.Description;
                    _HolidayMaster.StartDate = p_Holiday.StartDate;
                    _HolidayMaster.EndDate = p_Holiday.EndDate;

                    if (p_Holiday.HolidayID == Guid.Empty)
                    {
                        dbContext.HolidayMasters.Add(_HolidayMaster);
                    }

                    dbContext.SaveChanges();

                    _Result.IsSuccess = true;
                    _Result.Id = Convert.ToString(_HolidayMaster.HolidayID);
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

        public Result<Boolean> GenerateHoliday(int HolidayYear, Guid p_UserId)
        {
            Result<Boolean> _Result = new Result<Boolean>();

            try
            {
                List<HolidayMaster> listHoliday = new List<HolidayMaster>();

                HolidayMaster item1  = new HolidayMaster() { Title = "New Year's Day"                        , Description = "New Year's Day"                        , StartDate = new DateTime(HolidayYear, 1, 1)   , EndDate = new DateTime(HolidayYear, 1, 1)   , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item2  = new HolidayMaster() { Title = "Lunar New Year"                        , Description = "Lunar New Year"                        , StartDate = new DateTime(HolidayYear, 2, 1)   , EndDate = new DateTime(HolidayYear, 2, 1)   , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item3  = new HolidayMaster() { Title = "Maundy Thursday"                       , Description = "Maundy Thursday"                       , StartDate = new DateTime(HolidayYear, 4, 1)   , EndDate = new DateTime(HolidayYear, 4, 1)   , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item4  = new HolidayMaster() { Title = "Good Friday"                           , Description = "Good Friday"                           , StartDate = new DateTime(HolidayYear, 4, 2)   , EndDate = new DateTime(HolidayYear, 4, 2)   , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item5  = new HolidayMaster() { Title = "Bataan Day"                            , Description = "Bataan Day"                            , StartDate = new DateTime(HolidayYear, 4, 9)   , EndDate = new DateTime(HolidayYear, 4, 9)   , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item6  = new HolidayMaster() { Title = "Labour Day"                            , Description = "Labour Day"                            , StartDate = new DateTime(HolidayYear, 5, 1)   , EndDate = new DateTime(HolidayYear, 5, 1)   , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item7  = new HolidayMaster() { Title = "Eid al-Fitr"                           , Description = "Eid al-Fitr"                           , StartDate = new DateTime(HolidayYear, 5, 13)  , EndDate = new DateTime(HolidayYear, 5, 13)  , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item8  = new HolidayMaster() { Title = "Philippines Independence Day"          , Description = "Philippines Independence Day"          , StartDate = new DateTime(HolidayYear, 6, 12)  , EndDate = new DateTime(HolidayYear, 6, 12)  , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item9  = new HolidayMaster() { Title = "National Heroes' Day (in Philippines)" , Description = "National Heroes' Day (in Philippines)" , StartDate = new DateTime(HolidayYear, 6, 19)  , EndDate = new DateTime(HolidayYear, 6, 20)  , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item10 = new HolidayMaster() { Title = "National Heroes' Day (in Philippines)" , Description = "Bonifacio Day"                         , StartDate = new DateTime(HolidayYear, 8, 30)  , EndDate = new DateTime(HolidayYear, 8, 30)  , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item11 = new HolidayMaster() { Title = "Bonifacio Day"                         , Description = "Feast of the Immaculate Conception"    , StartDate = new DateTime(HolidayYear, 11, 30) , EndDate = new DateTime(HolidayYear, 11, 30) , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item12 = new HolidayMaster() { Title = "Feast of the Immaculate Conception"    , Description = "Feast of the Immaculate Conception"    , StartDate = new DateTime(HolidayYear, 12, 8)  , EndDate = new DateTime(HolidayYear, 12, 8)  , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item13 = new HolidayMaster() { Title = "Christmas Day"                         , Description = "Christmas Day"                         , StartDate = new DateTime(HolidayYear, 12, 25) , EndDate = new DateTime(HolidayYear, 12, 25) , HolidayID = Guid.NewGuid(), IsActive = true };
                HolidayMaster item14 = new HolidayMaster() { Title = "Rizal Day"                             , Description = "Rizal Day"                             , StartDate = new DateTime(HolidayYear, 12, 30) , EndDate = new DateTime(HolidayYear, 12, 30) , HolidayID = Guid.NewGuid(), IsActive = true };

                listHoliday.Add(item1);
                listHoliday.Add(item2);
                listHoliday.Add(item3);
                listHoliday.Add(item4);
                listHoliday.Add(item5);
                listHoliday.Add(item6);
                listHoliday.Add(item7);
                listHoliday.Add(item8);
                listHoliday.Add(item9);
                listHoliday.Add(item10);
                listHoliday.Add(item11);
                listHoliday.Add(item12);
                listHoliday.Add(item13);
                listHoliday.Add(item14);

                using (var dbContext = new ERPEntities())
                {

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
    }
}
