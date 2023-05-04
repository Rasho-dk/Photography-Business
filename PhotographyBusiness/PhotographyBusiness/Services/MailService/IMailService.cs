using PhotographyBusiness.Models;
using System.Net.Mail;

namespace PhotographyBusiness.Services.MailService
{
    public interface IMailService
    {
        Task SendMail(MailMessage message);
        Task SendRequestMail(Booking booking);
        Task SendConfirmationMail(Booking booking);
        Task SendCancellationMail(Booking booking);

    }
}
