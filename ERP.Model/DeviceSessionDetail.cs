using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class DeviceSessionDetail
    {
        public Guid DeviceId { get; set; }

        public bool IsConnected { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        public string DeviceName { get; set; }

    }
}
