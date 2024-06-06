namespace CMS_back.Mailing
{
    public interface IMailingService
    {
        void SendMail(MailMessage message);
    }
}
