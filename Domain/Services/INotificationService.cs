using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(NotificationRequest request, CancellationToken cancellationToken = default);
    }
}
