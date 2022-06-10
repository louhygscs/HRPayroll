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
    public class HistoryService : IHistoryService
    {
        public void InsertHistory<T>(string p_TableId, TableType p_TableType, OperationType p_OperationType, T p_ToSerialize, Guid? p_UserId)
        {
            using (var dbContext = new ERPEntities())
            {
                History _History = new History();

                _History.HistoryID = Guid.NewGuid();
                _History.TableId = p_TableId;
                _History.TableTypeId = Convert.ToInt32(p_TableType);
                _History.OperationTypeId = Convert.ToInt32(p_OperationType);
                _History.UserId = p_UserId;
                _History.CreatedDate = DateTime.Now;
                _History.Description = p_TableType.ToString() + " " + p_OperationType.ToString();
                _History.XmlContent = GlobalHelper.XMLSerializeObject<T>(p_ToSerialize);
                _History.IPAddress = GlobalHelper.GetIPAddress();

                dbContext.Histories.Add(_History);
                dbContext.SaveChanges();
            }
        }

        public Result<bool> SaveIpInformation(IpInformationModel p_IpInformationModel)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                using (var dbContext = new ERPEntities())
                {
                    IpInformation _IpInformation = new IpInformation();
                    if (!string.IsNullOrEmpty(p_IpInformationModel.IpAddress) && !string.IsNullOrEmpty(p_IpInformationModel.DeviceName))
                    {
                        _IpInformation.IpAddress = p_IpInformationModel.IpAddress;
                        _IpInformation.DeviceName = p_IpInformationModel.DeviceName;
                        _IpInformation.BrowserName = p_IpInformationModel.BrowserName;
                        _IpInformation.DeviceType = p_IpInformationModel.DeviceType;
                        _IpInformation.Id = Guid.NewGuid();
                        _IpInformation.CreatedDate = DateTime.Now;
                        dbContext.IpInformations.Add(_IpInformation);
                        dbContext.SaveChanges();
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
    }
}
