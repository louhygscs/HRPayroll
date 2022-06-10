using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
   public interface IDeviceService
    {
        Result<List<DeviceModel>> GetDeviceList();

        Result<DeviceModel> GetDeViceById(Guid p_Id);

        Result<bool> SaveDevice(DeviceModel p_DeviceModel, Guid p_UserId);

        Result<bool> DeleteDeviceById(Guid p_DeviceId, Guid p_UserId);

        Result<int> GetDeviceCount();

    }
}
