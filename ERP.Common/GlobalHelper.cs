using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace ERP.Common
{
    public static class GlobalHelper
    {
        public static string ApiHeaderToken = "dAbdfdXNlcm5hbW5Szc3ghdfdvcmQ=";
        public static string GetEnumDescription(Enum p_Value)
        {
            FieldInfo _FieldInfo = p_Value.GetType().GetField(p_Value.ToString());

            DescriptionAttribute[] _Attributes =
                (DescriptionAttribute[])_FieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (_Attributes != null &&
                _Attributes.Length > 0)
                return _Attributes[0].Description;
            else
                return p_Value.ToString();
        }

        public static string XMLSerializeObject<T>(this T p_ToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(p_ToSerialize.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, p_ToSerialize);
            return textWriter.ToString();
        }

        public static string GetIPAddress()
        {
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName());
            System.Net.IPAddress[] _IPAddress = ipEntry.AddressList;

            if (_IPAddress.Length > 0)
            {
                return _IPAddress[0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static DateTime StringToDateTime(string p_InputDate)
        {
            if (!string.IsNullOrEmpty(p_InputDate.Trim()))
            {
                string[] _SplitDateTime = p_InputDate.Trim().Split(' ');

                if (_SplitDateTime.Length > 2)
                {
                    string[] _SplitDate = _SplitDateTime[0].Trim().Split('/');
                    if (p_InputDate.Trim().Contains('-'))
                    {
                        _SplitDate = p_InputDate.Trim().Split('-');
                    }
                    if (_SplitDate.Length > 2)
                    {
                        return Convert.ToDateTime(_SplitDate[2] + "-" + _SplitDate[0] + "-" + _SplitDate[1] + " " + _SplitDateTime[1] + " " + _SplitDateTime[2]);
                    }
                }

            }

            return DateTime.Now;
        }

        public static DateTime StringToDate(string p_InputDate)
        {
            if (!string.IsNullOrEmpty(p_InputDate.Trim()))
            {
                string[] _SplitDate = p_InputDate.Trim().Split('/');
                if (p_InputDate.Trim().Contains('-'))
                {
                    _SplitDate = p_InputDate.Trim().Split('-');
                }

                if (_SplitDate.Length > 2)
                {
                    return Convert.ToDateTime(_SplitDate[2] + "-" + _SplitDate[0] + "-" + _SplitDate[1]);
                }
            }

            return DateTime.Now;
        }

        public static void Connect(string p_IPAddress)
        {
            try
            {
                Ping _Ping = new Ping();
                _Ping.PingCompleted += (sender, e) =>
                {
                    if (e.Reply.Status == IPStatus.Success)
                    {
                    }

                };
                _Ping.SendAsync(p_IPAddress, 3000, null);
            }
            catch(Exception e) {  }
        }


    }
}
