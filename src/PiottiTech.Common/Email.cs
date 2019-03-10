using System;
using System.Net.Mail;

namespace PiottiTech.Common
{
    public class Email
    {
        public static Result Send(MailMessage mailMessage)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Result result = new Result("Send email failed.", ex);
                Logger.Log(result);
                return result;
            }
            return new Result(true);
        }
    }
}