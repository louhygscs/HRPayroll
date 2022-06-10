using ERP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Helpers
{
    public static class EmailHelper
    {
        #region Variables

        private readonly static log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Methods

        public static bool SendMail(string p_Subject, string p_Body, List<string> p_ToMailIds)
        {
            bool flag = false;
            try
            {
                MailMessage mail = new MailMessage();
                foreach (string toMailId in p_ToMailIds)
                {
                    if (!string.IsNullOrEmpty(toMailId))
                    {
                        mail.To.Add(toMailId);
                    }
                }

                mail.From = new MailAddress(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]));
                mail.Subject = p_Subject;
                string Body = p_Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPHost"]);
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UserEmailID"]), Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UserEmailPassword"]));
                smtp.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSSL"]);
                smtp.Send(mail);
                flag = true;
            }
            catch (Exception _Exception)
            {
                flag = false;
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }
            return flag;
        }

        #endregion

    }
}