using System.Net;
using System.Net.Mail;
using SocialMedia.business.Abstract;

namespace SocialMedia.business.Concrete
{
    public class EmailManager : IEmailServices
    {
        public void SendEmail(string toEmailAddress, string newPassword)
        {
            string fromEmailAddress ="Gopost78@outlook.com";
            string fromPassword ="Gopost123";

            string subject = "Şifre Değiştirme";
            string body = "Yeni şifreniz "+newPassword;

            string smtpServer = "smtp.office365.com";
            int smtpPort = 587;

            NetworkCredential networkCredential = new NetworkCredential(fromEmailAddress,fromPassword);

            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port=smtpPort,
                Credentials=networkCredential,
                EnableSsl=true
            };

            MailMessage mailMessage = new MailMessage(fromEmailAddress,toEmailAddress,subject,body);

            try{
                smtpClient.Send(mailMessage);
                Console.WriteLine("Mesaj başarıyla gönderildi!");
            }
            catch(Exception ex){
                Console.WriteLine("Mesaj gönderirken hata aldı!");
                Console.WriteLine(ex);
            }
            finally{
                smtpClient.Dispose();
                mailMessage.Dispose();
            }

        }
    }
}