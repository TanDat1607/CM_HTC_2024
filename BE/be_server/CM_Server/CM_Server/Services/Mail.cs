using System.Net;
using System.Net.Mail;
using CM_Server.Entity;

namespace CM_Server.Services
{
    public class Mail
    {
        public async Task<bool> SendMail(MailInfo mailinfo)
        {
            try
            {
                // Thiết lập SMTP server và cổng
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;

                // Tạo và cấu hình đối tượng SmtpClient
                SmtpClient smtpClient = new SmtpClient(smtpAddress, portNumber);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(mailinfo.Emailsend, mailinfo.Password);

                // Tạo và cấu hình đối tượng MailMessage
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailinfo.Emailsend);
                mailMessage.To.Add(mailinfo.Emailto);
                mailMessage.Subject = mailinfo.Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = mailinfo.Body;
                
                int attempts = 0;
                bool success = false;

                // Thử gửi email và lặp lại nếu không thành công
                while (attempts < 3 && !success)
                {
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        attempts++;
                        Thread.Sleep(5000); // Chờ 5 giây trước khi thử lại
                    }
                }
                // Nếu gửi thành công, chờ đến 00:00 giờ ngày 1 tháng sau
                if (success)
                {
                    smtpClient.Dispose();
                    return true;
                }
                else
                {
                    // Nếu gửi không thành công sau 3 lần
                    smtpClient.Dispose();
                    return false;

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return false;
            }
        }
    }
}
