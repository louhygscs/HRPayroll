using System;

namespace ERP.Model
{
    public class DeviceModel
    {
         public Guid DeviceID { get; set; }

        public string DeviceName { get; set; }

        public string Address { get; set; }

        public string DeviceCode { get; set; }

        public string PhoneNo { get; set; }

        public int Port { get; set; }

        public string IPAddress { get; set; }

        public string ConnectionStatus { get; set; }

    }
}
