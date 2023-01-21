using System.Net.Mail;

namespace Csharp_Exam.Services
{
    public class E_mailService
    {
        //      Tested and working
        public void SendEmail(string fileName)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("UAB_Skanaus@gmail.com");
            mail.To.Add("CustommerEmail@gmail.com");                //Have to be valid E-mail
            mail.Subject = $"From: UAB Skanaus: {fileName}";
            mail.Body = "Please find the attached check\n See you next time";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(fileName);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            //Have to be valid E-mail and Password. Need to create it on you google acc under: App passwords
            SmtpServer.Credentials = new System.Net.NetworkCredential("enter_your@gmail.com", "Enter_YourApp_password");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
