using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using TreePorts.DTO;
using TreePorts.Interfaces.Repositories;

namespace TreePorts.Repositories
{
    public class MailService : IMailService
    {

		private readonly NotificationMetadata _notificationMetadata;
		public MailService(IOptions<NotificationMetadata> notificationMetadata)
		{
			_notificationMetadata = notificationMetadata.Value;
		}

        public NotificationMetadata getNotificationMetadata()
        {
			return _notificationMetadata;
        }

        public async Task SendEmailAsync(MimeMessage mimeMessage)
        {
			using (SmtpClient smtpClient = new SmtpClient())
			{
				await smtpClient.ConnectAsync(_notificationMetadata.SmtpServer,
				_notificationMetadata.Port, true);
				await smtpClient.AuthenticateAsync(_notificationMetadata.UserName,
				_notificationMetadata.Password);
				await smtpClient.SendAsync(mimeMessage);
				await smtpClient.DisconnectAsync(true);
			}
		}
    }
}
