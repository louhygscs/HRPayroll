using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Helpers
{
    public static class SessionHelper
    {
        public static SessionDetail SessionDetail
        {
            get
            {
                if (HttpContext.Current.Session["SessionDetail"] == null)
                {
                    return null;
                }
                else
                {
                    return (SessionDetail)HttpContext.Current.Session["SessionDetail"];
                }
            }
            set
            {
                HttpContext.Current.Session["SessionDetail"] = value;
            }
        }

        public static void RemoveSessionDetail()
        {
            HttpContext.Current.Session.Remove("SessionDetail");
        }

        public static string MessageSession
        {
            get
            {
                if (HttpContext.Current.Session["MessageSession"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToString(HttpContext.Current.Session["MessageSession"]);
                }
            }
            set
            {
                HttpContext.Current.Session["MessageSession"] = value;
            }
        }

        public static void RemoveMessageSession()
        {
            HttpContext.Current.Session.Remove("MessageSession");
        }

        public static string SelectMenuSession
        {
            get
            {
                if (HttpContext.Current.Session["SelectMenuSession"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToString(HttpContext.Current.Session["SelectMenuSession"]);
                }
            }
            set
            {
                HttpContext.Current.Session["SelectMenuSession"] = value;
            }
        }

        public static DeviceSessionDetail DeviceSessionDetail
        {
            get
            {
                if (HttpContext.Current.Session["DeviceSessionDetail"] == null)
                {
                    return null;
                }
                else
                {
                    return (DeviceSessionDetail)HttpContext.Current.Session["DeviceSessionDetail"];
                }
            }
            set
            {
                HttpContext.Current.Session["DeviceSessionDetail"] = value;
            }
        }

        public static void RemoveDeviceSessionDetail()
        {
            HttpContext.Current.Session.Remove("DeviceSessionDetail");
        }

        //public static bool CheckPermissionByRole(List<int> p_Roles)
        //{
        //    if (SessionHelper.SessionDetail != null)
        //    {
        //        int _Count = p_Roles.Where(r => r == Convert.ToInt32(SessionHelper.SessionDetail.RoleId)).Count();

        //        if (_Count > 0)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

    }
}