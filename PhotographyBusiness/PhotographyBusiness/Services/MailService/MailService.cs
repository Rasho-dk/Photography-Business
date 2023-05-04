using PhotographyBusiness.Models;
using System.Net;
using System.Net.Mail;

namespace PhotographyBusiness.Services.MailService
{
    public class MailService : IMailService
    {
        private string _sender = "vgroupdev@outlook.com";
        private string _senderPW = "VgroupFTW!";
        private string _jackEmail = "silasrasch@gmail.com";

        public MailService() {  }

        public async Task SendMail(MailMessage message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_sender, _senderPW)
            };

            await client.SendMailAsync(message);
        }

        public async Task SendCancellationMail(Booking booking)
        {
            MailMessage message = new MailMessage(new MailAddress(_sender), new MailAddress(booking.User.Email));
            message.Subject = $"Your booking has ben cancelled {booking.BookingId}";
            message.Body = $"Dear {booking.User.Name}," +
                $"\nWe are sorry to inform you that your booking ID: {booking.BookingId} from {booking.DateFrom} to {booking.DateTo} has ben cancelled." +
                $"\nKind Regards, Jack Saunders Photography";

            await SendMail(message);
        }

        public async Task SendConfirmationMail(Booking booking)
        {
            MailMessage message = new MailMessage(new MailAddress(_sender), new MailAddress(booking.User.Email));
            message.Subject = $"Booking confirmation {booking.BookingId}";
            message.Body = $"Dear {booking.User.Name}," +
                $"\nWe are happy to inform you that your booking {booking.BookingId} from {booking.DateFrom} to {booking.DateTo} has been confirmed" +
                $"Kind Regards, Jack Saunders Photography";
            
            await SendMail(message);
        }

        

        public async Task SendRequestMail(Booking booking)
        {
            MailMessage message = new MailMessage(new MailAddress(_sender), new MailAddress(_jackEmail));
            message.Subject = $"You have a new booking enquiry!";
            message.Body = $"You have a new booking enquiry - ID {booking.BookingId}" +
                $"\nYou can view it on your admin dashboard now under Booking Enquiries." +
                $"\nName: {booking.User.Name}" +
                $"\nEmail: {booking.User.Email}" +
                $"\nPhone: {booking.Address}";

            await SendMail(message);
        }

        public async Task SendUserCreationEmail(string email, string name)
        {
            MailMessage message = new MailMessage(new MailAddress(_sender), new MailAddress(email));
            message.Subject = $"Jack Saunders Photography - Account verification";
            message.Body = $"An account has been created on Jack Saunders Photography using this Email" +
                $"\nUsername: {name}";

            await SendMail(message);
        }
    }
}
