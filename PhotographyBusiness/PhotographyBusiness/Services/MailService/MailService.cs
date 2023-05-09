using PhotographyBusiness.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit;

namespace PhotographyBusiness.Services.MailService
{
    public class MailService : IMailService
    {
        private string _sender = "vgroupdev@outlook.com";
        private string _senderPW = "VgroupFTW!";
        private string _jackEmail = "silasrasch@gmail.com";

        public MailService() {  }

        /// <summary>
        /// Internal helper-method for connecting to SMTP and sending emails asynchronously
        /// </summary>
        /// <param name="message">A MimeMessage with sender, receiver, subject, and body</param>
        /// <returns>Void</returns>
        /// <exception cref="Exception"></exception>
        public async Task SendMail(MimeMessage message)
        {
            using (SmtpClient client = new SmtpClient()) 
            {
                try
                {
                    await client.ConnectAsync("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_sender, _senderPW);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Exception handling
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        public async Task SendCancellationMail(Booking booking)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jack Saunders Photography", _sender));
            message.To.Add(MailboxAddress.Parse(booking.User.Email));
            message.Subject = $"Your booking has been cancelled {booking.BookingId}";
            message.Body = new TextPart("plain")
            { Text = $@"Dear {booking.User.Name},
            We are sorry to inform you that your booking ID: {booking.BookingId} on {booking.Date} has been cancelled.
            Kind Regards, Jack Saunders Photography"
            };
                

            await SendMail(message);
        }

        public async Task SendConfirmationMail(Booking booking)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jack Saunders Photography", _sender));
            message.To.Add(MailboxAddress.Parse(booking.User.Email));
            message.Subject = $"Booking confirmation {booking.BookingId}";
            message.Body = new TextPart("plain")
            {
                Text = $@"Dear {booking.User.Name},
We are happy to inform you that your booking {booking.BookingId} on {booking.Date} has been confirmed
Kind Regards, Jack Saunders Photography"
            };
            await SendMail(message);
        }

        public async Task SendRequestMail(Booking booking)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jack Saunders Photography", _sender));
            message.To.Add(MailboxAddress.Parse(_jackEmail));
            message.Subject = $"You have a new booking enquiry!";
            message.Body = new TextPart("plain")
            {
                Text = $@"You have a new booking enquiry - ID {booking.BookingId}
You can view it on your admin dashboard now under Booking Enquiries.
Name: {booking.User.Name}
Email: {booking.User.Email}
Phone: {booking.Address}"
            };
                
            await SendMail(message);
        }

        public async Task SendUserCreationEmail(string email, string name)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jack Saunders Photography", _sender));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = $"Jack Saunders Photography - Account verification";
            message.Body = new TextPart("plain")
            {
                Text = $@"An account has been created on Jack Saunders Photography using this Email 
Username: {name}"
            };     

            await SendMail(message);
        }
    }
}
