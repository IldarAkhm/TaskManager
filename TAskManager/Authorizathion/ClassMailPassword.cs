using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Authorizathion
{
    internal class ClassMailPassword
    {
        public string senderMeial = "komanda_a_2023@mail.ru";
        public string recipientEmail;
        public string text = "";
        public string problem;
        public string status = "";
        private string password = "Korol1928";
        private string Code = "ILDHALADE";

        public ClassMailPassword() { }
        public ClassMailPassword(string email)
        {
            this.recipientEmail = email;
        }
        public ClassMailPassword(string email, string text, string problem, string status)
        {
            this.recipientEmail = email;
            this.text = text;
            this.problem = problem;
            this.status = status;
        }
        public void MailMessagee()
        {
            string bodyy = $"<b>Ваша проблема: {problem}</b?<br> <b>{text}</b> Статус:<b>{status}</b>.";
            if (status != "Отправлено в отдел распределения" && text != "")
            {
                bodyy = $"<b>Пришел ответ по вашей проблеме {problem}</b?<br> <b>{text}</b> Статус:<b>{status}</b>.";
            }
            else if (status == "Отправлено в отдел распределения" && text != "")
            {
                bodyy = $"<b>Пришел ответ по вашей проблеме {problem}</b?<br> <b>{text}</b> Статус:<b>{status}</b>.";
            }
            
            SmtpClient smtpClient = new SmtpClient("smtp.mail.ru");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            NetworkCredential networkCredential = new NetworkCredential(senderMeial, "6bnqHkGWy6ShpnrHL5wm");
            smtpClient.Credentials = networkCredential;

            MailAddress from = new MailAddress(senderMeial, "Komanda A");
            MailAddress to = new MailAddress(recipientEmail, "Komanda A");
            MailMessage message = new MailMessage(from, to);

            MailAddress replyto = new MailAddress(senderMeial);
            message.ReplyToList.Add(replyto);

            message.Subject = Code;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            message.Body = bodyy;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            message.IsBodyHtml = true;

            smtpClient.Send(message);
        }
        public void MailMessag()
        {
            
                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                NetworkCredential networkCredential = new NetworkCredential(senderMeial, "6bnqHkGWy6ShpnrHL5wm");
                smtpClient.Credentials = networkCredential;

                MailAddress from = new MailAddress(senderMeial, "Komanda A");
                MailAddress to = new MailAddress(recipientEmail, "Komanda A");
                MailMessage message = new MailMessage(from, to);

                MailAddress replyto = new MailAddress(senderMeial);
                message.ReplyToList.Add(replyto);

                message.Subject = Code;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                message.Body = $"<b>Провека почты.</b?<br> <b>{Code}</b>.";
                message.BodyEncoding= System.Text.Encoding.UTF8;

                message.IsBodyHtml = true;

                smtpClient.Send(message);
        }

    }
}
