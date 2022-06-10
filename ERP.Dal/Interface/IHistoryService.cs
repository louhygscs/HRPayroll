using ERP.Common;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IHistoryService
    {
        void InsertHistory<T>(string p_TableId, TableType p_TableType, OperationType p_OperationType, T p_ToSerialize, Guid? p_UserId);

        Result<bool> SaveIpInformation(IpInformationModel p_IpInformationModel);

    }
}
