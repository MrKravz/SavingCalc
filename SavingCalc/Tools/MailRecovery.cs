using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SavingCalc.Tools
{
    class MailRecovery
    {
            public string ToUser = default;
            public int recoveryCode = default;
            public const string FromUser = "SavingCalcRecovery@gmail.com";
            public const string FromUserPass = "SaveYourMoney44";
            public MailRecovery(string toUser)
            {
                ToUser = toUser;
            }
            public MailMessage CreateMessage()
            {
                MailAddress from = new MailAddress(FromUser);
                MailAddress to = new MailAddress(ToUser);
                MailMessage message = new MailMessage(from, to);
                message.Subject = "SavingCalc Recovery";
                Random rnd = new Random();
                recoveryCode = rnd.Next(999, 10000);
                message.Body = $"Recovery code is {recoveryCode} if you dont request password change just ignore this message";
                return message;
            }
            public void SendMail(MailMessage message)
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(FromUser, FromUserPass),
                    Timeout = 20000
                };
                smtp.Send(message);
            }
        }
}
