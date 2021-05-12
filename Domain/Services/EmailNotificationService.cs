using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly SmtpClient smtpClient;
        private readonly ILogger<EmailNotificationService> logger;

        public EmailNotificationService(SmtpClient smtpClient, ILogger<EmailNotificationService> logger)
        {
            this.smtpClient = smtpClient;
            this.logger = logger;
        }

        public Task SendNotificationAsync(NotificationRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is not EmailNotification emailRequest)
            {
                throw new InvalidOperationException($"Sending emails supported only for EmailNotification.");
            }

            logger.LogInformation($"Sending email to {emailRequest.To} about {emailRequest.Subject} ...");

            return smtpClient.SendMailAsync(emailRequest.From, emailRequest.To, emailRequest.Subject, emailRequest.Body,
                cancellationToken);
        }
    }
}
