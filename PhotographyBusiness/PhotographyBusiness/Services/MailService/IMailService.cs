﻿using MimeKit;
using PhotographyBusiness.Models;
using System.Net.Mail;

namespace PhotographyBusiness.Services.MailService
{
    public interface IMailService
    {
        Task SendMail(MimeMessage message);
        Task SendRequestMail(Booking booking);
        Task SendConfirmationMail(Booking booking);
        Task SendCancellationMail(Booking booking);
        Task SendUserCreationEmail(string email, string name);

    }
}
