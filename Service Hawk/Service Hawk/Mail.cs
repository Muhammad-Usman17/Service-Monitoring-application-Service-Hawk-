﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Service_Hawk
{
    class Mail
    {
        static private Mail obj;
      static  public  Mail func
        {
            get
            {
                if (obj == null)
                    obj = new Mail();
                return obj;
            }
        }
        public void sendNotification(String appAdd, String adminAdd)
        {

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(appAdd, ConfigUpdate.File.GetSetting("Systempassword"));
            client.Port = Int32.Parse(ConfigUpdate.File.GetSetting("port"));
            client.Host = ConfigUpdate.File.GetSetting("mailhost");
            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(appAdd);
            mail.To.Add(adminAdd);
            mail.Subject = ConfigUpdate.File.GetSetting("subject");
            mail.Body = (ConfigUpdate.File.GetSetting("body"));
            try
            {
                client.Send(mail);
                MessageBox.Show("Email sent ");
            }
            catch (SmtpException e)
            {

                MessageBox.Show(e.InnerException.Message);
            }
        }
        public void sendNotification(String body, String appAdd, String adminAdd)
        {

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(appAdd, ConfigUpdate.File.GetSetting("Systempassword"));
            client.Port = Int32.Parse(ConfigUpdate.File.GetSetting("port"));
            client.Host = ConfigUpdate.File.GetSetting("mailhost");
            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(appAdd);
            mail.To.Add(adminAdd);
            mail.Subject = ConfigUpdate.File.GetSetting("subject");
            mail.Body = (body);
            try
            {
                client.Send(mail);

            }
            catch (SmtpException e)
            {

                MessageBox.Show(e.InnerException.Message);
            }
        }
        public bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
 