using System;
using System.Threading.Tasks;
using MimeKit;
using TreePorts.DTO;

namespace TreePorts.Interfaces.Repositories
{
    public interface IMailService
    {
        Task SendEmailAsync(MimeMessage mimeMessage);

        NotificationMetadata getNotificationMetadata();
    }
}
